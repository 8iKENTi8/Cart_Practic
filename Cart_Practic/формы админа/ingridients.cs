﻿using MySql.Data.MySqlClient;
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
    public partial class ingridients : Form
    {
        public ingridients()
        {
            InitializeComponent();
        }

        DataTable tab;
        private bool newRowAdd = false;

        private void ReloadDB()
        {

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `ingredient_id` AS 'Id',`name` AS 'Название'," +
                " 'Update','Delete'" + "FROM `ingredients`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
            //label2.Text = tab.Rows[0]["name"].ToString();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[2, i] = linkCell;
                
                table[2, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[3, i] = linkCell;
                table[3, i].Style.BackColor = Color.Tomato;
            }


        }

        public bool Proverka(DataGridViewCellEventArgs e)
        {
            table[0, e.RowIndex].Style.BackColor = Color.White;
            table[1, e.RowIndex].Style.BackColor = Color.White;
            
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
                MessageBox.Show("Не введен ингридиент");
                return false;
            }
          

            //Проверка на корректность данных
          
            

            if (Regex.Match(table.Rows[e.RowIndex].Cells[1].Value.ToString(), @"[0-9|[+]").Success)
            {
                MessageBox.Show("Может содержать только буквы");
                return false;
            }

            return true;
        }
        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    string task = table.Rows[e.RowIndex].Cells[2].Value.ToString();
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
                                MySqlCommand command = new MySqlCommand("UPDATE `ingredients` SET `ingredient_id` = @ul, `name` = @lg WHERE `ingredients`.`ingredient_id` = @ul", db.getConnection());

                                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                                command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                                

                                db.openConnection();
                                if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была обновлена"); }

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
                if (e.ColumnIndex == 3)
                {
                    string task = table.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `ingredients`" +
                                " WHERE `ingredients`.`ingredient_id` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была удалена"); }

                            db.closeConnection();
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = table.Rows.Count - 2;
                      

                        DB db = new DB();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `ingredients`" +
                            "(`ingredient_id`, `name`) VALUES (@ul, @ul1)", db.getConnection());
                        command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                        command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();

                        table.Rows.RemoveAt(rowIndex);

                        table.Rows[e.RowIndex].Cells[3].Value = "Delete";

                        db.openConnection();
                        if (command.ExecuteNonQuery() == 1) { MessageBox.Show("ингридиент был добавлен"); }

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

        private void ingridients_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void table_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdd == false)
                {
                    newRowAdd = true;

                    int lastRow = tab.Rows.Count;

                    DataGridViewRow row = table.Rows[lastRow];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[3, lastRow] = linkCell;

                    row.Cells["Delete"].Value = "Insert";
                    table[3, lastRow].Style.BackColor = Color.Tomato;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void label8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите закрыть приложение",
                            "Закрытие приложения", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DataView data = tab.DefaultView;
                data.RowFilter = string.Format("Название like '%{0}%'", txtSearch.Text);
                table.DataSource = data.ToTable();

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[2, i] = linkCell;
                    table[2, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[3, i] = linkCell;
                    table[3, i].Style.BackColor = Color.Tomato;
                }
            }

            if (txtSearch.Text == "")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[2, i] = linkCell;
                    table[2, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[3, i] = linkCell;
                    table[3, i].Style.BackColor = Color.Tomato;
                }
            }
        }
    }
}
