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
    public partial class up_dish : UserControl
    {
        public up_dish()
        {
            InitializeComponent();
        }

        DataTable tab;

       
        public void load1()
        {
            comboBox1.Items.Clear();
            pictureBox1.Image = null;
            DB dB = new DB();

             tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `dishes`  WHERE `dishes`.`dish_id`=@ul", dB.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = Class_up_dish.id;

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            string url = "";
            url = $@"{tab.Rows[0][8]}";
            if (url == "")
            { }
            else
            {
                pictureBox1.Load(url);
            }
                


            textBox1.Text= tab.Rows[0][5].ToString();
            textBox2.Text= tab.Rows[0][3].ToString();
            textBox5.Text= tab.Rows[0][8].ToString();
            maskedTextBox1.Text= tab.Rows[0][6].ToString();
            maskedTextBox2.Text = tab.Rows[0][7].ToString();


            comboBox1.Text = Class_up_dish.id_cat;

            tab = new DataTable();

            command =
                new MySqlCommand("SELECT * FROM `categories` WHERE `categories`.`category_id` != @ul1", dB.getConnection());
            command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = Class_up_dish.id_cat;

            adapter.SelectCommand = command;

            adapter.Fill(tab);


            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox1.Items.Add(tab.Rows[i][1].ToString());
            }

        }


        private void up_dish_Load(object sender, EventArgs e)
        {
            //DB dB = new DB();

            //tab = new DataTable();

            //MySqlDataAdapter adapter = new MySqlDataAdapter();

            //MySqlCommand command =
            //    new MySqlCommand("SELECT `ingredients`.`ingredient_id` AS 'id' " +
            //    ", `ingredients`.`name` AS 'Название' FROM `ingredients`", dB.getConnection());

            //adapter.SelectCommand = command;

            //adapter.Fill(tab);

            //table.DataSource = tab;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox5.Text;

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            try
            {
                MySqlCommand command =
               new MySqlCommand("UPDATE `dishes` SET `img` = @im WHERE `dishes`.`dish_id` = @id", dB.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = Class_up_dish.id;
                command.Parameters.Add("@im", MySqlDbType.VarChar).Value = textBox5.Text;
                dB.openConnection();

                command.ExecuteNonQuery();
                dB.closeConnection();
                MessageBox.Show("Изменение прошло успешно");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

           

           
        }
    }
}
