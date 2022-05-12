using Sklad_v1_001.GlobalAttributes;
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

namespace Sklad_v1_001.FormUsers.SaleDocument
{
    /// <summary>
    /// Логика взаимодействия для SaleDocumentGrid.xaml
    /// </summary>
    public partial class SaleDocumentGrid : Page
    {
        Attributes attributes;

        public SaleDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
        }

        #region DataGrid SaleDocument
        private void saleDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void saleDocument_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void saleDocument_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Filters
        private void FilterAmount_ButtonApplyClick()
        {

        }
        private void FilterUserID_ButtonApplyClick(string text)
        {

        }
        private void FilterQuantity_ButtonApplyClick()
        {

        }


        #endregion

        #region TooBar
        private void ToolBarSaleDocument_ButtonAdd()
        {

        }

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
        #endregion

        #region нижняя грида - детали документа
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

       
    }
}
