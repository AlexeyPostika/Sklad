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
                //DocumentID = Document.ID;
                //this.StackPanelSummary.DataContext = Document;

                //switch (Document.Status)
                //{
                //    case 0:
                //        DocumentToolbar.ButtonApply.Visibility = Visibility.Visible;
                //        DocumentToolbar.ButtonPrintLabels.IsEnabled = false;
                //        break;
                //    case 1:
                //    case 2:
                //        DocumentToolbar.ButtonApply.Visibility = Visibility.Visible;
                //        DocumentToolbar.ButtonPrintLabels.IsEnabled = true;
                //        break;
                //}
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
        }

        private void Refresh()
        {
            DataTable dataTableSupplyDocumentDetails = supplyDocumentDetailsLogic.FillGrid(Document.ID);
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
                supplyDocumentDetails.Add(supplyDocumentDetailsLogic.Convert(row, new SupplyDocumentDetails.LocaleRow()));
            }

            CalculateSummary();
        }
       
        #region Продукт
        private void ToolBarProduct_ButtonNewProductClick()
        {
            //MainWindow.AppWindow.ButtonNewAddProduct();           
            newAddProductItem = new NewAddProductItem(attributes);
            addProductWindow = new FlexMessageBox();           
            addProductWindow.Content = newAddProductItem;
            addProductWindow.Show(Properties.Resources.Products);
            if (newAddProductItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newAddProductItem.ProductLocalRow != null )
                {                           
                    localeRowProduct = newAddProductItem.ProductLocalRow;
                    SupplyDocumentDetails.LocaleRow locale = new SupplyDocumentDetails.LocaleRow();
                    locale.TempID = supplyDocumentDetails.Count() + 1;
                    supplyDocumentDetails.Add(supplyDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, locale));
                }
            }
            CalculateSummary();
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
        private void ToolBarDelivery_ButtonNewProductClick()
        {
            SupplyDocumentDelivery.LocaleRow localeRowDelivery = new SupplyDocumentDelivery.LocaleRow();
            supplyDocumentDeliveryItem = new SupplyDocumentDeliveryItem(attributes);          
            addDeliveryWindow = new FlexMessageBox();
            // newDeliveryItem.LocaleRow=
            supplyDocumentDeliveryItem.Status = Status;
            addDeliveryWindow.Content = supplyDocumentDeliveryItem;
            addDeliveryWindow.Show(Properties.Resources.Deliveries);
            if (supplyDocumentDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (supplyDocumentDeliveryItem.DeliveryRow != null && !String.IsNullOrEmpty(supplyDocumentDeliveryItem.DeliveryRow.NameCompany))
                {                             
                    localeRowDelivery = supplyDocumentDeliveryItem.DeliveryRow;

                    SupplyDocumentDelivery.LocaleRow locale = new SupplyDocumentDelivery.LocaleRow();                          
                    localeRowDelivery.ID = 0;
                    locale.TempID = supplyDocumentDelivery.Count() + 1;
                    supplyDocumentDelivery.Add(localeRowDelivery);
                }
            }
            CalculateSummary();
        }

        private void ToolBarDelivery_ButtonDeleteClick()
        {
            CalculateSummary();
        }

        #endregion

        #region оплата
        private void ToolBarPayment_ButtonNewProductClick()
        {
            supplyDocumentPaymentLocaleRow = new SupplyDocumentPayment.LocaleRow();
            newSupplyDocumentPaymentItem = new NewSupplyDocumentPaymentItem();
            addSuppluPaymentWindow = new FlexMessageBox();
            newSupplyDocumentPaymentItem.AmountMax = (Double)summary.SummaryPaymentRemains;
            addSuppluPaymentWindow.Content = newSupplyDocumentPaymentItem;

            addSuppluPaymentWindow.Show(Properties.Resources.Payment1);
            if (newSupplyDocumentPaymentItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newSupplyDocumentPaymentItem.PaymentLocalRow != null)
                {                       
                    supplyDocumentPaymentLocaleRow = newSupplyDocumentPaymentItem.PaymentLocalRow;                  
                    supplyDocumentPayment.Add(supplyDocumentPaymentLocaleRow);
                }
            }
            CalculateSummary();
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
                    //Document.MassSupplyDocumentDetailsImageProduct = Document.MassSupplyDocumentDetailsImageProduct + currentrow.ImageProduct.ToString() + '|';                
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
                }
                else              
                    SummaryPaymentRemainsTemp = SummaryPaymentRemainsTemp + row.Amount;
            }
            
            if (SummaryPaymentBalansTemp< SummaryPaymentRemainsTemp)
            {
                SummaryPaymentRemainsTemp -= SummaryPaymentBalansTemp;
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
