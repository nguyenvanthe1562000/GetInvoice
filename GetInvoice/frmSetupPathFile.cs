using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GetInvoice.utilities;

namespace GetInvoice
{
    public partial class frmSetupPathFile : Form
    {
        private UserInfo local_user;
        public frmSetupPathFile(UserInfo user_info)
        {
            InitializeComponent();
            local_user = user_info;
            txtFileDirectory.Text = user_info.path_load_file;
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtFileDirectory.Text = fbd.SelectedPath;
                }
            }


            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = (local_user.path_load_file == "" ? "c:\\" : local_user.path_load_file);
            //openFileDialog1.Filter = "XML|*.xml";
            //openFileDialog1.FilterIndex = 0;
            //openFileDialog1.RestoreDirectory = true;

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string selectedFileName = openFileDialog1.FileName;
            //    txtFileDirectory.Text = selectedFileName;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ExeSQLNonQuery(string.Format("update s_user set path_load_file = '{0}' where ma_nd = '{1}'", txtFileDirectory.Text, local_user.ma_nd));
            Message_Box("Cập nhật thành công");
            this.Close();
        }

        private void frmSetupPathFile_Load(object sender, EventArgs e)
        {

        }
    }
}
