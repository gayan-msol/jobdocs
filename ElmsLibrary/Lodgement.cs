using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Size { get; set; }
        public string Weight { get; set; }
        public string RegNo { get; set; }
        public string RegName { get; set; }

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
