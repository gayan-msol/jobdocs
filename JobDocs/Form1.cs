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

namespace JobDocs
{
    public partial class Form1 : Form
    {
        List<string> columnsList = new List<string>();
        List<string> dtColumnsList = new List<string>
        {"<--->", "Title", "First Name", "Last Name", "Company Name",
            "Address Line 1", "Address Line 2", "Address Line 3",
            "Locality","State","Postcode","Country"
        };
        List<string> mappedList = new List<string>();
        string path = "";
        int columnCount = 0;
        string pdfPath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(path);
            string JobNo =txtJobNo.Text!=""? txtJobNo.Text: "111222";
            pdfPath = $"S:\\SCRIPTS\\Test Programmes\\TEMP\\{JobNo} - Data Summary.pdf";
            PdfReader pdfReader = new PdfReader(@"S:\ADMIN\FORMS\MS DATA SUMMARY\Templates\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf");
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(pdfPath, System.IO.FileMode.OpenOrCreate));
            pdfStamper.AcroFields.SetField("Date", DateTime.Today.ToShortDateString());
            pdfStamper.AcroFields.SetField("Job No", JobNo);
            pdfStamper.AcroFields.SetField("Job Name", txtJobName.Text);
            pdfStamper.AcroFields.SetField("Customer", txtCustomer.Text);
            pdfStamper.AcroFields.SetField("Original_File", fileName.Substring(fileName.IndexOf('-') +1 ));
            //for (int i = 0; i < columnCount; i++)
            //{
            //    pdfStamper.AcroFields.SetField($"Col{i+1}", columnsList[i]);
            //}
            int j = 0;

        for (int i =0; i< flowLayoutPanel2.Controls.Count;i++)
            {
                
                ComboBox c = (ComboBox)flowLayoutPanel2.Controls[i];
                if (c.SelectedIndex > 0)
                {
                    j++;
                    pdfStamper.AcroFields.SetField($"Col{j}", columnsList[i]);
                    pdfStamper.AcroFields.SetField($"dtCol{j}", c.SelectedValue.ToString());

                }
            }
           
            pdfStamper.Close();
            pdfReader.Close();

            PdfReader pdfReader2 = new PdfReader(@"S:\ADMIN\FORMS\MS DATA SUMMARY\Templates\PRODUCTION REPORT SEP17 - TEMPLATE.pdf");
            PdfStamper pdfStamper2 = new PdfStamper(pdfReader2, new System.IO.FileStream($"S:\\SCRIPTS\\Test Programmes\\TEMP\\{JobNo} - Production Report.pdf", System.IO.FileMode.OpenOrCreate));
            pdfStamper2.AcroFields.SetField("Job No", JobNo);
            pdfStamper2.AcroFields.SetField("Job Name", txtJobName.Text);
            pdfStamper2.AcroFields.SetField("Customer", txtCustomer.Text);
            pdfStamper2.Close();
            pdfReader2.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //// string Filepath = $"S:\\SCRIPTS\\Test Programmes\\TEMP\\{JobNo} - Data Summary.pdf";

            // using (PrintDialog Dialog = new PrintDialog())
            // {
            //     Dialog.ShowDialog();

            //     ProcessStartInfo printProcessInfo = new ProcessStartInfo()
            //     {
            //         Verb = "print",
            //         CreateNoWindow = true,
            //         FileName = @"C:\temp\LETTER TABLE SCRIPT.txt",
            //         WindowStyle = ProcessWindowStyle.Hidden
            //     };

            //     Process printProcess = new Process();
            //     printProcess.StartInfo = printProcessInfo;

            //     printProcess.Start();

            //     printProcess.WaitForInputIdle();

            //     Thread.Sleep(3000);

            //     if (false == printProcess.CloseMainWindow())
            //     {
            //         printProcess.Kill();
            //     }
            // }

            Print(pdfPath, "RICOH MP C5503 PCL 6");

        }

        public static bool Print(string file, string printer)
        {
          
            //try
            //{
                Process.Start(
                   Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion" +
                        @"\App Paths\AcroRd32.exe").GetValue("").ToString(),
                   string.Format("/h /t \"{0}\" \"{1}\"", file, "RICOH MP C5503 PCL 6"));
                return true;
            //}
            //catch { }
            //return false;
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
                    
                    flowLayoutPanel1.Controls.Add(new TextBox { Text = columnsList[i], Name = $"txtBox{i}" });
                    flowLayoutPanel2.Controls.Add(new ComboBox { DataSource = new List<string>(dtColumnsList), Name = $"combo{i}" });
                }
            }
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}
