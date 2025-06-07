using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banking_App_hard_
{
    public partial class Form2credit : Form
    {
        Form1 mainForm;
        public Form2credit(Form1 form1)
        {
            InitializeComponent();
            mainForm = form1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            this.Close();
        }
    }
}
