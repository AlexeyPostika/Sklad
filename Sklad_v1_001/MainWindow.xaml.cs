using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Sklad_v1_001.Control.FlexMenu;
using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.Product;
using Sklad_v1_001.FormUsers.SaleDocument;
using Sklad_v1_001.FormUsers.SupplyDocument;
using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
using Sklad_v1_001.FormUsers.Tovar;
using Sklad_v1_001.FormUsers.Zacupca;
using Sklad_v1_001.GlobalAttributes;

namespace Sklad_v1_001
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Attributes attributes;

        frameMenu _pageframeMenuLevel1;
        TovarZona tovarZona;
        TovarInZona tovarInZona;
        ZacupcaGrid zacupcaGrid;
        //продукция
        ProductGrid productGrid;
        // Продажи
        NewSaleDocumentGrid newSaleDocument;
        SaleDocumentGrid saleDocumentGrid;
        //поставки товра
        SupplyDocumentGrid supplyDocumentGrid;
        NewSupplyDocumentGrid newSupplyDocumentGrid;

        NewAddProductItem newAddProductItem;
        SupplyDocumentDeliveryItem supplyDocumentDeliveryItem;

        //public static WorkZona AppWindow;
        //public MainWindow mailWindows1;
        public static MainWindow AppWindow;
        private string path = "log.txt";

        public frameMenu PageframeMenuLevel
        {
            get
            {
                return _pageframeMenuLevel1;
            }

            set
            {
                _pageframeMenuLevel1 = value;
            }
        }
        public MainWindow(Attributes _attributes)
        {
            InitializeComponent();
            
            this.attributes = _attributes;
            AppWindow = this;

            PageframeMenuLevel = new frameMenu(attributes);
            this.frameMenuLevel1.Navigate(PageframeMenuLevel);

            //продукты
            PageframeMenuLevel.ButtonProductOpen += new Action(ButtonProductOpen);
            PageframeMenuLevel.ButtonProductEditOpen += new Action(ButtonProductEditOpen);
            //продажи     
            PageframeMenuLevel.ButtonNewSaleDocumentOpen += new Action(ButtonNewSaleDocumentOpen);
            PageframeMenuLevel.ButtonListSaleDocumentOpen += new Action(ButtonListSaleDocumentOpen);
            //перемещение
            PageframeMenuLevel.ButtonTransferDocumentOpen += new Action(ButtonTransferDocumentOpen);
            //поставки
            PageframeMenuLevel.ButtonDeliveryNewSupplyOpen += new Action(ButtonNewSupplyDocument);
            PageframeMenuLevel.ButtonDeliveryListSupplyOpen += new Action(ButtonSupplyDocument);
            //настройки                
            PageframeMenuLevel.ButtonSettingsOpen += new Action(ButtonSettingsOpen);
            //выход
            PageframeMenuLevel.ButtonExiteOpen += new Action(ButtonExiteOpen);

        }
   
        #region Product
        public void ButtonProductOpen()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            productGrid = new ProductGrid(attributes);
            //tovarZona = new TovarZona();
            frameWorkArea.Navigate(productGrid); // открытие страницы
        }

        public void ButtonProductEditOpen()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            if (tovarInZona==null)
                tovarInZona = new TovarInZona();
            this.frameWorkArea.Navigate(tovarInZona);
        }

        public void ButtonProductEditOpenF(FormUsers.Tovar.LocalRow _localRow)
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            if (tovarInZona == null)
                tovarInZona = new TovarInZona();
            tovarInZona.LocalRow = _localRow;
            this.frameWorkArea.Navigate(tovarInZona);
        }
        #endregion

        #region Продажи
        public void ButtonNewSaleDocumentOpen()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти
            newSaleDocument = new NewSaleDocumentGrid(attributes);
            frameWorkArea.Navigate(newSaleDocument);
        }

        public void ButtonNewSaleDocumentOpenBasket(ObservableCollection<FormUsers.SaleDocumentProduct.LocalRow> _listProduct = null)
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти
            if (_listProduct!=null && _listProduct.Count() > 0)
            {
                FormUsers.SaleDocument.LocalFilter localFilter= new FormUsers.SaleDocument.LocalFilter();
                newSaleDocument = new NewSaleDocumentGrid(attributes);
                newSaleDocument.DatalistBasketShop = _listProduct;
                frameWorkArea.Navigate(newSaleDocument);
            }        
        }
        private void ButtonListSaleDocumentOpen()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти
            //if (saleDocumentGrid==null)
            saleDocumentGrid = new SaleDocumentGrid(attributes);           
            frameWorkArea.Navigate(saleDocumentGrid);
        }

        #endregion

        #region перемещение
        public void ButtonTransferDocumentOpen()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти
        }
        #endregion

        #region поставки
        private void ButtonSupplyDocument()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            // zacupcaGrid = new ZacupcaGrid();
            //if (supplyDocumentGrid == null)
            supplyDocumentGrid = new SupplyDocumentGrid(attributes);
            frameWorkArea.Navigate(supplyDocumentGrid); // открытие страницы
        }
        public void ButtonSupplyDocumentF(FormUsers.SupplyDocument.LocalRow _localRow, Boolean _isDocumentChanged = false)
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            // zacupcaGrid = new ZacupcaGrid();
            if (_localRow != null && _isDocumentChanged)
                supplyDocumentGrid = new SupplyDocumentGrid(attributes);
            frameWorkArea.Navigate(supplyDocumentGrid); // открытие страницы
        }


        public void ButtonNewSupplyDocument()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти
           
            newSupplyDocumentGrid = new NewSupplyDocumentGrid(attributes);
            newSupplyDocumentGrid.Status = 0;
            this.frameWorkArea.Navigate(newSupplyDocumentGrid);
        }

        public void ButtonNewSupplyDocumentF(FormUsers.SupplyDocument.LocalRow document)
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            if (newSupplyDocumentGrid == null)
                newSupplyDocumentGrid = new NewSupplyDocumentGrid(attributes);
            newSupplyDocumentGrid.Document = document;
            this.frameWorkArea.Navigate(newSupplyDocumentGrid);
        }

        //
        public void ButtonNewAddProduct()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            FlexWindows addProductWindow = new FlexWindows(Properties.Resources.ADDPRODUCT);
            if (newAddProductItem == null)
                newAddProductItem = new NewAddProductItem(attributes);
            addProductWindow.Content = newAddProductItem;
            addProductWindow.ShowDialog();
        }

        public void ButtonNewAddProductF(FormUsers.Product.LocalRow _localeRow)
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            FlexWindows addProductWindow = new FlexWindows(Properties.Resources.ADDPRODUCT);
            newAddProductItem = new NewAddProductItem(attributes);
            if (newAddProductItem == null)
                newAddProductItem.ProductLocalRow = _localeRow;
            addProductWindow.Content = newAddProductItem;
            addProductWindow.ShowDialog();
        }

        public void ButtonNewDelivery()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            FlexWindows addDeliveryWindow = new FlexWindows(Properties.Resources.ADDDELIVERY);
            if (supplyDocumentDeliveryItem == null)
                supplyDocumentDeliveryItem = new SupplyDocumentDeliveryItem(attributes);
            addDeliveryWindow.Content = supplyDocumentDeliveryItem;
            addDeliveryWindow.ShowDialog();
        }

        public void ButtonNewDeliveryF(FormUsers.SupplyDocumentDelivery.LocaleRow _localeRow)
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти

            FlexWindows addDeliveryWindow = new FlexWindows(Properties.Resources.ADDDELIVERY);
            if (supplyDocumentDeliveryItem == null)
                supplyDocumentDeliveryItem = new SupplyDocumentDeliveryItem(attributes);
            supplyDocumentDeliveryItem.DeliveryRow = _localeRow;
            addDeliveryWindow.Content = supplyDocumentDeliveryItem;
            addDeliveryWindow.ShowDialog();
        }
        #endregion

        #region настройки
        private void ButtonSettingsOpen()
        {
            GC.Collect();   //Вызов сборщика мусора
            GC.WaitForPendingFinalizers();  //ждем освобождение памяти
        }
        #endregion

        #region выход
        private void ButtonExiteOpen()
        {

        }
        #endregion


        private void MainWindows_Loaded(object sender, RoutedEventArgs e)
        {
            Log("Loaded");
        }
        private void MainWindows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Log("Closing");
        }
        private void MainWindows_Closed(object sender, EventArgs e)
        {
            Log("Closed");
        }

        private void Log(string eventName)
        {
            using (StreamWriter Logger = new StreamWriter(path, true))
            {
                Logger.WriteLine(DateTime.Now.ToLongTimeString() + " - " + eventName);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            ////Osnovnou.Content=new FormUsers.Form2();
            //this.Height = 961;
            //this.Width = 1241;
            //this.Main.Margin = new Thickness(1, 1, 1, 1);
            //Autorizaciy.Visibility = Visibility.Collapsed;
            //Main.Content = new WorkZona();
            //Main.NavigationService.Navigate(new Uri("WorkZona.xaml", UriKind.Relative));
           //Main.Content = new Page1();
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void frameWorkArea_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }
    }
}
