using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CaptIt
{
    public delegate void g(IntPtr handle);
    public delegate void g2();

    public partial class GetWindowProcess : Form
    {
        public event g captFinished;
        public event g2 captCanceled;

        #region API
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            #region Helper methods

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }

            #endregion
        }

        const int DSTINVERT = 0x00550009;
        [DllImport("gdi32.dll")]
        static extern bool PatBlt(IntPtr hdc, int nXLeft, int nYLeft, int nWidth, int nHeight, uint dwRop);

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        static extern bool OffsetRect(ref RECT lprc, int dx, int dy);

        [DllImport("user32.dll")]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);[DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("user32.dll")]
        protected static extern IntPtr WindowFromPoint(int x, int y);
        private string GetWindowText(IntPtr hWnd)
        {
            StringBuilder text = new StringBuilder(256);
            if (GetWindowText(hWnd.ToInt32(), text, text.Capacity) > 0)
            {
                return text.ToString();
            }

            return String.Empty;
        }

        private string GetApplication(IntPtr hWnd)
        {
            try {
                int procId;
                GetWindowThreadProcessId(hWnd, out procId);
                Process proc = Process.GetProcessById(procId);
                return proc.MainModule.ModuleName;
            }
            catch
            {
                return "";
            }
        }

        #endregion
        public GetWindowProcess()
        {
            InitializeComponent();
            curCross = new Cursor(Environment.CurrentDirectory + "\\cross.cur");
        }
        
        ~GetWindowProcess()
        {
        }

        public static Rectangle GetRectFromMouse()
        {
            RECT rct;
            if (!GetWindowRect(GetHandle(), out rct))
            {
                MessageBox.Show("ERROR");
                return new Rectangle();
            }

            return new Rectangle(rct.Left, rct.Top, rct.Right - rct.Left + 1, rct.Bottom - rct.Top + 1);
        }

        private static IntPtr GetHandle()
        {
            return WindowFromPoint(Cursor.Position.X, Cursor.Position.Y);
        }

        private void DrawRevFrame(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return;

            IntPtr hdc = GetWindowDC(hWnd);
            RECT rect;
            GetWindowRect(hWnd, out rect);
            OffsetRect(ref rect, -rect.Left, -rect.Top);

            const int frameWidth = 3;

            PatBlt(hdc, rect.Left, rect.Top, rect.Right - rect.Left, frameWidth, DSTINVERT);
            PatBlt(hdc, rect.Left, rect.Bottom - frameWidth, frameWidth,
                -(rect.Bottom - rect.Top - 2 * frameWidth), DSTINVERT);
            PatBlt(hdc, rect.Right - frameWidth, rect.Top + frameWidth, frameWidth,
                rect.Bottom - rect.Top - 2 * frameWidth, DSTINVERT);
            PatBlt(hdc, rect.Right, rect.Bottom - frameWidth, -(rect.Right - rect.Left),
                frameWidth, DSTINVERT);
        }

        IntPtr _hWndCurrent;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(_hWndCurrent != GetHandle())
            {
                _hWndCurrent = GetHandle();
                if(_hWndCurrent != null || _hWndCurrent != (IntPtr)0)
                DrawRevFrame(GetHandle());
            }
        }

        bool _dragging = false;
        Cursor curCross;
        private void dragPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                this.Cursor = curCross;
                dragPictureBox.Image = null;
            }
        }

        private void dragPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                _dragging = false;
                this.Cursor = Cursors.Default;
                if (_hWndCurrent != IntPtr.Zero)
                {
                    DrawRevFrame(_hWndCurrent);
                    if(captFinished != null)
                    {
                        captFinished(_hWndCurrent);
                        _hWndCurrent = IntPtr.Zero;
                        this.Dispose();
                    }
                }
                else
                {
                    return;
                }
            }
        }
       

        private void dragPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                IntPtr hWnd = WindowFromPoint(MousePosition);
                if (hWnd == dragPictureBox.Handle)
                {
                    // Drawing a border around the dragPictureBox (where we start
                    // dragging) doesn't look nice, so we ignore this window
                    hWnd = IntPtr.Zero;
                }

                if (hWnd != _hWndCurrent)
                {
                    if (_hWndCurrent != null)
                    {
                        DrawRevFrame(_hWndCurrent);
                    }
                    DrawRevFrame(hWnd);
                    _hWndCurrent = hWnd;
                }
                if(hWnd != IntPtr.Zero)
                {
                    tbHandle.Text = hWnd.ToString();
                    tbText.Text = GetWindowText(hWnd);
                    tbApp.Text = GetApplication(hWnd);
                }
            }
        }

        private void GetWindowProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            captCanceled();
        }
    }
}
