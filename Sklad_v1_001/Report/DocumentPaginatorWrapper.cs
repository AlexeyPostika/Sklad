using Sklad_v1_001.Control.FlexProgressBar;
using System;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Xps.Packaging;

namespace Sklad_v1_001.Report
{
    public class DocumentPaginatorWrapper : DocumentPaginator
    {
        Size m_PageSize;
        Size m_FooterSize;
        DocumentPaginator m_Paginator;
        FlowDocument m_DocumentFooter;
        FlowDocument copy_DocumentFooter;
        Int32 m_TotalPages;
        Boolean m_ShowTotalPages;
        double m_progressItemSize;
        double m_progressTotal;
        FlexProgressBarData m_progressBarData;

        DocumentPage page;
        ContainerVisual containerVisual;
        DocumentPaginator paginator;
        DocumentPage newpagefooter;
        DrawingVisual drawingVisual;

        public DocumentPaginatorWrapper(DocumentPaginator paginator, Size pageSize, FlowDocument DocumentFooter, Size FooterSize, Int32 TotalPages, Boolean ShowTotalPages, FlexProgressBarData progressBarData)
        {
            //Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(delegate { return null; }), null);

            m_PageSize = pageSize;
            m_FooterSize = FooterSize;
            m_Paginator = paginator;
            m_DocumentFooter = DocumentFooter;
            m_TotalPages = TotalPages;
            m_ShowTotalPages = ShowTotalPages;
            m_Paginator.PageSize = new Size(m_PageSize.Width, m_PageSize.Height);
            m_progressItemSize = 50 / (double)TotalPages;
            m_progressTotal = 50;
            m_progressBarData = progressBarData;
        }

        Rect Move(Rect rect)
        {
            if (rect.IsEmpty)
            {
                return rect;
            }
            else
            {
                return new Rect(rect.Left, rect.Top, rect.Width, rect.Height);
            }
        }

        public void DrawPageNumber(DrawingVisual content, Int32 pageNumber)
        {
            using (DrawingContext drawingContext = content.RenderOpen())
            {
                String drawContent = "";
                if (m_TotalPages == 0)
                {
                    drawContent = Properties.Resources.PageDocPaginator + (pageNumber + 1).ToString() + Properties.Resources.FromDocPaginator + "1234567890";
                }
                else
                {
                    drawContent = Properties.Resources.PageDocPaginator + (pageNumber + 1).ToString() + Properties.Resources.FromDocPaginator + m_TotalPages.ToString();
                }

                FormattedText formattedText = new FormattedText(drawContent, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black);
                Double width_text = new Size(formattedText.Width, formattedText.Height).Width;
                Point point = new Point(m_PageSize.Width / 2 - width_text / 2, m_PageSize.Height - 30);
                drawingContext.DrawText(formattedText, point);

                //drawTestLine(drawingContext, Brushes.Red, 52.713333333333331);
                //drawTestLine(drawingContext, Brushes.Red, 52.713333333333331 + 75);
                //drawTestLine(drawingContext, Brushes.Red, 52.713333333333331 + 75 + 18.8);
                //drawTestLine(drawingContext, Brushes.Green, 202.5 + 159.4 + 15 * 12.5 + 116.4);
            }
        }

        void drawTestLine(DrawingContext drawingContext, SolidColorBrush solidColorBrush, Double posY)
        {
            drawingContext.DrawLine(new Pen(solidColorBrush, 1), new Point(0, posY), new Point(m_PageSize.Width, posY));
        }

        public override DocumentPage GetPage(Int32 pageNumber)
        {
            if (m_progressBarData.PbStatusBreak != true)
            {
                page = m_Paginator.GetPage(pageNumber);
                containerVisual = new ContainerVisual();
                containerVisual.Children.Add(page.Visual);

                if (m_ShowTotalPages)
                {
                    drawingVisual = new DrawingVisual();
                    DrawPageNumber(drawingVisual, pageNumber);
                    containerVisual.Children.Add(drawingVisual);
                }

                if (m_FooterSize.Height > 10)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (Package container = Package.Open(memoryStream, FileMode.Create, FileAccess.ReadWrite))
                        {
                            Visual footervisual;
                            using (XpsDocument xpsDoc = new XpsDocument(container, CompressionOption.NotCompressed))
                            {
                                copy_DocumentFooter = new FlowDocument();
                                AddDocument(m_DocumentFooter, copy_DocumentFooter);
                                copy_DocumentFooter.ColumnWidth = m_DocumentFooter.ColumnWidth;
                                copy_DocumentFooter.PagePadding = m_DocumentFooter.PagePadding;
                                paginator = ((IDocumentPaginatorSource)copy_DocumentFooter).DocumentPaginator;
                                paginator = new FooterPaginatorWrapper(paginator, new System.Windows.Size(m_PageSize.Width, m_FooterSize.Height));
                                newpagefooter = paginator.GetPage(0);
                                footervisual = newpagefooter.Visual;
                            }
                            containerVisual.Children.Add(footervisual);
                        }
                    }
                }

                if (m_TotalPages > 0)
                {
                    m_progressTotal = m_progressTotal + m_progressItemSize;
                    m_progressBarData.PbStatusValue = (Int32)m_progressTotal;
                }

                return new DocumentPage(containerVisual, m_PageSize, Move(page.BleedBox), Move(page.ContentBox));
            }
            else
            {
                return null;
            }
        }

        public static void AddDocument(FlowDocument from, FlowDocument to)
        {
            if (from != null)
            {
                TextRange range = new TextRange(from.ContentStart, from.ContentEnd);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XamlWriter.Save(range, memoryStream);
                    range.Save(memoryStream, DataFormats.XamlPackage);
                    TextRange textRange2 = new TextRange(to.ContentEnd, to.ContentEnd);
                    textRange2.Load(memoryStream, DataFormats.XamlPackage);
                }
            }
        }

        public override Boolean IsPageCountValid
        {
            get { return m_Paginator.IsPageCountValid; }
        }

        public override Int32 PageCount
        {
            get { return m_Paginator.PageCount; }
        }

        public override Size PageSize
        {
            get { return m_Paginator.PageSize; }

            set { m_Paginator.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return m_Paginator.Source; }
        }
    }

    public class FooterPaginatorWrapper : DocumentPaginator
    {
        Size m_PageSize;
        DocumentPaginator m_Paginator;
        DocumentPage page;
        ContainerVisual containerVisual;
        DocumentPage documentPage;

        public FooterPaginatorWrapper(DocumentPaginator paginator, Size pageSize)
        {
            m_PageSize = pageSize;
            m_Paginator = paginator;
            m_Paginator.PageSize = new Size(m_PageSize.Width, m_PageSize.Height);
        }

        Rect Move(Rect rect)
        {
            if (rect.IsEmpty)
            {
                return rect;
            }
            else
            {
                return new Rect(0, rect.Top, m_Paginator.PageSize.Width, m_Paginator.PageSize.Height);
            }
        }

        public override DocumentPage GetPage(Int32 pageNumber)
        {
            page = m_Paginator.GetPage(pageNumber);
            containerVisual = new ContainerVisual();
            containerVisual.Children.Add(page.Visual);
            containerVisual.Transform = new TranslateTransform(0, m_PageSize.Height);
            documentPage = new DocumentPage(containerVisual, m_PageSize, Move(page.BleedBox), Move(page.ContentBox));

            return documentPage;
        }

        public override Boolean IsPageCountValid
        {
            get { return m_Paginator.IsPageCountValid; }
        }

        public override Int32 PageCount
        {
            get { return m_Paginator.PageCount; }
        }

        public override Size PageSize
        {
            get { return m_Paginator.PageSize; }

            set { m_Paginator.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return m_Paginator.Source; }
        }
    }
}
