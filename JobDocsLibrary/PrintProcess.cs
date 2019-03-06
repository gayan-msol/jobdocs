using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class PrintInfo
    {
        public int ID { get; set; }
        public int ProcessID { get; set; }
        public string  Colour { get; set; }
        public string Sides { get; set; }
        public int Up { get; set; }
        public string PrintSize { get; set; }
        public string FinishedSize { get; set; }
        public int Pages { get; set; }
        public int  Qty { get; set; }

        public static List<PrintInfo> GetInfo(string doc_id)
        {
            List<PrintInfo> processList = new List<PrintInfo>();
            Dolphin dolphin = new Dolphin();
            string response = dolphin.getInfo(dolphin.PrintProcessInfo, doc_id);
            response = response.Replace("Print Size", "PrintSize");
            response = response.Replace("Finished Size", "FinishedSize");
            response = response.Replace("parent_qr_id", "ProcessID");
            response = response.Replace("qr_id", "ID");
            if (response != "[]")
            {
                processList = fastJSON.JSON.ToObject<List<PrintInfo>>(response);

            }
            return processList;
        }
    }
}
