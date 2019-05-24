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
                    selectedColumnList.Add(c.Name.Substring(2).Replace("%"," ")); 
                }
            }


            dataGridViewSample.DataSource =sampleTable= SampleSheet.GetSampleTable(sourceTable, selectedColumnList);

            SetupGridPrinter();
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            sourceTable = JobData.GetSourceTable(outputFileName, getDelimeter());

            columnList.Clear();
            columnList = JobData.GetColumnList(outputFileName, getDelimeter());
            foreach (var item in columnList)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = $"cb{item.Replace(" ","%")}";
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

        private void uncheckAllFileds()
        {
            List<string> dtFieldList = columnList.Where(x => !string.IsNullOrWhiteSpace(x) && x.Substring(0, 2) == "Dt").ToList();
            dtFieldList.Add("Source");
            dtFieldList.Add("MediaSelect");

            foreach (var item in dtFieldList)
            {
                Control[] cArr = flowLayoutPanelColumns.Controls.Find($"cb{item.Replace(" ", "_")}", true);
                if (cArr.Length > 0 && cArr[0] is CheckBox checkBox)
                    checkBox.Checked = !checkBoxExcludeDTFields.Checked;
            }

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
            dataGridPrinter1.PageNumber = 1;
            dataGridPrinter1.RowCount = 0;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
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
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 1170, 830);
            printDocument1.DefaultPageSettings.Landscape = true;
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
