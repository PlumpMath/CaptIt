using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CaptIt
{
    public enum KeyEventType
    {
        KEYDOWN, KEYUP
    }
    public delegate void del(Keys k, KeyEventType keyevent);

    public class KeyHook
    {
        public static event del KeyEvented;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSTEMKEYUP = 0x0105;
        private const int WM_SYSKEYDOWN = 0x104;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
                {
                    int vkCode = lParam.vkCode;
                    int flags = lParam.flags;
                    Keys key = (Keys)vkCode;

                    /*if (key == Keys.LControlKey || key == Keys.RControlKey) key = key | Keys.Control;
                    if (key == Keys.LShiftKey || key == Keys.RShiftKey) key = key | Keys.Shift;
                    if (key == Keys.LMenu || key == Keys.RMenu) key = key | Keys.Alt;*/
                    /*
                    if (flags == 0x20) key = key | Keys.Alt;
                    if (flags == 0x00) key = key | Keys.Control;*/

                    bool Alt = false, Control = false, Shift = false;

                    Alt = (System.Windows.Forms.Control.ModifierKeys & Keys.Alt) != 0;
                    Control = (System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0;
                    Shift = (System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0;

                    if(Alt) key |= Keys.Alt;
                    if(Control) key |= Keys.Control;
                    if(Shift) key |= Keys.Shift;

                    KeyEvented(key, KeyEventType.KEYDOWN);
                }
                else if (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSTEMKEYUP)
                {
                    int vkCode = lParam.vkCode;
                    int flags = lParam.flags;

                    Keys key = (Keys)vkCode;
                    /*
                    if (key == Keys.LControlKey || key == Keys.RControlKey) key = key | Keys.Control;
                    if (key == Keys.LShiftKey || key == Keys.RShiftKey) key = key | Keys.Shift;
                    if (key == Keys.LMenu || key == Keys.RMenu) key = key | Keys.Alt;*/

                    if (flags == 0x20) key = key | Keys.Alt;
                    if (flags == 0x00) key = key | Keys.Control;


                    KeyEvented(key, KeyEventType.KEYUP);
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, ref lParam);
        }

        public static void HookStart()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        public static void HookEnd()
        {
            UnhookWindowsHookEx(_hookID);
        }
    }
}
