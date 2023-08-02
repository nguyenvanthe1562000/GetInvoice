using GetInvoice.Gmail;
using GetInvoice.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetInvoice
{
    public partial class frmGmail : Form
    {
        UserInfo local_user { get; set; }
        public frmGmail(UserInfo userInfo )
        {
            InitializeComponent();
            setupGmail = new SetupGmailModel();
            this.local_user = userInfo;
            GetSetUpGmail();
        }
        string _SETUPGMAIL = "SETUPGMAIL.txt";
        SetupGmailModel setupGmail;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void frmGmail_Load(object sender, EventArgs e)
        {

           
            checkNull();

            if(!(setupGmail is null))
            {
                btn_CheckConnectDomain.Enabled = ckb_IsDomain.Checked;
            }    

        }
        public void RemoveText(object sender, EventArgs e)
        {
            if (txt_Gmail.Text == "Enter text here...")
            {
                txt_Gmail.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Gmail.Text))
                txt_Gmail.Text = "Enter text here...";

        }

        private void txt_Gmail_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ckb_EnableCal_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_EnableCal.Checked)
            {
                pn_Timer.Visible = true;
            }
            else
            {
                pn_Timer.Visible = false;
                num_Timer.Value = 0;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveSetUpGmail();
        }

        private void pn_Timer_Paint(object sender, PaintEventArgs e)
        {

        }
        void GetSetUpGmail()
        {
            var s3 = Application.StartupPath;
            String FilePath = Path.Combine(s3, _SETUPGMAIL);
            if (File.Exists(FilePath))
            {
                string jsonFromFile = File.ReadAllText(FilePath);
                this.setupGmail = JsonConvert.DeserializeObject<SetupGmailModel>(jsonFromFile);
                this.txt_Gmail.Text = local_user.gmail;
                this.ckb_IsDomain.Checked = string.IsNullOrEmpty(local_user.domain)? false : true;
                this.txt_PassWord.Text = local_user.password;
                this.txt_Domain.Text = local_user.domain;
                this.ckb_EnableCal.Checked = setupGmail.EnableCal;
                this.num_Timer.Value = setupGmail.Timer;
                this.txt_Subject.Text = setupGmail.FindSubject;
                this.txt_Pdf.Text = setupGmail.PathPDF;
                this.num_LoadMailTime.Value = setupGmail.LoadMailTime;
            }
        }
        void SaveSetUpGmail()
        {
            try
            {
                var dataPath = Application.StartupPath;
                String FilePath = Path.Combine(dataPath, _SETUPGMAIL);
                local_user.gmail = this.txt_Gmail.Text;
                local_user.domain = this.txt_Domain.Text;
                local_user.password = this.txt_PassWord.Text;
                setupGmail.EnableCal = this.ckb_EnableCal.Checked;
                setupGmail.FindSubject = this.txt_Subject.Text;
                setupGmail.Timer = Convert.ToInt32(this.num_Timer.Value);
                setupGmail.PathPDF = this.txt_Pdf.Text;
                setupGmail.LoadMailTime = Convert.ToInt32(this.num_LoadMailTime.Value);
                string json = JsonConvert.SerializeObject(setupGmail);
                File.WriteAllText(FilePath, json);
                utilities.ExeSQL($"update s_user set gmail= '{local_user.gmail}', domain='{local_user.domain}'," +
                    $"password = '{local_user.password}' ");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void ckb_IsDomain_CheckedChanged(object sender, EventArgs e)
        {
          
            if (ckb_IsDomain.Checked)
            {
                this.txt_PassWord.Enabled = true;
                this.txt_Domain.Enabled = true;
                   
            }
            else
            {
                this.txt_PassWord.Enabled = false;
                this.txt_Domain.Enabled = false;
                this.txt_Domain.Text = "";
                this.txt_PassWord.Text = "";
            }
        }

        private void txt_XML_TextChanged(object sender, EventArgs e)
        {
            checkNull();
        }
      

        private void txt_Pdf_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Hiển thị dialog và kiểm tra kết quả khi người dùng chọn thư mục
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Lấy đường dẫn thư mục đã chọn
                string selectedFolder = folderBrowserDialog.SelectedPath;
                this.txt_Pdf.Text = selectedFolder;
            }

        }

        private void txt_Domain_TextChanged(object sender, EventArgs e)
        {
            checkNull();


        }
        void checkNull()
        {
            return;
            if (ckb_IsDomain.Checked)
            {
                if (string.IsNullOrEmpty(this.txt_Domain.Text))
                {
                    this.errorProvider1.SetError(this.txt_Domain, "Chưa khai báo domain");
                }
                else
                {
                    this.errorProvider1.SetError(this.txt_Domain, "");

                }
                if (string.IsNullOrEmpty(this.txt_PassWord.Text))
                {
                    this.errorProvider1.SetError(this.txt_PassWord, "Chưa khai báo PassWord");
                }
                else
                {
                    this.errorProvider1.SetError(this.txt_PassWord, "");

                }

            }
            if (ckb_EnableCal.Checked)
            {
                if (this.num_Timer.Value <= 0)
                {
                    this.errorProvider1.SetError(this.num_Timer, "Chưa khai báo thời gian lặp");
                }
                else
                    this.errorProvider1.SetError(this.num_Timer,"");
            }
            if (string.IsNullOrEmpty(txt_Pdf.Text))
            {
                this.errorProvider1.SetError(this.txt_Pdf, "Chưa khai báo XML");
            }
            
        }

        private void frmGmail_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void txt_Pdf_TextChanged(object sender, EventArgs e)
        {
             
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IMapGmail mapGmail = new IMapGmail(local_user);

            GoogleGmail gmail =new GoogleGmail(local_user);
            var check = false;
            if (string.IsNullOrEmpty(txt_Domain.Text))
            {
                check= gmail.CheckConnect();
            }    
            else
            {
              check = mapGmail.CheckConnection(txt_Domain.Text, txt_Gmail.Text, txt_PassWord.Text);
            }    
            if(check)
            {
                MessageBox.Show("Kết nối thành công", "Thông báo");
            }
            else
                MessageBox.Show("Kết nối không thành công", "Cảnh báo");
        }

        private void txt_PassWord_Validating(object sender, CancelEventArgs e)
        {
            
            validate();
        }
        void validate()
        {

            if (string.IsNullOrEmpty(txt_PassWord.Text.Trim()) && string.IsNullOrEmpty(txt_Domain.Text))
            {
                errorProvider1.SetError(txt_PassWord, null);
                errorProvider1.SetError(txt_Domain, null);
            } 
            else
            {
                if (string.IsNullOrEmpty(txt_PassWord.Text.Trim()) || string.IsNullOrEmpty(txt_Domain.Text))
                {
                    errorProvider1.SetError(txt_PassWord, "Bạn chưa điền PassWord");
                    errorProvider1.SetError(txt_Domain, "Bạn chưa điền Domain");
                }
                else
                {

                    errorProvider1.SetError(txt_PassWord, null);
                    errorProvider1.SetError(txt_Domain, null);

                }
            }
            
        }

        private void txt_Domain_Validating(object sender, CancelEventArgs e)
        {
            validate();

        }
    }
}
