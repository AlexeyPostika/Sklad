using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.DeliveryDetails;
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
                     typeof(NewDeliveryItem), new PropertyMetadata(MessageBoxResult.Cancel));

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

        ConvertData convertData; 

        private DeliveryLogic deliveryLogic;
        private LocaleRow document;
        FileWork fileWork;

        public DeliveryLogic DeliveryLogic { get => deliveryLogic; set => deliveryLogic = value; }
        public DeliveryLogic deliveryLogicCom;
        public DeliveryDetailsLogic deliveryDetailsLogic;
        ManagerDeliveryList managerDeliveryList;
        ManagerDeliveryList managerDeliveryDetailsList;

        public LocaleRow Document { get => document; set => document = value; }

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
            convertData = new ConvertData();
          
            DeliveryLogic = new DeliveryLogic();
            deliveryLogicCom = new DeliveryLogic();
            DataTable deliveryList = deliveryLogicCom.FillGrid();           
            managerDeliveryList = new ManagerDeliveryList();
            //загрузили имена компаний
            foreach(DataRow row in deliveryList.Rows)
            {
                managerDeliveryList.innerList.Add(deliveryLogicCom.ConvertDelivery(row, new ManagerDelivery()));
            }
            DeliveryList.ComboBoxElement.ItemsSource = managerDeliveryList.innerList;
            DeliveryList.ComboBoxElement.SelectedValue = 0;
            //------------------------------------------------------------------------------

            //Загрузил менеджеров компании
            deliveryDetailsLogic = new DeliveryDetailsLogic();      

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
            
            if (String.IsNullOrEmpty(Document.PhonesCompany))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(Document.PhonesManager))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesManagerDeliveryCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesManagerDeliveryCompany.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(Document.AdressCompany))
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

        private void DeliveryList_DropDownClosed()
        {
            if (DeliveryList.Value != 0 && managerDeliveryList.innerList.Count != 0)
            {
                NameDeliveryCompany.Text = managerDeliveryList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryList.Value.ToString())) != null ?
                    managerDeliveryList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryList.Value.ToString())).Description :
                    Properties.Resources.UndefindField;

                LocalFilter localFilter = new LocalFilter();
                localFilter.DocumentID = managerDeliveryList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryList.Value.ToString())) != null ?
                    managerDeliveryList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryList.Value.ToString())).ID.ToString():"";
                DataTable deliveryDetailsList = deliveryDetailsLogic.FillGrid(localFilter);
                managerDeliveryDetailsList = new ManagerDeliveryList();
                //загрузили имена компаний
                foreach (DataRow row in deliveryDetailsList.Rows)
                {
                    managerDeliveryDetailsList.innerList.Add(deliveryDetailsLogic.ConvertDelivery(row, new ManagerDelivery()));
                }
                DeliveryDetailsList.ComboBoxElement.ItemsSource = managerDeliveryDetailsList.innerList;
                DeliveryDetailsList.ComboBoxElement.SelectedValue = 0;
                //------------------------------------------------------------------------------
            }

            DeliveryList.Visibility = Visibility.Collapsed;
            NameDeliveryCompany.Visibility = Visibility.Visible;
        }

        private void NameDeliveryCompany_ButtonClearClick()
        {
            DeliveryList.Visibility = Visibility.Visible;
            DeliveryList.ComboBoxElement.IsDropDownOpen = true;
            NameDeliveryCompany.Visibility = Visibility.Collapsed;
        }

        private void DeliveryDetailsList_DropDownClosed()
        {
            if (DeliveryDetailsList.Value != 0 && managerDeliveryDetailsList.innerList.Count != 0)
            {
                NameManagerDeliveryCompany.Text = managerDeliveryDetailsList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryDetailsList.Value.ToString())) != null ?
                     managerDeliveryDetailsList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(DeliveryDetailsList.Value.ToString())).Description :
                    Properties.Resources.UndefindField;
            }

            DeliveryDetailsList.Visibility = Visibility.Collapsed;
            NameManagerDeliveryCompany.Visibility = Visibility.Visible;
        }

        private void NameManagerDeliveryCompany_ButtonClearClick()
        {
            DeliveryDetailsList.Visibility = Visibility.Visible;
            DeliveryDetailsList.ComboBoxElement.IsDropDownOpen = true;
            NameManagerDeliveryCompany.Visibility = Visibility.Collapsed;
        }
    }
}
