using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace JobDocsLibrary
{
    public class PDF
    {
        public string TemplatePath { get; set; }
        public List<string> FieldList { get; set; }

        public PDF(string templatePath)
        {
            TemplatePath = templatePath;
            List<string> fieldList = new List<string>();
            PdfReader pdfReader = new PdfReader(templatePath);

            using (pdfReader)
            {
                foreach (var item in pdfReader.AcroFields.Fields)
                {
                    fieldList.Add(item.Key);
                }
            }

            FieldList = fieldList;

        }
    }
}
