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
        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("user32.dll")]
        protected static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        [DllImport("user32.dll")]
        protected static extern IntPtr WindowFromPoint(int x, int y);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public GetWindowProcess()
        {
            InitializeComponent();
        }

        IntPtr desktop;
        ~GetWindowProcess()
        {
            ReleaseDC(IntPtr.Zero, desktop);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            desktop = GetDC(IntPtr.Zero);
            using (Graphics g = Graphics.FromHdc(desktop))
            {
                ReleaseDC(IntPtr.Zero, desktop);
            }
        }
    }
}
