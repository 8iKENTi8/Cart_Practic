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

namespace Cart_Practic
{
    public partial class Adres_rest : Form
    {
        public Adres_rest()
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
                new MySqlCommand("SELECT  `restaurant_addresses`.`id_adres` AS 'id',`restaurants`.`name` AS 'Название ресторана'," +
                "`restaurant_addresses`.`place` AS 'Адрес', 'Update','Delete' " +
                "FROM `restaurant_addresses`, `restaurants` " +
                "WHERE `restaurant_addresses`.`restaurant_id`=" +
                "`restaurants`.`restaurant_id`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[3, i] = linkCell;
                table[3, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[4, i] = linkCell;
                table[4, i].Style.BackColor = Color.Tomato;
            }
        }
            
        public bool Proverka(DataGridViewCellEventArgs e)
        {
            table[0, e.RowIndex].Style.BackColor = Color.White;
            table[1, e.RowIndex].Style.BackColor = Color.White;
            table[2, e.RowIndex].Style.BackColor = Color.White;

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
                MessageBox.Show("Не введено название");
                return false;
            }
            else if (table.Rows[e.RowIndex].Cells[2].Value.ToString() == "")// проверяем 3-й столбец на пустые ячейки
            {
                table[2, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                MessageBox.Show("Не введен адрес");
                return false;
            }


            //Проверка на корректность данных

            if (Regex.Match(table.Rows[e.RowIndex].Cells[1].Value.ToString(), @"[0-9|[+]").Success)
            {
                table[1, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                MessageBox.Show("Может содержать только буквы");
                return false;
            }
           else if (Regex.Match(table.Rows[e.RowIndex].Cells[2].Value.ToString(), @"[0-9|[+]").Success)
            {
                table[2, e.RowIndex].Style.BackColor = Color.Tomato; // заодно покрасим
                MessageBox.Show("Может содержать только буквы");
                return false;
            }

            return true;
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    string task = table.Rows[e.RowIndex].Cells[3].Value.ToString();
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
                                MySqlCommand command = new MySqlCommand("UPDATE `restaurant_addresses` SET `place` = " +
                                    "@lg WHERE `restaurant_addresses`.`id_adres`=@ul  ", db.getConnection());

                                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                                command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();


                                db.openConnection();
                                if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Аккаунт был Обновлен"); }

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
                if (e.ColumnIndex == 4)
                {
                    string task = table.Rows[e.RowIndex].Cells[4].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `restaurant_addresses`" +
                                " WHERE `restaurant_addresses`.`id_adres` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Аккаунт был Удален"); }

                            db.closeConnection();
                        }
                    }
                  
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadDB();
            
        }

        private void Adres_rest_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Form form = new Admin_Form();
            form.Show();
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

        //Поиск
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("Адрес like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[3, i] = linkCell;
                    table[3, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[4, i] = linkCell;
                    table[4, i].Style.BackColor = Color.Tomato;
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

                    table[4, i] = linkCell;
                    table[4, i].Style.BackColor = Color.Tomato;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Add_adres().ShowDialog();
        }

       
    }
}
