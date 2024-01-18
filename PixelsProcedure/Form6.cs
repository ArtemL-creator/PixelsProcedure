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
    public partial class Form6 : Form
    {
        Bitmap bmp;
        //Bitmap gray;
        Bitmap bmpRes;

        struct MyPoint3D
        {
            int r;
            int a;
            int b;

            public int R
            {
                get
                {
                    return r;
                }
                set
                {
                    r = value;
                }
            }

            public int A
            {
                get
                {
                    return a;
                }
                set
                {
                    a = value;
                }
            }

            public int B
            {
                get
                {
                    return b;
                }
                set
                {
                    b = value;
                }
            }

            public MyPoint3D(int r, int a, int b)
            {
                this.r = r;
                this.a = a;
                this.b = b;
            }
        }

        public Form6()
        {
            InitializeComponent();
        }

        private MyPoint3D[] maximumPointsOfCircles(int[,,] H, int numberOfCircles)
        {
            MyPoint3D[] points3D = new MyPoint3D[numberOfCircles];

            int[,,] copyH = H.Clone() as int[,,];
            int rows = H.GetLength(0);
            int cols = H.GetLength(1);
            int depth = H.GetLength(2);

            for (int i = 0; i < numberOfCircles; i++)
            {
                int maxR = 0;
                int maxA = 0;
                int maxB = 0;

                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < cols; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            if (copyH[x, y, z] > copyH[maxR, maxA, maxB])
                            {
                                maxR = x;
                                maxA = y;
                                maxB = z;
                            }
                        }
                    }
                }

                points3D[i] = new MyPoint3D(maxR, maxA, maxB);
                copyH[maxR, maxA, maxB] = 0;
            }

            return points3D;
        }

        private Point[] maximumPointsOfStraight(int[,] H, int numberOfStraight)
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
                Point[] maxVals = maximumPointsOfStraight(H, numberOfStraight);

                for (int i = 0; i < numberOfStraight; i++)
                {
                    int maxX = maxVals[i].X;
                    int maxY = maxVals[i].Y;

                    double angleRad = (double)(Math.PI * maxY) / 180;
                    double a = Math.Cos(angleRad);
                    double b = Math.Sin(angleRad);

                    for (int x = 0; x < bmpRes.Width; x++)
                    {
                        for (int y = 0; y < bmpRes.Height; y++)
                        {
                            int res = (int)Math.Ceiling(a * x + b * y);
                            if (res == maxX) { bmpRes.SetPixel(x, y, Color.Red); }
                        }
                    }

                }

                pictureBox2.Image = bmpRes;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                bmpRes = new Bitmap(bmp.Width, bmp.Height);

                int maxR = Math.Min(bmp.Width, bmp.Height) / 2;
                int[,,] H = new int[maxR, bmp.Width, bmp.Height];

                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color g = bmp.GetPixel(x, y);
                        bmpRes.SetPixel(x, y, g);

                        if (g.R == 0 && g.G == 0 && g.B == 0)
                        {
                            for (int a = 0; a < bmp.Width; a++)
                            {
                                for (int b = 0; b < bmp.Height; b++)
                                {
                                    int r = (int)Math.Sqrt(Math.Pow(x - a, 2) + Math.Pow(y - b, 2));

                                    if ((a + r < bmp.Width && b + r < bmp.Height && a - r >= 0 && b - r >= 0) && (r > 0 && r < maxR))
                                    {
                                        H[r, a, b]++;
                                    }

                                }
                            }
                        }

                    }
                }
                int numberOfCircles = (int)numericUpDown1.Value;
                MyPoint3D[] maxVals = maximumPointsOfCircles(H, numberOfCircles);

                for (int i = 0; i < numberOfCircles; i++)
                {
                    MyPoint3D maxVal = maxVals[i];

                    for (int x = 0; x < bmpRes.Width; x++)
                    {
                        for(int y = 0; y < bmpRes.Height; y++)
                        {
                            int rad = (int)Math.Ceiling(Math.Sqrt(Math.Pow(x - maxVal.A, 2)+ Math.Pow(y - maxVal.B, 2)));

                            if (rad == maxVal.R) { bmpRes.SetPixel(x, y, Color.Red); }
                        }
                    }
                }

                pictureBox2.Image = bmpRes;
            }
        }

    }
}
