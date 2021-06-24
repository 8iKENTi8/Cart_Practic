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
    public partial class Sign_In : Form
    {
        int a = 0;
        public Sign_In()
        {
            InitializeComponent();
            

        }
        DataTable table;

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Проверка есть ли пользователь в бд
        public Boolean isUserExists(string log, string pass)
        {
            DB dB = new DB();

             table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `users` WHERE `log` = @ul AND" +
                "`pass`= @up AND `role_id`=2", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);

           

            if (table.Rows.Count > 0)
            {
                Class_up_dish.id = table.Rows[0][1].ToString();
                Class_up_dish.id_ingr = table.Rows[0][3].ToString();
                return true;
            }
                

            command =
              new MySqlCommand("SELECT * FROM `users` WHERE `log` = @ul AND" +
              "`pass`= @up AND `role_id`=1", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;


            adapter.SelectCommand = command;

            adapter.Fill(table);

            

            if (table.Rows.Count > 0)
            {
                Class_up_dish.id = table.Rows[0][1].ToString();
                Class_up_dish.id_ingr = table.Rows[0][3].ToString();
                a = 1;
                return true;
            }

            return false;

        }

        private void auth(object sender, EventArgs e)
        {
            string login = bunifuMaterialTextbox1.Text.Trim(),
                   pass = bunifuMaterialTextbox2.Text.Trim();


            if (login.Length < 5)
            {
                MessageBox.Show("Логин введен неверно!");
                return;
            }

            if (pass.Length < 5)
            {
                MessageBox.Show("Пароль введен неверно!");
                return;
            }

            if (isUserExists(login, pass) && a == 1)
            {
                MessageBox.Show("Вы вошли как admin");
                this.Hide();
                Admin_Form form = new Admin_Form();
                form.Show();
                return;
            }

            else if (isUserExists(login, pass))
            {
                MessageBox.Show("Вы вошли как user");
                this.Hide();
                User_Form form = new User_Form();
                form.Show();
                return;
            }
            else
                MessageBox.Show("Пользователя не существует");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string login = bunifuMaterialTextbox1.Text.Trim(),
                pass = bunifuMaterialTextbox2.Text.Trim();


            if (login.Length < 5)
            {
                MessageBox.Show("Логин введен неверно!");
                return;
            }

            if (pass.Length < 5)
            {
                MessageBox.Show("Пароль введен неверно!");
                return;
            }

            if (isUserExists(login, pass) && a == 1)
            {
                MessageBox.Show("Вы вошли как admin");
                this.Hide();
                Admin_Form form = new Admin_Form();
                form.Show();
                return;
            }

            else if (isUserExists(login, pass))
            {
                MessageBox.Show("Вы вошли как user");
                this.Hide();
                Class_up_dish.id1 = table.Rows[0][1].ToString();
                Class_up_dish.id2 = table.Rows[0][3].ToString();
                Class_up_dish.form.ShowDialog();
                //User_Form form = new User_Form();
                //form.Show();
                return;
            }
            else
                MessageBox.Show("Пользователя не существует");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sign_Up form = new Sign_Up();
            form.Show();
        }
    }
}
