namespace KeyboardMouseMonitor.UI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labCtrl = new System.Windows.Forms.Label();
            this.labAlt = new System.Windows.Forms.Label();
            this.labShift = new System.Windows.Forms.Label();
            this.labCaps = new System.Windows.Forms.Label();
            this.labKey = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(473, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "left";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "wheel";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(545, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "right";
            // 
            // labCtrl
            // 
            this.labCtrl.AutoSize = true;
            this.labCtrl.Location = new System.Drawing.Point(12, 144);
            this.labCtrl.Name = "labCtrl";
            this.labCtrl.Size = new System.Drawing.Size(29, 12);
            this.labCtrl.TabIndex = 5;
            this.labCtrl.Text = "Ctrl";
            // 
            // labAlt
            // 
            this.labAlt.AutoSize = true;
            this.labAlt.Location = new System.Drawing.Point(69, 144);
            this.labAlt.Name = "labAlt";
            this.labAlt.Size = new System.Drawing.Size(23, 12);
            this.labAlt.TabIndex = 6;
            this.labAlt.Text = "Alt";
            // 
            // labShift
            // 
            this.labShift.AutoSize = true;
            this.labShift.Location = new System.Drawing.Point(12, 109);
            this.labShift.Name = "labShift";
            this.labShift.Size = new System.Drawing.Size(35, 12);
            this.labShift.TabIndex = 7;
            this.labShift.Text = "Shift";
            // 
            // labCaps
            // 
            this.labCaps.AutoSize = true;
            this.labCaps.Location = new System.Drawing.Point(12, 73);
            this.labCaps.Name = "labCaps";
            this.labCaps.Size = new System.Drawing.Size(53, 12);
            this.labCaps.TabIndex = 8;
            this.labCaps.Text = "CapsLock";
            // 
            // labKey
            // 
            this.labKey.AutoSize = true;
            this.labKey.Location = new System.Drawing.Point(83, 109);
            this.labKey.Name = "labKey";
            this.labKey.Size = new System.Drawing.Size(23, 12);
            this.labKey.TabIndex = 9;
            this.labKey.Text = "Key";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(133, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(263, 136);
            this.listBox1.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(479, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "up";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(479, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "down";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 205);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labKey);
            this.Controls.Add(this.labCaps);
            this.Controls.Add(this.labShift);
            this.Controls.Add(this.labAlt);
            this.Controls.Add(this.labCtrl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labCtrl;
        private System.Windows.Forms.Label labAlt;
        private System.Windows.Forms.Label labShift;
        private System.Windows.Forms.Label labCaps;
        private System.Windows.Forms.Label labKey;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

