using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetInvoice.Model
{
    [Serializable]
    public class SetupGmailModel
    {
 
        public bool EnableCal { get; set; }
        public int Timer { get; set; }
        public string FindSubject { get; set; }
        public string PathPDF { get; set; }
        public int LoadMailTime { get; set; }


    }

}
