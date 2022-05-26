using System;
using System.Collections.Generic;
using System.Data;
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
                else if (trayLabel.ServiceType == "1" && trayLabel.Size == "S")
                {
                    trayLabel.Sort_Plan = $"1{p1}";
                }
                else if (trayLabel.ServiceType == "1" && trayLabel.Size == "L")
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

        public static List<TrayLabel> CreateLabelList(DataTable sourceTable, string serviceType, string size, string sortType)
        {
            List<TrayLabel> labels = new List<TrayLabel>();
            string sortCodeColumn = "";
            int BRCount = 0;
            int URCount = 0;

            switch (sortType)
            {
                case "PreSort":
                    sortCodeColumn = sourceTable.Columns.Contains("Dt_BP_Sort_Code") ? "Dt_BP_Sort_Code" : "Dt_BP_Sort_Order";//for Pioneer
                    break;
                case "Charity Mail":
                    sortCodeColumn = "Dt_BP_Sort_Code";
                    break;
                case "Print Post":
                    sortCodeColumn = sourceTable.Columns.Contains("Dt_PP_Sort_Code") ? "Dt_PP_Sort_Code" : "Dt_LH_Sort_Code";
                    break;
                default:
                    break;
            }

            foreach (DataRow row in sourceTable.Rows)
            {
                string sortCode = row[sortCodeColumn].ToString().Replace(' ', '_');
                TrayLabel trayLabel = new TrayLabel();
                trayLabel.ServiceType = serviceType == "Priority" ? "1" : "R";
                trayLabel.Size = size;
                trayLabel.SortType = sortType;

                if (sortCode != "")
                {

                    if (sortCode == "xOTHER" || sortCode == "x_OTHER")
                    {
                        continue;
                    }

                    size = size.Substring(0, 1);

                    if (sortCode.Split('_')[0] == "BD")
                    {
                        trayLabel.Sort_Plan_Type = "1";
                        string p1 = sortCode.Split('_')[1].Substring(1);
                        if (trayLabel.ServiceType == "R" && size == "S")
                        {
                            trayLabel.Sort_Plan = $"2{p1}";
                        }
                        else if (trayLabel.ServiceType == "R" && size == "L")
                        {
                            trayLabel.Sort_Plan = $"7{p1}";
                        }
                        else if (trayLabel.ServiceType == "1" && size == "S")
                        {
                            trayLabel.Sort_Plan = $"1{p1}";
                        }
                        else if (trayLabel.ServiceType == "1" && size == "L")
                        {
                            trayLabel.Sort_Plan = $"6{p1}";
                        }
                    }
                    else if (sortCode.Split('_')[0] == "BR")
                    {
                        BRCount++;
                        trayLabel.Sort_Plan_Type = "2";
                        trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[1]].ToString();
                    }
                    else if (sortCode.Split('_')[0] == "UR")
                    {
                        URCount++;
                        trayLabel.Sort_Plan_Type = "5";
                        trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[1]].ToString();
                    }
                    else if (sortCode.Split('_')[4] == "P")
                    {
                        trayLabel.Sort_Plan_Type = "3";
                        trayLabel.Sort_Plan = sortCode.Split('_')[3]; // postcode
                    }
                    else if (sortCode.Split('_')[4] == "A")
                    {
                        trayLabel.Sort_Plan_Type = "4";
                        string bsp = sortCode.Split('_')[5]; // bsp
                        if(bsp.Length > 3)
                        {
                            bsp = bsp.Substring(0, 3);
                        }
                        trayLabel.Sort_Plan = bsp;
                    }
                    else if (sortCode.Split('_')[4] == "R")
                    {
                        trayLabel.Sort_Plan_Type = "5";
                        trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[0]].ToString();
                        trayLabel.ServiceType = serviceType == "Priority" ? "G" : "H";
                    }


                    labels.Add(trayLabel);


                }

                
            }
            return labels;
        }

        

        public static string CreateLabelLines(List<TrayLabel> labels,string sortType, string lodgeDate, decimal countPerTray)
        {
            StringBuilder lines = new StringBuilder();
            //List<TrayLabel> labels = new List<TrayLabel>();
            //string sortCodeColumn = "";
            //int BRCount = 0;
            //int URCount = 0;

            //switch (sortType)
            //{

            //    case "PreSort":
            //        sortCodeColumn = sourceTable.Columns.Contains("Dt BP Sort Code") ? "Dt BP Sort Code" : "Dt_BP_Sort_Order"; // for Pioneer column names
            //        break;
            //    case "Charity Mail":
            //        sortCodeColumn = sourceTable.Columns.Contains("Dt BP Sort Code") ? "Dt BP Sort Code" : "Dt_BP_Sort_Order"; // for Pioneer column names
            //        break;
            //    case "Print Post":
            //        sortCodeColumn = sourceTable.Columns.Contains("Dt PP Sort Code") ? "Dt PP Sort Code" : "Dt LH Sort Code";
            //        break;
            //    default:
            //        break;
            //}

            //foreach (DataRow row in sourceTable.Rows)
            //{
            //    string sortCode = row[sortCodeColumn].ToString().Replace(' ', '_');
            //    TrayLabel trayLabel = new TrayLabel();
            //    trayLabel.ServiceType = serviceType == "Priority" ? "1" : "R";
            //    trayLabel.Size = size;
            //    trayLabel.SortType = sortType;

            //    if (sortCode != "")
            //    {

            //        if (sortCode == "xOTHER" || sortCode == "x_OTHER")
            //        {
            //            continue;
            //        }

            //        size = size.Substring(0, 1);

            //        if (sortCode.Split('_')[0] == "BD")
            //        {
            //            trayLabel.Sort_Plan_Type = "1";
            //            string p1 = sortCode.Split('_')[1].Substring(1);
            //            if (trayLabel.ServiceType == "R" && size == "S")
            //            {
            //                trayLabel.Sort_Plan = $"2{p1}";
            //            }
            //            else if (trayLabel.ServiceType == "R" && size == "L")
            //            {
            //                trayLabel.Sort_Plan = $"7{p1}";
            //            }
            //            else if (trayLabel.ServiceType == "1" && size == "S")
            //            {
            //                trayLabel.Sort_Plan = $"1{p1}";
            //            }
            //            else if (trayLabel.ServiceType == "1" && size == "L")
            //            {
            //                trayLabel.Sort_Plan = $"6{p1}";
            //            }
            //        }
            //        else if (sortCode.Split('_')[0] == "BR")
            //        {
            //            BRCount++;
            //            trayLabel.Sort_Plan_Type = "2";
            //            trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[1]].ToString();
            //        }
            //        else if (sortCode.Split('_')[0] == "UR")
            //        {
            //            URCount++;
            //            trayLabel.Sort_Plan_Type = "5";
            //            trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[1]].ToString();
            //        }
            //        else if (sortCode.Split('_')[4] == "P")
            //        {
            //            trayLabel.Sort_Plan_Type = "3";
            //            trayLabel.Sort_Plan = sortCode.Split('_')[3]; // postcode
            //        }
            //        else if (sortCode.Split('_')[4] == "A")
            //        {
            //            trayLabel.Sort_Plan_Type = "4";
            //            trayLabel.Sort_Plan = sortCode.Split('_')[5]; // bsp
            //        }
            //        else if (sortCode.Split('_')[4] == "R")
            //        {
            //            trayLabel.Sort_Plan_Type = "5";
            //            trayLabel.Sort_Plan = trayLabel.stateCodes[sortCode.Split('_')[0]].ToString();
            //            trayLabel.ServiceType = serviceType == "Priority" ? "G" : "H";
            //        }


            //        labels.Add(trayLabel);


            //    }
            //}

            int BRCount = labels.Where(x => x.Sort_Plan_Type == "2").ToList().Count;
            int URCount = labels.Where(x => x.Sort_Plan_Type == "5").ToList().Count;

            if (sortType != "Print Post")
            {
                for (int i = 0; i < labels.Count; i++)
                {
                    if (BRCount < 2000 && (labels[i].Sort_Plan_Type == "2" || labels[i].Sort_Plan_Type == "5"))
                    {
                        labels[i].Sort_Plan = "6";
                    }
                    else if (URCount < 2000 && (labels[i].Sort_Plan_Type == "2" || labels[i].Sort_Plan_Type == "5"))
                    {
                        labels[i].Sort_Plan = "6";
                    }
                }
            }

            int itemCount = 0;
            for (int i = 0; i < labels.Count; i++)
            {             

                itemCount++;
                if (i == labels.Count - 1 || $"{labels[i].Sort_Plan_Type}{labels[i].Sort_Plan}" != $"{labels[i + 1].Sort_Plan_Type}{labels[i + 1].Sort_Plan}")
                {
                    labels[i].LabelCount = (int)Math.Ceiling(itemCount / countPerTray);
                    lines.Append($"{labels[i].ServiceType},{labels[i].Sort_Plan_Type},{labels[i].Sort_Plan},{labels[i].Size},{labels[i].LabelCount},{lodgeDate}\n");
                    itemCount = 0;
                }
            }

            return lines.ToString();
        }



    }
}
