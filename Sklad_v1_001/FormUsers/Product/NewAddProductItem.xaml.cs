using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Category;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        Attributes attributes;

        LocaleRow productLocalRow;
        public LocaleRow ProductLocalRow { get => productLocalRow; set => productLocalRow = value; }

        CategoryLogic categoryLogic;
        ObservableCollection<GlobalList.Category> dataCategory;
        ObservableCollection<GlobalList.CategoryDetails> dataCategoryDetails;

        ConvertData convertData;

        public NewAddProductItem(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            convertData = new ConvertData();

            //загружаем категории
            categoryLogic = new CategoryLogic();
            dataCategory = new ObservableCollection<GlobalList.Category>();
            dataCategoryDetails = new ObservableCollection<GlobalList.CategoryDetails>();
            dataCategory = attributes.datalistCategory;
            dataCategoryDetails = attributes.datalistCategoryDetails;

            CategoryCat.ComboBoxElement.ItemsSource = dataCategory;
            CategoryCat.ComboBoxElement.SelectedValue = 0;

            CategoryDetails.ComboBoxElement.ItemsSource = dataCategoryDetails;
            CategoryDetails.ComboBoxElement.SelectedValue = 0;
            //-----------------------------------------------------------------------------

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

            if (String.IsNullOrEmpty(ProductLocalRow.CategoryDetailsName))
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
            
            if (String.IsNullOrEmpty(ProductLocalRow.BarCodeString))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, BarCode.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                BarCode.TextBox.Focus();
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

        private void CategoryDetailsName_ButtonClearClick()
        {
            CategoryDetails.Visibility = Visibility.Visible;
            CategoryDetails.ComboBoxElement.IsDropDownOpen = true;
            CategoryDetailsName.Visibility = Visibility.Collapsed;
        }

        private void CategoryDetails_DropDownClosed()
        {
            if (CategoryDetails.Value != 0 && dataCategoryDetails.Count != 0)
            {
                CategoryDetailsName.Text = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                    dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Description : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryDetailsName = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                 dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Description : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryDetailsDescription = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                  dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Name : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryID = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                    dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).CategoryID : 0;
            
            }          

            CategoryDetails.Visibility = Visibility.Collapsed;
            CategoryDetailsName.Visibility = Visibility.Visible;
        }

        private void CategoryDetailsName_ButtonTextChangedClick()
        {
            if (dataCategory.Count != 0)
            {
                CategoryDetailsName.Text = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                    dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Description : CategoryDetailsName.Text;

                ProductLocalRow.CategoryDetailsName = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                 dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Description : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryDetailsDescription = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                  dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Name : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryID = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                    dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).CategoryID : 0;
            }   
            else
            {
                ProductLocalRow.CategoryDetailsID = 0;
                ProductLocalRow.CategoryDetailsDescription =  Properties.Resources.UndefindField;

                ProductLocalRow.CategoryID =  0;
                ProductLocalRow.CategoryName =  Properties.Resources.UndefindField;
                ProductLocalRow.CategoryDescription =  Properties.Resources.UndefindField;

            }
        }

        #region Category
        private void CategoryName_ButtonClearClick()
        {
            CategoryCat.Visibility = Visibility.Visible;
            CategoryCat.ComboBoxElement.IsDropDownOpen = true;
            CategoryName.Visibility = Visibility.Collapsed;
        }
        private void CategoryCat_DropDownClosed()
        {
            if (CategoryCat.Value != 0 && dataCategory.Count != 0)
            {
                CategoryName.Text = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())) != null ?
                    dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())).Description : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryName = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())) != null ?
                   dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())).Description : Properties.Resources.UndefindField; 

                ProductLocalRow.CategoryID = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())) != null ?
                    dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())).ID : 0;

                ProductLocalRow.CategoryDescription = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())) != null ?
                    dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())).Name : Properties.Resources.UndefindField;
            }
            ObservableCollection<GlobalList.CategoryDetails> dataCategoryDetailsTemp = new ObservableCollection<GlobalList.CategoryDetails>();
            foreach (GlobalList.CategoryDetails categoryDetails in dataCategoryDetails)
            {
                if (categoryDetails.CategoryID== ProductLocalRow.ID)
                    dataCategoryDetailsTemp.Add(categoryDetails);
            }

            CategoryDetails.ComboBoxElement.ItemsSource = dataCategoryDetailsTemp;
            CategoryDetails.ComboBoxElement.SelectedValue = 0;

            CategoryCat.Visibility = Visibility.Collapsed;
            CategoryName.Visibility = Visibility.Visible;
        }

        private void CategoryName_ButtonTextChangedClick()
        {
            if (dataCategory.Count != 0)
            {
                CategoryDetailsName.Text = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                    dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Description :
                   CategoryDetailsName.Text;

                ProductLocalRow.CategoryName = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())) != null ?
                    dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())).Description : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryID = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())) != null ?
                    dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())).ID : 0;

                ProductLocalRow.CategoryDescription = dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())) != null ?
                    dataCategory.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString())).Name : Properties.Resources.UndefindField;
            }
            else
            {
                ProductLocalRow.CategoryDetailsID = 0;
                ProductLocalRow.CategoryDetailsDescription = Properties.Resources.UndefindField;

                ProductLocalRow.CategoryID = 0;
                ProductLocalRow.ID = 0;
                ProductLocalRow.CategoryName = Properties.Resources.UndefindField;
                ProductLocalRow.CategoryDescription = Properties.Resources.UndefindField;

            }
        }
        #endregion
    }
}
