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
    public partial class UScab : UserControl
    {
        public UScab()
        {
            InitializeComponent();

            label1.Text = Class_up_dish.id1;
            email.Text = Class_up_dish.id2;
        }

        public void load1()
        {
            label1.Text = Class_up_dish.id1;
            email.Text = Class_up_dish.id2;
        }

        public bool true_pass()
        {
            DB dB = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `users` WHERE `log` = @ul AND" +
                "`pass`= @up AND `role_id`=2", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = Class_up_dish.id;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = textBox1.Text;

            adapter.SelectCommand = command;

            adapter.Fill(table);

           

            if (table.Rows.Count > 0)
                return true;
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Введите свой старый пароль");
            else if (true_pass())
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("UPDATE `users` SET `email` = @ul WHERE `users`.`log` = @ul1", db.getConnection());

                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = email.Text;
                command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = label1.Text ;
               

                db.openConnection();
                if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Почта был обновлена"); }

                db.closeConnection();
            }
            else
                MessageBox.Show("Пароль не верный");



        }
    }
}
