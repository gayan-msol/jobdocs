using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HelperLibrary;

namespace JobDocsLibrary
{
    public class SampleSheet
    {
        public List<string> ColumnList { get; set; }



        public static DataTable GetSampleRecords(string fileName, string delimiter, List<string> selectedColList)
        {
            DataTable dataTable = JobData.GetSourceTable(fileName,delimiter);
            JobData.addIndexColumn(dataTable);
            DataTable sampleTable = dataTable.Clone();

            int nonEmptyRowIndex = getNonEmptyRowIndex(dataTable, selectedColList);
            if(nonEmptyRowIndex > 0)
            {
                if(nonEmptyRowIndex +2 <= dataTable.Rows.Count)
                {
                    sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex -1]);
                    sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex]);
                    sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex + 1]);
                }
                else
                {
                    sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex - 3]);
                    sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex - 2]);
                    sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex - 1]);
                }
            }

            //    DataRow row = dataTable.AsEnumerable().Any()
            return sampleTable;

        }

        private static int getNonEmptyRowIndex(DataTable dataTable, List<string> selectedColList)
        {
            int nonEmptyIndex = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                bool isEmpty = false;
                //foreach (DataColumn col in dataTable.Columns)
                foreach(string col in selectedColList)
                {
                    if (row[col].ToString() == "")
                    {
                        isEmpty = true;
                    }
                }
                if (!isEmpty)
                {
                    nonEmptyIndex = int.Parse(row["Index"].ToString());
                    return nonEmptyIndex;
                }
            }
            return nonEmptyIndex;
        }
    }
}
