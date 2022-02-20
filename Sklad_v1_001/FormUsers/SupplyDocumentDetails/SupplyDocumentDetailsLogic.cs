using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sklad_v1_001.FormUsers.SupplyDocumentDetails
{
    public class LocaleFilter : INotifyPropertyChanged
    {
        Int32 documentID;
        Int32 productID;
        string typeScreen;

        public Int32 DocumentID
        {
            get
            {
                return documentID;
            }

            set
            {
                documentID = value;
                OnPropertyChanged("DocumentID");
            }
        }

        public string TypeScreen
        {
            get
            {
                return typeScreen;
            }

            set
            {
                typeScreen = value;
                OnPropertyChanged("TypeScreen");
            }
        }

        public int ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                productID = value;
                OnPropertyChanged("ProductID");
            }
        }

        public LocaleFilter()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class LocaleRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // основная инфоррмация
        private Int32 iD;
        private Int32 tempID;
        private Int64 documentID;
        private String name;
        private Int32 quantity;
        private Byte[] imageProduct;
        private Int32 categoryID;
        private String categoryName;
        private String categoryDescription;
        private Int32 categoryDetailsID;
        private String categoryDetailsName;
        private String categoryDetailsDescription;

        // цены      
        private Decimal tagPriceUSA;
        private Double currencyUSA;
        private Decimal tagPriceRUS;
        private Double currencyRUS;
        private Boolean package;
        private String sizeProduct;
        private String model;
        private ImageSource imageSourcePackage;
        private String barCodeString;

        //стандартные поля
        private DateTime? createdDate;
        private String createdDateString;
        private Int32 createdUserID;
        private String createdUserIDString;
        private DateTime? lastModificatedDate;
        private String lastModificatedDateString;
        private Int32 lastModificatedUserID;
        private String lastModificatedUserIDString;

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
                OnPropertyChanged("ID");
            }
        }

        public Int32 TempID
        {
            get
            {
                return tempID;
            }

            set
            {
                tempID = value;
                OnPropertyChanged("TempID");
            }
        }

        public long DocumentID
        {
            get
            {
                return documentID;
            }

            set
            {
                documentID = value;
                OnPropertyChanged("DocumentID");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public Decimal TagPriceUSA
        {
            get
            {
                return tagPriceUSA;
            }

            set
            {
                tagPriceUSA = value;
                OnPropertyChanged("TagPriceUSA");
            }
        }
        public double CurrencyUSA
        {
            get
            {
                return currencyUSA;
            }

            set
            {
                currencyUSA = value;
                OnPropertyChanged("CurrencyUSA");
            }
        }
        public Decimal TagPriceRUS
        {
            get
            {
                return tagPriceRUS;
            }

            set
            {
                tagPriceRUS = value;
                OnPropertyChanged("TagPriceRUS");
            }
        }
        public double CurrencyRUS
        {
            get
            {
                return currencyRUS;
            }

            set
            {
                currencyRUS = value;
                OnPropertyChanged("CurrencyRUS");
            }
        }

        public Boolean Package
        {
            get
            {
                return package;
            }

            set
            {
                package = value;
                if (package)
                {
                    imageSourcePackage = ImageHelper.GenerateImage("IconPackage_x16.png");
                }
                else
                {
                    imageSourcePackage = ImageHelper.GenerateImage("IconMinus.png");
                }
                OnPropertyChanged("Package");
            }
        }
       
        public string SizeProduct
        {
            get
            {
                return sizeProduct;
            }

            set
            {
                sizeProduct = value;
                OnPropertyChanged("SizeProduct");
            }
        }

        public string Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public ImageSource ImageSourcePackage
        {
            get
            {
                return imageSourcePackage;
            }

            set
            {
                imageSourcePackage = value;
                OnPropertyChanged("ImageSourcePackage");
            }
        }

        public string BarCodeString
        {
            get
            {
                return barCodeString;
            }

            set
            {
                barCodeString = value;
                OnPropertyChanged("BarCodeString");
            }
        }

        public DateTime? CreatedDate
        {
            get
            {
                return createdDate;
            }

            set
            {
                createdDate = value;
                OnPropertyChanged("CreatedDate");
            }
        }
        public string CreatedDateString
        {
            get
            {
                return createdDateString;
            }

            set
            {
                createdDateString = value;
                OnPropertyChanged("CreatedDateString");
            }
        }
        public int CreatedUserID
        {
            get
            {
                return createdUserID;
            }

            set
            {
                createdUserID = value;
                OnPropertyChanged("CreatedUserID");
            }
        }
        public string CreatedUserIDString
        {
            get
            {
                return createdUserIDString;
            }

            set
            {
                createdUserIDString = value;
                OnPropertyChanged("CreatedUserIDString");
            }
        }
        public DateTime? LastModificatedDate
        {
            get
            {
                return lastModificatedDate;
            }

            set
            {
                lastModificatedDate = value;
                OnPropertyChanged("LastModificatedDate");
            }
        }
        public string LastModificatedDateString
        {
            get
            {
                return lastModificatedDateString;
            }

            set
            {
                lastModificatedDateString = value;
                OnPropertyChanged("LastModificatedDateString");
            }
        }
        public int LastModificatedUserID
        {
            get
            {
                return lastModificatedUserID;
            }

            set
            {
                lastModificatedUserID = value;
                OnPropertyChanged("LastModificatedUserID");
            }
        }
        public string LastModificatedUserIDString
        {
            get
            {
                return lastModificatedUserIDString;
            }

            set
            {
                lastModificatedUserIDString = value;
                OnPropertyChanged("LastModificatedUserIDString");
            }
        }

        public byte[] ImageProduct
        {
            get
            {
                return imageProduct;
            }

            set
            {
                imageProduct = value;
                OnPropertyChanged("ImageProduct");
            }
        }

        public int CategoryID
        {
            get
            {
                return categoryID;
            }

            set
            {
                categoryID = value;
                OnPropertyChanged("CategoryID");
            }
        }
        public String CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public string CategoryDescription
        {
            get
            {
                return categoryDescription;
            }

            set
            {
                categoryDescription = value;
                OnPropertyChanged("CategoryDescription");
            }
        }
        public int CategoryDetailsID
        {
            get
            {
                return categoryDetailsID;
            }

            set
            {
                categoryDetailsID = value;
                OnPropertyChanged("CategoryDetailsID");
            }
        }
        public string CategoryDetailsName
        {
            get
            {
                return categoryDetailsName;
            }

            set
            {
                categoryDetailsName = value;
                OnPropertyChanged("CategoryDetailsName");
            }
        }
        public string CategoryDetailsDescription
        {
            get
            {
                return categoryDetailsDescription;
            }

            set
            {
                categoryDetailsDescription = value;
                OnPropertyChanged("CategoryDetailsDescription");
            }
        }
    }

    public class SupplyDocumentDetailsLogic
    {
        string get_store_procedure = "xp_GetSupplyDocumentDetailsTable";

        // запрос
        SQLCommanSelect _sqlRequestSelect = null;
        
        // результаты запроса
        DataTable _data = null;
        DataTable _datarow = null;

        public SupplyDocumentDetailsLogic()
        {
            _sqlRequestSelect = new SQLCommanSelect();


            _data = new DataTable();
            _datarow = new DataTable();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);          

            _sqlRequestSelect.AddParametr("@p_ProductID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_ProductID", 0);
            
            _sqlRequestSelect.AddParametr("@p_DocumentID", SqlDbType.BigInt);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", 0);
        }

        public DataTable FillGrid()
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGridDocument(Int32 _documentID)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);
            _sqlRequestSelect.SetParametrValue("@p_ProductID", 0);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", _documentID);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGridProduct(Int32 _productID)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);
            _sqlRequestSelect.SetParametrValue("@p_ProductID", _productID);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", 0);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGrid(LocaleFilter filter)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", filter.TypeScreen);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", filter.DocumentID);
            _sqlRequestSelect.SetParametrValue("@p_ProductID", filter.ProductID);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public LocaleRow Convert(DataRow _dataRow, LocaleRow _localeRow)
        {
           // SaleDocumentDetailsList statusList = new SaleDocumentDetailsList();
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);
           
            _localeRow.ID = convertData.ConvertDataInt32("ID");
            _localeRow.TempID = convertData.ConvertDataInt32("ID");
            _localeRow.DocumentID = convertData.ConvertDataInt32("DocumentID");
            _localeRow.Name = convertData.ConvertDataString("Name");
            _localeRow.Quantity = convertData.ConvertDataInt32("Quantity");
           
            _localeRow.TagPriceUSA = convertData.ConvertDataDecimal("TagPriceUSA");
            _localeRow.CurrencyUSA = convertData.ConvertDataInt32("CurrencyUSA");
            _localeRow.TagPriceRUS = convertData.ConvertDataDecimal("TagPriceRUS");
            _localeRow.CurrencyRUS = convertData.ConvertDataDouble("CurrencyRUS");
            _localeRow.CategoryID= convertData.ConvertDataInt32("CategoryID");
            _localeRow.CategoryName = convertData.ConvertDataString("CategoryName");
            _localeRow.CategoryDetailsID = convertData.ConvertDataInt32("CategoryDetailsID");
            _localeRow.CategoryDetailsName = convertData.ConvertDataString("CategoryDetailsName");

            _localeRow.Model = convertData.ConvertDataString("Model");
            _localeRow.SizeProduct = convertData.ConvertDataString("SizeProduct");
            _localeRow.Package = convertData.ConvertDataBoolean("Size");
            _localeRow.BarCodeString= convertData.ConvertDataString("BarCodeString");

            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModificatedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModificatedDate);
            _localeRow.CreatedUserID = convertData.ConvertDataInt32("CreatedUserID");
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("LastModificatedUserID");

            return _localeRow;
        }

        public LocaleRow ConvertProductToSupplyDocumentDetails(Product.LocaleRow _row, LocaleRow _localeRow)
        {
            // SaleDocumentDetailsList statusList = new SaleDocumentDetailsList();
            ConvertData convertData = new ConvertData();         
           
            //категории
            _localeRow.CategoryID = _row.CategoryID;
            _localeRow.CategoryName = _row.CategoryName;
            _localeRow.CategoryDescription = _row.CategoryDescription;
            //подкатегории
            _localeRow.CategoryDetailsID = _row.CategoryDetailsID;
            _localeRow.CategoryDetailsName = _row.CategoryDetailsName;
            _localeRow.CategoryDetailsDescription = _row.CategoryDetailsDescription;
            
            //продукт
            //_localeRow.ID = _row.ID;
            _localeRow.Name = _row.Name;
            _localeRow.Quantity = _row.Quantity;
            _localeRow.TagPriceUSA = _row.TagPriceUSA;
            _localeRow.CurrencyUSA = 841;
            _localeRow.TagPriceRUS = _row.TagPriceRUS;
            _localeRow.CurrencyRUS = 663;
            _localeRow.Package = _row.Package;
            _localeRow.Model = _row.Model;
            _localeRow.SizeProduct = _row.SizeProduct;
            _localeRow.BarCodeString = _row.BarCodeString;

            //стандартные данные
            if (_localeRow.ID == 0)
            {
                _localeRow.CreatedDate = DateTime.Now;
                _localeRow.LastModificatedDate = DateTime.Now;
            }
            else
            {
                _localeRow.CreatedDate = _row.CreatedDate;
                _localeRow.LastModificatedDate = _row.LastModicatedDate;
            }         
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);         
            _localeRow.LastModificatedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModificatedDate);

            _localeRow.CreatedUserID = _row.CreatedUserID;
            _localeRow.LastModificatedUserID = _row.LastModificatedUserID;

            return _localeRow;
        }

        public Product.LocaleRow ConvertSupplyDocumentDetailsToProduct(Product.LocaleRow _row, LocaleRow _localeRow)
        {
            // SaleDocumentDetailsList statusList = new SaleDocumentDetailsList();
            ConvertData convertData = new ConvertData();
            _row.ID = _localeRow.ID;
            //категории
            _row.CategoryID = _localeRow.CategoryID;
            _row.CategoryName = _localeRow.CategoryName;
            _row.CategoryDescription = _localeRow.CategoryDescription;
            //подкатегории
            _row.CategoryDetailsID = _localeRow.CategoryDetailsID;
            _row.CategoryDetailsName = _localeRow.CategoryDetailsName;
            _row.CategoryDetailsDescription = _localeRow.CategoryDetailsDescription;

            //продукт
            //_localeRow.ID = _row.ID;
            _row.Name = _localeRow.Name;
            _row.Quantity = _localeRow.Quantity;
            _row.TagPriceUSA = _localeRow.TagPriceUSA;           
            _row.TagPriceRUS = _localeRow.TagPriceRUS;          
            _row.Package = _localeRow.Package;
            _row.RadioType = _localeRow.Package ? 1 : 2;
            _row.Model = _localeRow.Model;
            _row.SizeProduct = _localeRow.SizeProduct;
            _row.BarCodeString = _localeRow.BarCodeString;

            //стандартные данные
            if (_localeRow.ID == 0)
            {
                _row.CreatedDate = DateTime.Now;          
            }
            else
            {
                _row.CreatedDate = _localeRow.CreatedDate;
                _row.LastModicatedDate = _localeRow.LastModificatedDate;
            }         

            _row.CreatedUserID = _localeRow.CreatedUserID;
            _row.LastModificatedUserID = _localeRow.LastModificatedUserID;

            return _row;
        }
    }
}
