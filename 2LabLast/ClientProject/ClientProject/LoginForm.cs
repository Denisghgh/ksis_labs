using System;
using System.Windows.Forms;

namespace ClientProject
{
    public partial class LoginForm : Form
    {
        public ChatForm mainForm;

        public LoginForm()
        {
            InitializeComponent();
        }

        private bool ClientUserNameCheck(ref string clientUsername)
        {
            if (clientNameTextBox.Text != "")
            {
                clientUsername = clientNameTextBox.Text;
                if (clientLastNameTextBox.Text != "")
                    clientUsername = clientUsername + " " + clientLastNameTextBox.Text;
                return true;
            }
            MessageBox.Show("Client username is empty!");
            return false;
        }

        public void connectButton_Click(object sender, EventArgs e)
        {
            string clientUsername = "";
            if (ClientUserNameCheck(ref clientUsername))
            {
                ChatForm Form1 = new ChatForm();
                Form1.ClientUsername = clientUsername;
                Form1.Owner = this;
                disconnectButton.Visible = true;
                Form1.Show();

            }
        }
        private void CloseForm1()
        {
            this.Close();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            CloseForm1();
        }
    }
}
