using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cart_Practic.формы_админы
{
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
        }

        DataTable tab;
        private void ReloadDB()
        {

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `users`.`id`,`users`.`log` AS 'Логин',`users`.`email` AS 'Емаил'," +
                "`roles`.`title` AS 'Роль', 'Update','Delete' " +
                "FROM `users`, `roles`" +
                "WHERE `users`.`role_id`=`roles`.`role_id`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[4, i] = linkCell;
                table[4, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[5, i] = linkCell;
                table[5, i].Style.BackColor = Color.Tomato;
            }


        }
        
        public bool Proverka(DataGridViewCellEventArgs e)
        {
            table[0, e.RowIndex].Style.BackColor = Color.White;
            table[1, e.RowIndex].Style.BackColor = Color.White;
            table[2, e.RowIndex].Style.BackColor = Color.White;
            table[3, e.RowIndex].Style.BackColor = Color.White;


            //Проверка пустое ли значение
            if ((table.Rows[e.RowIndex].Cells[0].Value.ToString() == ""))// проверяем 1-й столбец на пустые ячейки
            {
                table[0, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                MessageBox.Show("Не введен id");
                return false;
            }
           else if (table.Rows[e.RowIndex].Cells[1].Value.ToString() == "")// проверяем 2-й столбец на пустые ячейки
            {
                table[1, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                MessageBox.Show("Не введен логин");
                return false;
            }
           else if (table.Rows[e.RowIndex].Cells[2].Value.ToString() == "")// проверяем 3-й столбец на пустые ячейки
            {
                table[2, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                MessageBox.Show("Не введен маил");
                return false;
            }
           else if (table.Rows[e.RowIndex].Cells[3].Value.ToString() == "")// проверяем 4-й столбец на пустые ячейки
            {
                table[3, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                MessageBox.Show("Не введена роль");
                return false;
            }

            //Проверка на корректность данных
            string mail = table.Rows[e.RowIndex].Cells[2].Value.ToString();
            string rol = table.Rows[e.RowIndex].Cells[3].Value.ToString();

            if (!mail.Contains("@") || !mail.Contains("."))
            {
                MessageBox.Show("Не корректный маил");
                table[2, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                return false;
            }
            else if (!(rol == "пользователь") && !(rol == "админ"))
            {
                MessageBox.Show("Не корректная роль");
                table[3, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                return false;
            }

            return true;
        }
        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    string task = table.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (task == "Update")
                    {
                        if (Proverka(e))
                        {
                            if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int rowIndex = e.RowIndex;

                                DB db = new DB();
                                MySqlCommand command = new MySqlCommand("UPDATE `users` SET `id` = @ul, `log` = @lg,  `email` = @em, `role_id` = @em1 WHERE `users`.`id` = @ul", db.getConnection());

                                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                                command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                                command.Parameters.Add("@em", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();

                                string role = table[3, rowIndex].Value.ToString();



                                if (Class_up_dish.roleId(role))
                                    command.Parameters.Add("@em1", MySqlDbType.VarChar).Value = 1;
                                else
                                    command.Parameters.Add("@em1", MySqlDbType.VarChar).Value = 2;



                                db.openConnection();
                                if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Пользователь был обновлен"); }

                                db.closeConnection();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (e.ColumnIndex == 5)
                {
                    string task = table.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `users`" +
                                " WHERE `users`.`id` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была удалена "); }

                            db.closeConnection();
                            ReloadDB();
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = table.Rows.Count - 2;


                        DB db = new DB();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `restaurants`" +
                            "(`restaurant_id`, `name`, `average_check`, `beg_time`, `end_time`, `description`) " +
                            "VALUES (@ul, @lg, @ps, @em, " +
                            "@em1, @em2)", db.getConnection());

                        command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                        command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                        command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                        command.Parameters.Add("@em", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();
                        command.Parameters.Add("@em1", MySqlDbType.VarChar).Value = table[4, rowIndex].Value.ToString();
                        command.Parameters.Add("@em2", MySqlDbType.VarChar).Value = table[5, rowIndex].Value.ToString();

                        table.Rows.RemoveAt(rowIndex);

                        table.Rows[e.RowIndex].Cells[7].Value = "Delete";

                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1) { MessageBox.Show("ресторан был добавлен"); }

                        db.closeConnection();
                        ReloadDB();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void users_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Form form = new Admin_Form();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new add_user().ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите закрыть приложение",
                             "Закрытие приложения", MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void search(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("Логин like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[4, i] = linkCell;
                    table[4, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[5, i] = linkCell;
                    table[5, i].Style.BackColor = Color.Tomato;
                }
            }

            if (txtSearch.Text == "")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[4, i] = linkCell;
                    table[4, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[5, i] = linkCell;
                    table[5, i].Style.BackColor = Color.Tomato;
                }
            }
        }
    }
}
