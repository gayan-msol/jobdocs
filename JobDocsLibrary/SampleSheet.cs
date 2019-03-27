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
            List<string> colList = new List<string>();
            foreach (DataColumn col in dataTable.Columns)                
            {
                colList.Add(col.ColumnName);

                //bool selected = false;
                //foreach (string colName in selectedColList)
                //{
                //    if(col.ColumnName==colName)
                //    {
                //        selected = true;
                //        break;
                //    }
                //}

                //if(!selected)
                //{
                //    sampleTable.Columns.Remove(col);
                //}
            }

            List<string> colsToRemove = colList.Where(x => !selectedColList.Contains(x) && x != "Index").ToList();

            foreach (string item in colsToRemove)
            {
                dataTable.Columns.Remove(item);
            }

            DataTable sampleTable = dataTable.Clone();


            int nonEmptyRowIndex = getNonEmptyRowIndex(dataTable);
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

        private static int getNonEmptyRowIndex(DataTable dataTable)
        {
            int nonEmptyIndex = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                bool isEmpty = false;
                //foreach (DataColumn col in dataTable.Columns)
                foreach(DataColumn col in dataTable.Columns)
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
