using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Dolphin
{
    public class JobProcess
    {
        public string DocID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public string LinkTo { get; set; }
        public string Description { get; set; }

        public static List<JobProcess> GetProcesses(string doc_id)
        {
            List<JobProcess> processList = new List<JobProcess>();
            DolphinConnector dolphin = new DolphinConnector();
            string response = dolphin.getInfo(dolphin.ProcessInfo, doc_id);
            response = response?.Replace("\"doc_id\":", "\"DocID\":");
            response = response?.Replace("\"qr_id\":", "\"ID\":");
            response = response?.Replace("\"Link To\":", "\"LinkTo\":");

            if (response != null && response != "[]")
            {
                processList = JsonSerializer.Deserialize<List<JobProcess>>(response);
            }
            return processList;
        }
    }
}
