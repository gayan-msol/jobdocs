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


namespace JobDocs
{
    public partial class Form1 : Form
    {

        string customer = "";
        public static string jobNo = "";
        public static string jobName = "";
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

            productionReport.createPdf(fileName, productionReport);
        }

        private void createPdf(string fileName)
        {
                           
            PDF dsPdf = new PDF(dataSummaryTemplate);
         

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
            txtCustomFinishedSize.Enabled = txtCustomPrintSize.Enabled = false;
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
                jobDirectory = richTextJobDirectory.Text = importedJob.DataFolder;

                cmbFileName.DataSource = DirectoryHelper.GetOutPutFiles(jobDirectory);

                if (importedJob.DocID != null)
                {
                    // temp
                    var test = Lodgement.GetLodgementDetails(importedJob.DocID);

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
            
            return new string[] {cmbStream?.Text, numericUpDownStreamQty.Value.ToString(),printQty.ToString()};
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
            try
            {/*
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = richTextJobDirectory.Text;
                saveFileDialog.FileName = $"{txtJobNo.Text} - Print Spec Sheet - {getPrintMachine()}";
                saveFileDialog.Filter = "PDF|*.pdf";
                saveFileDialog.ShowDialog();*/
                //if (saveFileDialog.FileName != null)
                {
                    PrintSpecSheet printSpecSheet = new PrintSpecSheet();

                    List<string> streamList = new List<string>();
                    foreach (DataGridViewRow row in dataGridViewStreams.Rows)
                    {
                        if (row.Cells[0].Value != null)
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
                    printSpecSheet.Guillotine = cmbGuillo?.Text;
                   // printSpecSheet.createPdf(saveFileDialog.FileName, printSpecSheet);

                    Print(printSpecSheet);
                }

            }
            catch (Exception ex)
            {
                ErrorHandling.ShowMessage(ex);
            }


        }

        private string getPrintMachine()
        {
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
            string printSize = cmbPrintSize?.Text;
            if (printSize == "Custom")
            {
                printSize = txtCustomPrintSize.Text;
            }

            return printSize;
        }

        private string getFinishedSize()
        {
            string finishedSize = cmbFinishedSize?.Text;
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
                return $"MSOL: {txtStockDescription.Text}";
            }
            else
            {
                return $"Customer: {txtStockDescription.Text}";
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
            txtCustomPrintSize.Enabled = (cmbPrintSize?.Text == "Custom");

            cmbFinishedSize.Text = cmbPrintSize.Text;

        }

        private void cmbFinishedSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomFinishedSize.Enabled = (cmbFinishedSize?.Text == "Custom");
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


        private void Print(PrintSpecSheet printSpecSheet)
        {



            PrintDocument printDoc = new PrintDocument();



            printDoc.DefaultPageSettings.Landscape = false;
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, 1170);
            printDoc.DocumentName = $"{jobDirectory}\\{printSpecSheet.JobNo} - Print Spec Sheet - {printSpecSheet.PrintMachine}";
            printDoc.PrinterSettings.PrinterName = "RICOH MP C5503 PCL 6";


            printDoc.DefaultPageSettings.Margins.Left = 10;
            printDoc.DefaultPageSettings.Margins.Right = 10;//100 = 1 inch = 2.54 cm
            printDoc.DefaultPageSettings.Margins.Top = 10;
            printDoc.DefaultPageSettings.Margins.Bottom = 10;



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

                Font fontHeader = new Font("Calibri", 32, FontStyle.Bold);
                Font fontFieldName = new Font("Calibri", 22);
                Font fontFieldNameSmall = new Font("Calibri", 14);
                Font fontValueLargeBold = new Font("Calibri", 22, FontStyle.Bold);
                Font fontValueLarge = new Font("Calibri", 22);
                Font fontValueSmall = new Font("Calibri", 14, FontStyle.Bold);

                Font font4 = new Font("Calibri", 18, FontStyle.Bold);

                int x1 = ev.MarginBounds.Left;
                int y1 = ev.MarginBounds.Top;
                int w = ev.MarginBounds.Width;
                int h = ev.MarginBounds.Height;
                int xLeft = 70;
                int xRight = w - xLeft;
                int xHeader = 148;
                int yHeader = 30;
                int yLine1 = 80;
                int yLineHeight = 22;
                int yLineGap = 55;
                int y2 = 65;
                int y3 = 140;
                int y4 = 160;

                float lineCount = 0;

                StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
                StringFormat formatCenter = new StringFormat(formatLeft);
                formatCenter.Alignment = StringAlignment.Center;

                g.DrawString("PRINT SPECIFICATION SHEET", fontHeader, Brushes.Black, xHeader, yHeader);

                g.DrawString("Job No:", fontFieldName, Brushes.Black, xLeft, yLine1+20);
                g.DrawString(printSpecSheet.JobNo, fontValueLargeBold, Brushes.Black, xLeft + 100, yLine1+20);

                g.DrawString("Print Machine:", fontFieldName, Brushes.Black, xLeft + 400, yLine1+20 );
                g.DrawString(printSpecSheet.PrintMachine, fontValueLargeBold, Brushes.Black, xLeft + 600, yLine1+20);

                lineCount = 1;
                g.DrawString("Job Directory:", fontFieldName, Brushes.Black, xLeft , yLine1 + yLineHeight + yLineGap);
                RectangleF rectJobDir = new RectangleF(xLeft + 200, yLine1 + yLineHeight + yLineGap +10, 550, 50);
                g.DrawString(printSpecSheet.JobDirectory, fontValueSmall, Brushes.Black, rectJobDir);

                lineCount = 2;
                g.DrawString("File Name:", fontFieldName, Brushes.Black, xLeft, yLine1 + yLineHeight*lineCount + yLineGap*lineCount);
                RectangleF rectFileName = new RectangleF(xLeft + 200, yLine1 + yLineHeight*lineCount + yLineGap*lineCount, 550, 75);
                g.DrawString(printSpecSheet.FileName, fontValueSmall, Brushes.Black, rectFileName);

                lineCount = 3;
                g.DrawString("Print Size:", fontFieldName, Brushes.Black, xLeft, yLine1 + yLineHeight * lineCount + yLineGap * lineCount);
                RectangleF rectPrintSize = new RectangleF(xLeft + 130, yLine1 + yLineHeight * lineCount + yLineGap * lineCount , 200, 44);
                g.DrawString(printSpecSheet.PrintSize, fontValueLarge, Brushes.Black, rectPrintSize);

                //g.DrawString("Guillo:", fontFieldName, Brushes.Black, xLeft +225, yLine1 + yLineHeight * lineCount + yLineGap * lineCount);
                //RectangleF rectGuillo = new RectangleF(xLeft + 305, yLine1 + yLineHeight * lineCount + yLineGap * lineCount , 100, 44);
                //g.DrawString(printSpecSheet.Guillotine, fontValueLarge, Brushes.Black, rectGuillo);
          
                g.DrawString("Finished Size:", fontFieldName, Brushes.Black, xLeft + 375, yLine1 + yLineHeight * lineCount + yLineGap * lineCount);
                RectangleF rectFinishSize = new RectangleF(xLeft + 550, yLine1 + yLineHeight * lineCount + yLineGap * lineCount , 200, 44);
                g.DrawString(printSpecSheet.FinishedSize, fontValueLarge, Brushes.Black, rectFinishSize);

                lineCount = 3.75F;
                g.DrawString("Guillotine:", fontFieldName, Brushes.Black, xLeft, yLine1 + yLineHeight * lineCount + yLineGap * lineCount);
                RectangleF rectGuillo = new RectangleF(xLeft + 135, yLine1 + yLineHeight * lineCount + yLineGap * lineCount, 100, 44);
                g.DrawString(printSpecSheet.Guillotine, fontValueLarge, Brushes.Black, rectGuillo);

                lineCount = 4.5F;
                g.DrawString("Stock:", fontFieldName, Brushes.Black, xLeft, yLine1 + yLineHeight * lineCount + yLineGap * lineCount);
                RectangleF rectStock = new RectangleF(xLeft + 125, yLine1 + yLineHeight * lineCount + yLineGap * lineCount + 10, 500, 44);
                g.DrawString(printSpecSheet.Stock, fontValueSmall, Brushes.Black, rectStock);

                lineCount = 5.25F;
                g.DrawString("Layout:", fontFieldName, Brushes.Black, xLeft, yLine1 + yLineHeight * lineCount + yLineGap * lineCount);
                RectangleF rectLayout = new RectangleF(xLeft + 125, yLine1 + yLineHeight * lineCount + yLineGap * lineCount, 500, 44);
                g.DrawString(printSpecSheet.Layout, fontValueLargeBold, Brushes.Black, rectLayout);

                lineCount = 6;
                Rectangle rectStreams = new Rectangle(xLeft, 530, xRight - xLeft, 300);
                g.DrawRectangle(Pens.Black, rectStreams);
                string streams="";
                foreach (string s in printSpecSheet.StreamList)
                { streams += $"{s}\n"; }
                g.DrawString(streams,fontValueSmall, Brushes.Black, rectStreams);

                lineCount = 7;
                Rectangle rectNotes = new Rectangle(xLeft, 835, xRight - xLeft, 150);
                g.DrawRectangle(Pens.Black, rectNotes);
                g.DrawString(printSpecSheet.Notes, fontValueSmall, Brushes.Black, rectNotes);

                lineCount = 8;
                Rectangle rectClientApproval = new Rectangle(xLeft, 990, xRight - xLeft, 60);
                g.DrawRectangle(Pens.Black, rectClientApproval);
                g.DrawLine(Pens.Black, xLeft + 223, 990, xLeft + 223, 1050);
                g.DrawString("Approved By:", fontValueSmall, Brushes.Black, xLeft,990);

                g.DrawLine(Pens.Black, xLeft + 446, 990, xLeft + 446, 1050);
                g.DrawString("From Company:", fontValueSmall, Brushes.Black, xLeft+223, 990);

                g.DrawString("Approved to at MSOL:", fontValueSmall, Brushes.Black, xLeft + 446, 990);




                lineCount = 9;
                Rectangle rectSignOff = new Rectangle(xLeft, 1055, xRight - xLeft, 60);
                g.DrawRectangle(Pens.Black, rectSignOff);
                g.DrawLine(Pens.Black, xLeft + 223, 1055, xLeft + 223, 1115);
                g.DrawString("Printed By:", fontValueSmall, Brushes.Black, xLeft, 1055);

                g.DrawLine(Pens.Black, xLeft + 446, 1055, xLeft + 446, 1115);
                g.DrawString("Date:\n              /       /", fontValueSmall, Brushes.Black, xLeft + 223, 1055);

                g.DrawString("Sign Off:", fontValueSmall, Brushes.Black, xLeft + 446, 1055);

                /*
                if (type == "Tray")
                {
                    string trayNo = $"Tray {i + 1} {tot}";
                    RectangleF rectF1 = new RectangleF(x1, y2, 450, 70);
                    Rectangle rect2 = new Rectangle(310, y2 + 80, 150, 50);

                    g.DrawString(jobNo, font1, Brushes.Black, x1, y1);
                    g.DrawString(jobName, font2, Brushes.Black, rectF1, formatCenter);
                    g.DrawString(customer, font3, Brushes.Black, x1, y3);
                    g.DrawRectangle(Pens.Black, x1, y2, 450, 70);

                    if (i == total + 1)
                    {
                        g.DrawString("Printed Date :                  Time:       ", font4, Brushes.Black, x1, y4);
                    }
                    else if (i == total)
                    {
                        g.DrawString("- Spoils", font1, Brushes.Black, 160, y1);
                        g.DrawString($"eLMS No:  {eLMS}", font4, Brushes.Black, x1, y4);
                    }
                    else
                    {
                        g.DrawString(trayNo, font4, Brushes.Black, x1, y4);
                        string streamLine = stream != "" ? $"Stream : {stream}" : stream;
                        g.DrawString($"{streamLine} \n{notes}", font3, Brushes.Black, rect2);
                    }

                    i++;
                    ev.HasMorePages = i >= total + 2 ? false : true;
                }
                else if (type == "Spoils")
                {
                    RectangleF rectF1 = new RectangleF(x1, y2, 450, 70);

                    g.DrawString(jobNo, font1, Brushes.Black, x1, y1);
                    g.DrawString(jobName, font2, Brushes.Black, rectF1, formatCenter);
                    g.DrawString(customer, font3, Brushes.Black, x1, y3);
                    g.DrawRectangle(Pens.Black, x1, y2, 450, 70);
                    g.DrawString("- Spoils", font1, Brushes.Black, 160, y1);
                    g.DrawString($"eLMS No:  {eLMS}", font4, Brushes.Black, x1, y4);
                }
                else if (type == "Box-A5")
                {
                    Font font5 = new Font("Calibri", 18);
                    Font font6 = new Font("Calibri", 18);
                    string boxNo1 = $"{i + 1} {tot}";
                    string boxNo2 = $"{i + 2} {tot}";
                    int xMiddle = 585;
                    int yMiddle = 415;
                    int yLine1 = 450;
                    int yLine2 = 525;
                    int yLine3 = 575;
                    int yLine4 = 625;
                    int yLine5 = 675;


                    Rectangle rect1 = new Rectangle(175, yLine1, 410, 75);
                    Rectangle rect2 = new Rectangle(175 + xMiddle, yLine1, 410, 75);
                    Rectangle rect3 = new Rectangle(x1, yLine5, 560, 120);
                    Rectangle rect4 = new Rectangle(x1 + xMiddle, yLine5, 560, 120);


                    g.DrawLine(Pens.Black, xMiddle, y1, xMiddle, y1 + 820);
                    g.DrawLine(Pens.Black, x1, yMiddle, x1 + 1150, yMiddle);
                    g.DrawString($"Job Name : ", font5, Brushes.Black, x1, yLine1);
                    g.DrawString(jobName, font6, Brushes.Black, rect1);
                    g.DrawString($"Job Number : {jobNo}", font5, Brushes.Black, x1, yLine2);
                    g.DrawString($"Stream : {stream}", font5, Brushes.Black, x1, yLine3);
                    g.DrawString($"Customer   : {customer}", font5, Brushes.Black, x1, yLine4);

                    g.DrawString($"Box : {boxNo1}", font5, Brushes.Black, 375, yLine3);
                    g.DrawRectangle(Pens.Black, rect3);
                    g.DrawString(notes, font5, Brushes.Black, rect3);
                    if (i + 1 == total)
                    {

                        jobName = "..........................................................................................";
                        jobNo = "....................";
                        stream = "......";
                        boxNo2 = "...... of ......";
                        notes = "";
                    }

                    g.DrawString($"Job Name : ", font5, Brushes.Black, x1 + xMiddle, yLine1);
                    g.DrawString(jobName, font5, Brushes.Black, rect2);
                    g.DrawString($"Job Number : {jobNo}", font5, Brushes.Black, x1 + xMiddle, yLine2);
                    g.DrawString($"Stream : {stream}", font5, Brushes.Black, x1 + xMiddle, yLine3);
                    g.DrawString($"Box : {boxNo2}", font5, Brushes.Black, 375 + xMiddle, yLine3);
                    g.DrawRectangle(Pens.Black, rect4);
                    g.DrawString(notes, font5, Brushes.Black, rect4);


                    i++;
                    i++;
                    ev.HasMorePages = i >= total ? false : true;

                }
                else if (type == "Box-A4")
                {
                    Font font5 = new Font("Calibri", 18);
                    Font font6 = new Font("Calibri", 24);
                    string boxNo1 = $"{i + 1} {tot}";
                    string boxNo2 = $"{i + 2} {tot}";
                    int yMiddle = 585;
                    int xMiddle = 415;
                    int yLine1 = 625;
                    int yLine2 = 725;
                    int yLine3 = 800;
                    int yLine4 = 875;
                    int yLine5 = 950;


                    Rectangle rect1 = new Rectangle(175, yLine1, 650, 100);
                    Rectangle rect2 = new Rectangle(175, yLine2, 410, 50);
                    Rectangle rect3 = new Rectangle(x1, yLine5, 810, 210);
                    Rectangle rect4 = new Rectangle(x1 + xMiddle, yLine5, 560, 300);


                    g.DrawLine(Pens.Black, 0, yMiddle, 830, yMiddle);
                    g.DrawString($"Job Name : ", font5, Brushes.Black, x1, yLine1);
                    g.DrawString(jobName, font6, Brushes.Black, rect1);
                    g.DrawString($"Job Number :", font5, Brushes.Black, x1, yLine2);
                    g.DrawString(jobNo, font6, Brushes.Black, rect2);
                    g.DrawString($"Stream : {stream}", font5, Brushes.Black, x1, yLine3);
                    g.DrawString($"Customer   : {customer}", font5, Brushes.Black, x1, yLine4);

                    g.DrawString($"Box : {boxNo1}", font5, Brushes.Black, 620, yLine3);
                    g.DrawRectangle(Pens.Black, rect3);
                    g.DrawString(notes, font5, Brushes.Black, rect3);

                    i++;

                    ev.HasMorePages = i >= total ? false : true;

                }*/
            }
        }

    }
} 
