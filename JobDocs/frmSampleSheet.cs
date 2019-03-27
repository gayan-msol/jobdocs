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


            dataGridViewSample.DataSource = SampleSheet.GetSampleRecords(outputFileName, delimiter, selectedColumnList);
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
           
                //Open the print dialog
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDocument1;
                printDialog.UseEXDialog = true;
                //Get the document
                if (DialogResult.OK == printDialog.ShowDialog())
                {
                    printDocument1.DocumentName = "Test Page Print";
                    printDocument1.Print();
                }
                /*
                Note: In case you want to show the Print Preview Dialog instead of 
                Print Dialog then comment the above code and uncomment the following code
                */

                //Open the print preview dialog
                //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
                //objPPdialog.Document = printDocument1;
                //objPPdialog.ShowDialog();
            
        }

        private void printDocument1_BeginPrint(object sender,
    System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iCount = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
