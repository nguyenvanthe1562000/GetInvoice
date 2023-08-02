﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetInvoice
{
    public partial class frmErr : Form
    {
        private readonly FileLogger _logger;
        public frmErr()
        {
            InitializeComponent();

        }
        string fileName;
        string className, methodName, formName;
        int lineNumber;
        public frmErr(LogType type, string formName, string message, StackFrame frame = null, Object logObject = null)
        {
            InitializeComponent();
            _logger = new FileLogger();
            if (frame == null)
            {
                var trace = new StackTrace(true);
                frame = trace.GetFrame(trace.FrameCount - 1);
            }
            fileName = Path.GetFileName(frame?.GetFileName() ?? "")?.Split('.')?.First();
            className = frame.GetMethod().ReflectedType.Name;
            methodName = frame.GetMethod().Name;
            lineNumber = frame.GetFileLineNumber();
            this.formName = formName;

            this.lbl_Mess.Text = message;
            this.rtb_Mess.Text = $"{message} \n " +
                $"Current Instance: {fileName}.({className}) \n" +
                $"Method: {methodName} \n" +
                $"Line: {lineNumber} \n";
            this.Focus();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string _logFilePath = Path.Combine(@"C:\ProgramData\GetInvoice", @"log\log.json");
            // Tạo một đối tượng ProcessStartInfo để cấu hình việc mở ứng dụng
            ProcessStartInfo startInfo = new ProcessStartInfo(_logFilePath);
            // Mở ứng dụng
            Process.Start(startInfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //emailPassword: xem để tạo password https://www.youtube.com/watch?v=6sA1ynYUJzM

                // Cấu hình thông tin email
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string emailFrom = "mylifefake1506@gmail.com";
                string emailPassword = "btkkhflaafyyllqt";

                // Tạo một đối tượng MailMessage
                MailMessage message = new MailMessage();
                message.From = new MailAddress(emailFrom);
                message.To.Add("nguyenvanthe1562000@gmail.com");
                message.Subject = $"Lỗi ứng dụng từ user: {frmMain.local_user.ma_nd} -- {frmMain.local_user.gmail}";
                message.Body =
                $"{message} -- Form Instance: {formName} \n" +
                $"Current Instance: {fileName}.({className}) \n" +
                $"Method: {methodName} \n" +
                $"Line: {lineNumber} \n";

                // Tạo một đối tượng SmtpClient để gửi email
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailFrom, emailPassword);
                smtpClient.EnableSsl = true;

                // Gửi email
                smtpClient.Send(message);

                var s = MessageBox.Show("Đã gửi đến Invoice vui lòng chờ phản hồi trong 30 phút - 2 giờ");
                if (DialogResult.OK == s)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gửi email: " + ex.Message);
            }
        }
    }
}
