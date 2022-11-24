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

namespace Sklad_v1_001.FormUsers.Shops
{
    /// <summary>
    /// Логика взаимодействия для NewShopsItem.xaml
    /// </summary>   
    public partial class NewShopsItem : Page
    {
        Attributes attributes;
        public NewShopsItem(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
        }

        private void checkBoxShop_ButtonCheckedClick(object sender, RoutedEventArgs e)
        {

        }

        private void checkBoxShop_ButtonUnCheckedClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
