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
    public partial class Cart_dish : Form
    {
        public Cart_dish()
        {
            InitializeComponent();
            table.Visible = false;
        }
        DataTable tab,tab1;
        public void Load1()
        {
            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `dishes` WHERE" +
                " `dishes`.`dish_id`=@UL", dB.getConnection());
            command.Parameters.Add("@UL", MySqlDbType.VarChar).Value = Class_up_dish.id;

            adapter.SelectCommand = command;


            adapter.Fill(tab);

            table.DataSource = tab;

            cost.Text = table[3, 0].Value.ToString()+" Р";
            name.Text = table[5, 0].Value.ToString();
            sostav.Text = table[4, 0].Value.ToString();
            time.Text = table[6, 0].Value.ToString() + "-" + table[7, 0].Value.ToString();
            pictureBox1.ImageLocation= table[8, 0].Value.ToString();

            tab1 = new DataTable();

           
            command =
                new MySqlCommand("SELECT `restaurants`.`name`,`restaurant_addresses`.`place` " +
                "FROM `dishes`,`restaurants`,`restaurant_addresses` WHERE " +
                "`restaurants`.`restaurant_id`=`dishes`.`restaurant_id` AND " +
                "`restaurant_addresses`.`restaurant_id`=`restaurants`.`restaurant_id` " +
                "AND `dishes`.`dish_id`=@UL", dB.getConnection());
            command.Parameters.Add("@UL", MySqlDbType.VarChar).Value = Class_up_dish.id;

            adapter.SelectCommand = command;


            adapter.Fill(tab1);

            table.DataSource = tab1;

            label11.Text = table[0, 0].Value.ToString();
            int count1 = table.Columns.Count - 1;

            if(count1==1)
                for (int i = 0; i < count1; i++)
                {
                    label12.Text = table[1, i].Value.ToString();
                }
            else
                for (int i = 0; i < count1; i++)
                {
                    label12.Text = table[1, i].Value.ToString()+", ";
                }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
