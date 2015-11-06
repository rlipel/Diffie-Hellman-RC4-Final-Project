using System;
using System.Windows.Forms;

namespace dh_client
{
    public partial class ClientMain : Form
    {
        delegate void safelyEdit(string edit); // To edit controls from multiple threads

        Connection connection = null;
        string userName;

        /* ClientMain(): Main function to the program */
        public ClientMain()
        {
            InitializeComponent();

            // Set default username to hostname:
            userName = System.Net.Dns.GetHostName();
            nameLabel.Text = userName;
            // Add OnProcessExit callback for when the app exits
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); 
        }

        /* Connect(): Connect to a server */
        void Connect(string ipaddress, int port)
        {
            if (connection != null)
                return; // There's an ongoing connection

            WriteChat(string.Format("Connecting to {0}:{1} ...", ipaddress, port));
            serverIpLabel.Text = "Connecting..";
            serverPortLabel.Text = "...";

            connection = new SecureConnection(ipaddress, port, this);

            if (connection.Connect())
            {
                serverIpLabel.Text = ipaddress;
                serverPortLabel.Text = port.ToString();
                WriteChat("Connected successfully. Establishing secure connection..");
            }
            else
            {
                connection = null;
                WriteChat("Failed to connect.");
                serverIpLabel.Text = "-";
                serverPortLabel.Text = "-";
            }
        }

        /* OnServerMessage(): Called upon message from server */
        public void OnServerMessage(string message)
        {
            if (message.StartsWith(("CLIENTLIST" + Convert.ToChar(0)))) // A clients list message from server
            {
                ClientListClear();
                string list = message.Substring(message.IndexOf(Convert.ToChar(0)) + 1);
                foreach (string name in list.Split(','))
                {
                    if(name.Length >= 3)
                        AddClientItem(name);
                }
                return;
            }
            else if (message.EndsWith(" has disconnected.") && message.IndexOf(':') == -1) // A client has disconnected; update the clients list
            {
                int i = message.IndexOf(" has disconnected.");
                string name = message.Substring(0, i);
                RemoveClientItem(name);
            }
            else if (message.EndsWith(" has connected.") && message.IndexOf(':') == -1) // A client has connected; update the clients list
            {
                int i = message.IndexOf(" has connected.");
                string name = message.Substring(0, i);
                AddClientItem(name);
            }

            // Regular message, display in chat
            WriteChat(message);
        }

        /* OnDisconnect(): Called when the connection ended */
        public void OnDisconnect()
        {
            connection = null;
            WriteChat("Disconnected from the server.");
            SetIPLabel("-");
            SetPortLabel("-");
            ClientListClear();
            WriteChat("---");
        }

        /* OnSecureReady(): Called upon key exchange completion */
        public void OnSecureReady()
        {
            // Send the key exchange completion message, along with client name:
            string introductionMessage = string.Format("Hey! My name is... :{0}", userName);
            connection.Send(introductionMessage);

            WriteChat("The connection has been secured and now ready.");
        }

        /* OnProcessExit(): Called when the app exits */
        void OnProcessExit(object sender, EventArgs e)
        {
            if (connection != null)
                connection.End();
        }

        #region UI sets

        /* WriteChat(): Write a line to the chat */
        void WriteChat(string message)
        {
            // Handle calls from secondary threads:
            if (chatBox.InvokeRequired)
                this.Invoke(new safelyEdit(WriteChat), new object[] { message });
            else
            {
                string time = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");
                chatBox.Items.Add(string.Format("[{0}] {1}", time, message));
            }
        }

        /* AddClientItem(): Add a name to the clients listBox */
        void AddClientItem(string name)
        {
            // Handle calls from secondary threads:
            if (clientsList.InvokeRequired)
                this.Invoke(new safelyEdit(AddClientItem), new object[] { name });
            else clientsList.Items.Add(name);
        }

        /* RemoveClientItem(): Add a name to the clients listBox */
        void RemoveClientItem(string name)
        {
            // Handle calls from secondary threads:
            if (clientsList.InvokeRequired)
                this.Invoke(new safelyEdit(RemoveClientItem), new object[] { name });
            else clientsList.Items.Remove(name);
        }

        /* ClientListClear(): Clear the clientsList listbox */
        void ClientListClear(string unused="")
        {
            // Handle calls from secondary threads:
            if (clientsList.InvokeRequired)
                this.Invoke(new safelyEdit(ClientListClear), new object[] { unused });
            else clientsList.Items.Clear();
        }

        /* SetIPLabel(): Set the value of the IP label */
        void SetIPLabel(string value)
        {
            if (serverIpLabel.InvokeRequired)
                this.Invoke(new safelyEdit(SetIPLabel), new object[] { value });
            else serverIpLabel.Text = value;
        }

        /* SetPortLabel(): Set the value of the port label */
        void SetPortLabel(string value)
        {
            if (serverPortLabel.InvokeRequired)
                this.Invoke(new safelyEdit(SetPortLabel), new object[] { value });
            else serverPortLabel.Text = value;
        }

        #endregion

        #region UI button clicks

        /* connectButton_Click(): Called when the connect button is pressed */
        private void connectButton_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                MessageBox.Show("You should first disconnect from the current server.", "Connect to a server");
                return;
            }

            ConnectDialog dialogForm = new ConnectDialog();
            string ip = null;
            int port = 0;

            dialogForm.ShowDialog(this);

            ip = dialogForm.GetServerIP();
            port = dialogForm.GetServerPort();

            dialogForm.Dispose();

            if (ip != null && port > 0 && port <= 65535)
            {
                // if already connected to a server, Disconnect();
                Connect(ip, port);
            }
        }

        /* connectButton_Click(): Called when the disconnnect button is pressed */
        private void disconnectButton_Click(object sender, EventArgs e)
        {
            if (connection != null)
                connection.End();
        }

        /* userSend_Click(): Called when the send button is pressed */
        private void userSend_Click(object sender, EventArgs e)
        {
            string input = userInput.Text;
            if (input.Length > 0)
            {
                if (connection != null)
                {
                    connection.Send(input);
                }
                userInput.Text = "";
            }
        }

        /* nameEdit_Click(): Called when the edit name button is pressed */
        private void nameEdit_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                MessageBox.Show("You can't change the name while you're connected to a server.", "Change name");
            }
            else
            {
                NameDialog dialogForm = new NameDialog(userName);
                dialogForm.ShowDialog(this);
                string newName = dialogForm.GetNewName();
                if (newName != userName && newName.Length > 3 && newName.IndexOf(':') == -1)
                {
                    userName = newName;
                    nameLabel.Text = newName;
                }
            }
        }

        #endregion
    }
}
