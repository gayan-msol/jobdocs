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
using Dolphin;
using System.Reflection;
using System.Security;
using System.Globalization;
using ElmsLibrary;
using HelperLibrary;

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
        string outputFileName = "";
        public static string userName;
        ElmsUser elmsUser = new ElmsUser();
        SortSummary sortSummary = new SortSummary();
        string sortType = "";
        string serviceType = "Regular";
        string size = "Small";
        DataTable sourceTable = new DataTable();
        List<Lodgement> lodgements = new List<Lodgement>();
        List<string> remainingItems = new List<string>();
        List<string> dtColList = new List<string>();
        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
        int fullratetagCount = 0;
        int intTagCount = 0;
        


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

        private void createProductionReport(Dolphin.Job job,string fileName)
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
            //Printing p = new Printing();
            //p.PrintPRoductionReport(productionReport);
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

                    for (int i = 0; i < tableLayoutPanel.Controls.Count; i++)
                    {

                        if (tableLayoutPanel.Controls[i] is ComboBox combo)
                        {
                            if (combo.SelectedIndex > 0 || (combo.Text != "" && combo.Text != "<Select or Enter DT Field>"))// index 0 is the displayed value which should be ignored
                            {
                                j++;
                                string column = combo.SelectedItem == null ? combo.Text : combo.SelectedItem.ToString();
                                pdfStamper.AcroFields.SetField($"Col{j}", columnsList[j]);
                                pdfStamper.AcroFields.SetField($"dtCol{j}", column);
                                outputList.Add(column);
                            }
                        }

                        //ComboBox c = (ComboBox)tableLayoutPanel.Controls[i];

                    }

                    //foreach (ComboBox combo in tableLayoutPanel.Controls)
                    //{
                    //    if (combo.SelectedIndex > 0 || (combo.Text != "" && combo.Text != "<Select or Enter DT Field>"))// index 0 is the displayed value which should be ignored
                    //    {
                    //        j++;
                    //        string column = combo.SelectedItem == null ? combo.Text : combo.SelectedItem.ToString();
                    //        pdfStamper.AcroFields.SetField($"Col{j}", columnsList[j]);
                    //        pdfStamper.AcroFields.SetField($"dtCol{j}", column);
                    //        outputList.Add(column);
                    //    }
                    //}

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

            //foreach (string file in files)
            //{
            //    if (file.Substring(file.Length - 4, 4) == ".txt")
            //    {
            //        path = file;
            //    }
            //    columnsList = JobData.GetColumnList(path, delimiter);
            //}

            if (files.Length > 0 && (Path.GetExtension(files[0]) == ".txt" || Path.GetExtension(files[0]) == ".csv"))
            {
                path = files[0];
                columnsList = DirectoryHelper.getColumnList(path);
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
                btnFullRateTags.Enabled = true;
                btnINTZones.Enabled = true;
                listBoxTrayLabels.Items.Clear();
                listBoxLodements.Items.Clear();
                lodgements.Clear();
                sourceTable = new DataTable();

                jobName = txtJobName.Text = importedJob.JobName;
                customer = txtCustomer.Text = importedJob.Customer;
                jobNo = importedJob.JobNumber;
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
                Form sampleSheet = new frmSampleSheet(outputFileName, DirectoryHelper.getDelimiter(outputFileName));
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
            var res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
           
                richTextOutputFilePath.Text = outputFileName = openFileDialog.FileName;
                //  wizardPage1.AllowNext = true;
                btnSampleSheet.Enabled = true;
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
                    ElmsUser user = new ElmsUser() { eLMSUserName = txtElmsUN.Text.Trim(), Password = txtElmsPWD.Text.Trim(), WindowsUser = userName };
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
            if(importedJob == null || importedJob.JobNumber == null)
            {
                MessageBox.Show("You need to import job data before using this function.");
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = jobDirectoryData;
                openFileDialog.Filter = "Text | *.txt| CSV | *.csv";
                var res = openFileDialog.ShowDialog();

                if (res == DialogResult.OK && openFileDialog.FileName != null)
                {
                    outputFileName = txtManifestFileName.Text = openFileDialog.FileName;

                    //List<string> lines =  File.ReadLines(outputFileName).ToList();

                    // int tabCount = lines[0].Split('\t').Length;
                    // int commaCount = lines[0].Split(',').Length;

                    sourceTable = TextFileRW.readTextFileToTable(outputFileName, DirectoryHelper.getDelimiter(outputFileName));

                    if (sourceTable != null)
                    {
                        btnLodge.Enabled = true;
                        btnCreateAndPrintTags.Enabled = true;

                    }


                    if (sourceTable.Columns.Contains("Dt BP Sort Code") || sourceTable.Columns.Contains("Dt_BP_Sort_Order"))
                    {
                        cmbLodgementType.SelectedItem = "PreSort";

                        int dpidCount = 0;
                        string dpidCol = sourceTable.Columns.Contains("Dt DPID") ? "Dt DPID" : "Dt Barcode";
                        for (int i = 0; i < sourceTable.Rows.Count; i++)
                        {
                            if (sourceTable.Rows[i][dpidCol].ToString() != "")
                            {
                                dpidCount++;
                            }
                        }

                        if (dpidCount < 300)
                        {
                            cmbLodgementType.SelectedItem = "Full Rate";
                        }

                    }
                    else if (sourceTable.Columns.Contains("Dt PP Sort Code") || sourceTable.Columns.Contains("Dt LH Sort Code"))
                    {
                        cmbLodgementType.SelectedItem = "Print Post";
                    }
                    else
                    {
                        cmbLodgementType.SelectedItem = "Full Rate";
                    }

                    switch (cmbLodgementType.SelectedItem)
                    {
                        case "PreSort":
                            listBoxTrayLabels.Items.Add("Presort Tags");
                            cbIncSorted.Checked = true;
                            break;
                        case "Print Post":
                            listBoxTrayLabels.Items.Add("Print Post Tags");
                            cbIncSorted.Checked = true;
                            break;
                        case "Full Rate":
                            cbIncSorted.Checked = false;
                            break;
                        default:
                            break;
                    }

                    var lodgementLines = importedJob.LodgementLines.Where(x => x.Name == sortType).ToList();

                    if (lodgementLines.Count > 0)
                    {
                        if (sortType == "PreSort" && lodgementLines[0].Description.Contains("Charity Mail"))
                        {
                            cmbLodgementType.SelectedItem = "Charity Mail";
                        }

                        if (lodgementLines[0].Description.Contains("Priority"))
                        {
                            rbPriority.Checked = true;
                        }
                        else
                        {
                            rbRegular.Checked = true;
                        }

                        cmbWeight.SelectedItem = lodgementLines[0].Weight;

                        size = lodgementLines[0].Size;

                        switch (size)
                        {
                            case "Small":
                                rbSmall.Checked = true;
                                break;
                            case "Small Plus":
                                rbSmallPlus.Checked = true;
                                break;
                            case "Large":
                                rbLarge.Checked = true;
                                break;
                            default:
                                break;
                        }

                        addLodgementLine();
                    }
                    else
                    {
                        MessageBox.Show("No matching lodgement details found in Dolphin.");
                    }
                }
            }

        }

        private void addLodgementLine()
        {          
            Lodgement lodgement = new Lodgement();
            lodgement.AccNo = importedJob.PostAccts.Where(x => x.AccType == "Aust Post").ToList()[0].AccNo;
            lodgement.JobName = txtJobName.Text;
            lodgement.JobNo = jobNo;

            bool intContract = lodgement.AccNo == "6258639" || sortType == "INT Contract";
            sortSummary = Lodgement.GetSortCategories(sourceTable, sortType, intContract);

            sortSummary.WeightCat = cmbWeight?.Text ?? "0";

            Lodgement.CreateSortSummary(sortSummary, jobNo, Path.GetDirectoryName(outputFileName));

            if (importedJob.PostAccts.Exists(x => x.AccType != "Aust Post"))
            {
                sortSummary.RegName = importedJob.PostAccts?.Where(x => x.AccType != "Aust Post").ToList()?[0].AccName ?? "";
                sortSummary.RegNo = importedJob.PostAccts.Where(x => x.AccType != "Aust Post").ToList()[0].AccNo;
            }

            lodgement.ServiceType = serviceType;
            lodgement.Size = size;
            lodgement.SortType = sortType;
            lodgement.Weight = cmbWeight?.Text ?? "0";

            List<InputFiled> inputList = new List<InputFiled>();

            inputList = DataAccess.GetInputFileds(sortType, lodgement.Weight, lodgement.Size);

            Dictionary<string, string> sortList = new Dictionary<string, string>();

            PropertyInfo[] properties = typeof(SortSummary).GetProperties();



            foreach (InputFiled input in inputList)
            {
                foreach (var p in properties)
                {
                    if (p.Name == input.SortCategory)
                    {
                        var test = p.GetValue(sortSummary);

                        sortList.Add(input.InputName, test.ToString());
                    }
                }

            }

            lodgement.SortList = sortList;
            LodgementType lodgementType = DataAccess.GetLodgementType(lodgement);
            lodgement.ProductGroup = lodgementType.ProductGroup;
            lodgement.ArticleType = lodgementType.ArticleType;

            lodgements.Add(lodgement);


            if (sortSummary.IntTotal > 0)
            {
                Lodgement lodgementINT = new Lodgement();
                lodgementINT.ServiceType = serviceType;
                lodgementINT.Size = size;
                if(lodgement.AccNo== "6258639")
                {
                    lodgementINT.SortType = "INT Contract";
                }
                else
                {
                    lodgementINT.SortType = "INT Full Rate";
                }
              
                lodgementINT.Weight = cmbWeight?.Text ?? "0";

                inputList = DataAccess.GetInputFileds(lodgementINT.SortType, lodgementINT.Weight, lodgementINT.Size);

                sortList = new Dictionary<string, string>();

                foreach (InputFiled input in inputList)
                {
                    foreach (var p in properties)
                    {
                        if (p.Name == input.SortCategory)
                        {
                            var test = p.GetValue(sortSummary);

                            sortList.Add(input.InputName, test.ToString());
                        }
                    }

                }

                lodgementINT.SortList = sortList;
                lodgementType = DataAccess.GetLodgementType(lodgementINT);
                lodgementINT.ProductGroup = lodgementType.ProductGroup;
                lodgementINT.ArticleType = lodgementType.ArticleType;

                lodgements.Add(lodgementINT);
            }

            listBoxLodements.Items.Clear();

            foreach (Lodgement l in lodgements)
            {
                listBoxLodements.Items.Add($"{l.SortType} - {l.ServiceType} - {l.Size}");
            }

           
        }

        private void btnLodge_Click(object sender, EventArgs e)
        {
            if (lodgements.Count > 0)
            {
                lodgements[0].ProgressiveLodgement = cbProgressiveLodge.Checked;
                eLMS.Lodge(lodgements, elmsUser);

                btnLodge.Enabled = false;
                lodgements.Clear();
            }

        }

        private void rbPre_Sort_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void rbPrintPost_CheckedChanged(object sender, EventArgs e)
        {
                    
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            elmsUser = DataAccess.GetElmsUser(userName);

            LodgePanel.Visible = elmsUser != null;
            LoginPanel.Visible = elmsUser == null;
            LodgePanel.Dock = DockStyle.Fill;
            LoginPanel.Dock = DockStyle.Fill;

            txtManifestFileName.Text = manifestFile;            
            cmbLodgementType.SelectedItem = "PreSort";

            if(importedJob != null)
            {
               
            }

         

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            richTextOutputFilePath.Clear();
        }

        private void txtJobName_TextChanged(object sender, EventArgs e)
        {
            jobName = importedJob.JobName =  txtJobName.Text;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void rbRegular_CheckedChanged(object sender, EventArgs e)
        {
            serviceType = rbRegular.Checked ? "Regular" : "Priority";
            cmbWeight.DataSource = DataAccess.GetWeightCategories(sortType, serviceType, size);
        }

        private void cbmLodgementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            sortType= cmbLodgementType?.SelectedItem.ToString();
            cmbWeight.DataSource = DataAccess.GetWeightCategories(sortType, serviceType, size);

            switch (sortType)
            {
                case "PreSort":
                    rbSmallPlus.Enabled = true;
                    rbLarge.Enabled = true;
                    rbPriority.Enabled = true;
                    rbSmall.Checked = true;
                    break;
                case "Print Post":
                    rbSmallPlus.Enabled = false;
                    rbLarge.Enabled = true;
                    rbPriority.Enabled = true;
                    rbLarge.Checked = true;
                    break;
                case "Charity Mail":
                    rbSmallPlus.Enabled = false;
                    rbLarge.Enabled = true;
                    rbPriority.Enabled = false;
                    rbRegular.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void rbSmall_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSmall.Checked)
            {
                size = "Small";
            }
            cmbWeight.DataSource = DataAccess.GetWeightCategories(sortType, serviceType, size);
        }

        private void rbLarge_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLarge.Checked)
            {
                size = "Large";
            }
            cmbWeight.DataSource = DataAccess.GetWeightCategories(sortType, serviceType, size);
        }

        private void rbSmallPlus_CheckedChanged(object sender, EventArgs e)
        {if (rbSmallPlus.Checked)
            {
                size = "Small Plus";
            }
            cmbWeight.DataSource = DataAccess.GetWeightCategories(sortType, serviceType, size);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lodgements.Clear();
            txtManifestFileName.Clear();
            listBoxLodements.Items.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lodgements.Clear();
            addLodgementLine();
        }

        private void btnINTZones_Click(object sender, EventArgs e)
        {
            if (sourceTable != null && sourceTable.Columns.Contains("Country"))
            {
                SortSummary summary = new SortSummary();
                bool contract = false;
                if (importedJob != null && importedJob.PostAccts.Where(x => x.AccType == "Aust Post").ToList()[0].AccNo == "6258639")
                {
                    contract = true;
                }

                if(sortType == "INT Contract")
                {
                    contract = true;
                }

                int count = 0;
                int unmatchedCount = 0;

                foreach (DataRow row in sourceTable.Rows)
                {
                    string country = row["Country"].ToString();

                    if(country != "")
                    {
                        count++;
                        string zone = DataAccess.GetIntZones(country, contract);

                        if (string.IsNullOrEmpty(zone))
                        {
                            unmatchedCount++;
                            summary.UnmatchedCountries += $"{country},";
                        }

                        summary.Z1 += zone == "Z1" ? 1 : 0;
                        summary.Z2 += zone == "Z2" ? 1 : 0;
                        summary.Z3 += zone == "Z3" ? 1 : 0;
                        summary.Z4 += zone == "Z4" ? 1 : 0;
                        summary.Z5 += zone == "Z5" ? 1 : 0;
                        summary.Z6 += zone == "Z6" ? 1 : 0;
                        summary.Z7 += zone == "Z7" ? 1 : 0;
                        summary.Z8 += zone == "Z8" ? 1 : 0;
                        summary.Z9 += zone == "Z9" ? 1 : 0;
                    }
                    
                }

                string summaryStr = $"Zone 1:\t {summary.Z1}\n";
                summaryStr += $"Zone 2:\t {summary.Z2}\n";
                summaryStr += $"Zone 3:\t {summary.Z3}\n";
                summaryStr += $"Zone 4:\t {summary.Z4}\n";
                summaryStr += $"Zone 5:\t {summary.Z5}\n";
                summaryStr += $"Zone 6:\t {summary.Z6}\n";
                summaryStr += $"Zone 7:\t {summary.Z7}\n";
                summaryStr += $"Zone 8:\t {unmatchedCount}\n";
                summaryStr += $"Total :\t {count}\n";

                File.WriteAllText($@"{Path.GetDirectoryName(outputFileName)}\International Zones.txt", summaryStr);
            }
        }

        private void btnFullRateTags_Click(object sender, EventArgs e)
        {
            int lblCount = (int)numericUpDownTags.Value;
            string service = serviceType == "Regular" ? "R" : "1";
            string traySize = size == "Small" ? "S" : "L";
            string labelName = $"{jobNo} {jobName}";
            if(labelName.Length > 32)
            {
                labelName = labelName.Substring(0, 32);
            }
            string labelText = $@"#Australia Post Visa Tray Label System - Ver:
3v0-700
#Label Plan File
#Label Plan Header
{labelName},MAILING SOLUTIONS,{jobName},6,{jobNo},
#Label Details
#Service,Sort_Plan_Type,Sort_Plan,Destination_ID,Mail_Size,Label_Qty,Date
{service},6,6,{traySize},{lblCount},{DateTime.Now.ToString("dd MMM yyyy")}
#End Of File";


            string lableFileName = $@"{importedJob.DataFolder}\{jobNo} - Tray Labels.lpf";

            File.WriteAllText(lableFileName, labelText);          

            string cmdImport = $"/C VisaCommand /i {lableFileName}";
            string cmdPrint = $"/C VisaCommand /p {labelName}";



            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = cmdImport,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
               

                }
            };

            if (cbImportLabel.Checked)
            {
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }

            if (cbPrintLabel.Checked)
            {
                proc.StartInfo.Arguments = cmdPrint;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }                                            
        }

    private void cbPrintLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrintLabel.Checked)
            {
                cbImportLabel.Checked = true;
            }
        }

        private void cbImportLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbImportLabel.Checked)
            {
                cbPrintLabel.Checked = false;
            }
        }

      
        private void combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (Control c in flowLayoutPanelDataSummary.Controls)
            //{
            //    if( c is ComboBox combo )
            //    {

            //    }
            //}

            //ComboBox cmb = sender as ComboBox;
            //string test = cmb.SelectedItem.ToString();


            ComboBox cmb = (ComboBox)sender;
            remainingItems = new List<string>(dtColList);
            foreach (Control c in tableLayoutPanel.Controls)
            {
                if (c is ComboBox combo)
                {
                    remainingItems.Remove(combo.SelectedItem.ToString());
                }

            }



            foreach (Control c in tableLayoutPanel.Controls)
            {
                if (c is ComboBox combo)
                {
                    string item = combo.SelectedItem.ToString();
                    List<string> bindingList = new List<string>(remainingItems);
                    bindingList.Add(item);
                    combo.DataSource = bindingList;
                    combo.SelectedItem = item;
                }

            }
        }


        //private void flowLayoutPanelDataSummary_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        //}

        private void cmbStreamFN_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_DragDrop(object sender, DragEventArgs e)
        {
            tableLayoutPanel.Controls.Clear();

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0 && (Path.GetExtension(files[0]) == ".txt" || Path.GetExtension(files[0]) == ".csv"))
            {
                path = files[0];
                columnsList = DirectoryHelper.getColumnList(path);
            }
            else
            {
                MessageBox.Show("Invalid file!");
                return;
            }


            dtColList = Address.getDtFieldList();
           
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Parent = splitContainer2.Panel1;
            tableLayoutPanel.AutoScroll = true;
            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.Controls.Add(new Label() { Text = "Source File Fields", Font = new Font(Label.DefaultFont, FontStyle.Bold) }, 0, 1);
            tableLayoutPanel.Controls.Add(new Label() { Text = "Datatools Fields", Font = new Font(Label.DefaultFont, FontStyle.Bold) }, 1, 1);
            tableLayoutPanel.Visible = false;

   
            int rowIndex = 1;
            int columnIndex = 0;
            remainingItems = new List<string>(dtColList);
            foreach (string col in columnsList)
            {
                

                tableLayoutPanel.RowCount += 1;
                rowIndex++;
                //if (rowIndex == 18)
                //{
                //    columnIndex += 3;

                //    tableLayoutPanel.ColumnCount += 3;
                //    rowIndex = 2;
                //    tableLayoutPanel.Controls.Add(new Label() { Text = "Source File Fields", Font = new Font(Label.DefaultFont, FontStyle.Bold) }, columnIndex, 1);
                //    tableLayoutPanel.Controls.Add(new Label() { Text = "Database Fields", Font = new Font(Label.DefaultFont, FontStyle.Bold) }, columnIndex + 1, 1);
                //    tableLayoutPanel.Controls.Add(new Label() { Text = "         ", Font = new Font(Label.DefaultFont, FontStyle.Bold) }, columnIndex - 1, 1);

                //}



                Label label = new Label { Text = col.Replace("\"","") , AutoSize=true};
                ComboBox comboBox = new ComboBox();

                comboBox.Refresh();
                comboBox.SelectionChangeCommitted += new EventHandler(combobox_SelectedIndexChanged);



                tableLayoutPanel.Controls.Add(label, columnIndex, rowIndex);
                tableLayoutPanel.Controls.Add(comboBox, columnIndex + 1, rowIndex);

                comboBox.DataSource = new List<string>(remainingItems);
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                string selection = remainingItems.Where(x => x == col.Replace("\"", "")).FirstOrDefault();
                if (selection == null)
                {
                    selection = remainingItems.Where(x => StringProcessing.MatchStrings(x, col.Replace("\"", "")) < 3).FirstOrDefault();
                }
                if(selection == null)
                {
                    selection = remainingItems.Where(x => col.Contains(x)).FirstOrDefault();
                }

                if (selection != null)
                {
                    comboBox.SelectedItem = selection;
                }


            }

            tableLayoutPanel.Visible = true;
        }
    

        private void splitContainer2_Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void bntCreateDSSheet_Click(object sender, EventArgs e)
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
        }

        private void btnINTTags_Click(object sender, EventArgs e)
        {
            printLabels(int.Parse(numericUpDownTags.Value.ToString()));
        }


        private void printLabels( int count)
        {

            int total = count;

            PrintDocument printDoc = new PrintDocument();
            
                printDoc.DefaultPageSettings.Landscape = true;
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom Size", 200, 467);
                printDoc.DefaultPageSettings.PaperSize.RawKind = 120;
                printDoc.DocumentName = $"Tray Lables - {txtJobNo.Text}";
                printDoc.PrinterSettings.PrinterName = "Godex G530";
            


            printDoc.DefaultPageSettings.Margins.Left = 10;
            printDoc.DefaultPageSettings.Margins.Right = 10;//100 = 1 inch = 2.54 cm
            printDoc.DefaultPageSettings.Margins.Top = 10;
            printDoc.DefaultPageSettings.Margins.Bottom = 10;
            int i = 0;


            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc; //Document property must be set before ShowDialog()

            DialogResult dialogResult = printDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                printDoc.Print();
            }

            void printDoc_PrintPage(object senderp, PrintPageEventArgs ev)
            {
                Graphics g = ev.Graphics;
                string jobNo = txtJobNo.Text.ToUpper();
                //string customer = txtCustomer.Text.ToUpper();
                string customer = txtCustomer.Text;
                Font font1 = new Font("Calibri", 32, FontStyle.Bold);
                string jobName = txtJobName.Text.ToUpper();
                Font font2 = new Font("Calibri", 22);
                string tot = total.ToString();
                Font font3 = new Font("Calibri", 12);
                Font font4 = new Font("Calibri", 18, FontStyle.Bold);
                Font font7 = new Font("Calibri", 14);

                int x1 = ev.MarginBounds.Left;
                int y1 = ev.MarginBounds.Top;
                int w = ev.MarginBounds.Width;
                int h = ev.MarginBounds.Height;
                int y2 = 65;
                int y3 = 140;
                int y4 = 160;

                StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
                StringFormat formatCenter = new StringFormat(formatLeft);
                formatCenter.Alignment = StringAlignment.Center;

                
               RectangleF rectF1 = new RectangleF(x1, y2, 450, 70);

                    g.DrawString($"{jobNo} - {jobName}", font3, Brushes.Black, x1, y1);
                    g.DrawString("INTERNATIONAL", font1, Brushes.Black, rectF1, formatCenter);
                    g.DrawString("Mailing Solutions", font3, Brushes.Black, x1, y3);
                    g.DrawRectangle(Pens.Black, x1, y2, 450, 70);
                    // g.DrawString("- INT", font1, Brushes.Black, 160, y1);
                   // g.DrawString($"eLMS No:  {eLMS}", font4, Brushes.Black, x1, y4);
                    string trayNo = $"Tray {i + 1} of {tot}";
                    g.DrawString(trayNo, font4, Brushes.Black, x1 + 300, y4);

                    i++;
                    ev.HasMorePages = i >= total ? false : true;
                

            }
        }

        private void btnCreateAndPrintTags_Click(object sender, EventArgs e)
        {
            if(listBoxTrayLabels.Items.Count == 0)
            {
                return;
            }
            string lodgeDate = dateTimePickerLodgeDate.Value.ToString("dd MMM yyyy");

            string service = serviceType == "Regular" ? "R" : "1";
            string traySize = size == "Small" ? "S" : "L";
            string labelName = $"{jobNo} {jobName}";
            if (labelName.Length > 32)
            {
                labelName = labelName.Substring(0, 32);
            }
            StringBuilder labelText = new StringBuilder("#Australia Post Visa Tray Label System - Ver:\n3v0-700\n#Label Plan File\n\n#Label Plan Header\n");
            labelText.Append( $"{labelName},MAILING SOLUTIONS,{jobName},6,{jobNo}\n\n#Label Details\n\n");

            if (cbIncSorted.Checked)
            {
                decimal countPerTray = 0;
                using (var frmCount = new FormCount())
                {
                    var res = frmCount.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        countPerTray = frmCount.Count;
                    }
                    else
                    {
                        return;
                    }
                }

                if (countPerTray == 0)
                {
                    MessageBox.Show("Articles per Tray must be greater than zero!");
                    return;
                }


                if (sourceTable != null)
                {

                    List<TrayLabel> labels = new List<TrayLabel>();
                    string sortCodeColumn = "";

                    switch (sortType)
                    {

                        case "PreSort":
                            sortCodeColumn = sourceTable.Columns.Contains("Dt BP Sort Code") ? "Dt BP Sort Code" : "Dt_BP_Sort_Order"; // for Pioneer column names
                            break;
                        case "Charity Mail":
                            sortCodeColumn = sourceTable.Columns.Contains("Dt BP Sort Code") ? "Dt BP Sort Code" : "Dt_BP_Sort_Order"; // for Pioneer column names
                            break;
                        case "Print Post":
                            sortCodeColumn = sourceTable.Columns.Contains("Dt PP Sort Code") ? "Dt PP Sort Code" : "Dt LH Sort Code";
                            break;
                        default:
                            break;
                    }

                    //foreach (DataRow row in sourceTable.Rows)
                    //{
                    //    TrayLabel label = new TrayLabel();
                    //    label.ServiceType = serviceType;
                    //    label.Size = size;
                    //    label.SortCode = row[sortCodeColumn].ToString();
                    //    label.SortType = sortType;
                    //    if (label.SortCode != "")
                    //    {
                    //        TrayLabel updatedLabel = TrayLabel.CreateLabelLine(label);
                    //        if(updatedLabel != null)
                    //        {
                    //            labels.Add(updatedLabel);
                    //        }

                    //    }
                    //}

                    labelText.Append(TrayLabel.CreateLabelLines(sourceTable, serviceType, size, sortType, lodgeDate, countPerTray));

                    //int itemCount = 0;
                    //for (int i = 0; i < labels.Count; i++)
                    //{

                    //    itemCount++;
                    //    if (i == labels.Count - 1 || $"{labels[i].ServiceType}{labels[i].Sort_Plan_Type}{labels[i].Sort_Plan}" != $"{labels[i+1].ServiceType}{labels[i+1].Sort_Plan_Type}{labels[i+1].Sort_Plan}")
                    //    {
                    //        labels[i].LabelCount = (int)Math.Ceiling(itemCount / countPerTray);
                    //        labelText.Append($"{labels[i].ServiceType},{labels[i].Sort_Plan_Type},{labels[i].Sort_Plan},{labels[i].Size},{labels[i].LabelCount},{lodgeDate}\n");
                    //        itemCount = 0;
                    //    }

                    //}
                }
            }
          
            if(fullratetagCount > 0)
            {
                labelText.Append($"{ service},6,6,{ traySize},{ fullratetagCount},{lodgeDate}\n");
            }




            labelText.Append("#End Of File");


            string lableFileName = $@"{Path.GetDirectoryName(outputFileName)}\{jobNo} - Tray Labels.lpf";

            File.WriteAllText(lableFileName, labelText.ToString());

            string cmdDel = $"/C VisaCommand /d {labelName}";
            string cmdImport = $"/C VisaCommand /i {lableFileName}";
            string cmdPrint = $"/C VisaCommand /p {labelName}";



            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = cmdDel,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,


                }
            };

            if (cbImportLabel.Checked)
            {                
                proc.Start();
                proc.WaitForExit();
                proc.Close();
                proc.StartInfo.Arguments = cmdImport;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }

            if (cbPrintLabel.Checked)
            {
                proc.StartInfo.Arguments = cmdPrint;
                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }




        }

        private void btnAddFullRate_Click(object sender, EventArgs e)
        {
            listBoxTrayLabels.Items.Add($"{(int)numericUpDownFR.Value} Full Rate Tags");
            fullratetagCount += (int)numericUpDownFR.Value;
        }

        private void btnAddINTTags_Click(object sender, EventArgs e)
        {
            listBoxTrayLabels.Items.Add($"{(int)numericUpDownINT.Value} International Tags");
            intTagCount += (int)numericUpDownINT.Value;
        }

        private void btnClearLabels_Click(object sender, EventArgs e)
        {
            listBoxTrayLabels.Items.Clear();
            fullratetagCount = 0;
            intTagCount = 0;
        }
    }
} 
