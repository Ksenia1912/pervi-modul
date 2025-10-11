using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        private string currentTool = "Line"; // Текущий инструмент
        private Point startPoint; // Начальная точка рисования
        private Point endPoint; // Конечная точка рисования
        private bool isDrawing = false; // Флаг рисования
        private Color currentColor = Color.Black; // Текущий цвет
        private List<DrawShape> shapes = new List<DrawShape>(); // Список всех фигур
        private List<Point> freehandPoints = new List<Point>(); // Точки для свободного рисования
        private bool isFreehandDrawing = false; // Флаг свободного рисования

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настраиваем форму
            this.Text = "Простой Paint";
            this.Size = new Size(800, 600);
            this.BackColor = Color.White;

            // Создаем панель инструментов
            Panel toolPanel = new Panel();
            toolPanel.Dock = DockStyle.Top;
            toolPanel.Height = 50;
            toolPanel.BackColor = Color.LightGray;

            // Кнопка для свободного рисования
            Button freehandButton = new Button();
            freehandButton.Text = "Карандаш";
            freehandButton.Location = new Point(10, 10);
            freehandButton.Size = new Size(80, 30);
            freehandButton.Click += (s, ev) => { currentTool = "Freehand"; };

            // Кнопка для рисования линий
            Button lineButton = new Button();
            lineButton.Text = "Линия";
            lineButton.Location = new Point(100, 10);
            lineButton.Size = new Size(80, 30);
            lineButton.Click += (s, ev) => { currentTool = "Line"; };

            // Кнопка для рисования кругов
            Button circleButton = new Button();
            circleButton.Text = "Круг";
            circleButton.Location = new Point(190, 10);
            circleButton.Size = new Size(80, 30);
            circleButton.Click += (s, ev) => { currentTool = "Circle"; };

            // Кнопка для рисования прямоугольников
            Button rectButton = new Button();
            rectButton.Text = "Прямоугольник";
            rectButton.Location = new Point(280, 10);
            rectButton.Size = new Size(100, 30);
            rectButton.Click += (s, ev) => { currentTool = "Rectangle"; };

            // Кнопка очистки
            Button clearButton = new Button();
            clearButton.Text = "Очистить";
            clearButton.Location = new Point(390, 10);
            clearButton.Size = new Size(80, 30);
            clearButton.Click += (s, ev) => { shapes.Clear(); freehandPoints.Clear(); this.Invalidate(); };

            // Кнопка выбора цвета
            Button colorButton = new Button();
            colorButton.Text = "Цвет";
            colorButton.Location = new Point(480, 10);
            colorButton.Size = new Size(80, 30);
            colorButton.Click += ColorButton_Click;

            // Добавляем кнопки на панель инструментов
            toolPanel.Controls.Add(freehandButton);
            toolPanel.Controls.Add(lineButton);
            toolPanel.Controls.Add(circleButton);
            toolPanel.Controls.Add(rectButton);
            toolPanel.Controls.Add(clearButton);
            toolPanel.Controls.Add(colorButton);

            // Добавляем панель инструментов на форму
            this.Controls.Add(toolPanel);

            // Настраиваем события мыши
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            this.Paint += Form1_Paint;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location;
            endPoint = e.Location;

            if (currentTool == "Freehand")
            {
                isFreehandDrawing = true;
                freehandPoints.Clear();
                freehandPoints.Add(e.Location);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                if (currentTool == "Freehand" && isFreehandDrawing)
                {
                    // Добавляем точки для свободного рисования
                    freehandPoints.Add(e.Location);
                }
                else
                {
                    // Для фигур - просто обновляем конечную точку
                    endPoint = e.Location;
                }
                this.Invalidate(); // Перерисовываем форму
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                endPoint = e.Location;

                if (currentTool == "Freehand")
                {
                    isFreehandDrawing = false;
                    // Сохраняем свободный рисунок как отдельную фигуру
                    if (freehandPoints.Count > 1)
                    {
                        DrawShape newShape = new DrawShape
                        {
                            Type = "Freehand",
                            StartPoint = startPoint,
                            EndPoint = endPoint,
                            Color = currentColor,
                            Points = new List<Point>(freehandPoints) // Сохраняем все точки
                        };
                        shapes.Add(newShape);
                    }
                    freehandPoints.Clear();
                }
                else
                {
                    // Создаем новую фигуру и добавляем в список
                    DrawShape newShape = new DrawShape
                    {
                        Type = currentTool,
                        StartPoint = startPoint,
                        EndPoint = endPoint,
                        Color = currentColor
                    };
                    shapes.Add(newShape);
                }

                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Рисуем все сохраненные фигуры
            foreach (DrawShape shape in shapes)
            {
                DrawShapeOnCanvas(g, shape);
            }

            // Рисуем текущую фигуру (предпросмотр)
            if (isDrawing)
            {
                if (currentTool == "Freehand" && freehandPoints.Count > 1)
                {
                    // Рисуем текущий свободный рисунок
                    using (Pen pen = new Pen(Color.FromArgb(128, currentColor), 2))
                    {
                        for (int i = 1; i < freehandPoints.Count; i++)
                        {
                            g.DrawLine(pen, freehandPoints[i - 1], freehandPoints[i]);
                        }
                    }
                }
                else
                {
                    DrawShape currentShape = new DrawShape
                    {
                        Type = currentTool,
                        StartPoint = startPoint,
                        EndPoint = endPoint,
                        Color = Color.FromArgb(128, currentColor) // Полупрозрачный цвет для предпросмотра
                    };
                    DrawShapeOnCanvas(g, currentShape);
                }
            }
        }

        private void DrawShapeOnCanvas(Graphics g, DrawShape shape)
        {
            using (Pen pen = new Pen(shape.Color, 2))
            {
                if (shape.Type == "Freehand" && shape.Points != null && shape.Points.Count > 1)
                {
                    // Рисуем свободную линию по точкам
                    for (int i = 1; i < shape.Points.Count; i++)
                    {
                        g.DrawLine(pen, shape.Points[i - 1], shape.Points[i]);
                    }
                }
                else
                {
                    int x = Math.Min(shape.StartPoint.X, shape.EndPoint.X);
                    int y = Math.Min(shape.StartPoint.Y, shape.EndPoint.Y);
                    int width = Math.Abs(shape.EndPoint.X - shape.StartPoint.X);
                    int height = Math.Abs(shape.EndPoint.Y - shape.StartPoint.Y);

                    switch (shape.Type)
                    {
                        case "Line":
                            g.DrawLine(pen, shape.StartPoint, shape.EndPoint);
                            break;
                        case "Circle":
                            g.DrawEllipse(pen, x, y, width, height);
                            break;
                        case "Rectangle":
                            g.DrawRectangle(pen, x, y, width, height);
                            break;
                    }
                }
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = currentColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
        }
    }

    // Класс для хранения информации о фигуре
    public class DrawShape
    {
        public string Type { get; set; } // Line, Circle, Rectangle, Freehand
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color Color { get; set; }
        public List<Point> Points { get; set; } // Для свободного рисования
    }
}