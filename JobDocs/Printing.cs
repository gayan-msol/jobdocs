using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobDocsLibrary;

namespace JobDocs
{
    public  class Printing
    {

        public async Task<int> PrintPSS(PrintSpecSheet printSpecSheet)
        {


            try
            {
                PrintDocument printDoc = new PrintDocument();



                printDoc.DefaultPageSettings.Landscape = false;
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, 1170);
                printDoc.DocumentName = $"{Form1.jobDirectoryData}\\{printSpecSheet.JobNo} - Print Spec Sheet - {printSpecSheet.PrintMachine}";
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

                    Font fontHeader = new Font("Calibri", 32, FontStyle.Bold);
                    Font fontFieldName = new Font("Calibri", 22);
                    Font fontMedium = new Font("Calibri", 14);
                    Font fontValueLargeBold = new Font("Calibri", 22, FontStyle.Bold);
                    Font fontValueLarge = new Font("Calibri", 22);
                    Font fontValueSmall = new Font("Calibri", 12);

                    Font font4 = new Font("Calibri", 18, FontStyle.Bold);

                    int x1 = ev.MarginBounds.Left;
                    int y1 = ev.MarginBounds.Top;
                    int w = ev.MarginBounds.Width;
                    int h = ev.MarginBounds.Height;
                    int xLeft = 70;
                    int xRight = w - xLeft;
                    int xHeader = 148;
                    int yHeader = 30;
                    int yLine1 = 80;
                    int yLineHeightL = 22;
                    int yLineHeightS = 10;
                    int yLineGap = 25;
                    int y2 = 65;
                    int y3 = 140;
                    int y4 = 160;

                    float lineCount = 0;
                    float yCurrent = 0;

                    StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
                    StringFormat formatCenter = new StringFormat(formatLeft);
                    formatCenter.Alignment = StringAlignment.Center;

                    g.DrawString("PRINT SPECIFICATION SHEET", fontHeader, Brushes.Black, xHeader, yHeader);

                    yCurrent = yLine1 + 20;
                    g.DrawString("Job No:", fontFieldName, Brushes.Black, xLeft, yCurrent);
                    g.DrawString(printSpecSheet.JobNo, fontValueLargeBold, Brushes.Black, xLeft + 100, yCurrent);

                    g.DrawString("Print Machine:", fontFieldName, Brushes.Black, xLeft + 300, yCurrent);
                    g.DrawString(printSpecSheet.PrintMachine, fontValueLargeBold, Brushes.Black, xLeft + 500, yCurrent);

                    lineCount++;
                    yCurrent += yLineHeightL + yLineGap;
                    g.DrawString("Job Name:", fontFieldName, Brushes.Black, xLeft, yCurrent);
                    RectangleF rectJobDir = new RectangleF(xLeft + 150, yCurrent, 550, 80);
                    if(g.MeasureString(printSpecSheet.JobName, fontFieldName).Width >545)
                    {
                        g.DrawString(printSpecSheet.JobName, fontMedium, Brushes.Black, rectJobDir);
                    }
                    else
                    {
                        g.DrawString(printSpecSheet.JobName, fontFieldName, Brushes.Black, rectJobDir);
                    }

                    lineCount++;
                    yCurrent += yLineHeightL + yLineGap;
                    g.DrawString("Customer:", fontFieldName, Brushes.Black, xLeft, yCurrent);
                    RectangleF rectCust = new RectangleF(xLeft + 150, yCurrent, 550, 80);
                    if (g.MeasureString(printSpecSheet.Customer, fontFieldName).Width > 545)
                    {
                        g.DrawString(printSpecSheet.Customer, fontMedium, Brushes.Black, rectCust);
                    }
                    else
                    {
                        g.DrawString(printSpecSheet.Customer, fontFieldName, Brushes.Black, rectCust);
                    }

                    lineCount += 2;
                    yCurrent += yLineHeightL + yLineGap +10;
                    g.DrawString("File Name:", fontFieldName, Brushes.Black, xLeft, yCurrent);
                    RectangleF rectFileName = new RectangleF(xLeft + 150, yCurrent + 10, 600, 150);
                    string fileNames = "";
                    foreach (string s in printSpecSheet.FileNames)
                    { fileNames += $"{s}\n"; }
                    g.DrawString(fileNames, fontValueSmall, Brushes.Black, rectFileName);

                    lineCount++;
                    yCurrent += 150 + yLineGap;
                    g.DrawString("Print Size:", fontMedium, Brushes.Black, xLeft, yCurrent);
                    RectangleF rectPrintSize = new RectangleF(xLeft + 100, yCurrent, 230, 44);
                    g.DrawString(printSpecSheet.PrintSize, fontMedium, Brushes.Black, rectPrintSize);

                    //g.DrawString("Guillo:", fontFieldName, Brushes.Black, xLeft +225, yLine1 + yLineHeight * lineCount + yLineGap * lineCount);
                    //RectangleF rectGuillo = new RectangleF(xLeft + 305, yLine1 + yLineHeight * lineCount + yLineGap * lineCount , 100, 44);
                    //g.DrawString(printSpecSheet.Guillotine, fontValueLarge, Brushes.Black, rectGuillo);
                    g.DrawString("Finished Size:", fontMedium, Brushes.Black, xLeft + 375, yCurrent);
                    RectangleF rectFinishSize = new RectangleF(xLeft + 500, yCurrent, 230, 44);
                    g.DrawString(printSpecSheet.FinishedSize, fontMedium, Brushes.Black, rectFinishSize);

                    lineCount += 0.75F;
                    yCurrent += yLineHeightS + yLineGap;
                    g.DrawString("Layout:", fontMedium, Brushes.Black, xLeft, yCurrent);
                    RectangleF rectLayout = new RectangleF(xLeft + 100, yCurrent, 400, 44);
                    g.DrawString(printSpecSheet.Layout, fontMedium, Brushes.Black, rectLayout);

                    g.DrawString("Guillotine:", fontMedium, Brushes.Black, xLeft + 375, yCurrent);
                    RectangleF rectGuillo = new RectangleF(xLeft + 500, yCurrent, 100, 44);
                    g.DrawString(printSpecSheet.Guillotine, fontMedium, Brushes.Black, rectGuillo);

                    lineCount += 0.75F;
                    // yCurrent += yLineHeightS + yLineGap;


                    lineCount += 0.75F;
                    yCurrent += yLineHeightS + yLineGap;
                    g.DrawString("Stock:", fontMedium, Brushes.Black, xLeft, yCurrent);
                    RectangleF rectStock = new RectangleF(xLeft + 100, yCurrent , 500, 100);

                    string stock = "";
                    foreach (string s in printSpecSheet.Stock)
                    { stock += $"{s}\n"; }
                    g.DrawString(stock, fontValueSmall, Brushes.Black, rectStock);



                    lineCount += 0.75F;
                    yCurrent += 130;
                    Rectangle rectStreams = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 250);
                    g.DrawRectangle(Pens.Black, rectStreams);
                    string streams = "";
                    foreach (string s in printSpecSheet.StreamList)
                    { streams += $"{s}\n"; }
                    g.DrawString(streams, fontValueSmall, Brushes.Black, rectStreams);

                    lineCount++;
                    yCurrent += 255;
                    Rectangle rectNotes = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 120);
                    g.DrawRectangle(Pens.Black, rectNotes);
                    g.DrawString(printSpecSheet.Notes, fontValueSmall, Brushes.Black, rectNotes);

                    lineCount++;
                    yCurrent += 125;
                    Rectangle rectClientApproval = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 60);
                    g.DrawRectangle(Pens.Black, rectClientApproval);
                    g.DrawLine(Pens.Black, xLeft + 223, (int)yCurrent, xLeft + 223, (int)yCurrent + 60);
                    g.DrawString("Approved By:", fontValueSmall, Brushes.Black, xLeft, (int)yCurrent);

                    g.DrawLine(Pens.Black, xLeft + 446, (int)yCurrent, xLeft + 446, (int)yCurrent + 60);
                    g.DrawString("From Company:", fontValueSmall, Brushes.Black, xLeft + 223, (int)yCurrent);

                    if (printSpecSheet.Approved)
                    {
                        g.DrawString(printSpecSheet.Contact, fontValueSmall, Brushes.Black, xLeft, (int)yCurrent + 30);
                        g.DrawString(printSpecSheet.Customer, fontValueSmall, Brushes.Black, xLeft + 223, (int)yCurrent + 30);
                        g.DrawString(Form1.userName, fontValueSmall, Brushes.Black, xLeft + 446, (int)yCurrent + 30);
                    }

                    g.DrawString("Approved to at MSOL:", fontValueSmall, Brushes.Black, xLeft + 446, (int)yCurrent);


                    lineCount++;
                    yCurrent += 60;
                    Rectangle rectSignOff = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 60);
                    g.DrawRectangle(Pens.Black, rectSignOff);
                    g.DrawLine(Pens.Black, xLeft + 223, (int)yCurrent, xLeft + 223, (int)yCurrent + 60);
                    g.DrawString("Printed By:", fontValueSmall, Brushes.Black, xLeft, (int)yCurrent);

                    g.DrawLine(Pens.Black, xLeft + 446, (int)yCurrent, xLeft + 446, (int)yCurrent + 60);
                    g.DrawString("Date:\n              /       /", fontValueSmall, Brushes.Black, xLeft + 223, (int)yCurrent);

                    string signoff = "";
                    if (printSpecSheet.PrintMachine == "DUPLO -> INKJET")
                    {
                        signoff = "  Duplo    |    Inkjet";
                    }
                    g.DrawString($"Sign Off:{signoff}", fontValueSmall, Brushes.Black, xLeft + 446, (int)yCurrent);                 
                }

                return 1;
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowMessage(ex);
                return 0;
            }
      


            
        }

        public async Task<int> PrintPRoductionReport(ProductionReport productionReport)
        {


            try
            {
                PrintDocument printDoc = new PrintDocument();



                printDoc.DefaultPageSettings.Landscape = false;
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 830, 1170);
                printDoc.DocumentName = $"{Form1.jobDirectoryData}\\{productionReport.JobNo} - Production Report";
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

                    Font fontHeader = new Font("Calibri", 32, FontStyle.Bold);
                    Font fontFieldName = new Font("Calibri", 22);
                    Font fontMedium = new Font("Calibri", 14);
                    Font fontValueLargeBold = new Font("Calibri", 22, FontStyle.Bold);
                    Font fontValueLarge = new Font("Calibri", 22);
                    Font fontValueSmall = new Font("Calibri", 12);

                    Font font4 = new Font("Calibri", 18, FontStyle.Bold);

                    int x1 = ev.MarginBounds.Left;
                    int y1 = ev.MarginBounds.Top;
                    int w = ev.MarginBounds.Width;
                    int h = ev.MarginBounds.Height;
                    int xLeft = 70;
                    int xRight = w - xLeft;
                    int xHeader = 148;
                    int yHeader = 30;
                    int yLine1 = 80;
                    int yLineHeightL = 22;
                    int yLineHeightS = 10;
                    int yLineGap = 25;
                    int y2 = 65;
                    int y3 = 140;
                    int y4 = 160;

                    float lineCount = 0;
                    float yCurrent = 0;

                    StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
                    StringFormat formatCenter = new StringFormat(formatLeft);
                    formatCenter.Alignment = StringAlignment.Center;

                    Image image = Image.FromFile(@"S:\SCRIPTS\DotNetProgrammes\PDF Templates\PRODUCTION REPORT.jpg");

                    g.DrawImage(image, image.Width, image.Height);

                    //lineCount += 0.75F;
                    //yCurrent += yLineHeightS + yLineGap;
                    //g.DrawString("Layout:", fontMedium, Brushes.Black, xLeft, yCurrent);
                    //RectangleF rectLayout = new RectangleF(xLeft + 100, yCurrent, 400, 44);
                    //g.DrawString(printSpecSheet.Layout, fontMedium, Brushes.Black, rectLayout);

                    //g.DrawString("Guillotine:", fontMedium, Brushes.Black, xLeft + 375, yCurrent);
                    //RectangleF rectGuillo = new RectangleF(xLeft + 500, yCurrent, 100, 44);
                    //g.DrawString(printSpecSheet.Guillotine, fontMedium, Brushes.Black, rectGuillo);

                    //lineCount += 0.75F;
                    //// yCurrent += yLineHeightS + yLineGap;


                    //lineCount += 0.75F;
                    //yCurrent += yLineHeightS + yLineGap;
                    //g.DrawString("Stock:", fontMedium, Brushes.Black, xLeft, yCurrent);
                    //RectangleF rectStock = new RectangleF(xLeft + 100, yCurrent, 500, 100);

                    //string stock = "";
                    //foreach (string s in printSpecSheet.Stock)
                    //{ stock += $"{s}\n"; }
                    //g.DrawString(stock, fontValueSmall, Brushes.Black, rectStock);



                    //lineCount += 0.75F;
                    //yCurrent += 130;
                    //Rectangle rectStreams = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 250);
                    //g.DrawRectangle(Pens.Black, rectStreams);
                    //string streams = "";
                    //foreach (string s in printSpecSheet.StreamList)
                    //{ streams += $"{s}\n"; }
                    //g.DrawString(streams, fontValueSmall, Brushes.Black, rectStreams);

                    //lineCount++;
                    //yCurrent += 255;
                    //Rectangle rectNotes = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 120);
                    //g.DrawRectangle(Pens.Black, rectNotes);
                    //g.DrawString(printSpecSheet.Notes, fontValueSmall, Brushes.Black, rectNotes);

                    //lineCount++;
                    //yCurrent += 125;
                    //Rectangle rectClientApproval = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 60);
                    //g.DrawRectangle(Pens.Black, rectClientApproval);
                    //g.DrawLine(Pens.Black, xLeft + 223, (int)yCurrent, xLeft + 223, (int)yCurrent + 60);
                    //g.DrawString("Approved By:", fontValueSmall, Brushes.Black, xLeft, (int)yCurrent);

                    //g.DrawLine(Pens.Black, xLeft + 446, (int)yCurrent, xLeft + 446, (int)yCurrent + 60);
                    //g.DrawString("From Company:", fontValueSmall, Brushes.Black, xLeft + 223, (int)yCurrent);

                    //if (printSpecSheet.Approved)
                    //{
                    //    g.DrawString(printSpecSheet.Contact, fontValueSmall, Brushes.Black, xLeft, (int)yCurrent + 30);
                    //    g.DrawString(printSpecSheet.Customer, fontValueSmall, Brushes.Black, xLeft + 223, (int)yCurrent + 30);
                    //    g.DrawString(Form1.userName, fontValueSmall, Brushes.Black, xLeft + 446, (int)yCurrent + 30);
                    //}

                    g.DrawString("Approved to at MSOL:", fontValueSmall, Brushes.Black, xLeft + 446, (int)yCurrent);


                    lineCount++;
                    yCurrent += 60;
                    Rectangle rectSignOff = new Rectangle(xLeft, (int)yCurrent, xRight - xLeft, 60);
                    g.DrawRectangle(Pens.Black, rectSignOff);
                    g.DrawLine(Pens.Black, xLeft + 223, (int)yCurrent, xLeft + 223, (int)yCurrent + 60);
                    g.DrawString("Printed By:", fontValueSmall, Brushes.Black, xLeft, (int)yCurrent);

                    g.DrawLine(Pens.Black, xLeft + 446, (int)yCurrent, xLeft + 446, (int)yCurrent + 60);
                    g.DrawString("Date:\n              /       /", fontValueSmall, Brushes.Black, xLeft + 223, (int)yCurrent);

               
                }

                return 1;
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowMessage(ex);
                return 0;
            }




        }
    }
}
