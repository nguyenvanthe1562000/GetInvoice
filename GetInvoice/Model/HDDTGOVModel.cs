using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetInvoice.Model
{
    //CaptCha hoadondientu.gdt.gov.vn
    public class CaptChaModel
    {
        public string Key { get; set; }
        public string Content { get; set; }//sgv
        public string PathImage { get; set; }

    }
    public class PostAuthenticate
    {
        public string ckey { get; set; }
        public string cvalue { get; set; }
        public string password { get; set; }
        public string username { get; set; }

    }
    public class HDDTRequestHeader
    {
        public Dictionary<string, string> Cookie { get; set; }
        public string Token { get; set; }
    }
    public class HDDTReponseMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string   Token { get; set; }
        public List<string> Messagesfailed { get; set; }
    }
    public class HDDTPurchase
    {
        public int Total { get; set; }
        public string State { get; set; }
        public List<HDDTPurchaseDetail> Datas { get; set; }
    }
    public class HDDTPurchaseDetail
    {
        public string Id { get ; set; }
        public string Mhdon { get; set; }
        public string Mtdtchieu { get; set; }
        public string Nbmst { get; set; }
        public string Khhdon { get; set; }
        public string Khmshdon { get; set; }
        public string Shdon { get; set; }
        public string shthdon { get; set; }
        public DateTime Tdlap { get; set; }


    }
}
