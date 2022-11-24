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
    /// Логика взаимодействия для ShopsGrid.xaml
    /// </summary>
    public partial class ShopsGrid : Page
    {
        Attributes attributes;
        public ShopsGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
        }
    }
}
