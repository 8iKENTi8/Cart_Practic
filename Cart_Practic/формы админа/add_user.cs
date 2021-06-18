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
    public partial class add_user : MetroForm
    {
        public add_user()
        {
            InitializeComponent();
            label6.Visible = false;

            DB dB = new DB();

            DataTable tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * " +
                "FROM `roles`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);


            for (int i = 0; i < tab.Rows.Count; i++)
            {
                comboBox1.Items.Add(tab.Rows[i][1].ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string rol = comboBox1.Text,
           log = textBox3.Text,
           mail = textBox2.Text,
           pas = textBox1.Text;


            // Проверка выбран ли комбобокс

            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете роль";
                return;
            }



            if (string.IsNullOrEmpty(textBox1.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете пароль";
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете маил";
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                label6.Visible = true;
                label6.Text = "Выберете логин";
                return;
            }





            //Добавление записи в бд
            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("CALL `add-us`(@p0,@p1,@p2,@p3);",
                dB.getConnection());
            command.Parameters.Add("@p0", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@p1", MySqlDbType.VarChar).Value = pas;
            command.Parameters.Add("@p2", MySqlDbType.VarChar).Value = mail;
            command.Parameters.Add("@p3", MySqlDbType.VarChar).Value = rol;




            dB.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("пользователь был создан!");
            else
                MessageBox.Show("пользователь не был создан!");

            dB.closeConnection();

            this.Hide();
        }
    }
}
