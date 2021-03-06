﻿using Sklad_v1_001.GlobalVariable;
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

namespace Sklad_v1_001.Control.FlexMenu
{
    /// <summary>
    /// Логика взаимодействия для frameMenu.xaml
    /// </summary>
    public partial class frameMenu : UserControl
    {
        //
        // свойство зависимостей
        public static readonly DependencyProperty VisibilitySaleProperty = DependencyProperty.Register(
                        "VisibilitySale",
                        typeof(Visibility),
                        typeof(frameMenu), new UIPropertyMetadata(Visibility.Collapsed));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility VisibilitySale
        {
            get
            {
                return (Visibility)GetValue(VisibilitySaleProperty);
            }
            set
            {
                SetValue(VisibilitySaleProperty, value);
            }
        }


        public event Action ButtonProductOpen;
        public event Action ButtonProductEditOpen;
        //операция продажа
        public event Action ButtonNewSaleDocumentOpen;
        public event Action ButtonListSaleDocumentOpen;
        //
        public event Action ButtonDeliveryOpen;
        public event Action ButtonTransferDocumentOpen;
        public event Action ButtonSettingsOpen;
        public event Action ButtonExiteOpen;
        public frameMenu()
        {
            InitializeComponent();
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonProduct.Image.Source = ImageHelper.GenerateImage("IconProducts.png");
            ButtonSaleDocument.Image.Source = ImageHelper.GenerateImage("IconSale.png");
            ButtonDelivery.Image.Source = ImageHelper.GenerateImage("IconDelivery.png");
            ButtonTransferDocument.Image.Source = ImageHelper.GenerateImage("IconTransfer.png");
            ButtonSettings.Image.Source = ImageHelper.GenerateImage("IconServices.png");
            ButtonExite.Image.Source = ImageHelper.GenerateImage("IconExit.png");
        }

        private void ButtonProduct_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            ButtonProductOpen?.Invoke();
        }

        private void ButtonProductEdit_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            ButtonProductEditOpen?.Invoke();
        }
        private void ButtonDelivery_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            ButtonDeliveryOpen?.Invoke();
        }

       

        private void ButtonTransferDocument_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            ButtonTransferDocumentOpen?.Invoke();
        }

        private void ButtonSettings_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            ButtonSettingsOpen?.Invoke();
        }

        private void ButtonExite_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            ButtonExiteOpen?.Invoke();
        }
        #region Sale
        private void ButtonSaleDocument_ButtonClick()
        {
            VisibilitySale = Visibility.Visible;
        }

        private void ButtonNewSaleDocument_ButtonClick()
        {
            ButtonNewSaleDocumentOpen?.Invoke();
        }

        private void ButtonListSaleDocument_ButtonClick()
        {
            ButtonListSaleDocumentOpen?.Invoke();
        }
        #endregion
    }
}
