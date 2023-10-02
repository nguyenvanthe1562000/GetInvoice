using GetInvoice.Gmail;
using GetInvoice.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
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
            frmMain._HDDTRequest = new Model.HDDTRequestHeader();
            _logger = new FileLogger();
        }
        CaptChaModel captcha;
        private readonly FileLogger _logger;
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            checkVPN();
            string _passEncrypt = utilities.Encrypt(txtPassword.Text.Trim());
            string _checkLogin = utilities.CheckLogin(txtUsername.Text.Trim(), _passEncrypt);
            if (_checkLogin == "ok")
            {
            
                //inUserInfo.ma_nd = txtUsername.Text.Trim();
                //inUserInfo.ten_nd = "Administrator";
                inUserInfo = getUserInfo(txtUsername.Text.Trim());

                if (inUserInfo.username_MST != null && inUserInfo.password_MST != null)
                {
                    if (this.txt_captcha_GOV.Text != "")
                    {
                        var check = await loginHDDTGOV(inUserInfo.username_MST, inUserInfo.password_MST);
                        if (!check)
                        {
                            return;
                        }
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        var loginGOV = MessageBox.Show("Bạn đã Setup tài khoản đăng nhập HDDT GOV bạn có muốn kết nối luôn không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (loginGOV == DialogResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                        }

                    }


                }
               
            }
            else
            {
                MessageBox.Show(_checkLogin, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        async Task<bool> loginHDDTGOV(string userName, string password)
        {
            try
            {

                HDDT_GOV _gov = new HDDT_GOV();
                var reponseMessage = await _gov.GetToken(userName, password, captcha.Key, txt_captcha_GOV.Text);
                if (reponseMessage.Success)
                {
                    frmMain._HDDTRequest.Token = reponseMessage.Token;
                    return true;
                }
                else
                {
                    txt_captcha_GOV.Text = string.Empty;
                    MessageBox.Show("Kết nối thuế không thành công\n" + "-" + reponseMessage.Message);
                    GetImgCaptCha();
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr frmErr = new frmErr(LogType.Error, this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr.ShowDialog();
                return false;
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
       void checkVPN()
        {

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface nic in networkInterfaces)
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ppp)
                {
                    // Kiểm tra xem giao diện này là một kết nối VPN
                    MessageBox.Show($"Bạn đang kết nối VPN \n\r Trạng thái kết nối VPN: {nic.OperationalStatus}\n\r vui lòng tắt vpn để tiếp tục sử dụng chương trình");
                }
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            //bool checkConn = IsServerConnected(connection);
            checkVPN();
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
            GetImgCaptCha();

        }
    

        public async void GetImgCaptCha()
        {
            try
            {


                HDDT_GOV _gov = new HDDT_GOV();
                try
                {
                    this.captcha = await _gov.GetCaptChaAsync();
                }
                catch (Exception ex)
                {
                   
                    _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                    frmErr frmErr = new frmErr(LogType.Error, this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                    frmErr.ShowDialog();
                    return;
                }
               
                MemoryStream memoryStream = new MemoryStream();
                try
                {
                    // Mở tệp tin để đọc
                    using (FileStream fileStream = new FileStream(this.captcha.PathImage, FileMode.Open, FileAccess.Read))
                    {
                        // Đọc dữ liệu từ tệp tin và sao chép vào MemoryStream
                        fileStream.CopyTo(memoryStream);
                    }

                    // Tạo hình ảnh từ dữ liệu trong MemoryStream
                    Image image = Image.FromStream(memoryStream);

                    // Gán hình ảnh vào PictureBox
                    this.img_Captcha.Image = image;
                    this.img_Captcha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                    // Hiển thị PictureBox (thêm PictureBox vào Form hoặc Control của bạn)
                    // Ví dụ: Form form = new Form(); form.Controls.Add(pictureBox); form.ShowDialog();

                    Console.WriteLine("Hình ảnh đã được hiển thị từ MemoryStream.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
                finally
                {
                    // Đóng MemoryStream khi bạn đã hoàn thành
                    memoryStream.Close();
                }


            }
            catch (Exception ex)
            {
                _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr frmErr = new frmErr(LogType.Error, this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr.ShowDialog();
                return;
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

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
