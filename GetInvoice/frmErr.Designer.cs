namespace GetInvoice
{
    partial class frmErr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmErr));
            this.rtb_Mess = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lbl_Mess = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_Mess
            // 
            this.rtb_Mess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtb_Mess.Location = new System.Drawing.Point(0, 160);
            this.rtb_Mess.Name = "rtb_Mess";
            this.rtb_Mess.Size = new System.Drawing.Size(618, 254);
            this.rtb_Mess.TabIndex = 0;
            this.rtb_Mess.Text = "";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Đóng";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(96, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(175, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "Gửi lỗi cho kỹ thuật";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(277, 116);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 38);
            this.button3.TabIndex = 3;
            this.button3.Text = "Mở file log";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbl_Mess
            // 
            this.lbl_Mess.AutoSize = true;
            this.lbl_Mess.Location = new System.Drawing.Point(93, 13);
            this.lbl_Mess.Name = "lbl_Mess";
            this.lbl_Mess.Size = new System.Drawing.Size(16, 16);
            this.lbl_Mess.TabIndex = 4;
            this.lbl_Mess.Text = "...";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Image = global::GetInvoice.Properties.Resources.warning__3_;
            this.button4.Location = new System.Drawing.Point(12, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 74);
            this.button4.TabIndex = 5;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // frmErr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 414);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lbl_Mess);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtb_Mess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmErr";
            this.Text = "Invoice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_Mess;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lbl_Mess;
        private System.Windows.Forms.Button button4;
    }
}