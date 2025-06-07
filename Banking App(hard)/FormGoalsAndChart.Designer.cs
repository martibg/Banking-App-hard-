using System;
using System.Windows.Forms;

namespace Banking_App_hard_
{
    partial class FormGoalsAndChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnBack = new System.Windows.Forms.Button();
            this.chartSpending = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtCategory = new System.Windows.Forms.ComboBox();
            this.txtTargetAmount = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAddGoal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dataGridViewGoals = new System.Windows.Forms.DataGridView();
            this.progressBarGoal = new System.Windows.Forms.ProgressBar();
            this.panelProgressBarBackground = new System.Windows.Forms.Panel();
            this.panelProgressBarFill = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chartSpending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(30, 28);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // chartSpending
            // 
            chartArea2.Name = "ChartArea1";
            this.chartSpending.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartSpending.Legends.Add(legend2);
            this.chartSpending.Location = new System.Drawing.Point(376, 12);
            this.chartSpending.Name = "chartSpending";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartSpending.Series.Add(series2);
            this.chartSpending.Size = new System.Drawing.Size(469, 297);
            this.chartSpending.TabIndex = 8;
            this.chartSpending.Text = "chart1";
            // 
            // txtCategory
            // 
            this.txtCategory.FormattingEnabled = true;
            this.txtCategory.Items.AddRange(new object[] {
            "Food",
            "Utilities",
            "Rent",
            "Transport",
            "Entertainment",
            "Other"});
            this.txtCategory.Location = new System.Drawing.Point(131, 79);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(168, 24);
            this.txtCategory.TabIndex = 9;
            // 
            // txtTargetAmount
            // 
            this.txtTargetAmount.Location = new System.Drawing.Point(131, 145);
            this.txtTargetAmount.Name = "txtTargetAmount";
            this.txtTargetAmount.Size = new System.Drawing.Size(168, 22);
            this.txtTargetAmount.TabIndex = 10;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(131, 188);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(168, 22);
            this.txtDescription.TabIndex = 10;
            // 
            // btnAddGoal
            // 
            this.btnAddGoal.Location = new System.Drawing.Point(131, 245);
            this.btnAddGoal.Name = "btnAddGoal";
            this.btnAddGoal.Size = new System.Drawing.Size(75, 23);
            this.btnAddGoal.TabIndex = 11;
            this.btnAddGoal.Text = "Add goal";
            this.btnAddGoal.UseVisualStyleBackColor = true;
            this.btnAddGoal.Click += new System.EventHandler(this.btnAddGoal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Target Ammount";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(22, 245);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(251, 245);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click_1);
            // 
            // dataGridViewGoals
            // 
            this.dataGridViewGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGoals.Location = new System.Drawing.Point(32, 329);
            this.dataGridViewGoals.Name = "dataGridViewGoals";
            this.dataGridViewGoals.RowHeadersWidth = 51;
            this.dataGridViewGoals.RowTemplate.Height = 24;
            this.dataGridViewGoals.Size = new System.Drawing.Size(1039, 150);
            this.dataGridViewGoals.TabIndex = 20;
            this.dataGridViewGoals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewGoals_CellClick);
            // 
            // progressBarGoal
            // 
            this.progressBarGoal.Location = new System.Drawing.Point(925, 168);
            this.progressBarGoal.Name = "progressBarGoal";
            this.progressBarGoal.Size = new System.Drawing.Size(100, 23);
            this.progressBarGoal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarGoal.TabIndex = 21;
            // 
            // panelProgressBarBackground
            // 
            this.panelProgressBarBackground.BackColor = System.Drawing.Color.LightGray;
            this.panelProgressBarBackground.Enabled = false;
            this.panelProgressBarBackground.Location = new System.Drawing.Point(925, 168);
            this.panelProgressBarBackground.Name = "panelProgressBarBackground";
            this.panelProgressBarBackground.Size = new System.Drawing.Size(100, 23);
            this.panelProgressBarBackground.TabIndex = 24;
            // 
            // panelProgressBarFill
            // 
            this.panelProgressBarFill.BackColor = System.Drawing.Color.Lime;
            this.panelProgressBarFill.Enabled = false;
            this.panelProgressBarFill.Location = new System.Drawing.Point(925, 168);
            this.panelProgressBarFill.Name = "panelProgressBarFill";
            this.panelProgressBarFill.Size = new System.Drawing.Size(100, 23);
            this.panelProgressBarFill.TabIndex = 25;
            // 
            // FormGoalsAndChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 502);
            this.Controls.Add(this.panelProgressBarFill);
            this.Controls.Add(this.panelProgressBarBackground);
            this.Controls.Add(this.progressBarGoal);
            this.Controls.Add(this.dataGridViewGoals);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddGoal);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtTargetAmount);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.chartSpending);
            this.Controls.Add(this.btnBack);
            this.Name = "FormGoalsAndChart";
            this.Text = "FormGoalsAndChart";
            this.Load += new System.EventHandler(this.FormGoalsAndChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSpending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSpending;
        private System.Windows.Forms.ComboBox txtCategory;
        private System.Windows.Forms.TextBox txtTargetAmount;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnAddGoal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private DataGridView dataGridViewGoals;
        private ProgressBar progressBarGoal;
        private Panel panelProgressBarBackground;
        private Panel panelProgressBarFill;
    }
}