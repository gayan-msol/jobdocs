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
using System.Reflection;


namespace JobDocs
{
    public partial class Form1 : Form
    {

        string customer = "";
        string jobNo = "";
        string jobName = "";
        public static string jobDirectory = "";
        Job importedJob = new Job();
        Address address = new Address();
        List<string> columnsList = new List<string>();
        string delimiter = "\t";
        List<string> outputList = new List<string>();
        string path = "";
        int columnCount = 0;
        string dataSummaryPdf = "";
        string prodRepPdf = "";
        string dataSummaryTemplate = @"S:\SCRIPTS\DotNetProgrammes\PDF Templates\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf";
        string productioReportTemplate = @"S:\SCRIPTS\DotNetProgrammes\PDF Templates\PRODUCTION REPORT SEP17 - TEMPLATE.pdf";
        string directoryBranch = "";
        string printSize = "";
        string finishedSize = "";
        string plex = "";
        int up = 1;
        string stockSaupplier = "";
        string stockDescription = "";


        public Form1()
        {
            InitializeComponent();
        }


        private void createProductionReport(Job job,string fileName)
        {
            ProductionReport productionReport = new ProductionReport();

            productionReport.Customer = job.Customer;
            productionReport.JobName = job.JobName;
            productionReport.JobNo = job.JobNumber;
            productionReport.Qty = job.Qty;

            List<MailPackItem> returnItems = MailPackItem.GetItems(job.DocID).Where(x => x.Return).ToList();

            string[] items = returnItems.Select(y => y.SupplyDescription).ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                PropertyInfo property = productionReport.GetType().GetProperties().Single(z => z.Name == $"Item{i + 1}");
                property.SetValue(productionReport, items[i]);
            }

            productionReport.createPdf(fileName, productionReport);
        }

        private void createPdf(string fileName)
        {
                           
            PDF dsPdf = new PDF(dataSummaryTemplate);
         

            try
            {

                //if(string.IsNullOrWhiteSpace(path))
                //{
                //    SaveFileDialog saveFileDialog = new SaveFileDialog();
                //    saveFileDialog.Filter = ".pdf|PDF";
                //    saveFileDialog.ShowDialog();
                //    path = saveFileDialog?.FileName;
                //}

               // string jobDirectory = Path.GetDirectoryName(path);
                string JobNo = txtJobNo.Text != "" ? txtJobNo.Text : "0000";
                string customer = comboBoxCustomer.SelectedItem == null ? comboBoxCustomer.Text : comboBoxCustomer.SelectedItem.ToString();

                    string drFileName = Path.GetFileName(path);
                    //dataSummaryPdf = $"{jobDirectory}\\{JobNo} - Data Summary.pdf";
                    PdfReader pdfReader = new PdfReader(dataSummaryTemplate);
                    //PdfReader pdfReader = new PdfReader(@"C:\Users\Gayan\Documents\MSOL\test data\DATA SUMMARY SHEET - APR18 - TEMPLATE.pdf");

                    using (PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate)))
                    {
                        pdfStamper.AcroFields.SetField("Date", DateTime.Today.ToShortDateString());
                        pdfStamper.AcroFields.SetField("Job No", JobNo);
                        pdfStamper.AcroFields.SetField("Job Name", txtJobName.Text);
                        pdfStamper.AcroFields.SetField("Customer", customer);
                        pdfStamper.AcroFields.SetField("Original_File", drFileName.Substring(drFileName.IndexOf('-') + 1));

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

                        List<string> addBlockList = Address.GetAddressBlock(outputList);
                        for(int k=0;k<addBlockList.Count;k++)
                        {
                            pdfStamper.AcroFields.SetField($"Out{k+1}", addBlockList[k]);
                        }

                    }           

                    pdfReader.Close();

            }
            catch(Exception e)
            {
         
                MessageBox.Show(e.Message);
            }

       
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = richTextJobDirectory.Text;
            saveFileDialog.FileName = $"{txtJobNo.Text} - Data Summary Sheet.pdf";
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.ShowDialog();
            if(saveFileDialog.FileName != null)
            {
                createPdf(saveFileDialog.FileName);
            }

          

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
          //  tabControl1.SelectedIndex = 1;
            txtJobNo.Select();
            rbDatabase.Checked = true;
            rbLSimplex.Checked = true;

            cmbFinishedSize.DataSource = StockItem.finishSizeList;
            cmbPrintSize.DataSource = StockItem.printSizeList;
            txtCustomFinishedSize.Enabled = txtCustomPrintSize.Enabled = false;

        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            columnsList.Clear();
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            List<string> itemsList = Address.getDtFieldList();

            foreach (string file in files)
            {
                if (file.Substring(file.Length - 4, 4) == ".txt")
                {
                    path = file;
                }
                columnsList = JobData.GetColumnList(path, delimiter);
            }

            if (columnsList != null)
            {
                columnCount = columnsList.Count;
                for(int i=0;i<columnsList.Count;i++)
                {                  
                    flowLayoutPanel1.Controls.Add(new TextBox { Text = columnsList[i], Size = new System.Drawing.Size(150, 25), Name = $"txtBox{i}" });
                    ComboBox comboBox = new ComboBox { Size = new System.Drawing.Size(150, 25) };
                    flowLayoutPanel2.Controls.Add(comboBox);
                    comboBox.DataSource = new List<string>(itemsList);
                    comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                    string selection = itemsList.Where(x =>StringProcessing.MatchStrings(x, columnsList[i]) < 2).FirstOrDefault();
                    if (selection != null)
                    {
                        comboBox.SelectedItem = selection;
                    }
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
           // groupBoxColour.Enabled = checkBox7100.Checked;
        }

        private void btnChangeJobDirectory_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = richTextJobDirectory.Text;
            DialogResult result = openFileDialog.ShowDialog();
            if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                richTextJobDirectory.Text =Path.GetDirectoryName(openFileDialog.FileName);
                cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(richTextJobDirectory.Text);

            }
        }
     
        private void btnImportFromDolphin_Click(object sender, EventArgs e)
        {
            importedJob = Job.GetJob(txtJobNo.Text);
            if(importedJob != null)
            {
                comboBoxCustomer.SelectedItem = importedJob.Customer;
                jobName = txtJobName.Text = importedJob.JobName;
                customer = txtCustomer.Text = importedJob.Customer;
                jobNo = txtJobNo.Text;
                jobDirectory = richTextJobDirectory.Text = DirectoryHelper.getJobDir(jobNo, customer, directoryBranch);

                cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(jobDirectory);

                if (importedJob.DocID != null)
                {
                    List<JobProcess> printProcesses = importedJob.ProcessList.Where(x => x.Name.Contains("Laser - Print") || x.Name.Contains("Print Envelopes") || x.Name.Contains("Ink Jet")).ToList();
                    cmbPrintJobs.DataSource = printProcesses;
                    cmbPrintJobs.DisplayMember = "Name";
                    cmbPrintJobs.Refresh();
                }
            }
            else
            {
                ErrorHandling.ShowMessage(null, "Could not import the job.");
            }
  
        }

        private void cmbPrintJobs_SelectedIndexChanged(object sender, EventArgs e)
        {

            JobProcess selectedProcess =(JobProcess) cmbPrintJobs.SelectedItem;
            PrintInfo printInfo = importedJob.PrintInfoList.Where(x => x.ProcessID == selectedProcess.ID).FirstOrDefault();
            MailPackItem stockItem = importedJob.ItemList.Where(x => x.LinkedTo == selectedProcess.LinkTo && !string.IsNullOrWhiteSpace(x.Description) ).FirstOrDefault();


                setPrintSize(printInfo);
            cmbFinishedSize.SelectedItem = printInfo.FinishedSize;
                setPlex(printInfo);
                setPrintmachine(selectedProcess, printInfo);
                numericUpDownUp.Value = printInfo?.Up ?? 1;
            numericUpDownStreamQty.Value = printInfo.Qty;
            

            if (stockItem != null)
            {
                setStockInfo(stockItem);
                cmbStream.DataSource = stockItem.Stream.Split(',').ToList();
            }

            lblPrintDescription.Text = selectedProcess.Description;
        
        }

        private void setStockInfo(MailPackItem stockItem)
        {
            rbSMSOL.Checked = stockItem.SuppliedBy == "Mailing Solutions";
            rbSCustomer.Checked= stockItem.SuppliedBy == "Customer";
            txtStockDescription.Text = stockItem.SuppliedBy == "Mailing Solutions" ? stockItem.Name : stockItem.SupplyDescription;
        }

        private void setPrintSize(PrintInfo printInfo)
        {
            if(printInfo!= null)
            {
                cmbPrintSize.SelectedItem = printInfo?.PrintSize;
            }
            else
            {

            }

            //Control[] controls = groupBoxPaper.Controls.Find($"rb{printInfo?.PrintSize}", true);
            //if(controls.Length >0)
            //{
            //    Control size = controls[0];
            //    size.Select();
            //}

        }

        private void setPlex(PrintInfo printInfo)
        {
            //cmbFinishedSize.SelectedItem = printInfo?.FinishedSize;

            Control[] controls = groupBoxLayout.Controls.Find($"rbL{printInfo?.Sides}", true);
            if (controls.Length > 0)
            {
                Control plex = controls[0];
                plex.Select();
            }
        }

        private void setPrintmachine(JobProcess selectedProcess, PrintInfo printInfo)
        {
            if (selectedProcess.Name.Contains("Laser"))
            {
                rbM8120.Checked = (printInfo.Colour == "Black");
                rbM7100.Checked = (printInfo.Colour == "Colour");
            }
            else if(selectedProcess.Name.Contains("Ink Jet"))
            {
                rbMInkjet.Checked = true;
            }
            else if (selectedProcess.Name.Contains("Envelopes"))
            {
                rbMDuplo.Checked = true;
            }

        }

        private void btnAddStream_Click(object sender, EventArgs e)
        {
            dataGridViewStreams.Rows.Add(SetStreamDetails());

        }

        private string[] SetStreamDetails()
        {
            decimal up = numericUpDownUp.Value;
            decimal recQty = numericUpDownStreamQty.Value;
            decimal sheetsPerRec = numericUpDownSheetsPerRec.Value;
            int printQty = 0;
            printQty = calculatePrintQty(recQty, up, sheetsPerRec);
            
            return new string[] {/* cmbStream?.SelectedItem?.ToString() + */cmbStream?.Text, numericUpDownStreamQty.Value.ToString(),printQty.ToString()};
        }

        private int calculatePrintQty(decimal recQty, decimal recsPerPage, decimal sheetsPerRec)
        {
            if(recsPerPage > 0)
            {
                return (int)Math.Ceiling(recQty * sheetsPerRec / recsPerPage);
            }
            else
            {
                return 0;
            }
        }

        private void btnSampleSheet_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["frmSampleSheet"];
            if(form == null)
            {
                Form sampleSheet = new frmSampleSheet();
                sampleSheet.ShowDialog();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintSpecSheet_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = richTextJobDirectory.Text;
            saveFileDialog.FileName = $"{txtJobNo.Text} - Print Spec Sheet - {getPrintMachine()}";
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.ShowDialog();
            if(saveFileDialog.FileName != null)
            {

             

                PrintSpecSheet printSpecSheet = new PrintSpecSheet();

                List<string> streamList = new List<string>();
                foreach (DataGridViewRow row in dataGridViewStreams.Rows)
                {
                    if(row.Cells[0].Value != null)
                    {
                        streamList.Add($"Stream {row.Cells["Stream"]?.Value} : Record Qty = {row.Cells["RecordQty"]?.Value}  : Print Qty = {row.Cells["PrintQty"]?.Value}");

                    }
                }

                printSpecSheet.StreamList = streamList;
                printSpecSheet.JobNo = txtJobNo.Text;
                printSpecSheet.JobDirectory = richTextJobDirectory.Text;
                printSpecSheet.FileName = cmbFileName?.Text;
                printSpecSheet.PrintMachine = getPrintMachine();
                printSpecSheet.PrintSize = getPrintSize();
                printSpecSheet.FinishedSize = getFinishedSize();
                printSpecSheet.Notes = richTexNotes.Text;
                printSpecSheet.Stock = getStockDetails();
                printSpecSheet.Layout = getLayoutInfo();
                printSpecSheet.createPdf( saveFileDialog.FileName, printSpecSheet);
              //  Print(saveFileDialog.FileName, "RICOH MP C5503 PCL 6");
            }


        }

        private string getPrintMachine()
        {
           // List<RadioButton> rbList = groupBoxPrintMachine.Controls.OfType<RadioButton>();

            foreach (Control item in groupBoxPrintMachine.Controls)
            {
                if(item is RadioButton radioButton && radioButton.Checked)
                {
                    return item.Name.Substring(3);
                }
            }
            return null;
        }

        private string getCheckedRadioButtonValue(GroupBox groupBox)
        {
            foreach (Control item in groupBox.Controls)
            {
                if (item is RadioButton radioButton && radioButton.Checked)
                {
                    return item.Name.Substring(3);
                }
            }

            return null;
        }

        private string getPrintSize()
        {
            // string printSize = getCheckedRadioButtonValue(groupBoxPaper);
            string printSize = cmbPrintSize?.SelectedItem.ToString();
            if (printSize == "Custom")
            {
                printSize = txtCustomPrintSize.Text;
            }

            return printSize;
        }

        private string getFinishedSize()
        {
            // string finishedSize = getCheckedRadioButtonValue(groupBoxFinishedSize);
            string finishedSize = cmbFinishedSize?.SelectedItem.ToString();
            if (finishedSize == "Custom")
            {
                finishedSize = txtCustomFinishedSize.Text;
            }

            return finishedSize;
        }

        private string getStockDetails()
        {
            if (rbSMSOL.Checked)
            {
                return $"MSOL Stock: {txtStockDescription.Text}";
            }
            else
            {
                return $"Customer Supplied Stock: {txtStockDescription.Text}";
            }
        }

        private string getLayoutInfo()
        {
            string plex = getCheckedRadioButtonValue(groupBoxLayout) ?? "Simplex";
            int sheetsPerSet = (int)numericUpDownSheetsPerRec.Value;
            if(sheetsPerSet>1)
            {
                return $"{plex} - {numericUpDownUp.Value.ToString()} UP - {sheetsPerSet} Sheets Per Set";

            }
            return $"{plex} - {numericUpDownUp.Value.ToString()} UP ";
        }

        private void rb7100_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxColour.Enabled = rbM7100.Checked;
        }

        private void rbDatabase_CheckedChanged(object sender, EventArgs e)
        {
            directoryBranch = rbDatabase.Checked ? DirectoryHelper.databaseBranch : DirectoryHelper.artworkBranch;
            if(!string.IsNullOrWhiteSpace( txtJobNo.Text))
            {
                richTextJobDirectory.Text = DirectoryHelper.getJobDir(txtJobNo.Text, txtCustomer.Text, directoryBranch);
                cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(richTextJobDirectory.Text);

            }
        }

        private void cmbPrintSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomPrintSize.Enabled = (cmbPrintSize.SelectedItem?.ToString() == "Custom");
        }

        private void cmbFinishedSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomFinishedSize.Enabled = (cmbFinishedSize?.SelectedItem?.ToString() == "Custom");
        }

        private void tbnClearStreams_Click(object sender, EventArgs e)
        {
            dataGridViewStreams.Rows.Clear();
        }

        private void btnProductionReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = richTextJobDirectory.Text;
            saveFileDialog.FileName = $"{txtJobNo.Text} - Production Report.pdf";
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != null)
            {
                createProductionReport(importedJob, saveFileDialog.FileName);
            }
        }

        private void rbArtwork_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
} 
