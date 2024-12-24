using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RegistrationLogin
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RegistrationLogin\RegistrationLogin\LoginDb.mdf;Integrated Security=True");
        SqlCommand command;

        int usernameCheck(string username, string password)
        {
            connection.Open();
            string query = "select count(*) from Users where Users.Username='" + username + "' and Password='"+password+"'";
            SqlCommand command = new SqlCommand(query, connection);
            int v = (int)command.ExecuteScalar();
            connection.Close();
            return v;
        }
        private void signInBtn_Click(object sender, EventArgs e)
        {
            try
            {

                if (email.Text != "" && password.Text != "")
                {
                    int v = usernameCheck(email.Text,password.Text);
                    if (v == 1)
                    {
                        MessageBox.Show("You have successfully entered");
                        email.Text = "";
                        password.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Username is not available !!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Fill in the fields!!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (password.UseSystemPasswordChar)
            {
                password.UseSystemPasswordChar = false;
            }
            else
            {
                password.UseSystemPasswordChar = true;
            }
        }
    }
}
