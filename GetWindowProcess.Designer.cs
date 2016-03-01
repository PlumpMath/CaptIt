namespace CaptIt
{
    partial class GetWindowProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetWindowProcess));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dragPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHandle = new System.Windows.Forms.TextBox();
            this.tbText = new System.Windows.Forms.TextBox();
            this.tbApp = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dragPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dragPictureBox
            // 
            this.dragPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dragPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("dragPictureBox.Image")));
            this.dragPictureBox.Location = new System.Drawing.Point(12, 12);
            this.dragPictureBox.Name = "dragPictureBox";
            this.dragPictureBox.Size = new System.Drawing.Size(103, 104);
            this.dragPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dragPictureBox.TabIndex = 3;
            this.dragPictureBox.TabStop = false;
            this.dragPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragPictureBox_MouseDown);
            this.dragPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dragPictureBox_MouseMove);
            this.dragPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dragPictureBox_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "왼쪽 과녁을 드래그하여 원하는 윈도우 위에 드롭하세요!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "핸들    : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "텍스트 : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "이름    :";
            // 
            // tbHandle
            // 
            this.tbHandle.Location = new System.Drawing.Point(181, 35);
            this.tbHandle.Name = "tbHandle";
            this.tbHandle.Size = new System.Drawing.Size(250, 23);
            this.tbHandle.TabIndex = 8;
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(181, 64);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(250, 23);
            this.tbText.TabIndex = 9;
            // 
            // tbApp
            // 
            this.tbApp.Location = new System.Drawing.Point(181, 93);
            this.tbApp.Name = "tbApp";
            this.tbApp.Size = new System.Drawing.Size(250, 23);
            this.tbApp.TabIndex = 10;
            // 
            // GetWindowProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(457, 128);
            this.Controls.Add(this.tbApp);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.tbHandle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dragPictureBox);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GetWindowProcess";
            this.Text = "윈도우 추적!";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetWindowProcess_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dragPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox dragPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbHandle;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.TextBox tbApp;
    }
}