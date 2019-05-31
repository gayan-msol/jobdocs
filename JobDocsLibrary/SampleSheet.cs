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
 
        public static DataTable GetSampleTable(DataTable sourceTable, List<string> selectedColList)
        {
            RemoveUnselectedColumns(sourceTable, selectedColList);

            JobData.addIndexColumn(sourceTable);

            DataTable sampleTable = sourceTable.Clone();

            List<int> sampleRecords = SelectSampleRecords(GenerateRecordList(sourceTable));

            if(sampleRecords.Count >0)
            {
                PopulateSampleTable(sourceTable, sampleTable, sampleRecords);
            }

            return sampleTable;
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


        private static List<Record> GenerateRecordList(DataTable dataTable)
        {
            List<Record> allRecordsList = new List<Record>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Record sampleRecord = new Record();
                List<int> emptyColList = new List<int>();
                int emptyCount = 0;
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    
                    if(string.IsNullOrWhiteSpace(dataTable.Rows[i][j].ToString()))
                    {
                        emptyCount++;
                        emptyColList.Add(j);
                    }
                }

                sampleRecord.Index = i;
                sampleRecord.EmptyColCount = emptyCount;
                sampleRecord.EmptyColIndexes = emptyColList;

                allRecordsList.Add(sampleRecord);

            }

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
                c--;
            }

            sampleRecords.Sort();

            foreach (int i in sampleRecords)
            {
                sampleTable.ImportRow(sourceTable.Rows[i]);
            }
        }



    }
}
