using Sklad_v1_001.GlobalAttributes;
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

namespace Sklad_v1_001.FormUsers.SaleDocument
{
    /// <summary>
    /// Логика взаимодействия для NewSaleDocument.xaml
    /// </summary>
    public partial class NewSaleDocumentGrid : Page
    {

        public static readonly DependencyProperty IsPaymentAddButtonProperty = DependencyProperty.Register(
                  "IsPaymentAddButton",
                  typeof(Boolean),
                 typeof(NewSaleDocumentGrid), new PropertyMetadata(true));

        public Boolean IsPaymentAddButton
        {
            get { return (Boolean)GetValue(IsPaymentAddButtonProperty); }
            set { SetValue(IsPaymentAddButtonProperty, value); }
        }
        Attributes attributes;

        LocalFilter localFilterDocument;
        LocalRow document;

        ObservableCollection<SaleDocumentProduct.LocalRow> datalistBasketShop;

        RowSummary summary;

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

        public LocalFilter LocalFilterDocument
        {
            get
            {
                return localFilterDocument;
            }

            set
            {
                localFilterDocument = value;
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
                    Document.UserID = attributes.numeric.userEdit.AddUserID;
                }

                switch (Document.Status)
                {
                    case 0:
                        SupplyDocumentDetailsToolBar.VisibilityApply = Visibility.Visible;
                        SupplyDocumentDetailsToolBar.BottonApplyb.IsEnabled = false;
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
                if (document.ID > 0)
                    Refresh();
            }
        }

        public ObservableCollection<SaleDocumentProduct.LocalRow> DatalistBasketShop
        {
            get
            {
                return datalistBasketShop;
            }

            set
            {         
                if (value.Count > 0)
                {
                    datalistBasketShop = value;

                    Document.UserID = attributes.numeric.userEdit.AddUserID;
                    this.DataProduct.ItemsSource = DatalistBasketShop;
                    status = 0;
                    CalculateSummary();
                    //foreach(SaleDocumentProduct.LocalRow basket in datalistBasketShop)
                    //{

                    //}
                }
            }
        }

        public NewSaleDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            LocalFilterDocument = new LocalFilter();
            Document = new LocalRow();

            summary = new RowSummary();

            this.DataContext = Document;

            UserIDDocument.ComboBoxElement.ItemsSource = attributes.datalistUsers;
            this.DocumentSummary.DataContext = summary;

        }

        private void Refresh()
        {
            //DataTable dataTableSupplyDocumentDetails = supplyDocumentDetailsLogic.FillGridDocument(Document.ID);
            //foreach (DataRow row in dataTableSupplyDocumentDetails.Rows)
            //{
            //    supplyDocumentDetails.Add(supplyDocumentDetailsLogic.Convert(row, new SupplyDocumentDetails.LocaleRow()));
            //}

            //DataTable dataTableSupplyDocumentDelivery = supplyDocumentDeliveryLogic.FillGrid(Document.ID);
            //foreach (DataRow row in dataTableSupplyDocumentDelivery.Rows)
            //{
            //    supplyDocumentDelivery.Add(supplyDocumentDeliveryLogic.Convert(row, new SupplyDocumentDelivery.LocaleRow()));
            //}

            //DataTable dataTableSupplyDocumentPayment = supplyDocumentPaymentLogic.FillGrid(Document.ID);
            //foreach (DataRow row in dataTableSupplyDocumentPayment.Rows)
            //{
            //    supplyDocumentPayment.Add(supplyDocumentPaymentLogic.Convert(row, new SupplyDocumentPayment.LocaleRow()));
            //}

            CalculateSummary();
        }

        #region AddProduct
        private void ToolBarProduct_ButtonNewProductClick()
        {

        }

        private void ToolBarProduct_ButtonDeleteClick()
        {

        }
        private void DataProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion

        #region Payment
        private void ToolBarPayment_ButtonNewProductClick()
        {

        }

        private void ToolBarPayment_ButtonDeleteClick()
        {

        }

        private void DataPayment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

            foreach (SaleDocumentProduct.LocalRow row in DatalistBasketShop)
            {
                SummaryQuantityProductTemp = SummaryQuantityProductTemp + row.Quantity;
               // SummaryTagPriceWithUSATemp = SummaryTagPriceWithUSATemp + row.TagPriceUSA;
                SummaryTagPriceWithRUSTemp = SummaryTagPriceWithRUSTemp + row.TagPriceWithVAT;
            }

            //SummaryPaymentRemainsTemp = SummaryAmountRUSTemp + SummaryTagPriceWithRUSTemp;

            //foreach (SupplyDocumentPayment.LocaleRow row in supplyDocumentPayment)
            //{
            //    if (row.Status == 1)
            //    {
            //        SummaryPaymentBalansTemp = SummaryPaymentBalansTemp + row.Amount;
            //        SummaryPaymentRemainsTemp = SummaryPaymentRemainsTemp - row.Amount;
            //    }

            //}


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
                    ToolBarProduct.ButtonNewProduct.IsEnabled = true;
                    ToolBarProduct.ButtonDelete.IsEnabled = true;
                    ToolBarPayment.ButtonDelete.IsEnabled = true;
                    break;
                case 1:
                    IsPaymentAddButton = false;
                  
                    ToolBarProduct.ButtonNewProduct.IsEnabled = false;
                    ToolBarProduct.ButtonDelete.IsEnabled = false;                   
                    ToolBarPayment.ButtonDelete.IsEnabled = false;
                    break;
            }


        }


        #endregion

        #region ToolBar
        private void SupplyDocumentDetailsToolBar_ButtonSave()
        {

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
