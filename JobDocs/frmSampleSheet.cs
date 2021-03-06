using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobDocsLibrary;


namespace JobDocs
{
    public partial class frmSampleSheet : Form
    {
        string outputFileName = "";
        string delimiter = "";
        List<string> columnList = new List<string>();
     //   string delimiter = "\t";
        DataTable sampleTable = new DataTable();
        DataTable sourceTable = new DataTable();
        List<int> selectedRecordList = new List<int>();

        private DataGridPrinter dataGridPrinter1 = null;

        public frmSampleSheet(string outputFile, string delim)
        {
            InitializeComponent();
            wizardPage1.AllowNext = true;
           // rbTab.Checked = true;
            outputFileName = outputFile;
            delimiter = delim;
           
        }




        private void wizardPage2_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {


         //   SetupGridPrinter();
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {


            List<string> selectedColumnList = new List<string>();
            foreach (CheckBox c in flowLayoutPanelColumns.Controls)
            {
                if (c.Checked || c.Name =="cbSource" || c.Name == "cbSort%Order")
                {
                    selectedColumnList.Add(c.Name.Substring(2).Replace("%", " "));
                }
            }

            string varCol = cmbColumn.SelectedItem?.ToString() ??  null;

            if(selectedColumnList.Count > 0)
            {
                dataGridViewSample.DataSource = sampleTable = SampleSheet.GetSampleTable(sourceTable, selectedColumnList, selectedRecordList, varCol);
            }
            else
            {
                e.Cancel = true;
            }

        }

        //private void checkBoxExcludeDTFields_CheckedChanged(object sender, EventArgs e)
        //{
        //    uncheckDTFileds();
        //}

        private void uncheckDTFileds()
        {
            List<string> dtFieldList = columnList.Where(x => !string.IsNullOrWhiteSpace(x) && x.Substring(0, 2) == "Dt").ToList();
            //  dtFieldList.Add("Source");
            dtFieldList.Add("MediaSelect");

            foreach (var item in dtFieldList)
            {

                Control[] cArr = flowLayoutPanelColumns.Controls.Find($"cb{item.Replace(" ", "%")}", true);
                if (cArr.Length > 0 && cArr[0] is CheckBox checkBox && checkBox.Name != "cbDt%BSP" && checkBox.Name != "cbDt%PP%Sort%Code")
                {
                    checkBox.Checked = false;

                    if (checkBox.Name == "cbDt%Barcode")
                    {
                        checkBox.Checked = columnList.Contains("Dt PP Sort Code");
                    }
                }

            }


        }

        private void uncheckAllFileds()
        {
             foreach (Control c in flowLayoutPanelColumns.Controls)
            {
                if(c is CheckBox cb)
                {
                    cb.Checked = !checkBoxUncheckAll.Checked;
                }
            }
        }



        private void rbTab_CheckedChanged(object sender, EventArgs e)
        {
           // delimiter = rbTab.Checked ? "\t" : ",";
        }

        private void wizardPage3_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
          
        }
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //dataGridPrinter1.PageNumber = 1;
            //dataGridPrinter1.RowCount = 0;
            //if (printDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    // printDocument1.Print();

            //}

            printSampleSheet( sampleTable, "test", 1, false, "a");

        }

        private void printSampleSheet(DataTable dataTable, string page, int count, bool unknownTotal, string type, int startPage = 1)
        {
            Font headerFont = new Font("Calibri", 10, FontStyle.Bold);
            Font bodyFont = new Font("Calibri", 10);

            
            int total = count + startPage - 1;
            int width = 1170;
            int colCount = dataTable.Rows.Count;
            float headerColWidth = 225;
            float dataColWidth = 300;
            List<int> colWidthList = new List<int>();

            int headerMaxLength = 225;
            int dataMaxLength = 300;

            List<float> lengths = new List<float>();
            foreach (DataColumn c in dataTable.Columns)
            {                
                lengths.Add(TextRenderer.MeasureText(c.ColumnName, bodyFont).Width);
            }

            headerColWidth = lengths.Max() +10;
            if(headerColWidth > headerMaxLength)
            {
                headerColWidth = headerMaxLength;
            }

            lengths = new List<float>();
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn c in dataTable.Columns)
                {
                    if(c.ColumnName != "Source")
                    {
                        lengths.Add(TextRenderer.MeasureText(row[c].ToString(), bodyFont).Width);
                    }

                }
            }

            dataColWidth =  lengths.Max() + 10;
            if(dataColWidth > dataMaxLength)
            {
                dataColWidth = dataMaxLength;
            }

        

            if(dataColWidth * colCount < 900)
            {
                dataColWidth = 300;
            }



            if ( colCount > 3)
            {
                width = (int)headerColWidth + (int)dataColWidth * colCount + 45;
            }
            PrintDocument printDoc = new PrintDocument();


            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, width);
            printDoc.DocumentName = $@"{Form1.jobDirectoryData}\JOB {Form1.jobNo} - Sample Records";
            printDoc.PrinterSettings.PrinterName = "Adobe PDF";
            
       //     printDoc.PrinterSettings.PrintFileName = $@"{Form1.jobDirectory}\Job {Form1.jobNo} - Sample Records";



            printDoc.DefaultPageSettings.Margins.Left = 20;
            printDoc.DefaultPageSettings.Margins.Right = 20;//100 = 1 inch = 2.54 cm
            printDoc.DefaultPageSettings.Margins.Top = 20;
            printDoc.DefaultPageSettings.Margins.Bottom = 20;
            int i = startPage - 1;


            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            DialogResult dialogResult = printDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                printDoc.Print();
            }

            void printDoc_PrintPage(object senderp, PrintPageEventArgs ev)
            {
                Graphics g = ev.Graphics;

             

                int x1 = ev.MarginBounds.Left;
                int y1 = ev.MarginBounds.Top;
                int w = ev.MarginBounds.Width;
                int h = ev.MarginBounds.Height;
                int yTable = y1 + 50;
                int yLine = yTable;
                int lineHeight = 20;


                StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
                StringFormat formatCenter = new StringFormat(formatLeft);
                formatCenter.Alignment = StringAlignment.Center;

            
                    
                    

                int tableHeight = sampleTable.Columns.Contains("Source") ? ((sampleTable.Columns.Count -1) * lineHeight) : (sampleTable.Columns.Count * lineHeight);

             

                g.DrawString("Job No  :", headerFont, Brushes.Black, x1 + 5, y1 + 5);
                g.DrawString(Form1.jobNo, bodyFont, Brushes.Black, x1 + 5 + 60, y1 + 5);
                g.DrawString("Job Name:", headerFont, Brushes.Black, x1 + 5 +160, y1 + 5);
                g.DrawString(Form1.jobName, bodyFont, Brushes.Black, x1 + 5 + 240, y1 + 5 );
                g.DrawString("Source  :", headerFont, Brushes.Black, x1 + 5, y1 + 5 + 20);
              
               
                if (sampleTable.Columns.Contains("Source"))
                {
                    g.DrawString(sampleTable.Rows[0]["Source"].ToString(), bodyFont, Brushes.Black, x1 + 5 + 60, y1 + 5 + 20);
                }


                 for (int c = 0; c < sampleTable.Columns.Count; c++)
                    {
                        if (c > 0)
                        {
                            yLine += lineHeight;

                        }


                        if (sampleTable.Columns[c].ColumnName == "Source")
                        {
                            yLine -= lineHeight;
                            continue;
                        }
                        g.DrawString(sampleTable.Columns[c].ColumnName, headerFont, Brushes.Black, x1 + 5, yLine);

                        for (int r = 0; r < sampleTable.Rows.Count; r++)
                        {
                            RectangleF rectangleF = new RectangleF(x1 + 5 + headerColWidth + r * dataColWidth, yLine, dataColWidth, 30);

                            g.DrawString(sampleTable.Rows[r][c].ToString(), bodyFont, Brushes.Black, rectangleF);
                            if(g.MeasureString(sampleTable.Rows[r][c].ToString(),bodyFont).Width > dataColWidth)
                            {
                                yLine += 15;
                            }
                           
                        }

                        g.DrawLine(Pens.Black, x1, yLine + lineHeight, x1 + headerColWidth + colCount* dataColWidth, yLine + lineHeight);

                        if (c == sampleTable.Columns.Count - 1)
                        {
                            tableHeight = yLine + lineHeight - yTable;
                        }

                        //if(c== sampleTable.Columns.Count -1)
                        //{
                        //    ev.HasMorePages = false;
                        //}
                        //else
                        //{
                        //    ev.HasMorePages = true;
                        //}

                }

                Rectangle borderRect = new Rectangle(x1, yTable, (int)headerColWidth + colCount * (int)dataColWidth, tableHeight);
                Rectangle headerCol = new Rectangle(x1, yTable, (int)headerColWidth, tableHeight);
                g.DrawRectangle(Pens.Black, headerCol);

                for (int c = 0; c < colCount; c++)
                {
                    Rectangle col = new Rectangle(x1 + (int)headerColWidth + c * (int)dataColWidth, yTable, (int)dataColWidth, tableHeight);
                    g.DrawRectangle(Pens.Black, col);
                }

                //i++;
                //    i++;
                //    ev.HasMorePages = i >= total ? false : true;


            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
           // DrawTopLabel(g);
            bool more = dataGridPrinter1.DrawDataGrid(g);
            if (more == true)
            {
                e.HasMorePages = true;
                dataGridPrinter1.PageNumber++;
            }
        }


        private void checkBoxUncheckAll_CheckedChanged(object sender, EventArgs e)
        {
            uncheckAllFileds();
        }

        private void rbComma_CheckedChanged(object sender, EventArgs e)
        {
           // delimiter = rbTab.Checked ? "," : "\t";       
        }

        private void frmSampleSheet_Load(object sender, EventArgs e)
        {

        }

        private void wizardPage1_Enter(object sender, EventArgs e)
        {
            try
            {
                sourceTable.Clear();
                sourceTable = JobData.GetSourceTable(outputFileName, delimiter);
                numUpDownPageNumbers.Maximum = sourceTable.Rows.Count;
                columnList.Clear();
                flowLayoutPanelColumns.Controls.Clear();
                columnList = JobData.GetColumnList(outputFileName, delimiter);
                cmbColumn.Items.Clear();
               

                foreach (var item in columnList)
                {
                    cmbColumn.Items.Add(item.ToString());

                   CheckBox checkBox = new CheckBox();
                    checkBox.Name = $"cb{item.Replace(" ", "%")}";
                    checkBox.Text = item;
                    checkBox.AutoSize = true;
                    checkBox.Checked = true;
                    flowLayoutPanelColumns.Controls.Add(checkBox);
                }
                uncheckDTFileds();
            }
            catch (Exception ex)
            {

                ErrorHandling.ShowMessage(ex);
                this.Close();
            }
        }

        private void checkBoxExcludeDTFields_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBoxUncheckAll_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void wizardPage3_Leave(object sender, EventArgs e)
        {
            columnList.Clear();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable excelTable = new DataTable();
            excelTable.Columns.Add("Field Name");
            for (int k = 0; k < sampleTable.Rows.Count; k++)
            {
                excelTable.Columns.Add($"Sample Record {k+1}");
            }


           
                for (int j = 0; j < sampleTable.Columns.Count; j++)
                {

                    DataRow row = excelTable.NewRow();
                    for (int i = 0; i < sampleTable.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            row[0] = sampleTable.Columns[j].ColumnName;
                        }
                       
                        {
                            row[i + 1] = sampleTable.Rows[i][j];
                        }
                    }
               
                    
                    excelTable.Rows.Add(row);
                }
            

            HelperLibrary.ExcelRW.CreatExcelFile($@"{Form1.jobDirectoryData}\Job {Form1.jobNo} - SAMPLE RECORDS.xlsx", excelTable, "Sample Records");
        }

        private void btnAddPageNumbers_Click(object sender, EventArgs e)
        {
            listBoxPageNumbers.Items.Add(numUpDownPageNumbers.Value);
            selectedRecordList.Add((int)numUpDownPageNumbers.Value-1);
        }

        private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
        {

        }
    }
}
