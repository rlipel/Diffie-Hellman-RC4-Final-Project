using System;
using System.Windows.Forms;

namespace dh_client
{
    public partial class ConnectDialog : Form
    {
        string result_ip = null;
        int result_port = 0;

        public ConnectDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int colonIndex = serverAddress.Text.IndexOf(':');
            if(colonIndex > 7)
            {
                result_ip = serverAddress.Text.Substring(0, colonIndex);
                int.TryParse(serverAddress.Text.Substring(colonIndex + 1), out result_port);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result_ip = null;
            result_port = 0;
            this.Close();
        }

        public string GetServerIP()
        {
            return result_ip;
        }

        public int GetServerPort()
        {
            return result_port;
        }
    }
}
