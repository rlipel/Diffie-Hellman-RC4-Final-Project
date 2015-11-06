using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace dh_client
{
    /* Connection class:
     *      This class abstracts all connection-related functionality.
     *      Connection represents the connection to the server, and handles sending and receiving from the server.
     * */

    public class Connection
    {
        /* A link to the main form- so we could call general methods from here */
        protected ClientMain main = null;

        /* Client fields */
        Socket clientSocket;
        string server_ip;
        int server_port;

        /* Connection(): Connection constructor */
        public Connection(string ipaddress, int port, ClientMain form)
        {
            main = form;

            clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            server_ip = ipaddress;
            server_port = port;
        }

        /* Connect(): Attempt to connect to server */
        public bool Connect()
        {
            try
            {
                clientSocket.Connect(server_ip, server_port);
            }
            catch
            {
                return false;
            }

            StartReceive();
            return true;
        }

        /* End(): Disconnect and dispose */
        public void End()
        {
            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket.Dispose();
                clientSocket = null;
                main.OnDisconnect();
            }
        }

        /* Send(string): Transform the string into bytes and call Send(byte[]) */
        public int Send(string message)
        {
            return Send(Encoding.ASCII.GetBytes(message));
        }

        /* Send(byte[]): Send the data to the client */
        public virtual int Send(byte[] data)
        {
            // Defines the length of the data with a 2-byte header attached to the beginning of the packet:
            int datalen = data.Length;
            byte[] lengthHeader = new byte[2];
            lengthHeader[0] = (byte)(datalen & 0xFF);
            lengthHeader[1] = (byte)((datalen >> 8) & 0xFF);

            // Header ready, attach it to the data:
            byte[] result = new byte[2 + datalen];
            lengthHeader.CopyTo(result, 0);
            data.CopyTo(result, 2);

            // Final packet ready. Send
            clientSocket.BeginSend(result, 0, result.Length, 0,
                new AsyncCallback(SendCallback), "state");
            return data.Length;
        }

        /* HandleMessage(): Handle method for incoming messages; forward it to the main program */
        protected virtual void HandleMessage(byte[] messageData)
        {
            string message = Encoding.ASCII.GetString(messageData);
            main.OnServerMessage(message);
        }

        /* StartReceive(): Wait for incoming data from socket */
        void StartReceive()
        {
            byte[] headerBuffer = new byte[2];
            clientSocket.BeginReceive(headerBuffer, 0, 2, SocketFlags.None,
                new AsyncCallback(OnReceiveHeader), headerBuffer);
        }

        /* OnReceiveHeader(): Received a message length header */
        void OnReceiveHeader(IAsyncResult result)
        {
            byte[] header = (byte[])result.AsyncState;
            int receivedBytes = 0;

            try
            {
                receivedBytes = clientSocket.EndReceive(result);
            }
            catch { }

            if (receivedBytes == 0)
            {
                // Connection error
                End();
            }
            else if (receivedBytes == 2)
            {
                // Transform the two length header bytes into an integer, messageSize
                int messageSize = (header[1] << 8) | (header[0] & 0xFF); // Little Endian
                if (messageSize > 0)
                {
                    byte[] messageBuffer = new byte[messageSize];
                    clientSocket.BeginReceive(messageBuffer, 0, messageSize, SocketFlags.None,
                        new AsyncCallback(OnReceiveMessage), messageBuffer);
                }
            }
        }

        /* OnReceiveMessage(): Received a message after length header */
        void OnReceiveMessage(IAsyncResult result)
        {
            byte[] message = (byte[])result.AsyncState;
            clientSocket.EndReceive(result);
            HandleMessage(message);
            StartReceive();
        }

        /* Socket asynchronous send callback */
        void SendCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch 
            {
                // Connection error
                End();
            }
        }
    }
}