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
    public partial class dishes : UserControl
    {
        public dishes()
        {
            InitializeComponent();
        }

        DataTable tab;

       public int GetPageCount()
        {
            int result = 0;

          DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT COUNT(*) FROM `users`", dB.getConnection());

            adapter.SelectCommand = command;

            dB.openConnection();
            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Почта был обновлена"); }

            

            double count = Convert.ToDouble(command.ExecuteScalar());

            result = Convert.ToInt32(Math.Ceiling(count/10));
            dB.closeConnection();

            return result;
        }

        private void dishes_Load(object sender, EventArgs e)
        {
           int page= GetPageCount();
          
        }
    }
}
