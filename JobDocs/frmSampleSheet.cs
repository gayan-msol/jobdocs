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
        List<string> columnList = new List<string>();
     //   string delimiter = "\t";
        DataTable sampleTable = new DataTable();
        DataTable sourceTable = new DataTable();

        private DataGridPrinter dataGridPrinter1 = null;

        public frmSampleSheet()
        {
            InitializeComponent();
            wizardPage1.AllowNext = false;
            rbTab.Checked = true;
           
        }

        private string getDelimeter()
        {
            string delimeter = "";
            if(rbComma.Checked)
            {
                return ",";
            }
            else
            {
                return "\t";
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text | *.txt| CSV | *.csv";
            openFileDialog.InitialDirectory = Form1.jobDirectory;
            openFileDialog.ShowDialog();
            if(!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                outputFileName = openFileDialog.FileName;
                richTextOutputFilePath.Text = outputFileName;
                wizardPage1.AllowNext = true ;

            }
        }

        private void wizardPage2_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            List<string> selectedColumnList = new List<string>();
            foreach (CheckBox c in flowLayoutPanelColumns.Controls)
            {
                if(c.Checked)
                {
                    selectedColumnList.Add(c.Name.Substring(2).Replace("%"," ")); 
                }
            }


            dataGridViewSample.DataSource =sampleTable= SampleSheet.GetSampleTable(sourceTable, selectedColumnList);

         //   SetupGridPrinter();
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            try
            {
                sourceTable = JobData.GetSourceTable(outputFileName, getDelimeter());
                columnList.Clear();
                columnList = JobData.GetColumnList(outputFileName, getDelimeter());
                foreach (var item in columnList)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = $"cb{item.Replace(" ", "%")}";
                    checkBox.Text = item;
                    checkBox.AutoSize = true;
                    checkBox.Checked = true;
                    flowLayoutPanelColumns.Controls.Add(checkBox);
                }
                uncheckDTFileds();
            }
            catch(Exception ex)
            {
                
                ErrorHandling.ShowMessage(ex);
                this.Close();
            }
       

           

     
        }

        private void checkBoxExcludeDTFields_CheckedChanged(object sender, EventArgs e)
        {
            uncheckDTFileds();
        }

        private void uncheckDTFileds()
        {
            List<string> dtFieldList = columnList.Where(x => !string.IsNullOrWhiteSpace(x) && x.Substring(0,2)=="Dt").ToList();
          //  dtFieldList.Add("Source");
            dtFieldList.Add("MediaSelect");

            foreach (var item in dtFieldList)
            {
                Control[] cArr =flowLayoutPanelColumns.Controls.Find($"cb{item.Replace(" ", "%")}", true);
                if(cArr.Length >0 && cArr[0] is CheckBox checkBox)
                    checkBox.Checked = !checkBoxExcludeDTFields.Checked;
            }
        }

        private void uncheckAllFileds()
        {
            //List<string> dtFieldList = columnList.Where(x => !string.IsNullOrWhiteSpace(x) && x.Substring(0, 2) == "Dt").ToList();
            //dtFieldList.Add("Source");
            //dtFieldList.Add("MediaSelect");

            //foreach (var item in dtFieldList)
            //{
            //    Control[] cArr = flowLayoutPanelColumns.Controls.Find($"cb{item.Replace(" ", "_")}", true);
            //    if (cArr.Length > 0 && cArr[0] is CheckBox checkBox)
            //        checkBox.Checked = !checkBoxExcludeDTFields.Checked;
            //}

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

            printSampleSheet(sampleTable, "test", 1, false, "a");

        }

        private void printSampleSheet(DataTable dataTable, string page, int count, bool unknownTotal, string type, int startPage = 1)
        {

            int total = count + startPage - 1;

            PrintDocument printDoc = new PrintDocument();


            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, 1170);
            printDoc.DocumentName = $@"{Form1.jobDirectory}\Job {Form1.jobNo} - Sample Records";
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

                Font font2 = new Font("Calibri", 22);
                string tot = unknownTotal ? "" : $"of {total}";
                Font font3 = new Font("Calibri", 12);
                Font font4 = new Font("Calibri", 18, FontStyle.Bold);

                int x1 = ev.MarginBounds.Left;
                int y1 = ev.MarginBounds.Top;
                int w = ev.MarginBounds.Width;
                int h = ev.MarginBounds.Height;
                int yTable = y1 + 100;
                int yLine = yTable;
                int lineHeight = 35;

                StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
                StringFormat formatCenter = new StringFormat(formatLeft);
                formatCenter.Alignment = StringAlignment.Center;

            
                    Font headerFont = new Font("Calibri", 10,FontStyle.Bold);
                    Font bodyFont = new Font("Calibri", 10);
                    string boxNo1 = $"{i + 1} {tot}";
                    string boxNo2 = $"{i + 2} {tot}";

                int tableHeight = sampleTable.Columns.Contains("Source") ? ((sampleTable.Columns.Count -1) * 35) : (sampleTable.Columns.Count * 35);

                Rectangle borderRect = new Rectangle(x1, yTable, 1130, tableHeight);
                Rectangle headerCol = new Rectangle(x1, yTable, 225, tableHeight);
                Rectangle col1 = new Rectangle(x1 + 225, yTable, 300, tableHeight);
                Rectangle col2 = new Rectangle(x1 + 525, yTable, 300, tableHeight);
                Rectangle col3 = new Rectangle(x1 + 825, yTable, 300, tableHeight);

                g.DrawRectangle(Pens.Black, headerCol);
                g.DrawRectangle(Pens.Black, col1);
                g.DrawRectangle(Pens.Black, col2);
                g.DrawRectangle(Pens.Black, col3);

                g.DrawString("Job No  :", headerFont, Brushes.Black, x1 + 5, y1 + 5);
                g.DrawString("Job Name:", headerFont, Brushes.Black, x1 + 5 , y1 + 5 + 30);
                g.DrawString("Source  :", headerFont, Brushes.Black, x1 + 5, y1 + 5 + 60);
                g.DrawString(Form1.jobNo, bodyFont, Brushes.Black, x1 + 5 + 100, y1 + 5);
                g.DrawString(Form1.jobName, bodyFont, Brushes.Black, x1 + 5 + 100, y1 + 5 + 30);
                if (sampleTable.Columns.Contains("Source"))
                {
                    g.DrawString(sampleTable.Rows[0]["Source"].ToString(), bodyFont, Brushes.Black, x1 + 5 + 100, y1 + 5 + 60);
                }



                for (int c= 0; c< sampleTable.Columns.Count; c++)
                {
                    if(c > 0)
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
                        RectangleF rectangleF = new RectangleF(x1 + 5 + 225 + r * 300, yLine, 300, 30);

                        g.DrawString(sampleTable.Rows[r][c].ToString(), bodyFont, Brushes.Black,rectangleF);

                        g.DrawLine(Pens.Black, x1, yLine + lineHeight, x1 + 1125, yLine + lineHeight);
                    }

                   
                }

                i++;
                    i++;
                    ev.HasMorePages = i >= total ? false : true;

    
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

        void SetupGridPrinter()
        {
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, 1170);
            printDocument1.DocumentName = $"{Form1.jobNo} - Sample Records";


            dataGridPrinter1 = new DataGridPrinter(dataGridViewSample, printDocument1,sampleTable);
        }

        private void checkBoxUncheckAll_CheckedChanged(object sender, EventArgs e)
        {
            uncheckAllFileds();
        }

        private void rbComma_CheckedChanged(object sender, EventArgs e)
        {
           // delimiter = rbTab.Checked ? "," : "\t";       
        }
    }
}
