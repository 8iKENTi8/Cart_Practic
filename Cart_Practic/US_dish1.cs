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
    public partial class US_dish1 : UserControl
    {
        public US_dish1()
        {
            InitializeComponent();
            ReloadDB();
            up_dish1.Hide();
        }
        DataTable tab;

        private void ReloadDB()
        {

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT  `restaurant_addresses`.`restaurant_id` AS 'id',`restaurants`.`name` AS 'Название ресторана'," +
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

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    string task = table.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (task == "Update")
                    {
                        up_dish1.Show();
                        up_dish1.BringToFront();
                        if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("UPDATE `restaurant_addresses` SET `place` = " +
                                "@lg WHERE `restaurant_addresses`.`restaurant_id`=@ul  ", db.getConnection());

                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();


                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Аккаунт был Обновлен"); }

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
                                " WHERE `restaurant_addresses`.`restaurant_id` = @ul ", db.getConnection());
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

        private void US_dish1_Load(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private void up_dish1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            up_dish1.Hide();
        }
    }
}