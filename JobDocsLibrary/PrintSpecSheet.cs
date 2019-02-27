﻿using iTextSharp.text.pdf;
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
        public string JobDirectory { get; set; }
        public string FileName { get; set; }
        public string PrintMachine { get; set; }
        public string PrintSize { get; set; }
        public string FinishedSize { get; set; }
        public string Stock { get; set; }
        public string Layout { get; set; }
        public List<string> StreamList { get; set; }
        public string Notes { get; set; }





        public void createPdf(string templatePath,string fileName, PrintSpecSheet specSheet)
        {
          //  PDF dsPdf = new PDF(dataSummaryTemplate);


            try
            {
                    PdfReader pdfReader = new PdfReader(templatePath);

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
