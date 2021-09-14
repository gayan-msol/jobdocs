using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fastJSON;

namespace Dolphin
{
    public class Job
    {
        public string JobName { get; set; }
        public string Customer { get; set; }
        public string Contact { get; set; } 
        public int Qty { get; set; }
        public string JobNumber { get; set; }
        public string Owner { get; set; }
        public string DocID { get; set; }
        public List<JobProcess> ProcessList { get; set; }
        public List<PrintInfo> PrintInfoList { get; set; }
        public List<MailPackItem> ItemList { get; set; }
        public List<PostAcc> PostAccts { get; set; }
        public List<LodgementLine> LodgementLines { get; set; }
        public string DataFolder { get; set; }
        public string ArtworkFolder { get; set; }

        public static Job GetJob(string jobNo)
        {
            Job job = new Job();
            Dolphin dolphin = new Dolphin();
            string response=dolphin.getInfo(dolphin.JobInfo, jobNo);
            if( response != null && response != "[]")
            {
                response = response.Replace("\"note\":", "\"JobName\":");
                response = response.Replace("\"ent\":", "\"Customer\":");
                response = response.Replace("\"contact\":", "\"Contact\":");
                response = response.Replace("\"usr\":", "\"Owner\":");
                response = response.Replace("\"doc_no\":", "\"JobNumber\":");
                response = response.Replace("\"doc_id\":", "\"DocID\":");
                response = response.Replace("\"data_folder\":", "\"DataFolder\":");
                response = response.Replace("\"artwork_folder\":", "\"ArtworkFolder\":");

                job = fastJSON.JSON.ToObject<List<Job>>(response)[0];
              
            
                job.ProcessList = JobProcess.GetProcesses(job.DocID);
                job.PrintInfoList = PrintInfo.GetInfo(job.DocID);
                job.ItemList = MailPackItem.GetItems(job.DocID);
                job.PostAccts = PostAcc.GetAccounts(job.DocID);
                job.LodgementLines = LodgementLine.GetLodgementLines(job.DocID);

                return job;
            }
            else
            {
                return null;
            }


            

           
        }

    }
}
