using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptIt
{
    public partial class UpdateForm : Form
    {
        VERSION v;
        public UpdateForm(VERSION v)
        {
            InitializeComponent();
            this.v = v;
            this.textBox1.Text = v.Description;
            this.label2.Text = String.Format("현재 버젼 : {0:0.0.0} 새로운 버젼 : {1:0.0.0}", MainForm.VERSION, v.Version);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(v.DownloadLink);
            MainForm.APPEXIT();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm.APPEXIT();
        }

        private void UpdateForm_Shown(object sender, EventArgs e)
        {

            this.button1.Focus();
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
