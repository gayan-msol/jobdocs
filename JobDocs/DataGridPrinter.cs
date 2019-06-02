using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace JobDocs
{

	public class DataGridPrinter
	{

		private PrintDocument ThePrintDocument;
		private DataTable TheTable;
		private DataGridView  TheDataGrid;

		public int RowCount = 0;  // current count of rows;
		private const int kVerticalCellLeeway = 10;
		public int PageNumber = 1;
		public ArrayList Lines = new ArrayList();

		int PageWidth;
		int PageHeight;
		int TopMargin;
		int BottomMargin;

        int leftMargin = 10;


		public DataGridPrinter(DataGridView aGrid, PrintDocument aPrintDocument, DataTable aTable)
		{
		
			TheDataGrid = aGrid;
			ThePrintDocument = aPrintDocument;
			TheTable = aTable;

			PageWidth = ThePrintDocument.DefaultPageSettings.PaperSize.Width;
			PageHeight = ThePrintDocument.DefaultPageSettings.PaperSize.Height;
			TopMargin = ThePrintDocument.DefaultPageSettings.Margins.Top;
			BottomMargin = ThePrintDocument.DefaultPageSettings.Margins.Bottom;

		}

		public void DrawHeader(Graphics g)
		{
			SolidBrush ForeBrush = new SolidBrush(SystemColors.InfoText);
			SolidBrush BackBrush = new SolidBrush(SystemColors.Window);
			Pen TheLinePen = new Pen(SystemColors.WindowText, 2);
			StringFormat cellformat = new StringFormat();
			cellformat.Trimming = StringTrimming.EllipsisCharacter;
			cellformat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
            Font headerFont = new Font("Calibri", 10, FontStyle.Bold);


			int columnwidth = PageWidth/TheTable.Columns.Count;

			int initialRowCount = RowCount;

			// draw the table header
			float startxposition = TheDataGrid.Location.X + leftMargin;
			RectangleF nextcellbounds = new RectangleF(0,0, 0, 0);

			RectangleF HeaderBounds  = new RectangleF(0, 0, 0, 0);

			HeaderBounds.X = TheDataGrid.Location.X;
			HeaderBounds.Y = TheDataGrid.Location.Y + TopMargin + (RowCount - initialRowCount) * (TheDataGrid.Font.SizeInPoints  + kVerticalCellLeeway);
			HeaderBounds.Height = TheDataGrid.Font.SizeInPoints + kVerticalCellLeeway;
			HeaderBounds.Width = PageWidth;

			g.FillRectangle(BackBrush, HeaderBounds);

			for (int k = 0; k < TheDataGrid.Columns.Count; k++)
			{
                float colWidth = GetColumnWidth(TheDataGrid,k, g, headerFont);
             //   float colWidth = TheDataGrid.Columns[k].Width;
                var w = TheDataGrid.Columns[k].Width;


                string nextcolumn = TheTable.Columns[k].ToString();
				RectangleF cellbounds = new RectangleF(startxposition, TheDataGrid.Location.Y + TopMargin + (RowCount - initialRowCount) * (TheDataGrid.Font.SizeInPoints  + kVerticalCellLeeway),
                   colWidth,
                    SystemFonts.DefaultFont.SizeInPoints + kVerticalCellLeeway);
				nextcellbounds = cellbounds;

				if (startxposition + colWidth <= PageWidth)
				{
					g.DrawString(nextcolumn, headerFont, ForeBrush, cellbounds, cellformat);
				}

                startxposition = startxposition + colWidth;

            }
	
			//if (TheDataGrid.GridLineStyle != DataGridLineStyle.None)
				g.DrawLine(TheLinePen, TheDataGrid.Location.X + leftMargin, nextcellbounds.Bottom, PageWidth, nextcellbounds.Bottom);
		}

		public bool DrawRows(Graphics g)
		{
			int lastRowBottom = TopMargin;

			try
			{
				SolidBrush ForeBrush = new SolidBrush(TheDataGrid.ForeColor);
				SolidBrush BackBrush = new SolidBrush(SystemColors.Window);
				SolidBrush AlternatingBackBrush = new SolidBrush(SystemColors.ControlLight);
				Pen TheLinePen = new Pen(TheDataGrid.ForeColor, 1);
				StringFormat cellformat = new StringFormat();
				cellformat.Trimming = StringTrimming.EllipsisCharacter;
				cellformat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
				int columnwidth = PageWidth/TheTable.Columns.Count;
                columnwidth = 0;
				int initialRowCount = RowCount;

				RectangleF RowBounds  = new RectangleF(0, 0, 0, 0);

				// draw vertical lines




				// draw the rows of the table
				for (int i = initialRowCount; i < TheTable.Rows.Count; i++)
				{
					DataRow dr = TheTable.Rows[i];
					int startxposition = TheDataGrid.Location.X + leftMargin;

                    RowBounds.X = TheDataGrid.Location.X + leftMargin ;
					RowBounds.Y = TheDataGrid.Location.Y + TopMargin + ((RowCount - initialRowCount)+1) * (TheDataGrid.Font.SizeInPoints  + kVerticalCellLeeway);
					RowBounds.Height = TheDataGrid.Font.SizeInPoints + kVerticalCellLeeway;
					RowBounds.Width = PageWidth;
					Lines.Add(RowBounds.Bottom);

					if (i%2 == 0)
					{
						g.FillRectangle(BackBrush, RowBounds);
					}
					else
					{
						g.FillRectangle(AlternatingBackBrush, RowBounds);
					}


					for (int j = 0; j < TheDataGrid.Columns.Count; j++)
					{

                        columnwidth = TheDataGrid.Columns[j].Width;


                        RectangleF cellbounds = new RectangleF(startxposition, 
							TheDataGrid.Location.Y + TopMargin + ((RowCount - initialRowCount) + 1) * (TheDataGrid.Font.SizeInPoints  + kVerticalCellLeeway),
							columnwidth, 
							TheDataGrid.Font.SizeInPoints + kVerticalCellLeeway);
									

						if (startxposition + columnwidth <= PageWidth)
						{
							g.DrawString(dr[j].ToString(), TheDataGrid.Font, ForeBrush, cellbounds, cellformat);
							lastRowBottom = (int)cellbounds.Bottom;
						}

						startxposition = startxposition + columnwidth;

                        g.DrawLine(TheLinePen, startxposition, TheDataGrid.Location.Y + TopMargin,startxposition,lastRowBottom);

                    }

                    RowCount++;

					if (RowCount * (TheDataGrid.Font.SizeInPoints  + kVerticalCellLeeway) > (PageHeight * PageNumber) - (BottomMargin+TopMargin))
					{
						DrawHorizontalLines(g, Lines);
						//DrawVerticalGridLines(g, TheLinePen, columnwidth, lastRowBottom);
						return true;
					}


				}

				DrawHorizontalLines(g, Lines);
               // DrawVerticalGridLines(g, TheLinePen, columnwidth, lastRowBottom);
                return false;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
				return false;
			}

		}

        float GetColumnWidth(DataGridView dataGridView,int colIndex, Graphics g, Font font)
        {
            float l = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    string s = row.Cells[colIndex].Value?.ToString() ?? "";
                    float width = g.MeasureString(s, font).Width;

                    l = width > l ? width : l;

                }
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowMessage(ex);
            }
          

            return l;
         

        }

		void DrawHorizontalLines(Graphics g, ArrayList lines)
		{
			Pen TheLinePen = new Pen(SystemColors.WindowText, 1);

			//if (TheDataGrid.GridLineStyle == DataGridLineStyle.None)
			//	return;

			for (int i = 0;  i < lines.Count; i++)
			{
					g.DrawLine(TheLinePen, TheDataGrid.Location.X + leftMargin, (float)lines[i], PageWidth, (float)lines[i]);
			}
		}

		void DrawVerticalGridLines(Graphics g, Pen TheLinePen, int columnwidth, int bottom)
		{
			//if (TheDataGrid.GridLineStyle == DataGridLineStyle.None)
			//	return;

			for (int k = 0; k < TheTable.Columns.Count; k++)
			{
				g.DrawLine(TheLinePen, TheDataGrid.Location.X + leftMargin  + k*columnwidth, 
					TheDataGrid.Location.Y + TopMargin,
					TheDataGrid.Location.X + leftMargin + k*columnwidth,
					bottom);
			}
		}


		public bool DrawDataGrid(Graphics g)
		  {

			try
			{
				DrawHeader(g);
				bool bContinue = DrawRows(g);
				return bContinue;
			}
			catch (Exception ex)
			{
			  MessageBox.Show(ex.Message.ToString());
			  return false;
			}

		  }

	}

}
