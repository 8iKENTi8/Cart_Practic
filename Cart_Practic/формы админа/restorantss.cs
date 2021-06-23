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
    public partial class restorantss : Form
    {
        public restorantss()
        {
            InitializeComponent();
        }

        DataTable tab;
        private bool newRowAdd = false;

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReloadDB()
        {

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT `restaurants`.`restaurant_id` AS 'id'," +
                " `restaurants`.`name` AS 'Название', `restaurants`.`average_check`" +
                " AS 'Средний чек', `restaurants`.`beg_time` AS 'Открытие ресторана'," +
                " `restaurants`.`end_time` AS 'Закрытие ресторана'," +
                " `restaurants`.`description` AS 'Описание', " +
                "`restaurants`.`img` AS 'Изображение'," +
                "'Update','Delete' FROM `restaurants`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
            //label2.Text = tab.Rows[0]["name"].ToString();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[7, i] = linkCell;

                table[7, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[8, i] = linkCell;
                table[8, i].Style.BackColor = Color.Tomato;
            }


        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    string task = table.Rows[e.RowIndex].Cells[7].Value.ToString();
                    if (task == "Update")
                    {
                        if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("UPDATE `restaurants` SET " +
                                "`restaurant_id` = @ul, " +
                                "`name` = @lg, `average_check` = @ps, " +
                                "`beg_time` = @em, `end_time` = @em1, `description` = @em2, `img`=@em3" +
                                " WHERE `restaurants`.`restaurant_id` = @ul", db.getConnection());

                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                            command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();
                            command.Parameters.Add("@em1", MySqlDbType.VarChar).Value = table[4, rowIndex].Value.ToString();
                            command.Parameters.Add("@em2", MySqlDbType.VarChar).Value = table[5, rowIndex].Value.ToString();
                            command.Parameters.Add("@em3", MySqlDbType.VarChar).Value = table[6, rowIndex].Value.ToString();

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Ресторан был обновлен"); }

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
                if (e.ColumnIndex == 8)
                {
                    string task = table.Rows[e.RowIndex].Cells[8].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `restaurants`" +
                                " WHERE `restaurants`.`restaurant_id` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Ресторан был удален"); }

                            db.closeConnection();
                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = table.Rows.Count - 2;


                        DB db = new DB();
                        MySqlCommand command = new MySqlCommand("INSERT INTO `restaurants`" +
                            "(`restaurant_id`, `name`, `average_check`, `beg_time`, `end_time`, `description`,`img`) " +
                            "VALUES (@ul, @lg, @ps, @em, " +
                            "@em1, @em2,@em3)", db.getConnection());

                        command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();
                        command.Parameters.Add("@lg", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
                        command.Parameters.Add("@ps", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                        command.Parameters.Add("@em", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();
                        command.Parameters.Add("@em1", MySqlDbType.VarChar).Value = table[4, rowIndex].Value.ToString();
                        command.Parameters.Add("@em2", MySqlDbType.VarChar).Value = table[5, rowIndex].Value.ToString();
                        command.Parameters.Add("@em3", MySqlDbType.VarChar).Value = table[6, rowIndex].Value.ToString();

                        table.Rows.RemoveAt(rowIndex);

                        table.Rows[e.RowIndex].Cells[8].Value = "Delete";

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

        private void restorantss_Load(object sender, EventArgs e)
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

                    table[8, lastRow] = linkCell;

                    row.Cells["Delete"].Value = "Insert";
                    table[8, lastRow].Style.BackColor = Color.Tomato;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    table[7, i] = linkCell;
                    table[7, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[8, i] = linkCell;
                    table[8, i].Style.BackColor = Color.Tomato;
                }
            }

            if (txtSearch.Text == "")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[7, i] = linkCell;
                    table[7, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[8, i] = linkCell;
                    table[8, i].Style.BackColor = Color.Tomato;
                }
            }
           }
    }
}
