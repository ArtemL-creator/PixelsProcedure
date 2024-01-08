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
    public partial class Form5 : Form
    {
        Bitmap bmp = new Bitmap(@"D:\Images\4f6ad752090bf5ae323bab7bc37e25e9(1).bmp");

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
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
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == newBmp.Width - 1 && y == 0)
                    {
                        c = bmp.GetPixel(bmp.Width - 1, 0);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == 0 && y == newBmp.Height - 1)
                    {
                        c = bmp.GetPixel(0, bmp.Height - 1);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == newBmp.Width - 1 && y == newBmp.Height - 1)
                    {
                        c = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }

                    // Строки и колонки
                    else if (y == 0 && (x != 0 && x != newBmp.Width - 1))
                    {
                        c = bmp.GetPixel(x - 1, 0);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (y == newBmp.Height - 1 && (x != 0 && x != newBmp.Width - 1))
                    {
                        c = bmp.GetPixel(x - 1, bmp.Height - 1);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == 0 && (y != 0 && y != newBmp.Height - 1))
                    {
                        c = bmp.GetPixel(0, y - 1);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }
                    else if (x == newBmp.Width - 1 && (y != 0 && y != newBmp.Height - 1))
                    {
                        c = bmp.GetPixel(bmp.Width - 1, y - 1);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }

                    // Само изображение
                    else
                    {
                        c = bmp.GetPixel(x - 1, y - 1);
                        g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                        newBmp.SetPixel(x, y, Color.FromArgb(g, g, g));
                    }

                }
            }


            pictureBox1.Image = newBmp;
        }
    }
}
