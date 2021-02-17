using Sklad_v1_001.GlobalVariable;
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

        }

        private void ButtonDelivery_ButtonClick()
        {

        }

        private void ButtonSaleDocument_ButtonClick()
        {

        }

        private void ButtonTransferDocument_ButtonClick()
        {

        }

        private void ButtonSettings_ButtonClick()
        {

        }
    }
}
