using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using GetInvoice.Gmail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static GetInvoice.utilities;

namespace GetInvoice
{
    public partial class frmMain : Form
    {
        public static UserInfo local_user;
        private string _DATABASE_STRING = "118.70.81.95,58386";
        private string USERNAME = "";
        private string FULLNAME = "";
        private string _NAME_CONNECTION_STRING = "ABC_8386";
        private string _DATABASE_NAME = "GETINVOICE";

        private string _DATASOURCE_STRING_DEST = "";
        private string _DATABASE_NAME_DEST = "";
        private string _USERID_DEST = "";
        private string _PASSWORD_DEST = "";
        bool _checkTaskMailMess = false;
        private readonly FileLogger _logger;  
        //xử lý tự động chạy
        private BackgroundWorker backgroundWorker;
  
        //public frmMain(string _userName, string _fullName)
        public frmMain(UserInfo user_info)
        {
            InitializeComponent();
            _logger = new FileLogger();

            USERNAME = user_info.ma_nd;
            FULLNAME = user_info.ten_nd;
            _DATASOURCE_STRING_DEST = user_info.data_source;
            _DATABASE_NAME_DEST = user_info.database_name;
            _USERID_DEST = user_info.user_sql;
            _PASSWORD_DEST = user_info.pass_sql;
            local_user = user_info;
            lblPathFile.Visible = true;
            lblPathFile.Text = user_info.path_load_file == "" ? "" : user_info.path_load_file;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();

        }

        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                while (!string.IsNullOrEmpty(local_user.domain))
                {
                    // Run your task here
                    await AutoReadGmail();

                    // Delay for the specified sleep duration
                    await Task.Delay(TimeSpan.FromMinutes(Program.setupGmail.Timer));
                }
            }
            catch (Exception ex)
            {
                if (!Program.setupGmail.EnableCal)
                {
                    _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                    frmErr frmErr = new frmErr(LogType.Error, this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                    frmErr.ShowDialog();
                }    
                return;
            }
            
        }

        private async Task AutoReadGmail()
        {
            var resutl = await Task.Run(async () =>
            {
                if (!(string.IsNullOrEmpty(local_user.domain)))
                {
                    IMapGmail mapGmail = new IMapGmail(local_user);
                    var result = await mapGmail.Start();
                    return result;
                }
                else
                {
                    GoogleGmail gmail = new GoogleGmail(local_user);
                    return await gmail.Star();
                }
            });
            if (!(resutl is null))
            {
                  this.tslbl_Status.Text = $"Có {resutl.Count} được tải về |";
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
                tslblMail.Text = local_user.gmail;
                tslblUserName.Text = FULLNAME + " (" + USERNAME + ")";
                tslblDataSource.Text = "Data source: " + _DATABASE_STRING;
                tslblDatabase.Text = "Database name: " + _DATABASE_NAME;
                LoadGrid(USERNAME);
            }
            catch (Exception ex)
            {
                _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr frmErr = new frmErr(LogType.Error,this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr.ShowDialog();
                return;
            }
          
        }

        private void tsbtnSync_Click(object sender, EventArgs e)
        {
            try
            {
                bool _is_exists_file = true;
                //string _upload_path = System.Configuration.ConfigurationManager.AppSettings["Upload_Path"];
                string _upload_path = local_user.path_load_file;
                if (_upload_path == "")
                {
                    Message_Box_Error("(Chưa thiết lập) Bạn chưa thiết lập thư mục chứa file xml");
                    return;
                }
                DirectoryInfo d = new DirectoryInfo(_upload_path);

                FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files
                string _msg = "";
                //Xu ly last imported
                ExeSQLNonQuery(string.Format("update f_hoadon set last_imported = 0 where created_by = '{0}'", USERNAME));
                foreach (FileInfo file in Files)
                {
                    if (file.Name.StartsWith("sync_"))
                    {

                    }
                    else
                    {
                        _msg = InsertDBWithFileName(string.Format(@"{0}/{1}", _upload_path, file.Name));
                        _is_exists_file = false;
                        if (_msg != "ok")
                        {
                            DialogResult result = MessageBox.Show(file.Name + Environment.NewLine + "Lỗi: " + _msg + Environment.NewLine + "Bạn có muốn tiếp tục tiến trình ?", "Lỗi", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                File.Move(string.Format(@"{0}/{1}", _upload_path, file.Name), string.Format(@"{0}{1}", System.IO.Directory.GetParent(_upload_path).FullName + @"\IMPORTED_FAILED\", file.Name));
                            }
                            else if (result == DialogResult.No)
                            {
                                File.Move(string.Format(@"{0}/{1}", _upload_path, file.Name), string.Format(@"{0}{1}", System.IO.Directory.GetParent(_upload_path).FullName + @"\IMPORTED_FAILED\", file.Name));
                                break;
                            }
                        }
                        else
                        {
                            File.Move(string.Format(@"{0}/{1}", _upload_path, file.Name), string.Format(@"{0}{1}", System.IO.Directory.GetParent(_upload_path).FullName + @"\IMPORTED_SUCCESSFULLY\", file.Name));
                        }
                    }
                }
                if (_is_exists_file)
                {
                    Message_Box_Error("Các file xml trong thư mục đã được đồng bộ hết");
                    return;
                }
                if (_msg == "ok")
                {
                    Message_Box("Đã thêm dữ liệu thành công");
                    Load_LastImport(USERNAME);
                }
            }
            catch (Exception ex)
            {
                
                _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr frmErr = new frmErr(LogType.Error, this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr.ShowDialog();
            }
        }

        private void tsbtnViewer_Click(object sender, EventArgs e)
        {
            frmViewer frm = new frmViewer();
            frm.ShowDialog();
        }

        #region InsertDBWithFileName
        private string InsertDBWithFileName(string xmlFile)
        {
            try
            {
                string _res = "";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlFile);

                

                //Hoa don
                DataTable dtMapping_HoaDon = ExeSQL("select * from m_mapping_columns where db_column_name is not null and db_table_name = 'f_hoadon'");
                DataTable dtHoaDon = ExeSQL("select top 0 * from f_hoadon");
                DataRow dr = dtHoaDon.NewRow();

                foreach (DataRow row in dtMapping_HoaDon.Rows)
                {
                    if (row["db_column_name"] != null && row["xml_path"] != null && xmldoc.SelectSingleNode(row["xml_path"].ToString()) != null)
                        dr[row["db_column_name"].ToString()] = xmldoc.SelectSingleNode(row["xml_path"].ToString()).InnerText;
                }
                dr["created_by"] = local_user.ma_nd;
                dtHoaDon.Rows.Add(dr);

                //DataTable dtHoaDon = DataTableFromXmlFile(xmlFile);

                //Chi tiet hoa don
                DataTable dtMapping_HoaDon_CT = ExeSQL("select * from m_mapping_columns where db_column_name is not null and db_table_name = 'f_hoadon_chitiet'");
                DataTable dtHoaDon_CT = ExeSQL("select top 0 * from f_hoadon_chitiet");

                string _hdct_path = "";
                _hdct_path = dtMapping_HoaDon_CT.Rows[0]["xml_path"].ToString().Split('|')[0];

                XmlNodeList nodesHDCT = xmldoc.SelectNodes(_hdct_path);
                foreach (XmlNode node_HDCT in nodesHDCT)
                {
                    DataRow drHDCT = dtHoaDon_CT.NewRow();
                    foreach (DataRow row in dtMapping_HoaDon_CT.Rows)
                    {
                        if (row["db_column_name"] != null && row["xml_path"] != null && row["xml_path"].ToString().Split('|').Count() == 2)
                        {
                            if (node_HDCT[row["xml_path"].ToString().Split('|')[1]] != null)
                                drHDCT[row["db_column_name"].ToString()] = node_HDCT[row["xml_path"].ToString().Split('|')[1]].InnerText;
                        }
                    }
                    dtHoaDon_CT.Rows.Add(drHDCT);
                }


                string _list_cols = "";
                string _list_cols_var = "";
                string _sqlInsert = "";

                using (var new_conn = new SqlConnection(ConfigurationManager.ConnectionStrings[_NAME_CONNECTION_STRING].ToString()))
                {
                    if (new_conn.State != ConnectionState.Open)
                        new_conn.Open();

                    SqlCommand cmdInsert = new_conn.CreateCommand();
                    foreach (DataColumn dc in dtHoaDon.Columns)
                    {
                        if (dtHoaDon.Rows[0][dc].ToString() != "")
                        {
                            _list_cols += dc.ColumnName + ", ";
                            _list_cols_var += "@" + dc.ColumnName + ", ";
                            if (dc.DataType.Name == "DateTime")
                            {
                                SqlParameter parameter = cmdInsert.Parameters.Add("@" + dc.ColumnName, System.Data.SqlDbType.DateTime);
                                parameter.Value = dtHoaDon.Rows[0][dc];
                            }
                            else
                            {
                                cmdInsert.Parameters.AddWithValue("@" + dc.ColumnName, dtHoaDon.Rows[0][dc].ToString());
                            }
                        }
                    }
                    _list_cols = _list_cols.Remove(_list_cols.Length - 2);
                    _list_cols_var = _list_cols_var.Remove(_list_cols_var.Length - 2);
                    _sqlInsert = string.Format("INSERT INTO f_hoadon ({0}) VALUES({1}) SELECT SCOPE_IDENTITY() ", _list_cols, _list_cols_var);
                    cmdInsert.CommandText = string.Format(_sqlInsert);
                    cmdInsert.CommandType = CommandType.Text;
                    string outParentId = cmdInsert.ExecuteScalar().ToString();

                    using (var adapterCT = new SqlDataAdapter("SELECT * FROM f_hoadon_chitiet", new_conn))
                    using (var builderCT = new SqlCommandBuilder(adapterCT))
                    {
                        foreach (DataRow iDr in dtHoaDon_CT.Rows)
                        {
                            iDr["parent_id"] = outParentId;
                        }
                        adapterCT.InsertCommand = builderCT.GetInsertCommand();
                        adapterCT.Update(dtHoaDon_CT);
                    }

                    //Insert into database destination
                    string conn_string_dest = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}", _DATASOURCE_STRING_DEST, _DATABASE_NAME_DEST, _USERID_DEST, _PASSWORD_DEST);
                    using (var conn_dest = new SqlConnection(conn_string_dest))
                    {
                        if (conn_dest.State != ConnectionState.Open)
                            conn_dest.Open();

                        cmdInsert.Connection = conn_dest;
                        string outParentId_dest = cmdInsert.ExecuteScalar().ToString();

                        var sqlQuery = "select * from f_hoadon_chitiet where 0 = 1";
                        var dataAdapter = new SqlDataAdapter(sqlQuery, conn_dest);
                        var ds = new DataSet();
                        dataAdapter.Fill(ds);


                        foreach (DataRow iDr in dtHoaDon_CT.Rows)
                        {
                            var desRow = ds.Tables[0].NewRow();
                            desRow.ItemArray = iDr.ItemArray.Clone() as object[];
                            desRow["parent_id"] = outParentId_dest;
                            ds.Tables[0].Rows.Add(desRow);
                        }

                        new SqlCommandBuilder(dataAdapter);
                        dataAdapter.Update(ds);

                        //EXEC_PROCEDURE
                        EXEC_PROCEDURE(conn_dest, "sp_execute_after_sync", DateTime.Now.ToString("yyyy-MM-dd"));


                        _res = "ok";
                    }
                }

                return _res;
            }
            catch (Exception ex)
            {

                

                if (ex.Message.Contains("Cannot insert duplicate key in object 'dbo.f_hoadon'"))
                {
                   
                    _logger.Log(LogType.Error, "Hóa đơn tồn tại trong cơ sở dữ liệu", new StackTrace(ex, true).GetFrames().Last());
                    frmErr frmErr = new frmErr(LogType.Error, this.Text, "Hóa đơn tồn tại trong cơ sở dữ liệu", new StackTrace(ex, true).GetFrames().Last());
                    frmErr.ShowDialog();
                   
                }
                else
                {
                    _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                    frmErr frmErr = new frmErr(LogType.Error, this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                    frmErr.ShowDialog();
                    
                }
                return "";
            }
        }
        #endregion

        private DataTable DataTableFromXmlFile(string xmlFile)
        {
            try
            {
                string _res = "";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlFile);

                //Hoa don
                DataTable dtMapping_HoaDon = ExeSQL("select * from m_mapping_columns where db_column_name is not null and db_table_name = 'f_hoadon'");
                DataTable dtHoaDon = ExeSQL("select top 0 * from f_hoadon");
                DataRow dr = dtHoaDon.NewRow();


                XmlSerializer serializer = new XmlSerializer(typeof(HDon));
                HDon objHDon = (HDon)serializer.Deserialize(new XmlTextReader(xmlFile));

                if (objHDon != null)
                {

                    string jsHD = JsonConvert.SerializeObject(objHDon);
                    JToken token = JToken.Parse(jsHD);
                    JToken match = token.SelectToken("DLHDon.TTChung.SHDon");

                    foreach (DataRow row in dtMapping_HoaDon.Rows)
                    {
                        if (row["db_column_name"] != null && row["xml_path_json"] != null && token.SelectToken(row["xml_path_json"].ToString().Replace("/", ".")).ToString() != "")
                            dr[row["db_column_name"].ToString()] = token.SelectToken(row["xml_path_json"].ToString().Replace("/", ".")).ToString();
                    }
                    dtHoaDon.Rows.Add(dr);
                }
                return dtHoaDon;
            }
            catch (Exception ex)
            {
                _logger.Log(LogType.Error, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr frmErr = new frmErr(LogType.Error, this.Text, ex.Message, new StackTrace(ex, true).GetFrames().Last());
                frmErr.ShowDialog();
                return null;
            }
        }

        private void LoadGrid(string _ma_nd)
        {
            DataTable dt = ExeSQL(string.Format("select top 1000 * from f_hoadon where created_by = '{0}' order by id desc", _ma_nd));

            GridColumn colso_luong = grvDanhSachHoaDon.Columns["tong_tien"];
            colso_luong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colso_luong.DisplayFormat.FormatString = "n2";

            GridColumn coldon_gia = grvDanhSachHoaDon.Columns["tien_thue"];
            coldon_gia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            coldon_gia.DisplayFormat.FormatString = "n2";

            GridColumn colThanh_tien = grvDanhSachHoaDon.Columns["thanh_tien"];
            colThanh_tien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colThanh_tien.DisplayFormat.FormatString = "n0";

            gridDanhSachHoaDon.DataSource = dt;
            grvDanhSachHoaDon.BestFitColumns();
        }


        private void LoadGrid_HoaDonChiTiet(int _parent_id)
        {
            DataTable dtHHDV = ExeSQL(string.Format("select * from f_hoadon_chitiet where parent_id = {0}", _parent_id));

            GridColumn colso_luong = grvHHDV.Columns["so_luong"];
            colso_luong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colso_luong.DisplayFormat.FormatString = "n2";

            GridColumn coldon_gia = grvHHDV.Columns["don_gia"];
            coldon_gia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            coldon_gia.DisplayFormat.FormatString = "n2";

            GridColumn colThanh_tien = grvHHDV.Columns["thanh_tien"];
            colThanh_tien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colThanh_tien.DisplayFormat.FormatString = "n0";

            colNgay_CN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colNgay_CN.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

            gridHHDV.DataSource = dtHHDV;
            grvHHDV.BestFitColumns();
        }

        private void tsbtnExportExcel_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
            saveDlg.InitialDirectory = @"C:\";
            saveDlg.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveDlg.FilterIndex = 0;
            saveDlg.RestoreDirectory = true;
            saveDlg.Title = "Export Excel File To";
            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@_ma_nd", local_user.ma_nd)
                };
                var arrayParas = parameters.ToArray();
                DataTable dtBK = ExeSQL_PROC_GET_TABLE("sp_BangKeHoaDon", arrayParas);

                GridColumn coltong_tien = grvBangKeHoaDon.Columns["tong_tien"];
                coltong_tien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                coltong_tien.DisplayFormat.FormatString = "n2";

                GridColumn coltien_thue = grvBangKeHoaDon.Columns["tien_thue"];
                coltien_thue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                coltien_thue.DisplayFormat.FormatString = "n2";

                GridColumn colThanh_tien = grvBangKeHoaDon.Columns["thanh_tien"];
                colThanh_tien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colThanh_tien.DisplayFormat.FormatString = "n0";

                GridColumn colso_luong = grvBangKeHoaDon.Columns["so_luong"];
                colso_luong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                colso_luong.DisplayFormat.FormatString = "n2";

                GridColumn coldon_gia = grvBangKeHoaDon.Columns["don_gia"];
                coldon_gia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                coldon_gia.DisplayFormat.FormatString = "n2";

                gridBangKeHoaDon.DataSource = dtBK;
                grvBangKeHoaDon.BestFitColumns();

                string path = saveDlg.FileName;
                gridBangKeHoaDon.ExportToXlsx(path);
                Process.Start(path);
            }
        }

        private void grvDanhSachHoaDon_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvDanhSachHoaDon.FocusedRowHandle >= 0)
            {
                txtKyHieuHD.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "ky_hieu_hd").ToString();
                txtSoHD.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "so_hd").ToString();
                txtNgayLap.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "ngay_lap").ToString();
                txtHinhThucThanhToan.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "hinh_thuc_thanh_toan").ToString();
                txtTienHang.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "thanh_tien").ToString();
                txtThueGTGT.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "tien_thue").ToString();
                txtTongTien.Text = String.Format("{0:n0}", grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "tong_tien").ToString());

                txtThueGTGT.Properties.DisplayFormat.FormatString = "n0";
                txtTienHang.Properties.DisplayFormat.FormatString = "n0";


                txtTen_NBan.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "ten_nban").ToString();
                txtSTK_NBan.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "so_tknh_nban").ToString();
                txtMST_NBan.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "mst_nban").ToString();
                txtDiaChi_NBan.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "dia_chi_nban").ToString();

                txtTen_NMua.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "ten_nmua").ToString();
                txtDiaChi_NMua.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "dia_chi_nmua").ToString();
                txtMST_NMua.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "mst_nmua").ToString();
                txtTen_Dvi_Mua.Text = grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "ten_nmua").ToString();

                int _parentId = Convert.ToInt32(grvDanhSachHoaDon.GetRowCellValue(grvDanhSachHoaDon.FocusedRowHandle, "id"));
                if (_parentId > 0)
                {
                    LoadGrid_HoaDonChiTiet(_parentId);
                }
            }
        }

        private void tsbtnLoad_Click(object sender, EventArgs e)
        {
            LoadGrid(USERNAME);
        }

        private void tsbtnDefineXML_Click(object sender, EventArgs e)
        {
            frmMappingColumn frm = new frmMappingColumn();
            frm.ShowDialog();
        }

        private void tsbtnTest_Click(object sender, EventArgs e)
        {
            string parent = System.IO.Directory.GetParent(lblPathFile.Text).FullName;
            MessageBox.Show(parent);
        }

        private void lblPathFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSetupPathFile frm = new frmSetupPathFile(local_user);
            frm.ShowDialog();
            local_user = getUserInfo(USERNAME);
            lblPathFile.Text = local_user.path_load_file;

            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(local_user.path_load_file) + @"\IMPORTED_SUCCESSFULLY");
                System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(local_user.path_load_file) + @"\IMPORTED_FAILED");
            }
            catch (Exception ez)
            {

            }
        }

        private void tsbtnLayDuLieuImport_Click(object sender, EventArgs e)
        {
            Load_LastImport(USERNAME);
        }

        private void Load_LastImport(string _ma_nd)
        {
            DataTable dtLastImport = ExeSQL(string.Format("select top 100 * from f_hoadon where last_imported = 1 and created_by = '{0}' order by id desc", _ma_nd));

            GridColumn colso_luong = grvDanhSachHoaDon.Columns["tong_tien"];
            colso_luong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colso_luong.DisplayFormat.FormatString = "n2";

            GridColumn coldon_gia = grvDanhSachHoaDon.Columns["tien_thue"];
            coldon_gia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            coldon_gia.DisplayFormat.FormatString = "n2";

            GridColumn colThanh_tien = grvDanhSachHoaDon.Columns["thanh_tien"];
            colThanh_tien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colThanh_tien.DisplayFormat.FormatString = "n0";

            gridDanhSachHoaDon.DataSource = dtLastImport;
            grvDanhSachHoaDon.BestFitColumns();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void test23ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setupEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGmail frmGmail = new frmGmail(local_user);
            frmGmail.StartPosition = FormStartPosition.CenterScreen;
            frmGmail.ShowDialog();
            TimeSpan.FromMinutes(Program.setupGmail.Timer);
        }

        private async void tsbtnDownloadMail_Click(object sender, EventArgs e)
        {

                     
            var resutl = await Task.Run(async () =>
            {
                if (!(string.IsNullOrEmpty(local_user.domain)))
                {
                    IMapGmail mapGmail = new IMapGmail(local_user);
                    var result= await mapGmail.Start();
                    return result;
                }    
                else
                {
                    GoogleGmail gmail = new GoogleGmail(local_user);
                     
                    return await gmail.Star();
              
                }    
           
            });
        if ((resutl is null))
            {
                MessageBox.Show("không có mail nào đc đọc");
            }    
        else
            {
                MessageBox.Show($"có tổng số {resutl.Count} mail hóa đơn");
            }

            // await Task.Delay(10000);

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsbtnChangePassword_Click(object sender, EventArgs e)
        {

        }
       
    }
}
