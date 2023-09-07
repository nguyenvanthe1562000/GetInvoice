using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetInvoice
{
    public partial class frmInvoiceView : Form
    {
        public frmInvoiceView()
        {
            InitializeComponent();
        }

        private void frmInvoiceView_Load(object sender, EventArgs e)
        {
            web_Invoice.Navigate(new Uri(@"C:\Users\truon\Downloads\invoice\invoice.html"));
        }
    }
}
