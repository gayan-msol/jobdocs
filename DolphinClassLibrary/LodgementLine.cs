using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dolphin
{
    public class LodgementLine
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string eLMS { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }

        //public static List<LodgementLine> GetLodgementLines(string doc_id)
        //{
        //    List<LodgementLine> processList = new List<LodgementLine>();
        //    Dolphin dolphin = new Dolphin();
        //    string response = dolphin.getInfo(dolphin.ProcessInfo, doc_id);
        //    response = response?.Replace("\"doc_id\":", "\"DocID\":");
        //    response = response?.Replace("\"qr_id\":", "\"ID\":");
        //    response = response?.Replace("\"Link To\":", "\"LinkTo\":");

        //    if (response != null && response != "[]")
        //    {
        //        processList = JsonSerializer.Deserialize<List<JobProcess>>(response);
        //    }
        //    return processList;
        //}
    }
}
