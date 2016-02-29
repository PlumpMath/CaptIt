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
    public partial class GetWindowProcess : Form
    {
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
        

        #endregion
        public GetWindowProcess()
        {
            InitializeComponent();
        }

        IntPtr desktop;
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

        IntPtr currentHandle;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(currentHandle != GetHandle())
            {
                currentHandle = GetHandle();
                if(currentHandle != null || currentHandle != (IntPtr)0)
                DrawRevFrame(GetHandle());
            }
        }
    }
}
