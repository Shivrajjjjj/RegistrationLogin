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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\RegistrationLogin\RegistrationLogin\LoginDb.mdf;Integrated Security=True");
        SqlCommand command;
        int usernameCheck(string username)
        {
            connection.Open();
            string query = "select count(*) from Users where Users.Username='" + username +  "'";
            SqlCommand command = new SqlCommand(query, connection);
            int v = (int)command.ExecuteScalar();
            connection.Close();
            return v;
        }
        private void signUpBtn_Click(object sender, EventArgs e)
        {
            try
            {

                if (fname.Text != "" && lname.Text != "" && email.Text!="" && password.Text!="")
                {
                    int v = usernameCheck(email.Text);
                    if (v != 1)
                    {
                        connection.Open();
                        string query = "insert into Users(Fname,Lname,Username,Password) values(@Fname,@Lname,@Username,@Password)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Fname", fname.Text);
                        command.Parameters.AddWithValue("@Lname", lname.Text);
                        command.Parameters.AddWithValue("@Username", email.Text);
                        command.Parameters.AddWithValue("@Password", password.Text);
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("You have successfully registered!");
                        fname.Text = "";
                        lname.Text = "";
                        email.Text = "";
                        password.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Username is available !!!!");
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
            Login login = new Login();
            login.Show();
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
