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
        static DataTable sampleTable = null;


        public static DataTable GetSampleTable(DataTable sourceTable, List<string> selectedColList, string variableColumn = null)
        {
            bool slipSheet = sourceTable.Columns.Contains("MediaSelect");
            RemoveUnselectedColumns(sourceTable, selectedColList);

            JobData.addIndexColumn(sourceTable);

             sampleTable = sourceTable.Clone();

            List<string> variableValues = new List<string>();

            List<int> sampleRecords = SelectSampleRecords(GenerateRecordList(sourceTable,out variableValues, variableColumn));

            addVariableValues(sampleRecords, variableValues,variableColumn, sourceTable);

            if(sampleRecords.Count >0)
            {
                PopulateSampleTable(sourceTable, sampleTable, sampleRecords);
            }

            if (slipSheet)
            {
                addRowIndex(sampleTable);
            }
            sampleTable.Columns.Remove("RowIndex");

            return sampleTable;
        }

        private static void addVariableValues(List<int> sampleRecords, List<string> variableVaues, string varColumn, DataTable sourceTable)
        {

            List<string> existingRecords = new List<string>();
            for (int i = 0; i < sampleRecords.Count; i++)
            {
                for (int j = 0; j < variableVaues.Count; j++)
                {
                    if (sourceTable.Rows[sampleRecords[i]][varColumn].ToString() == variableVaues[j])
                    {
                        existingRecords.Add(variableVaues[j]);
                    }
                }
            }

            foreach (var item in existingRecords)
            {
                variableVaues.Remove(item);
            }

            for (int i = 0; i < variableVaues.Count; i++)
            {
                for (int j = 0; j < sourceTable.Rows.Count; j++)
                {
                    if(sourceTable.Rows[j][varColumn].ToString() == variableVaues[i])
                    {
                        sampleRecords.Add(j);
                        break;
                    }
                }
            }
        }


        private static void RemoveUnselectedColumns(DataTable dataTable, List<string> selectedColumns)
        {
            List<string> colList = new List<string>();
            foreach (DataColumn col in dataTable.Columns)
            {
                colList.Add(col.ColumnName);
            }

            List<string> colsToRemove = colList.Where(x => !selectedColumns.Contains(x) ).ToList();

            foreach (string item in colsToRemove)
            {
                dataTable.Columns.Remove(item);
            }
        }

        private static void addRowIndex(DataTable table)
        {
          
                foreach (DataRow row in table.Rows)
                {
                    row["Sort Order"] = $"{row["Sort Order"]}   ({row["RowIndex"]})";
                }
            
        }

        private static List<Record> GenerateRecordList(DataTable dataTable,  out List<string> variableValues, string variableColumn = null)
        {
            List<Record> allRecordsList = new List<Record>();
            List<string> variables = new List<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if(variableColumn != null /*&& !string.IsNullOrWhiteSpace(dataTable.Rows[i][variableColumn].ToString())*/)
                {
                    variables.Add(dataTable.Rows[i][variableColumn].ToString());
                }

                Record sampleRecord = new Record();
                List<int> emptyColList = new List<int>();
                int emptyCount = 0;
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    
                    if(string.IsNullOrWhiteSpace(dataTable.Rows[i][j].ToString()))
                    {
                        //if(dataTable.Columns[j].ColumnName == variableColumn)
                        //{
                        //    sampleRecord.VariableColIndex = j;
                        //}
                        emptyCount++;
                        emptyColList.Add(j);
                    }
                }

                sampleRecord.Index = i;
                sampleRecord.EmptyColCount = emptyCount;
                sampleRecord.EmptyColIndexes = emptyColList;

                allRecordsList.Add(sampleRecord);

            }

           variableValues = variables.Distinct().ToList();

            return allRecordsList;
        }


        private static List<int> SelectSampleRecords(List<Record> sampleRecords)
        {
            List<int> indexList = new List<int>();

            int leastEmptyIndex = 0;
            int currentColCount = sampleRecords[0].EmptyColCount;
            for (int i = 0; i < sampleRecords.Count; i++)
            {


                if(sampleRecords[i].EmptyColCount == 0)
                {
                    indexList.Add(i);
                    return indexList;
                }

                if(currentColCount > sampleRecords[i].EmptyColCount)
                {
                    currentColCount = sampleRecords[i].EmptyColCount;
                    leastEmptyIndex = i;
                }
             
            }

            indexList.Add(leastEmptyIndex);

            if(sampleRecords[leastEmptyIndex].EmptyColCount >0)
            {
                List<int> emptyCols = sampleRecords[leastEmptyIndex].EmptyColIndexes;
                List<int> selectedRecords = new List<int>();

                for (int i = 0; i < emptyCols.Count; i++)
                {
                    int rec = sampleRecords.Where(x => !x.EmptyColIndexes.Contains(emptyCols[i])).FirstOrDefault()?.Index ?? -1;
                    selectedRecords.Add(rec);
                }

                foreach(int rec in selectedRecords.Distinct())
                {
                    if(rec >-1)
                    {
                        indexList.Add(rec);
                    }
                }
            }

            return indexList;
        }


        private static void PopulateSampleTable(DataTable sourceTable, DataTable sampleTable, List<int> sampleRecords)
        {
            int total = sourceTable.Rows.Count;
            int c = 1;

            int s1 = sampleRecords[0];

            while(sampleRecords.Count <3 && (s1 + c < total) )
            {
             
                    if(!sampleRecords.Contains(s1 + c))
                    {
                        sampleRecords.Add(s1 + c);
                    }
                c++;
            }

            c = 1;
            while (sampleRecords.Count < 3 && (s1 - c > 0))
            {

                if (!sampleRecords.Contains(s1 - c))
                {
                    sampleRecords.Add(s1 - c);
                }
                c++;
            }

            sampleRecords.Sort();

            foreach (int i in sampleRecords)
            {
                sampleTable.ImportRow(sourceTable.Rows[i]);
            }
        }



    }
}
