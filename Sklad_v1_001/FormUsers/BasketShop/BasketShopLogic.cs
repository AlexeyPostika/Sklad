using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.BasketShop
{
    public class LocalFilter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Int32 userID;
        private string screenTypeGrid;
        private String sortColumn;
        private Boolean sort;
        public Int32 UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }
        public string ScreenTypeGrid
        {
            get
            {
                return screenTypeGrid;
            }

            set
            {
                screenTypeGrid = value;
                OnPropertyChanged("ScreenTypeGrid");
            }
        }
        public string SortColumn
        {
            get
            {
                return sortColumn;
            }

            set
            {
                sortColumn = value;
                OnPropertyChanged("SortColumn");
            }
        }

        public Boolean Sort
        {
            get
            {
                return sort;
            }

            set
            {
                sort = value;
                OnPropertyChanged("Sort");
            }
        }
        public LocalFilter()
        {
            ScreenTypeGrid = ScreenType.ScreenTypeGrid;
            Sort = true;
            SortColumn = "ID";
        }
    }

    public class LocalRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ShemaStorаge shemaStorаgeLocal;

        private Int32 iD;
        private Int32 userID;
        private Int32 productID;
        private Int32 quantity;
        private DateTime? createdDate;
        private String createdDateString;
        private DateTime? lastModificatedDate;
        private String lastModificatedDateString;
        private Int32 createdUserID;
        private Int32 lastModificatedUserID;
        private Int32 saleDocumentID;

        private Boolean newDocumentBasketShop;

        public ShemaStorаge ShemaStorаgeLocal { get => shemaStorаgeLocal; set => shemaStorаgeLocal = value; }

        public Int32 ID
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

        public Int32 UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }

        public Int32 ProductID
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
        public Int32 Quantity
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

        public Int32 CreatedUserID
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

        public Int32 LastModificatedUserID
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

        public Int32 SaleDocumentID
        {
            get
            {
                return saleDocumentID;
            }

            set
            {
                saleDocumentID = value;
                OnPropertyChanged("SaleDocumentID");
            }
        }

        public Boolean NewDocumentBasketShop
        {
            get
            {
                return newDocumentBasketShop;
            }

            set
            {
                newDocumentBasketShop = value;
                OnPropertyChanged("NewDocumentBasketShop");
            }
        }
        public LocalRow()
        {
            ShemaStorаgeLocal = new ShemaStorаge();
        }
    }

    public class BasketShopLogic
    {
        Attributes attributes;
        ConvertData convertData;

        string get_store_procedure = "xp_GetBasketShopTable";
        string save_store_procedure = "xp_SaveBasketShopTable";

        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlRequestSave = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;

        public BasketShopLogic(Attributes _attributes)
        {
            this.attributes = _attributes;
            convertData = new ConvertData();

            _data = new DataTable();
            _datarow = new DataTable();

            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSave = new SQLCommanSelect();

            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.AddParametr("@p_UserID", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_UserID", attributes.numeric.userEdit.AddUserID.ToString());

            _sqlRequestSelect.AddParametr("@p_SortColumn", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", 0);

            _sqlRequestSelect.AddParametr("@p_Sort", SqlDbType.Bit);
            _sqlRequestSelect.SetParametrValue("@p_Sort", 0);

            //----------------------------------------------------------------------------
            _sqlRequestSave.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_AddUserID", attributes.numeric.userEdit.AddUserID);

            _sqlRequestSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ID", 0);

            _sqlRequestSave.AddParametr("@p_tableDetails", SqlDbType.Structured);
            _sqlRequestSave.SetParametrValue("@p_tableDetails", new DataTable());

        }

        public DataTable FillGrid()
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
         
            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public Int32 SaveRow(ShemaStorаge _shemaStorаge, Int32 _id = 0)
        {           
            _sqlRequestSave.SetParametrValue("@p_ID", _id);
            _sqlRequestSave.SetParametrValue("@p_tableDetails", _shemaStorаge.BasketShop);

            _sqlRequestSave.ComplexRequest(save_store_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSave.SqlAnswer.result;
        }

        public LocalRow Convert(DataRow _dataRow, LocalRow _localeRow)
        {          
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("ID");
                    
            _localeRow.UserID = convertData.ConvertDataInt32("UserID");
            _localeRow.ProductID = convertData.ConvertDataInt32("ProductID");
            _localeRow.Quantity = convertData.ConvertDataInt32("Quantity");
           
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
