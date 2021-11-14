using Sklad_v1_001.Control.FlexMessageBox;
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
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.FormUsers.Product
{
    /// <summary>
    /// Логика взаимодействия для NewAddProductItem.xaml
    /// </summary>
    public partial class NewAddProductItem : Page
    {
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewAddProductItem), new PropertyMetadata(MessageBoxResult.Cancel));
        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }
        LocaleRow productLocalRow;
        public LocaleRow ProductLocalRow { get => productLocalRow; set => productLocalRow = value; }

        public NewAddProductItem()
        {
            InitializeComponent();

            ProductLocalRow = new LocaleRow();

            this.Product.DataContext = ProductLocalRow;
        }

        private void ToolBarButton_ButtonClick()
        {
            if (FieldVerify())
            {
                IsClickButtonOK = MessageBoxResult.OK;
                Window win = Parent as Window;
                win.Close();
            }
        }
        private Boolean FieldVerify()
        {
            FlexMessageBox mb;

            if (String.IsNullOrEmpty(ProductLocalRow.Name))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, ProductName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                ProductName.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(ProductLocalRow.CategoryString))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                CategoryName.EditBoxUser.TextField.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(ProductLocalRow.Model))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, ModelName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                ModelName.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(ProductLocalRow.Quantity.ToString()))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, QuantityName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                QuantityName.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(ProductLocalRow.TagPriceUSA.ToString()))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, TagPriceUSAName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                TagPriceUSAName.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(ProductLocalRow.TagPriceRUS.ToString()))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, TagPriceRUSName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                TagPriceRUSName.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(ProductLocalRow.SizeProduct))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, SizeProductName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                SizeProductName.DescriptionInfo.Focus();
                return false;
            }
            return true;
        }

    }
}
