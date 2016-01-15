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
    public partial class SShotCaptured : Form
    {
        bool isautosave = false;
        public SShotCaptured(Image img, bool isShowPath, string path)
        {
            InitializeComponent();
            this.isautosave = isShowPath;
            this.pictureBox1.Image = (Image)img.Clone();
            if (isShowPath) this.textBox1.Text = path;
            else { this.label1.Text = "스크린샷이 캡춰되었습니다"; this.Size = new Size(385, this.Size.Height); }
        }

        ~SShotCaptured()
        {
            this.pictureBox1.Image.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void SShotCaptured_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Right - this.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
        }

        private void SShotCaptured_Shown(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.Location.Y >= Screen.PrimaryScreen.Bounds.Size.Height - this.Size.Height - 30)
            {
                this.Location = new Point(Location.X, Location.Y - 3);
            }
        }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (isautosave)
                System.Diagnostics.Process.Start(textBox1.Text);
            else
            {
                ImageEditor img = new ImageEditor(this.pictureBox1.Image.Clone() as Image);
                img.Show();
            }
        }
    }
}
