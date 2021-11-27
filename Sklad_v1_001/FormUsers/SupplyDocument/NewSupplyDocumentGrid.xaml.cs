using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.Product;
using Sklad_v1_001.FormUsers.SupplyDocumentPayment;
using Sklad_v1_001.GlobalList;
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
        //******************************

        //работаем с доставкой
        FlexMessageBox addDeliveryWindow;
        NewDeliveryItem newDeliveryItem;
        //******************************

        //работаем с оплатами
        FlexMessageBox addSuppluPaymentWindow;
        NewSupplyDocumentPaymentItem newSupplyDocumentPaymentItem;
        //******************************

        //остновной документ
        LocalRow document;

        //Продукт
        Product.LocaleRow localeRowProduct;
        ObservableCollection<Product.LocaleRow> detailsProduct;

        //доставка
        Delivery.LocaleRow localeRowDelivery;
        ObservableCollection<Delivery.LocaleRow> detailsDelivery;

        //оплата
        SupplyDocumentPayment.LocaleRow supplyDocumentPaymentLocaleRow;
        ObservableCollection<SupplyDocumentPayment.LocaleRow> detailsSupplyPayment;

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

            detailsProduct = new ObservableCollection<Product.LocaleRow>();
            detailsDelivery = new ObservableCollection<Delivery.LocaleRow>();
            detailsSupplyPayment = new ObservableCollection<SupplyDocumentPayment.LocaleRow>();

            Status = 0;

            SupplyTypeList supplyTypeList = new SupplyTypeList();
            this.StatusDocument.ComboBoxElement.ItemsSource = supplyTypeList.innerList;

            this.DataProduct.ItemsSource = detailsProduct;
            this.DataDelivery.ItemsSource = detailsDelivery;
            this.DataPayment.ItemsSource = detailsSupplyPayment;
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
            // newDeliveryItem.LocaleRow=
            //newAddProductItem.Status = Status;
            addProductWindow.Content = newAddProductItem;
            addProductWindow.Show(Properties.Resources.Products);
            if (newAddProductItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newAddProductItem.ProductLocalRow != null )
                {
                    localeRowProduct = newAddProductItem.ProductLocalRow;
                    detailsProduct.Add(localeRowProduct);
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
            localeRowDelivery = new Delivery.LocaleRow();
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
                    detailsDelivery.Add(localeRowDelivery);
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
            // newDeliveryItem.LocaleRow=
            // newSupplyDocumentPaymentItem.Status = Status;
            addSuppluPaymentWindow.Content = newSupplyDocumentPaymentItem;
            addSuppluPaymentWindow.Show(Properties.Resources.Payment1);
            if (newSupplyDocumentPaymentItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newSupplyDocumentPaymentItem.PaymentLocalRow != null)
                {
                    supplyDocumentPaymentLocaleRow = newSupplyDocumentPaymentItem.PaymentLocalRow;
                    detailsSupplyPayment.Add(supplyDocumentPaymentLocaleRow);
                }
            }
        }

        private void ToolBarPayment_ButtonDeleteClick()
        {

        }
        #endregion
    }
}
