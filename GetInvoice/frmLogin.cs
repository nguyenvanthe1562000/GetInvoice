using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static GetInvoice.utilities;

namespace GetInvoice
{
    public partial class frmLogin : Form
    {
        utilities clUtils = new utilities();
        //public string inUserName;
        //public string inFullName;
        public UserInfo inUserInfo = new UserInfo();

        public frmLogin()
        {
            InitializeComponent();
            txtUsername.Select();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _passEncrypt = utilities.Encrypt(txtPassword.Text.Trim());
            string _checkLogin = utilities.CheckLogin(txtUsername.Text.Trim(), _passEncrypt);
            if (_checkLogin == "ok")
            {
                this.DialogResult = DialogResult.OK;
                //inUserInfo.ma_nd = txtUsername.Text.Trim();
                //inUserInfo.ten_nd = "Administrator";
                inUserInfo = getUserInfo(txtUsername.Text.Trim());
                this.Close();
            }
            else
            {
                MessageBox.Show(_checkLogin, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //bool checkConn = IsServerConnected(connection);

            List<string> listConn = new List<string>();
            var conns = System.Configuration.ConfigurationManager.ConnectionStrings;
            for (int i = 0; i < conns.Count; i++)
            {
                if (conns[i].Name != "LocalSqlServer" && conns[i].Name != "OraAspNetConString")
                    listConn.Add(System.Configuration.ConfigurationManager.ConnectionStrings[i].Name);
            }
            if (listConn.Count == 0)
            {
                MessageBox.Show("Chưa khai báo chuỗi kết nối tới CSDL", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cboConnections.Properties.DataSource = listConn;
                cboConnections.ItemIndex = 0;
                utilities.NAME_CONNECTION_STRING = cboConnections.EditValue.ToString();
            }
        }

        private static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }
}
