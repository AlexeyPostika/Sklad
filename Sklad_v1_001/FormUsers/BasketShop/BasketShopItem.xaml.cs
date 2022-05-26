using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Sklad_v1_001.FormUsers.BasketShop
{
    /// <summary>
    /// Логика взаимодействия для BasketShopItem.xaml
    /// </summary>
    public partial class BasketShopItem : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Attributes attributes;

        ObservableCollection<LocalRow> listBasketShop;
        public ObservableCollection<LocalRow> ListBasketShop
        {
            get
            {
                return listBasketShop;
            }

            set
            {
                listBasketShop = value;
                if (value != null)
                    this.DataBasketShop.ItemsSource = ListBasketShop;
                OnPropertyChanged("ListBasketShop");
            }
        }
        public BasketShopItem(Attributes _attributes )
        {
            InitializeComponent();
            this.attributes = _attributes;

            this.DataBasketShop.ItemsSource = ListBasketShop;

        }

        private void EditBoxNumericGrid_TextChanged()
        {

        }

        private void basketShop_ButtonClick()
        {

        }
    }
}
