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

namespace Sklad_v1_001.FormUsers.Product
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
    /// Логика взаимодействия для ProductInGrid.xaml
    /// </summary>
    public partial class ProductInGrid : Page
    {
        Attributes attributes;

        LocalRow localRow;

        public LocalRow LocalRow
        {
            get
            {
                return localRow;
            }

            set
            {
                localRow = value;             
            }
        }

        public ProductInGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
        }

        private void RelatedProductDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        private void RelatedProductDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void FilterType_ButtonApplyClick(string text)
        {

        }

        private void FilterGender_ButtonApplyClick(string text)
        {

        }

        private void FilterSize_ButtonApplyClick(string text)
        {

        }

        private void FilterWeight_ButtonApplyClick()
        {

        }

        private void FilterTagPriceWithVAT_ButtonApplyClick()
        {

        }

        private void FilterSalePriceWithVAT_ButtonApplyClick()
        {

        }

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

        private void SearchFilter_ButtonClearClick()
        {

        }

        private void SearchFilter_ButtonTextChangedClick()
        {

        }

        private void ScrabToolbar_ButtonClearFiltersClick()
        {

        }
    }
}
