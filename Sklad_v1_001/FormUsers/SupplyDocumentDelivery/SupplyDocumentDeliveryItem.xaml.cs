using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.DeliveryDetails;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

namespace Sklad_v1_001.FormUsers.SupplyDocumentDelivery
{
    /// <summary>
    /// Логика взаимодействия для NewDeliveryItem.xaml
    /// </summary>
    public partial class SupplyDocumentDeliveryItem : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static readonly DependencyProperty IsEnableInvoiceProperty = DependencyProperty.Register(
                       "IsEnableInvoice",
                       typeof(Boolean),
                      typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(false));
        //IsEnableAdd
        public static readonly DependencyProperty IsEnableAddInvoiceProperty = DependencyProperty.Register(
                       "IsEnableAddInvoice",
                       typeof(Boolean),
                      typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(true));

        public static readonly DependencyProperty IsEnableTTNProperty = DependencyProperty.Register(
                      "IsEnableTTN",
                      typeof(Boolean),
                     typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(false));
        
        public static readonly DependencyProperty IsEnableAddTTNProperty = DependencyProperty.Register(
                       "IsEnableAddTTN",
                       typeof(Boolean),
                      typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(true));
      
        public static readonly DependencyProperty IsDocumentProperty = DependencyProperty.Register(
                       "IsDocument",
                       typeof(Boolean),
                      typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(true));
        
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                      "IsClickButtonOK",
                      typeof(MessageBoxResult),
                     typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(MessageBoxResult.Cancel));

        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
                   "IsEnableAdd",
                   typeof(Boolean),
                  typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEnableEditProperty = DependencyProperty.Register(
                    "IsEnableEdit",
                    typeof(Boolean),
                   typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(false));

        
        public static readonly DependencyProperty AmountUSAMaxProperty = DependencyProperty.Register(
                  "AmountUSAMax",
                  typeof(Double),
                 typeof(SupplyDocumentDeliveryItem));

        
        public static readonly DependencyProperty AmountRUSMaxProperty = DependencyProperty.Register(
                  "AmountRUSMax",
                  typeof(Double),
                 typeof(SupplyDocumentDeliveryItem));
        
        public static readonly DependencyProperty StatusDocumentProperty = DependencyProperty.Register(
                   "StatusDocument",
                   typeof(Boolean),
                  typeof(SupplyDocumentDeliveryItem), new PropertyMetadata(false));

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

        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }

        public Boolean IsEnableEdit
        {
            get { return (Boolean)GetValue(IsEnableEditProperty); }
            set { SetValue(IsEnableEditProperty, value); }
        }
        //
        public Double AmountUSAMax
        {
            get { return (Double)GetValue(AmountUSAMaxProperty); }
            set { SetValue(AmountUSAMaxProperty, value); }
        }
        public Double AmountRUSMax
        {
            get { return (Double)GetValue(AmountRUSMaxProperty); }
            set { SetValue(AmountRUSMaxProperty, value); }
        }

        public Boolean StatusDocument
        {
            get { return (Boolean)GetValue(StatusDocumentProperty); }
            set
            {
                SetValue(StatusDocumentProperty, value);                
            }
        }

        Attributes attributes;

        ConvertData convertData;

        //работаем с Delivery
        FlexMessageBox addDeliveryWindow;
        NewDeliveryItem newDeliveryItem;
        //работаем с DeliveryDetails
        FlexMessageBox addDeliveryDetailsWindow;
        NewDeliveryDetailsItem newDeliveryDetailsItem;

        private DeliveryLogic deliveryLogic;
        private LocaleRow deliveryRow;
       
        ObservableCollection<DeliveryCompany> dataDeliveryCompany;
        ObservableCollection<DeliveryCompanyDetails> dataDeliveryCompanyDetails;
        //DeliveryCompanyDetails deliveryCompanyDetails;

        public LocaleRow DeliveryRow
        {
            get
            {
                return deliveryRow;
            }

            set
            {
                deliveryRow = value;
                if (value != null)
                {
                    this.Invoice.ByteFaile = value.InvoiceDocumentByte;
                    this.Invoice.NameFile = "Invoice";
                    this.TTN.ByteFaile = value.TTNDocumentByte;
                    this.TTN.NameFile = "TTN";
                    if (value.InvoiceDocumentByte!=null)
                        this.Invoice.IsEnableLoop = true;
                    else
                        this.Invoice.IsEnableLoop = false;
                    if (value.TTNDocumentByte != null)
                        this.TTN.IsEnableLoop = true;
                    else
                        this.TTN.IsEnableLoop = false;

                    DeliveryCompany.IsEnabled = StatusDocument;
                    DeliveryCompanyDetails.IsEnabled = StatusDocument;
                    IsDocument = StatusDocument;
                    IsDocument = StatusDocument;
                    AdressDeliveryCompany.IsEnabled = StatusDocument;
                    AmounPaymentUSA.IsEnabled = StatusDocument;
                    AmounPaymentRUS.IsEnabled = StatusDocument;
                    this.Invoice.IsEnableAdd = StatusDocument;
                    this.TTN.IsEnableAdd = StatusDocument;
                    this.Invoice.TextBox.IsEnabled = StatusDocument;
                    this.TTN.TextBox.IsEnabled = StatusDocument;
                    this.OK.IsEnabled = StatusDocument;
                }
                this.delivery.DataContext = DeliveryRow;

                OnPropertyChanged("ProductLocalRow");
            }
        }      

        public SupplyDocumentDeliveryItem(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            convertData = new ConvertData();

            deliveryLogic = new DeliveryLogic();
            dataDeliveryCompany = new ObservableCollection<DeliveryCompany>();
            dataDeliveryCompanyDetails = new ObservableCollection<DeliveryCompanyDetails>();
            dataDeliveryCompany = attributes.datalistDeliveryCompany;
            dataDeliveryCompanyDetails = attributes.datalistDeliveryDetailsCompany;
            
            //загрузили имена компаний
            DeliveryCompany.ComboBoxElement.ItemsSource = dataDeliveryCompany;
            DeliveryCompany.ComboBoxElement.SelectedValue = 0;

            DeliveryCompanyDetails.ComboBoxElement.ItemsSource = dataDeliveryCompanyDetails;
            //------------------------------------------------------------------------------


            DeliveryRow = new LocaleRow();        
            
            
            this.delivery.DataContext = DeliveryRow;
            DataContext = this;
        }
        #region Load Invoice
        private async void Invoice_ButtonAddClick()
        {
            DeliveryRow.InvoiceDocumentByte = this.Invoice.ByteFaile;
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
            DeliveryRow.TTNDocumentByte = this.TTN.ByteFaile;
        }
        #endregion

        #region загрузка данных
        static async Task<FileWork> LoadInvoiceAsync(FileWork _fileWork)
        {
            return await Task.Run(() => LoadInvoice(_fileWork));
        }

        static FileWork LoadInvoice(FileWork _fileWork)
        {
            _fileWork.PDFToByte();
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
            DeliveryRow = null;
            IsClickButtonOK = MessageBoxResult.Cancel;
            Window win = Parent as Window;
            win.Close();
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            if (StatusDocument)
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

            if (String.IsNullOrEmpty(DeliveryRow.NameCompany))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, DeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                DeliveryCompany.ComboBoxElement.Focus();
                    return false;             
            }

            if (String.IsNullOrEmpty(DeliveryRow.ManagerName))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, DeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                DeliveryCompanyDetails.ComboBoxElement.Focus();           
                return false;
            }
            
            if (String.IsNullOrEmpty(DeliveryRow.PhonesCompany))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryRow.PhonesManager))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesManagerDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesManagerDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryRow.AdressCompany))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, AdressDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                AdressDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryRow.Invoice))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, Invoice.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                Invoice.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryRow.TTN))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, TTN.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                TTN.TextBox.Focus();
                return false;
            }
            if (DeliveryRow.AmountUSA<0)
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, AmounPaymentUSA.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                AmounPaymentUSA.TextBox.Focus();
                return false;
            }

            if (DeliveryRow.AmountRUS < 0)
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, AmounPaymentRUS.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                AmounPaymentRUS.TextBox.Focus();
                return false;
            }
            return true;
        }

        private void page_Unloaded(object sender, RoutedEventArgs e)
        {
           // Document = null;
        }     
      
        #region DeliveryCompany
       
        private void DeliveryCompany_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (DeliveryCompany.Value > 0)
            {
                IsEnableAdd = true;
            }
            else
            {
                IsEnableAdd = false;
            }
        }

        private void DeliveryCompany_DropDownClosed()
        {
            if (DeliveryCompany.Value != 0 && dataDeliveryCompany.Count != 0)
            {
                //DeliveryRow
                DeliveryRow.NameCompany = dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())) != null ?
                   dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())).Description : Properties.Resources.UndefindField;

                DeliveryRow.DeliveryID = dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())) != null ?
                    dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())).ID : 0;

                DeliveryRow.NameCompany = dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())) != null ?
                    dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())).Description : Properties.Resources.UndefindField;

                DeliveryRow.AdressCompany= dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())) != null ?
                    dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())).AdressCompany : Properties.Resources.UndefindField;
               
                DeliveryRow.PhonesCompany = dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())) != null ?
                   dataDeliveryCompany.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString())).Phones : Properties.Resources.UndefindField;
            }
            ObservableCollection<DeliveryCompanyDetails> dataCategoryDetailsTemp = new ObservableCollection<DeliveryCompanyDetails>();
            foreach (DeliveryCompanyDetails companyDetails in dataDeliveryCompanyDetails)
            {
                if (companyDetails.DeliveryID == DeliveryRow.DeliveryID)
                    dataCategoryDetailsTemp.Add(companyDetails);
            }

            DeliveryCompanyDetails.ComboBoxElement.ItemsSource = dataCategoryDetailsTemp;
            DeliveryCompanyDetails.ComboBoxElement.SelectedValue = 0;
        }

        private void DeliveryCompany_ButtonAdd()
        {
            newDeliveryItem = new NewDeliveryItem(attributes);
            addDeliveryWindow = new FlexMessageBox();
            addDeliveryWindow.Content = newDeliveryItem;
            addDeliveryWindow.Show(Properties.Resources.DeliveriesCompany);
            if (newDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newDeliveryItem.DeliveryCompanyRow != null)
                {
                    dataDeliveryCompany = attributes.datalistDeliveryCompany;
                    
                    DeliveryRow.DeliveryID = newDeliveryItem.DeliveryCompanyRow.ID;
                    DeliveryRow.NameCompany = newDeliveryItem.DeliveryCompanyRow.Description;
                    DeliveryRow.AdressCompany = newDeliveryItem.DeliveryCompanyRow.AdressCompany;
                    DeliveryRow.PhonesCompany = newDeliveryItem.DeliveryCompanyRow.Phones;
                    DeliveryCompany.ComboBoxElement.SelectedValue = DeliveryRow.DeliveryID;
                }
            }
        }

        private void DeliveryCompany_ButtonEdit()
        {
            newDeliveryItem = new NewDeliveryItem(attributes);
            addDeliveryWindow = new FlexMessageBox();
            newDeliveryItem.DeliveryCompanyRow = dataDeliveryCompany.First(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompany.Value.ToString()));
            addDeliveryWindow.Content = newDeliveryItem;
            addDeliveryWindow.Show(Properties.Resources.DeliveriesCompany);
            if (newDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newDeliveryItem.DeliveryCompanyRow != null)
                {
                    dataDeliveryCompany = attributes.datalistDeliveryCompany;

                    DeliveryRow.DeliveryID = newDeliveryItem.DeliveryCompanyRow.ID;
                    DeliveryRow.NameCompany = newDeliveryItem.DeliveryCompanyRow.Description;
                    DeliveryRow.AdressCompany = newDeliveryItem.DeliveryCompanyRow.AdressCompany;
                    DeliveryRow.PhonesCompany = newDeliveryItem.DeliveryCompanyRow.Phones;
                    DeliveryCompany.ComboBoxElement.SelectedValue = DeliveryRow.DeliveryID;
                }
            }
        }
        #endregion

        #region DeliveryCompanyDetails
        private void DeliveryCompanyDetails_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (DeliveryCompanyDetails.Value > 0)
            {
                IsEnableEdit = true;
            }
            else
            {
                IsEnableEdit = false;
            }
        }

        private void DeliveryCompanyDetails_DropDownClosed()
        {
            if (DeliveryCompanyDetails.Value != 0 && dataDeliveryCompanyDetails.Count != 0)
            {
                DeliveryRow.ManagerName = dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())) != null ?
                 dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())).Description : Properties.Resources.UndefindField;

                DeliveryRow.PhonesManager = dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())) != null ?
                  dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())).Phones : Properties.Resources.UndefindField;

                DeliveryRow.DeliveryID = dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())) != null ?
                    dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())).DeliveryID : 0;

                DeliveryRow.ID = dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())) != null ?
                   dataDeliveryCompanyDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString())).ID : 0;

            }
        }

        private void DeliveryCompanyDetails_ButtonAdd()
        {
            if (DeliveryCompany.Value > 0)
            {

                newDeliveryDetailsItem = new NewDeliveryDetailsItem(attributes);
                addDeliveryDetailsWindow = new FlexMessageBox();


                GlobalList.DeliveryCompanyDetails deliveryCompanyDetails = new GlobalList.DeliveryCompanyDetails();

                deliveryCompanyDetails.DeliveryIDString = dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value) != null ?
                   dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value).Description : "";

                deliveryCompanyDetails.DeliveryID = dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value) != null ?
                  dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value).ID : 0;

                newDeliveryDetailsItem.DeliveryCompanyDetailsRow = deliveryCompanyDetails;

                addDeliveryDetailsWindow.Content = newDeliveryDetailsItem;
                addDeliveryDetailsWindow.Show(Properties.Resources.DeliveriesCompany);
                if (newDeliveryDetailsItem.IsClickButtonOK == MessageBoxResult.OK)
                {
                    if (newDeliveryDetailsItem.DeliveryCompanyDetailsRow != null)
                    {
                        dataDeliveryCompany = attributes.datalistDeliveryCompany;
                        dataDeliveryCompanyDetails = attributes.datalistDeliveryDetailsCompany;

                        DeliveryRow.ManagerName = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.Description;
                        DeliveryRow.DeliveryID = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.DeliveryID;
                        DeliveryRow.PhonesManager = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.Phones;
                        DeliveryRow.ID = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.ID;

                        DeliveryCompany.ComboBoxElement.SelectedValue = dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryRow.DeliveryID).ID; 
                        DeliveryCompanyDetails.ComboBoxElement.SelectedValue = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.ID;
                    }
                }
            }
            else
            {
                FlexMessageBox mb;
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorCategory0, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, DeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                DeliveryCompany.ComboBoxElement.Focus();
            }
        }

        private void DeliveryCompanyDetails_ButtonEdit()
        {
            if (DeliveryCompany.Value > 0)
            {

                newDeliveryDetailsItem = new NewDeliveryDetailsItem(attributes);
                addDeliveryDetailsWindow = new FlexMessageBox();

                GlobalList.DeliveryCompanyDetails deliveryCompanyDetails = new GlobalList.DeliveryCompanyDetails();

                deliveryCompanyDetails = dataDeliveryCompanyDetails.First(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryCompanyDetails.Value.ToString()));

                deliveryCompanyDetails.DeliveryIDString = dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value) != null ?
                   dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value).Description : "";

                deliveryCompanyDetails.DeliveryID = dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value) != null ?
                  dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryCompany.Value).ID : 0;

                newDeliveryDetailsItem.DeliveryCompanyDetailsRow = deliveryCompanyDetails;

                addDeliveryDetailsWindow.Content = newDeliveryDetailsItem;
                addDeliveryDetailsWindow.Show(Properties.Resources.DeliveriesCompany);
                if (newDeliveryDetailsItem.IsClickButtonOK == MessageBoxResult.OK)
                {
                    if (newDeliveryDetailsItem.DeliveryCompanyDetailsRow != null)
                    {
                        dataDeliveryCompany = attributes.datalistDeliveryCompany;
                        dataDeliveryCompanyDetails = attributes.datalistDeliveryDetailsCompany;

                        DeliveryRow.ManagerName = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.Description;
                        DeliveryRow.DeliveryID = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.DeliveryID;
                        DeliveryRow.PhonesManager = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.Phones;
                        DeliveryRow.ID = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.ID;

                        DeliveryCompany.ComboBoxElement.SelectedValue = dataDeliveryCompany.FirstOrDefault(x => x.ID == DeliveryRow.DeliveryID).ID;
                        DeliveryCompanyDetails.ComboBoxElement.SelectedValue = newDeliveryDetailsItem.DeliveryCompanyDetailsRow.ID;
                    }
                }
            }
            else
            {
                FlexMessageBox mb;
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorCategory0, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, DeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                DeliveryCompany.ComboBoxElement.Focus();
            }
        }
        #endregion


    }
}
