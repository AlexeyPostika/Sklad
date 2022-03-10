using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.Product;
using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
using Sklad_v1_001.FormUsers.SupplyDocumentDetails;
using Sklad_v1_001.FormUsers.SupplyDocumentPayment;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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
        FlexMessageBox addProductWindow;
        NewAddProductItem newAddProductItem;
        ObservableCollection<Product.LocaleRow> dataListProduct;
        //******************************

        //работаем с категориями      
        ObservableCollection<Category.LocalRow> dataListCategory;
        //ObservableCollection<CategoryDetails.LocalRow> dataListDeliveryDetails;
        //******************************

        //работаем с доставкой
        FlexMessageBox addDeliveryWindow;
        SupplyDocumentDeliveryItem supplyDocumentDeliveryItem;
        ObservableCollection<Delivery.LocaleRow> dataListDelivery;
        ObservableCollection<DeliveryDetails.LocaleRow> dataListDeliveryDetails;

        //******************************

        //работаем с оплатами
        FlexMessageBox addSuppluPaymentWindow;
        NewSupplyDocumentPaymentItem newSupplyDocumentPaymentItem;
        //******************************

        //остновной документ      
        LocalRow document;
        SupplyDocumentLogic supplyDocumentLogic;

        //Продукт
        Product.LocaleRow localeRowProduct;
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
                if (document == null || document.ID==0)
                {
                    status = 0;
                }
                else
                    status = 1;
            
                this.DataContext = Document;
                NewDocument = Document.ID == 0;

                switch (Document.Status)
                {
                    case 0:
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
                        SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = true;
                        SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        UserIDDocument.IsEnabled = true;
                        break;
                    case 1:
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
                        SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonSaveb.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonSaveClose.IsEnabled = false;
                        SupplyDocumentDetailsToolBar.ButtonListcansel.IsEnabled = true;
                        UserIDDocument.IsEnabled = false;
                        //SupplyDocumentDetailsToolBar.ButtonPrintLabels.IsEnabled = false;
                        break;                  
                    case 2:
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Collapsed;
                        //DocumentToolbar.ButtonPrintLabels.IsEnabled = true;
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

            supplyDocumentLogic = new SupplyDocumentLogic();
            supplyDocumentDetailsLogic = new SupplyDocumentDetailsLogic();
            supplyDocumentDeliveryLogic = new SupplyDocumentDeliveryLogic();
            supplyDocumentPaymentLogic = new SupplyDocumentPaymentLogic();

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
            addProductWindow = new FlexMessageBox();
            newAddProductItem.ProductLocalRow = currentrow != null ? supplyDocumentDetailsLogic.ConvertSupplyDocumentDetailsToProduct(new Product.LocaleRow(), currentrow) : new Product.LocaleRow();
            addProductWindow.Content = newAddProductItem;
            addProductWindow.Show(Properties.Resources.Products);
            if (newAddProductItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newAddProductItem.ProductLocalRow != null)
                {
                    SupplyDocumentDetails.LocaleRow locale = new SupplyDocumentDetails.LocaleRow();
                    localeRowProduct = newAddProductItem.ProductLocalRow;
                    if (newAddProductItem.ProductLocalRow.ID == 0)
                    {                            
                        locale.TempID = supplyDocumentDetails.Count() + 1;
                        locale.LineDocument = supplyDocumentDetails.Count() + 1;
                        supplyDocumentDetails.Add(supplyDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, locale));
                    }
                    else
                    {
                        locale = supplyDocumentDetails.FirstOrDefault(x => x.ID == localeRowProduct.ID);
                        supplyDocumentDetails.Remove(locale);
                        if (locale == null)
                            locale = new SupplyDocumentDetails.LocaleRow();
                        supplyDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, locale);                      
                        locale.TempID = supplyDocumentDetails.Count() + 1;
                        locale.LineDocument = supplyDocumentDetails.Count() + 1;
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
            addDeliveryWindow = new FlexMessageBox();
            supplyDocumentDeliveryItem.DeliveryRow= currentrow != null ? currentrow : new SupplyDocumentDelivery.LocaleRow();
            supplyDocumentDeliveryItem.Status = Document.Status;
            addDeliveryWindow.Content = supplyDocumentDeliveryItem;
            addDeliveryWindow.Show(Properties.Resources.Deliveries);
            //тут проблема посмотреть, не правильно добавляется ID
            if (supplyDocumentDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (supplyDocumentDeliveryItem.DeliveryRow != null && !String.IsNullOrEmpty(supplyDocumentDeliveryItem.DeliveryRow.NameCompany))
                {
                    localeRowDelivery = supplyDocumentDeliveryItem.DeliveryRow;
                    SupplyDocumentDelivery.LocaleRow locale = new SupplyDocumentDelivery.LocaleRow();
                    if (localeRowDelivery.ID == 0)
                    {               
                        localeRowDelivery.TempID = supplyDocumentDelivery.Count() + 1;
                        locale.LineDocument = supplyDocumentDetails.Count() + 1;
                        supplyDocumentDelivery.Add(localeRowDelivery);
                    }
                    else
                    {
                        locale = supplyDocumentDelivery.FirstOrDefault(x => x.TempID == localeRowDelivery.ID);
                        supplyDocumentDelivery.Remove(locale);
                        locale = localeRowDelivery;
                        locale.TempID = supplyDocumentDelivery.Count() + 1;
                        locale.LineDocument = supplyDocumentDetails.Count() + 1;
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
        private void EditPayment(SupplyDocumentPayment.LocaleRow currentrow = null)
        {
            supplyDocumentPaymentLocaleRow = new SupplyDocumentPayment.LocaleRow();
            newSupplyDocumentPaymentItem = new NewSupplyDocumentPaymentItem();
            addSuppluPaymentWindow = new FlexMessageBox();
            newSupplyDocumentPaymentItem.AmountMax = (Double)summary.SummaryPaymentRemains;
            newSupplyDocumentPaymentItem.PaymentLocalRow = currentrow != null ? currentrow : new SupplyDocumentPayment.LocaleRow();
            addSuppluPaymentWindow.Content = newSupplyDocumentPaymentItem;
            addSuppluPaymentWindow.Show(Properties.Resources.Payment1);
            if (newSupplyDocumentPaymentItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newSupplyDocumentPaymentItem.PaymentLocalRow != null)
                {
                    SupplyDocumentPayment.LocaleRow paymentLocalRow = newSupplyDocumentPaymentItem.PaymentLocalRow;
                    SupplyDocumentPayment.LocaleRow locale = new SupplyDocumentPayment.LocaleRow();
                    if (paymentLocalRow.ID == 0)
                    {
                        paymentLocalRow.TempID = supplyDocumentPayment.Count() + 1;
                        locale.LineDocument = supplyDocumentDetails.Count() + 1;
                        supplyDocumentPayment.Add(paymentLocalRow);
                    }
                    else
                    {
                        locale = supplyDocumentPayment.FirstOrDefault(x => x.ID == paymentLocalRow.ID);
                        supplyDocumentPayment.Remove(locale);                      
                        locale = paymentLocalRow;
                        locale.LineDocument = supplyDocumentDetails.Count() + 1;
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
            SupplyDocumentPayment.LocaleRow currentrow = this.DataPayment.SelectedItem as SupplyDocumentPayment.LocaleRow;
            if (currentrow != null)
            {
                EditPayment(currentrow);
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

        private void SupplyDocumentDetailsToolBar_ButtonApply()
        {
            Sucsess();
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
                Document.ID = supplyDocumentLogic.SetRow(Document);
                MainWindow.AppWindow.ButtonSupplyDocumentF(Document, NewDocument);
            }
        }
        #endregion

    }
}
