using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;

namespace Banking_App_hard_
{
    public partial class Form2Saving : Form
    {
        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";
        private Form1 mainForm1;
        public Form2Saving(Form1 form1)
        {
            InitializeComponent();
            mainForm1 = form1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm1.Show();
            this.Close();
        }

        private void btnTransactionHistory_Click(object sender, EventArgs e)
        {
            TransactionHistory historyForm = new TransactionHistory(this);
            historyForm.Show();
            this.Hide();
        }

        private void btnAccountDetails_Click(object sender, EventArgs e)
        {
            AccountDetails detailsForm = new AccountDetails(this);
            detailsForm.Show();
            this.Hide();
        }

        private void btnSetTransfer_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtTransferAmount.Text, out decimal transferAmount) || transferAmount <= 0)
            {
                MessageBox.Show("Please enter a valid transfer amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string savingsQuery = @"
            SELECT a.AccountID, a.LinkedDebitAccountID, a.LastTransferDate
            FROM Accounts a
            WHERE a.CustomerID = @CustomerID AND a.AccountType = 'saving'";

                using (MySqlCommand savingsCmd = new MySqlCommand(savingsQuery, conn))
                {
                    savingsCmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);

                    using (var reader = savingsCmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("No savings account found for this user.");
                            return;
                        }

                        int savingAccountId = reader.GetInt32(reader.GetOrdinal("AccountID"));
                        int linkedDebitAccountId = reader.GetInt32(reader.GetOrdinal("LinkedDebitAccountID"));

                        DateTime? lastTransferDate = null;
                        int dateOrdinal = reader.GetOrdinal("LastTransferDate");
                        if (!reader.IsDBNull(dateOrdinal))
                        {
                            lastTransferDate = reader.GetDateTime(dateOrdinal);
                        }

                        if (lastTransferDate.HasValue && (DateTime.Now - lastTransferDate.Value).TotalDays < 30)
                        {
                            MessageBox.Show("A transfer has already been made in the last month.");
                            return;
                        }

                        reader.Close();

                        string balanceQuery = "SELECT Balance FROM Accounts WHERE AccountID = @DebitAccountID";
                        using (MySqlCommand balanceCmd = new MySqlCommand(balanceQuery, conn))
                        {
                            balanceCmd.Parameters.AddWithValue("@DebitAccountID", linkedDebitAccountId);
                            decimal debitBalance = Convert.ToDecimal(balanceCmd.ExecuteScalar());

                            if (debitBalance < transferAmount)
                            {
                                MessageBox.Show("Insufficient funds in the linked debit account.");
                                return;
                            }

                            using (var transaction = conn.BeginTransaction())
                            {
                                try
                                {
                                    string updateDebit = "UPDATE Accounts SET Balance = Balance - @Amount WHERE AccountID = @DebitAccountID";
                                    using (MySqlCommand cmd = new MySqlCommand(updateDebit, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@Amount", transferAmount);
                                        cmd.Parameters.AddWithValue("@DebitAccountID", linkedDebitAccountId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    string updateSaving = "UPDATE Accounts SET Balance = Balance + @Amount, LastTransferDate = @Now WHERE AccountID = @SavingAccountID";
                                    using (MySqlCommand cmd = new MySqlCommand(updateSaving, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@Amount", transferAmount);
                                        cmd.Parameters.AddWithValue("@Now", DateTime.Now);
                                        cmd.Parameters.AddWithValue("@SavingAccountID", savingAccountId);
                                        cmd.ExecuteNonQuery();
                                    }

                                    string logDebit = @"
                                INSERT INTO Transactions (AccountID, Type, Amount, Description, Category, Timestamp, BalanceAfter)
                                VALUES (@AccountID, 'Transfer Out', @Amount, 'Auto-transfer to savings', 'Savings Transfer', NOW(),
                                (SELECT Balance FROM Accounts WHERE AccountID = @AccountID))";
                                    using (MySqlCommand cmd = new MySqlCommand(logDebit, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@AccountID", linkedDebitAccountId);
                                        cmd.Parameters.AddWithValue("@Amount", transferAmount);
                                        cmd.ExecuteNonQuery();
                                    }

                                    string logSaving = @"
                                INSERT INTO Transactions (AccountID, Type, Amount, Description, Category, Timestamp, BalanceAfter)
                                VALUES (@AccountID, 'Transfer In', @Amount, 'Auto-transfer from debit', 'Savings Transfer', NOW(),
                                (SELECT Balance FROM Accounts WHERE AccountID = @AccountID))";
                                    using (MySqlCommand cmd = new MySqlCommand(logSaving, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@AccountID", savingAccountId);
                                        cmd.Parameters.AddWithValue("@Amount", transferAmount);
                                        cmd.ExecuteNonQuery();
                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Transfer successful.");
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Error during transfer: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}