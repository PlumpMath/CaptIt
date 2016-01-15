using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptIt.SettingPanels
{
    public partial class KeySet : UserControl
    {
        public KeySet() : this("", Keys.None) { }
        public KeySet(string text, Keys key)
        {
            InitializeComponent();
            this.label1.Text = text;
            SetKey(key);
        }

        public Keys Key
        {
            get
            {
                Keys k = (Keys)Enum.Parse(typeof(Keys), this.textBox1.Text);
                if (checkBox1.Checked) k |= Keys.Control;
                if (checkBox2.Checked) k |= Keys.Alt;
                if (checkBox3.Checked) k |= Keys.Shift;
                return k;
            }
            set
            {
                SetKey(value);
            }
        }

        public string LabelText
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            Keys clearKey = e.KeyCode;
            clearKey &= ~Keys.Alt;
            clearKey &= ~Keys.Control;
            clearKey &= ~Keys.Shift;

            this.textBox1.Text = clearKey.ToString();
        }

        private void SetKey(Keys key)
        {
            if ((key | Keys.Alt) == key) this.checkBox2.Checked = true;
            if ((key | Keys.Control) == key) this.checkBox1.Checked = true;
            if ((key | Keys.Shift) == key) this.checkBox3.Checked = true;

            Keys clearKey = key;
            clearKey &= ~Keys.Alt;
            clearKey &= ~Keys.Control;
            clearKey &= ~Keys.Shift;

            this.textBox1.Text = clearKey.ToString();
        }
    }
}
