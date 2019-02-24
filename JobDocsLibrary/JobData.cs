using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary;
using System.Data;

namespace JobDocsLibrary
{
    public class JobData
    {
        public static DataTable GetSourceTable(string fileName, string delimiter)
        {
            return TextFileRW.readTextFileToTable(fileName, delimiter);
        }

        public static void addIndexColumn(DataTable dataTable)
        {
            dataTable.Columns.Add("Index");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["Index"] = i + 1;
            }
        }


        public static List<string> GetColumnList(string fileName, string delimiter)
        {
            return TextFileRW.getColumns(fileName, delimiter);
        }
    }
}
