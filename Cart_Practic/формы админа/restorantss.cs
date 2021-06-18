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
                "'Update','Delete' FROM `restaurants`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
            //label2.Text = tab.Rows[0]["name"].ToString();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[6, i] = linkCell;

                table[6, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[7, i] = linkCell;
                table[7, i].Style.BackColor = Color.Tomato;
            }


        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}
