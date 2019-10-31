using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobDocsLibrary
{
    public class Print
    {
        public PrintDocument CreatePrintDocument(string page, int count, bool unknownTotal, string type, int startPage = 1)
        {

            int total = count + startPage - 1;

            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.Landscape = true;
      
             if (type == "Box")
            {
                printDoc.DefaultPageSettings.Landscape = true;
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, 1170);
                printDoc.DocumentName = $"Box Lables - {"job no"}";
                printDoc.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";

            }
            printDoc.DefaultPageSettings.Margins.Left = 10;
            printDoc.DefaultPageSettings.Margins.Right = 10;//100 = 1 inch = 2.54 cm
            printDoc.DefaultPageSettings.Margins.Top = 10;
            printDoc.DefaultPageSettings.Margins.Bottom = 10;
            int i = startPage - 1;


            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            return printDoc;

            void printDoc_PrintPage(object senderp, PrintPageEventArgs ev)
            {
                Graphics g = ev.Graphics;
                string jobNo = "testJobNo";
                string customer = "testCustomer";
                string stream = "testStream";
                string eLMS = "";
                string notes = "";
                Font fontHeader = new Font("Calibri", 32, FontStyle.Bold);
                string jobName = "testJobName";
                Font font2 = new Font("Calibri", 22);
                string tot = unknownTotal ? "" : $"of {total}";
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

                if (type == "Tray")
                {
                    string trayNo = $"Tray {i + 1} {tot}";
                    RectangleF rectF1 = new RectangleF(x1, y2, 450, 70);
                    Rectangle rect2 = new Rectangle(310, y2 + 80, 150, 50);

                    g.DrawString(jobNo, fontHeader, Brushes.Black, x1, y1);
                    g.DrawString(jobName, font2, Brushes.Black, rectF1, formatCenter);
                    g.DrawString(customer, font3, Brushes.Black, x1, y3);
                    g.DrawRectangle(Pens.Black, x1, y2, 450, 70);

                    if (i == total + 1)
                    {
                        g.DrawString("Printed Date :                  Time:       ", font4, Brushes.Black, x1, y4);
                    }
                    else if (i == total)
                    {
                        g.DrawString("- Spoils", fontHeader, Brushes.Black, 160, y1);
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

                    g.DrawString(jobNo, fontHeader, Brushes.Black, x1, y1);
                    g.DrawString(jobName, font2, Brushes.Black, rectF1, formatCenter);
                    g.DrawString(customer, font3, Brushes.Black, x1, y3);
                    g.DrawRectangle(Pens.Black, x1, y2, 450, 70);
                    g.DrawString("- Spoils", fontHeader, Brushes.Black, 160, y1);
                    g.DrawString($"eLMS No:  {eLMS}", font4, Brushes.Black, x1, y4);
                }
                else if (type == "Box")
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

            }

       
        }
    }
}
