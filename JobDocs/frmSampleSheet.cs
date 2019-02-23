using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            dataGridViewSample.DataSource = SampleSheet.GetSampleRecords(outputFileName, delimiter);
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            columnList = SampleSheet.GetColumnList(outputFileName, delimiter);
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

            foreach (var item in dtFieldList)
            {
                CheckBox c = (CheckBox)flowLayoutPanelColumns.Controls.Find($"cb{item.Replace(" ", "_")}", true)[0];
                c.Checked = !checkBoxExcludeDTFields.Checked;
            }
        }

        private void rbTab_CheckedChanged(object sender, EventArgs e)
        {
            delimiter = rbTab.Checked ? "\t" : ",";
        }
    }
}
