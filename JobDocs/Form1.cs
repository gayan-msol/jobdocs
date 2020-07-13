﻿using System;
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
using DolphinLibrary;
using System.Reflection;
using System.Security;
using System.Globalization;
using ElmsLibrary;

namespace JobDocs
{
    public partial class Form1 : Form
    {

        string customer = "";
        public static string jobNo = "";
        public static string jobName = "";
        public static string jobDirectoryData = "";
        public static string jobDirectoryArt = "";
        string manifestFile = "";
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
        //string productioReportTemplate = @"S:\SCRIPTS\DotNetProgrammes\PDF Templates\PRODUCTION REPORT SEP17 - TEMPLATE.pdf";
        //string directoryBranch = "";
        //string printSize = "";
        //string finishedSize = "";
        //string plex = "";
        //int up = 1;
        //string stockSaupplier = "";
        //string stockDescription = "";
        string outputFileName = "";
        public static string userName;
        ElmsUser elmsUser = new ElmsUser();


        public Form1()
        {
            InitializeComponent();
            userName = getCurrentUser();
        }

        private string getCurrentUser()
        {
            string winuser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
           string[] temp = winuser.Split('\\');

            string un = temp[temp.Length - 1];
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            return textInfo.ToTitleCase(un.Replace(".", " "));
        }

        private void createProductionReport(DolphinLibrary.Job job,string fileName)
        {
            ProductionReport productionReport = new ProductionReport();

            productionReport.Customer = job.Customer;
            productionReport.JobName = job.JobName;
            productionReport.JobNo = job.JobNumber;
            productionReport.Qty = job.Qty;

           
            List<string> items = new List<string>();
            foreach (DataGridViewRow row in dataGridViewReturnItems.Rows)
            {
                if (!string.IsNullOrEmpty(row?.Cells[0]?.Value?.ToString()))
                {
                    items.Add(row?.Cells[0]?.Value?.ToString());
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                PropertyInfo property = productionReport.GetType().GetProperties().SingleOrDefault(z => z.Name == $"Item{i+1}");
                if(property != null)
                {
                    property.SetValue(productionReport, items[i]);
                }
          
            }
            Printing p = new Printing();
            p.PrintPRoductionReport(productionReport);
            productionReport.createPdf(fileName, productionReport);
        }

        private void createPdf(string fileName)
        {

            JobDocsLibrary.PDF dsPdf = new JobDocsLibrary.PDF(dataSummaryTemplate);
         

            try
            {

                string JobNo = txtJobNo.Text != "" ? txtJobNo.Text : "0000";
                string customer = txtCustomer.Text;

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

                ErrorHandling.ShowMessage(e);
            }

       
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = richTextJobDirectory.Text;
                saveFileDialog.FileName = $"{txtJobNo.Text} - Data Summary Sheet.pdf";
                saveFileDialog.Filter = "PDF|*.pdf";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName != null)
                {
                    createPdf(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowMessage(ex);
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
                ErrorHandling.ShowMessage(ex);
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
            { ErrorHandling.ShowMessage(ex); }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {  
            tabControl1.SelectedIndex = 1;
            txtJobNo.Select();
            rbDatabase.Checked = true;
            rbLSimplex.Checked = true;

            cmbFinishedSize.DataSource = StockItem.finishSizeList;
            cmbPrintSize.DataSource = StockItem.printSizeList;
        //    txtCustomFinishedSize.Enabled = txtCustomPrintSize.Enabled = false;
            cmbGuillo.Text = "NO";

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
                    string selection = itemsList.Where(x => x == columnsList[i]).FirstOrDefault();
                    if(selection == null)
                    {
                        selection = itemsList.Where(x => StringProcessing.MatchStrings(x, columnsList[i]) < 2).FirstOrDefault();
                    }
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
                jobName = txtJobName.Text = importedJob.JobName;
                customer = txtCustomer.Text = importedJob.Customer;
                jobNo = txtJobNo.Text;
               // jobDirectory = richTextJobDirectory.Text = DirectoryHelper.getJobDir(jobNo, customer, directoryBranch);
                jobDirectoryData = richTextJobDirectory.Text = importedJob.DataFolder;
                jobDirectoryArt = importedJob.ArtworkFolder;
                txtCompany.Text = customer;
                txtContact.Text = importedJob.Contact;

                cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(jobDirectoryData);

                if (importedJob.DocID != null)
                {
                    // temp
                 //   var test = Lodgement.GetLodgementDetails(importedJob.DocID);

                    //temp


                    List<JobProcess> printProcesses = importedJob.ProcessList.Where(x => x.Name.Contains("Laser - Print") || x.Name.Contains("Print Envelopes") || x.Name.Contains("Ink Jet")).ToList();
                    cmbPrintJobs.DataSource = printProcesses;
                    cmbPrintJobs.DisplayMember = "Name";
                    cmbPrintJobs.Refresh();

                    List<MailPackItem> returnItems = MailPackItem.GetItems(importedJob.DocID).Where(x => x.Return).ToList();
                    string[] items = returnItems.Select(y => y.SupplyDescription).ToArray();
                    dataGridViewReturnItems.Rows.Clear();
                    foreach (string item in items)
                    {
                        dataGridViewReturnItems.Rows.Add(item);
                    }
                   

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

            numericUpDownStreamQty.Value = selectedProcess?.Qty ?? 0;

            setPrintSize(printInfo);
            cmbFinishedSize.SelectedItem = printInfo?.FinishedSize;
                setPlex(printInfo);
                setPrintmachine(selectedProcess, printInfo);
                numericUpDownUp.Value = printInfo?.Up ?? 1;
          
            

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
                if(txtStockDescription.Text.Length > 3)
                {
                    cmbPrintSize.Text = txtStockDescription.Text.Substring(0, 3);
                }
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
            
            return new string[] {cmbStream?.Text, numericUpDownStreamQty.Value.ToString(),numericUpDownSheetsPerRec.Value.ToString(), printQty.ToString()};
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
                Form sampleSheet = new frmSampleSheet(outputFileName, getDelimeter());
                sampleSheet.ShowDialog();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintSpecSheet_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    PrintSpecSheet printSpecSheet = new PrintSpecSheet();

                    List<string> streamList = new List<string>();
                    foreach (DataGridViewRow row in dataGridViewStreams.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            streamList.Add($"Stream {row.Cells["Stream"]?.Value} : Print Qty = {row.Cells["PrintQty"]?.Value} : Record Qty = {row.Cells["RecordQty"]?.Value}  : {row.Cells["Sheets"]?.Value} Sheets ");

                        }
                    }

                    printSpecSheet.StreamList = streamList;
                    printSpecSheet.JobNo = txtJobNo.Text;
                    printSpecSheet.JobName = txtJobName.Text;
                    printSpecSheet.JobDirectory = richTextJobDirectory.Text;
                    printSpecSheet.FileNames = getFileNames();
                    printSpecSheet.PrintMachine = getPrintMachine();
                    printSpecSheet.PrintSize = getPrintSize();
                    printSpecSheet.FinishedSize = getFinishedSize();
                    printSpecSheet.Notes = richTexNotes.Text;
                    printSpecSheet.Stock = getStockDetails();
                    printSpecSheet.Layout = getLayoutInfo();
                    printSpecSheet.Guillotine = cmbGuillo?.Text;
                    printSpecSheet.Customer = importedJob.Customer;
                    printSpecSheet.Contact = txtContact.Text;
                    printSpecSheet.Approved = checkBoxApproval.Checked;
                    printSpecSheet.AddInkJet = checkBoxAddInkJet.Checked;

                    print(printSpecSheet);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.ShowMessage(ex);
            }


        }

        private async Task print(PrintSpecSheet printSpecSheet)
        {
            Printing p = new Printing();

           var result =   await p.PrintPSS(printSpecSheet);
        }
        

        private string getPrintMachine()
        {
            if (checkBoxAddInkJet.Checked)
            {
                return "DUPLO -> INKJET";
            }
            else
            {
                foreach (Control item in groupBoxPrintMachine.Controls)
                {
                    if (item is RadioButton radioButton && radioButton.Checked)
                    {
                        if (radioButton.Name == "rbM7100")
                        {
                            if (rbCColour.Checked)
                            {
                                return "7100 - Colour";
                            }
                            else
                            {
                                return "7100 - Black";
                            }
                        }

                        return item.Name.Substring(3);
                    }
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
            string printSize = cmbPrintSize?.Text;
            //if (printSize == "Custom")
            //{
            //    printSize = txtCustomPrintSize.Text;
            //}

            return printSize;
        }

        private string getFinishedSize()
        {
            string finishedSize = cmbFinishedSize?.Text;
            //if (finishedSize == "Custom")
            //{
            //    finishedSize = txtCustomFinishedSize.Text;
            //}

            return finishedSize;
        }

        private string getStockPrefix()
        {
            string stockPrefix = rbSMSOL.Checked ? "MSOL" : "";
            stockPrefix = rbSCustomer.Checked ? "Customer" : stockPrefix;
            return stockPrefix;
        }

        private List<string> getStockDetails()
        {
            List<string> stockLines = new List<string>();

            if (listBoxStock.Items.Count == 0)
            {
                stockLines.Add(txtStockDescription.Text);
            }
            foreach (string s in listBoxStock.Items)
            {
                stockLines.Add(s);
            }

            return stockLines;
        }

        private List<string> getFileNames()
        {
            List<string> fileNames = new List<string>();
          if(listBoxFileNames.Items.Count == 0)
            {
                fileNames.Add(cmbFileName?.Text);
            }
            foreach (string s in listBoxFileNames.Items)
            {
                fileNames.Add(s);
            }
                                
            return fileNames;
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
            groupBoxColour.Visible = rbM7100.Checked;
        }

        private void rbDatabase_CheckedChanged(object sender, EventArgs e)
        {
            //directoryBranch = rbDatabase.Checked ? DirectoryHelper.databaseBranch : DirectoryHelper.artworkBranch;
            //if(!string.IsNullOrWhiteSpace( txtJobNo.Text))
            //{
            //    richTextJobDirectory.Text = DirectoryHelper.getJobDir(txtJobNo.Text, txtCustomer.Text, directoryBranch);
            //    cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(richTextJobDirectory.Text);

            //}
            if(rbDatabase.Checked)
            {
                richTextJobDirectory.Text = importedJob.DataFolder;
                cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(richTextJobDirectory.Text);
            }
            else
            {
                richTextJobDirectory.Text = importedJob.ArtworkFolder;
                cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(richTextJobDirectory.Text);
            }

        }

        private void cmbPrintSize_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  txtCustomPrintSize.Enabled = (cmbPrintSize?.Text == "Custom");

            cmbFinishedSize.Text = cmbPrintSize.Text;

        }

        private void cmbFinishedSize_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    txtCustomFinishedSize.Enabled = (cmbFinishedSize?.Text == "Custom");
            if(cmbFinishedSize?.Text != cmbPrintSize?.Text)
            {
                cmbGuillo.Text = "YES";
            }
            else
            {
                cmbGuillo.Text = "NO";
            }
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
            //directoryBranch = rbDatabase.Checked ? DirectoryHelper.databaseBranch : DirectoryHelper.artworkBranch;
            //if (!string.IsNullOrWhiteSpace(txtJobNo.Text))
            //{
            //    richTextJobDirectory.Text = DirectoryHelper.getJobDir(txtJobNo.Text, txtCustomer.Text, directoryBranch);
            //    cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(richTextJobDirectory.Text);

            //}
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

 
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text | *.txt| CSV | *.csv";
            openFileDialog.InitialDirectory = Form1.jobDirectoryData;
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
           
                richTextOutputFilePath.Text = outputFileName = openFileDialog.FileName;
                //  wizardPage1.AllowNext = true;

            }
        }

        private string getDelimeter()
        {
            string delimeter = "";
            if (rbComma.Checked)
            {
                return ",";
            }
            else
            {
                return "\t";
            }
        }

        private void btnAddFileName_Click(object sender, EventArgs e)
        {
            if(listBoxFileNames.Items.Count == 7)
            {
                MessageBox.Show("You cannot add anymore lines.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                listBoxFileNames.Items.Add($"Stream {cmbStreamFN?.Text.ToUpper()} - {cmbFileName?.Text}");
            }
        }

        private void btnRemoveFileName_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(listBoxFileNames);
            selectedItems = listBoxFileNames.SelectedItems;
            if(listBoxFileNames.SelectedIndex > -1)
            {
                for(int i=selectedItems.Count-1; i >=0; i--)
                {
                    listBoxFileNames.Items.Remove(selectedItems[i]);
                }
            }

           
        }

        private void btnRemoveStock_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(listBoxStock);
            selectedItems = listBoxStock.SelectedItems;
            if (listBoxStock.SelectedIndex > -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    listBoxStock.Items.Remove(selectedItems[i]);
                }
            }
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            if (listBoxStock.Items.Count == 5)
            {
                MessageBox.Show("You cannot add anymore lines.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                listBoxStock.Items.Add($"Stream {cmbStreamStock?.Text.ToUpper()}: {getStockPrefix()} - {txtStockDescription.Text}");
            }
        }

        private void rbMDuplo_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAddInkJet.Enabled = rbMDuplo.Checked;

            if (!rbMDuplo.Checked)
            {
                checkBoxAddInkJet.Checked = false;
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void checkBoxAddInkJet_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAddInkJet.Checked)
            {
                if (!cmbStreamFN.Items.Contains("DUPLO"))
                {
                    cmbStreamFN.Items.Add("DUPLO");
                }
                if (!cmbStreamFN.Items.Contains("INKJET"))
                {
                    cmbStreamFN.Items.Add("INKJET");
                }
            }
            else
            {
                if (cmbStreamFN.Items.Contains("DUPLO"))
                {
                    cmbStreamFN.Items.Remove("DUPLO");
                }
                if (cmbStreamFN.Items.Contains("INKJET"))
                {
                    cmbStreamFN.Items.Remove("INKJET");
                }
            }
        }

        private void btnSaveElmsLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtElmsUN.Text) && !string.IsNullOrWhiteSpace(txtElmsPWD.Text))
            {
                try
                {
                    ElmsUser user = new ElmsUser() { UserName = txtElmsUN.Text.Trim(), Password = txtElmsPWD.Text.Trim(), WindowsUser = userName };
                    DataAccess.saveElmsLogin(user);
                    MessageBox.Show("Login saved.");
                    LoginPanel.Visible = false;
                    LodgePanel.Visible = true;
                }
                catch(Exception ex)
                {
                    ErrorHandling.ShowMessage(ex);
                }
              
            }
            else
            {
                MessageBox.Show("Please enter username & passowrd.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnBrowseManifest_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            if(openFileDialog.FileName != null)
            {
                txtManifestFileName.Text = openFileDialog.FileName;
            }
        }

        private void btnLodge_Click(object sender, EventArgs e)
        {
            ElmsLibrary.Lodgement lodgement = new ElmsLibrary.Lodgement();
            lodgement.AccNo = importedJob.PostAccts.Where(x => x.AccType == "Aust Post").ToList()[0].AccNo;
            lodgement.JobName = txtJobName.Text;
            lodgement.JobNo = jobNo;
            if(importedJob.PostAccts.Exists(x => x.AccType != "Aust Post"))
            {
                lodgement.RegName = importedJob.PostAccts?.Where(x => x.AccType != "Aust Post").ToList()?[0].AccType ?? "";
                lodgement.RegNo = importedJob.PostAccts.Where(x => x.AccType != "Aust Post").ToList()[0].AccNo;
            }
    
            lodgement.ServiceType = cbServiceType?.SelectedItem.ToString();
            lodgement.Size = cbSize?.SelectedItem.ToString();
            lodgement.SortType = "Pre-Sort";
            lodgement.SortList = lodgement.ReadManifest(txtManifestFileName.Text, lodgement.SortType);

            /// add weight

            eLMS.Lodge(lodgement, elmsUser);

        }

        private void rbPre_Sort_CheckedChanged(object sender, EventArgs e)
        {
            cbSize.DataSource = DataAccess.GetSizes("Pre-Sort");
        }

        private void rbPrintPost_CheckedChanged(object sender, EventArgs e)
        {
            cbSize.DataSource = DataAccess.GetSizes("Print Post");           
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            elmsUser = DataAccess.GetElmsUser(userName);
            elmsUser.UserName = elmsUser.UserName.Trim();

            LodgePanel.Visible = elmsUser != null;
            LoginPanel.Visible = elmsUser == null;
            LodgePanel.Dock = DockStyle.Fill;
            LoginPanel.Dock = DockStyle.Fill;

            manifestFile = DirectoryHelper.GetManifestFile(jobDirectoryData, jobNo);

            txtManifestFileName.Text = manifestFile;

           // ElmsLibrary.Lodgement lodgement = new ElmsLibrary.Lodgement();

            

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            richTextOutputFilePath.Clear();
        }
    }
} 
