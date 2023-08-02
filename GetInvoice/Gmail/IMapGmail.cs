using Aspose.Email;
using Aspose.Email.Clients.Imap;
using Aspose.Email.Tools.Search;
using GetInvoice.Model;
using GmailAPI.APIHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetInvoice.Gmail
{
    public class IMapGmail
    {
        UserInfo local_user;
        SetupGmailModel setupGmail;
        public IMapGmail(UserInfo userInfo)
        {
            this.local_user = userInfo;
            setupGmail = Program.setupGmail;
            if (setupGmail.PathPDF is null)
            {
                throw new Exception("Bạn chưa setup địa chỉ lưu pdf");
            }

            if (userInfo.path_load_file is null)
            {
                throw new Exception("Bạn chưa setup địa chỉ lưu xml");
            }
            
        }
        public async  Task<List<GmailModel>> Start()
        {
            try
            {
                DataTable dtLastImport = utilities.ExeSQL($"select IdEmail, Domain " +
                    $" from f_LogGmail" +
                    $" where Domain = '{local_user.domain}'" +
                    $" group by IdEmail,Domain");

                DataTable dtImport = new DataTable();
                dtImport.TableName = "f_LogGmail";
                dtImport.Columns.Add("IdEmail", typeof(string));
                dtImport.Columns.Add("Domain", typeof(string));

                ImapClient client = new ImapClient(local_user.domain, local_user.gmail, local_user.password);
                client.SelectFolder("Inbox");
                List<GmailModel> EmailList = new List<GmailModel>();
                ImapQueryBuilder builder = new ImapQueryBuilder();
                 
                builder.InternalDate.Since(DateTime.Now
                                    .AddDays(-1*(setupGmail.LoadMailTime>0 ? setupGmail.LoadMailTime:7)));

                //            client.SelectFolder(ImapFolderInfo.InBox);
                var connectionState = client.ConnectionState;
                if(!(connectionState   == Aspose.Email.Clients.ConnectionState.Open))
                {
                    throw new Exception("không kết nối được đến domain hãy kiểm tra lại thông tin setup");
                }    
                MailQuery query = builder.GetQuery();
                PageSettings pageSettings = new PageSettings { AscendingSorting = false };
                ImapMessageInfoCollection messageInfoCol = client.ListMessages(query);
                Console.WriteLine("Đã load xong");
                var test = messageInfoCol.ToList();

                var matchedMessages = messageInfoCol.ToList();


                foreach (var message in setupGmail.FindSubject.Split(','))
                {
                    matchedMessages.RemoveAll(x => !(x.Subject.ToLower().Contains(message.ToLower())));
                }

                if (!(dtLastImport is null))
                {
                    var idList = dtLastImport.AsEnumerable().Select(rows => rows[0]).ToList();
                    matchedMessages.RemoveAll(x => idList.Contains(x.UniqueId));
                }
                foreach (ImapMessageInfo info in matchedMessages)
                {

                    DataRow dataRow = dtImport.NewRow();
                    dataRow[0] = info.UniqueId;
                    dataRow[1] =  local_user.domain;
                    dtImport.Rows.Add(dataRow);
              
                    // Display MIME Message ID
                    Console.WriteLine("Message Id = " + info.MessageId);
                    MailMessage message = client.FetchMessage(info.UniqueId);
                    string body = "";

                    if (message.HtmlBody != null)
                    {
                        body = message.HtmlBody;
                        Console.WriteLine(message.Body);
                    }
                    else if (message.HtmlBodyText != null)
                    {
                        body = message.Body;
                        Console.WriteLine(body);
                    }
                    foreach (Attachment attachment in message.Attachments)
                    {
                        string fileName = attachment.Name;
                        string savePath = "";
                        if (fileName.EndsWith(".xml"))
                        {
                            savePath = Path.Combine(local_user.path_load_file, fileName);
                        }
                        else
                        {
                            savePath = Path.Combine(setupGmail.PathPDF, fileName);
                        }
                        attachment.Save(savePath);
                        Console.WriteLine("Attachment saved: " + savePath);
                    }
                    GmailModel GMail = new GmailModel();
                    GMail.From = message.From.Address;
                    GMail.Body = message.Body;
                    GMail.MailDateTime = message.Date.ToString("dd/MM/yyyy");
                    EmailList.Add(GMail);
                }
                var row = utilities.InsertTable(dtImport);
                return EmailList;
            }
            catch (Exception ex)
            {
               throw new Exception(ex.ToString());
               return null;
            }
        }
        public bool CheckConnection(string Domain, string Gmail, string PassWord)
        {
            ImapClient client = new ImapClient(Domain, Gmail, PassWord);
            var check = client.ConnectionState;
            if(check == Aspose.Email.Clients.ConnectionState.Open)
            {
                return true;
            }    
            else
            { return false;}    
        }
    }
}
