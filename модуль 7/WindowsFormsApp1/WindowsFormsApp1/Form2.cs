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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Загрузка текста из файла при загрузке формы
            LoadHelpText();
        }

        private void LoadHelpText()
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, "Справка.txt");

                if (File.Exists(filePath))
                {
                    string helpText = File.ReadAllText(filePath, Encoding.UTF8);
                    richTextBox1.Text = helpText;
                }
                else
                {
                    richTextBox1.Text = "Файл справки не найден: " + filePath;
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Ошибка при загрузке файла справки: " + ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

    }
}