using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class MailPackItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Stream { get; set; }
        public string Category { get; set; }
        public string LinkedTo { get; set; }
        public string Weight { get; set; }
        public bool Return { get; set; }
        public string DocID { get; set; }
        public string SuppliedBy { get; set; }
        public string SupplyDescription { get; set; }



        public static List<MailPackItem> GetItems(string doc_id)
        {
            List<MailPackItem> itemList = new List<MailPackItem>();
            Dolphin dolphin = new Dolphin();
            string response = dolphin.getInfo(dolphin.MailPackItemInfo, doc_id);
            response = response?.Replace("\"Linked To\":", "\"LinkedTo\":");
            response = response?.Replace("\"MP Weight\":", "\"Weight\":");
            response = response?.Replace("\"qr_id\":", "\"ID\":");
            response = response?.Replace("\"doc_id\":", "\"DocID\":");
            response = response?.Replace("\"Return Stock\":", "\"Return\":");
            response = response?.Replace("\"Supplied By\":", "\"SuppliedBy\":");
            response = response?.Replace("\"Supply Description\":", "\"SupplyDescription\":");


            if (response != null && response != "[]")
            {
                itemList = fastJSON.JSON.ToObject<List<MailPackItem>>(response);
            }
            return itemList;
        }
    }

}
