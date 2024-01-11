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
    public partial class Form4 : Form
    {
        Bitmap bmp = new Bitmap(@"\Images\Untitled.bmp");
        Bitmap gray;
        Bitmap rotateBmp;
        Bitmap rotateGray;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private byte[] bmpToByte(Bitmap bmpImg)
        {
            ImageConverter converter = new ImageConverter();
            byte[] bmpArr = (byte[])converter.ConvertTo(bmpImg, typeof(byte[]));
            OtsuThreshold(bmpArr);
            return bmpArr;
        }

        private void Graph()
        {
            chart1.Series[0].Points.Clear();
            int[] h = new int[rotateGray.Height];
            for (int y = 0; y < rotateGray.Height; y++)
            {
                for (int x = 0; x < rotateGray.Width; x++)
                {
                    Color c = rotateGray.GetPixel(x, y);
                    if (c == Color.FromArgb(0, 0, 0))
                    { h[y]++; }
                }
            }

            for (int i = 0; i < h.Length; i++)
            {
                chart1.Series[0].Points.AddXY(i, h[h.Length - 1 - i]);
            }
        }

        private int turningGray(int k)
        {
            int Cx = gray.Width / 2;
            int Cy = gray.Height / 2;

            int angle = k;
            if (angle < 0) { angle = 360 + angle; }
            double angleRad = (double)(Math.PI * angle) / 180;

            rotateGray = new Bitmap(gray.Width, gray.Height);
            for (int x = 0; x < rotateGray.Width; x++)
            {
                for (int y = 0; y < rotateGray.Height; y++)
                {
                    { rotateGray.SetPixel(x, y, Color.FromArgb(255, 255, 255)); }
                }
            }

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    int X = (int)(Cx + (x - Cx) * Math.Cos(angleRad) - (y - Cy) * Math.Sin(angleRad));
                    int Y = (int)(Cy + (x - Cx) * Math.Sin(angleRad) + (y - Cy) * Math.Cos(angleRad));

                    if (X >= 0 && Y >= 0 && (X < (rotateGray.Width)) && (Y < (rotateGray.Height)))
                    { rotateGray.SetPixel(X, Y, gray.GetPixel(x, y)); }
                }
            }

            //for (int X = 0; X < rotateGray.Width; X++)
            //{
            //    for (int Y = 0; Y < rotateGray.Height; Y++)
            //    {
            //        int x = (int)((X - Cx) * Math.Cos(angleRad) + (Y - Cy) * Math.Sin(angleRad) + Cx);
            //        int y = (int)(-(X - Cx) * Math.Sin(angleRad) + (Y - Cy) * Math.Cos(angleRad) + Cy);
            //        if (x >= 0 && y >= 0 && (x < (gray.Width)) && (y < (gray.Height)))
            //        {
            //            Color c = gray.GetPixel(x, y);
            //            rotateGray.SetPixel(X, Y, c);
            //        }
            //    }
            //}

            int[] h = new int[rotateGray.Height];
            for (int y = 0; y < rotateGray.Height; y++)
            {
                for (int x = 0; x < rotateGray.Width; x++)
                {
                    Color c = rotateGray.GetPixel(x, y);
                    if (c == Color.FromArgb(0, 0, 0))
                    { h[y]++; }
                }
            }

            Console.WriteLine("angle: " + angle);
            return h.Count(x => x == 0);
        }

        private void resultImage(int angle)
        {
            int Cx = bmp.Width / 2;
            int Cy = bmp.Height / 2;

            double angleRad = (double)(Math.PI * angle) / 180;

            rotateBmp = new Bitmap(bmp.Width, bmp.Height);
            for (int x = 0; x < rotateBmp.Width; x++)
            {
                for (int y = 0; y < rotateBmp.Height; y++)
                {
                    { rotateBmp.SetPixel(x, y, Color.FromArgb(255, 255, 255)); }
                }
            }

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int X = (int)(Cx + (x - Cx) * Math.Cos(angleRad) - (y - Cy) * Math.Sin(angleRad));
                    int Y = (int)(Cy + (x - Cx) * Math.Sin(angleRad) + (y - Cy) * Math.Cos(angleRad));

                    if (X >= 0 && Y >= 0 && (X < (rotateBmp.Width)) && (Y < (rotateBmp.Height)))
                    { rotateBmp.SetPixel(X, Y, bmp.GetPixel(x, y)); }
                }
            }

            for (int X = 0; X < rotateBmp.Width; X++)
            {
                for (int Y = 0; Y < rotateBmp.Height; Y++)
                {
                    int x = (int)((X - Cx) * Math.Cos(angleRad) + (Y - Cy) * Math.Sin(angleRad) + Cx);
                    int y = (int)(-(X - Cx) * Math.Sin(angleRad) + (Y - Cy) * Math.Cos(angleRad) + Cy);
                    if (x >= 0 && y >= 0 && (x < (bmp.Width)) && (y < (bmp.Height)))
                    {
                        Color c = bmp.GetPixel(x, y);
                        rotateBmp.SetPixel(X, Y, c);
                    }
                }
            }
            pictureBox1.Image = rotateBmp;
            Graph();
        }

        private void calculatingTheAngle()
        {
            int[] H = new int[31];
            if (bmp != null)
            {

                for (int k = -15; k <= 15; k++)
                {
                    H[k + 15] = turningGray(k);
                } // Конец перебора градусов
            }
            int resultAngle = H.ToList().IndexOf(H.Max()) - 15;
            textBox2.Text = String.Format(" Угол {0}`", resultAngle);
            turningGray(resultAngle);
            resultImage(resultAngle);
        }

        private void binarization(int t)
        {
            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = gray.GetPixel(x, y);
                    byte g = c.R;
                    if (g <= t) g = 0;
                    else g = 255;

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }

            calculatingTheAngle();
        }

        private int OtsuThreshold(byte[] grayscaleImage)
        {
            int threshold = 0;
            int[] histogram = new int[256];

            // Вычисление гистограммы яркости
            for (int i = 0; i < grayscaleImage.Length; i++)
            {
                histogram[grayscaleImage[i]]++;
            }

            int totalPixels = grayscaleImage.Length;
            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += i * histogram[i];
            }

            double sumB = 0;
            int wB = 0;
            int wF = 0;
            double varMax = 0;

            for (int t = 0; t < 256; t++)
            {
                wB += histogram[t]; // Вес переднего плана
                if (wB == 0)
                    continue;

                wF = totalPixels - wB; // Вес фона
                if (wF == 0)
                    break;

                sumB += t * histogram[t];

                double mB = sumB / wB; // Среднее значение переднего плана
                double mF = (sum - sumB) / wF; // Среднее значение фона

                // Вычисление межклассовой дисперсии
                double varBetween = wB * wF * Math.Pow((mB - mF), 2);

                // Обновление порога, если найдена более высокая дисперсия
                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = t;
                }
            }

            binarization(threshold);
            textBox1.Text = String.Format(" Порог {0}", threshold);
            return threshold;
        }

        private void grayScale()
        {
            gray = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }

            bmpToByte(gray);
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
            grayScale();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
