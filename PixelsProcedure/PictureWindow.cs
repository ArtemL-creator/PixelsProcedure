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
    public partial class PictureWindow : Form
    {
        private Bitmap bmp;

        public PictureWindow(Bitmap bmp)
        {
            InitializeComponent();
            this.bmp = bmp;
        }

        private void enlargedImage_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
