using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal.StoreAPI;
using System;
using System.Collections.Generic;
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

        public RegisterDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
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
