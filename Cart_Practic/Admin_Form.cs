using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cart_Practic.формы_админы;

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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Adres_rest form = new Adres_rest();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ingridients form = new ingridients();
            form.Show();
        }

        // Категории
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Category form = new Category();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            users form = new users();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            restorantss form = new restorantss();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            dish_admin form = new dish_admin();
            form.Show();
        }
    }
}
