using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _2
{
    public partial class Form1 : Form
    {
        private string currentFile = ""; // Текущий открытый файл

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настраиваем форму
            this.Text = "Простой текстовый редактор";
            this.Size = new Size(700, 500);

            // Создаем меню
            MenuStrip menuStrip = new MenuStrip();

            // Меню "Файл"
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");

            ToolStripMenuItem newItem = new ToolStripMenuItem("Новый");
            newItem.Click += NewItem_Click;

            ToolStripMenuItem openItem = new ToolStripMenuItem("Открыть");
            openItem.Click += OpenItem_Click;

            ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить");
            saveItem.Click += SaveItem_Click;

            ToolStripMenuItem saveAsItem = new ToolStripMenuItem("Сохранить как");
            saveAsItem.Click += SaveAsItem_Click;

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Выход");
            exitItem.Click += ExitItem_Click;

            fileMenu.DropDownItems.Add(newItem);
            fileMenu.DropDownItems.Add(openItem);
            fileMenu.DropDownItems.Add(saveItem);
            fileMenu.DropDownItems.Add(saveAsItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitItem);

            menuStrip.Items.Add(fileMenu);

            // Добавляем меню на форму
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // Создаем текстовое поле
            TextBox textBox = new TextBox();
            textBox.Name = "textBox1";
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Both;
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new Font("Arial", 11);
            textBox.AcceptsTab = true;

            // Добавляем текстовое поле на форму
            this.Controls.Add(textBox);

            // Устанавливаем порядок отображения
            textBox.BringToFront();
        }

        private void NewItem_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)this.Controls["textBox1"];
            textBox.Text = "";
            currentFile = "";
            this.Text = "Простой текстовый редактор - Новый файл";
        }

        private void OpenItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openDialog.Title = "Открыть текстовый файл";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TextBox textBox = (TextBox)this.Controls["textBox1"];
                    textBox.Text = File.ReadAllText(openDialog.FileName, Encoding.UTF8);
                    currentFile = openDialog.FileName;
                    this.Text = "Простой текстовый редактор - " + Path.GetFileName(currentFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message, "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                SaveAsItem_Click(sender, e);
            }
            else
            {
                SaveFile(currentFile);
            }
        }

        private void SaveAsItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveDialog.Title = "Сохранить файл";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                currentFile = saveDialog.FileName;
                SaveFile(currentFile);
            }
        }

        private void SaveFile(string filename)
        {
            try
            {
                TextBox textBox = (TextBox)this.Controls["textBox1"];
                File.WriteAllText(filename, textBox.Text, Encoding.UTF8);
                this.Text = "Простой текстовый редактор - " + Path.GetFileName(filename);
                MessageBox.Show("Файл успешно сохранен!", "Сохранение",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}