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
    public partial class Form2 : Form
    {
        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";
        
        Form1 mainForm;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            mainForm = form1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {UserSession.FirstName} what would you like to do?";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT AccountType, Balance, Currency FROM Accounts WHERE CustomerID = @CustomerID";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblAccountType.Text = reader.GetString("AccountType");
                            lblBalance.Text = reader.GetDecimal("Balance").ToString("C");
                            lblCurrency.Text = reader.GetString("Currency");
                        }
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }

        private void btnPayWnd_Click(object sender, EventArgs e)
        {
            Pay pay = new Pay(this);
            pay.Show();
            this.Hide();
        }

        private void btnTransactionHistory_Click(object sender, EventArgs e)
        {
            TransactionHistory historyForm = new TransactionHistory(this);
            historyForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccountDetails accountDetails = new AccountDetails(this);   
            accountDetails.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormGoalsAndChart formGoalsAndChart = new FormGoalsAndChart(this);
            formGoalsAndChart.Show();
            this.Hide();
        }
    }
}
