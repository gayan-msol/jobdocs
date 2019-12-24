using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace JobDocsLibrary
{
    public class PrintSpecSheet
    {
        public string JobNo { get; set; }
        public string JobName { get; set; }
        public string Customer { get; set; }
        public string Contact { get; set; }
        public string JobDirectory { get; set; }
        public List<string> FileNames { get; set; }
        public string PrintMachine { get; set; }
        public string PrintSize { get; set; }
        public string FinishedSize { get; set; }
        public string Guillotine { get; set; }
        public List<string >Stock { get; set; }
        public string Layout { get; set; }
        public List<string> StreamList { get; set; }
        public string Notes { get; set; }
        public bool Approved { get; set; }
        public bool AddInkJet { get; set; }

        private string pdfTemplatePath = @"S:\SCRIPTS\DotNetProgrammes\PRINT SPEC SHEET\Spec Sheet Template.pdf";

        public void createPdf(string fileName, PrintSpecSheet specSheet)
        {

            try
            {
                    PdfReader pdfReader = new PdfReader(pdfTemplatePath);

                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate)))
                    {

                        foreach (PropertyInfo prop in specSheet.GetType().GetProperties())
                        {
                            pdfStamper.AcroFields.SetField(prop.Name, prop?.GetValue(specSheet)?.ToString());
                        }

                    StringBuilder stringBuilder = new StringBuilder();    

                    foreach (string item in specSheet.StreamList)
                    {
                        stringBuilder.Append($"{item}\n");
                    }
                    pdfStamper.AcroFields.SetField("StreamList", stringBuilder.ToString());

                    }

                    pdfReader.Close();
                

            }
            catch (Exception e)
            {

              
            }



        }
    }
}
