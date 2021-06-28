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
    public partial class resta : UserControl
    {
        public resta()
        {
            InitializeComponent();


            comboBox1.Text = "По умолчанию";
            comboBox1.Items.Add("По умолчанию");
            comboBox1.Items.Add("С 7 утра");
            comboBox1.Items.Add("С 8 утра");
            comboBox1.Items.Add("С 9 утра");
            comboBox1.Items.Add("До 9 вечера");
            comboBox1.Items.Add("До 10 вечера");
            comboBox1.Items.Add("До 11 вечера");




            comboBox2.Text = "По умолчанию";
            comboBox2.Items.Add("По умолчанию");
            comboBox2.Items.Add("Дорогой");
            comboBox2.Items.Add("Обычный");
            comboBox2.Items.Add("Не дорогой");

        }

        DataTable tab;

        int check = 1;
        int page = 0;

        List<Control> list_names = new List<Control>();
        List<Button> list_btn = new List<Button>();
        List<PictureBox> list_img = new List<PictureBox>();
        List<Control> list_sr = new List<Control>();
        List<Control> list_tm = new List<Control>();
        List<Control> list_tb = new List<Control>();

        public int GetPageCount()
        {
            int result = 0;

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT COUNT(*) FROM `restaurants`", dB.getConnection());

            adapter.SelectCommand = command;

            dB.openConnection();
            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Почта был обновлена"); }



            double count = Convert.ToDouble(command.ExecuteScalar());

            result = Convert.ToInt32(Math.Ceiling(count / 10));
            dB.closeConnection();

            return result;
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

            list_btn.Add(but0); list_btn.Add(but1);
            list_btn.Add(but2); list_btn.Add(but3);
            list_btn.Add(but4); list_btn.Add(but5);
            list_btn.Add(but6); list_btn.Add(but7);
            list_btn.Add(but8); list_btn.Add(but9);

            list_img.Add(img0); list_img.Add(img1);
            list_img.Add(img2); list_img.Add(img3);
            list_img.Add(img4); list_img.Add(img5);
            list_img.Add(img6); list_img.Add(img7);
            list_img.Add(img8); list_img.Add(img9);

            list_sr.Add(sr0); list_sr.Add(sr1);
            list_sr.Add(sr2); list_sr.Add(sr3);
            list_sr.Add(sr4); list_sr.Add(sr5);
            list_sr.Add(sr6); list_sr.Add(sr7);
            list_sr.Add(sr8); list_sr.Add(sr9);

            list_tm.Add(tm0); list_tm.Add(tm1);
            list_tm.Add(tm2); list_tm.Add(tm3);
            list_tm.Add(tm4); list_tm.Add(tm5);
            list_tm.Add(tm6); list_tm.Add(tm7);
            list_tm.Add(tm8); list_tm.Add(tm9);

            list_tb.Add(tb0); list_tb.Add(tb1);
            list_tb.Add(tb2); list_tb.Add(tb3);
            list_tb.Add(tb4); list_tb.Add(tb5);
            list_tb.Add(tb6); list_tb.Add(tb7);
            list_tb.Add(tb8); list_tb.Add(tb9);

        }

        //Вывод ресторанов
        public void outputData(int count)
        {
            //Названия
            for (int i = 0; i < count; i++)
            {
                list_names[i].Text = table[1, i].Value.ToString();
                list_btn[i].Visible = true;
                Class_up_dish.names[i] = table[1, i].Value.ToString();
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


            ////Картинки


            for (int i = 0; i < count; i++)
            {
                list_img[i].ImageLocation = table[6, i].Value.ToString();
                Class_up_dish.img[i] = table[6, i].Value.ToString();
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

            //Средний чек
            for (int i = 0; i < count; i++)
            {
                list_sr[i].Text = "Средний чек: " + table[2, i].Value.ToString() + " р";
                
                Class_up_dish.cost[i] = "Средний чек: " + table[2, i].Value.ToString() + " р";
            }

            for (int i = 0; i < 10; i++)
            {
                if (Class_up_dish.cost[i] == Class_up_dish.cost1[i])
                {
                    list_sr[i].Text = "";
                   
                    Class_up_dish.cost[i] = Class_up_dish.cost1[i];
                }
                else
                    Class_up_dish.cost[i] = Class_up_dish.cost1[i];
            }

            //Время
            for (int i = 0; i < count; i++)
            {
                list_tm[i].Text = table[3, i].Value.ToString() + "-"+ table[4, i].Value.ToString();

                Class_up_dish.tm[i] = table[3, i].Value.ToString() + "-" + table[4, i].Value.ToString();
                list_tb[i].Visible = true;
            }

            for (int i = 0; i < 10; i++)
            {
                if (Class_up_dish.tm[i] == Class_up_dish.tm1[i])
                {
                    list_tm[i].Text = "";
                    list_tb[i].Visible = false;

                    Class_up_dish.tm[i] = Class_up_dish.tm1[i];
                }
                else
                    Class_up_dish.tm[i] = Class_up_dish.tm1[i];
            }

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
            Class_up_dish.com = "CALL `get_rest`(@p0); ";
            PoYml(Class_up_dish.com);
        }
        private void resta_Load(object sender, EventArgs e)
        {
            
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
            if (check == (page))
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtSearch.Text != "")
                {
                    string srt2 = "%"+txtSearch.Text+"%";
                    Class_up_dish.com1 = $"CALL `get_res_search`(@p0, '{srt2}');";

                    PoYml(Class_up_dish.com1);
                }
                else
                {

                    PoYml(Class_up_dish.com);

                }
            
              }           
            }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str, str2;
            if (comboBox2.SelectedItem == null)
            {
                str = comboBox1.SelectedItem.ToString(); str2 = "";
            }
            else
            {
                if(comboBox1.SelectedItem == null)
                {
                    str = ""; str2 = comboBox2.SelectedItem.ToString();
                }

                else
                {
                    str = comboBox1.SelectedItem.ToString(); str2 = comboBox2.SelectedItem.ToString();
                }
            }

            if (Convert.ToString(comboBox2.SelectedItem) == "По умолчанию" && (comboBox1.SelectedItem == null || Convert.ToString(comboBox1.SelectedItem) == "По умолчанию"))
            {
                Class_up_dish.com = "CALL `get_rest`(@p0);";
                PoYml(Class_up_dish.com);
            }
            else if ((comboBox1.SelectedItem != null || Convert.ToString(comboBox1.SelectedItem) != "По умолчанию") && (comboBox2.SelectedItem == null || Convert.ToString(comboBox2.SelectedItem) == "По умолчанию"))
            {

                Ifcon(str, str2);
            }
            else if ((comboBox1.SelectedItem != null || Convert.ToString(comboBox1.SelectedItem) != "По умолчанию") && (comboBox2.SelectedItem != null || Convert.ToString(comboBox2.SelectedItem) != "По умолчанию"))
            {
                Ifcon(str, str2);
            }
             if (Convert.ToString(comboBox2.SelectedItem) == "Дорогой" && (Convert.ToString(comboBox1.SelectedItem) == "По умолчанию" || comboBox1.SelectedItem == null))
            {
                Class_up_dish.com = "CALL `get_res_dorog`(@p0);";
                PoYml(Class_up_dish.com);
            }

             if (Convert.ToString(comboBox2.SelectedItem) == "Обычный" && (Convert.ToString(comboBox1.SelectedItem) == "По умолчанию" || comboBox1.SelectedItem == null))
            {
                Class_up_dish.com = "CALL `get_res_simple`(@p0);";
                PoYml(Class_up_dish.com);
            }
             if (Convert.ToString(comboBox2.SelectedItem) == "Не дорогой" && (Convert.ToString(comboBox1.SelectedItem) == "По умолчанию" || comboBox1.SelectedItem == null))
            {
                Class_up_dish.com = "CALL `get_res_cheap`(@p0);";
                PoYml(Class_up_dish.com);
            }
            else
            {
                
                if((comboBox2.SelectedItem == null || Convert.ToString(comboBox2.SelectedItem) == "По умолчанию") && str.Contains("С"))
                {
                    if(str.Contains('9'))
                    {
                        Class_up_dish.com = "CALL `ger_res_time_begin`(@p0, 9);";
                    }
                    if (str.Contains('8'))
                    {
                        Class_up_dish.com = "CALL `ger_res_time_begin`(@p0, 8);";
                    }
                    if (str.Contains('7'))
                    {
                        Class_up_dish.com = "CALL `ger_res_time_begin`(@p0, 7);";
                    }
                  

                }
                else if((comboBox2.SelectedItem == null || Convert.ToString(comboBox2.SelectedItem) == "По умолчанию") && str.Contains("До"))
                {
                    if (str.Contains('9'))
                    {
                        Class_up_dish.com = "CALL `get_res_tm_down`(@p0, 21);";
                    }
                    if (str.Contains("10"))
                    {
                        Class_up_dish.com = "CALL `get_res_tm_down`(@p0, 22);";
                    }
                    if (str.Contains("11"))
                    {
                        Class_up_dish.com = "CALL `get_res_tm_down`(@p0, 23);";
                    }
                    
                }
                PoYml(Class_up_dish.com);
            }

        }

        public void conC(string str,string str2)
        {
            if (str.Contains('9'))
            {
                if (str2 == "Дорогой")
                    Class_up_dish.com = "CALL `get_res_beg_dorog`(@p0, 9);";

                else if (str2 == "Обычный")
                    Class_up_dish.com = "CALL `get_res_beg_simp`(@p0, 9);";

                else if (str2 == "Не дорогой")
                    Class_up_dish.com = "CALL `get_res_beg_cheap`(@p0, 9);";

                else
                    Class_up_dish.com = "CALL `ger_res_time_begin`(@p0, 9);";

            }

            else if (str.Contains('8'))
            {
                if (str2 == "Дорогой")
                    Class_up_dish.com = "CALL `get_res_beg_dorog`(@p0, 8);";

                else if (str2 == "Обычный")
                    Class_up_dish.com = "CALL `get_res_beg_simp`(@p0, 8);";

                else if (str2 == "Не дорогой")
                    Class_up_dish.com = "CALL `get_res_beg_cheap`(@p0, 8);";


                else
                    Class_up_dish.com = "CALL `ger_res_time_begin`(@p0, 8);";
            }

            else if (str.Contains('7'))
            {
                if (str2 == "Дорогой")
                    Class_up_dish.com = "CALL `get_res_beg_dorog`(@p0, 7);";

                else if (str2 == "Обычный")
                    Class_up_dish.com = "CALL `get_res_beg_simp`(@p0, 7);";

                else if (str2 == "Не дорогой")
                    Class_up_dish.com = "CALL `get_res_beg_cheap`(@p0, 7);";

                else
                    Class_up_dish.com = "CALL `ger_res_time_begin`(@p0, 7);";
            }
        }

        public void conDO(string str, string str2)
        {
            if (str.Contains('9'))
            {
                if (str2 == "Дорогой")
                    Class_up_dish.com = "CALL `get_res_dorog_down`(@p0, 21);";

                else if (str2 == "Обычный")
                    Class_up_dish.com = "CALL `get_res_simp_down`(@p0, 21);";

                else if (str2 == "Не дорогой")
                    Class_up_dish.com = "CALL `get_res_cheap_down`(@p0, 21);";

                else
                    Class_up_dish.com = "CALL `get_res_tm_down`(@p0, 21);";

            }

            else if (str.Contains("10"))
            {
                if (str2 == "Дорогой")
                    Class_up_dish.com = "CALL `get_res_dorog_down`(@p0, 22);";

                else if (str2 == "Обычный")
                    Class_up_dish.com = "CALL `get_res_simp_down`(@p0, 22);";

                else if (str2 == "Не дорогой")
                    Class_up_dish.com = "CALL `get_res_cheap_down`(@p0, 22);";


                else
                    Class_up_dish.com = "CALL `get_res_tm_down`(@p0, 22);";
            }

            else if (str.Contains("11"))
            {
                if (str2 == "Дорогой")
                    Class_up_dish.com = "CALL `get_res_dorog_down`(@p0, 23);";

                else if (str2 == "Обычный")
                    Class_up_dish.com = "CALL `get_res_simp_down`(@p0, 23);";

                else if (str2 == "Не дорогой")
                    Class_up_dish.com = "CALL `get_res_cheap_down`(@p0, 23);";

                else
                    Class_up_dish.com = "CALL `get_res_tm_down`(@p0, 23);";
            }
        }
        public void Ifcon(string str,string str2)
        {
            //comboBox2.Items.Add("Дорогой");
            //comboBox2.Items.Add("Обычный");
            //comboBox2.Items.Add("Не дорогой");
            if (str.Contains("С"))
            {
                conC(str, str2);
            }
            else if (str.Contains("До"))
            {
                conDO(str, str2);
            }
            PoYml(Class_up_dish.com);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str, str2;
            if (comboBox2.SelectedItem == null)
            {
                str = comboBox1.SelectedItem.ToString(); str2 = "";
            }
            else
            {
                str = comboBox1.SelectedItem.ToString(); str2 = comboBox2.SelectedItem.ToString();
            }
               

            if ((comboBox2.SelectedItem == null || Convert.ToString(comboBox2.SelectedItem) == "По умолчанию") && (comboBox1.SelectedItem == null || Convert.ToString(comboBox1.SelectedItem) == "По умолчанию"))
            {
                Class_up_dish.com = "CALL `get_rest`(@p0);";
                PoYml(Class_up_dish.com);
            }
            else if((comboBox1.SelectedItem != null || Convert.ToString(comboBox1.SelectedItem) != "По умолчанию")&& (comboBox2.SelectedItem == null || Convert.ToString(comboBox2.SelectedItem) == "По умолчанию"))
            {
                
                Ifcon(str,str2);
            }
            else if ((comboBox1.SelectedItem != null || Convert.ToString(comboBox1.SelectedItem) != "По умолчанию") && (comboBox2.SelectedItem != null || Convert.ToString(comboBox2.SelectedItem) != "По умолчанию"))
            {
                Ifcon(str,str2);
            }
             if(Convert.ToString(comboBox2.SelectedItem) == "Дорогой" && (comboBox1.SelectedItem == null || Convert.ToString(comboBox1.SelectedItem) == "По умолчанию"))
            {
                Class_up_dish.com = "CALL `get_res_dorog`(@p0);";
                PoYml(Class_up_dish.com);
            }
             if (Convert.ToString(comboBox2.SelectedItem) == "Обычный" && (comboBox1.SelectedItem == null || Convert.ToString(comboBox1.SelectedItem) == "По умолчанию"))
            {
                Class_up_dish.com = "CALL `get_res_simple`(@p0);";
                PoYml(Class_up_dish.com);
            }
             if (Convert.ToString(comboBox2.SelectedItem) == "Не дорогой" && (comboBox1.SelectedItem == null || Convert.ToString(comboBox1.SelectedItem) == "По умолчанию"))
            {
                Class_up_dish.com = "CALL `get_res_cheap`(@p0);";
                PoYml(Class_up_dish.com);
            }
        }

        private void but0_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 0].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
            
        }

        private void but1_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 1].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but2_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 2].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but3_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 3].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but4_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 4].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but5_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 5].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but6_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 6].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but7_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 7].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but8_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 8].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }

        private void but9_Click(object sender, EventArgs e)
        {
            Class_up_dish.id_res = table[0, 9].Value.ToString();

            Cart_res form = new Cart_res();
            form.Show();
            form.Load1();
        }
    }
}
