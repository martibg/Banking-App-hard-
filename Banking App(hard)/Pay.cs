using System;
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
    public partial class Pay : Form
    {
        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";

        Form2 mainForm2;
        public Pay(Form2 form2)
        {
            InitializeComponent();
            mainForm2 = form2;
        }

        private void Pay_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm2.Show();
            this.Close();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            decimal withdrawAmount;
            if (!decimal.TryParse(txtWithdrawAmount.Text, out withdrawAmount) || withdrawAmount <= 0)
            {
                MessageBox.Show("Please enter a valid amount to withdraw.");
                return;
            }

            string description = txtDescription.Text;
            string recipientUsername = txtRecipientUsername.Text.Trim();
            string category = cmbCategory.SelectedItem?.ToString() ?? "Uncategorized";
            int customerId = UserSession.CustomerID;
            int senderAccountId = 0;
            decimal senderCurrentBalance = 0;
            string senderUsername = "";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string getSenderUsername = "SELECT Username FROM LoginCredentials WHERE CustomerID = @CustomerID";
                using (MySqlCommand cmd = new MySqlCommand(getSenderUsername, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            senderUsername = reader.GetString("Username");
                        }
                    }
                }

                string getAccountQuery = "SELECT AccountID, Balance FROM Accounts WHERE CustomerID = @CustomerID LIMIT 1";
                using (MySqlCommand cmd = new MySqlCommand(getAccountQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            senderAccountId = reader.GetInt32("AccountID");
                            senderCurrentBalance = reader.GetDecimal("Balance");
                        }
                        else
                        {
                            MessageBox.Show("Account not found.");
                            return;
                        }
                    }
                }

                if (withdrawAmount > senderCurrentBalance)
                {
                    MessageBox.Show("Insufficient funds.");
                    return;
                }

                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    decimal senderNewBalance = senderCurrentBalance - withdrawAmount;

                    string updateSenderBalance = "UPDATE Accounts SET Balance = @Balance WHERE AccountID = @AccountID";
                    using (MySqlCommand cmd = new MySqlCommand(updateSenderBalance, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Balance", senderNewBalance);
                        cmd.Parameters.AddWithValue("@AccountID", senderAccountId);
                        cmd.ExecuteNonQuery();
                    }

                    string insertWithdrawal = @"INSERT INTO Transactions 
                (AccountID, Type, Amount, Description, BalanceAfter, Category)
                VALUES (@AccountID, 'Withdrawal', @Amount, @Description, @BalanceAfter, @Category)";
                    using (MySqlCommand cmd = new MySqlCommand(insertWithdrawal, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@AccountID", senderAccountId);
                        cmd.Parameters.AddWithValue("@Amount", withdrawAmount);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@BalanceAfter", senderNewBalance);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.ExecuteNonQuery();
                    }

                    if (!string.IsNullOrEmpty(recipientUsername))
                    {
                        string getRecipientQuery = @"SELECT a.AccountID, a.Balance
                                             FROM LoginCredentials l
                                             JOIN Accounts a ON l.CustomerID = a.CustomerID
                                             WHERE l.Username = @Username
                                             LIMIT 1";

                        int recipientAccountId = 0;
                        decimal recipientBalance = 0;

                        using (MySqlCommand cmd = new MySqlCommand(getRecipientQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Username", recipientUsername);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    recipientAccountId = reader.GetInt32("AccountID");
                                    recipientBalance = reader.GetDecimal("Balance");
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Recipient username not found.");
                                    return;
                                }
                            }
                        }

                        decimal recipientNewBalance = recipientBalance + withdrawAmount;
                        string updateRecipientBalance = "UPDATE Accounts SET Balance = @Balance WHERE AccountID = @AccountID";
                        using (MySqlCommand cmd = new MySqlCommand(updateRecipientBalance, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Balance", recipientNewBalance);
                            cmd.Parameters.AddWithValue("@AccountID", recipientAccountId);
                            cmd.ExecuteNonQuery();
                        }

                        string insertDeposit = @"INSERT INTO Transactions 
                    (AccountID, Type, Amount, Description, BalanceAfter, Category)
                    VALUES (@AccountID, 'Deposit', @Amount, @Description, @BalanceAfter, @Category)";
                        using (MySqlCommand cmd = new MySqlCommand(insertDeposit, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@AccountID", recipientAccountId);
                            cmd.Parameters.AddWithValue("@Amount", withdrawAmount);
                            cmd.Parameters.AddWithValue("@Description", $"Transfer from {senderUsername}");
                            cmd.Parameters.AddWithValue("@BalanceAfter", recipientNewBalance);
                            cmd.Parameters.AddWithValue("@Category", "Transfer");
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Transfer successful.");
                       
                    }
                    else
                    {
                        MessageBox.Show("Withdrawal successful.");
                    }

                    transaction.Commit();
                    mainForm2.RefreshAccountInfo();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }

                txtWithdrawAmount.Clear();
                txtDescription.Clear();
                txtRecipientUsername.Clear();
            }
        }
    }
}


