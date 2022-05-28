using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
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
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(BasketShopItem), new PropertyMetadata(MessageBoxResult.Cancel));

        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
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
            
            basketShop.Image.Source = ImageHelper.GenerateImage("IconPrintCheck_X30.png");

            this.DataBasketShop.ItemsSource = ListBasketShop;

        }

        private void EditBoxNumericGrid_TextChanged()
        {
            LocalRow currentrow = this.DataBasketShop.SelectedItem as LocalRow;
            if (currentrow != null)
            {
                if (currentrow.BasketQuantity <= currentrow.ProductQuantity)
                    currentrow.IsActiveRow = false;
                else
                    currentrow.IsActiveRow = true;
            }
        }

        private void basketShop_ButtonClick()
        {
            IsClickButtonOK = MessageBoxResult.OK;
            Window win = Parent as Window;
            win.Close();
        }
    }
}
