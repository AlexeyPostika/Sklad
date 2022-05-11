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
    /// Логика взаимодействия для NewSaleDocument.xaml
    /// </summary>
    public partial class NewSaleDocument : Page
    {
        public static readonly DependencyProperty IsPaymentAddButtonProperty = DependencyProperty.Register(
                  "IsPaymentAddButton",
                  typeof(Boolean),
                 typeof(NewSaleDocument), new PropertyMetadata(true));

        public Boolean IsPaymentAddButton
        {
            get { return (Boolean)GetValue(IsPaymentAddButtonProperty); }
            set { SetValue(IsPaymentAddButtonProperty, value); }
        }

        public NewSaleDocument()
        {
            InitializeComponent();
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
    }
}
