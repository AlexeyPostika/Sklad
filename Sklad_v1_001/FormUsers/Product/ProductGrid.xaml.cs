using Sklad_v1_001.Control.Contener;
using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Sklad_v1_001.FormUsers.Product
{
    /// <summary>
    /// Логика взаимодействия для ProductGrid.xaml
    /// </summary>
    public partial class ProductGrid : Page
    {
        Attributes attributes;

        ProductLogic productLogic;

        LocalFilter localFilter;

        ObservableCollection<LocalRow> datalist;

        public ProductGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            productLogic = new ProductLogic();

            localFilter = new LocalFilter();

            datalist = new ObservableCollection<LocalRow>();           

        }

        private void AddVisibilityControl(Panel PanelToAdd, Object VisibilityRow, UIElement ElementValue)
        {
            Boolean visibility = false;
            Boolean.TryParse(VisibilityRow.ToString(), out visibility);
            if (visibility == true)
                PanelToAdd.Children.Add(ElementValue);           
            
        }

        private void Refresh()
        {
            DataTable dataTable = productLogic.FillGrid(localFilter);
            foreach(DataRow row in dataTable.Rows)
            {
                LocalRow localRow = new LocalRow();
                datalist.Add(productLogic.Convert(row, localRow));

                ContenerRowDescription contenerRowDescription = new ContenerRowDescription();
                contenerRowDescription.KeyRow = localRow.ID.ToString(); //ключ для строки
                contenerRowDescription.ButtonAddClick += ContenerRowDescription_ButtonAddClick;
                contenerRowDescription.ButtonEditClick += ContenerRowDescription_ButtonEditClick;

                contenerRowDescription.PhotoImage = localRow.PhotoImage;
                contenerRowDescription.TextValue1 = localRow.Name;
                contenerRowDescription.TextValue2 = "Описание: " + localRow.Description;

                contenerRowDescription.TextEdit1 = "Категория товара: " + localRow.CategoryName;
                contenerRowDescription.TextEdit2 = "Подкатегория товара: " + localRow.CategoryDetailsName;
                contenerRowDescription.TextEdit3 = "Витрина: " + localRow.ShowcaseIDName;
                contenerRowDescription.TextEdit4 = "Размер упаковки: " + localRow.SizeProduct;
                contenerRowDescription.TextEdit5 = "Штрих-код: " + localRow.BarCodeString;
                contenerRowDescription.Description6.Visibility = Visibility.Collapsed;
                contenerRowDescription.Description7.Visibility = Visibility.Collapsed;

                contenerRowDescription.TextCount1 = "Количество на складе: "+localRow.Quantity.ToString();
                contenerRowDescription.TagPriceRUS = localRow.TagPriceRUS;
                AddVisibilityControl(Column1, true, contenerRowDescription);
            }

        }

        private void ContenerRowDescription_ButtonEditClick(object sender, RoutedEventArgs e)
        {
            ContenerRowDescription contenerRowDescription = sender as ContenerRowDescription;
            if (contenerRowDescription != null)
            {
                String rowID = datalist.FirstOrDefault(x => x.ID.ToString() == contenerRowDescription.KeyRow) != null ? datalist.FirstOrDefault(x => x.ID.ToString() == contenerRowDescription.KeyRow).ID.ToString() : String.Empty;
                if (rowID != "")
                {

                }
            }
        }

        private void ContenerRowDescription_ButtonAddClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            localFilter.PageNumber = (Int32)(page.ActualHeight) / 210;
            Refresh();
        }

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
