using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using System.Reflection;

namespace JobDocsLibrary
{
    public class ProductionReport
    {
        public string JobNo { get; set; }
        public string JobName { get; set; }
        public string Customer { get; set; }
        public int Qty { get; set; }
        public string Envelope { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
        public string Item5 { get; set; }
        public string Item6 { get; set; }
        private string productioReportTemplate = @"S:\SCRIPTS\DotNetProgrammes\PDF Templates\PRODUCTION REPORT SEP17 - TEMPLATE.pdf";


        public void createPdf(string fileName, ProductionReport productionReport)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(productioReportTemplate);

                using (PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate)))
                {

                    foreach (PropertyInfo prop in productionReport.GetType().GetProperties())
                    {
                        pdfStamper.AcroFields.SetField(prop.Name, prop?.GetValue(productionReport)?.ToString());
                    }       
                }
                pdfReader.Close();
            }
            catch (Exception e)
            {


            }



        }
    }
}
