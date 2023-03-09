using PdfSharp.Xps;
using POS.Crypto;
using POS.FlexControl.FlexMessageBox;
using POS.FlexControls;
using POS.GlobalVariable;
using POS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Xml.Serialization;
using static POS.Helper.MessageBoxTitleHelper;

namespace POS.Report
{
    /// <summary>
    /// Логика взаимодействия для FlexDocumentWindow.xaml
    /// </summary>
    public partial class FlexDocumentWindow : INotifyPropertyChanged
    {
        public static readonly DependencyProperty VisibilityExportProperty = DependencyProperty.Register(
                "VisibilityExport",
                typeof(Visibility),
                typeof(FlexDocumentWindow));

        public Visibility VisibilityExport
        {
            get { return (Visibility)GetValue(VisibilityExportProperty); }
            set { SetValue(VisibilityExportProperty, value); }
        }

        public static readonly DependencyProperty VisibilitySendEmailProperty = DependencyProperty.Register(
                "VisibilitySendEmail",
                typeof(Visibility),
                typeof(FlexDocumentWindow));

        public Visibility VisibilitySendEmail
        {
            get { return (Visibility)GetValue(VisibilitySendEmailProperty); }
            set { SetValue(VisibilitySendEmailProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        XpsDocument xpsDocument;
        public ReportData reportData;
        ConvertData convertData;
        String screenName;

        public FlexDocumentWindow(XpsDocument _xpsDocument = null, String XpsDocPath = null)
        {
            InitializeComponent();

            if (_xpsDocument == null)
            {
                _xpsDocument = new XpsDocument(XpsDocPath, FileAccess.Read);
            }

            FixedDocumentSequence fds = _xpsDocument.GetFixedDocumentSequence();
            xpsDocument = _xpsDocument;
            reportData = new ReportData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReportData));
            convertData = new ConvertData();

            using (StringReader string_reader = new StringReader(xpsDocument.CoreDocumentProperties.Description))
            {
                reportData = (ReportData)(xmlSerializer.Deserialize(string_reader));
            }

            if (MainWindow.AppWindow.currentWindow == "POS.Screens.ReportSales.ReportSalesGroup")
            {
                VisibilityExport = Visibility.Collapsed;
            }
            else
            {
                VisibilityExport = Visibility.Visible;
            }

            if (
                MainWindow.AppWindow.currentWindow == "POS.Screens.TransferDocument.TransferDocumentGrid" ||
                MainWindow.AppWindow.currentWindow == "POS.Screens.TransferRelatedDocument.TransferRelatedDocumentGrid"
                )
            {
                if (convertData.FlexDataConvertToInt32(reportData.recieverShopNumber) < 100)
                {
                    VisibilitySendEmail = Visibility.Collapsed;
                }
                else
                {
                    VisibilitySendEmail = Visibility.Visible;
                }
            }
            else
            {
                VisibilitySendEmail = Visibility.Collapsed;
            }

            String[] screenNameArray = MainWindow.AppWindow.currentWindow.Split('.');
            if(screenNameArray.Length > 1)
            {
                screenName = screenNameArray[screenNameArray.Length - 1];
            }
            else
            {
                screenName = MainWindow.AppWindow.currentWindow;
            }

            fdv.Document = fds;
            if (fds.DocumentPaginator.GetPage(0).Size.Width <= fds.DocumentPaginator.GetPage(0).Size.Height)
            {
                Width = 640;
                Height = 800;
            }
            else
            {
                Width = 800;
                Height = 640;
            }
            if(MainWindow.AppWindow != null)
            {
                if (MainWindow.AppWindow.Height > 0 && MainWindow.AppWindow.Height < Height)
                {
                    Height = MainWindow.AppWindow.Height;
                }
            }
            FitContentToWindow();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if ((WindowState == System.Windows.WindowState.Maximized) || (WindowState == System.Windows.WindowState.Normal))
            {
                FitContentToWindow();
            }
        }

        private void FitContentToWindow()
        {
            fdv.FitToWidth();
            fdv.FitToHeight();
        }

        private void PART_ScrollContentPresenter_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void PART_ScrollContentPresenter_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                fdv.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
            }
            if (e.Key == Key.Down)
            {
                fdv.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
            }
            if ((e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) ||
                (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
            {
                e.Handled = true;
            }
        }

        private void ButtonSavePdf_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.Title = Properties.Resources.SavePdfFile;
            saveFileDialog.FileName = GetFileName();

            System.Windows.Forms.DialogResult result = saveFileDialog.ShowDialog();
            MainWindow.AppWindow.Focus();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(memoryStream, FileMode.Create);
                        XpsDocument xpsDocument = new XpsDocument(package);
                        XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                        xpsDocumentWriter.Write(this.xpsDocument.GetFixedDocumentSequence());
                        xpsDocument.Close();
                        package.Close();

                        var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(memoryStream);
                        XpsConverter.Convert(pdfXpsDoc, saveFileDialog.FileName, 0);
                    }
                }
                catch (Exception ex)
                {
                    FlexMessageBox mb = new FlexMessageBox();
                    mb.Show(Properties.Resources.ErrorFileIsOpen, GenerateTitle(TitleType.Error, Properties.Resources.ErrorFileIsOpen), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonSaveXps_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XPS|*.xps";
            saveFileDialog.Title = Properties.Resources.SaveXpsFile;
            saveFileDialog.FileName = GetFileName();

            System.Windows.Forms.DialogResult result = saveFileDialog.ShowDialog();
            MainWindow.AppWindow.Focus();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    XpsDocument xpsd = new XpsDocument(saveFileDialog.FileName, FileAccess.Write);
                    XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsd);
                    xpsDocumentWriter.Write(xpsDocument.GetFixedDocumentSequence());
                    xpsd.Close();
                }
                catch (Exception ex)
                {
                    FlexMessageBox mb = new FlexMessageBox();
                    mb.Show(Properties.Resources.ErrorFileIsOpen, GenerateTitle(TitleType.Error, Properties.Resources.ErrorFileIsOpen), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private String GetFileName()
        {
            String FileName = "";

            if (String.IsNullOrEmpty(reportData.documentNumber))
            {
                reportData.documentNumber = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            }

            if (!String.IsNullOrEmpty(reportData.typeDocument))
            {
                FileName = screenName + "_" + reportData.typeDocument + "_" + reportData.documentNumber;
            }
            else
            {
                FileName = screenName + "_" + reportData.documentNumber;
            }
            return FileName;
        }

        private void ButtonSendMail_Click(object sender, RoutedEventArgs e)
        {
            FlexTextEditMessageBox mb1 = new FlexTextEditMessageBox();
            mb1.EditBox.LabelText = Properties.Resources.EmailClient;
            mb1.EditBox.LabelWidth = 80;
            mb1.EditBox.Width = 260;
            mb1.EditBox.Text = "Shop" + reportData.recieverShopNumber + "@miuz.ru";
            if (GlobalVariable.Numeric.globalData.RoleID != 0)
            {
                mb1.EditBox.IsEnabled = false;
            }
            mb1.Owner = this;

            MessageBoxResult dialogresult = mb1.Show(Properties.Resources.MessageInputEmail, Properties.Resources.TitleInputEmail, System.Windows.Forms.MessageBoxButtons.OKCancel);
            if (dialogresult == MessageBoxResult.OK)
            {
                try
                {
                    String tempFilePath = String.Concat(System.IO.Path.GetTempPath(), @"\", GetFileName(), ".pdf");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(memoryStream, FileMode.Create);
                        XpsDocument xpsDocument = new XpsDocument(package);
                        XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                        xpsDocumentWriter.Write(this.xpsDocument.GetFixedDocumentSequence());
                        xpsDocument.Close();
                        package.Close();

                        var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(memoryStream);
                        XpsConverter.Convert(pdfXpsDoc, tempFilePath, 0);
                    }

                    List<FromServer> fromServers = new List<FromServer>();
                    String[] ServicesFromAddressArray = Numeric.globalData.emailClass.ServicesFromAddressEmail.Split(';');
                    String[] ServicesHostArray = Numeric.globalData.emailClass.ServicesHostEmail.Split(';');
                    String[] ServicesPasswordArray = Numeric.globalData.emailClass.ServicesPasswordEmail.Split(';');
                    String[] ServicesPortArray = Numeric.globalData.emailClass.ServicesPortEmail.Split(';');
                    Int32[] countArray = { ServicesFromAddressArray.Length, ServicesHostArray.Length, ServicesPasswordArray.Length, ServicesPortArray.Length };
                    Int32 count = countArray.Min();

                    for (Int32 i = 0; i < count; i++)
                    {
                        CryptDecrypt cryptDecrypt = new CryptDecrypt();
                        FromServer fromServer = new FromServer();
                        fromServer.ServicesFromAddress = ServicesFromAddressArray[i];
                        fromServer.ServicesHost = ServicesHostArray[i];
                        fromServer.ServicesPassword = cryptDecrypt.Decrypt(ServicesPasswordArray[i]);
                        fromServer.ServicesPort = ServicesPortArray[i];
                        fromServers.Add(fromServer);
                    }

                    String ServicesToAddress = mb1.EditBox.Text;
                    String subject = String.Concat(Properties.Resources.MessageSendMail2, reportData.documentNumber, Properties.Resources.MessageSendMail4, reportData.typeDocument, Properties.Resources.MessageSendMail3, Numeric.globalData.requisites["Номер магазина"].ToString());
                    String fromTitle = String.Concat("tamuzservice");
                    String body = String.Concat(Properties.Resources.MessageSendMail1);

                    foreach (FromServer fromServer in fromServers)
                    {
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = fromServer.ServicesHost;
                        smtpClient.Port = Int32.Parse(fromServer.ServicesPort);
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        if (smtpClient.Host.Trim().Contains("smtp.office365.com"))
                        {
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.EnableSsl = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        }
                        else
                        {
                            smtpClient.UseDefaultCredentials = true;
                            smtpClient.EnableSsl = false;
                        }

                        smtpClient.Credentials = new NetworkCredential(fromServer.ServicesFromAddress, fromServer.ServicesPassword);

                        using (MailMessage message = new MailMessage())
                        {
                            message.From = new MailAddress(fromServer.ServicesFromAddress, fromTitle);
                            message.Subject = subject;
                            message.Body = body;
                            message.Attachments.Add(new Attachment(tempFilePath, MediaTypeNames.Application.Pdf));
                            message.To.Add(ServicesToAddress);
                            smtpClient.Send(message);
                        }

                        FlexMessageBox mb = new FlexMessageBox();
                        mb.Owner = this;
                        mb.Show(Properties.Resources.MessageSendEmail, GenerateTitle(TitleType.Information, Properties.Resources.InformationTitle), MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    FlexMessageBox mb = new FlexMessageBox();
                    mb.Owner = this;
                    mb.Show(Properties.Resources.ErrorSendMail, GenerateTitle(TitleType.Error, Properties.Resources.ErrorSendMailTitle), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            MainWindow.AppWindow.Focus();
        }

        private void control_Closed(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.SystemIdle, new DispatcherOperationCallback(delegate { return null; }), null);
            GC.Collect();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }
    }
}