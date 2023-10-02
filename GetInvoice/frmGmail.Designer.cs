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
            this.lblMST = new System.Windows.Forms.Label();
            this.txt_MST = new System.Windows.Forms.TextBox();
            this.txt_MstPass = new System.Windows.Forms.TextBox();
            this.lbl5 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_CaptCha = new System.Windows.Forms.TextBox();
            this.btn_login_HDDT = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.img_CaptCha = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ckb_MailDownload = new System.Windows.Forms.CheckBox();
            this.ckb_GovDownload = new System.Windows.Forms.CheckBox();
            this.pn_Timer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LoadMailTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_CaptCha)).BeginInit();
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
            this.pn_Timer.Location = new System.Drawing.Point(15, 469);
            this.pn_Timer.Name = "pn_Timer";
            this.pn_Timer.Size = new System.Drawing.Size(583, 32);
            this.pn_Timer.TabIndex = 0;
            this.pn_Timer.Visible = false;
            this.pn_Timer.Paint += new System.Windows.Forms.PaintEventHandler(this.pn_Timer_Paint);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(412, 5);
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
            this.num_LoadMailTime.ValueChanged += new System.EventHandler(this.num_LoadMailTime_ValueChanged);
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
            this.ckb_EnableCal.Location = new System.Drawing.Point(25, 439);
            this.ckb_EnableCal.Name = "ckb_EnableCal";
            this.ckb_EnableCal.Size = new System.Drawing.Size(209, 24);
            this.ckb_EnableCal.TabIndex = 1;
            this.ckb_EnableCal.Text = "Lên lịch kiểm tra HDDT";
            this.ckb_EnableCal.UseVisualStyleBackColor = true;
            this.ckb_EnableCal.CheckedChanged += new System.EventHandler(this.ckb_EnableCal_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mail";
            // 
            // txt_Gmail
            // 
            this.txt_Gmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Gmail.Location = new System.Drawing.Point(163, 20);
            this.txt_Gmail.Name = "txt_Gmail";
            this.txt_Gmail.Size = new System.Drawing.Size(281, 26);
            this.txt_Gmail.TabIndex = 3;
            this.txt_Gmail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_Gmail_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tìm theo tiêu đề:";
            // 
            // txt_Subject
            // 
            this.txt_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Subject.Location = new System.Drawing.Point(165, 118);
            this.txt_Subject.Name = "txt_Subject";
            this.txt_Subject.Size = new System.Drawing.Size(277, 26);
            this.txt_Subject.TabIndex = 5;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(8, 521);
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
            this.txt_PassWord.Location = new System.Drawing.Point(165, 54);
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
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "PassWord";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Domain";
            // 
            // txt_Domain
            // 
            this.txt_Domain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Domain.Location = new System.Drawing.Point(165, 86);
            this.txt_Domain.Name = "txt_Domain";
            this.txt_Domain.Size = new System.Drawing.Size(277, 26);
            this.txt_Domain.TabIndex = 12;
            this.txt_Domain.TextChanged += new System.EventHandler(this.txt_Domain_TextChanged);
            this.txt_Domain.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Domain_Validating);
            // 
            // ckb_IsDomain
            // 
            this.ckb_IsDomain.AutoSize = true;
            this.ckb_IsDomain.Location = new System.Drawing.Point(450, 20);
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
            this.label9.Location = new System.Drawing.Point(23, 410);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Path Pdf";
            // 
            // txt_Pdf
            // 
            this.txt_Pdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Pdf.Location = new System.Drawing.Point(178, 407);
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
            this.btn_CheckConnectDomain.Location = new System.Drawing.Point(98, 521);
            this.btn_CheckConnectDomain.Name = "btn_CheckConnectDomain";
            this.btn_CheckConnectDomain.Size = new System.Drawing.Size(203, 30);
            this.btn_CheckConnectDomain.TabIndex = 18;
            this.btn_CheckConnectDomain.Text = "Test Connect Domain";
            this.btn_CheckConnectDomain.UseVisualStyleBackColor = true;
            this.btn_CheckConnectDomain.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblMST
            // 
            this.lblMST.AutoSize = true;
            this.lblMST.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMST.Location = new System.Drawing.Point(6, 29);
            this.lblMST.Name = "lblMST";
            this.lblMST.Size = new System.Drawing.Size(44, 20);
            this.lblMST.TabIndex = 2;
            this.lblMST.Text = "MST";
            // 
            // txt_MST
            // 
            this.txt_MST.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MST.Location = new System.Drawing.Point(165, 21);
            this.txt_MST.Name = "txt_MST";
            this.txt_MST.Size = new System.Drawing.Size(277, 26);
            this.txt_MST.TabIndex = 3;
            // 
            // txt_MstPass
            // 
            this.txt_MstPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MstPass.Location = new System.Drawing.Point(165, 56);
            this.txt_MstPass.Name = "txt_MstPass";
            this.txt_MstPass.PasswordChar = '*';
            this.txt_MstPass.Size = new System.Drawing.Size(277, 26);
            this.txt_MstPass.TabIndex = 9;
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl5.Location = new System.Drawing.Point(6, 62);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(87, 20);
            this.lbl5.TabIndex = 10;
            this.lbl5.Text = "PassWord";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Nhập mã captcha";
            // 
            // txt_CaptCha
            // 
            this.txt_CaptCha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CaptCha.Location = new System.Drawing.Point(165, 90);
            this.txt_CaptCha.Name = "txt_CaptCha";
            this.txt_CaptCha.Size = new System.Drawing.Size(277, 26);
            this.txt_CaptCha.TabIndex = 13;
            // 
            // btn_login_HDDT
            // 
            this.btn_login_HDDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login_HDDT.Location = new System.Drawing.Point(307, 521);
            this.btn_login_HDDT.Name = "btn_login_HDDT";
            this.btn_login_HDDT.Size = new System.Drawing.Size(226, 30);
            this.btn_login_HDDT.TabIndex = 21;
            this.btn_login_HDDT.Text = "Test Connect HDDT GOV";
            this.btn_login_HDDT.UseVisualStyleBackColor = true;
            this.btn_login_HDDT.Click += new System.EventHandler(this.btn_login_HDDT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_Gmail);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ckb_IsDomain);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_Domain);
            this.groupBox1.Controls.Add(this.txt_PassWord);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_Subject);
            this.groupBox1.Location = new System.Drawing.Point(15, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 174);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mail";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_CaptCha);
            this.groupBox2.Controls.Add(this.lblMST);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lbl5);
            this.groupBox2.Controls.Add(this.img_CaptCha);
            this.groupBox2.Controls.Add(this.txt_MstPass);
            this.groupBox2.Controls.Add(this.txt_MST);
            this.groupBox2.Location = new System.Drawing.Point(17, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 185);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "hoadondientu.gdt.gov.vn";
            // 
            // img_CaptCha
            // 
            this.img_CaptCha.Location = new System.Drawing.Point(165, 122);
            this.img_CaptCha.Name = "img_CaptCha";
            this.img_CaptCha.Size = new System.Drawing.Size(273, 49);
            this.img_CaptCha.TabIndex = 11;
            this.img_CaptCha.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(23, 378);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 20);
            this.label10.TabIndex = 25;
            this.label10.Text = "Tùy chọn tải";
            // 
            // ckb_MailDownload
            // 
            this.ckb_MailDownload.AutoSize = true;
            this.ckb_MailDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckb_MailDownload.Location = new System.Drawing.Point(178, 377);
            this.ckb_MailDownload.Name = "ckb_MailDownload";
            this.ckb_MailDownload.Size = new System.Drawing.Size(70, 24);
            this.ckb_MailDownload.TabIndex = 26;
            this.ckb_MailDownload.Text = "MAIL";
            this.ckb_MailDownload.UseVisualStyleBackColor = true;
            // 
            // ckb_GovDownload
            // 
            this.ckb_GovDownload.AutoSize = true;
            this.ckb_GovDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckb_GovDownload.Location = new System.Drawing.Point(293, 377);
            this.ckb_GovDownload.Name = "ckb_GovDownload";
            this.ckb_GovDownload.Size = new System.Drawing.Size(68, 24);
            this.ckb_GovDownload.TabIndex = 27;
            this.ckb_GovDownload.Text = "GOV";
            this.ckb_GovDownload.UseVisualStyleBackColor = true;
            // 
            // frmGmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 563);
            this.Controls.Add(this.ckb_GovDownload);
            this.Controls.Add(this.ckb_MailDownload);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_login_HDDT);
            this.Controls.Add(this.txt_Pdf);
            this.Controls.Add(this.btn_CheckConnectDomain);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.ckb_EnableCal);
            this.Controls.Add(this.pn_Timer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cổng nhận HDDT Online";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGmail_FormClosing);
            this.Load += new System.EventHandler(this.frmGmail_Load);
            this.pn_Timer.ResumeLayout(false);
            this.pn_Timer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LoadMailTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_CaptCha)).EndInit();
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
        private System.Windows.Forms.Label lblMST;
        private System.Windows.Forms.TextBox txt_MST;
        private System.Windows.Forms.TextBox txt_MstPass;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.TextBox txt_CaptCha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox img_CaptCha;
        private System.Windows.Forms.Button btn_login_HDDT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckb_MailDownload;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ckb_GovDownload;
    }
}