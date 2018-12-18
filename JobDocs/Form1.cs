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
        string dataSummaryTemplate = @"S:\ADMIN\FORMS\MS DATA SUMMARY\Templates\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf";
        string productioReportTemplate = @"S:\ADMIN\FORMS\MS DATA SUMMARY\Templates\PRODUCTION REPORT SEP17 - TEMPLATE.pdf";
        static List<string> clinetsList = new List<string>();

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
                string customer = comboBoxCustomer.SelectedItem == null ? comboBoxCustomer.Text : comboBoxCustomer.SelectedItem.ToString();

                if (checkBoxDataSummary.Checked)
                {
                    string fileName = Path.GetFileName(path);
                    dataSummaryPdf = $"{jobDirectory}\\{JobNo} - Data Summary.pdf";
                    PdfReader pdfReader = new PdfReader(dataSummaryTemplate);
                    //PdfReader pdfReader = new PdfReader(@"C:\Users\Gayan\Documents\MSOL\test data\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf");

                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(dataSummaryPdf, System.IO.FileMode.OpenOrCreate)))
                    {
                        pdfStamper.AcroFields.SetField("Date", DateTime.Today.ToShortDateString());
                        pdfStamper.AcroFields.SetField("Job No", JobNo);
                        pdfStamper.AcroFields.SetField("Job Name", txtJobName.Text);
                        pdfStamper.AcroFields.SetField("Customer", customer);
                        pdfStamper.AcroFields.SetField("Original_File", fileName.Substring(fileName.IndexOf('-') + 1));

                        int j = 0;

                        for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                        {

                            ComboBox c = (ComboBox)flowLayoutPanel2.Controls[i];
                            if (c.SelectedIndex > 0 || c.Text != "")// index 0 is the displayed value which should be ignored
                            {
                                j++;
                                string column = c.SelectedItem == null ? c.Text : c.SelectedItem.ToString();
                                pdfStamper.AcroFields.SetField($"Col{j}", columnsList[i]);
                                pdfStamper.AcroFields.SetField($"dtCol{j}", column);
                                outputList.Add(column);
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
                    }           

                    pdfReader.Close();
                }

                if (checkBoxProductionReport.Checked)
                {
                    PdfReader pdfReader2 = new PdfReader(productioReportTemplate);
                    //PdfReader pdfReader2 = new PdfReader(@"C:\Users\Gayan\Documents\MSOL\test data\PRODUCTION REPORT SEP17 - TEMPLATE.pdf");

                    using (PdfStamper pdfStamper2 = new PdfStamper(pdfReader2, new System.IO.FileStream($"{jobDirectory}\\{JobNo} - Production Report.pdf", System.IO.FileMode.OpenOrCreate)))
                    {
                        pdfStamper2.AcroFields.SetField("Job No", JobNo);
                        pdfStamper2.AcroFields.SetField("Job Name", txtJobName.Text);
                        pdfStamper2.AcroFields.SetField("Customer", customer);
                        pdfStamper2.Close();
                    }
                   
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

           // button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            string clientDirPath = @"S:\DATABASES";
            foreach(string d in Directory.GetDirectories(clientDirPath))
            {
                clinetsList.Add(Path.GetFileName(d));
            }

            comboBoxCustomer.DataSource = clinetsList;


        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            columnsList.Clear();
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
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

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
