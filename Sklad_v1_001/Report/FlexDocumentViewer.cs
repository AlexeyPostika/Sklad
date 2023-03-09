using Sklad_v1_001.Report;
using Sklad_v1_001.Control.FlexMessageBox;
using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps;
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.Report
{
    public class FlexDocumentViewer : DocumentViewer
    {
        public FlexDocumentViewer()
        {
        }

        protected override void OnPrintCommand()
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.UserPageRangeEnabled = true;
                printDialog.CurrentPageEnabled = true;
                printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
                printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;
                FixedDocumentSequence docSeq = Document as FixedDocumentSequence;
                FixedDocument fixedDocument = docSeq.References[0].GetDocument(false);
                double documentWidth = Document.DocumentPaginator.GetPage(0).Size.Width;
                double documentHeight = Document.DocumentPaginator.GetPage(0).Size.Height;
                if (documentWidth <= documentHeight)
                {
                    printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
                }
                else
                {
                    printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                }
                if (printDialog.ShowDialog() == true)
                {
                    DocumentPaginator paginator = fixedDocument.DocumentPaginator;
                    if (printDialog.PageRangeSelection == PageRangeSelection.UserPages || printDialog.PageRangeSelection == PageRangeSelection.CurrentPage)
                    {
                        if (printDialog.PageRangeSelection == PageRangeSelection.CurrentPage)
                            printDialog.PageRange = new PageRange(MasterPageNumber, MasterPageNumber);
                        paginator = new PrintDocumentPaginator(fixedDocument.DocumentPaginator, printDialog.PageRange);
                        printDialog.PrintDocument(paginator, "PrintDocument");
                    }
                    if (printDialog.PageRangeSelection == PageRangeSelection.AllPages)
                    {
                        docSeq.PrintTicket = printDialog.PrintTicket;
                        XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);
                        writer.WriteAsync(docSeq, printDialog.PrintTicket);
                    }
                }
            }
            catch (Exception e)
            {
                FlexMessageBox mb1 = new FlexMessageBox();
                mb1.Show(Properties.Resources.PrintServerError, GenerateTitle(TitleType.Error, Properties.Resources.Print), MessageBoxButton.OK, MessageBoxImage.Error, 1);
            }
        }
    }
}