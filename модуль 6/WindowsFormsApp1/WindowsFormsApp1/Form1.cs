using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\TasksDatabase.accdb";
        private OleDbConnection connection;
        private int selectedTaskId = -1;

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadTasks();
            SetupPriorityComboBox();
        }

        private void InitializeDatabase()
        {
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
        }

        private void SetupPriorityComboBox()
        {
            comboBox1.Items.AddRange(new object[] { "Высокий", "Средний", "Низкий" });
            comboBox1.SelectedIndex = 1; // Средний по умолчанию
        }

        private void LoadTasks()
        {
            try
            {
                string query = "SELECT * FROM Tasks ORDER BY DueDate";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                // Настройка внешнего вида DataGridView
                if (dataGridView1.Columns.Count > 0)
                {
                    // Скрытие колонки "Код"
                    if (dataGridView1.Columns.Contains("Код"))
                    {
                        dataGridView1.Columns["Код"].Visible = false;
                    }

                    if (dataGridView1.Columns.Contains("Title"))
                    {
                        dataGridView1.Columns["Title"].HeaderText = "Название";
                        dataGridView1.Columns["Title"].Width = 150;
                    }

                    if (dataGridView1.Columns.Contains("Description"))
                    {
                        dataGridView1.Columns["Description"].HeaderText = "Описание";
                        dataGridView1.Columns["Description"].Width = 200;
                    }

                    if (dataGridView1.Columns.Contains("DueDate"))
                    {
                        dataGridView1.Columns["DueDate"].HeaderText = "Срок выполнения";
                        dataGridView1.Columns["DueDate"].Width = 120;
                    }

                    if (dataGridView1.Columns.Contains("Priority"))
                    {
                        dataGridView1.Columns["Priority"].HeaderText = "Приоритет";
                        dataGridView1.Columns["Priority"].Width = 80;
                    }

                    if (dataGridView1.Columns.Contains("IsCompleted"))
                    {
                        dataGridView1.Columns["IsCompleted"].HeaderText = "Выполнена";
                        dataGridView1.Columns["IsCompleted"].Width = 80;
                    }

                    if (dataGridView1.Columns.Contains("CreatedDate"))
                    {
                        dataGridView1.Columns["CreatedDate"].HeaderText = "Дата создания";
                        dataGridView1.Columns["CreatedDate"].Width = 120;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки задач: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Добавить
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            try
            {
                string query = @"INSERT INTO Tasks (Title, Description, DueDate, Priority, IsCompleted, CreatedDate) 
                             VALUES (@Title, @Description, @DueDate, @Priority, @IsCompleted, @CreatedDate)";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    // Явно указываем типы данных для параметров
                    command.Parameters.Add("@Title", OleDbType.VarWChar).Value = textBox1.Text.Trim();
                    command.Parameters.Add("@Description", OleDbType.VarWChar).Value = textBox2.Text ?? "";
                    command.Parameters.Add("@DueDate", OleDbType.Date).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@Priority", OleDbType.VarWChar).Value = comboBox1.SelectedItem?.ToString() ?? "Средний";
                    command.Parameters.Add("@IsCompleted", OleDbType.Boolean).Value = checkBox1.Checked;
                    command.Parameters.Add("@CreatedDate", OleDbType.Date).Value = DateTime.Now;

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Задача добавлена успешно!");
                        ClearForm();
                        LoadTasks();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления задачи: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Обновить
        {
            if (selectedTaskId == -1)
            {
                MessageBox.Show("Выберите задачу для обновления!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            try
            {
                string query = @"UPDATE Tasks SET Title = @Title, Description = @Description, DueDate = @DueDate, 
                             Priority = @Priority, IsCompleted = @IsCompleted WHERE Код = @Код";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.Add("@Title", OleDbType.VarWChar).Value = textBox1.Text.Trim();
                    command.Parameters.Add("@Description", OleDbType.VarWChar).Value = textBox2.Text ?? "";
                    command.Parameters.Add("@DueDate", OleDbType.Date).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@Priority", OleDbType.VarWChar).Value = comboBox1.SelectedItem?.ToString() ?? "Средний";
                    command.Parameters.Add("@IsCompleted", OleDbType.Boolean).Value = checkBox1.Checked;
                    command.Parameters.Add("@Код", OleDbType.Integer).Value = selectedTaskId;

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Задача обновлена успешно!");
                        ClearForm();
                        LoadTasks();
                    }
                    else
                    {
                        MessageBox.Show("Задача не найдена или не была обновлена.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления задачи: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Удалить
        {
            if (selectedTaskId == -1)
            {
                MessageBox.Show("Выберите задачу для удаления!");
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить эту задачу?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM Tasks WHERE Код = @Код";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.Add("@Код", OleDbType.Integer).Value = selectedTaskId;
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Задача удалена успешно!");
                            ClearForm();
                            LoadTasks();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления задачи: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) // Обновить список
        {
            LoadTasks();
            ClearForm();
        }

        private void button5_Click(object sender, EventArgs e) // Очистить
        {
            ClearForm();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Проверка, что строка не пустая и не является новой строкой
                if (selectedRow.Cells["Код"].Value != null &&
                    selectedRow.Cells["Код"].Value != DBNull.Value)
                {
                    selectedTaskId = Convert.ToInt32(selectedRow.Cells["Код"].Value);

                    // Заполнение формы данными выбранной задачи
                    textBox1.Text = selectedRow.Cells["Title"].Value?.ToString() ?? "";
                    textBox2.Text = selectedRow.Cells["Description"].Value?.ToString() ?? "";

                    if (selectedRow.Cells["DueDate"].Value != null &&
                        selectedRow.Cells["DueDate"].Value != DBNull.Value)
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells["DueDate"].Value);
                    }

                    string priority = selectedRow.Cells["Priority"].Value?.ToString() ?? "Средний";
                    comboBox1.SelectedItem = priority;

                    if (selectedRow.Cells["IsCompleted"].Value != null &&
                        selectedRow.Cells["IsCompleted"].Value != DBNull.Value)
                    {
                        checkBox1.Checked = Convert.ToBoolean(selectedRow.Cells["IsCompleted"].Value);
                    }
                }
            }
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
            comboBox1.SelectedIndex = 1; // Средний приоритет
            checkBox1.Checked = false;
            selectedTaskId = -1;

            // Снятие выделения с DataGridView
            dataGridView1.ClearSelection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(1);
            dataGridView1.ClearSelection();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection?.Close();
            connection?.Dispose();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (selectedTaskId == -1)
                    button1.PerformClick(); // Добавить
                else
                    button2.PerformClick(); // Обновить

                e.Handled = true;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            // Проверка существования колонки IsCompleted 
            if (dataGridView1.Columns.Contains("IsCompleted") &&
                row.Cells["IsCompleted"].Value != null &&
                row.Cells["IsCompleted"].Value != DBNull.Value)
            {
                try
                {
                    bool isCompleted = Convert.ToBoolean(row.Cells["IsCompleted"].Value);
                    if (isCompleted)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                catch
                {
                    // Ошибки преобразования игнорируются
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}