using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cart_Practic
{
    public partial class Admin_Form : Form
    {
        public Admin_Form()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sign_Up form = new Sign_Up();
            form.Show();
        }
    }
}
