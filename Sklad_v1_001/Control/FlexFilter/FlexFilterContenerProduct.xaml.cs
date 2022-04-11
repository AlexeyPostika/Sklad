using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sklad_v1_001.Control.FlexFilter
{
    /// <summary>
    /// Логика взаимодействия для FlexFilterContenerProduct.xaml
    /// </summary>
    public partial class FlexFilterContenerProduct : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                      "Value",
                       typeof(String),
                       typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));

        public String Value
        {
            get
            {
                return (String)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
                OnPropertyChanged("Value");
            }
        }


        public FlexFilterContenerProduct()
        {
            InitializeComponent();
        }

        private void Filter1_ButtonApplyClick(string text)
        {

        }
    }
}
