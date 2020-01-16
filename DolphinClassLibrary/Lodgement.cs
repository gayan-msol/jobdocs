using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinLibrary
{
    public class Lodgement
    {
        public int ID { get; set; }
        public int DocID { get; set; }
        public int Qty { get; set; }
        public DateTime Date { get; set; }
        public string SortType { get; set; }
        public int eLMS { get; set; }



        //public static void GetLodgementDetails(string docID)
        //{
        //    Dolphin dolphin = new Dolphin();

        //    var res = dolphin.getInfo(dolphin.LodgemntInfo, docID);

        //}


        public static List<Lodgement> GetLodgementDetails(string doc_id)
        {
            List<Lodgement> itemList = new List<Lodgement>();
            Dolphin dolphin = new Dolphin();
            string response = dolphin.getInfo(dolphin.LodgemntInfo, doc_id);
            response = response?.Replace("\"Qty\":", "\"Qty\":");
            response = response?.Replace("\"elms\":", "\"eLMS\":");
            response = response?.Replace("\"qr_id\":", "\"ID\":");
            response = response?.Replace("\"doc_id\":", "\"DocID\":");

            if (response != null && response != "[]")
            {
                itemList = fastJSON.JSON.ToObject<List<Lodgement>>(response);
            }

            string postInfo = dolphin.getInfo(dolphin.PostAccount, doc_id);
            postInfo = postInfo?.Replace("\"Note\":", "\"AccType\":");
            postInfo = postInfo?.Replace("\"post Account\":", "\"AccNo\":");
     

            if (response != null && response != "[]")
            {
                itemList = fastJSON.JSON.ToObject<List<Lodgement>>(response);
            }



            return itemList;
        }
    }
}
