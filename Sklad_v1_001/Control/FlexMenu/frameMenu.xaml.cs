using Sklad_v1_001.GlobalAttributes;
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
        //
        // свойство зависимостей
        public static readonly DependencyProperty VisibilitySaleProperty = DependencyProperty.Register(
                        "VisibilitySale",
                        typeof(Visibility),
                        typeof(frameMenu), new UIPropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty VisibilitySupplyProperty = DependencyProperty.Register(
                       "VisibilitySupply",
                       typeof(Visibility),
                       typeof(frameMenu), new UIPropertyMetadata(Visibility.Collapsed));

        //
        public static readonly DependencyProperty VisibilityRegisterProperty = DependencyProperty.Register(
                      "VisibilityRegister",
                      typeof(Visibility),
                      typeof(frameMenu), new UIPropertyMetadata(Visibility.Collapsed));

       
        public static readonly DependencyProperty VisibilityCompanyProperty = DependencyProperty.Register(
                     "VisibilityCompany",
                     typeof(Visibility),
                     typeof(frameMenu), new UIPropertyMetadata(Visibility.Collapsed));


        public static readonly DependencyProperty VisibilityShopsProperty = DependencyProperty.Register(
                     "VisibilityShops",
                     typeof(Visibility),
                     typeof(frameMenu), new UIPropertyMetadata(Visibility.Collapsed));

        // 
        public static readonly DependencyProperty VisibilitySettingProperty = DependencyProperty.Register(
                      "VisibilitySetting",
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

        public Visibility VisibilitySupply
        {
            get
            {
                return (Visibility)GetValue(VisibilitySupplyProperty);
            }
            set
            {
                SetValue(VisibilitySupplyProperty, value);
            }
        }

        public Visibility VisibilityRegister
        {
            get
            {
                return (Visibility)GetValue(VisibilityRegisterProperty);
            }
            set
            {
                SetValue(VisibilityRegisterProperty, value);
            }
        }

        public Visibility VisibilityCompany
        {
            get
            {
                return (Visibility)GetValue(VisibilityCompanyProperty);
            }
            set
            {
                SetValue(VisibilityCompanyProperty, value);
            }
        }
       
        public Visibility VisibilityShops
        {
            get
            {
                return (Visibility)GetValue(VisibilityShopsProperty);
            }
            set
            {
                SetValue(VisibilityShopsProperty, value);
            }
        }
        public Visibility VisibilitySetting
        {
            get
            {
                return (Visibility)GetValue(VisibilitySettingProperty);
            }
            set
            {
                SetValue(VisibilitySettingProperty, value);
            }
        }
        Attributes attributes;

        public event Action ButtonProductOpen;
        public event Action ButtonProductEditOpen;
        //операция продажа
        public event Action ButtonNewSaleDocumentOpen;
        public event Action ButtonListSaleDocumentOpen;
        //регистрация документов
        public event Action ButtonNewRegisterDocumentOpen;
        public event Action ButtonRegisterListDocument;
        //операция поставки
        public event Action ButtonDeliveryNewSupplyOpen;
        public event Action ButtonDeliveryListSupplyOpen;

        public event Action ButtonTransferDocumentOpen;
        
        //Компании
        public event Action ButtonNewCompanyOpen;
        public event Action ButtonListCompanyOpen;

        //Магазины и склады
        public event Action ButtonNewShopsOpen;
        public event Action ButtonListShopsOpen;

        public event Action ButtonExiteOpen;
        public frameMenu(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;

            var temp = attributes.datalistUsers.FirstOrDefault(x => x.ID == attributes.numeric.userEdit.AddUserID) != null ? attributes.datalistUsers.FirstOrDefault(x => x.ID == attributes.numeric.userEdit.AddUserID) : null;
            if (temp != null)
            {
                this.userInfo.LastNmae = temp.LastName + " " + temp.FirstName[0] + ". " + temp.SecondName[0] + ".";
                this.userInfo.RoleID = temp.RoleID.ToString();
                this.userInfo.ImageControl = temp.PhotoUserImage;
            }
           
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonProduct.Image.Source = ImageHelper.GenerateImage("IconProducts.png");
            ButtonSaleDocument.Image.Source = ImageHelper.GenerateImage("IconSale.png");
            ButtonRegisterDocument.Image.Source = ImageHelper.GenerateImage("IconRegisterDocumetn_X40.png");
            ButtonDelivery.Image.Source = ImageHelper.GenerateImage("IconDelivery.png");
            ButtonTransferDocument.Image.Source = ImageHelper.GenerateImage("IconTransfer.png");
            ButtonCompany.Image.Source = ImageHelper.GenerateImage("IconCraufting_X30.png");
            ButtonShops.Image.Source = ImageHelper.GenerateImage("IconShops_X30.png");
            ButtonSettings.Image.Source = ImageHelper.GenerateImage("IconServices.png");
            ButtonExite.Image.Source = ImageHelper.GenerateImage("IconExit.png");
        }

        private void ButtonProduct_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Collapsed;
            VisibilitySetting = Visibility.Collapsed;
            VisibilityCompany = Visibility.Collapsed;
            VisibilityShops = Visibility.Collapsed;
            ButtonProductOpen?.Invoke();
        }

        private void ButtonProductEdit_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Collapsed;
            VisibilitySetting = Visibility.Collapsed;
            VisibilityCompany = Visibility.Collapsed;
            VisibilityShops = Visibility.Collapsed;
            ButtonProductEditOpen?.Invoke();
        }

        #region SupplyDocument
        private void ButtonDelivery_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Visible;
            VisibilityRegister = Visibility.Collapsed;
            VisibilitySetting = Visibility.Collapsed;
            VisibilityCompany = Visibility.Collapsed;
            VisibilityShops = Visibility.Collapsed;
        }

        private void ButtonNewSupplyDocument_ButtonClick()
        {
            ButtonDeliveryNewSupplyOpen?.Invoke();
        }

        private void ButtonListSupplyDocument_ButtonClick()
        {
            ButtonDeliveryListSupplyOpen?.Invoke();
        }
        #endregion

        private void ButtonTransferDocument_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Collapsed;
            VisibilityRegister = Visibility.Collapsed;
            VisibilitySetting = Visibility.Collapsed;
            VisibilityCompany = Visibility.Collapsed;
            VisibilityShops = Visibility.Collapsed;
            ButtonTransferDocumentOpen?.Invoke();
        }
      
        private void ButtonExite_ButtonClick()
        {           
            ButtonExiteOpen?.Invoke();
        }
        #region Sale
        private void ButtonSaleDocument_ButtonClick()
        {
            VisibilitySale = Visibility.Visible;
            VisibilitySupply = Visibility.Collapsed;
            VisibilityRegister = Visibility.Collapsed;
            VisibilityCompany = Visibility.Collapsed;
            VisibilityShops = Visibility.Collapsed;
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

        #region Регистрация документа

        private void ButtonRegisterDocument_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Collapsed;
            VisibilityRegister = Visibility.Visible;
            VisibilityCompany = Visibility.Collapsed;
            VisibilityShops = Visibility.Collapsed;
        }

        private void ButtonListRegisterDocumentDocument_ButtonClick()
        {
            ButtonRegisterListDocument?.Invoke();
        }

        private void ButtonNewRegisterDocument_ButtonClick()
        {
            ButtonNewRegisterDocumentOpen?.Invoke();
        }

        #endregion

        #region Компания
        private void ButtonCompany_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Collapsed;
            VisibilityRegister = Visibility.Collapsed;
            VisibilityCompany = Visibility.Visible;
            VisibilitySetting = Visibility.Collapsed;
            VisibilityShops = Visibility.Collapsed;
        }

        private void buttonNewCompanyItem_ButtonClick()
        {
            ButtonNewCompanyOpen?.Invoke();
        }

        private void buttonCompanyGrid_ButtonClick()
        {
            ButtonListCompanyOpen?.Invoke();
        }
        #endregion

        #region Setting
        private void ButtonSettings_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Collapsed;
            VisibilityRegister = Visibility.Collapsed;
            VisibilityCompany = Visibility.Collapsed;
            VisibilitySetting = Visibility.Visible;
            VisibilityShops = Visibility.Collapsed;
        }
        #endregion

        #region Shops
        private void ButtonShops_ButtonClick()
        {
            VisibilitySale = Visibility.Collapsed;
            VisibilitySupply = Visibility.Collapsed;
            VisibilityRegister = Visibility.Collapsed;
            VisibilityCompany = Visibility.Collapsed;
            VisibilitySetting = Visibility.Collapsed;
            VisibilityShops = Visibility.Visible;
        }
        private void buttonNewShopsItem_ButtonClick()
        {
            ButtonNewShopsOpen?.Invoke();
        }

        private void buttonShopsGrid_ButtonClick()
        {
            ButtonListShopsOpen?.Invoke();
        }
        #endregion
    }
}
