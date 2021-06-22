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
        DataTable tab1;

        //Выгрузка селектов
     

        //Выгрузка рестаранов и ингридиентов
        public void res_and_ingr()
        {

            DB dB = new DB();

            

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            tab1 = new DataTable();
            

            MySqlCommand command =
                new MySqlCommand("SELECT `ingredients`.`ingredient_id`,`ingredients`.`name` AS 'Название'  FROM `ingredients` ", dB.getConnection());


            adapter.SelectCommand = command;

            adapter.Fill(tab1);
            table.DataSource = tab1;

            tab = new DataTable();

            command =
                new MySqlCommand("SELECT `restaurant_addresses`.`id_adres` AS 'Id'," +
                "`restaurants`.`name` AS 'Название',`restaurant_addresses`.`place` " +
                "AS 'Адрес' FROM `restaurants`,`restaurant_addresses` " +
                "WHERE `restaurant_addresses`.`restaurant_id`=" +
                "`restaurants`.`restaurant_id` ", dB.getConnection());


            adapter.SelectCommand = command;

            adapter.Fill(tab);

            dataGridView1.DataSource = tab;
        }

       
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


            Sostav.Text = tab.Rows[0][4].ToString();
            textBox1.Text= tab.Rows[0][5].ToString();
            textBox2.Text= tab.Rows[0][3].ToString();
            textBox5.Text= tab.Rows[0][8].ToString();
            maskedTextBox1.Text= tab.Rows[0][6].ToString();
            maskedTextBox2.Text = tab.Rows[0][7].ToString();
            Class_up_dish.id_res= tab.Rows[0][2].ToString();


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

            //Выгрузка рестаранов и ингридиентов

            res_and_ingr();

            

           

        }

        public void clearD()
        {
            dataGridView1.ClearSelection();
            table.ClearSelection();
            //SELECT `restaurants`.`restaurant_id`,`restaurant_addresses`.`id_adres` FROM `restaurants`,`restaurant_addresses` WHERE `restaurants`.`restaurant_id`= 3 AND `restaurants`.`restaurant_id`=`restaurant_addresses`.`restaurant_id`
            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `restaurants`.`restaurant_id`,`restaurant_addresses`.`id_adres`" +
                " FROM `restaurants`,`restaurant_addresses` WHERE `restaurants`.`restaurant_id`=@ul" +
                " AND `restaurants`.`restaurant_id`=`restaurant_addresses`.`restaurant_id`", dB.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = Class_up_dish.id_res;

            Class_up_dish.ingr1 = Class_up_dish.id_res;
            adapter.SelectCommand = command;

            adapter.Fill(tab);


            dataGridView1.Rows[Convert.ToInt32(tab.Rows[0][1])-1].Selected = true;
        }

        private void up_dish_Load(object sender, EventArgs e)
        {
           
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox5.Text;

            DB dB = new DB();

            tab = new DataTable();

          
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab1.DefaultView;
                data.RowFilter = string.Format("Название like '%{0}%'", textBox3.Text);
                table.DataSource = data.ToTable();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

            DB dB = new DB();



            MySqlDataAdapter adapter = new MySqlDataAdapter();

          

            tab = new DataTable();

            MySqlCommand command =
                new MySqlCommand("SELECT `restaurants`.`restaurant_id` AS 'Id'," +
                "`restaurants`.`name` AS 'Название',`restaurant_addresses`.`place` " +
                "AS 'Адрес' FROM `restaurants`,`restaurant_addresses` " +
                "WHERE `restaurant_addresses`.`restaurant_id`=" +
                "`restaurants`.`restaurant_id` ", dB.getConnection());


            adapter.SelectCommand = command;

            adapter.Fill(tab);

           

            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("Название like '%{0}%'", textBox4.Text);
                dataGridView1.DataSource = data.ToTable();


            }
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

          string a=  table.SelectedCells.ToString();
        }

        private void add_ingr_Click(object sender, EventArgs e)
        {
            if (Class_up_dish.ingr != "")
            {
                Sostav.Text += "," + Class_up_dish.ingr;
                

                DB db = new DB();
                MySqlCommand command = new MySqlCommand("UPDATE `dishes` SET `structure` = @ul1 WHERE `dishes`.`dish_id` = @ul", db.getConnection());

                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = Class_up_dish.id;
                command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = Sostav.Text;


                db.openConnection();
                if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Ингридиент добавлен"); }

                db.closeConnection();



                Class_up_dish.ingr = "";
            }
            else
                MessageBox.Show("Выберете ингридиент");
          

        }

        private void drop_ingr_Click(object sender, EventArgs e)
        {
            if (Class_up_dish.ingr != null)
            {
                if (Sostav.Text.Contains(Class_up_dish.ingr))
                {
                    Class_up_dish.ingr += ",";
                    Sostav.Text = Sostav.Text.Replace(Class_up_dish.ingr, "");
                   
                    DB db = new DB();
                    MySqlCommand command = new MySqlCommand("UPDATE `dishes` SET `structure` = @ul1 WHERE `dishes`.`dish_id` = @ul", db.getConnection());

                    command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = Class_up_dish.id;
                    command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = Sostav.Text;


                    db.openConnection();
                    if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Ингридиент удален"); }

                    db.closeConnection();
                    Class_up_dish.ingr = null;
                }
                else
                    MessageBox.Show("В этом блюде нет такого ингридиента");
                
                
            }
            else
                MessageBox.Show("Выберете ингридиент");
        }

        private void table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Class_up_dish.ingr = table[e.ColumnIndex, e.RowIndex].Value.ToString();
           
        }

        //Получаем id categorii
        public void getDataCat()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `categories` WHERE `categories`.`name`=@ul", db.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = comboBox1.Text;
           

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            adapter.SelectCommand = command;

            adapter.Fill(tab);

          Class_up_dish.id_cat= tab.Rows[0][0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("UPDATE `dishes` SET `category_id` = @ul1,`restaurant_id` = @ul2, `cost` = @ul3, `name` = @ul31, `beg_time` = @tm, `end_time` = @tm1  WHERE `dishes`.`dish_id` = @id", db.getConnection());
            getDataCat();
            command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = Class_up_dish.id_cat;
            command.Parameters.Add("@ul2", MySqlDbType.VarChar).Value = Class_up_dish.ingr1;
            command.Parameters.Add("@ul3", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@ul31", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@tm", MySqlDbType.VarChar).Value = maskedTextBox1.Text;
            command.Parameters.Add("@tm1", MySqlDbType.VarChar).Value = maskedTextBox2.Text;
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = Class_up_dish.id;


            db.openConnection();
            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была Обновлена"); }

            db.closeConnection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Class_up_dish.ingr1 = dataGridView1[0, e.RowIndex].Value.ToString();
        }
    }
}
