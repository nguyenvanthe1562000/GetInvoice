using GetInvoice.Model;
using Newtonsoft.Json;
using SharpCompress.Archives;
using SharpCompress.Readers;
using Svg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace GetInvoice.Gmail
{


    public class HDDT_GOV
    {
        string url = "https://hoadondientu.gdt.gov.vn:30000";
        SetupGmailModel setupGmail;
        UserInfo _user;

        public HDDT_GOV(UserInfo user = null)
        {
            setupGmail = Program.setupGmail;
            if (setupGmail.PathPDF is null)
            {
                throw new Exception("Bạn chưa setup địa chỉ lưu pdf");
            }
            _user = user;
        }
        public string test()
        {
            return "tesst";
        }
        public async Task<CaptChaModel> GetCaptChaAsync()
        {
            HttpClient getCaptcha = new HttpClient();
            string userArgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
            const SecurityProtocolType tls13 = (SecurityProtocolType)12288;

            ServicePointManager.SecurityProtocol = tls13 | SecurityProtocolType.Tls12;
            getCaptcha.Timeout = TimeSpan.FromSeconds(10);
            HttpResponseMessage response = await getCaptcha.GetAsync(url + "/captcha");

            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                frmMain._HDDTRequest.Cookie = new Dictionary<string, string>();
                foreach (var cookie in cookieValues)
                {
                    var temp = cookie.Split(';');
                    foreach (var item in temp)
                    {
                        var temp2 = item.Split('=');
                        if (temp2.Length > 1)
                        {
                            frmMain._HDDTRequest.Cookie.Add(temp2[0], temp2[1]);
                        }
                    }
                }
            }
            // Phát sinh Exception nếu mã trạng thái trả về là lỗi
            response.EnsureSuccessStatusCode();

            string htmltext = await response.Content.ReadAsStringAsync();

            var capt = JsonConvert.DeserializeObject<CaptChaModel>(htmltext);

            // Đọc nội dung content trả về - ĐỌC CHUỖI NỘI DUNG

            // Đường dẫn tới file SVG và nơi bạn muốn lưu hình ảnh

            string svgFilePath = Path.Combine(System.IO.Path.GetTempPath(), "CaptCha_HDDT.svg");
            string outputImagePath = Path.Combine(System.IO.Path.GetTempPath(), "CaptCha_HDDT.png");

            File.WriteAllText(svgFilePath, capt.Content);
            // Load file SVG
            SvgDocument svgDocument = SvgDocument.Open(svgFilePath);
            // Tạo một hình ảnh bitmap và vẽ SVG lên đó
            Bitmap bitmap = svgDocument.Draw();
            outputImagePath = Path.Combine(System.IO.Path.GetTempPath(), "CaptCha_HDDT.png");


            // Lưu hình ảnh dưới dạng file PNG
            bitmap.Save(outputImagePath, System.Drawing.Imaging.ImageFormat.Png);
            capt.PathImage = outputImagePath;
            // Giải phóng tài nguyên

            bitmap.Dispose();
            return capt;
        }

        public async Task<HDDTReponseMessage> GetToken(string MST, string Pass, string keyCaptCha, string cvalue)
        {
            PostAuthenticate authenticate = new PostAuthenticate();
            authenticate.username = MST;
            authenticate.password = Pass;
            authenticate.ckey = keyCaptCha;
            authenticate.cvalue = cvalue;

            const SecurityProtocolType tls13 = (SecurityProtocolType)12288;
            ServicePointManager.SecurityProtocol = tls13 | SecurityProtocolType.Tls12;

            var handler = new HttpClientHandler();
            var cookieContainer = new CookieContainer();
            handler.CookieContainer = cookieContainer;
            using (var httpClient = new HttpClient(handler))
            {
                foreach (var item in frmMain._HDDTRequest.Cookie)
                {
                    var cookie = new Cookie(item.Key, item.Value);
                    cookieContainer.Add(new Uri(url), cookie);
                    break;
                }

                var json = JsonConvert.SerializeObject(authenticate);
                var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                httpClient.Timeout = TimeSpan.FromSeconds(10);
                var response = await httpClient.PostAsync(this.url + @"/security-taxpayer/authenticate", content);
                if (response.IsSuccessStatusCode)
                {
                    // Request was successful
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<HDDTReponseMessage>(responseBody).Token;
                    return new HDDTReponseMessage() { Success = true, Message = "", Token = token };
                }
                else
                {

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var reponse = JsonConvert.DeserializeObject<HDDTReponseMessage>(responseBody);
                    return new HDDTReponseMessage() { Success = false, Message = reponse.Message, Token = "" };

                }
                // Your request now includes the added cookie
                // Process the response as needed
            }

        }
        public async Task<HDDTReponseMessage> Start(bool isAuto = false)
        {
            var listPurchase = new List<HDDTPurchase>();
            var reponseMessage = new HDDTReponseMessage();
            reponseMessage.Success = true;
            reponseMessage.Messagesfailed = new List<string>();
            var purchase = new HDDTPurchase();
            List<int> _Type_ttxly = new List<int> { 5, 6, 8 };//Kết quả kiểm tra (loại hóa đơn đã cấp mã,chưa cấp mã)
            try
            {
                do
                {


                    purchase = await GetHDDT(purchase.State, _Type_ttxly[0]);
                    if (purchase == null)
                    {
                        listPurchase = new List<HDDTPurchase>();
                        _Type_ttxly.Remove(_Type_ttxly[0]);
                    }
                    else if (purchase.State == null && _Type_ttxly.Count > 0)
                    {
                        _Type_ttxly.Remove(_Type_ttxly[0]);
                    }
                    else if (purchase.State == null && _Type_ttxly.Count == 0)
                    {
                        break;
                    }
                    if (purchase != null)
                    {

                        listPurchase.Add(purchase);
                    }
                } while (purchase.State != null || _Type_ttxly.Count > 0);
                if (listPurchase != null)
                {
                    DataTable dtLastImport = utilities.ExeSQL($"select IdEmail, Domain " +
                        $" from f_LogGmail" +
                        $" where Domain = 'HDDTGOV'" + $" AND created_by='{_user.ma_nd}'" + (isAuto ? "Success = 1 " : " 1 > 1 ") +
                        $" group by IdEmail,Domain");

                    DataTable dtImport = new DataTable();
                    dtImport.TableName = "f_LogGmail";
                    dtImport.Columns.Add("IdEmail", typeof(string));
                    dtImport.Columns.Add("Domain", typeof(string));
                    dtImport.Columns.Add("created_by", typeof(string));
                    dtImport.Columns.Add("Mst_HD", typeof(string));
                    dtImport.Columns.Add("SoHD", typeof(string));
                    dtImport.Columns.Add("Success", typeof(int));
                    dtImport.Columns.Add("CheckFile", typeof(int));
                    dtImport.Columns.Add("Ngay_HD", typeof(DateTime));
                    dtImport.Columns.Add("KHHD", typeof(string));




                    List<HDDTPurchaseDetail> PurchaseDetails = listPurchase.SelectMany(x => x.Datas).ToList();
                    if (!(dtLastImport is null))
                    {

                        var idList = dtLastImport.AsEnumerable().Select(rows => rows[0]).ToList();
                        PurchaseDetails.RemoveAll(x => idList.Contains(x.Id));

                    }
                    foreach (var detail in PurchaseDetails)
                    {


                        DataRow dataRow = dtImport.NewRow();
                        dataRow[0] = detail.Id;
                        dataRow[1] = "HDDTGOV";
                        dataRow[2] = _user.ma_nd;
                        dataRow[3] = detail.Nbmst;
                        dataRow[4] = detail.Shdon;
                        dataRow[7] = detail.Tdlap;
                        dataRow[8] = detail.Khhdon;

                        var okXML = await ExportXml(detail);
                        if (okXML.Success)
                        {
                            dataRow[5] = 1;
                        }
                        else
                        {
                            dataRow[5] = 0;
                            reponseMessage.Messagesfailed.Add(okXML.Message);
                        }

                        var ok = await ExportJSON(detail);
                        if (ok.Success)
                        {
                            dataRow[6] = 1;
                            dtImport.Rows.Add(dataRow);
                        }
                        else if (okXML.Success)
                        {
                            dataRow[6] = 1;
                            dtImport.Rows.Add(dataRow);
                        }
                        else
                        {
                            reponseMessage.Messagesfailed.Add(ok.Message);
                        }

                    }

                    reponseMessage.Message = dtImport.Rows.Count + "/" + PurchaseDetails.Count;
                    var row = utilities.InsertMultiRowTable(dtImport);
                    return reponseMessage;

                }
                else
                {
                    reponseMessage.Success = false;
                }
                return reponseMessage;
            }
            catch (Exception ex)
            {
                return reponseMessage;
            }


        }
        public async Task<HDDTPurchase> GetHDDT(string state = null, int ttxly = 5)
        {
            string token = frmMain._HDDTRequest.Token;
            var handler = new HttpClientHandler();
            var cookieContainer = new CookieContainer();
            handler.CookieContainer = cookieContainer;
            using (var httpClient = new HttpClient(handler))
            {
                foreach (var item in frmMain._HDDTRequest.Cookie)
                {
                    var cookie = new Cookie(item.Key, item.Value);
                    cookieContainer.Add(new Uri(url), cookie);
                    break;
                }

                string formDate = DateTime.Now.AddDays(-1 * (setupGmail.LoadMailTime > 0 ? setupGmail.LoadMailTime : 7)).ToString("dd/MM/yyyyTHH:mm:ss");
                string toDate = DateTime.Now.ToString("dd/MM/yyyyTHH:mm:ss");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string param = "";

                if (string.IsNullOrEmpty(state))
                {
                    param = $"sort=tdlap:desc,khmshdon:asc,shdon:desc&size=50&search=tdlap=ge={formDate};tdlap=le={toDate};ttxly=={ttxly}";
                }
                else
                {
                    param = $"sort=tdlap:desc,khmshdon:asc,shdon:desc&size=50&state={state}&search=tdlap=ge={formDate};tdlap=le={toDate};ttxly=={ttxly}";
                }
                var response = await httpClient.GetAsync(this.url + @"/query/invoices/purchase?" + param);
                if (response.IsSuccessStatusCode)
                {
                    // Request was successful
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var purchase = JsonConvert.DeserializeObject<HDDTPurchase>(responseBody);
                    return purchase;

                }
                // Your request now includes the added cookie
                // Process the response as needed
                return null;
            }
        }

        public async Task<HDDTReponseMessage> ExportXml(HDDTPurchaseDetail purchaseDetail = null)
        {
            try
            {
                string token = frmMain._HDDTRequest.Token;
                var handler = new HttpClientHandler();
                var cookieContainer = new CookieContainer();
                handler.CookieContainer = cookieContainer;
                using (var httpClient = new HttpClient(handler))
                {
                    foreach (var item in frmMain._HDDTRequest.Cookie)
                    {
                        var cookie = new Cookie(item.Key, item.Value);
                        cookieContainer.Add(new Uri(url), cookie);
                        break;
                    }

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var param = $"nbmst={purchaseDetail.Nbmst}&khhdon={purchaseDetail.Khhdon}&shdon={purchaseDetail.Shdon}&khmshdon={purchaseDetail.Khmshdon}";
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = await httpClient.GetStreamAsync(this.url + @"/query/invoices/export-xml?" + param);

                    //var response = await httpClient.GetStreamAsync(@"https://hoadondientu.gdt.gov.vn:30000/query/invoices/export-xml?nbmst=0109043428&khhdon=C23TMV&shdon=5&khmshdon=1");


                    string savePath = Path.Combine(System.IO.Path.GetTempPath(), "invoice_" + purchaseDetail.Id + ".zip");
                    using (FileStream outputFileStream = File.Create(savePath))
                    {
                        await response.CopyToAsync(outputFileStream);
                    }

                    string zipFilePath = savePath;
                    string extractPath = frmMain.local_user.path_load_file;
                    string extracthtmlPath = setupGmail.PathPDF;
                   
                    using (var archive = ArchiveFactory.Open(zipFilePath))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            if (entry.Key.EndsWith(".html"))
                            {
                                string newFileName = purchaseDetail.Nbmst + "_" + purchaseDetail.Shdon + "_" + entry.Key;
                                string destinationPath = Path.Combine(extracthtmlPath, newFileName);
                                entry.WriteToFile(destinationPath, new ExtractionOptions()
                                {
                                    ExtractFullPath = true,
                                    Overwrite = true
                                });
                            }
                        }

                        foreach (var entry in archive.Entries)
                        {
                            if (entry.Key.EndsWith(".html"))
                            {
                                string newFileName = purchaseDetail.Nbmst + "_" + purchaseDetail.Shdon + "_" + entry.Key;
                                string destinationPath = Path.Combine(extracthtmlPath, newFileName);
                                entry.WriteToFile(destinationPath, new ExtractionOptions()
                                {
                                    ExtractFullPath = true,
                                    Overwrite = true
                                });
                            }
                            if (entry.Key.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                            {
                                string newFileName = purchaseDetail.Nbmst + "_" + purchaseDetail.Shdon + "_" + entry.Key;
                                string destinationPath = Path.Combine(extractPath, newFileName);
                                entry.WriteToFile(destinationPath, new ExtractionOptions()
                                {
                                    ExtractFullPath = true,
                                    Overwrite = true
                                });
                                break;
                            }
                        }
                    }
                    File.Delete(zipFilePath);
                    return new HDDTReponseMessage()
                    {
                        Success = true
                    };
                }

            }
            catch (IOException ex)
            {

                return new HDDTReponseMessage() { Success = false, Message = $"lỗi tải hóa đơn MST:{purchaseDetail.Nbmst} - HĐ số {purchaseDetail.Shdon}" + "\r\n" + ex.Message };
            }
            catch (Exception ex)
            {
                return new HDDTReponseMessage() { Success = false, Message = $"lỗi tải hóa đơn MST:{purchaseDetail.Nbmst} - HĐ số {purchaseDetail.Shdon}" + "\r\n" + ex.Message };
            }

        }


        public async Task<HDDTReponseMessage> ExportJSON(HDDTPurchaseDetail purchaseDetail = null)
        {
            try
            {
                string token = frmMain._HDDTRequest.Token;
                var handler = new HttpClientHandler();
                var cookieContainer = new CookieContainer();
                handler.CookieContainer = cookieContainer;
                using (var httpClient = new HttpClient(handler))
                {
                    foreach (var item in frmMain._HDDTRequest.Cookie)
                    {
                        var cookie = new Cookie(item.Key, item.Value);
                        cookieContainer.Add(new Uri(url), cookie);
                        break;
                    }

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var param = $"/query/invoices/detail?nbmst={purchaseDetail.Nbmst}&khhdon={purchaseDetail.Khhdon}&shdon={purchaseDetail.Shdon}&khmshdon={purchaseDetail.Khmshdon}";
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                    var ssss = url + param;
                    var response = await httpClient.GetAsync(url + param);
                    if (response.IsSuccessStatusCode)
                    {
                        // Request was successful
                        var responseBody = await response.Content.ReadAsStringAsync();
                        string newFileName = purchaseDetail.Nbmst + "_" + purchaseDetail.Shdon + "_invoice.json";
                        string destinationPath = Path.Combine(frmMain.local_user.path_load_file, newFileName);
                        File.WriteAllText(destinationPath, responseBody);
                    }

                    //var response = await httpClient.GetStreamAsync(@"https://hoadondientu.gdt.gov.vn:30000/query/invoices/detail?nbmst=5700101690-002&khhdon=K23TYL&shdon=353431&khmshdon=1");


                    return new HDDTReponseMessage()
                    {
                        Success = true
                    };
                }

            }
            catch (IOException ex)
            {

                return new HDDTReponseMessage() { Success = false, Message = $"lỗi tải hóa đơn MST:{purchaseDetail.Nbmst} - HĐ số {purchaseDetail.Shdon}" + "\r\n" + ex.Message };
            }
            catch (Exception ex)
            {
                return new HDDTReponseMessage() { Success = false, Message = $"lỗi tải hóa đơn MST:{purchaseDetail.Nbmst} - HĐ số {purchaseDetail.Shdon}" + "\r\n" + ex.Message };
            }

        }

    }
}
