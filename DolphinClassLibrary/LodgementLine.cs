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
        public string elms { get; set; }
        public int QTY { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string Category { get; set; }

        public static List<LodgementLine> GetLodgementLines(string doc_id)
        {
            List<LodgementLine> processList = new List<LodgementLine>();
            Dolphin dolphin = new Dolphin();
            string response = dolphin.getInfo(dolphin.LodgementInfo, doc_id);
            //response = response?.Replace("\"Note\":", "\"AccType\":");
            //response = response?.Replace("\"Post Number\":", "\"AccNo\":");
            //response = response?.Replace("\"doc_id\":", "\"DocID\":");
            //response = response?.Replace("\"Customer Name\":", "\"AccName\":");

            if (response != null && response != "[]")
            {
                processList = fastJSON.JSON.ToObject<List<LodgementLine>>(response);
            }
            return processList;
        }
    }
}
