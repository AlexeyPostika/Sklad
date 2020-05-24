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

namespace Sklad_v1_001.FormUsers.Tovar
{
    /// <summary>
    /// Interaction logic for TovarZona.xaml
    /// </summary>
    public partial class TovarZona : UserControl
    {
        Tovar.LocalRow localrow;
        Tovar.TovarZonaLogic logic;
        public TovarZona()
        {
            InitializeComponent();
            logic = new TovarZonaLogic();
            logic.Select();
            localrow = new LocalRow();
            this.DataGrid.DataContext = localrow;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
