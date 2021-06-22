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
    public partial class dishes : UserControl
    {
        public dishes()
        {
            InitializeComponent();

            comboBox1.Text = "По умолчанию";
            comboBox1.Items.Add("По умолчанию");
            comboBox1.Items.Add("По возрастанию");
            comboBox1.Items.Add("По Убыванию");

            comboBox2.Text = "По умолчанию";
            comboBox2.Items.Add("По умолчанию");

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `categories`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);


            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox2.Items.Add(tab.Rows[i][1].ToString());
            }
        }

        DataTable tab;

        int check = 1;
        int page = 0;

        List<Control> list_names = new List<Control>();
        List<Control> list_cost = new List<Control>();
        List<Control> list_res = new List<Control>();
        List<PictureBox> list_img = new List<PictureBox>();
        List<Button> list_btn = new List<Button>();



        //Вывод блюд
        public void outputData(int count)
        {
            //Названия
            for (int i = 0; i < count; i++)
            {
                    list_names[i].Text = table[5, i].Value.ToString();
                list_btn[i].Visible = true;
                Class_up_dish.names[i] = table[5, i].Value.ToString();
            }

            for (int i = 0; i < 10; i++)
            {
                if (Class_up_dish.names[i] == Class_up_dish.names1[i])
                {
                    list_names[i].Text = "";
                    list_btn[i].Visible = false;
                    Class_up_dish.names[i] = Class_up_dish.names1[i];
                }
                else
                    Class_up_dish.names[i] = Class_up_dish.names1[i];
            }

            //Цены
            for (int i = 0; i < count; i++)
            {
                list_cost[i].Text = table[3, i].Value.ToString()+" р";
                Class_up_dish.cost[i] = table[3, i].Value.ToString();
            }

            for (int i = 0; i < 10; i++)
            {
                if (Class_up_dish.cost[i] == Class_up_dish.cost1[i])
                {
                    list_cost[i].Text = "";
                    Class_up_dish.cost[i] = Class_up_dish.cost1[i];
                }
                else
                    Class_up_dish.cost[i] = Class_up_dish.cost1[i];
            }

            ////Картинки
            

            for (int i = 0; i < count; i++)
            {
                list_img[i].ImageLocation= table[8, i].Value.ToString(); 
                Class_up_dish.img[i] = table[8, i].Value.ToString();
            }

            for (int i = 0; i < 10; i++)
            {
                if (Class_up_dish.img[i] == Class_up_dish.img1[i])
                {
                    list_img[i].ImageLocation = "";
                    Class_up_dish.img[i] = Class_up_dish.img1[i];
                }
                else
                    Class_up_dish.img[i] = Class_up_dish.img1[i];
            }


        }

        //добавление элементов в list
        public void listAdd()
        {
            list_names.Add(name0);
            list_names.Add(name1);
            list_names.Add(name2);
            list_names.Add(name3); list_names.Add(name4);
            list_names.Add(name5); list_names.Add(name6);
            list_names.Add(name7); list_names.Add(name8);
            list_names.Add(name9);

            list_cost.Add(cost0); list_cost.Add(cost1);
            list_cost.Add(cost2); list_cost.Add(cost3);
            list_cost.Add(cost4); list_cost.Add(cost5);
            list_cost.Add(cost6); list_cost.Add(cost7);
            list_cost.Add(cost8); list_cost.Add(cost9);

           
            list_img.Add(img0); list_img.Add(img1);
            list_img.Add(img2); list_img.Add(img3);
            list_img.Add(img4); list_img.Add(img5);
            list_img.Add(img6); list_img.Add(img7);
            list_img.Add(img8); list_img.Add(img9);

            list_res.Add(res1); list_res.Add(res2);
            list_res.Add(res3); list_res.Add(res4);
            list_res.Add(res5); list_res.Add(res6);
            list_res.Add(res7); list_res.Add(res8);
            list_res.Add(res9); list_res.Add(res10);

            list_btn.Add(but0); list_btn.Add(but1);
            list_btn.Add(but2); list_btn.Add(but3);
            list_btn.Add(but4); list_btn.Add(but5);
            list_btn.Add(but6); list_btn.Add(but7);
            list_btn.Add(but8); list_btn.Add(but9);
        }

        // Кол-во страниц
       public int GetPageCount()
        {
            int result = 0;

          DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT COUNT(*) FROM `dishes`", dB.getConnection());

            adapter.SelectCommand = command;

            dB.openConnection();
            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Почта был обновлена"); }

            

            double count = Convert.ToDouble(command.ExecuteScalar());

            result = Convert.ToInt32(Math.Ceiling(count/10));
            dB.closeConnection();

            return result;
        }

        public void PoYml(string com) 
        {
            page = GetPageCount();

            table.Visible = false;

            textBox1.Text = check.ToString();


            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command =
                new MySqlCommand(com, dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = check.ToString();

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;



            int count = table.Rows.Count - 1;


            listAdd();

            outputData(count);
        }

        private void dishes_Load(object sender, EventArgs e)
        {
            Class_up_dish.com = "CALL `get_dish`(@p0); ";
            PoYml(Class_up_dish.com);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            check = page;
            textBox1.Text = page.ToString();

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command =
                new MySqlCommand(Class_up_dish.com, dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = check.ToString();

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;



            int count = table.Rows.Count - 1;


            

            outputData(count);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            check = 1;
            textBox1.Text = check.ToString();

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command =
                new MySqlCommand(Class_up_dish.com, dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = check.ToString();

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;



            int count = table.Rows.Count - 1;

            outputData(count);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (check == 1) 
            {
                MessageBox.Show("Это самая первая страница, перелеснуть назад нельзя");
                return;
            }
               
                check -= 1;
            textBox1.Text = check.ToString();

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command =
                new MySqlCommand(Class_up_dish.com, dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = check.ToString();

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            int count = table.Rows.Count - 1;

            outputData(count);

        }

        private void button14_Click(object sender, EventArgs e)
        {
         if(check==(page))
            {
                MessageBox.Show("Это самая последняя страница, перелеснуть вперед нельзя");
                return;
            }
            check += 1;
            textBox1.Text = check.ToString();

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command =
                new MySqlCommand(Class_up_dish.com, dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = check.ToString();

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            int count = table.Rows.Count - 1;

            outputData(count);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString( comboBox1.SelectedItem )== "По умолчанию")
            {
                Class_up_dish.com = "CALL `get_dish`(@p0);";
                PoYml(Class_up_dish.com);
            }

            if (Convert.ToString(comboBox1.SelectedItem) == "По возрастанию")
            {
                Class_up_dish.com = "CALL `get_dish_up`(@p0);";
                PoYml(Class_up_dish.com);
            }

            if (Convert.ToString(comboBox1.SelectedItem) == "По Убыванию")
            {
                Class_up_dish.com = "CALL `get_dish_down`(@p0);";
                PoYml(Class_up_dish.com);
            }

        }

       // получить id категории
       public void getIdCat(string a)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `categories` WHERE `categories`.`name`=@ul", db.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = a;


            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            adapter.SelectCommand = command;

            adapter.Fill(tab);

            Class_up_dish.id_cat = tab.Rows[0][0].ToString();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(comboBox2.SelectedItem) != "По умолчанию")
            {
                getIdCat(comboBox2.SelectedItem.ToString());

                Class_up_dish.com =  $"CALL `get_dish_cat`(@p0, {Class_up_dish.id_cat});";

                PoYml(Class_up_dish.com);
            }
            else
            {
                Class_up_dish.com = "CALL `get_dish`(@p0);";
                PoYml(Class_up_dish.com);
            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtSearch.Text != "")
                {
                    Class_up_dish.com = $"CALL `get_dish_search`(@p0, '{txtSearch.Text}');";

                    PoYml(Class_up_dish.com);
                }
                else
                {
                    Class_up_dish.com = "CALL `get_dish`(@p0);";
                    PoYml(Class_up_dish.com);
                }
            }

            }
    }
}
