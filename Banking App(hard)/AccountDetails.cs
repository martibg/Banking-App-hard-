using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Banking_App_hard_
{
    public partial class AccountDetails : Form
    {
        private Form callingForm;
        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";
        public AccountDetails(Form caller)
        {
            InitializeComponent();
            callingForm = caller;
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            callingForm.Show();
            this.Close();
        }




        private void LoadDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT c.FirstName, c.LastName, c.DOB, c.Email, c.PhoneNumber, c.Address,
                   a.AccountType, a.Balance, a.Currency
            FROM Customers c
            JOIN Accounts a ON c.CustomerID = a.CustomerID
            WHERE c.CustomerID = @CustomerID
            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblFirstName.Text = "First Name: " + reader["FirstName"].ToString();
                            lblLastName.Text = "Last Name: " + reader["LastName"].ToString();
                            lblDOB.Text = "Date of Birth: " + Convert.ToDateTime(reader["DOB"]).ToShortDateString();
                            lblEmail.Text = "Email: " + reader["Email"].ToString();
                            lblPhoneNumber.Text = "Phone: " + reader["PhoneNumber"].ToString();
                            lblAddress.Text = "Address: " + reader["Address"].ToString();
                            lblAccountType.Text = "Account Type: " + reader["AccountType"].ToString();
                            lblBalance.Text = "Balance: $" + Convert.ToDecimal(reader["Balance"]).ToString("N2");
                            lblCurrency.Text = "Currency: " + reader["Currency"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("User data not found.");
                        }
                    }
                }
            }
        }

        private void AccountDetails_Load_1(object sender, EventArgs e)
        {
            LoadDetails();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
          
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(currentPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all password fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            if (!IsPasswordStrong(newPassword))
            {
                MessageBox.Show("Password must be at least 8 characters long and include:\n" +
                                "- At least 1 uppercase letter\n" +
                                "- At least 1 lowercase letter\n" +
                                "- At least 1 digit\n" +
                                "- At least 1 special character (!@#$%^&* etc.)");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = "SELECT PasswordHash FROM LoginCredentials WHERE CustomerID = @CustomerID";
                using (MySqlCommand cmd = new MySqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);
                    var storedPassword = cmd.ExecuteScalar()?.ToString();

                    if (storedPassword != currentPassword)
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }
                }
                string updateQuery = "UPDATE LoginCredentials SET PasswordHash = @NewPassword WHERE CustomerID = @CustomerID";
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Password changed successfully.");
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
            }
        }

        private bool IsPasswordStrong(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
    
}

