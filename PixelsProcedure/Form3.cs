using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelsProcedure
{
    public partial class Form3 : Form
    {
        Bitmap bmp = new Bitmap(@"D:\Images\4f6ad752090bf5ae323bab7bc37e25e9(2).bmp");
        Bitmap enlargetBmp;
        Rectangle rectangle = new Rectangle();

        private bool isMouseDown = false;
        private Point startPoint;
        private Point endPoint;

        public Form3()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            startPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                endPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            endPoint = e.Location;

            // Сохраняем координаты прямоугольной области
            int x = Math.Min(startPoint.X, endPoint.X);
            int y = Math.Min(startPoint.Y, endPoint.Y);
            int width = Math.Abs(startPoint.X - endPoint.X);
            int height = Math.Abs(startPoint.Y - endPoint.Y);

            Rectangle selectedRectangle = new Rectangle(x, y, width, height);
            rectangle = selectedRectangle;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isMouseDown)
            {
                //Pen pen = new Pen(Color.Red, 2);
                int x = Math.Min(startPoint.X, endPoint.X);
                int y = Math.Min(startPoint.Y, endPoint.Y);
                int width = Math.Abs(startPoint.X - endPoint.X);
                int height = Math.Abs(startPoint.Y - endPoint.Y);

                //e.Graphics.DrawRectangle(pen, x, y, width, height);
                //pen.Dispose();

                // Рисуем полупрозрачный прямоугольник
                using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.Red)))
                {
                    e.Graphics.FillRectangle(brush, x, y, width, height);
                }

                // Рисуем контур прямоугольника
                using (Pen pen = new Pen(Color.Red, 1))
                {
                    e.Graphics.DrawRectangle(pen, x, y, width, height);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP)|*.BMP";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap newBmp = new Bitmap(ofd.FileName);
                    bmp = newBmp;
                }
                catch
                {
                    MessageBox.Show("Картинка не открывается", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((int)numericUpDown1.Value > 0 && rectangle.Width > 0 && rectangle.Height > 0) 
            {
                //EnlargedImage enlargedImage = new EnlargedImage();
                //enlargedImage.ShowDialog();
                //Позже
                int L = (int)numericUpDown1.Value;

                enlargetBmp = new Bitmap(rectangle.Width, rectangle.Height);

                for (int x = 0; x < rectangle.Width; x++)
                {
                    for (int y = 0; y < rectangle.Height; y++)
                    {
                        Color c = bmp.GetPixel(rectangle.X + x, rectangle.Y + y);

                        enlargetBmp.SetPixel(x, y, c);
                    }
                }

                EnlargedImage enlargedImage = new EnlargedImage(enlargetBmp);
                enlargedImage.ShowDialog();
            }
        }
    }
}
