using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fastJSON;

namespace JobDocsLibrary
{
    public class Job
    {
        public string JobName { get; set; }
        public string Customer { get; set; }
        public string Qty { get; set; }
        public string JobNumber { get; set; }
        public string Owner { get; set; }
        public string DocID { get; set; }

        public static Job GetJob(string jobNo)
        {
            Job job = new Job();
            Dolphin dolphin = new Dolphin();
            string response=dolphin.getInfo(dolphin.JobInfo, jobNo);
            response = response.Replace("note", "JobName");
            response = response.Replace("ent", "Customer");
            response = response.Replace("usr", "Owner");
            response = response.Replace("doc_no", "JobNumber");
            response = response.Replace("doc_id", "DocID");
            if(response!="[]")
            {
                job = fastJSON.JSON.ToObject<List<Job>>(response)[0];
            }
            

            return job;
        }

    }
}
