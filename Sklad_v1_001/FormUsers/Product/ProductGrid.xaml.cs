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
    /// <summary>
    /// Логика взаимодействия для ProductGrid.xaml
    /// </summary>
    public partial class ProductGrid : Page
    {
        public ProductGrid(Attributes _attributes)
        {
            InitializeComponent();
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
           
            
        }
    }
}
