﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace PixelsProcedure
{
    public partial class Form2 : Form
    {
        public static List<TextBox> TextBoxes = new List<TextBox>();
        public static List<NumericUpDown> nudList = new List<NumericUpDown>();
        public static List<ListBox> lbList = new List<ListBox>();

        String[] colors = { "Black", "White", "Gray", "DarkGray", "Red", "Pink", "Purple", "DeepPink", "Orange", "Yellow", "Lime", "LightGreen", "Cyan", "ElectricBlue", "Magenta", "DarkRed", "DarkBlue", "DarkCyan", "DarkMagenta", "Brown", "DarkBrown" };

        Bitmap bmp;
        Bitmap gray;
        Bitmap colorful;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(@"D:\Images\4f6ad752090bf5ae323bab7bc37e25e9(2).bmp");
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

        public void loadListOfIntervals(int p)
        {
            int k = 256 / p - 1;
            for (int i = 0; i < p; i++)
            {
                NumericUpDown nud = new NumericUpDown();
                nud.Minimum = 1;
                nud.Maximum = 255;
                nud.Size = new Size(52, 20);

                ListBox lb = new ListBox();
                lb.Items.AddRange(colors);
                lb.ScrollAlwaysVisible = true;
                lb.Size = new Size(80, 20);
                lb.SelectedItem = colors[new Random().Next(0, colors.Length)];
                //colors[new Random().Next(0, colors.Length)]

                if (k > 248 && k < 255) { nud.Value = 255; }
                else { nud.Value = k; }
                //btn.Click += Btn_Click;
                k += 256 / p;

                nudList.Add(nud);
                lbList.Add(lb);

                flowLayoutPanel1.Controls.Add(nud);
                flowLayoutPanel1.Controls.Add(lb);
            }
        }

        //private void Btn_Click(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    MessageBox.Show(btn.Text);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            int p = (int)numericUpDown1.Value;
            loadListOfIntervals(p);
            //TextBox newTextBox = new TextBox();
            //TextBox lastOldTextBox = TextBoxes.LastOrDefault();

            //if (lastOldTextBox == null)
            //{
            //    newTextBox.Location = new Point(30, 50);
            //    newTextBox.Text = "Hello World!";
            //}
            //else
            //{
            //    newTextBox.Location = new Point(lastOldTextBox.Location.X, lastOldTextBox.Location.Y + 30);
            //    newTextBox.Text = $"Hello World! ({TextBoxes.Count})";
            //}
            //TextBoxes.Add(newTextBox);
            //this.Controls.Add(newTextBox);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = gray;
            nudList.Clear();
            lbList.Clear();
            flowLayoutPanel1.Controls.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lbList.Count > 0 && nudList.Count > 0)
            {
                String[] pixelsColors = new String[256];

                int i1 = 0;
                for (int i = 0; i < lbList.Count; i++)
                {
                    int i2 = (int)nudList[i].Value;
                    for (; i1 <= i2; i1++)
                    {
                        pixelsColors[i1] = lbList[i].SelectedItem.ToString();
                        //Console.WriteLine(i + ": " + nudList[i].Value + " -- " + lbList[i].SelectedItem.ToString());
                    }
                }

                colorful = new Bitmap(bmp.Width, bmp.Height);

                for (int x = 0; x < colorful.Width; x++)
                {
                    for (int y = 0; y < colorful.Height; y++)
                    {
                        Color c = bmp.GetPixel(x, y);
                        byte g = (byte)(0.3f * c.R + 0.59f * c.G + 0.11f * c.B);

                        for (int k = 0; k < pixelsColors.Length; k++)
                        {
                            if (g == (byte)k)
                            {
                                //g = (byte)pixelsColors[k];
                                c = Color.FromName(pixelsColors[k]);
                                break;
                            }
                        }
                        colorful.SetPixel(x, y, c);
                    }
                }
                pictureBox1.Image = colorful;
                //for (int i = 0; i < pixelsColors.Length; i++)
                //{
                //    Console.WriteLine(i + ": " + pixelsColors[i]);
                //}

                //for (int i = 0; i < lbList.Count; i++)
                //{
                //    Console.WriteLine(i + ": " + nudList[i].Value + " -- " + lbList[i].SelectedItem.ToString());
                //}
                //Console.WriteLine();
            }
        }
    }
}
