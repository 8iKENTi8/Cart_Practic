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
        }

        DataTable tab;

        int check = 1;
        int page = 0;

        List<Control> list_names = new List<Control>();
        List<Control> list_cost = new List<Control>();
        List<Control> list_res = new List<Control>();



        //Вывод блюд
        public void outputData(int count)
        {

            for (int i = 0; i < count; i++)
            {
                list_names[i].Text = table[5, i].Value.ToString();
            }

            for (int i = 0; i < count; i++)
            {
                list_cost[i].Text = table[3, i].Value.ToString()+" р";
            }
            
        }

        //добавление элементов в list
        public void listAdd()
        {
            list_names.Add(name1);
            list_names.Add(name2);
            list_names.Add(name3);
            list_names.Add(name4); list_names.Add(name5);
            list_names.Add(name6); list_names.Add(name7);
            list_names.Add(name8); list_names.Add(name9);
            list_names.Add(name10);

            list_cost.Add(cost1); list_cost.Add(cost2);
            list_cost.Add(cost3); list_cost.Add(cost4);
            list_cost.Add(cost5); list_cost.Add(cost6);
            list_cost.Add(cost7); list_cost.Add(cost8);
            list_cost.Add(cost9); list_cost.Add(cost10);

            list_res.Add(res1); list_res.Add(res2);
            list_res.Add(res3); list_res.Add(res4);
            list_res.Add(res5); list_res.Add(res6);
            list_res.Add(res7); list_res.Add(res8);
            list_res.Add(res9); list_res.Add(res10);
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

        private void dishes_Load(object sender, EventArgs e)
        {
            page= GetPageCount();

            table.Visible = false;

            textBox1.Text = check.ToString();

            

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            
            MySqlCommand command =
                new MySqlCommand(" CALL `get_dish`(@p0);", dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = check.ToString();

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

           

            int count = table.Rows.Count-1;


            listAdd();

            outputData(count-1);


        }

        private void button11_Click(object sender, EventArgs e)
        {
            check = page;
            textBox1.Text = page.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            check = 1;
            textBox1.Text = check.ToString();
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
        }
    }
}
