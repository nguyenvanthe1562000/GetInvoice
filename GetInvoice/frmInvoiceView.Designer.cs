namespace GetInvoice
{
    partial class frmInvoiceView
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
            this.web_Invoice = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // web_Invoice
            // 
            this.web_Invoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web_Invoice.Location = new System.Drawing.Point(0, 0);
            this.web_Invoice.MinimumSize = new System.Drawing.Size(20, 20);
            this.web_Invoice.Name = "web_Invoice";
            this.web_Invoice.Size = new System.Drawing.Size(1394, 683);
            this.web_Invoice.TabIndex = 0;
            // 
            // frmInvoiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 683);
            this.Controls.Add(this.web_Invoice);
            this.Name = "frmInvoiceView";
            this.Text = "frmInvoiceView";
            this.Load += new System.EventHandler(this.frmInvoiceView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser web_Invoice;
    }
}