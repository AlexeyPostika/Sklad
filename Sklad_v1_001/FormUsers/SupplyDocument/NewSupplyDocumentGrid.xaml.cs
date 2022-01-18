using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.Product;
using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
using Sklad_v1_001.FormUsers.SupplyDocumentDetails;
using Sklad_v1_001.FormUsers.SupplyDocumentPayment;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        NewDeliveryItem newDeliveryItem;
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
        public NewSupplyDocumentGrid()
        {
            InitializeComponent();

            supplyDocumentLogic = new SupplyDocumentLogic();
            supplyDocumentDetailsLogic = new SupplyDocumentDetailsLogic();
            supplyDocumentDeliveryLogic = new SupplyDocumentDeliveryLogic();
            supplyDocumentPaymentLogic = new SupplyDocumentPaymentLogic();

            //detailsProduct = new ObservableCollection<Product.LocaleRow>();

            supplyDocumentDetails = new ObservableCollection<SupplyDocumentDetails.LocaleRow>();
            supplyDocumentDelivery = new ObservableCollection<SupplyDocumentDelivery.LocaleRow>();
            supplyDocumentPayment = new ObservableCollection<SupplyDocumentPayment.LocaleRow>();

            dataListCategory = new ObservableCollection<Category.LocalRow>();
            dataListDelivery = new ObservableCollection<Delivery.LocaleRow>();
            dataListDeliveryDetails = new ObservableCollection<DeliveryDetails.LocaleRow>();

            Status = 0;

            SupplyTypeList supplyTypeList = new SupplyTypeList();
            this.StatusDocument.ComboBoxElement.ItemsSource = supplyTypeList.innerList;

            this.DataProduct.ItemsSource = supplyDocumentDetails;
            this.DataDelivery.ItemsSource = supplyDocumentDelivery;
            this.DataPayment.ItemsSource = supplyDocumentPayment;

            this.ToolBarDelivery.ButtonNewProduct.Text = Properties.Resources.ADD;
            this.ToolBarPayment.ButtonNewProduct.Text = Properties.Resources.ADD;
        }

        private void Refresh()
        {
            
        }
       
        #region Продукт
        private void ToolBarProduct_ButtonNewProductClick()
        {
            //MainWindow.AppWindow.ButtonNewAddProduct();           
            newAddProductItem = new NewAddProductItem();
            addProductWindow = new FlexMessageBox();           
            addProductWindow.Content = newAddProductItem;
            addProductWindow.Show(Properties.Resources.Products);
            if (newAddProductItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newAddProductItem.ProductLocalRow != null )
                {                           
                    localeRowProduct = newAddProductItem.ProductLocalRow;                                    
                    supplyDocumentDetails.Add(supplyDocumentDetailsLogic.ConvertProductToSupplyDocumentDetails(localeRowProduct, new SupplyDocumentDetails.LocaleRow()));
                }
            }
        }

        private void ToolBarProduct_ButtonDeleteClick()
        {

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
            Delivery.LocaleRow localeRowDelivery = new Delivery.LocaleRow();
            newDeliveryItem = new NewDeliveryItem();
            addDeliveryWindow = new FlexMessageBox();
            // newDeliveryItem.LocaleRow=
            newDeliveryItem.Status = Status;
            addDeliveryWindow.Content = newDeliveryItem;
            addDeliveryWindow.Show(Properties.Resources.Deliveries);
            if (newDeliveryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newDeliveryItem.Document != null && !String.IsNullOrEmpty(newDeliveryItem.Document.NameCompany))
                {                             
                    localeRowDelivery = newDeliveryItem.Document;                  
                    supplyDocumentDelivery.Add(supplyDocumentDeliveryLogic.ConvertDeliveryToSupplyDocumentDelivery(localeRowDelivery, new SupplyDocumentDelivery.LocaleRow()));
                }
            }
        }

        private void ToolBarDelivery_ButtonDeleteClick()
        {

        }

        #endregion

        #region оплата
        private void ToolBarPayment_ButtonNewProductClick()
        {
            supplyDocumentPaymentLocaleRow = new SupplyDocumentPayment.LocaleRow();
            newSupplyDocumentPaymentItem = new NewSupplyDocumentPaymentItem();
            addSuppluPaymentWindow = new FlexMessageBox();      
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
        }

        private void ToolBarPayment_ButtonDeleteClick()
        {

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
            /*
             *  //SupplyDocument
            _sqlRequestSave.SetParametrValue("@p_UserID", row.UserID);
            _sqlRequestSave.SetParametrValue("@p_ID", row.ID);
            _sqlRequestSave.SetParametrValue("@p_Status", row.Status);
            _sqlRequestSave.SetParametrValue("@p_Count", row.Count);
            _sqlRequestSave.SetParametrValue("@p_Amount", row.Amount);
            _sqlRequestSave.SetParametrValue("@p_ReffID", row.ReffID);
            _sqlRequestSave.SetParametrValue("@p_ReffDate", row.ReffDate);
            _sqlRequestSave.SetParametrValue("@p_SupplyDocumentNumber", row.SupplyDocumentNumber);

            //Категория товара
            _sqlRequestSave.SetParametrValue("@p_MassCategoryID", row.);
            _sqlRequestSave.SetParametrValue("@p_MassName", row.);
            _sqlRequestSave.SetParametrValue("@p_MassDescription", row.);
            
             */

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
                Document.MassSupplyDocumentDetailsImageProduct = new byte[] { };

                //SupplyDocumentDeliverry
                Document.MassSupplyDocumentDeliveryID="";
                Document.MassSupplyDocumentDeliveryDeliveryID="";
                Document.MassSupplyDocumentDeliveryDeliveryDetailsID="";
                Document.MassSupplyDocumentDeliveryTTN="";
                Document.MassSupplyDocumentDeliveryImageTTN="";
                Document.MassSupplyDocumentDeliveryInvoice="";
                Document.MassSupplyDocumentDeliveryImageInvoice="";

                //SupplyDocumentPayment
                Document.MassSupplyDocumentPaymentID="";
                Document.MassSupplyDocumentPaymentAmount="";
                Document.MassSupplyDocumentPaymentOperationType="";
                Document.MassSupplyDocumentPaymentDescription="";

                ////категория
                //foreach (Category.LocalRow currentrow in dataListCategory)
                //{
                //    Document.MassCategoryID = Document.MassCategoryID + currentrow.ID.ToString() + '|';
                //    Document.MassName = Document.MassName + currentrow.CategoryName.ToString() + '|';
                //    Document.MassDescription = Document.MassDescription + currentrow.Description.ToString() + '|';
                //    //подкатегория
                //    Document.MassCategoryDetailsID = Document.MassCategoryDetailsID + currentrow.CategoryDetailsID.ToString() + '|';
                //    Document.MassIDCategory = Document.MassIDCategory + currentrow.ID.ToString() + '|';
                //    Document.MassCategoryDetailsName = Document.MassCategoryDetailsName + currentrow.CategoryDetailsName.ToString() + '|';
                //    Document.MassCategoryDetailsDescription = Document.MassCategoryDetailsDescription + currentrow.CategoryDetailsDescription.ToString() + '|';
                //}
               
                ////компания доставка
                //foreach (Delivery.LocaleRow currentrow in dataListDelivery)
                //{
                //    Document.MassDeliveryID = Document.MassDeliveryID + currentrow.ID.ToString() + '|';
                //    Document.MassNameCompanyDelivery = Document.MassNameCompanyDelivery + currentrow.NameCompany.ToString() + '|';
                //    Document.MassPhonesDelivery = Document.MassPhonesDelivery + currentrow.PhonesCompany.ToString() + '|';
                //    Document.MassCountryDelivery = Document.MassCountryDelivery + "0" + '|';
                //    //детали
                //    Document.MassDeliveryDetailsID = Document.MassDeliveryDetailsID + currentrow.DetailsID.ToString() + '|';
                //    Document.MassIDDelivery = Document.MassIDDelivery + currentrow.ID.ToString() + '|';
                //    Document.MassManagerName = Document.MassManagerName + currentrow.ManagerName.ToString() + '|';
                //    Document.MassPhonesDeliveryDetails = Document.MassPhonesDeliveryDetails + currentrow.PhonesManager.ToString() + '|';
                //}

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
                }              

                //виды оплат
                foreach (SupplyDocumentPayment.LocaleRow currentrow in supplyDocumentPayment)
                {
                    Document.MassSupplyDocumentPaymentID = Document.MassSupplyDocumentPaymentID + currentrow.ID.ToString() + '|';
                    Document.MassSupplyDocumentPaymentAmount = Document.MassSupplyDocumentPaymentAmount + currentrow.ID.ToString() + '|';
                    Document.MassSupplyDocumentPaymentOperationType = Document.MassSupplyDocumentPaymentOperationType + currentrow.ID.ToString() + '|';
                    Document.MassSupplyDocumentPaymentDescription = Document.MassSupplyDocumentPaymentDescription + currentrow.ID.ToString() + '|';                   
                }

                //Document.ID = supplyDocumentLogic.SaveRow(Document);
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
    }
}
