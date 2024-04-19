using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Train_Department
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            string username = "Admin";
            string password = "1234";

            string enteredUsername = txtUsernameAdmin.Text;
            string enteredPassword = txtPasswordAdmin.Text;

            if (enteredUsername == username && enteredPassword == password)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();

                txtUsernameAdmin.Text = "";
                txtPasswordAdmin.Text = "";


            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtPasswordAdmin.Text = "";

                txtPasswordAdmin.Focus();
            }
        }

    }
}
