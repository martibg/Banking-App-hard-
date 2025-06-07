namespace Banking_App_hard_
{
    partial class Form2Saving
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnTransactionHistory = new System.Windows.Forms.Button();
            this.btnAccountDetails = new System.Windows.Forms.Button();
            this.txtTransferAmount = new System.Windows.Forms.TextBox();
            this.btnSetTransfer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(30, 28);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnTransactionHistory
            // 
            this.btnTransactionHistory.Location = new System.Drawing.Point(262, 320);
            this.btnTransactionHistory.Name = "btnTransactionHistory";
            this.btnTransactionHistory.Size = new System.Drawing.Size(75, 48);
            this.btnTransactionHistory.TabIndex = 2;
            this.btnTransactionHistory.Text = "Transaction History";
            this.btnTransactionHistory.UseVisualStyleBackColor = true;
            this.btnTransactionHistory.Click += new System.EventHandler(this.btnTransactionHistory_Click);
            // 
            // btnAccountDetails
            // 
            this.btnAccountDetails.Location = new System.Drawing.Point(409, 320);
            this.btnAccountDetails.Name = "btnAccountDetails";
            this.btnAccountDetails.Size = new System.Drawing.Size(75, 48);
            this.btnAccountDetails.TabIndex = 3;
            this.btnAccountDetails.Text = "Account Details";
            this.btnAccountDetails.UseVisualStyleBackColor = true;
            this.btnAccountDetails.Click += new System.EventHandler(this.btnAccountDetails_Click);
            // 
            // txtTransferAmount
            // 
            this.txtTransferAmount.Location = new System.Drawing.Point(354, 173);
            this.txtTransferAmount.Name = "txtTransferAmount";
            this.txtTransferAmount.Size = new System.Drawing.Size(176, 22);
            this.txtTransferAmount.TabIndex = 4;
            // 
            // btnSetTransfer
            // 
            this.btnSetTransfer.Location = new System.Drawing.Point(429, 219);
            this.btnSetTransfer.Name = "btnSetTransfer";
            this.btnSetTransfer.Size = new System.Drawing.Size(101, 43);
            this.btnSetTransfer.TabIndex = 5;
            this.btnSetTransfer.Text = "Set transfer";
            this.btnSetTransfer.UseVisualStyleBackColor = true;
            this.btnSetTransfer.Click += new System.EventHandler(this.btnSetTransfer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Transfer ammount";
            // 
            // Form2Saving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSetTransfer);
            this.Controls.Add(this.txtTransferAmount);
            this.Controls.Add(this.btnAccountDetails);
            this.Controls.Add(this.btnTransactionHistory);
            this.Controls.Add(this.btnBack);
            this.Name = "Form2Saving";
            this.Text = "Form2Saving";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnTransactionHistory;
        private System.Windows.Forms.Button btnAccountDetails;
        private System.Windows.Forms.TextBox txtTransferAmount;
        private System.Windows.Forms.Button btnSetTransfer;
        private System.Windows.Forms.Label label1;
    }
}