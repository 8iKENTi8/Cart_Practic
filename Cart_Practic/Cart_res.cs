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
    public partial class Cart_res : Form
    {
        public Cart_res()
        {
            InitializeComponent();
            table.Visible = false;
            adres.Text = "";
        }
        DataTable tab, tab1;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Load1()
        {
            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `restaurants` WHERE" +
                " `restaurants`.`restaurant_id`=@UL", dB.getConnection());
            command.Parameters.Add("@UL", MySqlDbType.VarChar).Value = Class_up_dish.id_res;

            adapter.SelectCommand = command;


            adapter.Fill(tab);

            table.DataSource = tab;

           name.Text= table[1, 0].Value.ToString();
            Class_up_dish.nameres= table[1, 0].Value.ToString(); 
            avch.Text="Средний чек: "+ table[2, 0].Value.ToString();
            time.Text= table[3, 0].Value.ToString()+"-"+ table[4, 0].Value.ToString(); 
            desc.Text= table[5, 0].Value.ToString();
            pictureBox1.ImageLocation= table[6, 0].Value.ToString();

            tab1 = new DataTable();


            command =
                new MySqlCommand("SELECT `restaurants`.`name`,`restaurant_addresses`.`place` " +
                "FROM `restaurants`,`restaurant_addresses` WHERE " +
                "`restaurant_addresses`.`restaurant_id`=`restaurants`.`restaurant_id` " +
                "AND `restaurants`.`restaurant_id`=@UL", dB.getConnection());
            command.Parameters.Add("@UL", MySqlDbType.VarChar).Value = Class_up_dish.id_res;

            adapter.SelectCommand = command;


            adapter.Fill(tab1);

            table.DataSource = tab1;

            
            int count1 = table.Rows.Count-1 ;

            if (count1 == 1)
                for (int i = 0; i < count1; i++)
                {
                    adres.Text = table[1, i].Value.ToString();
                }
            else
                for (int i = 0; i < count1; i++)
                {
                    if(i==count1-1)
                        adres.Text += table[1, i].Value.ToString();
                    else
                        adres.Text += table[1, i].Value.ToString() + ", ";

                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Class_up_dish.form.MEnuRes();





        }

        private void label15_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите закрыть приложение",
                           "Закрытие приложения", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
