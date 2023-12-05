using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DB db;

        private void button1_Click(object sender, EventArgs e)
        {
            db = new DB();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);


            string query = @"SELECT * FROM Science";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable datatable = new DataTable();

            adapter.Fill(datatable);

            dataGridView1.DataSource = datatable;

            dataGridView1.Columns["NameOfSientist"].HeaderText = "Имя учёного";
            dataGridView1.Columns["Organization"].HeaderText = "Организация";
            dataGridView1.Columns["Country"].HeaderText = "Страна";
            dataGridView1.Columns["ScienceDegree"].HeaderText = "Учёная степень";
            dataGridView1.Columns["NameOfConference"].HeaderText = "Название конференции";
            dataGridView1.Columns["Place"].HeaderText = "Место проведения";
            dataGridView1.Columns["DateOfPublication"].HeaderText = "Дата";
            dataGridView1.Columns["Type"].HeaderText = "Тип участия";
            dataGridView1.Columns["Theme"].HeaderText = "Тема доклада";
            dataGridView1.Columns["Publication"].HeaderText = "Публикация";

            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db = new DB();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);

            connection.Open();

            string query = @"SELECT NameOfSientist, COUNT(*) AS num_publications FROM Science 
                             WHERE strftime('%Y', DateOfPublication) = '2023' GROUP BY NameOfSientist;";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Name = Convert.ToString(reader["NameOfSientist"]);
                        string Num = Convert.ToString(reader["num_publications"]);

                        label1.Text += $"\n{Name}:{Num}";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db = new DB();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);

            connection.Open();

            string query = @"SELECT NameOfConference FROM Science WHERE Publication = 'Нет';";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Name = Convert.ToString(reader["NameOfConference"]);

                        label2.Text += $"\n{Name}";
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            db = new DB();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);

            connection.Open();

            string query = @"SELECT NameOfConference FROM Science WHERE ScienceDegree = 'Доктор - наук'
                             GROUP BY NameOfConference ORDER BY COUNT(*) DESC LIMIT 1;";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Name = Convert.ToString(reader["NameOfConference"]);

                        label3.Text += $"{Name}";
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db = new DB();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);

            connection.Open();

            string query = @"SELECT NameOfConference, COUNT(DISTINCT Country) AS num_countries
                             FROM Science GROUP BY NameOfConference;";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Name = Convert.ToString(reader["NameOfConference"]);
                        string Num = Convert.ToString(reader["num_countries"]);

                        label4.Text += $"\n{Name}:{Num}";
                    }
                }
            }
        }
    }
}
