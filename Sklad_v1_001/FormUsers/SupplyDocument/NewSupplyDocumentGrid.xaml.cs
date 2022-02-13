using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.Product;
using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
using Sklad_v1_001.FormUsers.SupplyDocumentDetails;
using Sklad_v1_001.FormUsers.SupplyDocumentPayment;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.HelperGlobal;
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

        //Суммы
        RowSummary summary;

        private Int32 status;

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

                switch (Document.Status)
                {
                    case 0:
                    case 1:
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
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

            supplyDocumentLogic = new SupplyDocumentLogic();
            supplyDocumentDetailsLogic = new SupplyDocumentDetailsLogic();
            supplyDocumentDeliveryLogic = new SupplyDocumentDeliveryLogic();
            supplyDocumentPaymentLogic = new SupplyDocumentPaymentLogic();

            supplyDocumentDetails = new ObservableCollection<SupplyDocumentDetails.LocaleRow>();
            supplyDocumentDelivery = new ObservableCollection<SupplyDocumentDelivery.LocaleRow>();
            supplyDocumentPayment = new ObservableCollection<SupplyDocumentPayment.LocaleRow>();

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
            if (supplyDocumentDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (supplyDocumentDeliveryItem.DeliveryRow != null && !String.IsNullOrEmpty(supplyDocumentDeliveryItem.DeliveryRow.NameCompany))
                {
                    localeRowDelivery = supplyDocumentDeliveryItem.DeliveryRow;
                    SupplyDocumentDelivery.LocaleRow locale = new SupplyDocumentDelivery.LocaleRow();
                    if (localeRowDelivery.ID == 0)
                    {               
                        localeRowDelivery.TempID = supplyDocumentDelivery.Count() + 1;
                        supplyDocumentDelivery.Add(localeRowDelivery);
                    }
                    else
                    {
                        locale = supplyDocumentDelivery.FirstOrDefault(x => x.ID == localeRowDelivery.ID);
                        supplyDocumentDelivery.Remove(locale);
                        locale = localeRowDelivery;
                        locale.TempID = supplyDocumentDelivery.Count() + 1;
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
                        supplyDocumentPayment.Add(paymentLocalRow);
                    }
                    else
                    {
                        locale = supplyDocumentPayment.FirstOrDefault(x => x.ID == paymentLocalRow.ID);
                        supplyDocumentPayment.Remove(locale);                      
                        locale = paymentLocalRow;
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
                Document.MassCategoryID = "";
                Document.MassName = "";
                Document.MassDescription = "";
                //Подкатегория товара
                Document.MassCategoryDetailsID="";
                Document.MassIDCategory="";
                Document.MassCategoryDetailsName="";
                Document.MassCategoryDetailsDescription="";

                //Delivery
                Document.MassDeliveryID = "";
                Document.MassNameCompanyDelivery = "";
                Document.MassPhonesDelivery = "";
                Document.MassCountryDelivery = "";
                //DeliveryDetails
                Document.MassDeliveryDetailsID = "";
                Document.MassIDDelivery = "";
                Document.MassManagerName = "";
                Document.MassPhonesDeliveryDetails = "";

                //SupplyDocumentDetails
                Document.MassSupplyDocumentDetailsID = "";
                Document.MassSupplyDocumentDetailsName = "";
                Document.MassSupplyDocumentDetailsQuantity = "";
                Document.MassSupplyDocumentDetailsTagPriceUSA = "";
                Document.MassSupplyDocumentDetailsTagPriceRUS = "";
                Document.MassSupplyDocumentDetailsCategoryID = "";
                Document.MassSupplyDocumentDetailsCategoryDetailsID = "";
                Document.MassSupplyDocumentDetailsImageProduct = "";
                Document.MassSupplyDocumentDetailsModel = "";
                Document.MassSupplyDocumentDetailsSizeProduct = "";
                Document.MassSupplyDocumentDetailsSize = "";
                Document.MassSupplyDocumentDetailsBarCode = "";

                //SupplyDocumentDeliverry
                Document.MassSupplyDocumentDeliveryID="";
                Document.MassSupplyDocumentDeliveryDeliveryID="";
                Document.MassSupplyDocumentDeliveryDeliveryDetailsID="";
                Document.MassSupplyDocumentDeliveryTTN="";
                Document.MassSupplyDocumentDeliveryImageTTN="";
                Document.MassSupplyDocumentDeliveryInvoice="";
                Document.MassSupplyDocumentDeliveryImageInvoice="";
                Document.MassSupplyDocumentDeliveryAmountUSA = "";
                Document.MassSupplyDocumentDeliveryAmountRUS = "";

                //SupplyDocumentPayment
                Document.MassSupplyDocumentPaymentID="";
                Document.MassSupplyDocumentPaymentAmount="";
                Document.MassSupplyDocumentPaymentOperationType="";
                Document.MassSupplyDocumentPaymentDescription="";
                Document.MassSupplyDocumentPaymentStatus = "";
              
                //продукты
                foreach (SupplyDocumentDetails.LocaleRow currentrow in supplyDocumentDetails)
                {
                    Document.MassSupplyDocumentDetailsID = Document.MassSupplyDocumentDetailsID + currentrow.ID.ToString() + '|';
                    Document.MassSupplyDocumentDetailsName = Document.MassSupplyDocumentDetailsName + currentrow.Name.ToString() + '|';
                    Document.MassSupplyDocumentDetailsQuantity = Document.MassSupplyDocumentDetailsQuantity + currentrow.Quantity.ToString() + '|';
                    Document.MassSupplyDocumentDetailsTagPriceUSA = Document.MassSupplyDocumentDetailsTagPriceUSA + currentrow.TagPriceUSA.ToString() + '|';
                    Document.MassSupplyDocumentDetailsTagPriceRUS = Document.MassSupplyDocumentDetailsTagPriceRUS + currentrow.TagPriceRUS.ToString() + '|';
                    Document.MassSupplyDocumentDetailsCategoryID = Document.MassSupplyDocumentDetailsCategoryID + currentrow.CategoryID.ToString() + '|';
                    Document.MassSupplyDocumentDetailsCategoryDetailsID = Document.MassSupplyDocumentDetailsCategoryDetailsID + currentrow.CategoryDetailsID.ToString() + '|';
                    Document.MassSupplyDocumentDetailsModel = Document.MassSupplyDocumentDetailsModel + currentrow.Model+ '|';
                    Document.MassSupplyDocumentDetailsSizeProduct = Document.MassSupplyDocumentDetailsSizeProduct + currentrow.SizeProduct + '|';
                    Document.MassSupplyDocumentDetailsSize = Document.MassSupplyDocumentDetailsSize + currentrow.Package + '|';
                    //Document.MassSupplyDocumentDetailsImageProduct = Document.MassSupplyDocumentDetailsImageProduct + currentrow.ImageProduct.ToString() + '|'; 
                    Document.MassSupplyDocumentDetailsBarCode = Document.MassSupplyDocumentDetailsBarCode + currentrow.BarCodeString + '|';                  
                }

                //сопуствующие товары
                foreach (SupplyDocumentDelivery.LocaleRow currentrow in supplyDocumentDelivery)
                {
                    Document.MassSupplyDocumentDeliveryID = Document.MassSupplyDocumentDeliveryID + currentrow.ID.ToString() + '|';
                    Document.MassSupplyDocumentDeliveryDeliveryID = Document.MassSupplyDocumentDeliveryDeliveryID + currentrow.DeliveryID.ToString() + '|';
                    Document.MassSupplyDocumentDeliveryDeliveryDetailsID = Document.MassSupplyDocumentDeliveryDeliveryDetailsID + currentrow.DeliveryDetailsID.ToString() + '|';
                    Document.MassSupplyDocumentDeliveryTTN = Document.MassSupplyDocumentDeliveryTTN + currentrow.TTN.ToString() + '|';
                    //Document.MassSupplyDocumentDeliveryImageTTN = Document.MassSupplyDocumentDeliveryImageTTN + currentrow.Model.ToString() + '|';
                    Document.MassSupplyDocumentDeliveryInvoice = Document.MassSupplyDocumentDeliveryInvoice + currentrow.Invoice.ToString() + '|';
                    // Document.MassSupplyDocumentDeliveryImageInvoice = Document.MassSupplyDocumentDeliveryImageInvoice + currentrow.Model.ToString() + '|';   
                    Document.MassSupplyDocumentDeliveryAmountUSA = Document.MassSupplyDocumentDeliveryAmountUSA + currentrow.AmountUSA.ToString() + '|';
                    Document.MassSupplyDocumentDeliveryAmountRUS= Document.MassSupplyDocumentDeliveryAmountRUS + currentrow.AmountRUS.ToString() + '|';
                }              

                //виды оплат
                foreach (SupplyDocumentPayment.LocaleRow currentrow in supplyDocumentPayment)
                {
                    Document.MassSupplyDocumentPaymentID = Document.MassSupplyDocumentPaymentID + currentrow.ID.ToString() + '|';
                    Document.MassSupplyDocumentPaymentAmount = Document.MassSupplyDocumentPaymentAmount + currentrow.Amount.ToString() + '|';
                    Document.MassSupplyDocumentPaymentStatus = Document.MassSupplyDocumentPaymentStatus + currentrow.Status.ToString() + '|';
                    Document.MassSupplyDocumentPaymentOperationType = Document.MassSupplyDocumentPaymentOperationType + currentrow.OpertionType.ToString() + '|';
                    Document.MassSupplyDocumentPaymentRRN = Document.MassSupplyDocumentPaymentRRN+ currentrow.RRN.ToString() + '|';
                    Document.MassSupplyDocumentPaymentDescription = Document.MassSupplyDocumentPaymentDescription + currentrow.Description.ToString() + '|';                   
                }
                Document.Amount = summary.SummaryProductTagPriceRUS;
                Document.Count = summary.SummaryQuantityProduct;

                Document.ID = supplyDocumentLogic.SaveRow(Document);
                //UpdateCurrentDocument(Document.ID);
                //MainWindow.AppWindow.DataChanged[ToString()] = false;

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

        }

        private void SupplyDocumentDetailsToolBar_ButtonListCancel()
        {

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
            
            if (SummaryPaymentBalansTemp< SummaryPaymentRemainsTemp)
            {             
                IsPaymentAddButton = true;
            }
            else
            {
                IsPaymentAddButton = false;
            }

            summary.SummaryQuantityProduct = SummaryQuantityProductTemp;
            summary.SummaryProductTagPriceUSA = SummaryTagPriceWithUSATemp;
            summary.SummaryProductTagPriceRUS = SummaryTagPriceWithRUSTemp;

            summary.SummaryDeliveryQuantity = SummaryQuantityDeliveryTemp;
            summary.SummaryAmountUSA = SummaryAmountUSATemp;
            summary.SummaryAmountRUS = SummaryAmountRUSTemp;

            summary.SummaryPaymentBalans = SummaryPaymentBalansTemp < 0 ? Math.Abs(SummaryPaymentBalansTemp) : SummaryPaymentBalansTemp;
            summary.SummaryPaymentRemains = SummaryPaymentRemainsTemp < 0 ? Math.Abs(SummaryPaymentRemainsTemp) : SummaryPaymentRemainsTemp; 
            
            if (SummaryPaymentBalansTemp > SummaryPaymentRemainsTemp)
            {
                IsApplyDocument = true;
            }
          else
            {
                IsApplyDocument = false;
            }

        }


        #endregion

     
    }
}
