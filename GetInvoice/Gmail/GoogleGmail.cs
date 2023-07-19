using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetInvoice.Model;
using GmailAPI.APIHelper;
using GmailAPI.APIHelper;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Newtonsoft.Json;

namespace GetInvoice.Gmail
{
    public class GoogleGmail
    {
        UserInfo local_user;
        SetupGmailModel setupGmail;
        public GoogleGmail(UserInfo userInfo)
        {
            this.local_user = userInfo;
            setupGmail = Program.setupGmail;
            if (setupGmail.PathPDF is null)
            {
                throw new Exception("Bạn chưa setup địa chỉ lưu pdf");
            }

            if (local_user.path_load_file is null)
            {
                throw new Exception("Bạn chưa setup địa chỉ lưu xml");
            }
        }

        public async Task<List<GmailModel>> Star()
        {
            
            List<GmailModel> MailLists = await GetAllEmails(local_user.gmail);
            return MailLists;
        }
        public bool CheckConnect()
        {
            try
            {
                GmailService GmailService = GmailAPIHelper.GetService();
                if  (!(GmailService is null))
                {
                    return true;
                }
                return false;   
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kết nối gmail \n {new StackTrace(ex, true).GetFrames().Last()}");
                return false;
            }
        }
        public  async Task<List<GmailAPI.APIHelper.GmailModel>> GetAllEmails(string HostEmailAddress)
        {
            try
            {
                DataTable dtLastImport = utilities.ExeSQL($"select IdEmail from f_LogGmail group by IdEmail" );
                
                DataTable dtImport = new DataTable();
                dtImport.TableName = "f_LogGmail";
                dtLastImport.Columns.Add("IdEmail", typeof(string));

                GmailService GmailService = GmailAPIHelper.GetService();
                List<GmailModel> EmailList = new List<GmailModel>();
                UsersResource.MessagesResource.ListRequest ListRequest = GmailService.Users.Messages.List(HostEmailAddress);

                ListRequest.LabelIds = "INBOX";
                ListRequest.IncludeSpamTrash = false;
                string subjectQuery = "";
                foreach (var item in setupGmail.FindSubject.Split(','))
                {
                    subjectQuery = $"subject:{item} AND";
                }

                    ListRequest.Q = subjectQuery;
                //GET ALL EMAILS
                ListMessagesResponse ListResponse = ListRequest.Execute();
                // Xóa các email đã được đánh dấu đã đọc và đc import vào Server
                if (dtLastImport.Rows.Count > 0)
                {
                    var idList = dtLastImport.AsEnumerable().Select(rows => rows[0]).ToList();
                       ListResponse.Messages.ToList().RemoveAll(x=> idList.Contains(x.Id));
                }

                if (ListResponse != null && ListResponse.Messages != null)
                {
                    Console.ReadLine();
                    //LOOP THROUGH EACH EMAIL AND GET WHAT FIELDS I WANT
                    foreach (Message Msg in ListResponse.Messages)
                    {

                        DataRow dataRow = dtImport.NewRow();
                        dataRow[0] = Msg.Id;
                        dtImport.Rows.Add(dataRow);
                    
                        //MESSAGE MARKS AS READ AFTER READING MESSAGE
                        GmailAPIHelper.MsgMarkAsRead(HostEmailAddress, Msg.Id);

                        UsersResource.MessagesResource.GetRequest Message = GmailService.Users.Messages.Get(HostEmailAddress, Msg.Id);
                        Console.WriteLine("\n-----------------NEW MAIL----------------------");
                        Console.WriteLine("STEP-1: Message ID:" + Msg.Id);

                        //MAKE ANOTHER REQUEST FOR THAT EMAIL ID...
                        Message MsgContent = Message.Execute();

                        if (MsgContent != null)
                        {
                            string FromAddress = string.Empty;
                            string Date = string.Empty;
                            string Subject = string.Empty;
                            string MailBody = string.Empty;
                            string ReadableText = string.Empty;
                            string MsgId = string.Empty ;

                            //LOOP THROUGH THE HEADERS AND GET THE FIELDS WE NEED (SUBJECT, MAIL)
                            foreach (var MessageParts in MsgContent.Payload.Headers)
                            {
                                if (MessageParts.Name == "From")
                                {
                                    FromAddress = MessageParts.Value;
                                    Console.WriteLine($"{FromAddress}");
                                }
                                else if (MessageParts.Name == "Date")
                                {
                                    Date = MessageParts.Value;
                                }
                                else if (MessageParts.Name == "Subject")
                                {
                                    Subject = MessageParts.Value;
                                }

                                //Console.WriteLine($"=========================================");
                            }
                            Console.WriteLine($"{FromAddress}---{Date}---{Subject}");

                            // READ MAIL BODY
                            Console.WriteLine("STEP-2: Read Mail Body");
                            List<string> FileName = GmailAPIHelper.GetAttachments(HostEmailAddress, Msg.Id, local_user.path_load_file, setupGmail.PathPDF);
                            if (FileName.Count() > 0)
                            {

                                foreach (var EachFile in FileName)
                                {
                                    Console.WriteLine("FileName : " + EachFile);
                                    //GET USER ID USING FROM EMAIL ADDRESS-------------------------------------------------------
                                    string[] RectifyFromAddress = FromAddress.Split(' ');
                                    string FromAdd = RectifyFromAddress[RectifyFromAddress.Length - 1];

                                    if (!string.IsNullOrEmpty(FromAdd))
                                    {
                                        FromAdd = FromAdd.Replace("<", string.Empty);
                                        FromAdd = FromAdd.Replace(">", string.Empty);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("STEP-3: Mail has no attachments.");
                            }

                            //READ MAIL BODY-------------------------------------------------------------------------------------
                            MailBody = string.Empty;
                            if (MsgContent.Payload.Parts == null && MsgContent.Payload.Body != null)
                            {
                                MailBody = MsgContent.Payload.Body.Data;
                            }
                            else
                            {
                                MailBody = GmailAPIHelper.MsgNestedParts(MsgContent.Payload.Parts);
                            }

                            //BASE64 TO READABLE TEXT--------------------------------------------------------------------------------
                            ReadableText = string.Empty;
                            if (!string.IsNullOrEmpty(MailBody))
                            { ReadableText = GmailAPIHelper.Base64Decode(MailBody); }
                            Console.WriteLine($"{ReadableText}");
                            Console.WriteLine($"=========================================");
                            Console.WriteLine("STEP-4: Identifying & Configure Mails.");

                            if (!string.IsNullOrEmpty(ReadableText))
                            {
                                GmailModel GMail = new GmailModel();
                                GMail.From = FromAddress;
                                GMail.Body = ReadableText;
                                GMail.MailDateTime = Date;
                                GMail.MsgID = MsgId;
                                EmailList.Add(GMail);
                            }
                        }

                    }
                    var row = utilities.InsertTable(dtImport);
                }
                return EmailList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                return null;
            }
        }

        
    }
}
