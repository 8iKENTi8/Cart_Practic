using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace Cart_Practic
{
    public partial class Add_adres : MetroForm
    {
        public Add_adres()
        {
            InitializeComponent();
            label6.Visible = false;

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `restaurants`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);


            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox1.Items.Add(tab.Rows[i][1].ToString());
            }

           
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string res = comboBox1.Text,
             place = textBox2.Text;
            

            // Проверка выбран ли комбобокс

            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете ресторан";
                return;
            }
          

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете место";
                return;
            }
           

            //Добавление записи в бд
            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("CALL `add-adres`(@p0, @p1);",
                dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = res;
            command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = place;
          



            dB.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("адрес был создан!");
            else
                MessageBox.Show("адрес не был создан!");

            dB.closeConnection();

            this.Hide();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b'&& l!='.')
            {
                e.Handled = true;
            }
        }
    }
}
