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
    public partial class EnlargedImage : Form
    {
        private Bitmap bmp;

        public EnlargedImage(Bitmap bmp)
        {
            InitializeComponent();
            this.bmp = bmp;
        }

        private bool okButton = false;

        public bool OKButtonClicked
        {
            get { return okButton; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            okButton = true;
            this.Close();
        }

        private void enlargedImage_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = bmp;
        }
    }
}
