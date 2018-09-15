using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Drawing.Printing;

namespace JobDocs
{
    public partial class Form1 : Form
    {
        List<string> columnsList = new List<string>();
        List<string> dtColumnsList = new List<string>
        {"<Select or Enter DT Field>", "Title", "First Name", "Last Name", "Company Name",
            "Address Line 1", "Address Line 2", "Address Line 3",
            "Locality","State","Postcode","Country"
        };
        List<string> outputList = new List<string>();
        string path = "";
        int columnCount = 0;
        string dataSummaryPdf = "";
        string prodRepPdf = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void createPdf()
        {
         

            try
            {
                string jobDirectory = Path.GetDirectoryName(path);
                string JobNo = txtJobNo.Text != "" ? txtJobNo.Text : "0000";


                if (checkBoxDataSummary.Checked)
                {
                    string fileName = Path.GetFileName(path);
                    dataSummaryPdf = $"{jobDirectory}\\{JobNo} - Data Summary.pdf";
                    PdfReader pdfReader = new PdfReader(@"S:\ADMIN\FORMS\MS DATA SUMMARY\Templates\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf");
                    //PdfReader pdfReader = new PdfReader(@"C:\Users\Gayan\Documents\MSOL\test data\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf");
                    PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(dataSummaryPdf, System.IO.FileMode.OpenOrCreate));
                    pdfStamper.AcroFields.SetField("Date", DateTime.Today.ToShortDateString());
                    pdfStamper.AcroFields.SetField("Job No", JobNo);
                    pdfStamper.AcroFields.SetField("Job Name", txtJobName.Text);
                    pdfStamper.AcroFields.SetField("Customer", txtCustomer.Text);
                    pdfStamper.AcroFields.SetField("Original_File", fileName.Substring(fileName.IndexOf('-') + 1));

                    int j = 0;

                    for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                    {

                        ComboBox c = (ComboBox)flowLayoutPanel2.Controls[i];
                        if (c.SelectedIndex > 0)
                        {
                            j++;
                            pdfStamper.AcroFields.SetField($"Col{j}", columnsList[i]);
                            pdfStamper.AcroFields.SetField($"dtCol{j}", c.SelectedValue.ToString());
                            outputList.Add(c.SelectedValue.ToString());
                        }
                    }
                    string[] addressArr = new string[9];
                    for (int k = 0; k < outputList.Count; k++)
                    {
                        if (outputList[k] == "Title")
                        {
                            addressArr[0] += "Title ";
                        }
                        if (outputList[k] == "First Name")
                        {
                            addressArr[0] += "First Name ";
                        }
                        if (outputList[k] == "First Name")
                        {
                            addressArr[0] += "Last Name";
                        }
                    }

                    pdfStamper.Close();
                    pdfReader.Close();
                }

                if (checkBoxProductionReport.Checked)
                {
                    PdfReader pdfReader2 = new PdfReader(@"S:\ADMIN\FORMS\MS DATA SUMMARY\Templates\PRODUCTION REPORT SEP17 - TEMPLATE.pdf");
                    PdfStamper pdfStamper2 = new PdfStamper(pdfReader2, new System.IO.FileStream($"{jobDirectory}\\{JobNo} - Production Report.pdf", System.IO.FileMode.OpenOrCreate));
                    pdfStamper2.AcroFields.SetField("Job No", JobNo);
                    pdfStamper2.AcroFields.SetField("Job Name", txtJobName.Text);
                    pdfStamper2.AcroFields.SetField("Customer", txtCustomer.Text);
                    pdfStamper2.Close();
                    pdfReader2.Close();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

       
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createPdf();

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //// string Filepath = $"S:\\SCRIPTS\\Test Programmes\\TEMP\\{JobNo} - Data Summary.pdf";
            /*
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom Size", 800, 582);
            printDoc.DefaultPageSettings.PaperSize.RawKind = 120;
            printDoc.DefaultPageSettings.Margins.Left = 10; //100 = 1 inch = 2.54 cm
         
            printDoc.DocumentName = "My Document Name"; //this can affect name of output PDF file if printer is a PDF printer
                                                        //printDoc.PrinterSettings.PrinterName = "CutePDF";
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc; //Document property must be set before ShowDialog()

            DialogResult dialogResult = printDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                printDoc.Print(); //start the print
            }

            void printDoc_PrintPage(object senderp, PrintPageEventArgs arg)
            {
                Graphics g = arg.Graphics;
                string textToPrint = txtJobNo.Text;
                Font font1 = new Font("Courier New", 16);
                Font font = new Font("Courier New", 12);
                // e.PageBounds is total page size (does not consider margins)
                // e.MarginBounds is the portion of page inside margins

                int x1 = arg.MarginBounds.Left;
                int y1 = arg.MarginBounds.Top;
                int w = arg.MarginBounds.Width;
                int h = arg.MarginBounds.Height;
         
              //  g.DrawRectangle(Pens.Red, x1, y1, w, h); //draw a rectangle around the margins of the page, also we can use: g.DrawRectangle(Pens.Red, e.MarginBounds)
                g.DrawString(textToPrint, font1, Brushes.Black, x1, y1);
                g.DrawString("new text", font, Brushes.Black, x1, 200);

                arg.HasMorePages = false; //set to true to continue printing next page
            }
            */

            try
            {

                Print(dataSummaryPdf, "RICOH MP C5503 PCL 6");
                Print(prodRepPdf, "RICOH MP C5503 PCL 6");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static bool Print(string file, string printer)
        {
            try
            {
                Process.Start(
                   Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion" +
                        @"\App Paths\Acrobat.exe").GetValue("").ToString(),
                   string.Format("/h /t \"{0}\" \"{1}\"", file, "RICOH MP C5503 PCL 6"));
                return true;
            }
            catch (Exception ex)
            { MessageBox.Show($"An error occurred: '{ex.Message}'"); }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.Substring(file.Length - 4, 4) == ".txt")
                {
                    path = file;
                }
                columnsList = BL_JobDocs.getColumns(path);
            }

            if (columnsList != null)
            {
                columnCount = columnsList.Count;
                for(int i=0;i<columnsList.Count;i++)
                {                  
                    flowLayoutPanel1.Controls.Add(new TextBox { Text = columnsList[i], Size = new System.Drawing.Size(150, 25), Name = $"txtBox{i}" });
                    flowLayoutPanel2.Controls.Add(new ComboBox { DataSource = new List<string>(dtColumnsList),Size=new System.Drawing.Size(150,25), Name = $"combo{i},", AutoCompleteMode=AutoCompleteMode.SuggestAppend,AutoCompleteSource=AutoCompleteSource.ListItems });
                }
            }
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void checkBoxDataSummary_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
