using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

        public event Action ButtonClick; //получаем название события
        Attributes attributes;

        //схема структуры SupplyDocument
        ShemaStorаge shemaStorаge;

        BasketShopLogic basketShopLogic;

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
                {
                    if (listBasketShop.Count>0)
                    {
                        basketShop.IsEnabled = true;
                    }
                    else
                    {
                        basketShop.IsEnabled = false;
                    }
                    this.DataBasketShop.ItemsSource = ListBasketShop;
                }
                else
                {
                    basketShop.IsEnabled = false;
                }
                OnPropertyChanged("ListBasketShop");
            }
        }
        public BasketShopItem(Attributes _attributes )
        {
            InitializeComponent();
            this.attributes = _attributes;

            shemaStorаge = new ShemaStorаge();

            basketShopLogic = new BasketShopLogic(attributes);

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

        private void delete_ButtonClick()
        {
            LocalRow currentrow = this.DataBasketShop.SelectedItem as LocalRow;
            if (currentrow != null)
            {
                ListBasketShop.Remove(currentrow);
                
                shemaStorаge.BasketShop.Clear();
                
                foreach (BasketShop.LocalRow local in ListBasketShop)
                {
                    try
                    {
                        DataRow rowTabel = shemaStorаge.BasketShop.NewRow();
                        rowTabel["UserID"] = local.UserID;
                        rowTabel["ProductID"] = local.ProductID;
                        rowTabel["Quantity"] = local.BasketQuantity;
                        shemaStorаge.BasketShop.Rows.Add(rowTabel);
                    }
                    catch (Exception ex) { }

                }
                basketShopLogic.SaveRow(shemaStorаge, 1);
            }
            ButtonClick?.Invoke();
        }
       
    }
}
