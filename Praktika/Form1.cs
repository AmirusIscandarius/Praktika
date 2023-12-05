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


            string query = @"SELECT * FROM Scientists";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable datatable = new DataTable();

            adapter.Fill(datatable);

            dataGridView1.DataSource = datatable;

            dataGridView1.Columns["NameOfScientists"].HeaderText = "Имя учёного";
            dataGridView1.Columns["Organization"].HeaderText = "Организация";
            dataGridView1.Columns["Country"].HeaderText = "Страна";
            dataGridView1.Columns["AcademDegree"].HeaderText = "Учёная степень";
            dataGridView1.Columns["ScientistId"].Visible = false;


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

            string query = @"SELECT Scientists.NameOfScientists AS NameOfScientists,
                            COUNT(Participation.PublicationStatus) as PublicationStatus
FROM Scientists
JOIN Participation ON Scientists.ScientistId = Participation.ScientistId
JOIN Conferences ON Participation.ConferenceId = Conferences.ConferenceId
WHERE Participation.PublicationStatus = 'Да'
  AND strftime('%Y', Conferences.DateOfConference) = '2022'
GROUP BY Scientists.NameOfScientists;";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                       string scientistName = dataTable.Rows[0]["NameOfScientists"].ToString();
                       string publicationCount = dataTable.Rows[0]["PublicationStatus"].ToString();
 
                       label1.Text += $"\n{scientistName}:{publicationCount}";
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

                string query = @"SELECT
    Conferences.NameOfConference AS NameOfConference
FROM
    Conferences
LEFT JOIN
    Participation ON Conferences.ConferenceId = Participation.ConferenceId
WHERE
    Participation.PublicationStatus = 'Нет';";

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

                string query = @"SELECT
    Conferences.NameOfConference AS NameOfConference,
    COUNT(Scientists.ScientistId) AS DoctorCount
FROM
    Conferences
JOIN
    Participation ON Conferences.ConferenceId = Participation.ConferenceId
JOIN
    Scientists ON Participation.ScientistId = Scientists.ScientistId
WHERE
    Scientists.AcademDegree= 'Доктор-наук'
GROUP BY
    Conferences.ConferenceId
ORDER BY
    DoctorCount DESC
LIMIT 1;;";

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

                string query = @"SELECT
    Conferences.NameOfConference AS NameOfConference,
    COUNT(DISTINCT Scientists.Country) AS CountryCount
FROM
    Conferences
JOIN
    Participation ON Conferences.ConferenceId = Participation.ConferenceId
JOIN
    Scientists ON Participation.ScientistId = Scientists.ScientistId
GROUP BY
    Conferences.ConferenceId
ORDER BY
    NameOfConference
LIMIT 1;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Name = Convert.ToString(reader["NameOfConference"]);
                            string Num = Convert.ToString(reader["CountryCount"]);

                            label4.Text += $"\n{Name}:{Num}";
                        }
                    }
                }
            }

        private void button6_Click(object sender, EventArgs e)
        {

            db = new DB();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);


            string query = @"SELECT * FROM Conferences";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable datatable = new DataTable();

            adapter.Fill(datatable);

            dataGridView2.DataSource = datatable;

            dataGridView2.Columns["NameOfConference"].HeaderText = "Название конфиренции";
            dataGridView2.Columns["Location"].HeaderText = "Место провидения";
            dataGridView2.Columns["DateOfConference"].HeaderText = "Дата";
            dataGridView2.Columns["ConferenceId"].Visible = false;


            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.ReadOnly = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            db = new DB();
            db.getConnection();

            SQLiteConnection connection = new SQLiteConnection(db.connection);


            string query = @"SELECT * FROM Participation";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

            DataTable datatable = new DataTable();

            adapter.Fill(datatable);

            dataGridView3.DataSource = datatable;

            dataGridView3.Columns["ParticipationType"].HeaderText = "Имя учёного";
            dataGridView3.Columns["PresentationTheme"].HeaderText = "Организация";
            dataGridView3.Columns["PublicationStatus"].HeaderText = "Страна";
            dataGridView3.Columns["DateOfConference"].HeaderText = "Учёная степень";
            dataGridView3.Columns["ParticipationId"].Visible = false;
            dataGridView3.Columns["ScientistId"].Visible = false;
            dataGridView3.Columns["ConferenceId"].Visible = false;


            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.RowHeadersVisible = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.MultiSelect = false;
            dataGridView3.ReadOnly = true;
        }
    }
}
