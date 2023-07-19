
namespace GetInvoice
{
    partial class frmSetupPathFile
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
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.txtFileDirectory = new DevExpress.XtraEditors.TextEdit();
            this.btnChooseFile = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDirectory.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(-131, 44);
            this.labelControl19.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(84, 16);
            this.labelControl19.TabIndex = 5;
            this.labelControl19.Text = "Đường dẫn file";
            // 
            // txtFileDirectory
            // 
            this.txtFileDirectory.Location = new System.Drawing.Point(159, 30);
            this.txtFileDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFileDirectory.Name = "txtFileDirectory";
            // 
            // 
            // 
            this.txtFileDirectory.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtFileDirectory.Properties.Appearance.Options.UseBackColor = true;
            this.txtFileDirectory.Properties.ReadOnly = true;
            this.txtFileDirectory.Size = new System.Drawing.Size(525, 22);
            this.txtFileDirectory.TabIndex = 4;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(692, 27);
            this.btnChooseFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(47, 28);
            this.btnChooseFile.TabIndex = 3;
            this.btnChooseFile.Text = "...";
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 33);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(114, 16);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Đường dẫn thư mục";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(639, 78);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSetupPathFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(773, 121);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl19);
            this.Controls.Add(this.txtFileDirectory);
            this.Controls.Add(this.btnChooseFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetupPathFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt thư mục chứa file xml";
            this.Load += new System.EventHandler(this.frmSetupPathFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtFileDirectory.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.TextEdit txtFileDirectory;
        private DevExpress.XtraEditors.SimpleButton btnChooseFile;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Button btnSave;
    }
}