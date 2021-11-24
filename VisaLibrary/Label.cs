using System;
using System.Collections.Generic;
using System.Text;

namespace VisaLibrary
{
    public class Label
    {
        public string JobNumber { get; set; }
        public int LabelCount { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }

        public static string CreateLabel(string Name, string JobNumber, string Qty, DateTime Date, string username, string JobName)
        {
            string version = "2v7-0921";
            return $"#Australia Post Visa Tray Label System - Ver:\n{version}\n#Label Plan File\n\n#Label Plan Header\n{Environment.UserName},,,{Date.ToString("dd-MMM-yyyy")},{username},9997,,,Mailing Solutions,{JobName},,28888,\n" +
                $"#Label Details\n#Label_Destn_Name,Label_Sub_Destn_Name,Label_Qty,Stacker_Nbr,Prod_Type_Code,Date_Type,Specific_Date,Print_Time_Type_Code,Tray_SubDestn_Id,Tray_Destn_Id\n" +
                $"WA,Full Rate Regular,{Qty},1,4415,1,{Date.ToString("dd-MMM-yyyy")},2,234602,47292006\n#End Of File";
        }
    }
}
