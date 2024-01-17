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
    public partial class Form6 : Form
    {
        Bitmap bmp;
        //Bitmap gray;
        Bitmap bmpRes;

        public Form6()
        {
            InitializeComponent();
        }

        private Point[] maximumPoints(int[,] H, int numberOfStraight)
        {
            Point[] points = new Point[numberOfStraight];

            int[,] copyH = H.Clone() as int[,];
            int rows = H.GetLength(0);
            int cols = H.GetLength(1);

            for (int i = 0; i < numberOfStraight; i++)
            {
                int maxX = 0;
                int maxY = 0;

                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < cols; y++)
                    {
                        if (copyH[x, y] > copyH[maxX, maxY])
                        {
                            maxX = x;
                            maxY = y;
                        }
                    }
                }

                points[i] = new Point(maxX, maxY);
                copyH[maxX, maxY] = 0;
            }

            return points;
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (bmp != null) { pictureBox1.Image = bmp; }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                bmpRes = new Bitmap(bmp.Width, bmp.Height);

                int maxR = (int)Math.Sqrt(Math.Pow(bmp.Width, 2) + Math.Pow(bmp.Height, 2));
                int[,] H = new int[maxR, 360];

                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color g = bmp.GetPixel(x, y);
                        bmpRes.SetPixel(x, y, g);

                        if (g.R == 0 && g.G == 0 && g.B == 0)
                        {
                            for (int fi = 0; fi < 359; fi++)
                            {
                                double fiRad = (double)(Math.PI * fi) / 180;
                                int r = (int)(x * Math.Cos(fiRad) + y * Math.Sin(fiRad));

                                if (r >= 0 && r < maxR)
                                {
                                    H[r, fi]++;
                                }
                            }
                        }

                    }
                }

                int numberOfStraight = (int)numericUpDown1.Value;
                Point[] maxVal = maximumPoints(H, numberOfStraight);

                for (int i = 0; i < numberOfStraight; i++)
                {
                    int maxX = maxVal[i].X;
                    int maxY = maxVal[i].Y;

                    double angleRad = (double)(Math.PI * maxY) / 180;
                    double a = Math.Cos(angleRad);
                    double b = Math.Sin(angleRad);

                    for (int x = 0; x < bmp.Width; x++)
                    {
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            int res = (int)Math.Ceiling(a * x + b * y);
                            if (res == maxX) { bmpRes.SetPixel(x, y, Color.Red); }
                        }
                    }

                }

                pictureBox2.Image = bmpRes;
            }
        }
    }
}
