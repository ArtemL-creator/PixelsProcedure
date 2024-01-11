using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PixelsProcedure
{
    public partial class Form1 : Form
    {
        Bitmap bmp = new Bitmap(@"\Images\4f6ad752090bf5ae323bab7bc37e25e9(2).bmp");
        Bitmap gray;
        int[] pixels = new int[256];
        int[] funcPix = new int[256];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Graph(int[] pixels)
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < pixels.Length; i++)
            {
                chart1.Series[0].Points.AddXY(i, pixels[i]);
            }
        }

        private void GraphFunc(int[] funcPix)
        {
            chart2.Series[0].Points.Clear();
            for (int i = 0; i < funcPix.Length; i++)
            {
                chart2.Series[0].Points.AddXY(i, funcPix[i]);
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
            chart2.Series[0].Points.Clear();
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
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

            for (int var = 0; var < funcPix.Length; var++)
            {
                funcPix[var] = var;
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button3_Click(object sender, EventArgs e)
        {
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

            for (int var = 0; var < funcPix.Length; var++)
            {
                if (var < Q1) funcPix[var] = 0;
                else if (var > Q2) funcPix[var] = 255;
                else funcPix[var] = (int)((float)(var - Q1) / (Q2 - Q1) * 255);
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button4_Click(object sender, EventArgs e)
        {
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

            for (int var = 0; var < funcPix.Length; var++)
            {
                funcPix[var] = (int)((float)var / 255 * (Q2 - Q1) + Q1);
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button5_Click(object sender, EventArgs e)
        {
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

            for (int var = 0; var < funcPix.Length; var++)
            {
                funcPix[var] = (int)(255 * Math.Pow((float)var / 255, pow));
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button6_Click(object sender, EventArgs e)
        {
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

            for (int var = 0; var < funcPix.Length; var++)
            {
                if (b < 0 && var + b < 0) funcPix[var] = 0;
                else if (b > 0 && var + b > 255) funcPix[var] = 255;
                else funcPix[var] = (int)((int)var + b);
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button7_Click(object sender, EventArgs e)
        {
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

            for (int var = 0; var < funcPix.Length; var++)
            {
                if (var >= p) funcPix[var] = 255 - var;
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button8_Click(object sender, EventArgs e)
        {
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

            for (int var = 0; var < funcPix.Length; var++)
            {
                if (var <= p) funcPix[var] = 0;
                else funcPix[var] = 255;
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
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

                    int[] pixels2 = quantization(256 / value);
                    for (int k = 0; k < pixels2.Length; k++)
                    {
                        if (g == (byte)k)
                        {
                            g = (byte)pixels2[k];
                            break;
                        }
                    }

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }

            for (int var = 0; var < funcPix.Length; var++)
            {
                int[] pixels2 = quantization(256 / value);
                for (int k = 0; k < pixels2.Length; k++)
                {
                    if (var == k)
                    {
                        funcPix[var] = pixels2[k];
                        break;
                    }
                }
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(bmp);
            form2.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            gray = new Bitmap(bmp.Width, bmp.Height);
            pixels = new int[256];
            double a = (double)-255 / 16256;
            double b = (double)65025 / 16256;

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    g = (byte)(a * Math.Pow((int)g, 2) + b * (int)g);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                    pixels[g] += 1;
                }
            }

            for (int var = 0; var < funcPix.Length; var++)
            {
                funcPix[var] = (int)(a * Math.Pow(var, 2) + b * var);
            }

            pictureBox1.Image = gray;
            Graph(pixels);
            GraphFunc(funcPix);
        }

        private void button0_Click(object sender, EventArgs e)
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

        private void button12_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
