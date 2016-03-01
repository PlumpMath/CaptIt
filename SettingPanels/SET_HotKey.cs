using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptIt.SettingPanels
{
    public partial class SET_HotKey : Form, ISettingPanel
    {
        public SET_HotKey()
        {
            InitializeComponent();
        }

        public void SaveSet()
        {
            HotKeyManager.Hotkeys[0].Key = KS_FSS.Key;
            HotKeyManager.SaveKeySet(HotKeyManager.Hotkeys[0]);
            HotKeyManager.Hotkeys[1].Key = KS_DSS.Key;
            HotKeyManager.SaveKeySet(HotKeyManager.Hotkeys[1]);
            HotKeyManager.Hotkeys[2].Key = KS_H1.Key;
            (HotKeyManager.Hotkeys[2] as HotKeyScreenShot).SetKey = KS_HD1.Key;
            HotKeyManager.SaveHotKeySet(HotKeyManager.Hotkeys[2] as HotKeyScreenShot);
            HotKeyManager.Hotkeys[6].Key = KS_Handle.Key;
            ((HotKeyManager.Hotkeys[6]) as WindowScreenShot).SetKey = KS_HandleGet.Key;
        }

        public void LoadSet()
        {
            this.KS_FSS.Key = HotKeyManager.GetKeySet(HotKeyManager.Hotkeys[0]);
            this.KS_DSS.Key = HotKeyManager.GetKeySet(HotKeyManager.Hotkeys[1]);
            this.KS_H1.Key = HotKeyManager.GetHotKeySet(HotKeyManager.Hotkeys[2] as HotKeyScreenShot)[0];
            this.KS_HD1.Key = HotKeyManager.GetHotKeySet(HotKeyManager.Hotkeys[2] as HotKeyScreenShot)[1];
            this.KS_Handle.Key = HotKeyManager.GetHandleKeySet(((HotKeyManager.Hotkeys[6]) as WindowScreenShot))[0];
            this.KS_HandleGet.Key = HotKeyManager.GetHandleKeySet(((HotKeyManager.Hotkeys[6]) as WindowScreenShot))[1];
        }
    }
}
