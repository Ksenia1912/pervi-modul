using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5
{
    public partial class Form1 : Form
    {
        private TextBox display;
        private double firstNumber = 0;
        private string operation = "";
        private bool newOperation = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настраиваем форму
            this.Text = "Калькулятор";
            this.Size = new Size(300, 400);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Создаем дисплей
            display = new TextBox();
            display.Location = new Point(10, 10);
            display.Size = new Size(260, 30);
            display.Font = new Font("Arial", 14);
            display.TextAlign = HorizontalAlignment.Right;
            display.ReadOnly = true;
            display.Text = "0";
            this.Controls.Add(display);

            // Массив кнопок калькулятора
            string[,] buttonLayout = {
                {"C", "±", "%", "÷"},
                {"7", "8", "9", "×"},
                {"4", "5", "6", "-"},
                {"1", "2", "3", "+"},
                {"0", "", ".", "="}
            };

            int startX = 10;
            int startY = 50;
            int buttonWidth = 60;
            int buttonHeight = 50;
            int spacing = 5;

            // Создаем кнопки
            for (int row = 0; row < buttonLayout.GetLength(0); row++)
            {
                for (int col = 0; col < buttonLayout.GetLength(1); col++)
                {
                    string buttonText = buttonLayout[row, col];

                    if (!string.IsNullOrEmpty(buttonText))
                    {
                        Button button = new Button();
                        button.Text = buttonText;
                        button.Size = new Size(buttonWidth, buttonHeight);
                        button.Location = new Point(
                            startX + col * (buttonWidth + spacing),
                            startY + row * (buttonHeight + spacing)
                        );
                        button.Font = new Font("Arial", 12);

                        // Назначаем обработчики событий
                        if (char.IsDigit(buttonText[0]) || buttonText == ".")
                        {
                            button.Click += NumberButton_Click;
                        }
                        else if (buttonText == "=")
                        {
                            button.Click += EqualsButton_Click;
                        }
                        else if (buttonText == "C")
                        {
                            button.Click += ClearButton_Click;
                        }
                        else if (buttonText == "±")
                        {
                            button.Click += PlusMinusButton_Click;
                        }
                        else if (buttonText == "%")
                        {
                            button.Click += PercentButton_Click;
                        }
                        else
                        {
                            button.Click += OperatorButton_Click;
                        }

                        this.Controls.Add(button);
                    }
                }
            }

            // Делаем кнопку 0 шире
            Button zeroButton = this.Controls.OfType<Button>().First(b => b.Text == "0");
            zeroButton.Size = new Size(buttonWidth * 2 + spacing, buttonHeight);
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Text;

            if (display.Text == "0" || newOperation)
            {
                display.Text = number;
                newOperation = false;
            }
            else
            {
                // Проверяем, чтобы точка была только одна
                if (number == "." && display.Text.Contains("."))
                {
                    return;
                }
                display.Text += number;
            }
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (!newOperation)
            {
                Calculate();
            }

            firstNumber = double.Parse(display.Text);
            operation = button.Text;
            newOperation = true;
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (!newOperation && !string.IsNullOrEmpty(operation))
            {
                Calculate();
                operation = "";
                newOperation = true;
            }
        }

        private void Calculate()
        {
            double secondNumber = double.Parse(display.Text);
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "×":
                    result = firstNumber * secondNumber;
                    break;
                case "÷":
                    if (secondNumber != 0)
                    {
                        result = firstNumber / secondNumber;
                    }
                    else
                    {
                        display.Text = "Ошибка";
                        return;
                    }
                    break;
            }

            // Форматируем результат
            display.Text = result.ToString().Replace(",", ".");
            if (display.Text.Contains("."))
            {
                display.Text = display.Text.TrimEnd('0').TrimEnd('.');
                if (display.Text == "") display.Text = "0";
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            display.Text = "0";
            firstNumber = 0;
            operation = "";
            newOperation = true;
        }

        private void PlusMinusButton_Click(object sender, EventArgs e)
        {
            if (display.Text != "0" && display.Text != "Ошибка")
            {
                double number = double.Parse(display.Text);
                number = -number;
                display.Text = number.ToString().Replace(",", ".");
            }
        }

        private void PercentButton_Click(object sender, EventArgs e)
        {
            if (display.Text != "0" && display.Text != "Ошибка")
            {
                double number = double.Parse(display.Text);
                number = number / 100;
                display.Text = number.ToString().Replace(",", ".");
                newOperation = true;
            }
        }

        // Обработка нажатия клавиш на клавиатуре
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    SimulateButtonClick("0");
                    return true;
                case Keys.D1:
                case Keys.NumPad1:
                    SimulateButtonClick("1");
                    return true;
                case Keys.D2:
                case Keys.NumPad2:
                    SimulateButtonClick("2");
                    return true;
                case Keys.D3:
                case Keys.NumPad3:
                    SimulateButtonClick("3");
                    return true;
                case Keys.D4:
                case Keys.NumPad4:
                    SimulateButtonClick("4");
                    return true;
                case Keys.D5:
                case Keys.NumPad5:
                    SimulateButtonClick("5");
                    return true;
                case Keys.D6:
                case Keys.NumPad6:
                    SimulateButtonClick("6");
                    return true;
                case Keys.D7:
                case Keys.NumPad7:
                    SimulateButtonClick("7");
                    return true;
                case Keys.D8:
                case Keys.NumPad8:
                    SimulateButtonClick("8");
                    return true;
                case Keys.D9:
                case Keys.NumPad9:
                    SimulateButtonClick("9");
                    return true;
                case Keys.Add:
                    SimulateButtonClick("+");
                    return true;
                case Keys.Subtract:
                    SimulateButtonClick("-");
                    return true;
                case Keys.Multiply:
                    SimulateButtonClick("×");
                    return true;
                case Keys.Divide:
                    SimulateButtonClick("÷");
                    return true;
                case Keys.Enter:
                    SimulateButtonClick("=");
                    return true;
                case Keys.Escape:
                    SimulateButtonClick("C");
                    return true;
                case Keys.Decimal:
                    SimulateButtonClick(".");
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SimulateButtonClick(string buttonText)
        {
            var button = this.Controls.OfType<Button>().FirstOrDefault(b => b.Text == buttonText);
            if (button != null)
            {
                button.PerformClick();
            }
        }
    }
}