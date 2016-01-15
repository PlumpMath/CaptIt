using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace CaptIt
{
    public partial class MainForm : Form
    {
        private const string NAME = "캡칫 v0.0.4";
        private const int VERSION = 3;

        CaptureManager captureManager = new CaptureManager();
        public static Settings Setting = new Settings();

        class api
        {
            [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect, // x-coordinate of upper-left corner
           int nTopRect, // y-coordinate of upper-left corner
           int nRightRect, // x-coordinate of lower-right corner
           int nBottomRect, // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
        );
        }

        public MainForm()
        {
            InitializeComponent();
            this.Text = NAME;
            this.notifyIcon1.Text = NAME;

            //this.Region = Region.FromHrgn(api.CreateRoundRectRgn(0, 0, Width, Height, 50, 30));

            this.button1.Image = this.imageList1.Images[0];
        }

        ~MainForm()
        {
            this.notifyIcon1.Visible = false;
            this.notifyIcon1.Icon.Dispose();
            this.notifyIcon1.Dispose();
        }

        private void 설정SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm f = new SettingForm();
            f.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void 종료EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private Point mousePoint;
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button1, new Point(button1.Size.Width, button1.Size.Height));
        }

        private void 전체화면캡쳐FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotKeyManager.Hotkeys[0].CaptureSShot();
        }

        private void 영역지정캡쳐DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotKeyManager.Hotkeys[1].CaptureSShot();
        }
    }
    
}
