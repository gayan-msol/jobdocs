using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElmsLibrary;
using JobDocsLibrary;
using Dolphin;
using System.Data;
using BBUtils;


namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            function2();
        }

        static void function2()
        {
            DataTable dt = TextFileRW.readTextFileToTable(@"S:\DATABASES\ADVANCE PRESS\JOB 208017 CURTIN A6 CARD\Remakes\JOB 208017 OUTPUT FILE - A - Add1 1-20,000.txt", ",");
            DataTable dtMatching = TextFileRW.readTextFileToTable(@"S:\DATABASES\ADVANCE PRESS\JOB 208017 CURTIN A6 CARD\Remakes\IDs.txt", "\t");
            DataTable dtMatched = new DataTable();
            string matchingColumn = "Const ID";

            foreach (DataColumn col in dt.Columns)
            {
                dtMatched.Columns.Add(col.ColumnName);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j= 0; j < dtMatching.Rows.Count; j++)
                {
                    if(dt.Rows[i][matchingColumn].ToString() == dtMatching.Rows[j][0].ToString())
                    {
                        DataRow dataRow = dtMatched.NewRow();
                        dataRow.ItemArray = dt.Rows[i].ItemArray;
                        dtMatched.Rows.Add(dataRow);
                    }
                }

            }

            TextFileRW.writeTableToTxtFile(dtMatched, @"S:\DATABASES\ADVANCE PRESS\JOB 208017 CURTIN A6 CARD\Remakes\Remakes.txt", ",");
        }
        static void function1()
        {
            DataTable dt = TextFileRW.readTextFileToTable(@"S:\DATABASES\ADVANCE PRESS\JOB 208017 CURTIN A6 CARD\Remakes\Remakes.txt", "\t");
            int index = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dt.Rows[i]["Name"].ToString()))
                {
                    dt.Rows[index]["Address"] += $", {dt.Rows[i]["Address"]}";
                }
                else
                {
                    index = i;
                }
            }

            TextFileRW.writeTableToTxtFile(dt, @"S:\DATABASES\SCOTT PRINT\JOB 208030 MEGASTAR MAGAZINE\JOB208030DR - Correct mailing numbers_merged.txt", "\t");
        }


    }
}
