using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;

namespace JobDocs
{
    public class PrintSpecSheet_
    {
        public string JobNo { get; set; }
        public string JobDirectory { get; set; }
        public string FileName { get; set; }
        public string PrintMachine { get; set; }
        public string PrintSize { get; set; }
        public string FinishedSize { get; set; }
        public string Stock { get; set; }
        public string Layout { get; set; }
        public List<string> StreamList { get; set; }
        public string Notes { get; set; }


        private void Print(string jobNo, int PrintMachine, string jobDirectory, string fileName, string PrintSize, string FinishedSize, string Stock, string layout, List<string> streamList, string notes)
        {

      

            PrintDocument printDoc = new PrintDocument();
  

            
                printDoc.DefaultPageSettings.Landscape = false;
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, 1170);
                printDoc.DocumentName = $"{jobNo} - Print Spec - {PrintMachine}";
                printDoc.PrinterSettings.PrinterName = "Adobe PDF";
            

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
     
                Font font1 = new Font("Calibri", 32, FontStyle.Bold);
                Font font2 = new Font("Calibri", 22);
                Font font3 = new Font("Calibri", 12);
                Font font4 = new Font("Calibri", 18, FontStyle.Bold);

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

                g.DrawString("PRINT SPECIFICATION SHEET", font1, Brushes.Black, x1 + 200, y1 + 100);
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
