using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dolphin;

namespace JobSetup
{
    public partial class Form1 : Form
    {
        Job importedJob = new Job();
        string jobName = "";
        string customer = "";
        string jobNo = "";
        string jobDirectoryData = "";
        string jobDirectoryArt = "";
        string oldJobNo = "";
        string newJobDirectoryData = "";
        string newJobDirectoryArt = "";



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            importedJob = Job.GetJob(textBoxJobNo.Text);
            if (importedJob != null)
            {


                jobName= importedJob.JobName;
                customer = importedJob.Customer;
                jobNo = importedJob.JobNumber;
                txtData.Text  = importedJob.DataFolder;
                txtArt.Text = importedJob.ArtworkFolder;
                oldJobNo = "";
               if(!string.IsNullOrWhiteSpace(txtData.Text))
                {
                    newJobDirectoryData = $@"{Directory.GetParent(txtData.Text).FullName}\JOB {jobNo} {jobName.ToUpper()}";
                    txtOldJobNo.Text = Path.GetFileName(txtData.Text).Split(' ')[1];
                    
                }

                if (!string.IsNullOrWhiteSpace(txtArt.Text))
                {
                    txtOldJobNo.Text = Path.GetFileName(txtArt.Text).Split(' ')[1];
                     newJobDirectoryArt = $@"{Directory.GetParent(txtArt.Text).FullName}\JOB {jobNo} {jobName.ToUpper()}";
                 
                }






            }

        }

        private  void CopyDirectory(string sourceDir, string destinationDir)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
           
            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            List<string> extList = new List<string>() { ".xlsx", ".xls", ".txt", ".csv", ".pdf", ".lpf", ".ps" };

            foreach (FileInfo file in dir.GetFiles())
            {

                if (!extList.Contains(file.Extension))
                {

                    string targetFilePath = Path.Combine(destinationDir, file.Name.Replace(oldJobNo, jobNo));
                    file.CopyTo(targetFilePath);
                }
            }

                   
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name.Replace(oldJobNo, jobNo));
                    CopyDirectory(subDir.FullName, newDestinationDir);
                }
            
        }

        private void textBoxJobNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtData.Text) && Directory.Exists(txtData.Text))
            {
                CopyDirectory(txtData.Text, newJobDirectoryData);
            }
            if (!string.IsNullOrWhiteSpace(txtArt.Text) && Directory.Exists(txtArt.Text))
            {
                CopyDirectory(txtArt.Text, newJobDirectoryArt);
            }


        }
    }
}
