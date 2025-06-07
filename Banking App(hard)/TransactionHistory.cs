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
    public partial class TransactionHistory : Form
    {
        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";
        private Form callingForm;
        public TransactionHistory(Form caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void TransactionHistory_Load(object sender, EventArgs e)
        {
            LoadTransactionHistory();
        }

        private void LoadTransactionHistory()
        {
            int customerId = UserSession.CustomerID;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string accountQuery = "SELECT AccountID FROM Accounts WHERE CustomerID = @CustomerID";

                List<int> accountIds = new List<int>();

                using (MySqlCommand accountCmd = new MySqlCommand(accountQuery, conn))
                {
                    accountCmd.Parameters.AddWithValue("@CustomerID", customerId);
                    using (MySqlDataReader reader = accountCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accountIds.Add(reader.GetInt32("AccountID"));
                        }
                    }
                }

                if (accountIds.Count == 0)
                {
                    MessageBox.Show("No accounts found for this user.");
                    return;
                }

                string transactionQuery = $@"
            SELECT 
                a.AccountType AS 'Account Type',
                t.Type AS 'Transaction Type',
                t.Amount,
                t.Description,
                t.Category,
                t.Timestamp,
                t.BalanceAfter AS 'Balance After'
            FROM Transactions t
            JOIN Accounts a ON t.AccountID = a.AccountID
            WHERE t.AccountID IN ({string.Join(",", accountIds)})
            ORDER BY t.Timestamp DESC";

                using (MySqlCommand cmd = new MySqlCommand(transactionQuery, conn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewTransactions.DataSource = table;
                    dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            callingForm.Show();
            this.Close();
        }
    }
}
