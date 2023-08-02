namespace GetInvoice
{
    partial class frmGmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGmail));
            this.pn_Timer = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.num_LoadMailTime = new System.Windows.Forms.NumericUpDown();
            this.num_Timer = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckb_EnableCal = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Gmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Subject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.txt_PassWord = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Domain = new System.Windows.Forms.TextBox();
            this.ckb_IsDomain = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Pdf = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btn_CheckConnectDomain = new System.Windows.Forms.Button();
            this.pn_Timer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LoadMailTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pn_Timer
            // 
            this.pn_Timer.Controls.Add(this.label8);
            this.pn_Timer.Controls.Add(this.num_LoadMailTime);
            this.pn_Timer.Controls.Add(this.num_Timer);
            this.pn_Timer.Controls.Add(this.label3);
            this.pn_Timer.Controls.Add(this.label2);
            this.pn_Timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pn_Timer.Location = new System.Drawing.Point(35, 247);
            this.pn_Timer.Name = "pn_Timer";
            this.pn_Timer.Size = new System.Drawing.Size(563, 32);
            this.pn_Timer.TabIndex = 0;
            this.pn_Timer.Visible = false;
            this.pn_Timer.Paint += new System.Windows.Forms.PaintEventHandler(this.pn_Timer_Paint);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(422, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "Last date";
            // 
            // num_LoadMailTime
            // 
            this.num_LoadMailTime.Location = new System.Drawing.Point(507, 3);
            this.num_LoadMailTime.Name = "num_LoadMailTime";
            this.num_LoadMailTime.Size = new System.Drawing.Size(53, 26);
            this.num_LoadMailTime.TabIndex = 8;
            // 
            // num_Timer
            // 
            this.num_Timer.Location = new System.Drawing.Point(198, 3);
            this.num_Timer.Name = "num_Timer";
            this.num_Timer.Size = new System.Drawing.Size(77, 26);
            this.num_Timer.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "phút/ lần";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tự động kiểm tra mỗi";
            // 
            // ckb_EnableCal
            // 
            this.ckb_EnableCal.AutoSize = true;
            this.ckb_EnableCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckb_EnableCal.Location = new System.Drawing.Point(35, 217);
            this.ckb_EnableCal.Name = "ckb_EnableCal";
            this.ckb_EnableCal.Size = new System.Drawing.Size(200, 24);
            this.ckb_EnableCal.TabIndex = 1;
            this.ckb_EnableCal.Text = "Lên lịch kiểm tra gmail";
            this.ckb_EnableCal.UseVisualStyleBackColor = true;
            this.ckb_EnableCal.CheckedChanged += new System.EventHandler(this.ckb_EnableCal_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gmail";
            // 
            // txt_Gmail
            // 
            this.txt_Gmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Gmail.Location = new System.Drawing.Point(178, 9);
            this.txt_Gmail.Name = "txt_Gmail";
            this.txt_Gmail.Size = new System.Drawing.Size(277, 26);
            this.txt_Gmail.TabIndex = 3;
            this.txt_Gmail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_Gmail_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tìm theo tiêu đề:";
            // 
            // txt_Subject
            // 
            this.txt_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Subject.Location = new System.Drawing.Point(178, 139);
            this.txt_Subject.Name = "txt_Subject";
            this.txt_Subject.Size = new System.Drawing.Size(277, 26);
            this.txt_Subject.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(31, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(542, 37);
            this.label5.TabIndex = 6;
            this.label5.Text = "VD: tìm tiêu đề có các nội dung : hóa đơn điện tử số,CÔNG TY TNHH";
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(35, 285);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 30);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "Lưu";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_PassWord
            // 
            this.txt_PassWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PassWord.Location = new System.Drawing.Point(178, 43);
            this.txt_PassWord.Name = "txt_PassWord";
            this.txt_PassWord.PasswordChar = '*';
            this.txt_PassWord.Size = new System.Drawing.Size(277, 26);
            this.txt_PassWord.TabIndex = 9;
            this.txt_PassWord.Validating += new System.ComponentModel.CancelEventHandler(this.txt_PassWord_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(31, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "PassWord";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(31, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Domain";
            // 
            // txt_Domain
            // 
            this.txt_Domain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Domain.Location = new System.Drawing.Point(178, 75);
            this.txt_Domain.Name = "txt_Domain";
            this.txt_Domain.Size = new System.Drawing.Size(277, 26);
            this.txt_Domain.TabIndex = 12;
            this.txt_Domain.TextChanged += new System.EventHandler(this.txt_Domain_TextChanged);
            this.txt_Domain.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Domain_Validating);
            // 
            // ckb_IsDomain
            // 
            this.ckb_IsDomain.AutoSize = true;
            this.ckb_IsDomain.Location = new System.Drawing.Point(471, 9);
            this.ckb_IsDomain.Name = "ckb_IsDomain";
            this.ckb_IsDomain.Size = new System.Drawing.Size(104, 20);
            this.ckb_IsDomain.TabIndex = 13;
            this.ckb_IsDomain.Text = "Mail Domain";
            this.ckb_IsDomain.UseVisualStyleBackColor = true;
            this.ckb_IsDomain.CheckedChanged += new System.EventHandler(this.ckb_IsDomain_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(31, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Path Pdf";
            // 
            // txt_Pdf
            // 
            this.txt_Pdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Pdf.Location = new System.Drawing.Point(178, 107);
            this.txt_Pdf.Name = "txt_Pdf";
            this.txt_Pdf.Size = new System.Drawing.Size(277, 26);
            this.txt_Pdf.TabIndex = 17;
            this.txt_Pdf.TextChanged += new System.EventHandler(this.txt_Pdf_TextChanged);
            this.txt_Pdf.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_Pdf_MouseDoubleClick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btn_CheckConnectDomain
            // 
            this.btn_CheckConnectDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CheckConnectDomain.Location = new System.Drawing.Point(116, 285);
            this.btn_CheckConnectDomain.Name = "btn_CheckConnectDomain";
            this.btn_CheckConnectDomain.Size = new System.Drawing.Size(203, 30);
            this.btn_CheckConnectDomain.TabIndex = 18;
            this.btn_CheckConnectDomain.Text = "Test Connect Domain";
            this.btn_CheckConnectDomain.UseVisualStyleBackColor = true;
            this.btn_CheckConnectDomain.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmGmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 324);
            this.Controls.Add(this.btn_CheckConnectDomain);
            this.Controls.Add(this.txt_Pdf);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ckb_IsDomain);
            this.Controls.Add(this.txt_Domain);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_PassWord);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Subject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Gmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckb_EnableCal);
            this.Controls.Add(this.pn_Timer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGmail";
            this.Text = "Thiết lập Gmail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGmail_FormClosing);
            this.Load += new System.EventHandler(this.frmGmail_Load);
            this.pn_Timer.ResumeLayout(false);
            this.pn_Timer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LoadMailTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pn_Timer;
        private System.Windows.Forms.CheckBox ckb_EnableCal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Gmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Subject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown num_Timer;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox txt_PassWord;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Domain;
        private System.Windows.Forms.CheckBox ckb_IsDomain;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Pdf;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btn_CheckConnectDomain;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown num_LoadMailTime;
    }
}