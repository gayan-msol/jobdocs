using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace Dolphin
{
    public class DolphinConnector
    {
        public string[] JobInfo = { "jobs", "doc_no" };
        public string[] QuoteInfo = { "quotes", "doc_id" };
        public string[] ProcessInfo = { "process", "doc_id" };
        public string[] LodgemntInfo = { "lodgement", "doc_id" };
        public string[] MailPackItemInfo = { "outwork", "doc_id" };
        public string[] PrintProcessInfo = { "laser_print_options", "doc_id" };
        public string[] PostAccount = { "post_account", "doc_id" };
        public string[] StockRequest = { "stock_request", "doc_id" };

        public DolphinConnector()
        {
          
        }

        public  string getInfo(string[] paramArr, string queryValue, QueryType queryType = QueryType.eq )
        {
            string url = $"http://msol-p1:3000/{paramArr[0]}?{paramArr[1]}={queryType}.{queryValue}";
            string content = "";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            try
            {
                using (var webResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
       
                    

            return content;
        }
    }
}
