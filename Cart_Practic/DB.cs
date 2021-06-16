using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart_Practic
{
    class DB
    {
        //Строка подключения к бд

        MySqlConnection connection = new MySqlConnection("server=localhost;" +
            "port=3306;username=root;password=root;database=остров");

        //Открывает соединение
        public void openConnection()
        {
            //Если соединение закрыто, то открываем
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        //Закрывает соединение
        public void closeConnection()
        {
            //Если соединение открыто, то закрывавем
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        //Получаем соединение
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
