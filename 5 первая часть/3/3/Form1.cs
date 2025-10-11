using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3
{
    public partial class Form1 : Form
    {
        private ListBox taskListBox;
        private TextBox taskTextBox;
        private List<TaskItem> tasks = new List<TaskItem>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настраиваем форму
            this.Text = "Менеджер задач";
            this.Size = new Size(500, 400);
            this.BackColor = Color.White;

            // Заголовок
            Label titleLabel = new Label();
            titleLabel.Text = "Список задач";
            titleLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(200, 30);
            this.Controls.Add(titleLabel);

            // Поле для ввода новой задачи
            taskTextBox = new TextBox();
            taskTextBox.Location = new Point(20, 60);
            taskTextBox.Size = new Size(300, 20);
            taskTextBox.Font = new Font("Arial", 11);
            taskTextBox.Text = "Введите новую задачу...";
            taskTextBox.ForeColor = Color.Gray;
            taskTextBox.GotFocus += TaskTextBox_GotFocus;
            taskTextBox.LostFocus += TaskTextBox_LostFocus;
            this.Controls.Add(taskTextBox);

            // Кнопка добавления задачи
            Button addButton = new Button();
            addButton.Text = "Добавить";
            addButton.Location = new Point(330, 58);
            addButton.Size = new Size(80, 25);
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            // Enter тоже добавляет задачу
            taskTextBox.KeyDown += TaskTextBox_KeyDown;

            // Список задач
            taskListBox = new ListBox();
            taskListBox.Location = new Point(20, 100);
            taskListBox.Size = new Size(390, 200);
            taskListBox.Font = new Font("Arial", 10);
            taskListBox.SelectionMode = SelectionMode.MultiExtended;
            this.Controls.Add(taskListBox);

            // Панель с кнопками управления
            Panel controlPanel = new Panel();
            controlPanel.Location = new Point(20, 310);
            controlPanel.Size = new Size(390, 40);

            // Кнопка отметки выполнения
            Button completeButton = new Button();
            completeButton.Text = "Выполнено";
            completeButton.Location = new Point(0, 0);
            completeButton.Size = new Size(80, 30);
            completeButton.Click += CompleteButton_Click;
            controlPanel.Controls.Add(completeButton);

            // Кнопка удаления
            Button deleteButton = new Button();
            deleteButton.Text = "Удалить";
            deleteButton.Location = new Point(90, 0);
            deleteButton.Size = new Size(80, 30);
            deleteButton.Click += DeleteButton_Click;
            controlPanel.Controls.Add(deleteButton);

            // Кнопка очистки всех задач
            Button clearAllButton = new Button();
            clearAllButton.Text = "Очистить все";
            clearAllButton.Location = new Point(180, 0);
            clearAllButton.Size = new Size(80, 30);
            clearAllButton.Click += ClearAllButton_Click;
            controlPanel.Controls.Add(clearAllButton);

            // Кнопка сброса отметок
            Button resetButton = new Button();
            resetButton.Text = "Сбросить";
            resetButton.Location = new Point(270, 0);
            resetButton.Size = new Size(80, 30);
            resetButton.Click += ResetButton_Click;
            controlPanel.Controls.Add(resetButton);

            this.Controls.Add(controlPanel);

            // Статистика
            Label statsLabel = new Label();
            statsLabel.Name = "statsLabel";
            statsLabel.Location = new Point(20, 360);
            statsLabel.Size = new Size(300, 20);
            statsLabel.Font = new Font("Arial", 9);
            this.Controls.Add(statsLabel);

            UpdateStatistics();
        }

        private void TaskTextBox_GotFocus(object sender, EventArgs e)
        {
            // При получении фокуса очищаем текст подсказки
            if (taskTextBox.Text == "Введите новую задачу...")
            {
                taskTextBox.Text = "";
                taskTextBox.ForeColor = Color.Black;
            }
        }

        private void TaskTextBox_LostFocus(object sender, EventArgs e)
        {
            // При потере фокуса показываем подсказку если поле пустое
            if (string.IsNullOrWhiteSpace(taskTextBox.Text))
            {
                taskTextBox.Text = "Введите новую задачу...";
                taskTextBox.ForeColor = Color.Gray;
            }
        }

        private void TaskTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddButton_Click(sender, e);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string taskText = taskTextBox.Text.Trim();

            // Игнорируем текст подсказки
            if (taskText == "Введите новую задачу...")
            {
                taskText = "";
            }

            if (!string.IsNullOrEmpty(taskText))
            {
                TaskItem newTask = new TaskItem
                {
                    Text = taskText,
                    IsCompleted = false,
                    CreatedDate = DateTime.Now
                };

                tasks.Add(newTask);
                taskListBox.Items.Add(newTask.DisplayText);
                taskTextBox.Text = "";
                taskTextBox.ForeColor = Color.Black;
                taskTextBox.Focus();

                UpdateStatistics();
            }
            else
            {
                MessageBox.Show("Введите текст задачи!", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CompleteButton_Click(object sender, EventArgs e)
        {
            if (taskListBox.SelectedIndex >= 0)
            {
                // Создаем копию выбранных индексов
                List<int> selectedIndices = new List<int>();
                foreach (int index in taskListBox.SelectedIndices)
                {
                    selectedIndices.Add(index);
                }

                foreach (int index in selectedIndices)
                {
                    if (index < tasks.Count)
                    {
                        tasks[index].IsCompleted = true;
                        taskListBox.Items[index] = tasks[index].DisplayText;
                    }
                }
                UpdateStatistics();
            }
            else
            {
                MessageBox.Show("Выберите задачу для отметки!", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (taskListBox.SelectedIndex >= 0)
            {
                // Создаем копию выбранных индексов в обратном порядке
                List<int> selectedIndices = new List<int>();
                foreach (int index in taskListBox.SelectedIndices)
                {
                    selectedIndices.Add(index);
                }
                selectedIndices.Sort((a, b) => b.CompareTo(a));

                foreach (int index in selectedIndices)
                {
                    if (index < tasks.Count)
                    {
                        tasks.RemoveAt(index);
                        taskListBox.Items.RemoveAt(index);
                    }
                }
                UpdateStatistics();
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления!", "Внимание",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            if (tasks.Count > 0)
            {
                DialogResult result = MessageBox.Show("Удалить все задачи?", "Подтверждение",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    tasks.Clear();
                    taskListBox.Items.Clear();
                    UpdateStatistics();
                }
            }
            else
            {
                MessageBox.Show("Список задач пуст!", "Информация",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (tasks.Count > 0)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasks[i].IsCompleted = false;
                    taskListBox.Items[i] = tasks[i].DisplayText;
                }
                UpdateStatistics();
            }
        }

        private void UpdateStatistics()
        {
            int totalTasks = tasks.Count;
            int completedTasks = tasks.Count(t => t.IsCompleted);
            int pendingTasks = totalTasks - completedTasks;

            // Находим Label по имени
            Control[] foundControls = this.Controls.Find("statsLabel", true);
            if (foundControls.Length > 0 && foundControls[0] is Label statsLabel)
            {
                statsLabel.Text = $"Всего задач: {totalTasks} | Выполнено: {completedTasks} | Осталось: {pendingTasks}";
            }
        }
    }

    // Класс для хранения информации о задаче
    public class TaskItem
    {
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public string DisplayText
        {
            get
            {
                string status = IsCompleted ? "✓ ВЫПОЛНЕНО" : "● В РАБОТЕ";
                string date = CreatedDate.ToString("dd.MM HH:mm");
                return $"{status} | {date} | {Text}";
            }
        }
    }
}