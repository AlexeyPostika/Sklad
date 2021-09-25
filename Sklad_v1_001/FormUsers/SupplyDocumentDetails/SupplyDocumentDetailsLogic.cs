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
        private Int64 documentID;
        private String name;
        private Int32 quantity;
        
        // цены      
        private Double tagPriceUSA;
        private Double currencyUSA;
        private Double tagPriceRUS;
        private Double currencyRUS;
        
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
        public double TagPriceUSA
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
        public double TagPriceRUS
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

            _sqlRequestSelect.AddParametr("@p_DocumentID", SqlDbType.BigInt);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", 0);

            _sqlRequestSelect.AddParametr("@p_ProductID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_ProductID", 0);
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

        public DataTable FillGrid(Int32 _productID)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeItem);
            _sqlRequestSelect.SetParametrValue("@p_ProductID", _productID);

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
            _localeRow.DocumentID = convertData.ConvertDataInt32("DocumentID");
            _localeRow.Name = convertData.ConvertDataString("Name");
            _localeRow.Quantity = convertData.ConvertDataInt32("Quantity");
           
            _localeRow.TagPriceUSA = convertData.ConvertDataDouble("TagPriceUSA");
            _localeRow.CurrencyUSA = convertData.ConvertDataInt32("CurrencyUSA");
            _localeRow.TagPriceRUS = convertData.ConvertDataDouble("TagPriceRUS");
            _localeRow.CurrencyRUS = convertData.ConvertDataDouble("CurrencyRUS");

            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModificatedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModificatedDate);
            _localeRow.CreatedUserID = convertData.ConvertDataInt32("CreatedUserID");
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("LastModificatedUserID");

            return _localeRow;
        }


    }
}
