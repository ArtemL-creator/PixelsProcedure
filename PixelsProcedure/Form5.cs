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
        Bitmap bmp = new Bitmap(@"\Images\4f6ad752090bf5ae323bab7bc37e25e9(1).bmp");
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

        private void conversion(double[,] h, bool isEmbossingOperator)
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

                    if (isEmbossingOperator)
                    {
                        g += 128;
                    }

                    if (g < 0) { g = 0; }
                    else if (g > 255) { g = 255; }

                    newBmp.SetPixel(x - 1, y - 1, Color.FromArgb(g, g, g));
                }
            }

            bmp = newBmp;
        }

        private void calculationOfKernelsResponses(List<int[,]> h)
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

                    int g_00 = c_00.R;
                    int g_10 = c_10.R;
                    int g_20 = c_20.R;

                    int g_01 = c_01.R;
                    int g_11 = c_11.R;
                    int g_21 = c_21.R;

                    int g_02 = c_02.R;
                    int g_12 = c_12.R;
                    int g_22 = c_22.R;

                    int[] kernelsResponses = new int[h.Count];
                    for (int i = 0; i < h.Count; i++)
                    {
                        int res = g_00 * h[i][0, 0] + g_10 * h[i][1, 0] + g_20 * h[i][2, 0] + g_01 * h[i][0, 1] + g_11 * h[i][1, 1] + g_21 * h[i][2, 1] + g_02 * h[i][0, 2] + g_12 * h[i][1, 2] + g_22 * h[i][2, 2];
                        kernelsResponses[i] = res;
                    }

                    int s = 0;
                    for (int i = 0; i < kernelsResponses.Length; i++)
                    {
                        s += (int)Math.Pow(kernelsResponses[i], 2);
                    }

                    byte g = (byte)(Math.Sqrt(s));

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
            if (!isGrayScale) { grayScale(); }
            else { extension(); }

            if (radioButton1.Checked)
            {
                double[,] h = {
                    { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
                    { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
                    { 1.0 / 9, 1.0 / 9, 1.0 / 9 } };

                conversion(h, false);
            }
            else if (radioButton2.Checked)
            {
                double[,] h = {
                    { 1.0 / 10, 1.0 / 10, 1.0 / 10 },
                    { 1.0 / 10, 1.0 / 5, 1.0 / 10 },
                    { 1.0 / 10, 1.0 / 10, 1.0 / 10 } };

                conversion(h, false);
            }
            else if (radioButton3.Checked)
            {
                double[,] h = {
                    { 1.0 / 16, 1.0 / 8, 1.0 / 16 },
                    { 1.0 / 8, 1.0 / 4, 1.0 / 8 },
                    { 1.0 / 16, 1.0 / 8, 1.0 / 16 } };

                conversion(h, false);
            }

            pictureBox1.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!isGrayScale) { grayScale(); }
            else { extension(); }

            if (radioButton4.Checked)
            {
                double[,] h = {
                    { -1, -1, -1 },
                    { -1, 9, -1 },
                    { -1, -1, -1 } };

                conversion(h, false);
            }
            else if (radioButton5.Checked)
            {
                double[,] h = {
                    { 0, -1, 0 },
                    { -1, 5, -1 },
                    { 0, -1, 0 } };

                conversion(h, false);
            }
            else if (radioButton6.Checked)
            {
                double[,] h = {
                    { -1, -2, 1 },
                    { -2, 5, -2 },
                    { 1, -2, 1 } };

                conversion(h, false);
            }

            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!isGrayScale) { grayScale(); }
            else { extension(); }

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

                    byte[] gArr = new byte[9] { c_00.R, c_10.R, c_20.R, c_01.R, c_11.R, c_21.R, c_02.R, c_12.R, c_22.R };
                    Array.Sort(gArr);

                    byte g = gArr[4];
                    newBmp.SetPixel(x - 1, y - 1, Color.FromArgb(g, g, g));
                }
            }

            bmp = newBmp;
            pictureBox1.Image = bmp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!isGrayScale) { grayScale(); }
            else { extension(); }

            if (radioButton7.Checked)
            {
                int[,] h1 = { 
                    { -1, 0, 1 }, 
                    { -2, 0, 2 },
                    { -1, 0, 1 } };

                int[,] h2 = { 
                    { 1, 2, 1 }, 
                    { 0, 0, 0 }, 
                    { -1, -2, -1 } };

                List<int[,]> h = new List<int[,]>
                {
                    h1,
                    h2
                };

                calculationOfKernelsResponses(h);
            }
            else if (radioButton9.Checked)
            {
                int[,] h1 = { { 5, 5, 5 }, { -3, 0, -3 }, { -3, -3, -3 } };
                int[,] h2 = { { -3, 5, 5 }, { -3, 0, 5 }, { -3, -3, -3 } };
                int[,] h3 = { { -3, -3, 5 }, { -3, 0, 5 }, { -3, -3, 5 } };
                int[,] h4 = { { -3, -3, -3 }, { -3, 0, 5 }, { -3, 5, 5 } };
                int[,] h5 = { { 5, 5, -3 }, { 5, 0, -3 }, { -3, -3, -3 } };
                int[,] h6 = { { 5, -3, -3 }, { 5, 0, -3 }, { 5, -3, -3 } };
                int[,] h7 = { { -3, -3, -3 }, { 5, 0, -3 }, { 5, 5, -3 } };
                int[,] h8 = { { -3, -3, -3 }, { -3, 0, -3 }, { 5, 5, 5 } };

                List<int[,]> h = new List<int[,]>
                {
                    h1,
                    h2,
                    h3,
                    h4,
                    h5,
                    h6,
                    h7,
                    h8
                };

                calculationOfKernelsResponses(h);
            }

            pictureBox1.Image = bmp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!isGrayScale) { grayScale(); }
            else { extension(); }

            if (radioButton8.Checked)
            {
                double[,] h = {
                    { 0, 1, 0 },
                    { -1, 0, 1 },
                    { 0, -1, 0 } };

                conversion(h, true);
            }
            else if (radioButton10.Checked)
            {
                double[,] h = {
                    { 0, -1, 0 },
                    { 1, 0, -1 },
                    { 0, 1, 0 } };

                conversion(h, true);
            }

            pictureBox1.Image = bmp;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
