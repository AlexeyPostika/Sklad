using Sklad_v1_001.Control.FlexMessageBox;
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

        LocalRow document;

        public LocalRow Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
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
        public NewSupplyDocumentGrid()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            
        }
       
        #region Продукт
        private void ToolBarProduct_ButtonNewProductClick()
        {
            MainWindow.AppWindow.ButtonNewAddProduct();
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
            MainWindow.AppWindow.ButtonNewDelivery();
        }

        private void ToolBarDelivery_ButtonDeleteClick()
        {

        }

        private void ToolBarPayment_ButtonNewProductClick()
        {

        }

        private void ToolBarPayment_ButtonDeleteClick()
        {

        }
        #endregion
    }
}
