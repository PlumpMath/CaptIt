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
    public partial class SET_General : Form, ISettingPanel
    {
        public SET_General()
        {
            InitializeComponent();
        }

        public void SaveSet()
        {
            MainForm.Setting.isShowForm = this.cbFormShow.Checked;
            MainForm.Setting.isAutoStart = this.checkBox1.Checked;
            MainForm.Setting.SaveSettings();

            MainForm.Setting.SetStartup(checkBox1.Checked);
        }
        
        public void LoadSet()
        {
            this.cbFormShow.Checked = MainForm.Setting.isShowForm;
            this.checkBox1.Checked = MainForm.Setting.isAutoStart;
        }
    }
}
