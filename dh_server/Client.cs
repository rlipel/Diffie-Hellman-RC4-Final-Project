using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace dh_server
{
    /* Client class:
     *      This class represents a single client that is connected to the server.
     *      This class contains all the network functionality regarding to the client: Sending & receiving messages, etc.
     * */

    public class Client
    {
        /* A link to the main form- so we could call general methods from here */
        ServerMain main = null;

        /* Client fields */
        Socket clientSocket;
        public string name;

        /* Client(): Client constructor */
        public Client(Socket sock, ServerMain form)
        {
            main = form;
            clientSocket = sock;

            // Set default client name to its host name:
            IPEndPoint remoteIpEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
            IPAddress clientIP = remoteIpEndPoint.Address;
            name = Dns.GetHostEntry(clientIP).HostName;

            StartReceive();
        }

        /* End(): Dispose the client and kill the connection */
        public void End()
        {
            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket.Dispose();
                clientSocket = null;
                main.OnClientDisconnect(this);
            }
        }

        /* Send(string): Transform the string into bytes and call Send(byte[]) */
        public virtual int Send(string message)
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
            main.OnClientMessage(this, message);
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