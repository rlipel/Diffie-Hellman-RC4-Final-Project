using System;
using System.Windows.Forms;

namespace dh_server
{
    public partial class ClientSendDialog : Form
    {
        public ClientSendDialog(string clientName)
        {
            InitializeComponent();
            this.Text += clientName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            messageBox.Text = null;
            this.Close();
        }

        public string GetMessageText()
        {
            return messageBox.Text;
        }
    }
}
