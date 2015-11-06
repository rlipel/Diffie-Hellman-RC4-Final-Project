using System;
using System.Windows.Forms;

namespace dh_client
{
    public partial class NameDialog : Form
    {
        string defaultName;
        public NameDialog(string prevName)
        {
            InitializeComponent();
            defaultName = prevName;
            newUserName.Text = prevName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newUserName.Text = defaultName;
            this.Close();
        }

        public string GetNewName()
        {
            return newUserName.Text;
        }
    }
}
