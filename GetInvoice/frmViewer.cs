using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static GetInvoice.utilities;

namespace GetInvoice
{
    public partial class frmViewer : Form
    {
        public frmViewer()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "XML|*.xml";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFileName = openFileDialog1.FileName;
                    txtFileDirectory.Text = selectedFileName;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(HDon));
                HDon objHDon = (HDon)serializer.Deserialize(new XmlTextReader(txtFileDirectory.Text));

                if (objHDon != null)
                {

                    string jsHD = JsonConvert.SerializeObject(objHDon);
                    JToken token = JToken.Parse(jsHD);
                    JToken match = token.SelectToken("DLHDon.TTChung.SHDon");

                    txtSoHD.Text = objHDon.DLHDon.TTChung.SHDon;
                    DateTime dt = DateTime.ParseExact(objHDon.DLHDon.TTChung.NLap, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    txtNgayHD.Text = dt.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    txtNMua_Ten.Text = objHDon.DLHDon.NDHDon.NMua.Ten;
                    txtNMua_MST.Text = objHDon.DLHDon.NDHDon.NMua.MST;
                    txtNMua_DiaChi.Text = objHDon.DLHDon.NDHDon.NMua.DChi;
                    txtNMua_SoTKNH.Text = objHDon.DLHDon.NDHDon.NMua.STKNHang;

                    txtNBan_Ten.Text = objHDon.DLHDon.NDHDon.NBan.Ten;
                    txtNBan_TenDonVi.Text = objHDon.DLHDon.NDHDon.NBan.Ten;
                    txtNBan_MST.Text = objHDon.DLHDon.NDHDon.NBan.MST;
                    txtNBan_DiaChi.Text = objHDon.DLHDon.NDHDon.NBan.DChi;

                    lblTienBangChu.Text = objHDon.DLHDon.NDHDon.TToan.TgTTTBChu;

                    #region Chi tiet hoadon
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(txtFileDirectory.Text);

                    //Chi tiet hoa don
                    DataTable dtMapping_HoaDon_CT = ExeSQL("select * from m_mapping_columns where db_column_name is not null and db_table_name = 'f_hoadon_chitiet'");
                    DataTable dtHoaDon_CT = ExeSQL("select top 0 * from f_hoadon_chitiet");

                    string _hdct_path = "";
                    _hdct_path = dtMapping_HoaDon_CT.Rows[0]["xml_path"].ToString().Split('|')[0];

                    XmlNodeList nodesHDCT = xmldoc.SelectNodes(_hdct_path);
                    foreach (XmlNode node_HDCT in nodesHDCT)
                    {
                        try
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
                        catch (Exception ex)
                        {

                        }
                    }
                    gridChiTietHD.DataSource = dtHoaDon_CT;
                    #endregion
                }
                else
                    Message_Box_Error("Không convert được dữ liệu");
            }
            catch (Exception ex)
            {
                Message_Box_Exception(ex.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
