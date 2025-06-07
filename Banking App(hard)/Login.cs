using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;



namespace Banking_App_hard_
{
    public partial class Login : Form
    {

        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";
        Random rnd = new Random();
        



        Form1 mainForm;
        public Login(Form1 form1)
        {
            InitializeComponent();
            mainForm = form1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime birthdate = datDOB.SelectionStart;
            int balance;
            string accountType = txtAccountType.Text.Trim().ToLower();

            if (accountType == "saving")
            {
                balance = 0;
            }
            else if (accountType == "credit")
            {
                balance = 0;
            }
            else
            {
                balance = rnd.Next(100, 10000);
            }

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtFName.Text) ||
                string.IsNullOrWhiteSpace(txtLName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNum.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtAccountType.Text) ||
                string.IsNullOrWhiteSpace(txtCurrency.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsStrongPassword(txtPassword.Text))
            {
                MessageBox.Show("Password must be at least 8 characters and include upper/lowercase letters, numbers, and special characters.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidName(txtFName.Text.Trim()) || !IsValidName(txtLName.Text.Trim()))
            {
                MessageBox.Show("Names must start with an uppercase letter and contain only letters.", "Invalid Name Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(txtPhoneNum.Text.Trim(), @"^\+?[0-9\s\-()]{10,20}$"))
            {
                MessageBox.Show("Enter a valid phone number format (digits, optional '+', spaces, dashes, or parentheses).", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM logincredentials WHERE Username = @Username", conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        long count = (long)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("This username is already taken. Please choose another one.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }
                    }

                    int customerId;

                    using (MySqlCommand cmd = new MySqlCommand(
                        @"INSERT INTO Customers (FirstName, LastName, DOB, Email, PhoneNumber, Address) 
                  VALUES (@FirstName, @LastName, @DOB, @Email, @PhoneNumber, @Address);
                  SELECT LAST_INSERT_ID();", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtFName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLName.Text);
                        cmd.Parameters.AddWithValue("@DOB", birthdate);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNum.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);

                        customerId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    int? linkedDebitAccountID = null;

                    if (accountType == "saving")
                    {
                        string linkUsername = txtLinkUsername.Text.Trim();
                        string linkPassword = txtLinkPassword.Text.Trim();

                        if (string.IsNullOrWhiteSpace(linkUsername) || string.IsNullOrWhiteSpace(linkPassword))
                        {
                            MessageBox.Show("Please enter the debit account credentials to link this savings account.", "Missing Debit Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }

                        using (MySqlCommand debitCmd = new MySqlCommand(
                            @"SELECT a.AccountID 
                      FROM logincredentials l
                      JOIN accounts a ON l.CustomerID = a.CustomerID
                      WHERE l.Username = @Username AND l.PasswordHash = @Password AND a.AccountType = 'debit'", conn, transaction))
                        {
                            debitCmd.Parameters.AddWithValue("@Username", linkUsername);
                            debitCmd.Parameters.AddWithValue("@Password", linkPassword);

                            var result = debitCmd.ExecuteScalar();
                            if (result == null)
                            {
                                MessageBox.Show("Invalid debit credentials or no debit account found.", "Link Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                transaction.Rollback();
                                return;
                            }

                            linkedDebitAccountID = Convert.ToInt32(result);
                        }
                    }

                    using (MySqlCommand cmd = new MySqlCommand(
                        @"INSERT INTO accounts (CustomerID, AccountType, Balance, Currency, LinkedDebitAccountID) 
                  VALUES (@CustomerID, @AccountType, @Balance, @Currency, @LinkedDebitAccountID)", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@AccountType", txtAccountType.Text);
                        cmd.Parameters.AddWithValue("@Balance", balance);
                        cmd.Parameters.AddWithValue("@Currency", txtCurrency.Text);
                        cmd.Parameters.AddWithValue("@LinkedDebitAccountID", linkedDebitAccountID.HasValue ? (object)linkedDebitAccountID : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    using (MySqlCommand cmd = new MySqlCommand(
                        @"INSERT INTO logincredentials (CustomerID, Username, PasswordHash) 
                  VALUES (@CustomerID, @Username, @PasswordHash)", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@PasswordHash", txtPassword.Text);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    UserSession.CustomerID = customerId;
                    UserSession.Username = txtUsername.Text;
                    UserSession.FirstName = txtFName.Text;
                    UserSession.LastName = txtLName.Text;
                    UserSession.Email = txtEmail.Text;

                    MessageBox.Show("Account created successfully.");
                }
                catch (MySqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Insert Error: " + ex.Message);
                    isValid = false;
                }
            }

            if (isValid)
            {
                switch (accountType)
                {
                    case "saving":
                        Form2Saving form2Saving = new Form2Saving(mainForm);
                        form2Saving.Show();
                        break;
                    case "credit":
                        Form2credit form2Credit = new Form2credit(mainForm);
                        form2Credit.Show();
                        break;
                    default:
                        Form2 form2 = new Form2(mainForm);
                        form2.Show();
                        break;
                }
                this.Hide();
            }
        }

        private bool IsStrongPassword(string password)
        {
            if (password.Length < 8)
                return false;
            if (!password.Any(char.IsUpper))
                return false;
            if (!password.Any(char.IsLower))
                return false;
            if (!password.Any(char.IsDigit))
                return false;
            if (!password.Any(ch => "!@#$%^&*()-_=+[]{}|;:'\",.<>?/`~".Contains(ch)))
                return false;

            return true;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private bool IsValidName(string name)
        {
            string pattern = @"^[A-Z][a-z]+$";
            return Regex.IsMatch(name, pattern);
        }



    }

}

