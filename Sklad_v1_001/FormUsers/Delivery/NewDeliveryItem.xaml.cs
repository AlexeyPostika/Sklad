using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.FormUsers.Delivery
{
    /// <summary>
    /// Логика взаимодействия для NewDeliveryItem.xaml
    /// </summary>
    public partial class NewDeliveryItem : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static readonly DependencyProperty IsEnableInvoiceProperty = DependencyProperty.Register(
                       "IsEnableInvoice",
                       typeof(Boolean),
                      typeof(NewDeliveryItem), new PropertyMetadata(false));
        //IsEnableAdd
        public static readonly DependencyProperty IsEnableAddInvoiceProperty = DependencyProperty.Register(
                       "IsEnableAddInvoice",
                       typeof(Boolean),
                      typeof(NewDeliveryItem), new PropertyMetadata(true));

        public static readonly DependencyProperty IsEnableTTNProperty = DependencyProperty.Register(
                      "IsEnableTTN",
                      typeof(Boolean),
                     typeof(NewDeliveryItem), new PropertyMetadata(false));
        
        public static readonly DependencyProperty IsEnableAddTTNProperty = DependencyProperty.Register(
                       "IsEnableAddTTN",
                       typeof(Boolean),
                      typeof(NewDeliveryItem), new PropertyMetadata(true));
      
        public static readonly DependencyProperty IsDocumentProperty = DependencyProperty.Register(
                       "IsDocument",
                       typeof(Boolean),
                      typeof(NewDeliveryItem), new PropertyMetadata(true));
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                      "IsClickButtonOK",
                      typeof(MessageBoxResult),
                     typeof(NewDeliveryItem), new PropertyMetadata());

        public Boolean IsEnableInvoice
        {
            get { return (Boolean)GetValue(IsEnableInvoiceProperty); }
            set { SetValue(IsEnableInvoiceProperty, value); }

        }
        public Boolean IsEnableAddInvoice
        {
            get { return (Boolean)GetValue(IsEnableAddInvoiceProperty); }
            set { SetValue(IsEnableAddInvoiceProperty, value); }

        }
        public Boolean IsEnableTTN
        {
            get { return (Boolean)GetValue(IsEnableTTNProperty); }
            set { SetValue(IsEnableTTNProperty, value); }
        }
        public Boolean IsEnableAddTTN
        {
            get { return (Boolean)GetValue(IsEnableAddTTNProperty); }
            set { SetValue(IsEnableAddTTNProperty, value); }
        }
        public Boolean IsDocument
        {
            get { return (Boolean)GetValue(IsDocumentProperty); }
            set { SetValue(IsDocumentProperty, value); }
        }
       
        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }
       
        private DeliveryLogic deliveryLogic;
        private LocaleRow localeRow;
        FileWork fileWork;

        public DeliveryLogic DeliveryLogic { get => deliveryLogic; set => deliveryLogic = value; }
        public LocaleRow Document { get => localeRow; set => localeRow = value; }

        public Int32 status;
        public Int32 Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;              
            }
        }

        public NewDeliveryItem()
        {
            InitializeComponent();
            DeliveryLogic = new DeliveryLogic();
            Document = new LocaleRow();
           
            Document.Status = Status;
            
            
            this.delivery.DataContext = Document;
            DataContext = this;
        }
        #region Load Invoice
        private async void Invoice_ButtonAddClick()
        {
            Document.InvoiceDocumentByte = this.Invoice.ByteFaile;
        }
        private void Invoice_ButtonLoopClick()
        {

        }

        private void Invoice_ButtonClearClick()
        {
            
        }
        #endregion

        #region TTN
        private async void TTN_ButtonAddClick()
        {
            Document.TTNDocumentByte = this.TTN.ByteFaile;
        }
        #endregion

        #region загрузка данных
        static async Task<FileWork> LoadInvoiceAsync(FileWork _fileWork)
        {
            return await Task.Run(() => LoadInvoice(_fileWork));
        }

        static FileWork LoadInvoice(FileWork _fileWork)
        {
            _fileWork.PDFTo();
            return _fileWork;
        }
        #endregion

        private void ToolBarButton_ButtonClick()
        {
            if (FieldVerify())
            {
                IsClickButtonOK = MessageBoxResult.OK;
                Window win = Parent as Window;
                win.Close();
            }
        }

        private void Cancel_ButtonClick()
        {
            Document = null;
            IsClickButtonOK = MessageBoxResult.Cancel;
            Window win = Parent as Window;
            win.Close();
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Status == 0)
            {
                IsDocument = true;
                IsEnableAddTTN = true;
                IsEnableAddInvoice = true;              
            }
            else
            {
                IsDocument = false;
                IsEnableAddTTN = false;
                IsEnableAddInvoice = false;
            }
        }

        private Boolean FieldVerify()
        {
            FlexMessageBox mb;

            if (String.IsNullOrEmpty(Document.NameCompany))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, NameDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                NameDeliveryCompany.EditBoxUser.TextField.Focus();
                    return false;             
            }

            if (String.IsNullOrEmpty(Document.ManagerName))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, NameManagerDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                NameManagerDeliveryCompany.EditBoxUser.TextField.Focus();           
                return false;
            }
            
            if (String.IsNullOrEmpty(Document.Phones))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(Document.PhonesDetails))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesManagerDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesManagerDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(Document.Adress))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, AdressDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                AdressDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(Document.Invoice))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, Invoice.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                Invoice.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(Document.TTN))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, TTN.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                TTN.TextBox.Focus();
                return false;
            }
            return true;
        }

        private void page_Unloaded(object sender, RoutedEventArgs e)
        {
           // Document = null;
        }
    }
}
