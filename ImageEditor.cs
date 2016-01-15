using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptIt
{
    public partial class ImageEditor : Form
    {
        public ImageEditor(Image img)
        {
            InitializeComponent();
            this.pictureBox1.Image = (Image)img.Clone();
            this.Size = img.Size;
        }

        private void ImageEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.pictureBox1.Image.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this.Dispose();
        }
    }
}
