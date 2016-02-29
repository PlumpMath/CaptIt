using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptIt
{
    public class CaptureManager
    {
        HotKeyManager hotKeyManager = new HotKeyManager();

        public CaptureManager()
        {
            KeyHook.HookStart();
            KeyHook.KeyEvented += KeyHook_KeyEvented;
        }

        ~CaptureManager()
        {
            KeyHook.HookEnd();
        }


        private void KeyHook_KeyEvented(Keys k, KeyEventType keyevent)
        {
            if (keyevent == KeyEventType.KEYDOWN)
            {
                if (hotKeyManager.isHotKeyed(k))
                {
                    SShot captured = hotKeyManager.CapturHotkeyed(k);
                    captured.CaptureSShot();
                }

                if (hotKeyManager.isHotKeyHotkeyed(k))
                {
                    HotKeyScreenShot captured = hotKeyManager.CaptureSetKeyed(k);
                    captured.SetRect();
                }

                if(hotKeyManager.isHandleHotKeyed(k))
                {
                    WindowScreenShot captured = hotKeyManager.CaptureHandleHotKeyed(k);
                    captured.SetWindowHandle();
                }
            }
        }
    }
}
