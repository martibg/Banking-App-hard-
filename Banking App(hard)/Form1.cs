using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Banking_App_hard_
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login(this);
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT lc.CustomerID, c.FirstName, c.LastName, c.Email
                         FROM LoginCredentials lc
                         JOIN Customers c ON lc.CustomerID = c.CustomerID
                         WHERE lc.Username = @Username AND lc.PasswordHash = @PasswordHash";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int customerId = reader.GetInt32("CustomerID");
                            UserSession.CustomerID = customerId;
                            UserSession.Username = username;
                            UserSession.FirstName = reader.GetString("FirstName");
                            UserSession.LastName = reader.GetString("LastName");
                            UserSession.Email = reader.GetString("Email");

                            
                            reader.Close(); 

                            string typeQuery = "SELECT AccountType FROM Accounts WHERE CustomerID = @CustomerID LIMIT 1";

                            using (MySqlCommand typeCmd = new MySqlCommand(typeQuery, conn))
                            {
                                typeCmd.Parameters.AddWithValue("@CustomerID", customerId);
                                string accountType = (string)typeCmd.ExecuteScalar();

                                if (accountType.ToLower() == "saving" || accountType.ToLower() == "savings")
                                {
                                    Form2Saving form2Saving = new Form2Saving(this); 
                                    form2Saving.Show();
                                }
                                else
                                {
                                    Form2 form2 = new Form2(this);
                                    form2.Show();
                                }
                            }

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
    }
}

