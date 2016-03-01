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
    public delegate void d(Rectangle rect, Image img);
    public delegate void d2();
    public partial class dragForm : Form
    {
        public event d dragFinished;
        public event d2 dragCanceled;

        Point loc, siz;
        bool keystate = false;
        Graphics g;
        Pen p = new Pen(Color.Red, 2f);
        Image firstScreen;

        Bitmap back;

        bool isClicking = false;
        public dragForm()
        {
            InitializeComponent();
            this.Size = CaptureLib.fullScreensSize();
            this.MouseDown += (b, d) =>
            {
                if (d.Button == MouseButtons.Left)
                {
                    loc = d.Location;
                    keystate = true;
                    isClicking = true;
                }
                else if (d.Button == MouseButtons.Right)
                {
                    dragCanceled();
                    this.Dispose();
                }
            };
            this.MouseMove += (b, d) =>
            {
                g = Graphics.FromImage(back);
                g.DrawImage(firstScreen, new PointF());
                if (isClicking)
                {
                    g.DrawLine(p, loc.X, loc.Y, loc.X, d.Y);
                    g.DrawLine(p, loc.X, loc.Y, d.X, loc.Y);
                    g.DrawLine(p, d.X, loc.Y, d.X, d.Y);
                    g.DrawLine(p, loc.X, d.Y, d.X, d.Y);
                }
                else
                {
                    g.DrawLine(p, new Point(0, d.Y), new Point(Size.Width, d.Y));
                    g.DrawLine(p, new Point(d.X, 0), new Point(d.X, Size.Height));
                }

                this.CreateGraphics().DrawImage(back, 0, 0);

                if (!keystate) return;
            };
            this.MouseUp += (b, d) =>
            {
                if (d.Button == MouseButtons.Left)
                {
                    siz = d.Location;
                    isClicking = false;
                    keystate = false;
                    if (dragFinished != null)
                    {
                        int x, y, width, height;
                        x = siz.X > loc.X ? loc.X : siz.X;
                        y = siz.Y > loc.Y ? loc.Y : siz.Y;
                        width = siz.X > loc.X ? siz.X - loc.X : loc.X - siz.X;
                        height = siz.Y > loc.Y ? siz.Y - loc.Y : loc.Y - siz.Y;
                        Rectangle rect = new Rectangle(x, y, width, height);

                        if(width == 0 || height == 0)
                        {
                            //MessageBox.Show("가로 혹은 세로의 길이가 0입니다!", "에러");
                            dragCanceled();
                        }

                        this.Hide();
                        back.Dispose();
                        g.Dispose(); p.Dispose();

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        if (!(width == 0 || height == 0))
                            dragFinished(rect, ((Bitmap)firstScreen).Clone(rect, firstScreen.PixelFormat));
                        firstScreen.Dispose();
                    }
                    this.Dispose();
                }
            };
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            this.UpdateStyles();

            Cursor.Hide();
            
            this.BackgroundImage = this.firstScreen = HotKeyManager.ScreenCapture.CaptureScreen();
            back = new Bitmap(Size.Width, Size.Height);
            this.Opacity = 1;
        }

        private void dragForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor.Show();
        }
    }
}
