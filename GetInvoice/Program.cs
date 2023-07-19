using GetInvoice.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetInvoice
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //Application.Run(new frmMain("testzzz","Test Usernam"));

            ReadFileConfig();
            frmLogin fLogin = new frmLogin();
            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new frmMain(fLogin.inUserInfo));
            }
            else
            {
                Application.Exit();
            }

        }
        public static SetupGmailModel setupGmail { get; set; }
        static string _SETUPGMAIL = "SETUPGMAIL.txt";
         static void ReadFileConfig()
        {
            setupGmail = new SetupGmailModel();
            string FilePath = Path.Combine(Environment.CurrentDirectory, _SETUPGMAIL);
            if (File.Exists(FilePath))
            {
                string jsonFromFile = File.ReadAllText(FilePath);
                Program.setupGmail = JsonConvert.DeserializeObject<SetupGmailModel>(jsonFromFile);
               
            }
            
        }

    }


    public class UserInfo
    {
        public string ma_nd { get; set; }
        public string ten_nd { get; set; }
        public string mat_khau { get; set; }
        public string data_source { get; set; }
        public string user_sql { get; set; }
        public string pass_sql { get; set; }
        public string database_name { get; set; }
        public string path_load_file { get; set; }
        public string domain { get; set; }
        public string gmail { get; set; }
        public string password { get; set; }
    }

    //public static class Globals
    //{
    //    public const Int32 BUFFER_SIZE = 512; // Unmodifiable
    //    public static String FILE_NAME = "Output.txt"; // Modifiable
    //    public static readonly String CODE_PREFIX = "US-"; // Unmodifiable
    //}
}
