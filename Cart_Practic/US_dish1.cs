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
                new MySqlCommand("SELECT `dishes`.`dish_id` AS 'id', `dishes`.`name` AS 'Название'," +
                "`restaurants`.`name` AS 'Ресторан', `categories`.`name` AS 'Категория', " +
                "`dishes`.`cost` as 'Цена',`dishes`.`structure` as 'Состав', " +
                "`dishes`.`beg_time` as 'Время начала',`dishes`.`end_time` AS 'Конец подачи', " +
                "`dishes`.`img` as 'Ссылка на изображение','Update','Delete' FROM `dishes`,`categories`," +
                "`restaurants` WHERE `dishes`.`category_id`=`categories`.`category_id` " +
                "AND `dishes`.`restaurant_id`=`restaurants`.`restaurant_id`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[9, i] = linkCell;
                table[9, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[10, i] = linkCell;
                table[10, i].Style.BackColor = Color.Tomato;
            }


        }
        
        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                    string task = table.Rows[e.RowIndex].Cells[9].Value.ToString();
                    if (task == "Update")
                    {
                       
                        button1.Visible = true;
                        Class_up_dish.id = table.Rows[e.RowIndex].Cells[0].Value.ToString();
                        Class_up_dish.id_cat = table.Rows[e.RowIndex].Cells[3].Value.ToString();

                        up_dish1.load1();
                        up_dish1.Show();
                        up_dish1.BringToFront();
                        up_dish1.clearD();
                       
                        
                       
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
                if (e.ColumnIndex == 10)
                {
                    string task = table.Rows[e.RowIndex].Cells[10].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `dishes`" +
                                " WHERE `dishes`.`dish_id` = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была удалена"); }

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
            button1.Visible = false;
        }

        private void up_dish1_Load(object sender, EventArgs e)
        {

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

                    table[9, i] = linkCell;
                    table[9, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[10, i] = linkCell;
                    table[10, i].Style.BackColor = Color.Tomato;
                }
            }

            if (txtSearch.Text == "")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[9, i] = linkCell;
                    table[9, i].Style.BackColor = Color.FromArgb(46, 169, 79);
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    table[10, i] = linkCell;
                    table[10, i].Style.BackColor = Color.Tomato;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            up_dish1.Visible=false;
           
            button1.Visible = false;



        }
    }
}
