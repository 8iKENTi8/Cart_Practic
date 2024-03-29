﻿using MySql.Data.MySqlClient;
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
            label2.Visible = false;
            label3.Visible = false;
            button2.Visible = false;
          

            comboBox2.Text = "По умолчанию";
            comboBox2.Items.Add("По умолчанию");
           
            Class_up_dish.com= "CALL `get_dish`(@p0); ";
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
        List<PictureBox> list_img = new List<PictureBox>();
        List<Button> list_btn = new List<Button>();
        List<Control> list_tm = new List<Control>();
        List<Control> btn_tm = new List<Control>();




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

            //Время
            for (int i = 0; i < count; i++)
            {
                list_tm[i].Text = table[6, i].Value.ToString() + "-"+ table[7, i].Value.ToString();
                Class_up_dish.tm[i] = table[6, i].Value.ToString() + "-" + table[7, i].Value.ToString();
                btn_tm[i].Visible = true;
            }

            for (int i = 0; i < 10; i++)
            {
                if (Class_up_dish.tm[i] == Class_up_dish.tm1[i])
                {
                    list_tm[i].Text = "";
                    btn_tm[i].Visible = false;
                    Class_up_dish.tm[i] = Class_up_dish.tm1[i];
                }
                else
                    Class_up_dish.tm[i] = Class_up_dish.tm1[i];
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

          

            list_btn.Add(but0); list_btn.Add(but1);
            list_btn.Add(but2); list_btn.Add(but3);
            list_btn.Add(but4); list_btn.Add(but5);
            list_btn.Add(but6); list_btn.Add(but7);
            list_btn.Add(but8); list_btn.Add(but9);

            list_tm.Add(tm0); list_tm.Add(tm1);
            list_tm.Add(tm2); list_tm.Add(tm3);
            list_tm.Add(tm4); list_tm.Add(tm5);
            list_tm.Add(tm6); list_tm.Add(tm7);
            list_tm.Add(tm8); list_tm.Add(tm9);

            btn_tm.Add(tb0); btn_tm.Add(tb1);
            btn_tm.Add(tb2); btn_tm.Add(tb3);
            btn_tm.Add(tb4); btn_tm.Add(tb5);
            btn_tm.Add(tb6); btn_tm.Add(tb7);
            btn_tm.Add(tb8); btn_tm.Add(tb9);
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

            check = 1;
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

        public void load1()
        {
            Class_up_dish.com = "CALL `get_dish`(@p0); ";
            PoYml(Class_up_dish.com);
            comboBox1.Text = "По умолчанию";
            comboBox2.Text = "По умолчанию";

        }

        public void load2()
        {
            Class_up_dish.com = $"CALL `get_dish_menu`(@p0, {Class_up_dish.id_res});";
            PoYml(Class_up_dish.com);
            comboBox1.Text = "По умолчанию";
            comboBox2.Text = "По умолчанию";
            label2.Visible = true;
            label3.Visible = true;
            button2.Visible = true;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label1.Visible = false;
            filtcost.Visible = false;
            label3.Text = Class_up_dish.nameres;
        }

            private void dishes_Load(object sender, EventArgs e)
        {
            load1();

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
           
            if (Convert.ToString( comboBox1.SelectedItem )== "По умолчанию" && (Convert.ToString(comboBox2.SelectedItem) == "По умолчанию"|| comboBox2.SelectedItem == null))
            {
                Class_up_dish.com = "CALL `get_dish`(@p0);";
                PoYml(Class_up_dish.com);
            }
             else if (Convert.ToString(comboBox1.SelectedItem) == "По умолчанию" && Convert.ToString(comboBox2.SelectedItem) != "По умолчанию")
            {
                getIdCat(comboBox2.SelectedItem.ToString());
                Class_up_dish.com = $"CALL `get_dish_cat`(@p0, {Class_up_dish.id_cat});";
                PoYml(Class_up_dish.com);
            }


             if (Convert.ToString(comboBox1.SelectedItem) == "По возрастанию" && comboBox2.SelectedItem == null)
            {
                Class_up_dish.com = "CALL `get_dish_up`(@p0);";
                PoYml(Class_up_dish.com);
            }
           else if (Convert.ToString(comboBox1.SelectedItem) == "По возрастанию" && Convert.ToString(comboBox2.SelectedItem) != "По умолчанию")
            {
                getIdCat(comboBox2.SelectedItem.ToString());

                Class_up_dish.com = $"CALL `get_dish_cat_up`(@p0, {Class_up_dish.id_cat});";

                PoYml(Class_up_dish.com);
            }

             if (Convert.ToString(comboBox1.SelectedItem) == "По Убыванию" && comboBox2.SelectedItem == null)
            {

                Class_up_dish.com = $"CALL `get_dish_down`(@p0);";

                PoYml(Class_up_dish.com);
            }
           else if (Convert.ToString(comboBox1.SelectedItem) == "По Убыванию" && Convert.ToString(comboBox2.SelectedItem) != "По умолчанию")
            {
                getIdCat(comboBox2.SelectedItem.ToString());
                Class_up_dish.com = $"CALL `get_dish_cat_down`(@p0,{Class_up_dish.id_cat});";
                PoYml(Class_up_dish.com);
            }
          
            else if (Convert.ToString(comboBox1.SelectedItem) == "По Убыванию" && Convert.ToString(comboBox2.SelectedItem) == "По умолчанию")
            {

                Class_up_dish.com = $"CALL `get_dish_down`(@p0);";

                PoYml(Class_up_dish.com);
            }
            else if (Convert.ToString(comboBox1.SelectedItem) == "По возрастанию" && Convert.ToString(comboBox2.SelectedItem) == "По умолчанию")
            {

                Class_up_dish.com = $"CALL `get_dish_up`(@p0);";

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
            if(Convert.ToString(comboBox2.SelectedItem) != "По умолчанию" 
                && Convert.ToString(comboBox1.SelectedItem) == "По возрастанию")
            {
                getIdCat(comboBox2.SelectedItem.ToString());

                Class_up_dish.com = $"CALL `get_dish_cat_up`(@p0, {Class_up_dish.id_cat});";

                PoYml(Class_up_dish.com);
            }

            else if(Convert.ToString(comboBox2.SelectedItem) != "По умолчанию"
                && Convert.ToString(comboBox1.SelectedItem) == "По Убыванию")
            {
                getIdCat(comboBox2.SelectedItem.ToString());

                Class_up_dish.com = $"CALL `get_dish_cat_down`(@p0, {Class_up_dish.id_cat});";

                PoYml(Class_up_dish.com);
            }

           else if (Convert.ToString(comboBox2.SelectedItem) != "По умолчанию")
            {
                getIdCat(comboBox2.SelectedItem.ToString());

                Class_up_dish.com =  $"CALL `get_dish_cat`(@p0, {Class_up_dish.id_cat});";

                PoYml(Class_up_dish.com);
            }

            else if(Convert.ToString(comboBox2.SelectedItem) == "По умолчанию"&&Convert.ToString(comboBox1.SelectedItem) == "По умолчанию")
            {
                Class_up_dish.com = "CALL `get_dish`(@p0);";
                PoYml(Class_up_dish.com);
            }
            else if(Convert.ToString(comboBox2.SelectedItem) == "По умолчанию" && Convert.ToString(comboBox1.SelectedItem) == "По возрастанию")
            {
                Class_up_dish.com = "CALL `get_dish_up`(@p0);";
                PoYml(Class_up_dish.com);
            }
            else
            {
                Class_up_dish.com = "CALL `get_dish_down`(@p0);";
                PoYml(Class_up_dish.com);
            }

        }

        //Поиск
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtSearch.Text != "")
                {
                    string srt2 = "%" + txtSearch.Text + "%";
                    Class_up_dish.com1 = $"CALL `get_dish_search`(@p0, '{srt2}');";

                    PoYml(Class_up_dish.com1);
                }
                else
                {
                    
                    PoYml(Class_up_dish.com);
                   
                }
            }

            }


        private void but0_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 0].Value.ToString();
            
            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();

        }

        private void but1_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 1].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but2_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 2].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but3_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 3].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but4_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 4].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but5_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 5].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but6_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 6].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but7_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 7].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but8_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 8].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void but9_Click(object sender, EventArgs e)
        {
            Class_up_dish.id = table[0, 9].Value.ToString();

            Cart_dish form = new Cart_dish();
            form.Show();
            form.Load1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class_up_dish.com = "CALL `get_dish`(@p0); ";
            PoYml(Class_up_dish.com);
            label2.Visible = false;
            label3.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            label1.Visible = true;
            filtcost.Visible = true;
            label3.Visible = false;
        }
    }
}
