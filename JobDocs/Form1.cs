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

namespace JobDocs
{
    public partial class Form1 : Form
    {
        List<string> columnsList = new List<string>();
        List<string> dtColumnsList = new List<string>
        {"<Not Mapped>", "Title", "First Name", "Last Name", "Company Name",
            "Address Line 1", "Address Line 2", "Address Line 3",
            "Locality","State","Postcode","Country"
        };
        string path = "";
        int columnCount = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         

            PdfReader pdfReader = new PdfReader(@"C:\Users\Gayan\Documents\MSOL\test data\DATA SUMMARY SHEET - APR18 - Fillable.pdf");
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(@"C:\Users\Gayan\Documents\MSOL\test data\filled.pdf", System.IO.FileMode.OpenOrCreate));
            pdfStamper.AcroFields.SetField("Col1", "222000");
            pdfStamper.Close();
            pdfReader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Filepath = @"C:\Users\Gayan\Documents\MSOL\test data\DATA SUMMARY SHEET - APR18 - Fillable.pdf";

            using (PrintDialog Dialog = new PrintDialog())
            {
                Dialog.ShowDialog();

                ProcessStartInfo printProcessInfo = new ProcessStartInfo()
                {
                    Verb = "print",
                    CreateNoWindow = true,
                    FileName = Filepath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process printProcess = new Process();
                printProcess.StartInfo = printProcessInfo;
                printProcess.Start();

                printProcess.WaitForInputIdle();

                Thread.Sleep(3000);

                if (false == printProcess.CloseMainWindow())
                {
                    printProcess.Kill();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (file.Substring(file.Length - 4, 4) == ".txt")
                {
                    path = file;
                }
                columnsList = BL_JobDocs.getColumns(path);
            }

            if (columnsList != null)
            {
                columnCount = columnsList.Count;
                for(int i=0;i<columnsList.Count;i++)
                {
                    
                    flowLayoutPanel1.Controls.Add(new TextBox { Text = columnsList[i], Name = $"txtBox{i}" });
                    flowLayoutPanel2.Controls.Add(new ComboBox { DataSource = new List<string>(dtColumnsList), Name = $"combo{i}" });
                }
            }
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}
