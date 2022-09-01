using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal.StoreAPI;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocument;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentDelivery;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentDetails;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentPayment;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
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

namespace Sklad_v1_001.FormUsers.RegisterDocument
{
    /// <summary>
    /// Логика взаимодействия для RegisterDocumentGrid.xaml
    /// </summary>
    public partial class RegisterDocumentGrid : Page
    {
        Attributes attributes;
        //схема структуры регистрации документов
        ShemaStorаge shemaStore;

        public RegisterDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
            shemaStore = new ShemaStorаge();
        }

        #region Toolbar верхний
        private void ToolBarSaleDocument_ButtonEdit()
        {

        }

        private void ToolBarSaleDocument_ButtonDelete()
        {

        }

        private void ToolBarSaleDocument_ButtonClear()
        {

        }

        private void ToolBarSaleDocument_ButtonScan(string text)
        {

        }

        private void ToolBarSaleDocument_ButtonClean()
        {

        }
        private void ToolBarSaleDocument_ButtonRefresh()
        {           
            Request request = new Request(attributes);
            request.supplyDocument.Document.Status = 6;//затягиваем документы, которые нужно подтвердить
            Response response = request.GetCommand(2);
            if (response != null)
            {                
                foreach (SupplyDocumentRequest rowResponse in response.SupplyDocumentListOutput.ListDocuments)
                {
                    DataRow row = shemaStore.RegisterDocument.NewRow();
                    row["Count"] = rowResponse.Document.Count;
                    row["Amount"] = rowResponse.Document.Amount;
                    row["ReffID"] = rowResponse.Document.ID;
                   // row["ReffDate"] = null;
                    //row["RegisterDocumentNumber"] = null;
                    row["SupplyDocumentNumber"] = rowResponse.Document.SupplyDocumentNumber;                   
                    row["CreatedDate"] = rowResponse.Document.CreatedDate == null ? DateTime.Now : rowResponse.Document.CreatedDate;
                    row["CreatedUserID"] = rowResponse.Document.CreatedUserID;
                    row["LastModificatedDate"] = DateTime.Now;
                    row["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                    row["Status"] = rowResponse.Document.Status == 6 ? 0 : rowResponse.Document.Status;
                    row["ShopID"] = rowResponse.Document.ShopID;
                    row["CompanyId"] = rowResponse.Document.CompanyID;
                    row["ReffTimeRow"] = rowResponse.Document.TimeRow;
                    shemaStore.RegisterDocument.Rows.Add(row);

                    foreach (SupplyDocumentDeliveryRequest rowDeliveryReff in rowResponse.Delivery)
                    {
                        DataRow rowDelivery = shemaStore.RegisterDocumentDelivery.NewRow();
                        rowDelivery["DocumentID"] = rowDeliveryReff.DocumentID;
                        rowDelivery["DeliveryID"] = rowDeliveryReff.DeliveryID;
                        rowDelivery["DeliveryDetailsID"] = rowDeliveryReff.DeliveryDetailsID;
                        rowDelivery["DeliveryTTN"] = "";
                        if (rowDeliveryReff.ImageTTN != null)
                            rowDelivery["ImageTTN"] = rowDeliveryReff.ImageTTN;
                        rowDelivery["Invoice"] = rowDeliveryReff.Invoice;
                        if (rowDeliveryReff.ImageInvoice != null)
                            rowDelivery["ImageInvoice"] = rowDeliveryReff.ImageInvoice;
                        rowDelivery["AmountUSA"] = rowDeliveryReff.AmountUSA;
                        rowDelivery["AmountRUS"] = rowDeliveryReff.AmountRUS;
                        rowDelivery["Description"] = rowDeliveryReff.Description;
                        rowDelivery["DeliveryTTN"] = rowDeliveryReff.DeliveryTTN;
                        rowDelivery["CreatedDate"] = rowDeliveryReff.CreatedDate == null ? DateTime.Now : rowDeliveryReff.CreatedDate;
                        rowDelivery["CreatedUserID"] = rowDeliveryReff.CreatedUserID;
                        rowDelivery["LastModificatedDate"] = DateTime.Now;
                        rowDelivery["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                        shemaStore.RegisterDocumentDelivery.Rows.Add(rowDelivery);
                    }
                    foreach (SupplyDocumentDetailsRequest rowDetailsReff in rowResponse.Details)
                    {
                        DataRow rowDetails = shemaStore.RegisterDocumentDetails.NewRow();
                        rowDetails["DocumentID"] = rowDetailsReff.DocumentID;
                        rowDetails["Name"] = rowDetailsReff.Name;
                        rowDetails["Quantity"] = rowDetailsReff.Quantity;
                        rowDetails["TagPriceUSA"] = rowDetailsReff.TagPriceUSA;
                        rowDetails["TagPriceRUS"] = rowDetailsReff.TagPriceRUS;
                        rowDetails["CategoryID"] = rowDetailsReff.CategoryID;
                        rowDetails["CategoryDetailsID"] = rowDetailsReff.CategoryDetailsID;
                        if (rowDetailsReff.ImageProduct != null)
                            rowDetails["ImageProduct"] = rowDetailsReff.ImageProduct;
                        rowDetails["Barecodes"] = rowDetailsReff.BarcodesShop;
                        rowDetails["CreatedDate"] = rowDetailsReff.CreatedDate == null ? DateTime.Now : rowDetailsReff.CreatedDate;
                        rowDetails["CreatedUserID"] = rowDetailsReff.CreatedUserID;
                        rowDetails["LastModificatedDate"] = DateTime.Now;
                        rowDetails["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                        rowDetails["Model"] = rowDetailsReff.Model;
                        rowDetails["SizeProduct"] = rowDetailsReff.SizeProduct;
                        rowDetails["Size"] = rowDetailsReff.Size;                        
                        shemaStore.RegisterDocumentDetails.Rows.Add(rowDetails);
                    }
                    foreach (SupplyDocumentPaymentRequest rowPaymentReff in rowResponse.Payment)
                    {
                        DataRow rowPayment = shemaStore.RegisterDocumentPayment.NewRow();
                        rowPayment["DocumentID"] = rowPaymentReff.DocumentID;
                        rowPayment["Status"] = rowPaymentReff.Status;
                        rowPayment["OperationType"] = rowPaymentReff.OperationType;
                        rowPayment["Amount"] = rowPaymentReff.Amount;
                        rowPayment["Description"] = rowPaymentReff.Description;
                        rowPayment["RRN"] = rowPaymentReff.RRN;
                        rowPayment["CreatedDate"] = rowPaymentReff.CreatedDate == null ? DateTime.Now : rowPaymentReff.CreatedDate;
                        rowPayment["CreatedUserID"] = rowPaymentReff.CreatedUserID;
                        rowPayment["LastModificatedDate"] = DateTime.Now;
                        rowPayment["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                        shemaStore.RegisterDocumentPayment.Rows.Add(rowPayment);
                    }
                }
            }
        }

        #endregion

        #region DataGrid список документов
        private void saleDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void saleDocument_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void saleDocument_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region RegisterDetails
        private void DataProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DataPayment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion

        #region Paginator

        private void ToolBarNextToBack_ButtonBack()
        {

        }
        private void ToolBarNextToBack_ButtonNext()
        {

        }
        private void ToolbarNextPageData_ButtonBackIn()
        {

        }

        private void ToolbarNextPageData_ButtonNextEnd()
        {

        }

        #endregion

        #region Filters
        private void FilterShopID_ButtonApplyClick(string text)
        {

        }

        private void FilterQuantity_ButtonApplyClick()
        {

        }

        private void FilterAmount_ButtonApplyClick()
        {

        }

        private void filterIdUserInput_ButtonApplyClick(string text)
        {

        }

        private void FilterUserID_ButtonApplyClick(string text)
        {

        }

        #endregion

       
    }
}
