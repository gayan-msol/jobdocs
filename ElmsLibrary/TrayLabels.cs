using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElmsLibrary
{
    public class TrayLabel
    {
        public Dictionary<string, int> stateCodes { get; set; } = new Dictionary<string, int>();
        public string SortType { get; set; }
        public string SortCode { get; set; }
        public string ServiceType { get; set; }
        public string Sort_Plan_Type { get; set; }
        public string Sort_Plan { get; set; }
        public string Size { get; set; }
        public int LabelCount { get; set; }
        public string Date { get; set; }


        public TrayLabel()
        {
            stateCodes.Add("ACT", 1);
            stateCodes.Add("NSW", 2);
            stateCodes.Add("VIC", 3);
            stateCodes.Add("QLD", 4);
            stateCodes.Add("SA", 5);
            stateCodes.Add("WA", 6);
            stateCodes.Add("TAS", 7);
            stateCodes.Add("NT", 8);
        }

        public static string CreateLabelLine(TrayLabel trayLabel, DateTime lodgeDate)
        {
            string sortCode = trayLabel.SortCode.Replace(' ', '_');
            if (trayLabel.SortCode == "xOTHER" || trayLabel.SortCode == "x_OTHER")
            {
                return "";
            }

            string planType = "";
            string plan = "";
            string qty = trayLabel.LabelCount.ToString();          
            string service = trayLabel.ServiceType == "Priority" ? "1" : "R";
            string size = trayLabel.Size.Substring(0, 1);
            string date = lodgeDate.ToString("dd MMM yyyy");


            if (sortCode.Split('_')[0] == "BD")
            {
                planType = "1";
                string p1 = sortCode.Split('_')[2].Substring(1);
                if(service == "R" && size == "S")
                {
                    planType = $"2{p1}";
                }
                else if (service == "R" && size == "L")
                {
                    planType = $"7{p1}";
                }
                else if (service == "P" && size == "S")
                {
                    planType = $"1{p1}";
                }
                else if (service == "P" && size == "L")
                {
                    planType = $"6{p1}";
                }
            }
            if (sortCode.Split('_')[0] == "BR")
            {
                planType = "2";
                plan = "6";
            }
            if (sortCode.Split('_')[0] == "UR")
            {
                planType = "5";
                plan = "6";
            }


            else if (sortCode.Split('_')[4] == "P")
            {
                planType = "3";
                plan = sortCode.Split('_')[3]; // postcode
            }
            else if (sortCode.Split('_')[4] == "A")
            {
                planType = "4";
                plan = sortCode.Split('_')[5]; // bsp
            }
            else if (sortCode.Split('_')[4] == "R")
            {
                planType = "5";
                plan =trayLabel.stateCodes[sortCode.Split('_')[0]].ToString();
                service = trayLabel.ServiceType == "Priority" ? "G" : "H";
            }

            return $"{service},{planType},{plan},{size}";
        }

        //public static string CreateLabelPlan(List<TrayLabel> trayLabels)
        //{
        //    string labelPlan = "#Australia Post Visa Tray Label System - Ver:\n3v0-700\n#Label Plan File\n#Label Plan Header\n";
        //    labelPlan += $"{jobNo} {jobName},MAILING SOLUTIONS,{jobName},6,{jobNo}";

        //}
    }
}
