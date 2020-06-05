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
using Sklad_v1_001.FormUsers.Tovar;
using System.Collections.ObjectModel;
using System.Data;

namespace Sklad_v1_001.FormUsers.Tovar
{
    /// <summary>
    /// Interaction logic for TovarZona.xaml
    /// </summary>
    public partial class TovarZona : UserControl
    {
        Tovar.LocalRow localrow;
        Tovar.TovarZonaLogic logicTovarZona;
        ObservableCollection<Tovar.LocalRow> dataProduct;
        public TovarZona()
        {
            InitializeComponent();
            dataProduct = new ObservableCollection<LocalRow>();

            logicTovarZona = new TovarZonaLogic();

            this.DataGrid.ItemsSource = dataProduct;
           
            //this.DataGrid.DataContext = localrow;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Refresh()
        {
            DataTable table = new DataTable();
            //получили данные
            table = logicTovarZona.Select();
            //заполнили данные
            foreach(DataRow row in table.Rows)
            {
                dataProduct.Add(logicTovarZona.Convert(row, new LocalRow()));
            }
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToolBarNextToBack_ButtonBack()
        {

        }

        private void ToolBarNextToBack_ButtonNext()
        {

        }
    }
}
