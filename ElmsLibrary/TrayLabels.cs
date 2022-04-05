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

        public static TrayLabel CreateLabelLine(TrayLabel trayLabel)
        {
            string sortCode = trayLabel.SortCode.Replace(' ', '_');
            if (sortCode == "xOTHER" || sortCode == "x_OTHER")
            {
                return null;
            }

            trayLabel.ServiceType = trayLabel.ServiceType == "Priority" ? "1" : "R";
            trayLabel.Size = trayLabel.Size.Substring(0, 1);

            if (sortCode.Split('_')[0] == "BD")
            {
                trayLabel.Sort_Plan_Type = "1";
                string p1 = sortCode.Split('_')[1].Substring(1);
                if(trayLabel.ServiceType == "R" && trayLabel.Size == "S")
                {
                    trayLabel.Sort_Plan = $"2{p1}";
                }
                else if (trayLabel.ServiceType == "R" && trayLabel.Size == "L")
                {
                    trayLabel.Sort_Plan = $"7{p1}";
                }
                else if (trayLabel.ServiceType == "P" && trayLabel.Size == "S")
                {
                    trayLabel.Sort_Plan = $"1{p1}";
                }
                else if (trayLabel.ServiceType == "P" && trayLabel.Size == "L")
                {
                    trayLabel.Sort_Plan = $"6{p1}";
                }
            }
            else if (sortCode.Split('_')[0] == "BR")
            {
                trayLabel.Sort_Plan_Type = "2";
                trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[1]].ToString();
            }
            else if (sortCode.Split('_')[0] == "UR")
            {
                trayLabel.Sort_Plan_Type = "5";
                trayLabel.Sort_Plan = "6";
            }
            else if (sortCode.Split('_')[4] == "P")
            {
                trayLabel.Sort_Plan_Type = "3";
                trayLabel.Sort_Plan = sortCode.Split('_')[3]; // postcode
            }
            else if (sortCode.Split('_')[4] == "A")
            {
                trayLabel.Sort_Plan_Type = "4";
                trayLabel.Sort_Plan = sortCode.Split('_')[5]; // bsp
            }
            else if (sortCode.Split('_')[4] == "R")
            {
                trayLabel.Sort_Plan_Type = "5";
                trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[0]].ToString();
                trayLabel.ServiceType = trayLabel.ServiceType == "Priority" ? "G" : "H";
            }

            trayLabel.SortCode = ""; // sortcode can change within the same label type.

            return trayLabel;
        }

    }
}
