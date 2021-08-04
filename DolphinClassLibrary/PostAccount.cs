using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolphin
{
    public class PostAccount
    {
        public int ID { get; set; }
        public int DocID { get; set; }
        public string AccType { get; set; }
        public string AccNo { get; set; }


        //public static void GetLodgementDetails(string docID)
        //{
        //    Dolphin dolphin = new Dolphin();

        //    var res = dolphin.getInfo(dolphin.LodgemntInfo, docID);

        //}


        public static List<PostAccount> GetAccountDetails(string doc_id)
        {
            List<PostAccount> itemList = new List<PostAccount>();
            Dolphin dolphin = new Dolphin();
            
            string postInfo = dolphin.getInfo(dolphin.PostAccount, doc_id);
            postInfo = postInfo?.Replace("\"Note\":", "\"AccType\":");
            postInfo = postInfo?.Replace("\"Post Number\":", "\"AccNo\":");
    
            if (postInfo != null && postInfo != "[]")
            {
                itemList = fastJSON.JSON.ToObject<List<PostAccount>>(postInfo);
            }

            return itemList;
        }
    }
}
