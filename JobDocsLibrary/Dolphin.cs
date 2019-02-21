using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace JobDocsLibrary
{
    public class Dolphin
    {
        public string[] JobInfo = { "jobs", "doc_no" };
        public string[] QuoteInfo = { "quotes", "doc_id" };
        public string[] ProcessInfo = { "process", "doc_id" };
        public string[] LodgemntInfo = { "lodgment", "doc_id" };
        public string[] MailPackItemInfo = { "outwork", "doc_id" };
        public string[] PrintProcessInfo = { "laser_print_options", "doc_id" };

        public Dolphin()
        {
          
        }

        public  string getInfo(string[] paramArr, string queryValue)
        {
            string url = $"http://msol-p1:3000/{paramArr[0]}?{paramArr[1]}=eq.{queryValue}";
            string content = "";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            using (var webResponse = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    content = reader.ReadToEnd();
                }
            }


            return content;
        }
    }
}
