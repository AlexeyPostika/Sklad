﻿using System;
using System.Collections.Generic;
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
using Sklad_v1_001.FormUsers;
using Sklad_v1_001.FormUsers.SupplyDocument;
using Sklad_v1_001.FormUsers.Tovar;
using Sklad_v1_001.FormUsers.Zacupca;

namespace Sklad_v1_001
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        frameMenu _pageframeMenuLevel1;
        TovarZona tovarZona;
        TovarInZona tovarInZona;
        ZacupcaGrid zacupcaGrid;
        SupplyDocumentGrid supplyDocumentGrid;

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
        public MainWindow()
        {
            InitializeComponent();

            AppWindow = this;

            PageframeMenuLevel = new frameMenu();
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
            PageframeMenuLevel.ButtonDeliveryOpen += new Action(ButtonDeliveryOpen);
            //настройки                
            PageframeMenuLevel.ButtonSettingsOpen += new Action(ButtonSettingsOpen);
            //выход
            PageframeMenuLevel.ButtonExiteOpen += new Action(ButtonExiteOpen);

        }

        #region Product
        public void ButtonProductOpen()
        {
            tovarZona = new TovarZona();
            frameWorkArea.Navigate(tovarZona); // открытие страницы
        }

        public void ButtonProductEditOpen()
        {
            tovarInZona = new TovarInZona();
            this.frameWorkArea.Navigate(tovarInZona);
        }

        public void ButtonProductEditOpenF(FormUsers.Tovar.LocalRow _localRow)
        {
            tovarInZona = new TovarInZona();
            tovarInZona.LocalRow = _localRow;
            this.frameWorkArea.Navigate(tovarInZona);
        }
        #endregion

        #region Продажи
        public void ButtonNewSaleDocumentOpen()
        {
            
        }
        private void ButtonListSaleDocumentOpen()
        {
            
        }

        #endregion

        #region перемещение
        public void ButtonTransferDocumentOpen()
        {

        }
        #endregion

        #region поставки
        private void ButtonDeliveryOpen()
        {
           // zacupcaGrid = new ZacupcaGrid();
            supplyDocumentGrid = new SupplyDocumentGrid();
            frameWorkArea.Navigate(supplyDocumentGrid); // открытие страницы
        }
        #endregion

        #region настройки
        private void ButtonSettingsOpen()
        {

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
