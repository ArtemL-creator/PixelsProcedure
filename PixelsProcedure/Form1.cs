using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PixelsProcedure
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap gray;

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

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(@"D:\Images\4f6ad752090bf5ae323bab7bc37e25e9(2).bmp");
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
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
            pictureBox1.Image = gray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Q1 = (int)numericUpDown1.Value;
            int Q2 = (int)numericUpDown2.Value;

            gray = new Bitmap(bmp.Width, bmp.Height);
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
                }
            }
            pictureBox1.Image = gray;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int Q1 = (int)numericUpDown3.Value;
            int Q2 = (int)numericUpDown4.Value;

            gray = new Bitmap(bmp.Width, bmp.Height);
            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    g = (byte)((float)g / 255 * (Q2 - Q1) + Q1);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
            pictureBox1.Image = gray;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int pow = (int)trackBar1.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);
            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    g = (byte)(255 * Math.Pow((float)g / 255, pow));
                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
            pictureBox1.Image = gray;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int b = (int)numericUpDown5.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);

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
                }
            }
            pictureBox1.Image = gray;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int p = (int)numericUpDown6.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    if (g >= p) g = (byte)(255 - (int)g);

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
            pictureBox1.Image = gray;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int p = (int)numericUpDown7.Value;
            gray = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    Color c = bmp.GetPixel(x, y);
                    byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);
                    if (g <= p) g = 0;
                    else g = 255;

                    gray.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
            pictureBox1.Image = gray;
        }
    }
}
