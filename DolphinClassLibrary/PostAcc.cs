using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolphin
{
    public class PostAcc
    {
        public string AccNo { get; set; }
        public string AccName { get; set; }
        public string AccType { get; set; }
        public int DocID { get; set; }

        public static List<PostAcc> GetAccounts(string doc_id)
        {
            List<PostAcc> processList = new List<PostAcc>();
            Dolphin dolphin = new Dolphin();
            string response = dolphin.getInfo(dolphin.PostAccount, doc_id);
            response = response?.Replace("\"Note\":", "\"AccType\":");
            response = response?.Replace("\"Post Number\":", "\"AccNo\":");
            response = response?.Replace("\"doc_id\":", "\"DocID\":");
            response = response?.Replace("\"Customer Name\":", "\"AccName\":");

            if (response != null && response != "[]")
            {
                processList = fastJSON.JSON.ToObject<List<PostAcc>>(response);
            }
            return processList;
        }
    }
}

