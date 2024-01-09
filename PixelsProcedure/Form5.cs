using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PixelsProcedure
{
    public partial class Form5 : Form
    {
        Bitmap bmp = new Bitmap(@"D:\Images\4f6ad752090bf5ae323bab7bc37e25e9(1).bmp");
        bool isGrayScale = false;

        public Form5()
        {
            InitializeComponent();
        }

        private void extension()
        {
            Bitmap newBmp = new Bitmap(bmp.Width + 2, bmp.Height + 2);

            for (int x = 0; x < newBmp.Width; x++)
            {
                for (int y = 0; y < newBmp.Height; y++)
                {
                    Color c;
                    byte g;

                    // Углы
                    if (x == 0 && y == 0)
                    {
                        c = bmp.GetPixel(0, 0);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == newBmp.Width - 1 && y == 0)
                    {
                        c = bmp.GetPixel(bmp.Width - 1, 0);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == 0 && y == newBmp.Height - 1)
                    {
                        c = bmp.GetPixel(0, bmp.Height - 1);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == newBmp.Width - 1 && y == newBmp.Height - 1)
                    {
                        c = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }

                    // Строки и колонки
                    else if (y == 0 && (x != 0 && x != newBmp.Width - 1))
                    {
                        c = bmp.GetPixel(x - 1, 0);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (y == newBmp.Height - 1 && (x != 0 && x != newBmp.Width - 1))
                    {
                        c = bmp.GetPixel(x - 1, bmp.Height - 1);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == 0 && (y != 0 && y != newBmp.Height - 1))
                    {
                        c = bmp.GetPixel(0, y - 1);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == newBmp.Width - 1 && (y != 0 && y != newBmp.Height - 1))
                    {
                        c = bmp.GetPixel(bmp.Width - 1, y - 1);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }

                    // Само изображение
                    else
                    {
                        c = bmp.GetPixel(x - 1, y - 1);
                        g = c.R;
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }

                }
            }

            bmp = newBmp;
        }

        private void grayScale()
        {
            Bitmap gray = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }

            bmp = gray;
            isGrayScale = true;
            extension();
        }

        private void conversion(double[,] h)
        {
            Bitmap newBmp = new Bitmap(bmp.Width - 2, bmp.Height - 2);

            for (int x = 1; x < bmp.Width - 1; x++)
            {
                for (int y = 1; y < bmp.Height - 1; y++)
                {
                    // 1 колонка
                    Color c_00 = bmp.GetPixel(x - 1, y - 1);
                    Color c_10 = bmp.GetPixel(x - 1, y);
                    Color c_20 = bmp.GetPixel(x - 1, y + 1);

                    // 2 колонка
                    Color c_01 = bmp.GetPixel(x, y - 1);
                    Color c_11 = bmp.GetPixel(x, y);
                    Color c_21 = bmp.GetPixel(x, y + 1);

                    // 3 колонка
                    Color c_02 = bmp.GetPixel(x + 1, y - 1);
                    Color c_12 = bmp.GetPixel(x + 1, y);
                    Color c_22 = bmp.GetPixel(x + 1, y + 1);

                    byte g_00 = c_00.R;
                    byte g_10 = c_10.R;
                    byte g_20 = c_20.R;

                    byte g_01 = c_01.R;
                    byte g_11 = c_11.R;
                    byte g_21 = c_21.R;

                    byte g_02 = c_02.R;
                    byte g_12 = c_12.R;
                    byte g_22 = c_22.R;

                    byte g = (byte)(g_00 * h[0, 0] + g_10 * h[1, 0] + g_20 * h[2, 0] + g_01 * h[0, 1] + g_11 * h[1, 1] + g_21 * h[2, 1] + g_02 * h[0, 2] + g_12 * h[1, 2] + g_22 * h[2, 2]);
                    if (g < 0) { g = 0; }
                    else if (g > 255) { g = 255; }

                    newBmp.SetPixel(x - 1, y - 1, Color.FromArgb(g, g, g));
                }
            }

            bmp = newBmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isGrayScale = false;
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
            isGrayScale = false;
            pictureBox1.Image = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!isGrayScale)
            {
                grayScale();
            }
            else
            {
                extension();
            }

            double[,] h = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            if (radioButton1.Checked)
            {
                h[0, 0] = (double)1 / 9;
                h[1, 0] = (double)1 / 9;
                h[2, 0] = (double)1 / 9;

                h[0, 1] = (double)1 / 9;
                h[1, 1] = (double)1 / 9;
                h[2, 1] = (double)1 / 9;

                h[0, 2] = (double)1 / 9;
                h[1, 2] = (double)1 / 9;
                h[2, 2] = (double)1 / 9;
            }
            else if (radioButton2.Checked)
            {
                h[0, 0] = (double)1 / 10;
                h[1, 0] = (double)1 / 10;
                h[2, 0] = (double)1 / 10;

                h[0, 1] = (double)1 / 10;
                h[1, 1] = (double)1 / 5;
                h[2, 1] = (double)1 / 10;

                h[0, 2] = (double)1 / 10;
                h[1, 2] = (double)1 / 10;
                h[2, 2] = (double)1 / 10;
            }
            else if (radioButton3.Checked)
            {
                h[0, 0] = (double)1 / 16;
                h[1, 0] = (double)1 / 8;
                h[2, 0] = (double)1 / 16;

                h[0, 1] = (double)1 / 8;
                h[1, 1] = (double)1 / 4;
                h[2, 1] = (double)1 / 8;

                h[0, 2] = (double)1 / 16;
                h[1, 2] = (double)1 / 8;
                h[2, 2] = (double)1 / 16;
            }

            conversion(h);
           
            pictureBox1.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!isGrayScale)
            {
                grayScale();
            }
            else
            {
                extension();
            }

            double[,] h = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            if (radioButton4.Checked)
            {
                h[0, 0] = -1;
                h[1, 0] = -1;
                h[2, 0] = -1;

                h[0, 1] = -1;
                h[1, 1] = 9;
                h[2, 1] = -1;

                h[0, 2] = -1;
                h[1, 2] = -1;
                h[2, 2] = -1;
            }
            else if (radioButton5.Checked)
            {
                h[0, 0] = 0;
                h[1, 0] = -1;
                h[2, 0] = 0;

                h[0, 1] = -1;
                h[1, 1] = 5;
                h[2, 1] = -1;

                h[0, 2] = 0;
                h[1, 2] = -1;
                h[2, 2] = 0;
            }
            else if (radioButton6.Checked)
            {
                h[0, 0] = 1;
                h[1, 0] = -2;
                h[2, 0] = 1;

                h[0, 1] = -2;
                h[1, 1] = 5;
                h[2, 1] = -2;

                h[0, 2] = 1;
                h[1, 2] = -2;
                h[2, 2] = 1;
            }

            conversion(h);
           
            pictureBox1.Image = bmp;
        }
    }
}
