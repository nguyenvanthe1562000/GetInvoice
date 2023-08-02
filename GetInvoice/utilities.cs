using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetInvoice
{
    public class utilities
    {
        public static string NAME_CONNECTION_STRING { get; set; }
        public static bool DEST_IS_CONNECTED { get; set; } = true;
        public static string Encrypt(string plainText)
        {
            string result;
            try
            {
                var bytes = Encoding.ASCII.GetBytes(plainText);
                using (var mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
                {
                    var key = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes("sfdjf48mdfdf3054"));
                    var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider { Key = key, Mode = CipherMode.ECB };
                    var cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
                    var inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                    result = Convert.ToBase64String(inArray);
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
        public static string Decrypt(string encryptedString)
        {
            string result;
            try
            {
                var array = Convert.FromBase64String(encryptedString);
                using (var mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
                {
                    var key = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes("sfdjf48mdfdf3054"));
                    var tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider { Key = key, Mode = CipherMode.ECB };
                    result = Encoding.ASCII.GetString(tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length));
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        public static DataTable ExeSQL_PROC_GET_TABLE(string _proc_name, SqlParameter[] paras)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[NAME_CONNECTION_STRING].ToString()))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format(_proc_name);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(paras);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }

        public static DataTable ExeSQL(string _sqlString)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[NAME_CONNECTION_STRING].ToString()))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format(_sqlString);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
        }

        public static void ExeSQLNonQuery(string _sqlString)
        {
            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[NAME_CONNECTION_STRING].ToString()))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format(_sqlString);
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Message_Box_Exception(ex.Message);
            }
        }

        public static int ExeScalar(string _sqlString)
        {
            try
            {
                int _res = 0;
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[NAME_CONNECTION_STRING].ToString()))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format(_sqlString);
                    cmd.CommandType = CommandType.Text;

                    _res = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return _res;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {

            }
        }

        public static bool SQLCheckTableExists(string _dataSource, string _databaseName, string _userID, string _passSQL, string _table_name)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection cnn;
                string connetionString = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};Connection Timeout=10", _dataSource, _databaseName, _userID, _passSQL);
                cnn = new SqlConnection(connetionString);

                if (cnn.State != ConnectionState.Open && DEST_IS_CONNECTED == true)
                    cnn.Open();
                else
                {
                    DEST_IS_CONNECTED = false;
                    return false;
                }

                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = string.Format("select * from sys.tables where [name] = '{0}'", _table_name);
                cmd.CommandType = CommandType.Text;

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                DEST_IS_CONNECTED = false;
                Message_Box_Exception(ex.Message);
                return false;
            }
        }

        public static void CreateTableSQL(string _dataSource, string _databaseName, string _userID, string _passSQL, string _sqlString)
        {
            try
            {
                string connetionString = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}", _dataSource, _databaseName, _userID, _passSQL);
                using (var conn = new SqlConnection(connetionString))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format(_sqlString);
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Message_Box_Exception(ex.Message);
            }
            finally
            {

            }
        }

        public static int InsertTable(DataTable _datatable)
        {
            try
            {
                string _list_cols = "";
                string _list_cols_var = "";
                string _sqlInsert = "";
                int _res = 0;
                using (var new_conn = new SqlConnection(ConfigurationManager.ConnectionStrings[NAME_CONNECTION_STRING].ToString()))
                {
                    if (new_conn.State != ConnectionState.Open)
                        new_conn.Open();

                    SqlCommand cmdInsert = new_conn.CreateCommand();
                    foreach (DataColumn dc in _datatable.Columns)
                    {
                        if (_datatable.Rows[0][dc].ToString() != "")
                        {
                            _list_cols += dc.ColumnName + ", ";
                            _list_cols_var += "@" + dc.ColumnName + ", ";
                            if (dc.DataType.Name == "DateTime")
                            {
                                SqlParameter parameter = cmdInsert.Parameters.Add("@" + dc.ColumnName, System.Data.SqlDbType.DateTime);
                                parameter.Value = _datatable.Rows[0][dc];
                            }
                            else
                            {
                                cmdInsert.Parameters.AddWithValue("@" + dc.ColumnName, _datatable.Rows[0][dc].ToString());
                            }
                        }
                    }
                    _list_cols = _list_cols.Remove(_list_cols.Length - 2);
                    _list_cols_var = _list_cols_var.Remove(_list_cols_var.Length - 2);
                    _sqlInsert = string.Format("INSERT INTO {0} ({1}) VALUES({2}) SELECT SCOPE_IDENTITY() ", _datatable.TableName, _list_cols, _list_cols_var);
                    cmdInsert.CommandText = string.Format(_sqlInsert);
                    cmdInsert.CommandType = CommandType.Text;
                    _res = (int)cmdInsert.ExecuteScalar();

                }
                return _res;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {

            }
        }

        //public static IEnumerable<string> GetInsertQueryFromDataTable(DataTable dataTable)
        //{
        //    foreach (DataRow row in dataTable.AsEnumerable())
        //    {
        //        var s = new StringBuilder(string.Format("INSERT INTO {0} SET ", dataTable.TableName));

        //        foreach (DataColumn v in dataTable.Columns)
        //        {
        //            var r = new StringBuilder(row[v.ColumnName].ToString());
        //            r.Replace(@"\", @"\\");
        //            r.Replace("\"", "\\\"");
        //            s.AppendFormat("{0}=\"{1}\", ", v.ColumnName, r);
        //        }

        //        s.Remove(s.Length - 2, 1);
        //        s.AppendLine(";");
        //        yield return s.ToString();
        //    }
        //}

        public static string CheckLogin(string _user, string _password)
        {
            try
            {
                DataTable dt = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[NAME_CONNECTION_STRING].ToString()))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format("select * from s_user where ma_nd=@ma_nd and mat_khau=@password");
                    cmd.Parameters.Add("@ma_nd", SqlDbType.NVarChar, 50).Value = _user;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = _password;
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
                if (dt.Rows.Count == 0)
                    return "Tên truy cập hoặc mật khẩu không chính xác";
                else
                {
                    if (dt.Rows[0]["trangthai"].ToString() == "0")
                        return "Người dùng đang bị khóa, liên hệ admin để xử lý";
                    else if (DateTime.Parse(dt.Rows[0]["expired_date"].ToString()) < DateTime.Now)
                        return "Tài khoản người dùng hết hạn sử dụng, liên hệ admin để xử lý";
                    else
                    {
                        string _data_source = dt.Rows[0]["data_source"].ToString();
                        string _database_name = dt.Rows[0]["database_name"].ToString();
                        string _user_id = dt.Rows[0]["user_sql"].ToString();
                        string _pass_sql = dt.Rows[0]["pass_sql"].ToString();

                        if (_data_source != "" && _database_name != "" && _user_id != "" && _pass_sql != "")
                        {
                            if (!SQLCheckTableExists(_data_source, _database_name, _user_id, _pass_sql, "f_hoadon"))
                                CreateTableSQL(_data_source, _database_name, _user_id, _pass_sql, _createHoaDon);

                            if (!SQLCheckTableExists(_data_source, _database_name, _user_id, _pass_sql, "f_hoadon_chitiet"))
                                CreateTableSQL(_data_source, _database_name, _user_id, _pass_sql, _createHoaDonChiTiet);
                            if (!SQLCheckTableExists(_data_source, _database_name, _user_id, _pass_sql, "f_LogGmail"))
                                CreateTableSQL(_data_source, _database_name, _user_id, _pass_sql, _createLogGmail);
                        }
                        else
                        {
                            return "Chưa khai báo chuỗi kết nối của user đăng nhập";
                        }

                        try
                        {
                            System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(dt.Rows[0]["path_load_file"].ToString()).FullName + @"\IMPORTED_SUCCESSFULLY");
                            System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(dt.Rows[0]["path_load_file"].ToString()).FullName + @"\IMPORTED_FAILED");
                        }
                        catch (Exception e)
                        {

                        }


                        return "ok";
                    }

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static void EXEC_PROCEDURE(SqlConnection p_conn, string _proc_name, string _proc_value)
        {
            try
            {
                using (var conn = p_conn)
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = _proc_name;
                    cmd.Parameters.AddWithValue("@para_1", _proc_value);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Message_Box_Exception(ex.Message);
            }
        }

        public static UserInfo getUserInfo(string _user)
        {
            try
            {
                UserInfo resUser = new UserInfo();
                DataTable dt = new DataTable();
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[NAME_CONNECTION_STRING].ToString()))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = string.Format("select ma_nd, ten_nd, trangthai, data_source, database_name, user_sql, pass_sql, expired_date, path_load_file,[gmail],domain,password " +
                        "from s_user where ma_nd=@ma_nd");
                    cmd.Parameters.Add("@ma_nd", SqlDbType.NVarChar, 50).Value = _user;
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    resUser.ma_nd = dt.Rows[0]["ma_nd"].ToString();
                    resUser.ten_nd = dt.Rows[0]["ten_nd"].ToString();
                    resUser.data_source = dt.Rows[0]["data_source"].ToString();
                    resUser.database_name = dt.Rows[0]["database_name"].ToString();
                    resUser.user_sql = dt.Rows[0]["user_sql"].ToString();
                    resUser.pass_sql = dt.Rows[0]["pass_sql"].ToString();
                    resUser.path_load_file = dt.Rows[0]["path_load_file"].ToString();
                    resUser.domain = dt.Rows[0]["domain"].ToString();
                    resUser.password = dt.Rows[0]["password"].ToString();
                    resUser.gmail = dt.Rows[0]["gmail"].ToString();

                }
                return resUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DialogResult Message_Box(string msg)
        {
            return MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Message_Box_Error(string msg)
        {
            return MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult Message_Box_Exception(string msg)
        {
            return MessageBox.Show(msg, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        public class HDon
        {
            public DLHDon DLHDon { get; set; }
        }

        public class DLHDon
        {
            public TTChung TTChung { get; set; }
            public NDHDon NDHDon { get; set; }
            public List<TTin> TTKhac { get; set; }
        }

        public class TTChung
        {
            public string PBan { get; set; }
            public string THDon { get; set; }
            public string KHMSHDon { get; set; }
            public string KHHDon { get; set; }
            public string SHDon { get; set; }
            public string MHSo { get; set; }
            public string NLap { get; set; }
            public string SBKe { get; set; }
            public string NBKe { get; set; }
            public string DVTTe { get; set; }
            public string TGia { get; set; }
            public string HTTToan { get; set; }
            public string MSTTCGP { get; set; }
            public string MSTDVNUNLHDon { get; set; }
            public string TDVNUNLHDon { get; set; }
            public string DCDVNUNLHDon { get; set; }
            public List<TTin> TTKhac { get; set; }
        }

        public class NDHDon
        {
            public NBan NBan { get; set; }
            public NMua NMua { get; set; }
            public List<HHDVu> DSHHDVu { get; set; }
            public TToan TToan { get; set; }
        }

        public class NBan
        {
            public string Ten { get; set; }
            public string MST { get; set; }
            public string DChi { get; set; }
            public string SDThoai { get; set; }
            public string DCTDTu { get; set; }
            public string STKNHang { get; set; }
            public string TNHang { get; set; }
            public string Fax { get; set; }
            public string Website { get; set; }
            public List<TTin> TTKhac { get; set; }
        }

        public class NMua

        {
            public string Ten { get; set; }
            public string MST { get; set; }
            public string DChi { get; set; }
            public string MKHang { get; set; }
            public string SDThoai { get; set; }
            public string HVTNMHang { get; set; }
            public string STKNHang { get; set; }
            public string TNHang { get; set; }
            public List<TTin> TTKhac { get; set; }
        }

        public class DSHHDVu
        {
            public List<HHDVu> HHDVu { get; set; }
        }

        public class HHDVu
        {
            public string TChat { get; set; }
            public int STT { get; set; }
            public string MHHDVu { get; set; }
            public string THHDVu { get; set; }
            public string DVTinh { get; set; }
            public string SLuong { get; set; }
            public string DGia { get; set; }
            public string TLCKhau { get; set; }
            public string STCKhau { get; set; }
            public string ThTien { get; set; }
            public string TSuat { get; set; }
            public List<TTin> TTKhac { get; set; }
        }

        public class TToan
        {
            public List<LTSuat> THTTLTSuat { get; set; }
            public string TgTCThue { get; set; }
            public string TgTThue { get; set; }
            //public string DSLPhi { get; set; }
            public string TTCKTMai { get; set; }
            public string TgTTTBSo { get; set; }
            public string TgTTTBChu { get; set; }
            public List<TTin> TTKhac { get; set; }
        }

        public class TTin
        {
            public string TTruong { get; set; }
            public string KDLieu { get; set; }
            public string DLieu { get; set; }
        }

        public class LTSuat
        {
            public string TSuat { get; set; }
            public string ThTien { get; set; }
            public string TThue { get; set; }
        }

        public static string _createHoaDonChiTiet = @"CREATE TABLE [dbo].[f_hoadon_chitiet](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NOT NULL,
	[stt] [int] NULL,
	[ma_hang] [nvarchar](50) NULL,
	[ten_hang] [nvarchar](250) NULL,
	[dvt] [nvarchar](50) NULL,
	[so_luong] [numeric](18, 5) NULL,
	[don_gia] [numeric](18, 5) NULL,
	[ty_le_chiet_khau] [numeric](18, 5) NULL,
	[so_tien_chiet_khau] [numeric](18, 5) NULL,
	[thanh_tien] [numeric](18, 5) NULL,
	[thue_vat] [nvarchar](50) NULL,
	[tien_thue] [numeric](18, 5) NULL,
	[tong_tien] [numeric](18, 5) NULL,
	[created_at] [date] NULL,
 CONSTRAINT [PK_f_hoadon_chitiet] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)";
        public static string _createHoaDon = @"CREATE TABLE [dbo].[f_hoadon](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ky_hieu_mau_so_hd] [nvarchar](50) NULL,
	[ky_hieu_hd] [nvarchar](50) NULL,
	[so_hd] [nvarchar](50) NULL,
	[ngay_lap] [date] NULL,
	[so_bang_ke] [nvarchar](50) NULL,
	[don_vi_tte] [nvarchar](50) NULL,
	[hinh_thuc_thanh_toan] [nvarchar](50) NULL,
	[ten_nban] [nvarchar](250) NULL,
	[mst_nban] [nvarchar](50) NULL,
	[dia_chi_nban] [nvarchar](250) NULL,
	[sdt_nban] [nvarchar](50) NULL,
	[so_tknh_nban] [nvarchar](50) NULL,
	[ngan_hang_nban] [nvarchar](250) NULL,
	[fax_nban] [nvarchar](50) NULL,
	[website_nban] [nvarchar](50) NULL,
	[ten_nmua] [nvarchar](250) NULL,
	[mst_nmua] [nvarchar](50) NULL,
	[dia_chi_nmua] [nvarchar](250) NULL,
	[sdt_nmua] [nvarchar](50) NULL,
	[so_tknh_nmua] [nvarchar](50) NULL,
	[ngan_hang_nmua] [nvarchar](250) NULL,
	[fax_nmua] [nvarchar](50) NULL,
	[website_nmua] [nvarchar](50) NULL,
	[file_name] [nvarchar](50) NULL,
	[thue_vat] [numeric](18, 5) NULL,
	[tien_thue] [numeric](18, 5) NULL,
	[thanh_tien] [numeric](18, 5) NULL,
	[tong_tien] [numeric](18, 5) NULL,
	[tong_tien_chu] [nvarchar](500) NULL,
	[created_by] [nvarchar](50) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK_f_hoadon] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)";

        public static string _createLogGmail = @"CREATE TABLE [dbo].[f_LogGmail](
	            [IdGmail] [VARCHAR](128) DEFAULT('') NOT NULL,
                [Domain] [VARCHAR](228) DEFAULT('') NOT NULL
            )
      ";
    }
}

