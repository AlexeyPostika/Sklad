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

        ObservableCollection<BasketShop.LocalRow> datalistBasketShop;

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

        public ObservableCollection<BasketShop.LocalRow> DatalistBasketShop
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
                    foreach(BasketShop.LocalRow basket in datalistBasketShop)
                    {

                    }
                }
            }
        }

        public NewSaleDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            LocalFilterDocument = new LocalFilter();

            UserIDDocument.ComboBoxElement.ItemsSource = attributes.datalistUsers;
            
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
