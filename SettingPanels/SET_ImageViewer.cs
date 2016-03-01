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
    public partial class SET_ImageViewer : Form, ISettingPanel
    {
        public SET_ImageViewer()
        {
            InitializeComponent();
        }

        public void SaveSet()
        {
            MainForm.Setting.isShowEditorAfterCaptureSShot = this.checkBox1.Checked;
            MainForm.Setting.isShowCheckCapturedForm = this.checkBox2.Checked;
            MainForm.Setting.isCopytoClipboard = this.checkBox3.Checked;
            MainForm.Setting.SaveSettings();
        }

        public void LoadSet()
        {
            this.checkBox1.Checked = MainForm.Setting.isShowEditorAfterCaptureSShot;
            this.checkBox2.Checked = MainForm.Setting.isShowCheckCapturedForm;
            this.checkBox3.Checked = MainForm.Setting.isCopytoClipboard;
        }
    }
}
