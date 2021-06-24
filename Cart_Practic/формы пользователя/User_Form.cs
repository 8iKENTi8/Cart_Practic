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
    public partial class User_Form : Form
    {
        public User_Form()
        {
            InitializeComponent();
            panel3.Height = button1.Height;
            panel3.Top = button1.Top;
            dishes1.BringToFront();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Height = button2.Height;
            panel3.Top = button2.Top;
            resta1.BringToFront();
            resta1.load1();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Height = button3.Height;
            panel3.Top = button3.Top;
            uScab1.BringToFront();
            uScab1.load1();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Height = button1.Height;
            panel3.Top = button1.Top;
            dishes1.BringToFront();
            dishes1.load1();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Height = button4.Height;
            panel3.Top = button4.Top;
            this.Hide();
            Sign_Up form = new Sign_Up();
            form.Show();
        }

        private void resta1_Load(object sender, EventArgs e)
        {

        }

        private void uScab1_Load(object sender, EventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите закрыть приложение",
                            "Закрытие приложения", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void MEnuRes()
        {

            panel3.Height = button1.Height;
            panel3.Top = button1.Top;
            dishes1.BringToFront();
            dishes1.load2();

        }
    }
}
