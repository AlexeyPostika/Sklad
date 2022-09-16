using Newtonsoft.Json;
using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.Payment;
using Sklad_v1_001.FormUsers.Product;
using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
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

namespace Sklad_v1_001.FormUsers.SupplyDocument
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
    public partial class NewSupplyDocumentGrid : Page
    {
        //IsPaymentAddButton
        //AmountMax
        public static readonly DependencyProperty IsPaymentAddButtonProperty = DependencyProperty.Register(
                   "IsPaymentAddButton",
                   typeof(Boolean),
                  typeof(NewSupplyDocumentGrid), new PropertyMetadata(true));

        //IsApplyDocument
        public static readonly DependencyProperty IsApplyDocumentProperty = DependencyProperty.Register(
                  "IsApplyDocument",
                  typeof(Boolean),
                 typeof(NewSupplyDocumentGrid), new PropertyMetadata(false));
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
        PaymentItem newSupplyDocumentPaymentItem;
        Payment.PaymentLogic paymentLogic;
        //******************************

        //остновной документ      
        LocalRow document;
        SupplyDocumentLogic supplyDocumentLogic;
        Request request;

        //Продукт
        Product.LocalRow localeRowProduct;
        //ObservableCollection<Product.LocaleRow> detailsProduct;

        //доставка
        SupplyDocumentDeliveryLogic supplyDocumentDeliveryLogic;      
        ObservableCollection<SupplyDocumentDelivery.LocaleRow> supplyDocumentDelivery;

        //SupplyDocumentDetails
        SupplyDocumentDetailsLogic supplyDocumentDetailsLogic;      
        ObservableCollection<SupplyDocumentDetails.LocaleRow> supplyDocumentDetails;

        //оплата
        SupplyDocumentPaymentLogic supplyDocumentPaymentLogic;
        SupplyDocumentPayment.LocaleRow supplyDocumentPaymentLocaleRow;
        ObservableCollection<SupplyDocumentPayment.LocaleRow> supplyDocumentPayment;

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
                        ToolBarProduct.ButtonNewProduct.IsEnabled = true;
                        ToolBarProduct.ButtonDelete.IsEnabled = true;
                        ToolBarDelivery.ButtonNewProduct.IsEnabled = true;
                        ToolBarDelivery.ButtonDelete.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
                        SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonRequestSend.IsEnabled = true;
                        UserIDDocument.IsEnabled = true;
                        break;
                    case 1:
                    case 6:
                        ToolBarProduct.ButtonNewProduct.IsEnabled = false;
                        ToolBarProduct.ButtonDelete.IsEnabled = false;
                        ToolBarDelivery.ButtonNewProduct.IsEnabled = false;
                        ToolBarDelivery.ButtonDelete.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
                        SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonRequestSend.IsEnabled = false;
                        UserIDDocument.IsEnabled = false;                   
                        //SupplyDocumentDetailsToolBar.ButtonPrintLabels.IsEnabled = false;
                        break;                  
                    case 2:
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Collapsed;
                        //DocumentToolbar.ButtonPrintLabels.IsEnabled = true;
                        break;
                    case 3:
                        ToolBarProduct.ButtonNewProduct.IsEnabled = false;
                        ToolBarProduct.ButtonDelete.IsEnabled = false;
                        ToolBarDelivery.ButtonNewProduct.IsEnabled = false;
                        ToolBarDelivery.ButtonDelete.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;                 
                        SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonRequestSend.IsEnabled = false;
                        IsApplyDocument = true;
                        UserIDDocument.IsEnabled = false;
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


        public NewSupplyDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            Document = new LocalRow();
            summary = new RowSummary();

            shemaStorаge = new ShemaStorаge();

            request = new Request(attributes);
            supplyDocumentLogic = new SupplyDocumentLogic(attributes);
            supplyDocumentDetailsLogic = new SupplyDocumentDetailsLogic(attributes);
            supplyDocumentDeliveryLogic = new SupplyDocumentDeliveryLogic(attributes);
            supplyDocumentPaymentLogic = new SupplyDocumentPaymentLogic(attributes);

            supplyDocumentDetails = new ObservableCollection<SupplyDocumentDetails.LocaleRow>();
            supplyDocumentDelivery = new ObservableCollection<SupplyDocumentDelivery.LocaleRow>();
            supplyDocumentPayment = new ObservableCollection<SupplyDocumentPayment.LocaleRow>();

            datalistDeleted = new ObservableCollection<ComplexKey>();

            dataListCategory = new ObservableCollection<Category.LocalRow>();
            dataListDelivery = new ObservableCollection<Delivery.LocaleRow>();
            dataListDeliveryDetails = new ObservableCollection<DeliveryDetails.LocaleRow>();          

            SupplyTypeList supplyTypeList = new SupplyTypeList();
            this.StatusDocument.ComboBoxElement.ItemsSource = supplyTypeList.innerList;

            this.DataProduct.ItemsSource = supplyDocumentDetails;
            this.DataDelivery.ItemsSource = supplyDocumentDelivery;
            this.DataPayment.ItemsSource = supplyDocumentPayment;

            this.ToolBarDelivery.ButtonNewProduct.Text = Properties.Resources.ADD;
            this.ToolBarPayment.ButtonNewProduct.Text = Properties.Resources.ADD;

            this.DataContext = Document;
            this.DocumentSummary.DataContext = summary;

            UserIDDocument.ComboBoxElement.ItemsSource = attributes.datalistUsers;
           // Refresh();
        }

        private void Refresh()
        {
            supplyDocumentDetails.Clear();
            supplyDocumentDelivery.Clear();
            supplyDocumentPayment.Clear();

            DataTable dataTableSupplyDocumentDetails = supplyDocumentDetailsLogic.FillGridDocument(Document.ID);
            foreach(DataRow row in dataTableSupplyDocumentDetails.Rows)
            {
                supplyDocumentDetails.Add(supplyDocumentDetailsLogic.Convert(row, new SupplyDocumentDetails.LocaleRow()));
            }
           
            DataTable dataTableSupplyDocumentDelivery = supplyDocumentDeliveryLogic.FillGrid(Document.ID);
            foreach (DataRow row in dataTableSupplyDocumentDelivery.Rows)
            {
                supplyDocumentDelivery.Add(supplyDocumentDeliveryLogic.Convert(row, new SupplyDocumentDelivery.LocaleRow()));
            }
           
            DataTable dataTableSupplyDocumentPayment = supplyDocumentPaymentLogic.FillGrid(Document.ID);
            foreach (DataRow row in dataTableSupplyDocumentPayment.Rows)
            {
                supplyDocumentPayment.Add(supplyDocumentPaymentLogic.Convert(row, new SupplyDocumentPayment.LocaleRow()));
            }

            CalculateSummary();
        }
       
        #region Продукт

        private void EditProduct(SupplyDocumentDetails.LocaleRow currentrow = null)
        {
            newAddProductItem = new NewAddProductItem(attributes);
            addProductWindow = new FlexWindows(Properties.Resources.Products);
            newAddProductItem.StatusDocument = Document.Status == 0;
            newAddProductItem.ProductLocalRow = currentrow != null ? supplyDocumentDetailsLogic.ConvertSupplyDocumentDetailsToProduct(new Product.LocalRow(), currentrow) : new Product.LocalRow();
            addProductWindow.Content = newAddProductItem;
            addProductWindow.ShowDialog();
            if (newAddProductItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newAddProductItem.ProductLocalRow != null)
                {
                    SupplyDocumentDetails.LocaleRow locale = new SupplyDocumentDetails.LocaleRow();
                    localeRowProduct = newAddProductItem.ProductLocalRow;
                    if (newAddProductItem.ProductLocalRow!=null)
                    {                            
                        locale.TempID = supplyDocumentDetails.Count() + 1;
                        locale.LineDocument = supplyDocumentDetails.Count() + 1;
                        supplyDocumentDetails.Add(supplyDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, locale));
                    }
                    else
                    {
                        int tempID = supplyDocumentDetails.FirstOrDefault(x => x.LineDocument == localeRowProduct.ID) != null ? supplyDocumentDetails.FirstOrDefault(x => x.LineDocument == localeRowProduct.ID).LineDocument : 0;
                        
                        locale = supplyDocumentDetails.FirstOrDefault(x => x.LineDocument == localeRowProduct.ID);
                        supplyDocumentDetails.Remove(locale);
                        
                        if (locale == null)
                            locale = new SupplyDocumentDetails.LocaleRow();

                        supplyDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, locale);                      
                        locale.TempID = tempID;
                        locale.LineDocument = tempID;
                        supplyDocumentDetails.Add(locale);
                    }
                }
            }
            CalculateSummary();
        }

        private void ToolBarProduct_ButtonNewProductClick()
        {
            //MainWindow.AppWindow.ButtonNewAddProduct();           
            EditProduct();
        }

        private void DataProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SupplyDocumentDetails.LocaleRow currentrow = this.DataProduct.SelectedItem as SupplyDocumentDetails.LocaleRow;
            if (currentrow != null)
            {
                //MainWindow.AppWindow.ButtonNewAddProductF(supplyDocumentDetailsLogic.ConvertSupplyDocumentDetailsToProduct(new Product.LocaleRow(), currentrow));
                EditProduct(currentrow);
            }
        }

        private void ToolBarProduct_ButtonDeleteClick()
        {
            if (DataProduct.SelectedItems.Count > 0)
            {
                FlexMessageBox mb = new FlexMessageBox();
                MessageBoxResult dialogresult = mb.Show(Properties.Resources.QuestionDelete, GenerateTitle(TitleType.Question, Properties.Resources.Deletion), MessageBoxButton.OKCancel, MessageBoxImage.Question, 3);
                if (dialogresult == MessageBoxResult.OK)
                {
                    var currentRowViews = DataProduct.SelectedItems;
                    foreach (SupplyDocumentDetails.LocaleRow currentrow in currentRowViews)
                    {
                        SupplyDocumentDetails.LocaleRow deleteProduct = supplyDocumentDetails.LastOrDefault(x => x.TempID == currentrow.TempID);                      
                        ComplexKey complexKey = new ComplexKey();
                        complexKey.Id = deleteProduct.ID;
                        complexKey.Type = 1;
                        datalistDeleted.Add(complexKey);
                    }
                    foreach (ComplexKey complex in datalistDeleted)
                    {
                        SupplyDocumentDetails.LocaleRow deleteProduct = supplyDocumentDetails.LastOrDefault(x => x.ID == complex.Id);
                        supplyDocumentDetails.Remove(deleteProduct);
                    }
                }                 
            }
            CalculateSummary();
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
            SupplyDocumentDelivery.LocaleRow localeRowDelivery = new SupplyDocumentDelivery.LocaleRow();
            supplyDocumentDeliveryItem = new SupplyDocumentDeliveryItem(attributes);
            addDeliveryWindow = new FlexWindows(Properties.Resources.Deliveries);

            supplyDocumentDeliveryItem.StatusDocument = Document.Status == 0;        
            supplyDocumentDeliveryItem.DeliveryRow= currentrow != null ? currentrow : new SupplyDocumentDelivery.LocaleRow();
           
            addDeliveryWindow.Content = supplyDocumentDeliveryItem;
            addDeliveryWindow.ShowDialog();
            //тут проблема посмотреть, не правильно добавляется ID
            if (supplyDocumentDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (supplyDocumentDeliveryItem.DeliveryRow != null && !String.IsNullOrEmpty(supplyDocumentDeliveryItem.DeliveryRow.NameCompany))
                {
                    localeRowDelivery = supplyDocumentDeliveryItem.DeliveryRow;
                    SupplyDocumentDelivery.LocaleRow locale = new SupplyDocumentDelivery.LocaleRow();
                    if (localeRowDelivery.LineDocument == 0)
                    {               
                        localeRowDelivery.TempID = supplyDocumentDelivery.Count() + 1;
                        localeRowDelivery.LineDocument = supplyDocumentDelivery.Count() + 1;
                        supplyDocumentDelivery.Add(localeRowDelivery);
                    }
                    else
                    {
                        int tempID = supplyDocumentDelivery.FirstOrDefault(x => x.LineDocument == localeRowDelivery.LineDocument) != null ? supplyDocumentDelivery.FirstOrDefault(x => x.LineDocument == localeRowDelivery.LineDocument).LineDocument : 0;
                        locale = supplyDocumentDelivery.FirstOrDefault(x => x.LineDocument == localeRowDelivery.LineDocument);
                        supplyDocumentDelivery.Remove(locale);
                        locale = localeRowDelivery;
                        locale.TempID = tempID;
                        locale.LineDocument = tempID;
                        supplyDocumentDelivery.Add(locale);
                    }
                }
            }
            CalculateSummary();         
        }
        private void ToolBarDelivery_ButtonNewProductClick()
        {
            EditDelivery();
        }

        private void DataDelivery_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SupplyDocumentDelivery.LocaleRow currentrow = this.DataDelivery.SelectedItem as SupplyDocumentDelivery.LocaleRow;
            if (currentrow != null)
            {               
                EditDelivery(currentrow);
            }
        }

        private void ToolBarDelivery_ButtonDeleteClick()
        {
            datalistDeleted.Clear();

            if (DataDelivery.SelectedItems.Count > 0)
            {
                FlexMessageBox mb = new FlexMessageBox();
                MessageBoxResult dialogresult = mb.Show(Properties.Resources.QuestionDelete, GenerateTitle(TitleType.Question, Properties.Resources.Deletion), MessageBoxButton.OKCancel, MessageBoxImage.Question, 3);
                if (dialogresult == MessageBoxResult.OK)
                {
                    var currentRowViews = DataDelivery.SelectedItems;
                    foreach (SupplyDocumentDelivery.LocaleRow currentrow in currentRowViews)
                    {
                        SupplyDocumentDelivery.LocaleRow deleteDelivery = supplyDocumentDelivery.LastOrDefault(x => x.TempID == currentrow.TempID);
                        ComplexKey complexKey = new ComplexKey();
                        complexKey.Id = deleteDelivery.TempID;
                        complexKey.Type = 2;
                        datalistDeleted.Add(complexKey);
                    }
                    foreach (ComplexKey complex in datalistDeleted)
                    {
                        SupplyDocumentDelivery.LocaleRow deleteDelivery = supplyDocumentDelivery.LastOrDefault(x => x.TempID == complex.Id);
                        supplyDocumentDelivery.Remove(deleteDelivery);
                    }
                }
            }
            CalculateSummary();
        }

        #endregion

        #region оплата
        private void EditPayment(Payment.LocaleRow currentrow = null)
        {
            supplyDocumentPaymentLocaleRow = new SupplyDocumentPayment.LocaleRow();
            newSupplyDocumentPaymentItem = new PaymentItem();
            addSuppluPaymentWindow = new FlexWindows(Properties.Resources.Payment1);
            newSupplyDocumentPaymentItem.AmountMax = (Double)summary.SummaryPaymentRemains;
            newSupplyDocumentPaymentItem.StatusDocument = Document.Status == 0;
            newSupplyDocumentPaymentItem.PaymentLocalRow = currentrow != null ? currentrow : new Payment.LocaleRow();          
            addSuppluPaymentWindow.Content = newSupplyDocumentPaymentItem;
            addSuppluPaymentWindow.ShowDialog();
            if (newSupplyDocumentPaymentItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newSupplyDocumentPaymentItem.PaymentLocalRow != null)
                {
                    SupplyDocumentPayment.LocaleRow paymentLocalRow = paymentLogic.ConvertPaymentToSupplyDocument(newSupplyDocumentPaymentItem.PaymentLocalRow, new SupplyDocumentPayment.LocaleRow());
                    SupplyDocumentPayment.LocaleRow locale = new SupplyDocumentPayment.LocaleRow();
                    if (paymentLocalRow.LineDocument == 0)
                    {
                        paymentLocalRow.TempID = supplyDocumentPayment.Count() + 1;
                        paymentLocalRow.LineDocument = supplyDocumentPayment.Count() + 1;
                        supplyDocumentPayment.Add(paymentLocalRow);
                    }
                    else
                    {
                        int tempID = supplyDocumentPayment.FirstOrDefault(x => x.LineDocument == paymentLocalRow.ID) != null ? supplyDocumentPayment.FirstOrDefault(x => x.LineDocument == paymentLocalRow.ID).LineDocument : 0;
                        
                        locale = supplyDocumentPayment.FirstOrDefault(x => x.LineDocument == paymentLocalRow.LineDocument);
                        supplyDocumentPayment.Remove(locale);                      
                        locale = paymentLocalRow;
                        locale.LineDocument = tempID;
                        supplyDocumentPayment.Add(locale);
                    }
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
           List<SupplyDocumentPayment.LocaleRow> currentrow = this.DataPayment.SelectedItems.Cast<SupplyDocumentPayment.LocaleRow>().ToList();
            if (currentrow != null && currentrow.Count()>0)
            {
                paymentLogic = new PaymentLogic(attributes);
                EditPayment(paymentLogic.ConvertSupplyDocumentToPayment(currentrow.First(), new Payment.LocaleRow()));
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
                    var currentRowViews = DataPayment.SelectedItems;
                    foreach (SupplyDocumentPayment.LocaleRow currentrow in currentRowViews)
                    {
                        SupplyDocumentPayment.LocaleRow deletePayment = supplyDocumentPayment.LastOrDefault(x => x.TempID == currentrow.TempID);                      
                        ComplexKey complexKey = new ComplexKey();
                        complexKey.Id = deletePayment.TempID;
                        complexKey.Type = 3;
                        datalistDeleted.Add(complexKey);
                    }
                    foreach (ComplexKey complex in datalistDeleted)
                    {
                        SupplyDocumentPayment.LocaleRow deletePayment = supplyDocumentPayment.LastOrDefault(x => x.TempID == complex.Id);
                        supplyDocumentPayment.Remove(deletePayment);
                    }
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

            if (FieldVerify(supplyDocumentDetails))
            {
                shemaStorаge.SupplyDocumentDetails.Clear();
                shemaStorаge.SupplyDocumentDelivery.Clear();
                shemaStorаge.SupplyDocumentPayment.Clear();
               
                request.supplyDocument.Details.Clear();
                request.supplyDocument.Delivery.Clear();
                request.supplyDocument.Payment.Clear();

                //продукты                   
                foreach (SupplyDocumentDetails.LocaleRow currentrow in supplyDocumentDetails)
                {                    
                    DataRow row = shemaStorаge.SupplyDocumentDetails.NewRow();
                    row["DocumentID"] = 0;
                    row["Name"] = currentrow.Name;
                    row["Quantity"] = currentrow.Quantity;
                    row["TagPriceUSA"] = currentrow.TagPriceUSA;
                    row["TagPriceRUS"] = currentrow.TagPriceRUS;
                    row["CategoryID"] = currentrow.CategoryID;
                    row["CategoryDetailsID"] = currentrow.CategoryDetailsID;
                    if (currentrow.ImageProduct!=null)
                        row["ImageProduct"] = currentrow.ImageProduct;
                    row["Barcodes"] = currentrow.BarCodeString;
                    row["CreatedDate"] = currentrow.CreatedDate == null ? DateTime.Now : currentrow.CreatedDate;
                    row["CreatedUserID"] = currentrow.CreatedUserID;
                    row["LastModificatedDate"] = DateTime.Now;
                    row["LastModificatedUserID"] = Document.UserID;
                    row["Model"] = currentrow.Model;
                    row["SizeProduct"] = currentrow.SizeProduct;
                    row["Size"] = currentrow.Package;
                    row["BarcodesInput"] = currentrow.BarcodesInput;
                    shemaStorаge.SupplyDocumentDetails.Rows.Add(row);

                }

                //сопуствующие товары             
                foreach (SupplyDocumentDelivery.LocaleRow currentrow in supplyDocumentDelivery)
                {                                     
                    DataRow row = shemaStorаge.SupplyDocumentDelivery.NewRow();
                    row["DocumentID"] = 0;
                    row["DeliveryID"] = currentrow.DeliveryID;
                    row["DeliveryDetailsID"] = currentrow.DeliveryDetailsID;
                    row["DeliveryTTN"] = "";
                    if (currentrow.TTNDocumentByte != null)
                        row["ImageTTN"] = currentrow.TTNDocumentByte;
                    row["Invoice"] = currentrow.Invoice;
                    if (currentrow.InvoiceDocumentByte != null)
                        row["ImageInvoice"] = currentrow.InvoiceDocumentByte;
                    row["AmountUSA"] = currentrow.AmountUSA;
                    row["AmountRUS"] = currentrow.AmountRUS;
                    row["Description"] = currentrow.Description;
                    row["TTN"] = currentrow.TTN;
                    row["CreatedDate"] = currentrow.CreatedDate == null ? DateTime.Now : currentrow.CreatedDate;
                    row["CreatedUserID"] = currentrow.CreatedUserID;
                    row["LastModificatedDate"] = DateTime.Now;
                    row["LastModificatedUserID"] = Document.UserID;                
                    shemaStorаge.SupplyDocumentDelivery.Rows.Add(row);                    
                }

                //виды оплат
                shemaStorаge.SupplyDocumentPayment.Clear();
                foreach (SupplyDocumentPayment.LocaleRow currentrow in supplyDocumentPayment)
                {
                    DataRow row = shemaStorаge.SupplyDocumentPayment.NewRow();
                    row["DocumentID"] = 0;
                    row["Status"] = currentrow.Status;
                    row["OperationType"] = currentrow.OpertionType;
                    row["Amount"] = currentrow.Amount;                
                    row["Description"] = currentrow.Description;
                    row["RRN"] = currentrow.RRN;
                    row["CreatedDate"] = currentrow.CreatedDate == null ? DateTime.Now : currentrow.CreatedDate;
                    row["CreatedUserID"] = currentrow.CreatedUserID;
                    row["LastModificatedDate"] = DateTime.Now;
                    row["LastModificatedUserID"] = Document.UserID;
                    shemaStorаge.SupplyDocumentPayment.Rows.Add(row);
                                      
                }
                Document.Amount = summary.SummaryProductTagPriceRUS;
                Document.Count = summary.SummaryQuantityProduct;
                Document.ShemaStorаgeLocal = shemaStorаge;
                Document.ID = supplyDocumentLogic.SaveRowTable(Document);
               
                return Document.ID;
            }
            return 0;
        }

        Int32 SaveRequest()
        {
            Document.ID = supplyDocumentLogic.SaveRespons(Document);
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
            if (Save() > 0)
                MainWindow.AppWindow.ButtonSupplyDocumentF(Document, NewDocument);
        }

        private void SupplyDocumentDetailsToolBar_ButtonListCancel()
        {
            MainWindow.AppWindow.ButtonSupplyDocumentF(Document, NewDocument);
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

            foreach (SupplyDocumentDetails.LocaleRow row in supplyDocumentDetails)
            {
                SummaryQuantityProductTemp = SummaryQuantityProductTemp + row.Quantity;
                SummaryTagPriceWithUSATemp = SummaryTagPriceWithUSATemp + row.TagPriceUSA;
                SummaryTagPriceWithRUSTemp = SummaryTagPriceWithRUSTemp + row.TagPriceRUS; 
            }

            SummaryQuantityDeliveryTemp = supplyDocumentDelivery.Count();
            foreach (SupplyDocumentDelivery.LocaleRow row in supplyDocumentDelivery)
            {               
                SummaryAmountUSATemp = SummaryAmountUSATemp + row.AmountUSA;
                SummaryAmountRUSTemp = SummaryAmountRUSTemp + row.AmountRUS;
            }

            SummaryPaymentRemainsTemp = SummaryAmountRUSTemp + SummaryTagPriceWithRUSTemp;

            foreach (SupplyDocumentPayment.LocaleRow row in supplyDocumentPayment)
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
                    ToolBarProduct.ButtonNewProduct.IsEnabled = true;
                    ToolBarProduct.ButtonDelete.IsEnabled = true;
                    ToolBarDelivery.ButtonNewProduct.IsEnabled = true;
                    ToolBarDelivery.ButtonDelete.IsEnabled = true;
                    ToolBarPayment.ButtonDelete.IsEnabled = true;
                    break;
                case 1:
                    IsPaymentAddButton = false;
                    IsApplyDocument = false;
                    ToolBarProduct.ButtonNewProduct.IsEnabled = false;
                    ToolBarProduct.ButtonDelete.IsEnabled = false;
                    ToolBarDelivery.ButtonNewProduct.IsEnabled = false;
                    ToolBarDelivery.ButtonDelete.IsEnabled = false;
                    ToolBarPayment.ButtonDelete.IsEnabled = false;
                    break;
            }
           

        }


        #endregion

        #region SetSupplyDocument
        private void Sucsess()
        {
            if (Save() > 0)
            {
                request = new Request(attributes);
                Document.SupplyDocumentNumber = supplyDocumentLogic.SetRow(Document);
                if (Document.ID > 0)
                {
                    SupplyDocumentLogic supplyDocumentLogic = new SupplyDocumentLogic(attributes);
                    SupplyDocument.LocalFilter localeFilter = new SupplyDocument.LocalFilter();
                    localeFilter.ScreenTypeGrid = ScreenType.ItemByStatus;
                    localeFilter.ID = Document.ID;
                    DataTable documentTable = supplyDocumentLogic.FillGrid(localeFilter);
                    foreach(DataRow row in documentTable.Rows)
                    {
                        supplyDocumentLogic.Convert(row, Document);
                        DataTable dataTableSupplyDocumentDetails = supplyDocumentDetailsLogic.FillGridDocument(Document.ID);
                        foreach (DataRow rowDetails in dataTableSupplyDocumentDetails.Rows)
                        {
                            SupplyDocumentDetails.LocaleRow localeRowDetails = new SupplyDocumentDetails.LocaleRow();
                            supplyDocumentDetails.Add(supplyDocumentDetailsLogic.Convert(rowDetails, localeRowDetails));
                            SupplyDocumentDetailsRequest supplyDocumentDetailsRequest = new SupplyDocumentDetailsRequest(attributes);
                            //localeRowDetails.BarCodeString = string.Empty;
                            supplyDocumentDetailsLogic.Convert(localeRowDetails, supplyDocumentDetailsRequest);
                            request.supplyDocument.Details.Add(supplyDocumentDetailsRequest);
                        }

                        DataTable dataTableSupplyDocumentDelivery = supplyDocumentDeliveryLogic.FillGrid(Document.ID);
                        foreach (DataRow rowdelivery in dataTableSupplyDocumentDelivery.Rows)
                        {
                            SupplyDocumentDelivery.LocaleRow localeRowDelivery = new SupplyDocumentDelivery.LocaleRow();
                            supplyDocumentDelivery.Add(supplyDocumentDeliveryLogic.Convert(rowdelivery, localeRowDelivery));
                            SupplyDocumentDeliveryRequest rowDeliveryRequest = new SupplyDocumentDeliveryRequest(attributes);
                            supplyDocumentDeliveryLogic.Convert(localeRowDelivery, rowDeliveryRequest);
                            request.supplyDocument.Delivery.Add(rowDeliveryRequest);
                        }

                        DataTable dataTableSupplyDocumentPayment = supplyDocumentPaymentLogic.FillGrid(Document.ID);
                        foreach (DataRow rowPayment in dataTableSupplyDocumentPayment.Rows)
                        {
                            SupplyDocumentPayment.LocaleRow localeRowPayment = new SupplyDocumentPayment.LocaleRow();
                            supplyDocumentPayment.Add(supplyDocumentPaymentLogic.Convert(rowPayment, localeRowPayment));
                            SupplyDocumentPaymentRequest rowPaymentRequest = new SupplyDocumentPaymentRequest(attributes);
                            supplyDocumentPaymentLogic.Convert(localeRowPayment, rowPaymentRequest);
                            request.supplyDocument.Payment.Add(rowPaymentRequest);
                        }
                    }
                    Document.Count = request.supplyDocument.Payment.Count() + request.supplyDocument.Delivery.Count() + request.supplyDocument.Details.Count();
                    SupplyDocumentRequest supplyDocumentRequest = new SupplyDocumentRequest(attributes);
                    supplyDocumentLogic.Convert(Document, request.supplyDocument.Document);
                   
                    Response response= request.GetCommand(1);
                    if (response!=null && response.ErrorCode == 0)
                    {
                        Document.Status = response.SupplyDocumentOutput.Document.Status;
                        Document.ReffID = 0;
                        Document.ReffDate = response.SupplyDocumentOutput.Document.SyncDate;
                        if (SaveRequest() == 0)
                        {
                            FlexMessageBox mb2 = new FlexMessageBox();
                            List<BitmapImage> ButtonImages = new List<BitmapImage>();
                            ButtonImages.Add(ImageHelper.GenerateImage("IconAdd.png"));
                            ButtonImages.Add(ImageHelper.GenerateImage("IconContinueWork.png"));
                            List<string> ButtonText = new List<string>();
                            ButtonText.Add(Properties.Resources.AddSmall);
                            ButtonText.Add(Properties.Resources.MessageIgnore);

                            mb2.Show(Properties.Resources.ErrorDB, GenerateTitle(TitleType.Error, Properties.Resources.ErrorDBTitle), MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        FlexMessageBox mb2 = new FlexMessageBox();
                        List<BitmapImage> ButtonImages = new List<BitmapImage>();
                        ButtonImages.Add(ImageHelper.GenerateImage("IconAdd.png"));
                        ButtonImages.Add(ImageHelper.GenerateImage("IconContinueWork.png"));
                        List<string> ButtonText = new List<string>();
                        ButtonText.Add(Properties.Resources.AddSmall);
                        ButtonText.Add(Properties.Resources.MessageIgnore);

                        mb2.Show("Ошибка: " + response.ErrorCode + " - " + response.DescriptionEX, GenerateTitle(TitleType.Error, Properties.Resources.ErrorSendAPITitle), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                   
                }


                MainWindow.AppWindow.ButtonSupplyDocumentF(Document, NewDocument);
            }
        }

        #endregion

      
    }
}
