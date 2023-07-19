using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static GetInvoice.utilities;

namespace GetInvoice
{
    public partial class frmMappingColumn : Form
    {
        public frmMappingColumn()
        {
            InitializeComponent();
            Load_Data();
        }

        private void Load_Data()
        {
            DataTable dt = ExeSQL("select * from m_mapping_columns order by db_column_name");
            gridMappingCols.DataSource = dt;
            grvMappingCols.BestFitColumns();
        }

        private void frmMappingColumn_Load(object sender, EventArgs e)
        {

        }
    }
}
