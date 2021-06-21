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

namespace Sklad_v1_001.Control.ToolBar
{
    /// <summary>
    /// Логика взаимодействия для ToolBarZakupkaxaml.xaml
    /// </summary>
    public partial class SupplyToolBar : UserControl
    {
        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
           "IsEnableAdd",
           typeof(Boolean),
           typeof(SupplyToolBar), new UIPropertyMetadata(true));

        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }

        public event Action ButtonAdd;
        public event Action ButtonSave;
        public event Action ButtonEdit;
        public SupplyToolBar()
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonAdd?.Invoke();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonSave?.Invoke();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonEdit?.Invoke();
        }
    }
}
