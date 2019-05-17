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

        SampleRecord sampleRecord = new SampleRecord();

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

            List<string> colsToRemove = colList.Where(x => !selectedColList.Contains(x) && x != "RowIndex").ToList(); // removing unselected columns

            foreach (string item in colsToRemove)
            {
                dataTable.Columns.Remove(item);
            }

            DataTable sampleTable = dataTable.Clone();


            // int nonEmptyRowIndex = getNonEmptyRowIndex(dataTable);
            int nonEmptyRowIndex = GetNonEmptyRecord(dataTable);

            if(nonEmptyRowIndex == -1)
            {
              List<int> sampleRecords=  SelectSampleRecords(CreateSampleRecordCandidates(dataTable));
                sampleRecords.Sort();

                if(sampleRecords.Count <3)
                {

                }
                int c = 0;
                while (sampleRecords.Count < 3)
                {
                  
                        if(sampleRecords[c] -1 >= 0 && !sampleRecords.Contains(sampleRecords[c] - 1))
                        {
                            sampleRecords.Add(sampleRecords[c] - 1);
                        }
                    c++;
                    
                  
                }
                sampleRecords.Sort();

                foreach (int i in sampleRecords)
                {
                    sampleTable.ImportRow(dataTable.Rows[i]);
                }


            }
            else
            {
                if (nonEmptyRowIndex >= 0)
                {
                    if (nonEmptyRowIndex + 2 <= dataTable.Rows.Count)
                    {
                        sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex]);
                        sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex + 1]);
                        sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex + 2]);
                    }
                    else if (nonEmptyRowIndex + 1 <= dataTable.Rows.Count)
                    {
                        sampleTable.ImportRow(dataTable.Rows[nonEmptyRowIndex - 1]);
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
               

                foreach(DataColumn col in dataTable.Columns)
                {
                    if (row[col].ToString() == "")
                    {
                        isEmpty = true;
                    }
                }
                
                if (!isEmpty)
                {
                    nonEmptyIndex = int.Parse(row["RowIndex"].ToString());
                    return nonEmptyIndex;
                }
            }
            return nonEmptyIndex;
        }


        private static List<SampleRecord> CreateSampleRecordCandidates(DataTable dataTable)
        {
            List<SampleRecord> sampleRecCandidates = new List<SampleRecord>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                SampleRecord sampleRecord = new SampleRecord();
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

                sampleRecCandidates.Add(sampleRecord);

            }

            return sampleRecCandidates;
        }

        private static int GetNonEmptyRecord(DataTable dataTable)
        {
         List<SampleRecord> sampleRecords = CreateSampleRecordCandidates(dataTable);


            foreach(SampleRecord s in sampleRecords)
            {
                if(s.EmptyColCount ==0)
                {
                    return s.Index;
                }
            }

            return -1;
        }

        private static List<int> SelectSampleRecords(List<SampleRecord> sampleRecords)
        {
            List<int> indexList = new List<int>();

            int leastEmptyIndex = 0;
            int currentColCount = sampleRecords[0].EmptyColCount;
            for (int i = 0; i < sampleRecords.Count; i++)
            {
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
                    int rec = sampleRecords.Where(x => !x.EmptyColIndexes.Contains(emptyCols[i])).FirstOrDefault().Index;
                    selectedRecords.Add(rec);
                }

                foreach(int rec in selectedRecords.Distinct())
             indexList.Add( rec  );
            }

            return indexList;
        }



        //private List<int> selectSampleRecords(List<SampleRecord> sampleRecords)
        //{



        //}


    }
}
