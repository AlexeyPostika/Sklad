using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.Category;
using Sklad_v1_001.FormUsers.CategoryDetails;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal;
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
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.FormUsers.Product
{
    /// <summary>
    /// Логика взаимодействия для NewAddProductItem.xaml
    /// </summary>
    public partial class NewAddProductItem : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewAddProductItem), new PropertyMetadata(MessageBoxResult.Cancel));

        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
                    "IsEnableAdd",
                    typeof(Boolean),
                   typeof(NewAddProductItem), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEnableEditProperty = DependencyProperty.Register(
                     "IsEnableEdit",
                     typeof(Boolean),
                    typeof(NewAddProductItem), new PropertyMetadata(false));

        public static readonly DependencyProperty StatusDocumentProperty = DependencyProperty.Register(
                    "StatusDocument",
                    typeof(Boolean),
                   typeof(NewAddProductItem), new PropertyMetadata(false));

        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }
        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }

        public Boolean IsEnableEdit
        {
            get { return (Boolean)GetValue(IsEnableEditProperty); }
            set { SetValue(IsEnableEditProperty, value); }
        }

        public Boolean StatusDocument
        {
            get { return (Boolean)GetValue(StatusDocumentProperty); }
            set { SetValue(StatusDocumentProperty, value);}
        }

        Attributes attributes;

        //работаем с Category
        FlexMessageBox addCategoryWindow;
        NewCategoryItem newCategoryItem;
        //работаем с CategoryDetails
        FlexMessageBox addCategoryDetailsWindow;
        NewCategoryDetailsItem newCategoryDetailsItem;

        LocaleRow productLocalRow;
        public LocaleRow ProductLocalRow
        {
            get
            {
                return productLocalRow;
            }

            set
            {
                productLocalRow = value;      
                if (value!=null)
                {                   
                    if (value.Package)
                    {
                        value.RadioType = 2;
                        RadioYEStoNO.NO.IsChecked = false;
                    }
                    else
                    {
                        value.RadioType = 1;
                        RadioYEStoNO.YES.IsChecked = true;
                    }
                    Product.IsEnabled = StatusDocument;

                }
                this.Product.DataContext = ProductLocalRow;

                OnPropertyChanged("ProductLocalRow");
            }
        }

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
                //mb = new FlexMessageBox();
                //mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
               // CategoryName.EditBoxUser.TextField.Focus();
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

        #region CategoryDetails
        private void CategoryDetails_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (CategoryDetails.Value > 0)
            {
                IsEnableEdit = true;
            }
            else
            {
                IsEnableEdit = false;
            }
        }

        private void CategoryDetailsName_ButtonClearClick()
        {
           
        }

        private void CategoryDetails_DropDownClosed()
        {
            if (CategoryDetails.Value != 0 && dataCategoryDetails.Count != 0)
            {                
                ProductLocalRow.CategoryDetailsName = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                 dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Description : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryDetailsDescription = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                  dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Name : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryID = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                    dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).CategoryID : 0;              

            }                    
        }

        private void CategoryDetailsName_ButtonTextChangedClick()
        {
            if (dataCategory.Count != 0)
            {            
                ProductLocalRow.CategoryDetailsName = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                 dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Description : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryDetailsDescription = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                  dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).Name : Properties.Resources.UndefindField;

                ProductLocalRow.CategoryID = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                    dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).CategoryID : 0;

                ProductLocalRow.ID = dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())) != null ?
                  dataCategoryDetails.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString())).ID : 0;
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
        #endregion

        #region Category
        private void CategoryCat_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (CategoryCat.Value > 0)
            {
                IsEnableAdd = true;
            }
            else
            {
                IsEnableAdd = false;
            }
        }
       
        private void CategoryCat_DropDownClosed()
        {
            if (CategoryCat.Value != 0 && dataCategory.Count != 0)
            {
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
                if (categoryDetails.CategoryID== ProductLocalRow.CategoryID)
                    dataCategoryDetailsTemp.Add(categoryDetails);
            }

            CategoryDetails.ComboBoxElement.ItemsSource = dataCategoryDetailsTemp;
            CategoryDetails.ComboBoxElement.SelectedValue = 0;
        }

        private void CategoryName_ButtonTextChangedClick()
        {
            if (dataCategory.Count != 0)
            {
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

        #region CategoryAdd
        private void CategoryCat_ButtonAdd()
        {          
            newCategoryItem = new NewCategoryItem(attributes);
            addCategoryWindow = new FlexMessageBox();          
            addCategoryWindow.Content = newCategoryItem;
            addCategoryWindow.Show(Properties.Resources.CATEGORY);
            if (newCategoryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newCategoryItem.CategoryRow != null)
                {
                    dataCategory = attributes.datalistCategory;
                   
                    ProductLocalRow.CategoryName = newCategoryItem.CategoryRow.Description;
                    ProductLocalRow.CategoryID = newCategoryItem.CategoryRow.ID;
                    ProductLocalRow.CategoryDescription = newCategoryItem.CategoryRow.Name;
                    CategoryCat.ComboBoxElement.SelectedValue = ProductLocalRow.CategoryID;
                }
            }
        }

        private void CategoryCat_ButtonEdit()
        {
            newCategoryItem = new NewCategoryItem(attributes);
            addCategoryWindow = new FlexMessageBox();
            newCategoryItem.CategoryRow = dataCategory.First(x=>x.ID== convertData.FlexDataConvertToInt32(CategoryCat.Value.ToString()));
            addCategoryWindow.Content = newCategoryItem;
            addCategoryWindow.Show(Properties.Resources.CATEGORY);
            if (newCategoryItem.IsClickButtonOK == MessageBoxResult.OK)
            {
                if (newCategoryItem.CategoryRow != null)
                {
                    dataCategory = attributes.datalistCategory;
                    
                    ProductLocalRow.CategoryName = newCategoryItem.CategoryRow.Description;
                    ProductLocalRow.CategoryID = newCategoryItem.CategoryRow.ID;
                    ProductLocalRow.CategoryDescription = newCategoryItem.CategoryRow.Name;
                    CategoryCat.ComboBoxElement.SelectedValue = ProductLocalRow.CategoryID;
                }
            }
        }
        #endregion

        #region CategoryDetailsAdd
        private void CategoryDetails_ButtonAdd()
        {
            if (CategoryCat.Value > 0)
            {

                newCategoryDetailsItem = new NewCategoryDetailsItem(attributes);
                addCategoryDetailsWindow = new FlexMessageBox();
                
                GlobalList.CategoryDetails categoryDetailsTemp = new GlobalList.CategoryDetails();               

                categoryDetailsTemp.CategoryIDString = dataCategory.FirstOrDefault(x => x.ID == CategoryCat.Value) != null ?
                   dataCategory.FirstOrDefault(x => x.ID == CategoryCat.Value).Description : "";

                categoryDetailsTemp.CategoryID = dataCategory.FirstOrDefault(x => x.ID == CategoryCat.Value) != null ?
                  dataCategory.FirstOrDefault(x => x.ID == CategoryCat.Value).ID : 0;

                newCategoryDetailsItem.CategoryDetailsRow = categoryDetailsTemp;

                addCategoryDetailsWindow.Content = newCategoryDetailsItem;
                addCategoryDetailsWindow.Show(Properties.Resources.CATEGORYDETAILS);
                if (newCategoryDetailsItem.IsClickButtonOK == MessageBoxResult.OK)
                {
                    if (newCategoryDetailsItem.CategoryDetailsRow != null)
                    {
                        dataCategory = attributes.datalistCategory;
                        dataCategoryDetails = attributes.datalistCategoryDetails;

                        ProductLocalRow.CategoryDetailsName = newCategoryDetailsItem.CategoryDetailsRow.Description;
                        ProductLocalRow.CategoryID = newCategoryDetailsItem.CategoryDetailsRow.CategoryID;
                        ProductLocalRow.CategoryDetailsDescription = newCategoryDetailsItem.CategoryDetailsRow.Name;
                        ProductLocalRow.ID = newCategoryDetailsItem.CategoryDetailsRow.ID;
                        CategoryCat.ComboBoxElement.SelectedValue = dataCategory.FirstOrDefault(x => x.ID == ProductLocalRow.CategoryID).ID;
                        CategoryDetails.ComboBoxElement.SelectedValue = ProductLocalRow.ID;
                    }
                }
            }
            else
            {
                FlexMessageBox mb;
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorCategory0, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryCat.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                CategoryCat.ComboBoxElement.Focus();
            }
        }

        private void CategoryDetails_ButtonEdit()
        {
            if (CategoryCat.Value > 0)
            {
                newCategoryDetailsItem = new NewCategoryDetailsItem(attributes);
                addCategoryDetailsWindow = new FlexMessageBox();
                GlobalList.CategoryDetails categoryDetailsTemp = new GlobalList.CategoryDetails();
                if (CategoryDetails.Value > 0)
                    categoryDetailsTemp = dataCategoryDetails.First(x => x.ID == convertData.FlexDataConvertToInt32(CategoryDetails.Value.ToString()));

                categoryDetailsTemp.CategoryIDString = dataCategory.FirstOrDefault(x => x.ID == categoryDetailsTemp.CategoryID) != null ?
                   dataCategory.FirstOrDefault(x => x.ID == categoryDetailsTemp.CategoryID).Description : "";

                categoryDetailsTemp.CategoryID = dataCategory.FirstOrDefault(x => x.ID == categoryDetailsTemp.CategoryID) != null ?
                  dataCategory.FirstOrDefault(x => x.ID == categoryDetailsTemp.CategoryID).ID : 0;

                newCategoryDetailsItem.CategoryDetailsRow = categoryDetailsTemp;

                addCategoryDetailsWindow.Content = newCategoryDetailsItem;
                addCategoryDetailsWindow.Show(Properties.Resources.CATEGORYDETAILS);
                if (newCategoryDetailsItem.IsClickButtonOK == MessageBoxResult.OK)
                {
                    if (newCategoryDetailsItem.CategoryDetailsRow != null)
                    {
                        dataCategory = attributes.datalistCategory;
                        dataCategoryDetails = attributes.datalistCategoryDetails;
                        ProductLocalRow.CategoryDetailsName = newCategoryDetailsItem.CategoryDetailsRow.Description;
                        ProductLocalRow.CategoryID = newCategoryDetailsItem.CategoryDetailsRow.CategoryID;
                        ProductLocalRow.CategoryDetailsDescription = newCategoryDetailsItem.CategoryDetailsRow.Name;
                        ProductLocalRow.ID = newCategoryDetailsItem.CategoryDetailsRow.ID;
                       
                        CategoryCat.ComboBoxElement.SelectedValue = dataCategory.FirstOrDefault(x => x.ID == ProductLocalRow.CategoryID).ID;
                        CategoryDetails.ComboBoxElement.SelectedValue = newCategoryDetailsItem.CategoryDetailsRow.ID;
                    }
                }
            }
            else
            {
                FlexMessageBox mb;
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorCategory0, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryCat.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                CategoryCat.ComboBoxElement.Focus();              
            }
           
        }

        #endregion

        
    }
}
