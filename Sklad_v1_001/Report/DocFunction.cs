using MessagingToolkit.Barcode;
using Sklad_v1_001.Control.FlexProgressBar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Xml.Serialization;

namespace Sklad_v1_001.Report
{
    public class DocFunction
    {
        public FlexProgressBarData progressBarData;
        public Boolean isMemoryStream;

        Dictionary<String, String> fileDct;
        String xpsOutPath;
        String tempPath;
        Double progressItemSize;
        Guid fileGuid;
        public String filePath;

        public DocFunction()
        {
            fileGuid = Guid.NewGuid();
            tempPath = Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + @"\Temp\Printdoc\";
            if (!System.IO.Directory.Exists(tempPath))
                System.IO.Directory.CreateDirectory(tempPath);

            if (!isMemoryStream)
            {
                xpsOutPath = tempPath + @"printdoc_" + fileGuid;
                if (!System.IO.Directory.Exists(xpsOutPath))
                    System.IO.Directory.CreateDirectory(xpsOutPath);
            }

            filePath = tempPath + fileGuid + ".xps";
        }

        public Double GetTextBlockHeightMonth(DataRow tableRow, TextBox tableTextBox, TableRow TableHeader, Double pageWidth)
        {
            Double maxheigth = 0;
            TableCell cell = TableHeader.Cells[0];
            TextBlock testTextBlock = new TextBlock();
            testTextBlock.FontFamily = tableTextBox.FontFamily;
            testTextBlock.FontSize = tableTextBox.FontSize;
            testTextBlock.TextWrapping = TextWrapping.Wrap;
            testTextBlock.Text = tableRow[0].ToString();
            testTextBlock.Width = pageWidth - 40;
            testTextBlock.Padding = new Thickness(
                    tableTextBox.Padding.Left + cell.Padding.Left + cell.BorderThickness.Left,
                    tableTextBox.Padding.Top + cell.Padding.Top + cell.BorderThickness.Top,
                    tableTextBox.Padding.Right + cell.Padding.Right + cell.BorderThickness.Right,
                    tableTextBox.Padding.Bottom + cell.Padding.Bottom + cell.BorderThickness.Bottom
            );
            testTextBlock.Measure(new System.Windows.Size(pageWidth, Double.PositiveInfinity));
            testTextBlock.Arrange(new Rect(testTextBlock.DesiredSize));
            maxheigth = (Double)testTextBlock.DesiredSize.Height;

            return maxheigth;
        }

        public Double GetTextBlockHeight(DataRow tableRow, TableRow TableHeader)
        {
            Double maxheigth = 0;
            TextBlock testTextBlock = new TextBlock();
            TextBox tableTextBox;
            Int32 i = 0;

            foreach (TableCell cell in TableHeader.Cells)
            {
                tableTextBox = (cell.Blocks.FirstBlock as BlockUIContainer).Child as TextBox;

                testTextBlock.Text = tableRow[i].ToString();
                testTextBlock.FontFamily = tableTextBox.FontFamily;
                testTextBlock.FontSize = tableTextBox.FontSize;
                testTextBlock.TextWrapping = tableTextBox.TextWrapping;
                testTextBlock.Width = tableTextBox.ActualWidth;
                testTextBlock.Padding = new Thickness(
                        tableTextBox.Padding.Left + cell.Padding.Left + cell.BorderThickness.Left,
                        tableTextBox.Padding.Top + cell.Padding.Top + cell.BorderThickness.Top,
                        tableTextBox.Padding.Right + cell.Padding.Right + cell.BorderThickness.Right,
                        tableTextBox.Padding.Bottom + cell.Padding.Bottom + cell.BorderThickness.Bottom
                );

                testTextBlock.Measure(new System.Windows.Size(tableTextBox.Width, Double.PositiveInfinity));
                testTextBlock.Arrange(new Rect(testTextBlock.DesiredSize));
                if ((double)testTextBlock.ActualHeight > maxheigth)
                    maxheigth = (double)testTextBlock.ActualHeight;

                i++;
            }

            return maxheigth;
        }

        public Table AddTable(Table tableCopy, Table ContentDataTable, TextBox Column1, FlowDocument FD)
        {
            Table tableCopy1 = new Table();
            tableCopy1.CellSpacing = 0;
            tableCopy1.Margin = new Thickness(0);
            tableCopy1.Padding = tableCopy.Padding;
            tableCopy1.FontFamily = Column1.FontFamily;
            tableCopy1.FontSize = Column1.FontSize;

            tableCopy1.BorderBrush = tableCopy.BorderBrush;
            tableCopy1.BorderThickness = tableCopy.BorderThickness;

            foreach (TableRowGroup tableRowGroup1 in tableCopy.RowGroups)
            {
                TableRowGroup tableRowGroup = new TableRowGroup();
                TableRow tableRow2 = new TableRow();
                if (tableRowGroup1.Rows.Count > 0)
                {
                    tableRow2 = (TableRow)XamlReader.Parse(XamlWriter.Save(tableRowGroup1.Rows[0]));
                    tableRowGroup.Rows.Add(tableRow2);
                    tableCopy1.RowGroups.Add(tableRowGroup);
                }
            }

            if (tableCopy1.RowGroups.Count > 1)
            {
                for (Int32 i = 0; i < tableCopy1.RowGroups.Count; i++)
                {
                    if (i > 0)
                    {
                        tableCopy1.RowGroups[i].Rows.Clear();
                    }
                }
            }

            tableCopy1.BreakPageBefore = true;
            tableCopy1.Style = tableCopy.Style;

            for (Int32 i = 0; i < tableCopy.Columns.Count; i++)
            {
                TableColumn tableColumn = new TableColumn();
                tableColumn.Width = tableCopy.Columns[i].Width;
                tableCopy1.Columns.Add(tableColumn);
            }

            FD.Blocks.Add(tableCopy1);

            return tableCopy1;
        }

        public FlowDocument AddBreakSection(
            FlowDocument FD,
            Window currentWindow,
            FlowDocument FlowDocumentFooter,
            FlowDocumentScrollViewer FlowDocumentScrollViewerFooter,
            Int32 totalPages,
            Boolean showTotalPages,
            Int32 pageNumber,
            Table tableCopy
            )
        {
            if (isMemoryStream)
            {
                Section section = new Section();
                section.Margin = new Thickness(0);
                section.Padding = new Thickness(0, 0, 0, 0);
                section.FontSize = 0.1;
                section.BreakPageBefore = true;
                FD.Blocks.Add(section);
            }
            else
            {
                Thickness pagePadding = FD.PagePadding;
                Double columnWidth = FD.ColumnWidth;
                Style style = FD.Style;
                String xpsPath = tempPath + "printdoc_" + Guid.NewGuid();

                SaveAsXps(
                    currentWindow,
                    FlowDocumentFooter,
                    FlowDocumentScrollViewerFooter,
                    FD,
                    totalPages,
                    pageNumber,
                    showTotalPages,
                    xpsPath
                );

                FD = new FlowDocument();
                FD.PagePadding = pagePadding;
                FD.ColumnWidth = columnWidth;
            }
            return FD;
        }

        void GenerateXps(String xpsFullPath, FlowDocument FD, FlowDocument FlowDocumentFooter, System.Windows.Size PageSize, System.Windows.Size FooterSize, Int32 totalPages, Int32 pageNumber, Boolean showTotalPages)
        {
            XpsDocument xpsDocument;

            using (Package container = Package.Open(xpsFullPath, FileMode.Create))
            {
                using (xpsDocument = new XpsDocument(container, CompressionOption.Maximum))
                {
                    Report.ReportData reportData = FD.Tag as Report.ReportData;
                    if (reportData == null)
                    {
                        reportData = new ReportData();
                        reportData.documentNumber = "";
                        reportData.recieverShopNumber = "";
                        reportData.typeDocument = "";
                    }
                    XmlSerializer serializer = new XmlSerializer(typeof(Report.ReportData));
                    StringWriter writer = new StringWriter();
                    serializer.Serialize(writer, reportData);
                    xpsDocument.CoreDocumentProperties.Description = writer.ToString();

                    XpsPackagingPolicy xpsPackagingPolicy = new XpsPackagingPolicy(xpsDocument);
                    XpsSerializationManager rsm = new XpsSerializationManager(xpsPackagingPolicy, false);
                    DocumentPaginator paginator = ((IDocumentPaginatorSource)FD).DocumentPaginator;
                    paginator = new DocumentPaginatorWrapper(paginator, PageSize, FlowDocumentFooter, FooterSize, totalPages, showTotalPages, progressBarData);
                    if (progressBarData.PbStatusBreak != true)
                    {
                        rsm.SaveAsXaml(paginator);
                        rsm.Commit();
                    }
                }
                container.Close();
            }
        }

        public void SaveAsXps(
            Window currentWindow,
            FlowDocument FlowDocumentFooter,
            FlowDocumentScrollViewer FlowDocumentScrollViewerFooter,
            FlowDocument FD,
            Int32 totalPages,
            Int32 pageNumber,
            Boolean showTotalPages,
            String xpsPath
        )
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(delegate { return null; }), null);

            String xpsFullPath = xpsPath + "_" + pageNumber + ".xps";
            String dirTempPath = xpsPath + "_" + pageNumber;


            System.Windows.Size PageSize = new System.Windows.Size(currentWindow.Width, currentWindow.Height);
            System.Windows.Size FooterSize = new System.Windows.Size(FlowDocumentFooter.ColumnWidth, PageSize.Height - FlowDocumentScrollViewerFooter.Height);

            GenerateXps(xpsFullPath, FD, FlowDocumentFooter, PageSize, FooterSize, totalPages, pageNumber, showTotalPages);

            if (progressBarData.PbStatusBreak != true)
            {
                if (pageNumber == 1)
                {
                    ExtractZip(xpsFullPath, xpsOutPath);
                    Thread syncThread = new Thread(new ThreadStart(delegate
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(xpsFullPath);
                        fi.Delete();
                    }));
                    syncThread.Priority = ThreadPriority.Highest;
                    syncThread.SetApartmentState(ApartmentState.MTA);
                    syncThread.IsBackground = true;
                    syncThread.Start();
                }
                else
                {
                    ExtractZip(xpsFullPath, dirTempPath);
                    Directory.CreateDirectory(xpsOutPath + @"\Documents\1\Pages\");
                    Directory.CreateDirectory(xpsOutPath + @"\Documents\1\Pages\_rels\");
                    File.Copy(dirTempPath + @"\Documents\1\Pages\1.fpage", xpsOutPath + @"\Documents\1\Pages\" + pageNumber + ".fpage", true);
                    File.Copy(dirTempPath + @"\Documents\1\Pages\_rels\1.fpage.rels", xpsOutPath + @"\Documents\1\Pages\_rels\" + pageNumber + ".fpage.rels", true);

                    String[] files = System.IO.Directory.GetFiles(dirTempPath + @"\Resources");
                    Directory.CreateDirectory(xpsOutPath + @"\Resources\");
                    foreach (string file in files)
                    {
                        String fileName = System.IO.Path.GetFileName(file);
                        File.Move(file, xpsOutPath + @"\Resources\" + fileName);
                    }

                    Thread syncThread = new Thread(new ThreadStart(delegate
                    {
                        FileInfo fi = new System.IO.FileInfo(xpsFullPath);
                        fi.Delete();
                        DirectoryInfo di = new System.IO.DirectoryInfo(dirTempPath);
                        di.Delete(true);
                    }));

                    syncThread.Priority = ThreadPriority.Highest;
                    syncThread.SetApartmentState(ApartmentState.MTA);
                    syncThread.IsBackground = true;
                    syncThread.Start();
                }
            }
        }

        void ExtractZip(String xpsFullPath, String dirFullPath)
        {
            if (!Directory.Exists(dirFullPath))
                Directory.CreateDirectory(dirFullPath);
            //ZipFile.ExtractToDirectory(xpsFullPath, dirFullPath);
        }

        void AddFilesToZip(string zipFile, Dictionary<String, String> fileDct, FlowDocument FD)
        {
            Double progressTotal = progressBarData.PbStatusValue;
            progressItemSize = 5 / (Double)fileDct.Count;

            MemoryStream memStream = new MemoryStream();
            Package zip = System.IO.Packaging.Package.Open(memStream, FileMode.OpenOrCreate);
            foreach (KeyValuePair<string, string> entry in fileDct)
            {
                string destFilename = ".\\" + entry.Value + Path.GetFileName(entry.Key);
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                    zip.DeletePart(uri);
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
                using (FileStream fileStream = new FileStream(entry.Key, FileMode.Open, FileAccess.Read))
                {
                    using (Stream dest = part.GetStream())
                    {
                        CopyStream(fileStream, dest);
                    }
                }

                progressTotal = progressTotal + progressItemSize;
                progressBarData.PbStatusValue = (Int32)progressTotal;

                GcCollectAll();
            }
            using (FileStream zipfilestream = new FileStream(zipFile, FileMode.Append, FileAccess.Write))
            {
                memStream.Position = 0;
                CopyStream(memStream, zipfilestream);
            }
            memStream.Close();
        }

        void CopyStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, bytesRead);
        }

        /*public void ViewXps(String xpsPath)
        {
            XpsDocument xpsDocument = new XpsDocument(xpsPath, FileAccess.Read);            
            FlexDocumentWindow printpreview = new FlexDocumentWindow(xpsDocument, convertData.FlexDataConvertToInt64(FD.Tag.ToString()));
            printpreview.Owner = MainWindow.AppWindow;
            printpreview.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            printpreview.Title = Properties.Resources.DocumentPreview;
            MainWindow.AppWindow.Activate();
            printpreview.Show();
            xpsDocument.Close();

            GcCollectAll();
        }*/

        public void GcCollectAll()
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(delegate { return null; }), null);
            GC.Collect();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        public void SaveXps(FlowDocument FD, string xpsPath, Int32 pageNumber)
        {
            using (Package package = Package.Open(xpsPath + "_" + pageNumber + ".xps", FileMode.Create))
            {
                using (var xpsDoc = new XpsDocument(package, CompressionOption.NotCompressed))
                {
                    var xpsSm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
                    DocumentPaginator dp = ((IDocumentPaginatorSource)FD).DocumentPaginator;
                    dp.PageSize = new System.Windows.Size(1122, 793);
                    xpsSm.SaveAsXaml(dp);
                }
            }
        }

        public void AddRowToTable(TableRowGroup RowGroups, DataRow tableRow, TableRow TableHeader, String[] cellAligmnet)
        {
            TableRow currentRow = new TableRow();
            TextBlock txBlock;
            BlockUIContainer blockUIContainer;
            TableCell tablecell;
            TextBox tableTextBox;
            Int32 i = 0;
            foreach (TableCell cell in TableHeader.Cells)
            {
                tableTextBox = (cell.Blocks.FirstBlock as BlockUIContainer).Child as TextBox;

                txBlock = new TextBlock();
                txBlock.Text = tableRow[i].ToString();

                if (tableTextBox.ActualWidth > tableTextBox.Width)
                    txBlock.Width = tableTextBox.ActualWidth;
                else
                    txBlock.Width = tableTextBox.Width;

                txBlock.FontFamily = tableTextBox.FontFamily;
                txBlock.FontSize = tableTextBox.FontSize;
                txBlock.TextWrapping = tableTextBox.TextWrapping;
                txBlock.TextAlignment = (TextAlignment)System.Enum.Parse(typeof(TextAlignment), cellAligmnet[i]);

                blockUIContainer = new BlockUIContainer(txBlock);
                blockUIContainer.Margin = new Thickness(0);
                blockUIContainer.Padding = new Thickness(0);

                tablecell = new TableCell(blockUIContainer);
                tablecell.ColumnSpan = cell.ColumnSpan;
                tablecell.Style = cell.Style;
                currentRow.Cells.Add(tablecell);
                i++;
            }
            RowGroups.Rows.Add(currentRow);
        }

        public void AddFixedRowToTable(TableRowGroup RowGroups, DataRow tableRow, TableRow TableHeader, String[] cellAligmnet)//, Report.LabelDocument7x23.ReportData docinfo
        {
            TableRow currentRow = new TableRow();
            TextBox textBox;
            BlockUIContainer blockUIContainer;
            TableCell tablecell;
            TextBox tableTextBox;
            Int32 i = 0;
            Int32 k = 0;

            foreach (TableCell cell in TableHeader.Cells)
            {
                tableTextBox = (cell.Blocks.FirstBlock as BlockUIContainer).Child as TextBox;
                tablecell = new TableCell();

                if (k == 0 || k == 2 || k == 4 || k == 6 || k == 8 || k == 10)
                {
                    String[] lines = tableRow[i].ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    String[] fontSize = lines[lines.Length - 1].Split(new[] { '|' }, StringSplitOptions.None);

                    for (Int32 line = 0; line < lines.Length - 1; line++)
                    {
                        textBox = new TextBox();
                        textBox.Text = lines[line].ToString();
                        textBox.Width = tableTextBox.Width;
                        textBox.FontFamily = tableTextBox.FontFamily;
                        textBox.FontSize = Double.Parse(fontSize[line].ToString());
                        textBox.TextWrapping = TextWrapping.Wrap;
                        textBox.Height = tableTextBox.Height / (lines.Length - 1);
                        textBox.TextAlignment = TextAlignment.Center;

                        //if (docinfo.isDiscount == true && line == 0)
                        //{
                        //    if (k == 2 || k == 6 || k == 10)
                        //    {
                        //        textBox.TextDecorations = TextDecorations.Strikethrough;
                        //    }
                        //}

                        if ((lines.Length - 1) == 2 && line == 0)
                        {
                            textBox.VerticalAlignment = VerticalAlignment.Center;
                            textBox.VerticalContentAlignment = VerticalAlignment.Center;
                        }
                        if ((lines.Length - 1) == 2 && line == 1)
                        {
                            textBox.VerticalAlignment = VerticalAlignment.Top;
                            textBox.VerticalContentAlignment = VerticalAlignment.Top;
                        }

                        if ((lines.Length - 1) == 1 && line == 0)
                        {
                            textBox.VerticalAlignment = VerticalAlignment.Center;
                            textBox.VerticalContentAlignment = VerticalAlignment.Center;
                        }

                        blockUIContainer = new BlockUIContainer(textBox);
                        tablecell.Blocks.Add(blockUIContainer);
                    }
                    tablecell.ColumnSpan = cell.ColumnSpan;
                    tablecell.Style = cell.Style;
                    currentRow.Cells.Add(tablecell);
                    i++;
                }
                if (k == 11 || k == 12)
                {
                    tablecell.ColumnSpan = cell.ColumnSpan;
                    tablecell.Style = cell.Style;
                    currentRow.Cells.Add(tablecell);
                }

                if (k == 1 || k == 5 || k == 9)
                {
                    textBox = new TextBox();
                    textBox.Text = "→";
                    textBox.Width = tableTextBox.Width;
                    textBox.FontFamily = tableTextBox.FontFamily;
                    textBox.FontSize = tableTextBox.FontSize;
                    textBox.TextWrapping = TextWrapping.Wrap;
                    textBox.Height = tableTextBox.Height;
                    textBox.VerticalAlignment = VerticalAlignment.Center;
                    textBox.VerticalContentAlignment = VerticalAlignment.Center;
                    textBox.Margin = new Thickness(-10, 0, 0, 0);
                    blockUIContainer = new BlockUIContainer(textBox);

                    tablecell = new TableCell(blockUIContainer);
                    tablecell.ColumnSpan = cell.ColumnSpan;
                    tablecell.Style = cell.Style;
                    currentRow.Cells.Add(tablecell);
                }
                k++;
            }
            RowGroups.Rows.Add(currentRow);
        }

        public void GenerateBarCode(String documentNumber, System.Windows.Controls.Image barcodeImage)
        {
            Int64 temp;
            if (Int64.TryParse(documentNumber.ToString(), out temp))
            {
                string barcodeString = documentNumber;
                Int32 QualityPrint = 10;
                BarcodeEncoder barcodeEncoder = new BarcodeEncoder();
                barcodeEncoder.CharacterSet = "UTF-8";
                barcodeEncoder.Width = Int32.Parse(barcodeImage.Width.ToString()) * QualityPrint;
                barcodeEncoder.Height = Int32.Parse(barcodeImage.Height.ToString()) * QualityPrint;
                barcodeEncoder.IncludeLabel = false;
                //barcodeEncoder.LabelFont = new Font("Arial", QualityPrint * 10);
                WriteableBitmap img = barcodeEncoder.Encode(BarcodeFormat.Code128, barcodeString);
                barcodeImage.Source = img;
            }
        }

        public void PrintXps(Window currentWindow,
            FlowDocument FlowDocumentFooter,
            FlowDocumentScrollViewer FlowDocumentScrollViewerFooter,
            FlowDocument FD,
            FlexProgressBarData progressBarData,
            Int32 totalPages,
            Boolean showTotalPages,
            Boolean isMemoryStream,
            Table tableCopy = null
            )
        {
            XpsDocument xpsDocument = null;
            String xpsPath;

            System.Windows.Size PageSize = new System.Windows.Size(currentWindow.Width, currentWindow.Height);
            System.Windows.Size FooterSize = new System.Windows.Size(FlowDocumentFooter.ColumnWidth, PageSize.Height - FlowDocumentScrollViewerFooter.Height);

            if (isMemoryStream)
            {
                xpsPath = "pack://" + Guid.NewGuid() + ".xps";
                Stream fileStream = new MemoryStream();
                Package package = Package.Open(fileStream, FileMode.Create, FileAccess.ReadWrite);
                Uri documentUri = new Uri(xpsPath);
                PackageStore.AddPackage(documentUri, package);
                xpsDocument = new XpsDocument(package, CompressionOption.NotCompressed, documentUri.AbsoluteUri);
                XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDocument), false);
                DocumentPaginator paginator = ((IDocumentPaginatorSource)FD).DocumentPaginator;
                paginator = new DocumentPaginatorWrapper(paginator, PageSize, FlowDocumentFooter, FooterSize, totalPages, showTotalPages, progressBarData);
                if (progressBarData.PbStatusBreak != true)
                {
                    try
                    {
                        rsm.SaveAsXaml(paginator);
                        rsm.Commit();

                    }
                    catch { }
                }
            }
            else
            {
                if (progressBarData.PbStatusBreak != true)
                {
                    FD = AddBreakSection(FD, currentWindow, FlowDocumentFooter, FlowDocumentScrollViewerFooter, 0, showTotalPages, 1, tableCopy);

                    String fixedDocumentStr = @"<FixedDocument xmlns=""http://schemas.microsoft.com/xps/2005/06"">";
                    for (Int32 i = 1; i <= totalPages; i++)
                    {
                        String pageNumber = (i).ToString();
                        fixedDocumentStr = String.Concat(fixedDocumentStr, @"<PageContent Source=""Pages/" + pageNumber + @".fpage""/>");
                        String pageString = File.ReadAllText(xpsOutPath + @"\Documents\1\Pages\" + pageNumber + ".fpage", Encoding.Default);
                        pageString = pageString.Replace("из 1234567890", "из " + (totalPages).ToString());
                        File.WriteAllText(xpsOutPath + @"\Documents\1\Pages\" + pageNumber + ".fpage", pageString);
                    }
                    fixedDocumentStr = String.Concat(fixedDocumentStr, @"</FixedDocument>");
                    File.WriteAllText(xpsOutPath + @"\Documents\1\FixedDocument.fdoc", fixedDocumentStr);

                    fileDct = new Dictionary<string, string>();
                    foreach (string file in Directory.EnumerateFiles(xpsOutPath, "*", SearchOption.AllDirectories))
                    {
                        String relativePath = file.Replace(xpsOutPath, "").Substring(1);
                        fileDct[file] = relativePath.Replace(Path.GetFileName(relativePath), "");
                    }
                    AddFilesToZip(xpsOutPath + ".xps", fileDct, FD);

                    xpsDocument = new XpsDocument(xpsOutPath + ".xps", FileAccess.Read);
                }
            }

            if (isMemoryStream)
            {
                Report.ReportData reportData = FD.Tag as Report.ReportData;
                if (reportData == null)
                {
                    reportData = new ReportData();
                    reportData.documentNumber = "";
                    reportData.recieverShopNumber = "";
                    reportData.typeDocument = "";
                }
                XmlSerializer serializer = new XmlSerializer(typeof(Report.ReportData));
                StringWriter writer = new StringWriter();
                serializer.Serialize(writer, reportData);
                xpsDocument.CoreDocumentProperties.Description = writer.ToString();
            }

            progressBarData.XpsDoc = xpsDocument;

            Thread syncThread = new Thread(new ThreadStart(delegate
            {
                DirectoryInfo di = new System.IO.DirectoryInfo(xpsOutPath);
                if (System.IO.Directory.Exists(xpsOutPath))
                    di.Delete(true);
            }));

            syncThread.Priority = ThreadPriority.Highest;
            syncThread.SetApartmentState(ApartmentState.MTA);
            syncThread.IsBackground = true;
            syncThread.Start();
        }

        public void ViewXps(String xpsPath)
        {
            //XpsDocument xpsDocument = new XpsDocument(xpsPath, FileAccess.Read);
            //FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
            //FlexDocumentWindow printpreview = new FlexDocumentWindow(fds);
            //printpreview.Owner = MainWindow.AppWindow;
            //printpreview.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //printpreview.Title = Properties.Resources.DocumentPreview;
            //MainWindow.AppWindow.Activate();
            //printpreview.Show();
            //xpsDocument.Close();
            //GcCollect();
        }













        // Новый механизм
        public List<RowDefinition> listRowDefinition;
        public List<AccessText> listTextBox;
        public List<StackPanel> listStackPanel;
        public List<ContentControl> listContentControl;
        AccessText textBox;
        RowDefinition rowDefinition;
        StackPanel stackPanel;
        ContentControl contentControl;

        void AddFilesToZip(string zipFile, Dictionary<String, String> fileDct)
        {
            MemoryStream memStream = new MemoryStream();
            Package zip = System.IO.Packaging.Package.Open(memStream, FileMode.OpenOrCreate);
            foreach (KeyValuePair<string, string> entry in fileDct)
            {
                string destFilename = ".\\" + entry.Value + Path.GetFileName(entry.Key);
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                    zip.DeletePart(uri);
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Maximum);
                using (FileStream fileStream = new FileStream(entry.Key, FileMode.Open, FileAccess.Read))
                {
                    using (Stream dest = part.GetStream())
                    {
                        CopyMemoryStream(fileStream, dest);
                    }
                }
            }
            using (FileStream zipfilestream = new FileStream(zipFile, FileMode.Append, FileAccess.Write))
            {
                memStream.Position = 0;
                CopyMemoryStream(memStream, zipfilestream);
            }
            memStream.Close();
        }

        void CopyMemoryStream(Stream source, Stream target)
        {
            const int bufSize = 0x1000;
            byte[] buf = new byte[bufSize];
            int bytesRead = 0;
            while ((bytesRead = source.Read(buf, 0, bufSize)) > 0)
            {
                target.Write(buf, 0, bytesRead);
                target.Flush();
            }
        }

        public void ClearChildren(Grid _grid)
        {
            if (_grid != null && _grid.Children.Count > 0)
            {
                StackPanel _rootStackPanel = _grid.Children[0] as StackPanel;
                if (_rootStackPanel != null)
                {
                    if (_rootStackPanel.Children.Count > 0)
                    {
                        Grid _rootGrid = _rootStackPanel.Children[0] as Grid;
                        if (_rootGrid != null)
                        {
                            foreach (UIElement _uIElement in _rootGrid.Children)
                            {
                                StackPanel _stackPanel = _uIElement as StackPanel;
                                if (_stackPanel != null)
                                {
                                    _stackPanel.Children.Clear();
                                }
                            }
                            _rootGrid.Children.Clear();
                        }
                        _rootGrid.RowDefinitions.Clear();
                    }
                    _rootStackPanel.Children.Clear();
                }
                _grid.Children.Clear();
            }
        }

        public void DisposeAll(DataTable documentDataTable)
        {
            for (int i = 0; i < listTextBox.Count; i++)
            {
                listTextBox[i] = null;
                listTextBox.Remove(listTextBox[i]);
            }
            listTextBox = null;

            for (int i = 0; i < listStackPanel.Count; i++)
            {
                listStackPanel[i] = null;
                listStackPanel.Remove(listStackPanel[i]);
            }
            listStackPanel = null;

            for (int i = 0; i < listContentControl.Count; i++)
            {
                listContentControl[i] = null;
                listContentControl.Remove(listContentControl[i]);
            }
            listContentControl = null;
            documentDataTable.Dispose();
            documentDataTable = null;
        }

        public void GcCollect()
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(delegate { return null; }), null);
            GC.Collect();
        }


        Window window;
        XmlSerializer serializer;
        StringWriter stringWriter;
        Grid grid;
        RowDefinition gridRow;
        DataRow dataRow;
        Grid rootGridLocal;
        StackPanel stackPanelLocal;
        Int32 currentRow;
        String[] linesString;
        String[] fontSize;
        Int32 pageNumber;
        Double progressTotal;
        StackPanel rootStackPanel;
        String gridTableCopyString;
        Grid gridTableCopy;
        GridLength rowHeight;
        Size size;
        Rect rect;
        public void GetElementsLabel(Window window)
        {
            listRowDefinition = new List<RowDefinition>();
            for (int indexElement = 0; indexElement < 7 * 23; indexElement++)
            {
                rowDefinition = new RowDefinition();
                listRowDefinition.Add(rowDefinition);
            }

            listStackPanel = new List<StackPanel>();
            for (int indexElement = 0; indexElement < 7 * 23; indexElement++)
            {
                stackPanel = new StackPanel();
                stackPanel.VerticalAlignment = VerticalAlignment.Center;
                listStackPanel.Add(stackPanel);
            }

            listTextBox = new List<AccessText>();
            for (int indexElement = 0; indexElement < 7 * 23 * 3; indexElement++)
            {
                textBox = new AccessText();
                textBox.Text = "";
                textBox.Style = (Style)window.TryFindResource("AccessTextCenter10");
                listTextBox.Add(textBox);
            }

            listContentControl = new List<ContentControl>();
            for (int indexElement = 0; indexElement < 7 * 23; indexElement++)
            {
                contentControl = new ContentControl();
                contentControl.Style = (Style)window.TryFindResource("IconArrow");
                contentControl.HorizontalAlignment = HorizontalAlignment.Center;
                contentControl.VerticalAlignment = VerticalAlignment.Center;
                listContentControl.Add(contentControl);
            }
        }

        public void GenerateLabelXps(
            FlexProgressBarData progressBarData,
            Window _window,
            String documentNumber,
            String title,
            DataTable documentDataTable,
            StackPanel _rootStackPanel,
            Grid gridTable,
            bool isDiscount,
            GridLength _rowHeight
        )
        {
            window = _window;
            rowHeight = _rowHeight;
            gridTableCopyString = XamlWriter.Save(gridTable);
            gridTableCopy = (Grid)XamlReader.Parse(gridTableCopyString);
            gridTableCopy.Children.Clear();

            Report.ReportData reportData = new Report.ReportData();
            reportData.documentNumber = documentNumber.ToString();
            reportData.typeDocument = title.ToString();

            pageNumber = 1;
            progressTotal = 0;
            progressItemSize = 100 / ((Double)documentDataTable.Rows.Count);

            rootStackPanel = _rootStackPanel;

            try
            {
                rootGridLocal = new Grid();
                stackPanelLocal = new StackPanel();
                using (XpsDocument xpsDocument = new XpsDocument(filePath, FileAccess.Write, CompressionOption.NotCompressed))
                {
                    serializer = new XmlSerializer(typeof(Report.ReportData));
                    stringWriter = new StringWriter();
                    serializer.Serialize(stringWriter, reportData);
                    xpsDocument.CoreDocumentProperties.Description = stringWriter.ToString();

                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                    SerializerWriterCollator collator = writer.CreateVisualsCollator();
                    collator.BeginBatchWrite();
                    currentRow = 0;
                    while (currentRow < documentDataTable.Rows.Count)
                    {
                        grid = CreatePageLabel(documentDataTable, isDiscount);
                        collator.Write(grid);
                        ClearChildren(grid);
                        if (pageNumber % 10 == 0)
                        {
                            GcCollect();
                        }
                        if (progressBarData.PbStatusBreak)
                        {
                            collator.Cancel();
                            break;
                        }
                    }
                    collator.EndBatchWrite();
                }
                progressBarData.XpsDocPath = filePath;
            }
            catch (Exception ex)
            {
            }
            ClearChildren(grid);
            DisposeAll(documentDataTable);
            GcCollectAll();
        }

        public Grid CreatePageLabel(DataTable documentDataTable, bool isDiscount)
        {
            if (pageNumber % 10 == 0)
            {
                GcCollectAll();
            }

            stackPanelLocal.Margin = rootStackPanel.Margin;
            rootGridLocal.Children.Add(stackPanelLocal);
            stackPanelLocal.Children.Add(gridTableCopy);
            Int32 rowGrid = 0;
            Int32 stackPanelIndex = 0;
            Int32 texBoxIndex = 0;
            while (true)
            {
                gridRow = listRowDefinition[rowGrid];
                gridRow.Height = rowHeight;
                gridTableCopy.RowDefinitions.Add(gridRow);
                for (int column = 0; column < 6; column++)
                {
                    dataRow = documentDataTable.Rows[currentRow];
                    linesString = dataRow[column].ToString().Split(new String[] { Environment.NewLine }, StringSplitOptions.None);
                    fontSize = linesString[linesString.Length - 1].Split(new[] { '|' }, StringSplitOptions.None);
                    for (int countString = 0; countString < linesString.Length - 1; countString++)
                    {
                        if (isDiscount == true && countString == 0)
                        {
                            if (column * 2 == 2 || column * 2 == 6 || column * 2 == 10)
                            {
                                listTextBox[texBoxIndex].TextDecorations = TextDecorations.Strikethrough;
                            }
                        }
                        listTextBox[texBoxIndex].Text = linesString[countString];
                        listTextBox[texBoxIndex].FontSize = Double.Parse(fontSize[countString].ToString());
                        listStackPanel[stackPanelIndex].Children.Add(listTextBox[texBoxIndex]);
                        texBoxIndex++;
                    }
                    gridTableCopy.Children.Add(listStackPanel[stackPanelIndex]);
                    Grid.SetColumn(listStackPanel[stackPanelIndex], column * 2);
                    Grid.SetRow(listStackPanel[stackPanelIndex], rowGrid);
                    if (column == 1 || column == 3 || column == 5)
                    {
                        gridTableCopy.Children.Add(listContentControl[stackPanelIndex]);
                        Grid.SetColumn(listContentControl[stackPanelIndex], column * 2 - 1);
                        Grid.SetRow(listContentControl[stackPanelIndex], rowGrid);
                    }
                    stackPanelIndex++;
                }
                rowGrid++;
                currentRow++;
                progressTotal = progressTotal + progressItemSize;
                progressBarData.PbStatusValue = (Int32)progressTotal;
                if (rowGrid > 23 || currentRow >= documentDataTable.Rows.Count)
                {
                    break;
                }
            }
            size = new System.Windows.Size(window.Width, window.Height);
            rect = new Rect(0, 0, window.Width, window.Height);
            rootGridLocal.Measure(size);
            rootGridLocal.Arrange(rect);
            rootGridLocal.UpdateLayout();

            pageNumber = pageNumber + 1;

            return rootGridLocal;
        }
    }
}
