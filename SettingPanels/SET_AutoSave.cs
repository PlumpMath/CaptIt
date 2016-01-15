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
    public partial class SET_AutoSave : Form, ISettingPanel
    {
        public SET_AutoSave()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog1.SelectedPath;
                this.textBox1.Focus();
            }
        }

        public void SaveSet()
        {
            MainForm.Setting.isSaveAutoAfterCaptureSShot = this.checkBox2.Checked;
            MainForm.Setting.AutoSavePath = this.textBox1.Text;
            MainForm.Setting.AutoSaveFileName = this.textBox2.Text;
            if (this.radioButton1.Checked)
                MainForm.Setting.ImageSaveFormat = ImageSaveFormat.PNG;
            else if (this.radioButton2.Checked)
                MainForm.Setting.ImageSaveFormat = ImageSaveFormat.JPG;
            else
                MainForm.Setting.ImageSaveFormat = ImageSaveFormat.BMP;
            MainForm.Setting.SaveSettings();
        }

        public void LoadSet()
        {
            this.checkBox2.Checked = MainForm.Setting.isSaveAutoAfterCaptureSShot;
            this.textBox1.Text = MainForm.Setting.AutoSavePath;
            this.textBox2.Text = MainForm.Setting.AutoSaveFileName;
            switch(MainForm.Setting.ImageSaveFormat)
            {
                case ImageSaveFormat.PNG:
                    {
                        this.radioButton1.Checked = true;
                        break;
                    }
                case ImageSaveFormat.JPG:
                    {
                        this.radioButton2.Checked = true;
                        break;
                    }
                case ImageSaveFormat.BMP:
                    {
                        this.radioButton3.Checked = true;
                        break;
                    }
            }
        }
    }
}
