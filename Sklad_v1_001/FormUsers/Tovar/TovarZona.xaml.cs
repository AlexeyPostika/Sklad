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
            table = logicTovarZona.Select();
            foreach(DataRow row in table.Rows)
            {
                localrow = new LocalRow();
                localrow.ID = Int32.Parse(row["ID"].ToString());
                localrow.Name = row["Name"].ToString();
                localrow.TypeProduct = row["TypeDescription"].ToString();
                localrow.Cena = Int32.Parse(row["Cena"].ToString());
                localrow.Vetrina = Int32.Parse(row["IDVetrina"].ToString());
                localrow.PhotoImage = @"..\..\Icone\tovar\picture_80px.png";
                dataProduct.Add(localrow);
            }
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
