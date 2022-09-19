using Newtonsoft.Json;
using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.Product;
using Sklad_v1_001.FormUsers.RegisterDocumentDetails;
using Sklad_v1_001.FormUsers.RegisterDocumentPayment;
using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
using Sklad_v1_001.FormUsers.SupplyDocumentDetails;
using Sklad_v1_001.FormUsers.SupplyDocumentPayment;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.HelperGlobal.StoreAPI;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocument;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentDelivery;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentDetails;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentPayment;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;
using Sklad_v1_001.FormUsers.RegisterDocumetnDelivery;
using Sklad_v1_001.FormUsers.Payment;

namespace Sklad_v1_001.FormUsers.RegisterDocument
{

    public class DataContextSpy : Freezable
    {
        public DataContextSpy()
        {
            // This binding allows the spy to inherit a DataContext.
            BindingOperations.SetBinding(this, DataContextProperty, new Binding());
        }

        public object DataContext
        {
            get { return GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        // Borrow the DataContext dependency property from FrameworkElement.
        public static readonly DependencyProperty DataContextProperty = FrameworkElement
            .DataContextProperty.AddOwner(typeof(DataContextSpy));

        protected override Freezable CreateInstanceCore()
        {
            // We are required to override this abstract method.
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Логика взаимодействия для NewSupplyDocument.xaml
    /// </summary>
    public partial class NewRegisterDocumentGrid : Page
    {
        //IsPaymentAddButton
        //AmountMax
        public static readonly DependencyProperty IsPaymentAddButtonProperty = DependencyProperty.Register(
                   "IsPaymentAddButton",
                   typeof(Boolean),
                  typeof(NewRegisterDocumentGrid), new PropertyMetadata(true));

        //IsApplyDocument
        public static readonly DependencyProperty IsApplyDocumentProperty = DependencyProperty.Register(
                  "IsApplyDocument",
                  typeof(Boolean),
                 typeof(NewRegisterDocumentGrid), new PropertyMetadata(false));
        public Boolean IsPaymentAddButton
        {
            get { return (Boolean)GetValue(IsPaymentAddButtonProperty); }
            set { SetValue(IsPaymentAddButtonProperty, value); }
        }
        public Boolean IsApplyDocument
        {
            get { return (Boolean)GetValue(IsApplyDocumentProperty); }
            set { SetValue(IsApplyDocumentProperty, value); }
        }

        public struct ComplexKey
        {
            int id;
            int type;

            public int Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value;
                }
            }

            public int Type
            {
                get
                {
                    return type;
                }

                set
                {
                    type = value;
                }
            }
        }


        Attributes attributes;
        //работаем с продуктами
        FlexWindows addProductWindow;
        NewAddProductItem newAddProductItem;
        ObservableCollection<Product.LocalRow> dataListProduct;
        //******************************

        //работаем с категориями      
        ObservableCollection<Category.LocalRow> dataListCategory;
        //ObservableCollection<CategoryDetails.LocalRow> dataListDeliveryDetails;
        //******************************

        //работаем с доставкой
        FlexWindows addDeliveryWindow;
        SupplyDocumentDeliveryItem supplyDocumentDeliveryItem;
        ObservableCollection<Delivery.LocaleRow> dataListDelivery;
        ObservableCollection<DeliveryDetails.LocaleRow> dataListDeliveryDetails;

        //******************************

        //работаем с оплатами
        FlexWindows addSuppluPaymentWindow;
        PaymentItem newRegisterDocumentPaymentItem;
        Payment.PaymentLogic paymentLogic;
        //******************************

        //остновной документ      
        LocalRow document;
        RegisterDocumentLogic registerDocumentLogic;
        Request request;

        //Продукт
        Product.LocalRow localeRowProduct;
        //ObservableCollection<Product.LocaleRow> detailsProduct;

        //доставка
        RegisterDocumentDeliveryLogic registerDocumentDeliveryLogic;      
        ObservableCollection<RegisterDocumetnDelivery.LocaleRow> registerDocumentDelivery;

        //SupplyDocumentDetails
        RegisterDocumentDetailsLogic registerDocumentDetailsLogic;      
        ObservableCollection<RegisterDocumentDetails.LocaleRow> registerDocumentDetails;

        //оплата
        RegisterDocumentPaymentLogic registerDocumentPaymentLogic;
        RegisterDocumentPayment.LocaleRow registerDocumentPaymentLocaleRow;
        ObservableCollection<RegisterDocumentPayment.LocaleRow> registerDocumentPayment;

        //коллекция для удаления строк
        ObservableCollection<ComplexKey> datalistDeleted;

        //Суммы
        RowSummary summary;

        //схема структуры SupplyDocument
        ShemaStorаge shemaStorаge;

        private Int32 status;
        Boolean newDocument;

        public Boolean NewDocument
        {
            get
            {
                return newDocument;
            }

            set
            {
                newDocument = value;
            }
        }

        public LocalRow Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
                if (document == null || document.ID == 0)
                {
                    status = 0;
                }
                else
                    status = 1;

                this.DataContext = Document;
                NewDocument = Document.ID == 0;
                if (NewDocument)
                {
                    Document.LastModificatedUserID = attributes.numeric.userEdit.AddUserID;
                }

                switch (Document.Status)
                {
                    case 0:
                        //ToolBarProduct.ButtonNewProduct.IsEnabled = true;
                        //ToolBarProduct.ButtonDelete.IsEnabled = true;
                        //ToolBarDelivery.ButtonNewProduct.IsEnabled = true;
                        //ToolBarDelivery.ButtonDelete.IsEnabled = true;
                        //SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
                        //SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = true;
                        //SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = true;
                        //SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        //SupplyDocumentDetailsToolBar.ButtonRequestSend.IsEnabled = true;
                        //UserIDDocument.IsEnabled = true;
                        break;
                    case 1:
                    case 6:
                        //ToolBarProduct.ButtonNewProduct.IsEnabled = false;
                        //ToolBarProduct.ButtonDelete.IsEnabled = false;
                        //ToolBarDelivery.ButtonNewProduct.IsEnabled = false;
                        //ToolBarDelivery.ButtonDelete.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
                        //SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        //SupplyDocumentDetailsToolBar.ButtonRequestSend.IsEnabled = false;
                        //UserIDDocument.IsEnabled = false;                   
                        //SupplyDocumentDetailsToolBar.ButtonPrintLabels.IsEnabled = false;
                        break;                  
                    case 2:
                        //SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Collapsed;
                        //DocumentToolbar.ButtonPrintLabels.IsEnabled = true;
                        break;
                    case 3:
                        //ToolBarProduct.ButtonNewProduct.IsEnabled = false;
                        //ToolBarProduct.ButtonDelete.IsEnabled = false;
                        //ToolBarDelivery.ButtonNewProduct.IsEnabled = false;
                        //ToolBarDelivery.ButtonDelete.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;                 
                        //SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        //SupplyDocumentDetailsToolBar.ButtonRequestSend.IsEnabled = false;
                        //IsApplyDocument = true;
                        //UserIDDocument.IsEnabled = false;
                        break;
                }
                if (document.ID>0)
                    Refresh();
            }
        }

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


        public NewRegisterDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            Document = new LocalRow();
            summary = new RowSummary();

            shemaStorаge = new ShemaStorаge();

            request = new Request(attributes);
            registerDocumentLogic = new RegisterDocumentLogic(attributes);
            registerDocumentDetailsLogic = new RegisterDocumentDetailsLogic(attributes);
            registerDocumentDeliveryLogic = new RegisterDocumentDeliveryLogic(attributes);
            registerDocumentPaymentLogic = new RegisterDocumentPaymentLogic(attributes);

            registerDocumentDetails = new ObservableCollection<RegisterDocumentDetails.LocaleRow>();
            registerDocumentDelivery = new ObservableCollection<RegisterDocumetnDelivery.LocaleRow>();
            registerDocumentPayment = new ObservableCollection<RegisterDocumentPayment.LocaleRow>();

            datalistDeleted = new ObservableCollection<ComplexKey>();

            dataListCategory = new ObservableCollection<Category.LocalRow>();
            dataListDelivery = new ObservableCollection<Delivery.LocaleRow>();
            dataListDeliveryDetails = new ObservableCollection<DeliveryDetails.LocaleRow>();          

            SupplyTypeList supplyTypeList = new SupplyTypeList();
            this.StatusDocument.ComboBoxElement.ItemsSource = supplyTypeList.innerList;

            this.DataProduct.ItemsSource = registerDocumentDetails;
            this.DataDelivery.ItemsSource = registerDocumentDelivery;
            this.DataPayment.ItemsSource = registerDocumentPayment;
           
            this.ToolBarPayment.ButtonNewProduct.Text = Properties.Resources.ADD;

            this.DataContext = Document;
            this.DocumentSummary.DataContext = summary;

            UserIDDocument.ComboBoxElement.ItemsSource = attributes.datalistUsers;
           // Refresh();
        }

        private void Refresh()
        {
            registerDocumentDetails.Clear();
            registerDocumentDelivery.Clear();
            registerDocumentPayment.Clear();

            DataTable dataTableSupplyDocumentDetails = registerDocumentDetailsLogic.FillGridDocument(Document.ID);
            foreach(DataRow row in dataTableSupplyDocumentDetails.Rows)
            {
                registerDocumentDetails.Add(registerDocumentDetailsLogic.Convert(row, new RegisterDocumentDetails.LocaleRow()));
            }
           
            DataTable dataTableSupplyDocumentDelivery = registerDocumentDeliveryLogic.FillGrid(Document.ID);
            foreach (DataRow row in dataTableSupplyDocumentDelivery.Rows)
            {
                registerDocumentDelivery.Add(registerDocumentDeliveryLogic.Convert(row, new RegisterDocumetnDelivery.LocaleRow()));
            }
           
            DataTable dataTableSupplyDocumentPayment = registerDocumentPaymentLogic.FillGrid(Document.ID);
            foreach (DataRow row in dataTableSupplyDocumentPayment.Rows)
            {
                registerDocumentPayment.Add(registerDocumentPaymentLogic.Convert(row, new RegisterDocumentPayment.LocaleRow()));
            }

            CalculateSummary();
        }
       
        #region Продукт

        private void EditProduct(RegisterDocumentDetails.LocaleRow currentrow = null)
        {
            newAddProductItem = new NewAddProductItem(attributes);
            addProductWindow = new FlexWindows(Properties.Resources.Products);
            newAddProductItem.StatusDocument = Document.Status == 0;
            newAddProductItem.ProductLocalRow = currentrow != null ? registerDocumentDetailsLogic.ConvertSupplyDocumentDetailsToProduct(new Product.LocalRow(), currentrow) : new Product.LocalRow();
            addProductWindow.Content = newAddProductItem;
            addProductWindow.ShowDialog();
            if (newAddProductItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newAddProductItem.ProductLocalRow != null)
                {
                    RegisterDocumentDetails.LocaleRow locale = new RegisterDocumentDetails.LocaleRow();
                    localeRowProduct = newAddProductItem.ProductLocalRow;
                    if (newAddProductItem.ProductLocalRow!=null)
                    {                            
                        locale.TempID = registerDocumentDetails.Count() + 1;
                        locale.LineDocument = registerDocumentDetails.Count() + 1;
                        registerDocumentDetails.Add(registerDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, locale));
                    }
                    else
                    {
                        int tempID = registerDocumentDetails.FirstOrDefault(x => x.LineDocument == localeRowProduct.ID) != null ? registerDocumentDetails.FirstOrDefault(x => x.LineDocument == localeRowProduct.ID).LineDocument : 0;
                        
                        locale = registerDocumentDetails.FirstOrDefault(x => x.LineDocument == localeRowProduct.ID);
                        registerDocumentDetails.Remove(locale);
                        
                        if (locale == null)
                            locale = new RegisterDocumentDetails.LocaleRow();

                        registerDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, locale);                      
                        locale.TempID = tempID;
                        locale.LineDocument = tempID;
                        registerDocumentDetails.Add(locale);
                    }
                }
            }
            CalculateSummary();
        }

       

        private void DataProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SupplyDocumentDetails.LocaleRow currentrow = this.DataProduct.SelectedItem as SupplyDocumentDetails.LocaleRow;
            if (currentrow != null)
            {
                //MainWindow.AppWindow.ButtonNewAddProductF(supplyDocumentDetailsLogic.ConvertSupplyDocumentDetailsToProduct(new Product.LocaleRow(), currentrow));
                //EditProduct(currentrow);
            }
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Поставщик

        private void EditDelivery(SupplyDocumentDelivery.LocaleRow currentrow = null)
        {
            RegisterDocumetnDelivery.LocaleRow localeRowDelivery = new RegisterDocumetnDelivery.LocaleRow();
            //supplyDocumentDeliveryItem = new SupplyDocumentDeliveryItem(attributes);
            //addDeliveryWindow = new FlexWindows(Properties.Resources.Deliveries);

            //supplyDocumentDeliveryItem.StatusDocument = Document.Status == 0;
            //supplyDocumentDeliveryItem.DeliveryRow = currentrow != null ? currentrow : new SupplyDocumentDelivery.LocaleRow();

            //addDeliveryWindow.Content = supplyDocumentDeliveryItem;
            //addDeliveryWindow.ShowDialog();
            ////тут проблема посмотреть, не правильно добавляется ID
            //if (supplyDocumentDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            //{
            //    if (supplyDocumentDeliveryItem.DeliveryRow != null && !String.IsNullOrEmpty(supplyDocumentDeliveryItem.DeliveryRow.NameCompany))
            //    {
            //        localeRowDelivery = supplyDocumentDeliveryItem.DeliveryRow;
            //        SupplyDocumentDelivery.LocaleRow locale = new SupplyDocumentDelivery.LocaleRow();
            //        if (localeRowDelivery.LineDocument == 0)
            //        {
            //            localeRowDelivery.TempID = registerDocumentDelivery.Count() + 1;
            //            localeRowDelivery.LineDocument = registerDocumentDelivery.Count() + 1;
            //            registerDocumentDelivery.Add(localeRowDelivery);
            //        }
            //        else
            //        {
            //            int tempID = registerDocumentDelivery.FirstOrDefault(x => x.LineDocument == localeRowDelivery.LineDocument) != null ? registerDocumentDelivery.FirstOrDefault(x => x.LineDocument == localeRowDelivery.LineDocument).LineDocument : 0;
            //            locale = registerDocumentDelivery.FirstOrDefault(x => x.LineDocument == localeRowDelivery.LineDocument);
            //            registerDocumentDelivery.Remove(locale);
            //            locale = localeRowDelivery;
            //            locale.TempID = tempID;
            //            locale.LineDocument = tempID;
            //            registerDocumentDelivery.Add(locale);
            //        }
            //    }
            //}
            CalculateSummary();         
        }
       
        private void DataDelivery_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SupplyDocumentDelivery.LocaleRow currentrow = this.DataDelivery.SelectedItem as SupplyDocumentDelivery.LocaleRow;
            if (currentrow != null)
            {               
                EditDelivery(currentrow);
            }
        }

        #endregion

        #region оплата
        private void EditPayment(Payment.LocaleRow currentrow = null)
        {
            registerDocumentPaymentLocaleRow = new RegisterDocumentPayment.LocaleRow();
            newRegisterDocumentPaymentItem = new PaymentItem();
            addSuppluPaymentWindow = new FlexWindows(Properties.Resources.Payment1);
            newRegisterDocumentPaymentItem.AmountMax = (Double)summary.SummaryPaymentRemains == 0 ? (Double)summary.SummaryPaymentBalans : (Double)summary.SummaryPaymentRemains;
            newRegisterDocumentPaymentItem.StatusDocument = Document.Status == 0;
            newRegisterDocumentPaymentItem.PaymentLocalRow = currentrow != null ? currentrow : new Payment.LocaleRow();          
            addSuppluPaymentWindow.Content = newRegisterDocumentPaymentItem;
            addSuppluPaymentWindow.ShowDialog();
            if (newRegisterDocumentPaymentItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newRegisterDocumentPaymentItem.PaymentLocalRow != null)
                {
                    Int32 countTemp = registerDocumentPayment.Count() + 1;
                    var temp = registerDocumentPayment.FirstOrDefault(x => x.LineDocument == newRegisterDocumentPaymentItem.PaymentLocalRow.LineDocument || x.TempID == newRegisterDocumentPaymentItem.PaymentLocalRow.TempID);
                    if (temp == null)
                    {
                        registerDocumentPaymentLocaleRow.Amount = newRegisterDocumentPaymentItem.PaymentLocalRow.Amount;
                        registerDocumentPaymentLocaleRow.CreatedDate = newRegisterDocumentPaymentItem.PaymentLocalRow.CreatedDate;
                        registerDocumentPaymentLocaleRow.CreatedUserID = newRegisterDocumentPaymentItem.PaymentLocalRow.CreatedUserID;
                        registerDocumentPaymentLocaleRow.Description = newRegisterDocumentPaymentItem.PaymentLocalRow.Description;
                        registerDocumentPaymentLocaleRow.DocumentID = newRegisterDocumentPaymentItem.PaymentLocalRow.DocumentID;
                        registerDocumentPaymentLocaleRow.ID = newRegisterDocumentPaymentItem.PaymentLocalRow.ID;
                        registerDocumentPaymentLocaleRow.ImageSourceRRN = newRegisterDocumentPaymentItem.PaymentLocalRow.ImageSourceRRN;
                        registerDocumentPaymentLocaleRow.LineDocument = newRegisterDocumentPaymentItem.PaymentLocalRow.LineDocument == 0 ? countTemp : newRegisterDocumentPaymentItem.PaymentLocalRow.LineDocument;
                        registerDocumentPaymentLocaleRow.OpertionType = newRegisterDocumentPaymentItem.PaymentLocalRow.OpertionType;
                        registerDocumentPaymentLocaleRow.OpertionTypeString = newRegisterDocumentPaymentItem.PaymentLocalRow.OpertionTypeString;
                        registerDocumentPaymentLocaleRow.RRN = newRegisterDocumentPaymentItem.PaymentLocalRow.RRN;
                        registerDocumentPaymentLocaleRow.RRNDocumentByte = newRegisterDocumentPaymentItem.PaymentLocalRow.RRNDocumentByte;
                        registerDocumentPaymentLocaleRow.Status = newRegisterDocumentPaymentItem.PaymentLocalRow.Status;
                        registerDocumentPaymentLocaleRow.StatusString = newRegisterDocumentPaymentItem.PaymentLocalRow.StatusString;
                        registerDocumentPaymentLocaleRow.TempID = newRegisterDocumentPaymentItem.PaymentLocalRow.TempID == 0 ? countTemp : newRegisterDocumentPaymentItem.PaymentLocalRow.TempID;
                        registerDocumentPayment.Add(registerDocumentPaymentLocaleRow);
                    }
                    else
                    {
                        temp.Amount = newRegisterDocumentPaymentItem.PaymentLocalRow.Amount;
                        temp.CreatedDate = newRegisterDocumentPaymentItem.PaymentLocalRow.CreatedDate;
                        temp.CreatedUserID = newRegisterDocumentPaymentItem.PaymentLocalRow.CreatedUserID;
                        temp.Description = newRegisterDocumentPaymentItem.PaymentLocalRow.Description;
                        temp.DocumentID = newRegisterDocumentPaymentItem.PaymentLocalRow.DocumentID;
                        temp.ID = newRegisterDocumentPaymentItem.PaymentLocalRow.ID;
                        temp.ImageSourceRRN = newRegisterDocumentPaymentItem.PaymentLocalRow.ImageSourceRRN;
                        temp.LineDocument = newRegisterDocumentPaymentItem.PaymentLocalRow.LineDocument;
                        temp.OpertionType = newRegisterDocumentPaymentItem.PaymentLocalRow.OpertionType;
                        temp.OpertionTypeString = newRegisterDocumentPaymentItem.PaymentLocalRow.OpertionTypeString;
                        temp.RRN = newRegisterDocumentPaymentItem.PaymentLocalRow.RRN;
                        temp.RRNDocumentByte = newRegisterDocumentPaymentItem.PaymentLocalRow.RRNDocumentByte;
                        temp.Status = newRegisterDocumentPaymentItem.PaymentLocalRow.Status;
                        temp.StatusString = newRegisterDocumentPaymentItem.PaymentLocalRow.StatusString;
                        temp.TempID = newRegisterDocumentPaymentItem.PaymentLocalRow.TempID;
                    }

                    //RegisterDocumentPayment.LocaleRow paymentLocalRow = newSupplyDocumentPaymentItem.PaymentLocalRow;
                    //RegisterDocumentPayment.LocaleRow locale = new SupplyDocumentPayment.LocaleRow();
                    //if (paymentLocalRow.LineDocument == 0)
                    //{
                    //    paymentLocalRow.TempID = registerDocumentPayment.Count() + 1;
                    //    paymentLocalRow.LineDocument = registerDocumentPayment.Count() + 1;
                    //    registerDocumentPayment.Add(paymentLocalRow);
                    //}
                    //else
                    //{
                    //    int tempID = registerDocumentPayment.FirstOrDefault(x => x.LineDocument == paymentLocalRow.ID) != null ? registerDocumentPayment.FirstOrDefault(x => x.LineDocument == paymentLocalRow.ID).LineDocument : 0;

                    //    locale = registerDocumentPayment.FirstOrDefault(x => x.LineDocument == paymentLocalRow.LineDocument);
                    //    registerDocumentPayment.Remove(locale);                      
                    //    locale = paymentLocalRow;
                    //    locale.LineDocument = tempID;
                    //    registerDocumentPayment.Add(locale);
                    //}
                }
            }
            CalculateSummary();
        }

        private void ToolBarPayment_ButtonNewProductClick()
        {
            EditPayment();
            CalculateSummary();
        }

        private void DataPayment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<RegisterDocumentPayment.LocaleRow> currentrow = this.DataPayment.SelectedItems.Cast<RegisterDocumentPayment.LocaleRow>().ToList();
            if (currentrow != null && currentrow.Count() > 0)
            {
                paymentLogic = new PaymentLogic(attributes);
                EditPayment(paymentLogic.ConvertRegisterDocumentToPayment(currentrow.First(), new Payment.LocaleRow()));
            }
        }

        private void ToolBarPayment_ButtonDeleteClick()
        {
            if (DataPayment.SelectedItems.Count > 0)
            {
                FlexMessageBox mb = new FlexMessageBox();
                MessageBoxResult dialogresult = mb.Show(Properties.Resources.QuestionDelete, GenerateTitle(TitleType.Question, Properties.Resources.Deletion), MessageBoxButton.OKCancel, MessageBoxImage.Question, 3);
                if (dialogresult == MessageBoxResult.OK)
                {
                    //var currentRowViews = DataPayment.SelectedItems;
                    //foreach (SupplyDocumentPayment.LocaleRow currentrow in currentRowViews)
                    //{
                    //    SupplyDocumentPayment.LocaleRow deletePayment = registerDocumentPayment.LastOrDefault(x => x.TempID == currentrow.TempID);                      
                    //    ComplexKey complexKey = new ComplexKey();
                    //    complexKey.Id = deletePayment.TempID;
                    //    complexKey.Type = 3;
                    //    datalistDeleted.Add(complexKey);
                    //}
                    //foreach (ComplexKey complex in datalistDeleted)
                    //{
                    //    RegisterDocumentPayment.LocaleRow deletePayment = registerDocumentPayment.LastOrDefault(x => x.TempID == complex.Id);
                    //    RegisterDocumentPayment.Remove(deletePayment);
                    //}
                }
            }
            CalculateSummary();
        }
        #endregion

        #region Save
        private Boolean FieldVerify(ObservableCollection<SupplyDocumentDetails.LocaleRow> currentrowRelatedProduct)//, ObservableCollection<Del.LocaleRow> currentrowDelivery
        {
            //FlexMessageBox mb2 = new FlexMessageBox();
            //FlexMessageBox mb3 = new FlexMessageBox();
            //List<BitmapImage> ButtonImages = new List<BitmapImage>();
            //ButtonImages.Add(ImageHelper.GenerateImage("IconAdd.png"));
            //ButtonImages.Add(ImageHelper.GenerateImage("IconContinueWork.png"));
            //List<string> ButtonText = new List<string>();
            //ButtonText.Add(Properties.Resources.AddSmall);
            //ButtonText.Add(Properties.Resources.MessageIgnore);

            //Stones();

            //if (!String.IsNullOrEmpty(Document.Email))
            //{
            //    if (!Regex.IsMatch(Document.Email, @"\A(?:[\w0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[\w0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w0-9](?:[\w0-9-]*[\w0-9])?\.)+[\w0-9](?:[\w0-9-]*[\w0-9])?)\Z", RegexOptions.IgnoreCase))
            //    {
            //        mb2.Show(Properties.Resources.IncorrectEmail, GenerateTitle(TitleType.Error, Properties.Resources.InvalidField), MessageBoxButton.OK, MessageBoxImage.Error);
            //        ClientEmail.TextBox.Focus();
            //        return false;
            //    }
            //}

            //if (Document.IsValidPhone == false && !String.IsNullOrEmpty(Document.CashFormattedText))
            //{
            //    mb3.Show(Properties.Resources.IncorrectPhone, GenerateTitle(TitleType.Error, Properties.Resources.InvalidField), MessageBoxButton.OK, MessageBoxImage.Error);
            //    ClientNumberPhone.TextBox.Focus();
            //    return false;
            //}

            return true;
        }


        Int32 Save()
        {
            ConvertData convertdata = new ConvertData();

            //if (FieldVerify(registerDocumentDetails))
            //{
            shemaStorаge.RegisterDocument.Clear();
            shemaStorаge.RegisterDocumentDetails.Clear();
            shemaStorаge.RegisterDocumentDelivery.Clear();
            shemaStorаge.RegisterDocumentPayment.Clear();

            request.supplyDocument.Details.Clear();
            request.supplyDocument.Delivery.Clear();
            request.supplyDocument.Payment.Clear();              

            //продукты                   
            foreach (RegisterDocumentDetails.LocaleRow currentrow in registerDocumentDetails)
            {
                DataRow row = shemaStorаge.RegisterDocumentDetails.NewRow();
                row["DocumentID"] = Document.ReffID;
                row["Name"] = currentrow.Name;
                row["Quantity"] = currentrow.Quantity;
                row["TagPriceUSA"] = currentrow.TagPriceUSA;
                row["TagPriceRUS"] = currentrow.TagPriceRUS;
                row["CategoryID"] = currentrow.CategoryID;
                row["CategoryDetailsID"] = currentrow.CategoryDetailsID;
                if (currentrow.ImageProduct != null)
                    row["ImageProduct"] = currentrow.ImageProduct;
                row["Barecodes"] = currentrow.BarCodeString;
                row["CreatedDate"] = currentrow.CreatedDate == null ? DateTime.Now : currentrow.CreatedDate;
                row["CreatedUserID"] = currentrow.CreatedUserID;
                row["LastModificatedDate"] = DateTime.Now;
                row["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID; 
                row["Model"] = currentrow.Model;
                row["SizeProduct"] = currentrow.SizeProduct;
                row["Size"] = currentrow.Package;
                row["ShopID"] = attributes.numeric.attributeCompany.ShopId;
                row["CompanyID"] = attributes.numeric.attributeCompany.CompanyID;
                row["ReffTimeRow"] = currentrow.ReffTimeRow;             
                shemaStorаge.RegisterDocumentDetails.Rows.Add(row);

            }

            //сопуствующие товары             
            foreach (RegisterDocumetnDelivery.LocaleRow currentrow in registerDocumentDelivery)
            {
                DataRow row = shemaStorаge.RegisterDocumentDelivery.NewRow();
                row["DocumentID"] = Document.ReffID;
                row["DeliveryID"] = currentrow.DeliveryID;
                row["DeliveryDetailsID"] = currentrow.DeliveryDetailsID;
                row["DeliveryTTN"] = currentrow.DeliveryTTN;
                if (currentrow.TTNDocumentByte != null)
                    row["ImageTTN"] = currentrow.TTNDocumentByte;
                row["Invoice"] = currentrow.Invoice;
                if (currentrow.InvoiceDocumentByte != null)
                    row["ImageInvoice"] = currentrow.InvoiceDocumentByte;
                row["AmountUSA"] = currentrow.AmountUSA;
                row["AmountRUS"] = currentrow.AmountRUS;
                row["Description"] = currentrow.Description;
                row["TTN"] = currentrow.DeliveryTTN;
                row["CreatedDate"] = currentrow.CreatedDate == null ? DateTime.Now : currentrow.CreatedDate;
                row["CreatedUserID"] = currentrow.CreatedUserID;
                row["LastModificatedDate"] = DateTime.Now;
                row["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID; 
                row["ShopID"] = attributes.numeric.attributeCompany.ShopId;
                row["CompanyID"] = attributes.numeric.attributeCompany.CompanyID;
                row["ReffTimeRow"] =  currentrow.ReffTimeRow;       
                shemaStorаge.RegisterDocumentDelivery.Rows.Add(row);
            }

            //виды оплат
            shemaStorаge.SupplyDocumentPayment.Clear();
            foreach (RegisterDocumentPayment.LocaleRow currentrow in registerDocumentPayment)
            {
                DataRow row = shemaStorаge.RegisterDocumentPayment.NewRow();
                row["DocumentID"] = Document.ReffID;
                row["Status"] = currentrow.Status;
                row["OperationType"] = currentrow.OpertionType;
                row["Amount"] = currentrow.Amount;
                row["Description"] = currentrow.Description;
                row["RRN"] = currentrow.RRN;
                row["CreatedDate"] = currentrow.CreatedDate == null ? DateTime.Now : currentrow.CreatedDate;
                row["CreatedUserID"] = currentrow.CreatedUserID;
                row["LastModificatedDate"] = DateTime.Now;
                row["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                row["ShopID"] = attributes.numeric.attributeCompany.ShopId;
                row["CompanyID"] = attributes.numeric.attributeCompany.CompanyID;
                row["ReffTimeRow"] = currentrow.ReffTimeRow;     
                shemaStorаge.RegisterDocumentPayment.Rows.Add(row);

            }
            Document.Amount = summary.SummaryProductTagPriceRUS;
            Document.Count = summary.SummaryQuantityProduct;
            //докуметн
            DataRow rowDocument = shemaStorаge.RegisterDocument.NewRow();
            rowDocument["Count"] = Document.Count;
            rowDocument["Amount"] = Document.Amount;
            rowDocument["ReffID"] = Document.ReffID;
            rowDocument["ReffDate"] = Document.ReffDate;
            rowDocument["SupplyDocumentNumber"] = Document.SupplyDocumentNumber;
            rowDocument["CreatedDate"] = DateTime.Now;
            rowDocument["CreatedUserID"] = 1;// Document.UserID;
            rowDocument["LastModificatedDate"] = DateTime.Now;
            rowDocument["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
            rowDocument["Status"] = Document.Status;
            rowDocument["ShopID"] = attributes.numeric.attributeCompany.ShopId;
            rowDocument["CompanyID"] = attributes.numeric.attributeCompany.CompanyID;
            rowDocument["ReffTimeRow"] = Document.ReffTimeRow;
            rowDocument["RegisterDocumentNumber"] = 0;
            //rowDocument["ReffTimeRow"] = Document.ReffTimeRow;
            shemaStorаge.RegisterDocument.Rows.Add(rowDocument);
            Document.ShemaStorаgeLocal = shemaStorаge;
            Document.ID = registerDocumentLogic.SaveRowTable(Document);

            return Document.ID;
            //}
            //return 0;
        }

        Int32 SaveRequest()
        {
            Document.ID = registerDocumentLogic.SaveRespons(Document);
            return Document.ID;
        }
        #endregion

        #region ToolBarNewSupplyDocument
        private void SupplyDocumentDetailsToolBar_ButtonSave()
        {
            Save();
        }

        private void SupplyDocumentDetailsToolBar_ButtonSaveclose()
        {
            if (Save() > 0) { }
                //MainWindow.AppWindow.ButtonSupplyDocumentF(Document, NewDocument);
        }

        private void SupplyDocumentDetailsToolBar_ButtonListCancel()
        {
            //MainWindow.AppWindow.ButtonSupplyDocumentF(Document, NewDocument);
        }

        private void SupplyDocumentDetailsToolBar_ButtonRequest()
        {
            Sucsess();
        }

        private void SupplyDocumentDetailsToolBar_ButtonApply()
        {
           
        }
        #endregion

        #region CalculateSummary 

        //просто проверка денег
        void CalculateSummary()
        {
           // screenState = false;
            Int32 SummaryQuantityProductTemp = 0;
            decimal SummaryTagPriceWithUSATemp = 0;
            decimal SummaryTagPriceWithRUSTemp = 0;

            Int32 SummaryQuantityDeliveryTemp = 0;
            decimal SummaryAmountUSATemp = 0;
            decimal SummaryAmountRUSTemp = 0;

            decimal SummaryPaymentBalansTemp = 0; //Оплачено
            decimal SummaryPaymentRemainsTemp = 0; // остаток         

            foreach (RegisterDocumentDetails.LocaleRow row in registerDocumentDetails)
            {
                SummaryQuantityProductTemp = SummaryQuantityProductTemp + row.Quantity;
                SummaryTagPriceWithUSATemp = SummaryTagPriceWithUSATemp + row.TagPriceUSA;
                SummaryTagPriceWithRUSTemp = SummaryTagPriceWithRUSTemp + row.TagPriceRUS; 
            }

            SummaryQuantityDeliveryTemp = registerDocumentDelivery.Count();
            foreach (RegisterDocumetnDelivery.LocaleRow row in registerDocumentDelivery)
            {               
                SummaryAmountUSATemp = SummaryAmountUSATemp + row.AmountUSA;
                SummaryAmountRUSTemp = SummaryAmountRUSTemp + row.AmountRUS;
            }

            SummaryPaymentRemainsTemp = SummaryAmountRUSTemp + SummaryTagPriceWithRUSTemp;

            foreach (RegisterDocumentPayment.LocaleRow row in registerDocumentPayment)
            {
                if (row.Status == 1)
                {
                    SummaryPaymentBalansTemp = SummaryPaymentBalansTemp + row.Amount;
                    SummaryPaymentRemainsTemp = SummaryPaymentRemainsTemp - row.Amount;
                }           
                
            }


            summary.SummaryQuantityProduct = SummaryQuantityProductTemp;
            summary.SummaryProductTagPriceUSA = SummaryTagPriceWithUSATemp;
            summary.SummaryProductTagPriceRUS = SummaryTagPriceWithRUSTemp;

            summary.SummaryDeliveryQuantity = SummaryQuantityDeliveryTemp;
            summary.SummaryAmountUSA = SummaryAmountUSATemp;
            summary.SummaryAmountRUS = SummaryAmountRUSTemp;

            summary.SummaryPaymentBalans = SummaryPaymentBalansTemp < 0 ? Math.Abs(SummaryPaymentBalansTemp) : SummaryPaymentBalansTemp;
            summary.SummaryPaymentRemains = SummaryPaymentRemainsTemp < 0 ? Math.Abs(SummaryPaymentRemainsTemp) : SummaryPaymentRemainsTemp;

            switch (Document.Status)
            {
                case 0:
                    if (SummaryPaymentRemainsTemp > 0)
                    {
                        IsPaymentAddButton = true;
                    }
                    else
                    {
                        IsPaymentAddButton = false;
                    }

                    if (Math.Abs(SummaryPaymentRemainsTemp) <= 0)
                    {
                        IsApplyDocument = true;
                    }
                    else
                    {
                        IsApplyDocument = false;
                    }
                    
                    break;
                case 1:
                    IsPaymentAddButton = false;
                    IsApplyDocument = false;
                   
                    break;
            }
           

        }


        #endregion

        #region SetSupplyDocument
        private void Sucsess()
        {
            if (Save() > 0)
            {
                //request = new Request(attributes);
                //Document.SupplyDocumentNumber = registerDocumentLogic.SetRow(Document);
                //if (Document.ID > 0)
                //{
                //    RegisterDocumentLogic registerDocumentLogic = new RegisterDocumentLogic(attributes);
                //    RegisterDocument.LocalFilter localeFilter = new RegisterDocument.LocalFilter();
                //    localeFilter.ScreenTypeGrid = ScreenType.ItemByStatus;
                //    localeFilter.ID = Document.ID;
                //    DataTable documentTable = registerDocumentLogic.FillGrid(localeFilter);
                //    foreach(DataRow row in documentTable.Rows)
                //    {
                //        registerDocumentLogic.Convert(row, Document);
                //        DataTable dataTableSupplyDocumentDetails = registerDocumentDetailsLogic.FillGridDocument(Document.ID);
                //        foreach (DataRow rowDetails in dataTableSupplyDocumentDetails.Rows)
                //        {
                //            SupplyDocumentDetails.LocaleRow localeRowDetails = new SupplyDocumentDetails.LocaleRow();
                //            registerDocumentDetails.Add(registerDocumentDetailsLogic.Convert(rowDetails, localeRowDetails));
                //            SupplyDocumentDetailsRequest supplyDocumentDetailsRequest = new SupplyDocumentDetailsRequest();
                //            //localeRowDetails.BarCodeString = string.Empty;
                //            registerDocumentDetailsLogic.Convert(localeRowDetails, supplyDocumentDetailsRequest);
                //            request.supplyDocument.Details.Add(supplyDocumentDetailsRequest);
                //        }

                //        DataTable dataTableSupplyDocumentDelivery = registerDocumentDeliveryLogic.FillGrid(Document.ID);
                //        foreach (DataRow rowdelivery in dataTableSupplyDocumentDelivery.Rows)
                //        {
                //            RegisterDocumentDelivery.LocaleRow localeRowDelivery = new RegisterDocumentDelivery.LocaleRow();
                //            registerDocumentDelivery.Add(registerDocumentDeliveryLogic.Convert(rowdelivery, localeRowDelivery));
                //            SupplyDocumentDeliveryRequest rowDeliveryRequest = new SupplyDocumentDeliveryRequest(attributes);
                //            registerDocumentDeliveryLogic.Convert(localeRowDelivery, rowDeliveryRequest);
                //            request.supplyDocument.Delivery.Add(rowDeliveryRequest);
                //        }

                //        DataTable dataTableSupplyDocumentPayment = registerDocumentPaymentLogic.FillGrid(Document.ID);
                //        foreach (DataRow rowPayment in dataTableSupplyDocumentPayment.Rows)
                //        {
                //            SupplyDocumentPayment.LocaleRow localeRowPayment = new SupplyDocumentPayment.LocaleRow();
                //            registerDocumentPayment.Add(registerDocumentPaymentLogic.Convert(rowPayment, localeRowPayment));
                //            SupplyDocumentPaymentRequest rowPaymentRequest = new SupplyDocumentPaymentRequest(attributes);
                //            registerDocumentPaymentLogic.Convert(localeRowPayment, rowPaymentRequest);
                //            request.supplyDocument.Payment.Add(rowPaymentRequest);
                //        }
                //    }
                //    Document.Count = request.supplyDocument.Payment.Count() + request.supplyDocument.Delivery.Count() + request.supplyDocument.Details.Count();
                //    SupplyDocumentRequest supplyDocumentRequest = new SupplyDocumentRequest(attributes);
                //    registerDocumentLogic.Convert(Document, request.supplyDocument.Document);
                   
                //    Response response= request.GetCommand(1);
                //    if (response!=null && response.ErrorCode == 0)
                //    {
                //        Document.Status = response.SupplyDocumentOutput.Document.Status;
                //        Document.ReffID = 0;
                //        Document.ReffDate = response.SupplyDocumentOutput.Document.SyncDate;
                //        if (SaveRequest() == 0)
                //        {
                //            FlexMessageBox mb2 = new FlexMessageBox();
                //            List<BitmapImage> ButtonImages = new List<BitmapImage>();
                //            ButtonImages.Add(ImageHelper.GenerateImage("IconAdd.png"));
                //            ButtonImages.Add(ImageHelper.GenerateImage("IconContinueWork.png"));
                //            List<string> ButtonText = new List<string>();
                //            ButtonText.Add(Properties.Resources.AddSmall);
                //            ButtonText.Add(Properties.Resources.MessageIgnore);

                //            mb2.Show(Properties.Resources.ErrorDB, GenerateTitle(TitleType.Error, Properties.Resources.ErrorDBTitle), MessageBoxButton.OK, MessageBoxImage.Error);
                //        }
                //    }
                //    else
                //    {
                //        FlexMessageBox mb2 = new FlexMessageBox();
                //        List<BitmapImage> ButtonImages = new List<BitmapImage>();
                //        ButtonImages.Add(ImageHelper.GenerateImage("IconAdd.png"));
                //        ButtonImages.Add(ImageHelper.GenerateImage("IconContinueWork.png"));
                //        List<string> ButtonText = new List<string>();
                //        ButtonText.Add(Properties.Resources.AddSmall);
                //        ButtonText.Add(Properties.Resources.MessageIgnore);

                //        mb2.Show("Ошибка: " + response.ErrorCode + " - " + response.DescriptionEX, GenerateTitle(TitleType.Error, Properties.Resources.ErrorSendAPITitle), MessageBoxButton.OK, MessageBoxImage.Error);
                //    }
                //    // Выполняем запрос по адресу и получаем ответ в виде строки
                   
                //}


               // MainWindow.AppWindow.ButtonSupplyDocumentF(Document, NewDocument);
            }
        }


        #endregion

        private void SupplyDocumentDetailsToolBar_ButtonCancel()
        {

        }
    }
}
