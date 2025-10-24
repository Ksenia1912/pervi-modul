using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            InitializeScrollBar();
        }

        private void InitializeScrollBar()
        {
            // Настройка вертикального скроллбара
            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = 100;
            vScrollBar1.Value = 0;
            vScrollBar1.LargeChange = 10;
            vScrollBar1.SmallChange = 1;
            vScrollBar1.Visible = false; // Изначально скрыт
            vScrollBar1.Scroll += VScrollBar1_Scroll;
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // Прокрутка RichTextBox при изменении положения скроллбара
            int scrollValue = vScrollBar1.Value;
            richTextBox1.SelectionStart = 0;
            richTextBox1.ScrollToCaret();

            // Альтернативный метод прокрутки
            int lineToScroll = (int)((double)scrollValue / vScrollBar1.Maximum * GetLineCount());
            if (lineToScroll < richTextBox1.Lines.Length)
            {
                richTextBox1.SelectionStart = richTextBox1.GetFirstCharIndexFromLine(lineToScroll);
                richTextBox1.ScrollToCaret();
            }
        }

        private int GetLineCount()
        {
            return richTextBox1.Text.Split('\n').Length;
        }

        private void UpdateScrollBar()
        {
            // Обновление скроллбар в зависимости от содержимого RichTextBox
            int lineCount = GetLineCount();
            int visibleLines = richTextBox1.Height / richTextBox1.Font.Height;

            if (lineCount > visibleLines)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = Math.Max(0, lineCount - visibleLines);
                vScrollBar1.LargeChange = Math.Max(1, visibleLines / 2);
            }
            else
            {
                vScrollBar1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        // Парсинг функции и вычисление значения
        private double EvaluateFunction(string function, double x1, double x2)
        {
            try
            {
                // Заменяем x1 и x2 на числовые значения
                string expression = function
                    .Replace("x1", x1.ToString(System.Globalization.CultureInfo.InvariantCulture))
                    .Replace("x2", x2.ToString(System.Globalization.CultureInfo.InvariantCulture))
                    .Replace(",", "."); // Для корректного парсинга

                // Используем DataTable для вычисления выражения
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
                DataRow row = table.NewRow();
                table.Rows.Add(row);

                return double.Parse(row["expression"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка вычисления функции: {ex.Message}");
            }
        }

        // Численное вычисление градиента по x1
        private double NumericGradX1(string function, double x1, double x2, double h = 0.0001)
        {
            double f1 = EvaluateFunction(function, x1 + h, x2);
            double f2 = EvaluateFunction(function, x1 - h, x2);
            return (f1 - f2) / (2 * h);
        }

        // Численное вычисление градиента по x2
        private double NumericGradX2(string function, double x1, double x2, double h = 0.0001)
        {
            double f1 = EvaluateFunction(function, x1, x2 + h);
            double f2 = EvaluateFunction(function, x1, x2 - h);
            return (f1 - f2) / (2 * h);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем функцию из первого текстбокса
                string function = textBox1.Text.Trim();
                if (string.IsNullOrEmpty(function))
                {
                    MessageBox.Show("Введите функцию!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем и парсим начальные значения из второго текстбокса
                string[] values = textBox2.Text.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length != 2)
                {
                    MessageBox.Show("Введите два значения x1 и x2 через пробел!", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double x1 = Convert.ToDouble(values[0]);
                double x2 = Convert.ToDouble(values[1]);
                double eps = Convert.ToDouble(textBox3.Text);
                double step = Convert.ToDouble(textBox4.Text);

                int k = 0;
                double prevValue, currentValue, x1_prev, x2_prev;

                // Тестовая проверка функции
                double testValue = EvaluateFunction(function, x1, x2);

                // Алгоритм градиентного спуска
                do
                {
                    x1_prev = x1;
                    x2_prev = x2;
                    prevValue = EvaluateFunction(function, x1_prev, x2_prev);

                    // Вычисляем градиенты численным методом
                    double grad1 = NumericGradX1(function, x1_prev, x2_prev);
                    double grad2 = NumericGradX2(function, x1_prev, x2_prev);

                    // Обновляем значения
                    x1 = x1_prev - step * grad1;
                    x2 = x2_prev - step * grad2;
                    currentValue = EvaluateFunction(function, x1, x2);

                    k++;

                    // Защита от бесконечного цикла
                    if (k > 10000)
                    {
                        throw new Exception("Превышено максимальное количество итераций. Возможно, функция не сходится.");
                    }
                }
                while (Math.Abs(currentValue - prevValue) > eps);

                // Выводим результаты в RichTextBox
                string result = $"Функция: {function}\r\n" +
                               $"Начальные значения: x1={values[0]}, x2={values[1]}\r\n" +
                               $"Точность: {eps}, Шаг: {step}\r\n" +
                               $"Результаты:\r\n" +
                               $"x1 = {x1:F6}\r\n" +
                               $"x2 = {x2:F6}\r\n" +
                               $"f(x1,x2) = {currentValue:F6}\r\n" +
                               $"Количество итераций: {k}\r\n" +
                               $"Время вычисления: {DateTime.Now:HH:mm:ss}\r\n" +
                               $"----------------------------------------\r\n\r\n";

                // Добавляем результат в начало RichTextBox
                richTextBox1.Text = result + richTextBox1.Text;

                // Прокручиваем к началу
                richTextBox1.SelectionStart = 0;
                richTextBox1.ScrollToCaret();

                // Обновляем скроллбар
                UpdateScrollBar();
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода! Пожалуйста, введите корректные числовые значения.",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            vScrollBar1.Visible = false;
        }

        // Обработка изменения размера RichTextBox
        private void richTextBox1_SizeChanged(object sender, EventArgs e)
        {
            UpdateScrollBar();
        }

        // Обработка изменения текста в RichTextBox
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateScrollBar();
        }

       
    }
}