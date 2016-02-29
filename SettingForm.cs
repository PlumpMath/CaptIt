using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaptIt.SettingPanels;

namespace CaptIt
{
    public partial class SettingForm : Form
    {
        SET_ImageViewer ImageViewerForm = new SET_ImageViewer() { TopLevel = false };
        SET_AutoSave AutoSaveForm = new SET_AutoSave() { TopLevel = false };
        SET_HotKey HotKeyForm = new SET_HotKey() { TopLevel = false };
        SET_General GeneralForm = new SET_General() { TopLevel = false };

        public SettingForm()
        {
            InitializeComponent();
            
            ImageViewerForm.Parent = this.panel1;
            AutoSaveForm.Parent = this.panel1;
            HotKeyForm.Parent = this.panel1;
            GeneralForm.Parent = this.panel1;

            ImageViewerForm.LoadSet();
            AutoSaveForm.LoadSet();
            HotKeyForm.LoadSet();
            GeneralForm.LoadSet();

            GeneralForm.Show();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            ImageViewerForm.SaveSet();
            AutoSaveForm.SaveSet();
            HotKeyForm.SaveSet();
            GeneralForm.SaveSet();

            this.Dispose();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            MainForm.Setting.LoadSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ImageViewerForm.Show();
            AutoSaveForm.Hide();
            HotKeyForm.Hide();
            GeneralForm.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AutoSaveForm.Show();
            ImageViewerForm.Hide();
            HotKeyForm.Hide();
            GeneralForm.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HotKeyForm.Show();
            AutoSaveForm.Hide();
            ImageViewerForm.Hide();
            GeneralForm.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HotKeyForm.Hide();
            AutoSaveForm.Hide();
            ImageViewerForm.Hide();
            GeneralForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.Show();
        }
    }
}
