using Sklad_v1_001.GlobalAttributes;
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

namespace Sklad_v1_001.FormUsers.Product
{
    /// <summary>
    /// Логика взаимодействия для ProductItemGrid.xaml
    /// </summary>
    public partial class ProductItemGrid : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Attributes attribute;

        LocalRow localRowDetails;

        public LocalRow LocalRowDetails
        {
            get
            {
                return localRowDetails;
            }

            set
            {
                localRowDetails = value;
                this.Edit.DataContext = LocalRowDetails;
                OnPropertyChanged("LocalRowDetails");
            }
        }

        public ProductItemGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attribute = _attributes;

            this.Edit.DataContext = LocalRowDetails;
        }
    }
}
