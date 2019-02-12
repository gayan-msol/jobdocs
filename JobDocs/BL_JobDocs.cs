using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace JobDocs
{
    class BL_JobDocs
    {

        public static List<string> addressBlock(List<string> colList)        
        {
            List<string> addressColList = new List<string>();
            StringBuilder nameLine = new StringBuilder();
            StringBuilder locStPcLine = new StringBuilder();

            nameLine.Append(colList.Contains("Title") ? "<Title> " : "");
            nameLine.Append(colList.Contains("First Name") ? "<First Name> " : "");
            nameLine.Append(colList.Contains("Last Name") ? "<Last Name> " : "");
            nameLine.Append(colList.Contains("Name Suffix") ? "<Name Suffix>" : "");
            if(nameLine.ToString() !="")
            {
                addressColList.Add(nameLine.ToString());
            }
            if(colList.Contains("Position"))
            {
                addressColList.Add("<Position>");
            }
            if (colList.Contains("Company Name"))
            {
                addressColList.Add("<Company Name>");
            }
            if (colList.Contains("Address Line 1"))
            {
                addressColList.Add("<Address Line 1>");
            }
            if (colList.Contains("Address Line 2"))
            {
                addressColList.Add("<Address Line 2>");
            }
            if (colList.Contains("Address Line 3"))
            {
                addressColList.Add("<Address Line 3>");
            }
            if (colList.Contains("Address Line 4"))
            {
                addressColList.Add("<Address Line 4>");
            }
            if (colList.Contains("Address Line 5"))
            {
                addressColList.Add("<Address Line 5>");
            }

            locStPcLine.Append(colList.Contains("Locality") ? "<Locality> " : "");
            locStPcLine.Append(colList.Contains("State") ? "<State> " : "");
            locStPcLine.Append(colList.Contains("Postcode") ? "<Postcode>" : "");
            if (locStPcLine.ToString() != "")
            {
                addressColList.Add(locStPcLine.ToString());
            }
            if (colList.Contains("Country"))
            {
                addressColList.Add("<Country>");
            }
            return addressColList;
        }

        public static DataTable readCSV(string fileName)
        {
            DataTable table = new DataTable();

            using (TextFieldParser txtParser = new TextFieldParser(fileName))
            {
                txtParser.SetDelimiters("\t");

                while (!txtParser.EndOfData)
                {
                    string[] fields = txtParser.ReadFields();

                    int l = fields.GetLength(0);
                    long line = txtParser.LineNumber;

                    if (line == 2)
                    {
                        foreach (string field in fields)
                        {
                            table.Columns.Add(field);
                        }
                    }
                    else
                    { table.Rows.Add(fields); }
                }
            }

            return table;
        }

        public static List<string> getColumns(string path)
        {
            List<string> colList = new List<string>();
            using (TextFieldParser txtParser = new TextFieldParser(path))
            {
                txtParser.SetDelimiters("\t");

                while (!txtParser.EndOfData)
                {
                    string[] fields = txtParser.ReadFields();

                    //int l = fields.GetLength(0);
                    long line = txtParser.LineNumber;

                    if (line == 2)
                    {
                        foreach (string field in fields)
                        {
                            colList.Add(field);
                        }
                        return colList;
                    }
                   
                }
            }
            return null;
        }
    }
}
