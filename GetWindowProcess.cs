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
    public delegate void d3(IntPtr i);
    public partial class GetWindowProcess : Form
    {
        Graphics g;
        Pen p = new Pen(Color.Red, 2f);

        public event d3 GotHandle;
        
        class API
        {
            [DllImport("user32.dll")]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
            [DllImport("user32.dll")]
            public static extern IntPtr WindowFromPoint(int x, int y);
            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
            [DllImport("user32.dll")]
            public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);[DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);
            [DllImport("User32.dll")]
            public static extern IntPtr GetDC(IntPtr hwnd);
            [DllImport("User32.dll")]
            public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int Left;        // x position of upper-left corner
                public int Top;         // y position of upper-left corner
                public int Right;       // x position of lower-right corner
                public int Bottom;      // y position of lower-right corner
            }
        }

        public GetWindowProcess()
        {
            InitializeComponent();
            this.Size = CaptureLib.fullScreensSize();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();

            desktop = API.GetDC(desktop);
            g = Graphics.FromHdc(desktop);
        }
        IntPtr desktop;

        ~GetWindowProcess()
        {
            g.Dispose();
            API.ReleaseDC(IntPtr.Zero, desktop);
        }

        IntPtr currentHandle;       
        private void GetWindowProcess_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void GetWindowProcess_MouseUp(object sender, MouseEventArgs e)
        {
            currentHandle = GetProcessMouseOn();
        }

        private IntPtr GetProcessMouseOn()
        {
            return API.WindowFromPoint(Cursor.Position.X, Cursor.Position.Y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr handle = GetProcessMouseOn();
            API.RECT rect = new API.RECT();
            if (!API.GetWindowRect(new HandleRef(this, this.Handle), out rect))
            {
                return;
            }
            g.Clear(this.BackColor);
            g.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Bottom);
            g.DrawRectangle(p, new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left + 1, rect.Bottom - rect.Top + 1));
        }
    }
}
