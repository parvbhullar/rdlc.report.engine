/* ====================================================================

*/

using System;
using Oranikle.Report.Engine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace Oranikle.Report.Engine
{

    ///<summary>
    /// Renders a report to PDF.   This is a page oriented formatting renderer.
    ///</summary>
    public class RenderDMP : IPresent
    {
        Report r;					// report
        DMPTextWriter tw;				// where the output is going
        
        StringBuilder dmpContent;
        static readonly char[] lineBreak = new char[] { '\n' };
        static readonly char[] wordBreak = new char[] { ' ' };
        //		static readonly int MEASUREMAX = int.MaxValue;  //  .Net 2 doesn't seem to have a limit; 1.1 limit was 32
        static readonly int MEASUREMAX = 32;  //  guess I'm wrong -- .Net 2 doesn't seem to have a limit; 1.1 limit was 32
        
        bool IsInPageHeader = false;
        bool IsInPageFooter = false;
        bool IsInBody = false;

        public RenderDMP(Report rep, IStreamGen sg)
        {
            r = rep;
            tw = new DMPTextWriter(sg.GetTextWriter(), "");
        }

        public Report Report()
        {
            return r;
        }

        public bool IsPagingNeeded()
        {
            return true;
        }

        public void Start()
        {
            // Create the anchor for all pdf objects
            CompressionConfig cc = RdlEngineConfig.GetCompression();
            
            //Set initialize dmpcontent
            dmpContent = new StringBuilder("");
        }

        public void End()
        {
            //Write everything
            tw.Write(dmpContent);
            return;
        }

        public void RunPages(Pages pgs)	// this does all the work
        {
            foreach (Page p in pgs)
            {
                //Generate DMP Content Frame 
               
                DMPPage page = new DMPPage(p, r);
               
                tw.Pages.Add(page);
               
                ProcessPage(pgs, p);

                dmpContent.Append(page.ContentToString());
            }
            return;
        }

        // render all the objects in a page in PDF
        private void ProcessPage(Pages pgs, IEnumerable items)
        {
            foreach (PageItem pi in items)
            {
                if (pi.SI.BackgroundImage != null)
                {	
                }

                if (pi is PageTextHtml)
                {
                    PageTextHtml pth = pi as PageTextHtml;
                    
                    continue;
                }

                if (pi is PageText)
                {
                    PageText pt = pi as PageText;
                    tw.LastPage.IsInBody = IsInBody;
                    tw.LastPage.IsInPageFooter = IsInPageFooter;
                    tw.LastPage.IsInPageHeader = IsInPageHeader;
                    tw.LastPage.InsertContent(pt.Text, pt.X, pt.Y, pt.W, pt.H, pt.SI, pt.CanGrow); 
                    continue;
                }

                if (pi is PageLine)
                {
                    PageLine pl = pi as PageLine;
                    tw.LastPage.IsInBody = IsInBody;
                    tw.LastPage.IsInPageFooter = IsInPageFooter;
                    tw.LastPage.IsInPageHeader = IsInPageHeader;
                    tw.LastPage.InsertLine(pl.X, pl.Y, pl.X2, pl.Y2, pl.SI);
                    continue;
                }

                if (pi is PageEllipse)
                {
                    PageEllipse pe = pi as PageEllipse;
                   // elements.AddEllipse(pe.X, pe.Y, pe.H, pe.W, pe.SI, pe.HyperLink);
                    continue;
                }

                if (pi is PageImage)
                {
                   continue;
                }

                if (pi is PageRectangle)
                {
                    PageRectangle pr = pi as PageRectangle;
                    
                    continue;
                }
                if (pi is PagePie)
                {   // TODO
                    PagePie pp = pi as PagePie;
                    // elements.AddPie(pr.X, pr.Y, pr.H, pr.W, pi.SI, pi.HyperLink, patterns, pi.Tooltip);
                    continue;
                }
                if (pi is PagePolygon)
                {
                    PagePolygon ppo = pi as PagePolygon;
                    continue;
                }
                if (pi is PageCurve)
                {
                    PageCurve pc = pi as PageCurve;
                    continue;
                }
            }
        }

        // Body: main container for the report
        public void BodyStart(Body b)
        {
            IsInBody = true;
        }

        public void BodyEnd(Body b)
        {
            IsInBody = false;
        }

        public void PageHeaderStart(PageHeader ph)
        {
            IsInPageHeader = true;
        }

        public void PageHeaderEnd(PageHeader ph)
        {
            IsInPageHeader = false;
        }

        public void PageFooterStart(PageFooter pf)
        {
            IsInPageFooter = true;
        }

        public void PageFooterEnd(PageFooter pf)
        {
            IsInPageFooter = false;
        }

        public void Textbox(Textbox tb, string t, Row row)
        {
        }

        public void DataRegionNoRows(DataRegion d, string noRowsMsg)
        {
        }

        // Lists
        public bool ListStart(List l, Row r)
        {
            return true;
        }

        public void ListEnd(List l, Row r)
        {
        }

        public void ListEntryBegin(List l, Row r)
        {
        }

        public void ListEntryEnd(List l, Row r)
        {
        }

        // Tables					// Report item table
        public bool TableStart(Table t, Row row)
        {
            return true;
        }

        public void TableEnd(Table t, Row row)
        {
        }

        public void TableBodyStart(Table t, Row row)
        {
        }

        public void TableBodyEnd(Table t, Row row)
        {
        }

        public void TableFooterStart(Footer f, Row row)
        {
        }

        public void TableFooterEnd(Footer f, Row row)
        {
        }

        public void TableHeaderStart(Header h, Row row)
        {
        }

        public void TableHeaderEnd(Header h, Row row)
        {
        }

        public void TableRowStart(TableRow tr, Row row)
        {
        }

        public void TableRowEnd(TableRow tr, Row row)
        {
        }

        public void TableCellStart(TableCell t, Row row)
        {
            return;
        }

        public void TableCellEnd(TableCell t, Row row)
        {
            return;
        }

        public bool MatrixStart(Matrix m, MatrixCellEntry[,] matrix, Row r, int headerRows, int maxRows, int maxCols)				// called first
        {
            return true;
        }

        public void MatrixColumns(Matrix m, MatrixColumns mc)	// called just after MatrixStart
        {
        }

        public void MatrixCellStart(Matrix m, ReportItem ri, int row, int column, Row r, float h, float w, int colSpan)
        {
        }

        public void MatrixCellEnd(Matrix m, ReportItem ri, int row, int column, Row r)
        {
        }

        public void MatrixRowStart(Matrix m, int row, Row r)
        {
        }

        public void MatrixRowEnd(Matrix m, int row, Row r)
        {
        }

        public void MatrixEnd(Matrix m, Row r)				// called last
        {
        }

        public void Chart(Chart c, Row r, ChartBase cb)
        {
        }

        public void Image(Oranikle.Report.Engine.Image i, Row r, string mimeType, Stream ior)
        {
        }

        public void Line(Line l, Row r)
        {
            return;
        }

        public bool RectangleStart(Oranikle.Report.Engine.Rectangle rect, Row r)
        {
            return true;
        }

        public void RectangleEnd(Oranikle.Report.Engine.Rectangle rect, Row r)
        {
        }

        public void Subreport(Subreport s, Row r)
        {
        }

        public void GroupingStart(Grouping g)			// called at start of grouping
        {
        }
        public void GroupingInstanceStart(Grouping g)	// called at start for each grouping instance
        {
        }
        public void GroupingInstanceEnd(Grouping g)	// called at start for each grouping instance
        {
        }
        public void GroupingEnd(Grouping g)			// called at end of grouping
        {
        }
    }
}
