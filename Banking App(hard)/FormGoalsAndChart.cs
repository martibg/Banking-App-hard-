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
    public partial class FormGoalsAndChart : Form
    {
        string connectionString = "Server=localhost;Database=bank_aplication;User Id=root;Password=Qwe1209poi!;";
        Form2 mainForm2;
        private int selectedGoalId = -1;
        public FormGoalsAndChart(Form2 form2)
        {
            InitializeComponent();
            mainForm2 = form2;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm2.Show();
            this.Close();
        }

        private void LoadSpendingPieChart()
        {
            int customerId = UserSession.CustomerID;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string getAccountQuery = "SELECT AccountID FROM Accounts WHERE CustomerID = @CustomerID LIMIT 1";
                int accountId;

                using (MySqlCommand cmd = new MySqlCommand(getAccountQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Account not found.");
                        return;
                    }
                    accountId = Convert.ToInt32(result);
                }

                string query = @"
            SELECT Category, SUM(Amount) AS TotalSpent
            FROM Transactions
            WHERE AccountID = @AccountID AND Type = 'Withdrawal' AND Category IS NOT NULL
            GROUP BY Category
            ORDER BY TotalSpent DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        chartSpending.Series.Clear();
                        chartSpending.Titles.Clear();

                        var series = chartSpending.Series.Add("Spending");
                        series.ChartType = SeriesChartType.Pie;
                        series.IsValueShownAsLabel = true;
                        series.Label = "#VALX (#PERCENT{P1})";  
                        series.ToolTip = "#VALX: $#VAL"; 

                        chartSpending.Titles.Add("Spending by Category");

                        while (reader.Read())
                        {
                            string category = reader["Category"].ToString();
                            decimal total = Convert.ToDecimal(reader["TotalSpent"]);
                            series.Points.AddXY(category, total);
                        }
                    }
                }
            }
        }

        private void FormGoalsAndChart_Load(object sender, EventArgs e)
        {
            LoadSpendingPieChart();
            LoadGoals();

            panelProgressBarFill.Width = 0;

        }

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            string category = txtCategory.Text.Trim();
            string description = txtDescription.Text.Trim();
            decimal targetAmount;

            if (!decimal.TryParse(txtTargetAmount.Text, out targetAmount) || targetAmount <= 0)
            {
                MessageBox.Show("Enter a valid target amount.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Goals (CustomerID, Category, TargetAmount, Description)
                         VALUES (@CustomerID, @Category, @TargetAmount, @Description)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@TargetAmount", targetAmount);
                    cmd.Parameters.AddWithValue("@Description", description);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Goal added!");
            LoadGoals(); 
        }


        private void LoadGoals()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
             SELECT 
                g.GoalID, 
                g.Category, 
                g.TargetAmount, 
                g.Description,
                IFNULL(SUM(t.Amount), 0) AS CurrentSpending,
                ROUND(IFNULL(SUM(t.Amount), 0) / g.TargetAmount * 100, 2) AS ProgressPercent
             FROM Goals g
             LEFT JOIN Transactions t 
                ON g.Category = t.Category 
                AND t.AccountID IN (
                    SELECT AccountID FROM Accounts WHERE CustomerID = g.CustomerID
                )
                AND t.Type = 'Withdrawal'
             WHERE g.CustomerID = @CustomerID
             GROUP BY g.GoalID, g.Category, g.TargetAmount, g.Description";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridViewGoals.DataSource = table;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedGoalId == -1)
            {
                MessageBox.Show("Please select a goal to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this goal?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Goals WHERE GoalID = @GoalID AND CustomerID = @CustomerID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GoalID", selectedGoalId);
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Goal deleted.");
            LoadGoals();
            selectedGoalId = -1;
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (selectedGoalId == -1)
            {
                MessageBox.Show("Please select a goal to update.");
                return;
            }

            string category = txtCategory.Text.Trim();
            string description = txtDescription.Text.Trim();
            if (!decimal.TryParse(txtTargetAmount.Text, out decimal targetAmount) || targetAmount <= 0)
            {
                MessageBox.Show("Enter a valid target amount.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Goals 
                         SET Category = @Category, TargetAmount = @TargetAmount, Description = @Description 
                         WHERE GoalID = @GoalID AND CustomerID = @CustomerID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GoalID", selectedGoalId);
                    cmd.Parameters.AddWithValue("@CustomerID", UserSession.CustomerID);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@TargetAmount", targetAmount);
                    cmd.Parameters.AddWithValue("@Description", description);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Goal updated.");
            LoadGoals();
            selectedGoalId = -1;
        }

        private void dataGridViewGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && dataGridViewGoals.Rows[e.RowIndex].Cells["ProgressPercent"].Value != null)
            {
                DataGridViewRow row = dataGridViewGoals.Rows[e.RowIndex];

                selectedGoalId = Convert.ToInt32(row.Cells["GoalID"].Value);
                txtCategory.Text = row.Cells["Category"].Value.ToString();
                txtTargetAmount.Text = row.Cells["TargetAmount"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();

                string progressStr = row.Cells["ProgressPercent"].Value.ToString();
                if (decimal.TryParse(progressStr, out decimal progress))
                {
                    UpdateProgressBar(progress);
                }
                else
                {
                    UpdateProgressBar(0); // fallback
                }
            }

        }


        private void UpdateProgressBar(decimal progress)
        {
            int maxWidth = panelProgressBarBackground.Width;

            int value = (int)Math.Min(progress, 100); 
            int fillWidth = (int)((value / 100m) * maxWidth);

            panelProgressBarFill.Width = fillWidth;

            int red, green;
            if (value <= 50)
            {
                red = (int)(255 * (value / 50m));
                green = 255;
            }
            else
            {
                red = 255;
                green = (int)(255 * ((100 - value) / 50m));
            }

            panelProgressBarFill.BackColor = Color.FromArgb(red, green, 0);
        }
    }
}
