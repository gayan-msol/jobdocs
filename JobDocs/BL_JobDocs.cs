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



        //public static DataTable readCSV(string fileName)
        //{
        //    DataTable table = new DataTable();

        //    using (TextFieldParser txtParser = new TextFieldParser(fileName))
        //    {
        //        txtParser.SetDelimiters("\t");

        //        while (!txtParser.EndOfData)
        //        {
        //            string[] fields = txtParser.ReadFields();

        //            int l = fields.GetLength(0);
        //            long line = txtParser.LineNumber;

        //            if (line == 2)
        //            {
        //                foreach (string field in fields)
        //                {
        //                    table.Columns.Add(field);
        //                }
        //            }
        //            else
        //            { table.Rows.Add(fields); }
        //        }
        //    }

        //    return table;
        //}

        //public static List<string> getColumns(string path)
        //{
        //    List<string> colList = new List<string>();
        //    using (TextFieldParser txtParser = new TextFieldParser(path))
        //    {
        //        txtParser.SetDelimiters("\t");

        //        while (!txtParser.EndOfData)
        //        {
        //            string[] fields = txtParser.ReadFields();

        //            //int l = fields.GetLength(0);
        //            long line = txtParser.LineNumber;

        //            if (line == 2)
        //            {
        //                foreach (string field in fields)
        //                {
        //                    colList.Add(field);
        //                }
        //                return colList;
        //            }
                   
        //        }
        //    }
        //    return null;
        //}
    }
}
