using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ProductItemGrid.xaml
    /// </summary>
    public partial class ProductItemGrid : Page
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public StatusWindows statusWindows { get; set; }

        ImageSql imageSql;
        Attributes attribute;

        ProductLogic productLogic;

        LocalRow localRowDetails;

        //схема структуры
        ShemaStorаge shemaStorаge;
        //StatusWindows

        public LocalRow LocalRowDetails
        {
            get
            {
                return localRowDetails;
            }

            set
            {
                localRowDetails = value;               
                this.Edit.DataContext = LocalRowDetails;
                Edit.BareCode = value.BarCodeString;
                OnPropertyChanged("LocalRowDetails");
            }
        }

        public ProductItemGrid(Attributes _attributes)
        {
            InitializeComponent();
            statusWindows = StatusWindows.None;

            this.attribute = _attributes;
            imageSql = new ImageSql();
            shemaStorаge = new ShemaStorаge();

            productLogic = new ProductLogic(attribute);

            this.Edit.DataContext = LocalRowDetails;
            this.Edit.DataListCollectionShowCase = attribute.datalistShowCase;
            this.Edit.DataListCollectionManufacturer = attribute.datalistManufacturer;
        }

        #region ToolBar
        private void toolbar_ButtonSave()
        {
            Save();
        }

        private void toolbar_ButtonSaveclose()
        {
            if (Save() > 0)
            {
                Window win = Parent as Window;
                win.Close();
            }
        }

        private void toolbar_ButtonListCancel()
        {
            statusWindows = StatusWindows.Cancel;
            Window win = Parent as Window;
            win.Close();
        }
        #endregion

        #region Save
        Int32 Save()
        {
            ConvertData convertdata = new ConvertData();

            if (FieldVerify())
            {
                shemaStorаge.Product.Clear();
                shemaStorаge.ProductImage.Clear();
                
                //продукты     
                DataRow rowProduct = shemaStorаge.Product.NewRow();
                rowProduct["Name"] = 0;
                rowProduct["CategoryID"] = LocalRowDetails.CategoryID;
                rowProduct["CategoryDetailsID"] = LocalRowDetails.CategoryDetailsID;
                rowProduct["Barecode"] = LocalRowDetails.BarCodeString;
                rowProduct["ProcreatorID"] = LocalRowDetails.ManufacturerID;
                rowProduct["Model"] = LocalRowDetails.Model;
                rowProduct["TagPriceUSA"] = LocalRowDetails.TagPriceUSA;
                rowProduct["TagPriceRUS"] = LocalRowDetails.TagPriceRUS;
                rowProduct["SupplyDocumentID"] = 0;
                rowProduct["Description"] = LocalRowDetails.Description;
                rowProduct["Quantity"] = LocalRowDetails.Quantity;
                rowProduct["ShowcaseID"] = LocalRowDetails.ShowcaseID;
                rowProduct["SizeProduct"] = LocalRowDetails.SizeProduct;
                rowProduct["CreatedDate"] = LocalRowDetails.CreatedDate;
                rowProduct["CreatedUserID"] = LocalRowDetails.CreatedUserID;
                rowProduct["LastModificatedDate"] = LocalRowDetails.LastModicatedDate;
                rowProduct["LastModificatedUserID"] = 1;//attribute.AddUser;
                rowProduct["SupplyDocumentDetailsID"] = 0;
                rowProduct["Status"] = LocalRowDetails.Status;
                shemaStorаge.Product.Rows.Add(rowProduct);

                //фото продукта
                Int32 temp = 0;
                if (LocalRowDetails.ListImage != null && LocalRowDetails.ListImage.Count>0)
                {
                    foreach (ImageSource image in LocalRowDetails.ListImage)
                    {
                        DataRow row = shemaStorаge.ProductImage.NewRow();
                        row["DocumentID"] = LocalRowDetails.ID;                      
                        row["Images"] = imageSql.ImageSourceToBytes(image);
                        shemaStorаge.ProductImage.Rows.Add(row);
                    }
                    temp = productLogic.SaveProductImage(shemaStorаge);
                }

                LocalRowDetails.ID = productLogic.SaveProduct(LocalRowDetails,shemaStorаge);              

                if (LocalRowDetails.ID>0 && temp>=0)
                {
                    statusWindows = StatusWindows.Refresh;
                    return LocalRowDetails.ID;
                }
                else
                {
                    FlexMessageBox mb = new FlexMessageBox();
                    mb.Show(Properties.Resources.ErrorSaveProduct, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, Properties.Resources.SAVE), MessageBoxButton.OK, MessageBoxImage.Error);                                      
                }
                // return Document.ID;
            }           
            return 0;
        }
        #endregion

        #region FieldVerify
        private Boolean FieldVerify()
        {
            FlexMessageBox mb;

            //if (String.IsNullOrEmpty(ProductLocalRow.Name))
            //{
            //    mb = new FlexMessageBox();
            //    mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, ProductName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    ProductName.DescriptionInfo.Focus();
            //    return false;
            //}

            //if (String.IsNullOrEmpty(ProductLocalRow.CategoryDetailsName))
            //{
            //    //mb = new FlexMessageBox();
            //    //mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    // CategoryName.EditBoxUser.TextField.Focus();
            //    return false;
            //}

            //if (String.IsNullOrEmpty(ProductLocalRow.Model))
            //{
            //    mb = new FlexMessageBox();
            //    mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, ModelName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    ModelName.DescriptionInfo.Focus();
            //    return false;
            //}

            //if (String.IsNullOrEmpty(ProductLocalRow.BarCodeString))
            //{
            //    mb = new FlexMessageBox();
            //    mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, BarCode.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    BarCode.TextBox.Focus();
            //    return false;
            //}

            //if (String.IsNullOrEmpty(ProductLocalRow.Quantity.ToString()))
            //{
            //    mb = new FlexMessageBox();
            //    mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, QuantityName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    QuantityName.TextBox.Focus();
            //    return false;
            //}

            //if (String.IsNullOrEmpty(ProductLocalRow.TagPriceUSA.ToString()))
            //{
            //    mb = new FlexMessageBox();
            //    mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, TagPriceUSAName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    TagPriceUSAName.TextBox.Focus();
            //    return false;
            //}

            //if (String.IsNullOrEmpty(ProductLocalRow.TagPriceRUS.ToString()))
            //{
            //    mb = new FlexMessageBox();
            //    mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, TagPriceRUSName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    TagPriceRUSName.TextBox.Focus();
            //    return false;
            //}

            //if (String.IsNullOrEmpty(ProductLocalRow.SizeProduct))
            //{
            //    mb = new FlexMessageBox();
            //    mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, SizeProductName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
            //    SizeProductName.DescriptionInfo.Focus();
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
