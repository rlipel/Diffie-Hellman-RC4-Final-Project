using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace dh_server
{
    /* ServerMain class: Main class of the server program
     * Contains the main functionality of the server, accepting clients, forwarding messages,
     * and executing commands of the admin.
     */

    public partial class ServerMain : Form
    {
        /* "Safe" delegates to add listBox items from multiple threads */
        delegate void safelyEdit(string text); // To edit controls from multiple threads

        const string SERVER_IP = "127.0.0.1"; // ** Run server on local host **
        const int SERVER_PORT = 2000;

        TcpListener tcpListener;

        List<Client> clients = new List<Client>();

        /* ServerMain(): Main of the program */
        public ServerMain()
        {
            InitializeComponent();
        }

        /* ServerMain_Load(): On form load, initialize application */
        void ServerMain_Load(object sender, EventArgs e)
        {
            WriteLog(string.Format("Initializing server (address: {0}:{1}) ...", SERVER_IP, SERVER_PORT));

            // Initialize Diffie Hellman parameters:
            DiffieHellmanParams.Initialize();

            // Set up TcpListener..
            tcpListener = new TcpListener(IPAddress.Parse(SERVER_IP), SERVER_PORT);
            tcpListener.Start();
            // Set AcceptCallback to be called when there's an incoming connection
            tcpListener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), "state");

            WriteLog("Server is ready to receive connections.");

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            clientsBox.ContextMenuStrip = clientMenuStrip;
        }

        /* AcceptCallback(): Callback for incoming clients */
        void AcceptCallback(IAsyncResult ar)
        {
            Socket socket = tcpListener.EndAcceptSocket(ar);
            tcpListener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), "state");

            // Create a new client object:
            Client client = new SecureClient(socket, this);
            // Now wait for exchange to finish.. (client to send 'OKAY')
        }

        /* Broadcast(): Send a message to all connected clients */
        void Broadcast(string message)
        {
            foreach (Client client in clients)
            {
                client.Send(message);
            }
        }

        /* OnClientMessage(): Called when a client sent the server a message */
        public void OnClientMessage(Client client, string message)
        {
            if (message.StartsWith("Hey! My name is... :") && clients.IndexOf(client) == -1) // Message that indicates key exchange finish
            {
                // Key exchange complete and connection with client established
                clients.Add(client);

                // Set client name:
                int colon = message.IndexOf(':');
                string newName = message.Substring(colon + 1);
                if (newName.Length >= 3)
                    client.name = newName;

                // Announce new client:
                WriteLog(string.Format("{0} has connected.", client.name));
                Broadcast(client.name + " has connected.");
                AddClientItem(client.name);
                // Send the client the list of online chatters:
                SendClientList(client);
                return;
            }

            // Sent it to all clients:
            string broadcast = string.Format("{0}: {1}", client.name, message);
            Broadcast(broadcast);
            // Diaplay it in the log
            if (broadcast.Length > 40)
                broadcast = broadcast.Substring(0, 40) + "...";
            WriteLog(broadcast);
        }

        /* OnClientMessage(): Called when a client disconnected */
        public void OnClientDisconnect(Client client)
        {
            clients.Remove(client); // Remove the client from the clients list
            WriteLog(string.Format("{0} has disconnected.", client.name));
            Broadcast(client.name + " has disconnected.");
            RemoveClientItem(client.name);
        }

        /* SendClientList(): Send the list of connected clients to a client */
        void SendClientList(Client toClient)
        {
            string listMessage = "CLIENTLIST" + Convert.ToChar(0); // Append CLIENTLIST a 0x0 byte so users can't exploit this
            foreach (Client client in clients)
            {
                listMessage += client.name + ",";
            }

            toClient.Send(listMessage);
        }

        /* When a client in the clients list is clicked */
        private void clientsBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                clientsBox.SelectedIndex = clientsBox.IndexFromPoint(e.Location);
                if (clientsBox.SelectedIndex != -1)
                {
                    clientMenuStrip.Show();
                }
            }
        }

        /* Excecute admin command from server command line ('send' button) */
        void cmdSend_Click(object sender, EventArgs e)
        {
            string msg = cmdLine.Text;
            if (msg.Length > 0)
            {
                cmdLine.Text = "";
                Broadcast("Admin: " + msg);
                WriteLog("You have sent a global message.");
            }
        }

        /* OnProcessExit(): Called when the application exists */
        void OnProcessExit(object sender, EventArgs e)
        {
            tcpListener.Server.Close();
            tcpListener.Stop();
        }

        /* WriteLog(): Write a line to the log listBox */
        void WriteLog(string message)
        {
            // Handle calls from secondary threads:
            if (logBox.InvokeRequired)
            {
                this.Invoke(new safelyEdit(WriteLog), new object[] { message });
            }
            else
            {
                string time = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");
                logBox.Items.Add(string.Format("[{0}] {1}", time, message));
            }
        }

        /* AddClientItem(): Add a name to the clients listBox */
        void AddClientItem(string name)
        {
            // Handle calls from secondary threads:
            if (clientsBox.InvokeRequired)
                this.Invoke(new safelyEdit(AddClientItem), new object[] { name });
            else clientsBox.Items.Add(name);
        }

        /* RemoveClientItem(): Add a name to the clients listBox */
        void RemoveClientItem(string name)
        {
            if (clientsBox.InvokeRequired)
                this.Invoke(new safelyEdit(RemoveClientItem), new object[] { name });
            else clientsBox.Items.Remove(name);
        }

        /* clientMenuStrip_ItemClicked(): When an option in the client menu is pressed */
        private void clientMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string clientName = clientsBox.Items[clientsBox.SelectedIndex].ToString();

            // Find client object based on name:
            Client client = null;
            foreach (Client c in clients)
            {
                if (c.name == clientName)
                {
                    client = c;
                    break;
                }
            }
            if (client == null)
                return;

            ToolStripItem item = e.ClickedItem;
            if (item.Text == "Send Message") // Send a message to selected client
            {
                ClientSendDialog dialogForm = new ClientSendDialog(clientName);

                dialogForm.ShowDialog(this);
                string msg = dialogForm.GetMessageText();
                dialogForm.Dispose();

                if (!string.IsNullOrEmpty(msg))
                {
                    client.Send(msg);
                    WriteLog("You have sent " + clientName + " a message.");
                }
            }
            else if (item.Text == "Kick") // Kick selected client
            {
                WriteLog(clientName + " has been kicked from the server.");
                client.End();
            }
        }

        /* shutdown_Click(): When shutdown button is pressed */
        private void shutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /* help_Click(): When help button is pressed */
        private void help_Click(object sender, EventArgs e)
        {
            string text = "Secure Client-Server by Ron Lipel\n\n" + 
                            "This server manages secure connections with clients and forwards each other's messages.\n" + 
                            "The traffic between the server and the clients is encrypted with RC4 & Diffie Hellman.\n" +
                            "Right click a connected client in the clients list in order to send a specific message, or kick selected client.\n" + 
                            "You can also send a global admin message using the text input.";

            MessageBox.Show(text, "Help");
        }
    }
}
