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
            DataTable dt = TextFileRW.readTextFileToTable(@"S:\DATABASES\SCOTT PRINT\JOB 208030 MEGASTAR MAGAZINE\JOB208030DR - Correct mailing numbers.txt", "\t");
            int index = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(string.IsNullOrEmpty(dt.Rows[i]["Name"].ToString()))
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
