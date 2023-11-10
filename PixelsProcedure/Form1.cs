using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PixelsProcedure
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap gray;
        int[] pixels = new int[256];

        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Graph(int[] pixels)
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                Console.WriteLine(i + ": " + pixels[i]);
                chart1.Series[0].Points.AddXY(i, pixels[i]);
            }
        }

        private int[] quantization(int value)
        {
            int average = value / 2;
            int k = value;
            int[] pixels2 = new int[256];

            for (int i = 0; i < pixels2.Length; i++)
            {
                if (i < k)
                {
                    pixels2[i] = average;
                }
                else
                {
                    k += value;
                    average += value;
                }
            }
            return pixels2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            bmp = new Bitmap(@"D:\Images\4f6ad752090bf5ae323bab7bc37e25e9(2).bmp");
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int Q1 = (int)numericUpDown1.Value;
            int Q2 = (int)numericUpDown2.Value;

            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);

                    if (g < Q1) g = 0;
                    else if (g > Q2) g = 255;
                    else g = (byte)((float)(g - Q1) / (Q2 - Q1) * 255);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int Q1 = (int)numericUpDown3.Value;
            int Q2 = (int)numericUpDown4.Value;

            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    g = (byte)((float)g / 255 * (Q2 - Q1) + Q1);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            double pow = (double)numericUpDown8.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    g = (byte)(255 * Math.Pow((float)g / 255, pow));

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int b = (int)numericUpDown5.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    if (b < 0 && g + b < 0) g = 0;
                    else if (b > 0 && g + b > 255) g = 255;
                    else g = (byte)((int)g + b);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int p = (int)numericUpDown6.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    if (g >= p) g = (byte)(255 - (int)g);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int p = (int)numericUpDown7.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    if (g <= p) g = 0;
                    else g = 255;

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int value = 256;

            if (radioButton1.Checked)
            {
                value = 2;
            }
            else if (radioButton2.Checked)
            {
                value = 4;
            }
            else if (radioButton3.Checked)
            {
                value = 8;
            }
            else if (radioButton4.Checked)
            {
                value = 16;
            }
            else if (radioButton5.Checked)
            {
                value = 32;
            }
            else if (radioButton6.Checked)
            {
                value = 64;
            }
            else if (radioButton7.Checked)
            {
                value = 128;
            }

            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);

                    int[] pixels2 = quantization(256/value);
                    for (int k = 0; k < pixels2.Length; k++)
                    {
                        if (g == (byte)k)
                        {
                            g = (byte)pixels2[k];
                        }
                    }

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }
            pictureBox1.Image = gray;
            Graph(pixels);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
