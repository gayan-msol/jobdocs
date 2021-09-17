using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ElmsLibrary
{
    public class Lodgement
    {
        public string JobNo { get; set; }
        public string JobName { get; set; }
        public string AccNo { get; set; }
        public string SortType { get; set; }
        public string ServiceType { get; set; }
        public string ProductGroup { get; set; }
        public string ArticleType { get; set; }
        public Dictionary<string, string> SortList { get; set; }
        public Dictionary<string, string> WeightList { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string RegNo { get; set; }
        public string RegName { get; set; }





        public static SortSummary GetSortCategories(DataTable dataTable, string sortType , bool intContract = false)
        {
            SortSummary sortSummary = new SortSummary();
            int unmatchedCount = 0;
            if (sortType == "Full Rate")
            {
                if (dataTable.Columns.Contains("Country"))
                {
                

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if(row["Country"] == "")
                        {
                            sortSummary.AusTotal += 1;
                        }
                        else
                        {
                            sortSummary.IntTotal += 1;
                    
                            string country = row["Country"].ToString();
                            string zone = DataAccess.GetIntZone(country, intContract);

                            if (string.IsNullOrEmpty(zone))
                            {
                                unmatchedCount++;
                                sortSummary.UnmatchedCountries += $"{country},";
                            }

                            sortSummary.Z1 += zone == "Z1" ? 1 : 0;
                            sortSummary.Z2 += zone == "Z2" ? 1 : 0;
                            sortSummary.Z3 += zone == "Z3" ? 1 : 0;
                            sortSummary.Z4 += zone == "Z4" ? 1 : 0;
                            sortSummary.Z5 += zone == "Z5" ? 1 : 0;
                            sortSummary.Z6 += zone == "Z6" ? 1 : 0;
                            sortSummary.Z7 += zone == "Z7" ? 1 : 0;
                            sortSummary.Z8 += zone == "Z8" ? 1 : 0;
                            sortSummary.Z9 += zone == "Z9" ? 1 : 0;
                        }
                    }
                }
                else
                {
                    sortSummary.AusTotal = dataTable.Rows.Count;
                }
             
            }
            else
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string sortCodeColumn = "";

                    switch (sortType)
                    {

                        case "PreSort":
                            sortCodeColumn = dataTable.Columns.Contains("Dt BP Sort Code") ? "Dt BP Sort Code" : "Dt_BP_Sort_Order"; // for Pioneer column names
                            break;
                        case "Charity Mail":
                            sortCodeColumn = dataTable.Columns.Contains("Dt BP Sort Code") ? "Dt BP Sort Code" : "Dt_BP_Sort_Order"; // for Pioneer column names
                            break;
                        case "Print Post":
                            sortCodeColumn = "Dt PP Sort Code";
                            break;
                        default:
                            break;
                    }


                    string sortCode = row[sortCodeColumn]?.ToString().Replace(' ', '_') ?? string.Empty;

                    if (string.IsNullOrEmpty(sortCode))
                    {
                        continue;
                    }


                    if (sortCode == "x_OTHER" || sortCode == "xOTHER")
                    {
                        sortSummary.IntTotal += 1;

                        string country = row["Country"].ToString();
                        string zone = DataAccess.GetIntZone(country, intContract);

                        if (string.IsNullOrEmpty(zone))
                        {
                            unmatchedCount++;
                            sortSummary.UnmatchedCountries += $"{country},";
                        }

                        sortSummary.Z1 += zone == "Z1" ? 1 : 0;
                        sortSummary.Z2 += zone == "Z2" ? 1 : 0;
                        sortSummary.Z3 += zone == "Z3" ? 1 : 0;
                        sortSummary.Z4 += zone == "Z4" ? 1 : 0;
                        sortSummary.Z5 += zone == "Z5" ? 1 : 0;
                        sortSummary.Z6 += zone == "Z6" ? 1 : 0;
                        sortSummary.Z7 += zone == "Z7" ? 1 : 0;
                        sortSummary.Z8 += zone == "Z8" ? 1 : 0;
                        sortSummary.Z9 += zone == "Z9" ? 1 : 0;
                    }
                    else if (sortCode.Split('_')[0] == "BD")
                    {
                        if (sortCode.Split('_')[2].Substring(0, 1) == "6")
                        {
                            sortSummary.BDSS += 1;
                        }
                        else
                        {
                            sortSummary.BDOS += 1;
                        }
                    }
                    else if (sortCode.Split('_')[0] == "BR")
                    {
                        if (sortCode.Split('_')[1] == "WA")
                        {
                            sortSummary.BRSS += 1;
                        }
                        else
                        {
                            sortSummary.BROS += 1;
                        }
                    }
                    else if (sortCode.Split('_')[0] == "UR")
                    {
                        if (sortCode.Split('_')[1] == "WA")
                        {
                            sortSummary.URSS += 1;
                        }
                        else
                        {
                            sortSummary.UROS += 1;
                        }
                    }
                    else if (sortCode.Split('_')[4] == "A") ////// Print Post
                    {
                        if (sortCode.Split('_')[0] == "WA")
                        {
                            sortSummary.ADSS += 1;
                        }
                        else
                        {
                            sortSummary.ADOS += 1;
                        }
                    }
                    else if (sortCode.Split('_')[4] == "P")
                    {
                        if (sortCode.Split('_')[0] == "WA")
                        {
                            sortSummary.PDSS += 1;
                        }
                        else
                        {
                            sortSummary.PDOS += 1;
                        }
                    }
                    else if (sortCode.Split('_')[4] == "R")
                    {
                        if (sortCode.Split('_')[0] == "WA")
                        {
                            sortSummary.RESSS += 1;
                        }
                        else
                        {
                            sortSummary.RESOS += 1;
                        }
                    }

                }

         

                sortSummary.AusTotal = dataTable.Rows.Count - sortSummary.IntTotal;
            }

            if (intContract)
            {
                sortSummary.Z8 = unmatchedCount;
                sortSummary.UnmatchedCountries = "";
            }


            return sortSummary;
        }

        public static void CreateSortSummary(SortSummary sortInfo,  string jobNo, string dir)
        {

            PropertyInfo[] properties = typeof(SortSummary).GetProperties();

            StringBuilder sortSummary = new StringBuilder();
            string spacing = "              ";

            sortSummary.Append("             Same State    Other State\n");
            sortSummary.Append($"Direct       {sortInfo.BDSS}{spacing.Substring(sortInfo.BDSS.ToString().Length)}{sortInfo.BDOS}\n");
            sortSummary.Append($"Residue      {sortInfo.BRSS}{spacing.Substring(sortInfo.BRSS.ToString().Length)}{sortInfo.BROS}\n");
            sortSummary.Append($"Unbarcoded   {sortInfo.URSS}{spacing.Substring(sortInfo.URSS.ToString().Length)}{sortInfo.UROS}\n");
            sortSummary.Append("------------International-------------\n");


            int intCount = 0;

            foreach (PropertyInfo p in properties)
            {
                if (p.Name.Substring(0, 1) == "Z")
                {
                    intCount += int.Parse(p.GetValue(sortInfo).ToString());
                    sortSummary.Append($"        {p.Name} :\t {p.GetValue(sortInfo)}\n");
                }
            }
            sortSummary.Append($"Total Int. : {intCount}");
            if (!string.IsNullOrEmpty(sortInfo.UnmatchedCountries))
            {
                sortSummary.Append($"*** Unmatched countries : {sortInfo.UnmatchedCountries}");
            }


            File.WriteAllText($@"{dir}\JOB {jobNo} Presort Summary.txt", sortSummary.ToString());

        }

        public Dictionary<string,string> ReadManifest(string fileName, string sortType)
        {
            PDDocument pDDocument = PDDocument.load(fileName);
            Dictionary<string, string> sortList = new Dictionary<string, string>();

            PDFTextStripper pdftextstrpper = new PDFTextStripper();
            string text = pdftextstrpper.getText(pDDocument);
            string[] lines = text.Split('\n');

            List<SortCategory> sortCategories = DataAccess.GetSortCategories(sortType);
            
            if(sortType == "Pre-Sort")
            {

                for (int i = 0; i < sortCategories.Count; i++)
                {
                    if (i == 3)
                    {
                        i++;
                    }
                    sortList.Add(sortCategories[i].ElementName, lines[i].Split(' ')[0] == "0" ? "" : lines[i].Split(' ')[0]);
                }
            }

            return sortList;

        }

      

    }
}
