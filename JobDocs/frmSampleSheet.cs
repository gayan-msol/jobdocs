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
        string delimiter = "\t";
        DataTable sampleTable = new DataTable();

        private DataGridPrinter dataGridPrinter1 = null;

        public frmSampleSheet()
        {
            InitializeComponent();
            wizardPage1.AllowNext = false;
            rbTab.Checked = true;
           
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text | *.txt";
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
                    selectedColumnList.Add(c.Name.Substring(2).Replace("_"," ")); 
                }
            }


            dataGridViewSample.DataSource =sampleTable= SampleSheet.GetSampleRecords(outputFileName, delimiter, selectedColumnList);

            SetupGridPrinter();
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            columnList = JobData.GetColumnList(outputFileName, delimiter);
            foreach (var item in columnList)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = $"cb{item.Replace(" ","_")}";
                checkBox.Text = item;
                checkBox.AutoSize = true;
                checkBox.Checked = true;
                flowLayoutPanelColumns.Controls.Add(checkBox);
            }
            uncheckDTFileds();
        }

        private void checkBoxExcludeDTFields_CheckedChanged(object sender, EventArgs e)
        {
            uncheckDTFileds();
        }

        private void uncheckDTFileds()
        {
            List<string> dtFieldList = columnList.Where(x => !string.IsNullOrWhiteSpace(x) && x.Substring(0,2)=="Dt").ToList();
            dtFieldList.Add("Source");
            dtFieldList.Add("MediaSelect");

            foreach (var item in dtFieldList)
            {
                Control[] cArr =flowLayoutPanelColumns.Controls.Find($"cb{item.Replace(" ", "_")}", true);
                if(cArr.Length >0 && cArr[0] is CheckBox checkBox)
                    checkBox.Checked = !checkBoxExcludeDTFields.Checked;
            }
        }

        private void rbTab_CheckedChanged(object sender, EventArgs e)
        {
            delimiter = rbTab.Checked ? "\t" : ",";
        }

        private void wizardPage3_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
          
        }
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            dataGridPrinter1.PageNumber = 1;
            dataGridPrinter1.RowCount = 0;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }

            ////Open the print dialog
            //PrintDialog printDialog = new PrintDialog();
            //printDialog.Document = printDocument1;
            //printDialog.UseEXDialog = true;
            ////Get the document
            //if (DialogResult.OK == printDialog.ShowDialog())
            //{
            //    printDocument1.DocumentName = "Test Page Print";
            //    printDocument1.Print();
            //}

            // Note: In case you want to show the Print Preview Dialog instead of 
            //  Print Dialog then comment the above code and uncomment the following code


            //Open the print preview dialog
            //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            //objPPdialog.Document = printDocument1;
            //objPPdialog.ShowDialog();

        }
        //void DrawTopLabel(Graphics g)
        //{
        //    int TopMargin = printDocument1.DefaultPageSettings.Margins.Top;

        //    g.FillRectangle(new SolidBrush(label1.BackColor), label1.Location.X, label1.Location.Y + TopMargin, label1.Size.Width, label1.Size.Height);
        //    g.DrawString(label1.Text, label1.Font, new SolidBrush(label1.ForeColor), label1.Location.X + 50, label1.Location.Y + TopMargin, new StringFormat());
        //}

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
            dataGridPrinter1 = new DataGridPrinter(dataGridViewSample, printDocument1,sampleTable);
        }


    }
}
