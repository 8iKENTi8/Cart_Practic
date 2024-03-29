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
    public partial class Sign_Up : Form
    {
        public Sign_Up()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Sign_In sign = new Sign_In();
            sign.Show();
            
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked == true)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        // Проверка существует ли ползователь
        private Boolean isUserExists()
        {
            DB dB = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `users` WHERE `log` = @ul",
                dB.getConnection());

            command.Parameters.Add("@ul",
                MySqlDbType.VarChar).Value = bunifuMaterialTextbox1.Text;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть, введите другой");
                return true;
            }
            else
                return false;
        }

        // Занесение данных в бд
        private void RegUser(string log, string pass, string email)
        {
            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("INSERT INTO `users` (`id`, `log`, `pass`, `email`, `role_id`)" +
                " VALUES (NULL, @ul, @up, @em,2);",
                dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = email;

            dB.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан!");
            else
                MessageBox.Show("Аккаунт не был создан!");

            dB.closeConnection();

        }

        //Регистрация пользователя в бд
        private void button1_Click(object sender, EventArgs e)
        {
            string login = bunifuMaterialTextbox1.Text.Trim(),
                   pass = bunifuMaterialTextbox2.Text.Trim(),
                   pass_2 = bunifuMaterialTextbox3.Text.Trim(),
                   email = bunifuMaterialTextbox4.Text.Trim().ToLower();

            if (login.Length < 5 || login.Length > 20)
            {
                MessageBox.Show("Логин введен неверно!");
                return;
            }

            if (login.Contains("@") || login.Contains("."))
            {
                MessageBox.Show("Логин содержит некорректные символы");
                return;
            }

            if (pass.Length < 5 || pass.Length > 20)
            {
                MessageBox.Show("Пароль введен неверно!");
                return;
            }

            if (pass != pass_2)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Некоректный email");
                return;
            }

            if (isUserExists())
                return;

            RegUser(login, pass, email);

            this.Hide();
            Sign_In sign = new Sign_In();
            sign.Show();
        }
    }
}
