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
        public Dictionary<string, string> SortList { get; set; }
        public Dictionary<string, string> WeightList { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string RegNo { get; set; }
        public string RegName { get; set; }




        public static PreSort GetPreSortCategories(DataTable dataTable, string sortCodeColumn)
        {
            PreSort preSort = new PreSort();

            foreach (DataRow row in dataTable.Rows)
            {
                string sortCode = row[sortCodeColumn].ToString();

                if (sortCode.Split(' ')[0] == "BD")
                {
                    if (sortCode.Split(' ')[2].Substring(0, 1) == "6")
                    {
                        preSort.BDSS += 1;
                    }
                    else
                    {
                        preSort.BDOS += 1;
                    }
                }
                else if (sortCode.Split(' ')[0] == "BR")
                {
                    if (sortCode.Split(' ')[1] == "WA")
                    {
                        preSort.BRSS += 1;
                    }
                    else
                    {
                        preSort.BROS += 1;
                    }
                }
                else if (sortCode.Split(' ')[0] == "UR")
                {
                    if (sortCode.Split(' ')[1] == "WA")
                    {
                        preSort.URSS += 1;
                    }
                    else
                    {
                        preSort.UROS += 1;
                    }
                }
                else if (sortCode == "x OTHER")
                {
                    string country = row["Country"].ToString();
                    string zone = DataAccess.GetIntZone(country);

                    if (string.IsNullOrEmpty(zone))
                    {
                        preSort.UnmatchedCountries += $"{country},";
                    }

                    preSort.Z1 += zone == "Z1" ? 1 : 0;
                    preSort.Z2 += zone == "Z2" ? 1 : 0;
                    preSort.Z3 += zone == "Z3" ? 1 : 0;
                    preSort.Z4 += zone == "Z4" ? 1 : 0;
                    preSort.Z5 += zone == "Z5" ? 1 : 0;
                    preSort.Z6 += zone == "Z6" ? 1 : 0;
                    preSort.Z7 += zone == "Z7" ? 1 : 0;
                    preSort.Z8 += zone == "Z8" ? 1 : 0;
                    preSort.Z9 += zone == "Z9" ? 1 : 0;
                }
            }

            return preSort;
        }

        public static void CreateSortSummary(PreSort sortInfo,  string jobNo, string dir)
        {

            PropertyInfo[] properties = typeof(PreSort).GetProperties();

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
                    sortSummary.Append($"        {p.Name} : {p.GetValue(sortInfo)}\n");
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
