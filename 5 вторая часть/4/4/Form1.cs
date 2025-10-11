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

namespace _4
{
    public partial class Form1 : Form
    {
        private PictureBox pictureBox;
        private float scale = 1.0f;
        private string currentImagePath = "";
        private Image originalImage;
        private ToolStripLabel scaleLabel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настраиваем форму
            this.Text = "Просмотр изображений";
            this.Size = new Size(800, 600);
            this.BackColor = Color.White;

            // Создаем меню
            MenuStrip menuStrip = new MenuStrip();

            // Меню "Файл"
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");

            ToolStripMenuItem openItem = new ToolStripMenuItem("Открыть");
            openItem.Click += OpenItem_Click;

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Выход");
            exitItem.Click += ExitItem_Click;

            fileMenu.DropDownItems.Add(openItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitItem);

            // Меню "Вид"
            ToolStripMenuItem viewMenu = new ToolStripMenuItem("Вид");

            ToolStripMenuItem zoomInItem = new ToolStripMenuItem("Увеличить");
            zoomInItem.Click += ZoomInItem_Click;

            ToolStripMenuItem zoomOutItem = new ToolStripMenuItem("Уменьшить");
            zoomOutItem.Click += ZoomOutItem_Click;

            ToolStripMenuItem fitToScreenItem = new ToolStripMenuItem("По размеру экрана");
            fitToScreenItem.Click += FitToScreenItem_Click;

            ToolStripMenuItem originalSizeItem = new ToolStripMenuItem("Оригинальный размер");
            originalSizeItem.Click += OriginalSizeItem_Click;

            viewMenu.DropDownItems.Add(zoomInItem);
            viewMenu.DropDownItems.Add(zoomOutItem);
            viewMenu.DropDownItems.Add(new ToolStripSeparator());
            viewMenu.DropDownItems.Add(fitToScreenItem);
            viewMenu.DropDownItems.Add(originalSizeItem);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(viewMenu);

            // Создаем панель инструментов
            ToolStrip toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;

            // Кнопка открытия
            ToolStripButton openButton = new ToolStripButton();
            openButton.Text = "Открыть";
            openButton.Click += OpenItem_Click;

            // Кнопка увеличения
            ToolStripButton zoomInButton = new ToolStripButton();
            zoomInButton.Text = "+";
            zoomInButton.Click += ZoomInItem_Click;

            // Кнопка уменьшения
            ToolStripButton zoomOutButton = new ToolStripButton();
            zoomOutButton.Text = "-";
            zoomOutButton.Click += ZoomOutItem_Click;

            // Кнопка по размеру экрана
            ToolStripButton fitButton = new ToolStripButton();
            fitButton.Text = "По размеру";
            fitButton.Click += FitToScreenItem_Click;

            // Кнопка оригинального размера
            ToolStripButton originalButton = new ToolStripButton();
            originalButton.Text = "100%";
            originalButton.Click += OriginalSizeItem_Click;

            // Метка масштаба
            scaleLabel = new ToolStripLabel();
            scaleLabel.Text = "100%";

            toolStrip.Items.Add(openButton);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(zoomInButton);
            toolStrip.Items.Add(zoomOutButton);
            toolStrip.Items.Add(fitButton);
            toolStrip.Items.Add(originalButton);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(scaleLabel);

            // Создаем PictureBox для отображения изображения
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.BackColor = Color.LightGray;

            // Добавляем элементы на форму
            this.Controls.Add(pictureBox);
            this.Controls.Add(toolStrip);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // Настраиваем события колесика мыши для масштабирования
            pictureBox.MouseWheel += PictureBox_MouseWheel;
        }

        private void OpenItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы|*.*";
            dialog.Title = "Выберите изображение";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Освобождаем предыдущее изображение
                    if (originalImage != null)
                    {
                        originalImage.Dispose();
                        originalImage = null;
                    }

                    currentImagePath = dialog.FileName;
                    originalImage = Image.FromFile(currentImagePath);
                    pictureBox.Image = originalImage;
                    scale = 1.0f;
                    UpdateScaleLabel();
                    this.Text = "Просмотр изображений - " + Path.GetFileName(currentImagePath);

                    // Устанавливаем режим масштабирования
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message, "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ZoomInItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                scale += 0.1f;
                ApplyScale();
            }
        }

        private void ZoomOutItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null && scale > 0.1f)
            {
                scale -= 0.1f;
                ApplyScale();
            }
        }

        private void FitToScreenItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                scale = 1.0f;
                UpdateScaleLabel();
            }
        }

        private void OriginalSizeItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                scale = 1.0f;
                UpdateScaleLabel();
            }
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pictureBox.Image != null)
            {
                if (e.Delta > 0)
                {
                    // Колесико вверх - увеличиваем
                    scale += 0.1f;
                }
                else
                {
                    // Колесико вниз - уменьшаем
                    if (scale > 0.1f)
                    {
                        scale -= 0.1f;
                    }
                }
                ApplyScale();
            }
        }

        private void ApplyScale()
        {
            if (pictureBox.Image != null && originalImage != null)
            {
                // Создаем новое изображение с нужным размером
                int newWidth = (int)(originalImage.Width * scale);
                int newHeight = (int)(originalImage.Height * scale);

                // Создаем временное изображение
                Bitmap tempImage = new Bitmap(originalImage, newWidth, newHeight);

                // Устанавливаем новое изображение
                pictureBox.Image = tempImage;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                UpdateScaleLabel();
            }
        }

        private void UpdateScaleLabel()
        {
            scaleLabel.Text = $"{scale * 100:0}%";
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Освобождаем ресурсы при закрытии формы
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (originalImage != null)
            {
                originalImage.Dispose();
            }
            if (pictureBox.Image != null && pictureBox.Image != originalImage)
            {
                pictureBox.Image.Dispose();
            }
        }
    }
}