using MySql.Data.MySqlClient;
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
    public partial class dish_admin : Form
    {
        public dish_admin()
        {
            InitializeComponent();
            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uS_dish11_Load(object sender, EventArgs e)
        {

        }

        private void uS_dish11_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Form form = new Admin_Form();
            form.Show();
        }
    }
}
