using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        DB db;

        private void button1_Click(object sender, EventArgs e)
        {
            string phone = textBox1.Text;
            string password = textBox2.Text;

            if (phone != string.Empty
               && password != string.Empty)
            {
                checkAccount(phone, password);
            }
            else
            {
                MessageBox.Show("Введите данные", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkAccount(string phone, string password)
        {
            // Подключение к БД
            db = new DB();
            db.getConnection();

            using (SQLiteConnection con = new SQLiteConnection(db.connection))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();

                // Команда для БД
                string query = @"SELECT * FROM users WHERE phone=
                        '" + phone + "' " +
                    "AND password='" + password + "'";

                int count = 0;
                cmd.CommandText = query;
                cmd.Connection = con;

                object result = cmd.ExecuteScalar();

                SQLiteDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    count++;
                }

                if (count == 1)
                {
                    MessageBox.Show("Вы вошли", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 gen = new Form1();
                    gen.Show();
                    gen.FormClosed += new FormClosedEventHandler(form_FormClosed2);
                    this.Hide();

                    void form_FormClosed2(object sender, FormClosedEventArgs e)
                    {
                        this.Close();
                    }

                }
                else
                    MessageBox.Show("Неверная почта или пароль", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
