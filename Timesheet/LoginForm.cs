using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Timesheet
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBoxUsername.Text = Environment.UserName;
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBoxUsername.Text.Trim();
            string enteredPassword = textBoxPassword.Text.Trim();

            if (ValidateWindowsCredentials(enteredUsername, enteredPassword))
            {
                MenuForm menuform = new MenuForm();
                menuform.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Credentials,Please Try Again.");
            }
        }

        public bool ValidateWindowsCredentials(string username, string password)
        {
            using (PrincipalContext context = new PrincipalContext(ContextType.Machine))
            {
                return context.ValidateCredentials(username, password);
            }
        }

        
        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as System.Windows.Forms.TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as System.Windows.Forms.TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
        }

        private void textBoxUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxUsername.Text))
            {
                e.Cancel = true;
                textBoxUsername.Focus();
                errorProvider.SetError(textBoxUsername, "Enter a Username!");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBoxUsername, null);
            }
        }

        private void textBoxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                e.Cancel = true;
                textBoxPassword.Focus();
                errorProvider.SetError(textBoxPassword, "Enter a Password !");
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textBoxPassword, null);
            }

        }

        private void showcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showcheckBox.Checked)
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
