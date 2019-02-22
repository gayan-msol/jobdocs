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
using JobDocsLibrary;

namespace JobDocs
{
    public partial class Form1 : Form
    {

        string customer = "";
        string jobNo = "";
        string jobName = "";
        string jobDirectory = "";
        Job importedJob = new Job();
        Address address = new Address();
        List<string> columnsList = new List<string>();
        List<PrintInfo> printInfoList = new List<PrintInfo>();
        Dictionary<string, PrintInfo> printProcessList = new Dictionary<string, PrintInfo>();
        List<MailPackItem> itemList = new List<MailPackItem>();

        List<string> outputList = new List<string>();
        string path = "";
        int columnCount = 0;
        string dataSummaryPdf = "";
        string prodRepPdf = "";
        string dataSummaryTemplate = @"S:\SCRIPTS\DotNetProgrammes\PDF Templates\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf";
        string productioReportTemplate = @"S:\SCRIPTS\DotNetProgrammes\PDF Templates\PRODUCTION REPORT SEP17 - TEMPLATE.pdf";
        static List<string> clientsList = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void createPdf()
        {
            PDF dsPdf = new PDF(dataSummaryTemplate);
         

            try
            {

                if(string.IsNullOrWhiteSpace(path))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = ".pdf|PDF";
                    saveFileDialog.ShowDialog();
                    path = saveFileDialog?.FileName;
                }

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
                        outputList.Clear();

                        for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                        {

                            ComboBox c = (ComboBox)flowLayoutPanel2.Controls[i];
                            if (c.SelectedIndex > 0 || (c.Text != ""&& c.Text != "<Select or Enter DT Field>"))// index 0 is the displayed value which should be ignored
                            {
                                j++;
                                string column = c.SelectedItem == null ? c.Text : c.SelectedItem.ToString();
                                pdfStamper.AcroFields.SetField($"Col{j}", columnsList[i]);
                                pdfStamper.AcroFields.SetField($"dtCol{j}", column);
                                outputList.Add(column);
                            }
                        }

                        List<string> addBlockList = BL_JobDocs.addressBlock(outputList);
                        for(int k=0;k<addBlockList.Count;k++)
                        {
                            pdfStamper.AcroFields.SetField($"Out{k+1}", addBlockList[k]);
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
                System.Diagnostics.Process.Start(
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
   

            tabControl1.SelectedIndex = 1;
            txtJobNo.Select();
            //foreach (string d in Directory.GetDirectories(clientDirPath))
            //{
            //    clientsList.Add(Path.GetFileName(d));
            //}
            //foreach (string d in Directory.GetDirectories(miscDirpath))
            //{
            //    clientsList.Add(Path.GetFileName(d));
            //}

            //comboBoxCustomer.DataSource = clientsList;


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
                    flowLayoutPanel2.Controls.Add(new ComboBox { DataSource = new List<string>(Address.getDtFieldList()),Size=new System.Drawing.Size(150,25), Name = $"combo{i},", AutoCompleteMode=AutoCompleteMode.SuggestAppend,AutoCompleteSource=AutoCompleteSource.ListItems });
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
        {/*
            jobNo = txtJobNo.Text;
            txtJobName.ResetText();
            customer = comboBoxCustomer.SelectedItem.ToString();
            jobName = "";
            if (Directory.Exists($"{clientDirPath}\\{customer}"))
            {
                string[] jobList = Directory.GetDirectories($"{clientDirPath}\\{customer}");
                foreach (string s in jobList)
                {
                    jobName = Path.GetFileName(s);

                    if (jobName.Contains(jobNo))
                    {

                        txtJobName.Text = jobName.Replace($"JOB {jobNo}", "");
                        jobDirectory = jobName;
                        txtJobDirectory.Text = s;
                        break;
                    }

                }
            }
            else if (Directory.Exists($"{miscDirpath}\\{customer}"))
            {
                string[] jobList = Directory.GetDirectories($"{miscDirpath}\\{customer}");
                foreach (string s in jobList)
                {
                    jobName = Path.GetFileName(s);

                    if (jobName.Contains(jobNo))
                    {

                        txtJobName.Text = jobName.Replace($"JOB {jobNo}", "");
                        jobDirectory = jobName;
                        txtJobDirectory.Text = s;

                        break;
                    }

                }
            }*/
        }



        private void btnSecondStream_Click(object sender, EventArgs e)
        {

        }

        private void checkBox7100_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxColour.Enabled = checkBox7100.Checked;
        }

        private void btnChangeJobDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                richTextJobDirectory.Text = folderBrowser.SelectedPath;
            }
        }

       


        private void btnImportFromDolphin_Click(object sender, EventArgs e)
        {
            importedJob = Job.GetJob(txtJobNo.Text);
            comboBoxCustomer.SelectedItem = importedJob.Customer;
            jobName = txtJobName.Text = importedJob.JobName;
            customer = txtCustomer.Text = importedJob.Customer;
            jobNo = txtJobNo.Text;
            jobDirectory= richTextJobDirectory.Text = DirectoryHelper.getJobDir(jobNo,customer,DirectoryHelper.databaseBranch);

            cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(jobDirectory);


            if(importedJob.DocID != null)
            {
                List<JobProcess> printProcesses = importedJob.ProcessList.Where(x => x.Name.Contains("Laser - Print")).ToList();
                cmbPrintJobs.DataSource = printProcesses;
                cmbPrintJobs.DisplayMember = "Name";
                cmbPrintJobs.Refresh();              
            }
        }

        private void cmbPrintJobs_SelectedIndexChanged(object sender, EventArgs e)
        {

            JobProcess selectedProcess =(JobProcess) cmbPrintJobs.SelectedItem;
            PrintInfo printInfo = importedJob.PrintInfoList.Where(x => x.ProcessID == selectedProcess.ID).FirstOrDefault();
            MailPackItem stockItem = importedJob.ItemList.Where(x => x.LinkedTo == selectedProcess.LinkTo && !string.IsNullOrWhiteSpace(x.Description) ).FirstOrDefault();

            if(printInfo != null)
            {
                setPrintSize(printInfo);
                setPlex(printInfo);
                setPrintmachine(printInfo);
                numericUpDownUp.Value = printInfo.Up;
            }

            if(stockItem != null)
            {
                setStockInfo(stockItem);
                cmbStream.DataSource = stockItem.Stream.Split(',').ToList();
            }

            lblPrintDescription.Text = selectedProcess.Description;
        
        }

        private void setStockInfo(MailPackItem stockItem)
        {
            rbMSOLStock.Checked = stockItem.SuppliedBy == "Mailing Solutions";
            rbCustomerStock.Checked= stockItem.SuppliedBy == "Customer";
            txtStockDescription.Text = stockItem.SuppliedBy == "Mailing Solutions" ? stockItem.Name : stockItem.SupplyDescription;
        }

        private void setPrintSize(PrintInfo printInfo)
        {
            Control size = groupBoxPaper.Controls.Find($"rb{printInfo.PrintSize}", true)[0];
            size.Select();
        }

        private void setPlex(PrintInfo printInfo)
        {
            Control plex = groupBoxPlex.Controls.Find($"rb{printInfo.Sides}", true)[0];
            plex.Select();
        }

        private void setPrintmachine(PrintInfo printInfo)
        {        
            checkBox8120.Checked = printInfo.Colour == "Black";
            checkBox7100.Checked = printInfo.Colour == "Colour";            
        }

        private void btnAddStream_Click(object sender, EventArgs e)
        {
            listBoxStreams.Items.Add(SetStreamDetails());

        }

        private string SetStreamDetails()
        {
            decimal up = numericUpDownUp.Value;
            decimal recQty = numericUpDownStreamQty.Value;
            int printQty = (int)Math.Ceiling(recQty/up);
            return ( $"Stream {cmbStream.SelectedItem} - Record Qty:{numericUpDownStreamQty.Value} - Print Qty:{printQty}");
        }
    }
} 
