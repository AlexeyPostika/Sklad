using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.Shops
{
    public class LocaleFilter : INotifyPropertyChanged
    {
        private string search;
        private Int32 iD;
        private string screenTypeGrid;

        private String active;
        private String phone;
        private String createdByUserID;
        private String lastModifiedByUserID;
        private String name;
        private String shopNumber;

        private Int32 filterCreatedDate;
        private DateTime? fromCreatedDate;
        private DateTime? toCreatedDate;

        private Int32 filterLastModifiedDate;
        private DateTime? fromLastModifiedDate;
        private DateTime? toLastModifiedDate;

        private String sortColumn;
        private Boolean sort;

        private Int32 pageNumber;
        private Int32 pagerowCount;

        public string Search
        {
            get
            {
                return search;
            }

            set
            {
                search = value;
                OnPropertyChanged("Search");
            }
        }

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

        public Int32 FilterCreatedDate
        {
            get
            {
                return filterCreatedDate;
            }

            set
            {
                filterCreatedDate = value;
                OnPropertyChanged("FilterCreatedDate");
            }
        }

        public DateTime? FromCreatedDate
        {
            get
            {
                return fromCreatedDate;
            }

            set
            {
                fromCreatedDate = value;
                OnPropertyChanged("FromCreatedDate");
            }
        }

        public DateTime? ToCreatedDate
        {
            get
            {
                return toCreatedDate;
            }

            set
            {
                toCreatedDate = value;
                OnPropertyChanged("ToCreatedDate");
            }
        }

        public Int32 FilterLastModifiedDate
        {
            get
            {
                return filterLastModifiedDate;
            }

            set
            {
                filterLastModifiedDate = value;
                OnPropertyChanged("FilterLastModifiedDate");
            }
        }

        public DateTime? FromLastModifiedDate
        {
            get
            {
                return fromLastModifiedDate;
            }

            set
            {
                fromLastModifiedDate = value;
                OnPropertyChanged("FromLastModifiedDate");
            }
        }

        public DateTime? ToLastModifiedDate
        {
            get
            {
                return toLastModifiedDate;
            }

            set
            {
                toLastModifiedDate = value;
                OnPropertyChanged("ToLastModifiedDate");
            }
        }
        //deliveryID
        public string Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
                OnPropertyChanged("Active");
            }
        }
        //managerUserID
        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
                OnPropertyChanged("Phone");
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

        public string ShopNumber
        {
            get
            {
                return shopNumber;
            }

            set
            {
                shopNumber = value;
                OnPropertyChanged("ShopNumber");
            }
        }
        public string CreatedByUserID
        {
            get
            {
                return createdByUserID;
            }

            set
            {
                createdByUserID = value;
                OnPropertyChanged("CreatedByUserID");
            }
        }

        public string LastModifiedByUserID
        {
            get
            {
                return lastModifiedByUserID;
            }

            set
            {
                lastModifiedByUserID = value;
                OnPropertyChanged("LastModifiedByUserID");
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

        public Int32 PageNumber
        {
            get
            {
                return pageNumber;
            }

            set
            {
                pageNumber = value;
                OnPropertyChanged("PageNumber");
            }
        }

        public Int32 PagerowCount
        {
            get
            {
                return pagerowCount;
            }

            set
            {
                pagerowCount = value;
                OnPropertyChanged("PagerowCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LocaleFilter()
        {
            ScreenTypeGrid = ScreenType.ScreenTypeGrid;
            Search = "";

            FromCreatedDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            ToCreatedDate = DateTime.Now;
            FromCreatedDate = FromCreatedDate?.AddSeconds(10);

            FromLastModifiedDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            ToLastModifiedDate = DateTime.Now;
            FromLastModifiedDate = FromLastModifiedDate?.AddSeconds(10);

            PageNumber = 0;
            PagerowCount = 16;
            Sort = true;
            SortColumn = "ID";

            Name = "All";
            CreatedByUserID = "All";
            LastModifiedByUserID = "All";
            ShopNumber = "All";
            Phone = "All";
            Active = "All";
        }

    }

    public class LocaleRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private Int32 addUserID;
        private Int32 iD ;
        private Int32 shopNumber ;
        private Int32 companyID ;
        private String name ;
        private String address ;
        private String phone ;
        private DateTime? createdDate;
        private String createdDateString;
        private DateTime? lastModifiedDate;
        private String lastModifiedDateString;
        private Int32 createdByUserID;
        private Int32 lastModifiedByUserID;
        private DateTime? syncDate ;
        private Int32 syncStatus ;
        private Boolean active;
        private Boolean isAddresRegister;
        private String country;
        private String city;
        private String administrative;
        private String street;
        private String housenumber;
        private Int64 postCode;
        private Double lat;
        private Double lng;

        [Timestamp]
        public Byte[] TimeRow { get; set; }
        public String ReffTimeRow { get; set; }
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
        public int AddUserID
        {
            get
            {
                return addUserID;
            }

            set
            {
                addUserID = value;
                OnPropertyChanged("AddUserID");
            }
        }
        public int ShopNumber
        {
            get
            {
                return shopNumber;
            }

            set
            {
                shopNumber = value;
                OnPropertyChanged("ShopNumber");
            }
        }
        public int CompanyID
        {
            get
            {
                return companyID;
            }

            set
            {
                companyID = value;
                OnPropertyChanged("CompanyID");
            }
        }
        public String Name
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
        public String Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public String Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
                OnPropertyChanged("Phone");
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

        public String CreatedDateString
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

        public DateTime? LastModifiedDate
        {
            get
            {
                return lastModifiedDate;
            }

            set
            {
                lastModifiedDate = value;
                OnPropertyChanged("LastModifiedDate");
            }
        }

        public String LastModifiedDateString
        {
            get
            {
                return lastModifiedDateString;
            }

            set
            {
                lastModifiedDateString = value;
                OnPropertyChanged("LastModifiedDateString");
            }
        }

        public Int32 CreatedByUserID
        {
            get
            {
                return createdByUserID;
            }

            set
            {
                createdByUserID = value;
                OnPropertyChanged("CreatedByUserID");
            }
        }

        public Int32 LastModifiedByUserID
        {
            get
            {
                return lastModifiedByUserID;
            }

            set
            {
                lastModifiedByUserID = value;
                OnPropertyChanged("LastModifiedByUserID");
            }
        }
        public DateTime? SyncDate
        {
            get
            {
                return syncDate;
            }

            set
            {
                syncDate = value;
                OnPropertyChanged("SyncDate");
            }
        }
        public int SyncStatus
        {
            get
            {
                return syncStatus;
            }

            set
            {
                syncStatus = value;
                OnPropertyChanged("SyncStatus");
            }
        }
        public Boolean Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
                OnPropertyChanged("Active");
            }
        }
        
        public Boolean IsAddresRegister
        {
            get
            {
                return isAddresRegister;
            }

            set
            {
                isAddresRegister = value;
                OnPropertyChanged("IsAddresRegister");
            }
        }

        public String Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        public String City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }

        public String Administrative
        {
            get
            {
                return administrative;
            }

            set
            {
                administrative = value;
                OnPropertyChanged("Administrative");
            }
        }

        public String Street
        {
            get
            {
                return street;
            }

            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }
 
        public String Housenumber
        {
            get
            {
                return housenumber;
            }

            set
            {
                housenumber = value;
                OnPropertyChanged("Housenumber");
            }
        }
      
        public Int64 PostCode
        {
            get
            {
                return postCode;
            }

            set
            {
                postCode = value;
                OnPropertyChanged("PostCode");
            }
        }

        public Double Lat
        {
            get
            {
                return lat;
            }

            set
            {
                lat = value;
                OnPropertyChanged("Lat");
            }
        }
       
        public Double Lng
        {
            get
            {
                return lng;
            }

            set
            {
                lng = value;
                OnPropertyChanged("Lng");
            }
        }
        public LocaleRow()
        {
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            SyncDate = DateTime.Now;
            IsAddresRegister = false;
        }
    }
    public class ShopsLogic
    {
        Attributes attributes;
        ConvertData convertData;

        string get_store_procedure = "";
        string get_filters_procedure = "";
        string get_summary_procedure = "";

        string get_save_procedure = "xp_SaveShop";

        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlRequestSelectFilters = null;
        SQLCommanSelect _sqlRequestSelectSummary = null;
        SQLCommanSelect _sqlRequestSave = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;

        //структура таблиц
        ShemaStorаge shemaStorаge;

        public ShopsLogic(Attributes _attributes)
        {
            this.attributes = _attributes;
            convertData = new ConvertData();

            _data = new DataTable();
            _datarow = new DataTable();
            shemaStorаge = new ShemaStorаge();

            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSelectFilters = new SQLCommanSelect();
            _sqlRequestSelectSummary = new SQLCommanSelect();
            _sqlRequestSave = new SQLCommanSelect();

            _sqlRequestSave.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_AddUserID", attributes.numeric.userEdit.AddUserID);

            _sqlRequestSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ID", 0);

            _sqlRequestSave.AddParametr("@p_ShopNumber", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ShopNumber", 0);

            _sqlRequestSave.AddParametr("@p_CompanyID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_CompanyID", attributes.numeric.attributeCompany.CompanyID);

            _sqlRequestSave.AddParametr("@p_Name", SqlDbType.NVarChar, 255);
            _sqlRequestSave.SetParametrValue("@p_Name", String.Empty);

            _sqlRequestSave.AddParametr("@p_Address", SqlDbType.NVarChar, 255);
            _sqlRequestSave.SetParametrValue("@p_Address", String.Empty);

            _sqlRequestSave.AddParametr("@p_Phone", SqlDbType.NVarChar, 40);
            _sqlRequestSave.SetParametrValue("@p_Phone", String.Empty);

            _sqlRequestSave.AddParametr("@p_CreatedUserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_CreatedUserID", 0);

            _sqlRequestSave.AddParametr("@p_LastModificatedDate", SqlDbType.DateTime);
            _sqlRequestSave.SetParametrValue("@p_LastModificatedDate", DateTime.Now);

            _sqlRequestSave.AddParametr("@p_LastModificatedUserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_LastModificatedUserID", 0);

            _sqlRequestSave.AddParametr("@p_ReffTimeRow", SqlDbType.NVarChar, 255);
            _sqlRequestSave.SetParametrValue("@p_ReffTimeRow", String.Empty);

            _sqlRequestSave.AddParametr("@p_Active", SqlDbType.Bit);
            _sqlRequestSave.SetParametrValue("@p_Active", 0);

            _sqlRequestSave.AddParametr("@p_isAddresRegister", SqlDbType.Bit);
            _sqlRequestSave.SetParametrValue("@p_isAddresRegister", 0);

            _sqlRequestSave.AddParametr("@p_Сountry", SqlDbType.NVarChar, 50);
            _sqlRequestSave.SetParametrValue("@p_Сountry", String.Empty);

            _sqlRequestSave.AddParametr("@p_City", SqlDbType.NVarChar, 50);
            _sqlRequestSave.SetParametrValue("@p_City", String.Empty);

            _sqlRequestSave.AddParametr("@p_Administrative", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Administrative", String.Empty);

            _sqlRequestSave.AddParametr("@p_Street", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Street", String.Empty);

            _sqlRequestSave.AddParametr("@p_Housenumber", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Housenumber", String.Empty);

            _sqlRequestSave.AddParametr("@p_PostCode", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_PostCode", 0);

            _sqlRequestSave.AddParametr("@p_Lat", SqlDbType.Money);
            _sqlRequestSave.SetParametrValue("@p_Lat", 0);
            
            _sqlRequestSave.AddParametr("@p_Lng", SqlDbType.Money);
            _sqlRequestSave.SetParametrValue("@p_Lng", 0);

        }

        public DataTable FillGrid(Int64 _supplyDocumentNumber)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ItemByStatus);
            _sqlRequestSelect.SetParametrValue("@p_Search", _supplyDocumentNumber);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGrid(LocaleFilter _localFilter)
        {
            /*
             *  private string ;
                private Int32 ;
                private string ;

                private String ;
                private String ;
                private String ;
                private String ;
                private String ;
                private String ;

                private Int32 filterCreatedDate;
                private DateTime? fromCreatedDate;
                private DateTime? toCreatedDate;

                private Int32 filterLastModifiedDate;
                private DateTime? fromLastModifiedDate;
                private DateTime? toLastModifiedDate;

                private String sortColumn;
                private Boolean sort;

                private Int32 pageNumber;
                private Int32 pagerowCount;

            */
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ItemByStatus);
            _sqlRequestSelect.SetParametrValue("@p_Search", _localFilter.Search);

            _sqlRequestSelect.SetParametrValue("@p_ID", _localFilter.ID);
            _sqlRequestSelect.SetParametrValue("@p_Active", _localFilter.Active);
            _sqlRequestSelect.SetParametrValue("@p_Phone", _localFilter.Phone);
            _sqlRequestSelect.SetParametrValue("@p_Name", _localFilter.Name);
            _sqlRequestSelect.SetParametrValue("@p_ShopNumber", _localFilter.ShopNumber);
            _sqlRequestSelect.SetParametrValue("@p_CreatedByUserID", _localFilter.CreatedByUserID);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedByUserID", _localFilter.LastModifiedByUserID);

            //_sqlRequestSelect.SetParametrValue("@p_FromCreatedDate", _localFilter.FromCreatedDate);
            //_sqlRequestSelect.SetParametrValue("@p_ToCreatedDate", _localFilter.ToCreatedDate);
            //_sqlRequestSelect.SetParametrValue("@p_FromLastModifiedDate", _localFilter.FromLastModifiedDate);
            //_sqlRequestSelect.SetParametrValue("@p_ToLastModifiedDate", _localFilter.ToLastModifiedDate);

            _sqlRequestSelect.SetParametrValue("@p_PageNumber", _localFilter.PageNumber);
            _sqlRequestSelect.SetParametrValue("@p_PagerowCount", _localFilter.PagerowCount);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", _localFilter.SortColumn);
            _sqlRequestSelect.SetParametrValue("@p_Sort", _localFilter.Sort); //тест github

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public Int32 SaveRow(LocaleRow _localeRow)
        {
            //SupplyDocument          
            _sqlRequestSave.SetParametrValue("@p_ID", _localeRow.ID);
            _sqlRequestSave.SetParametrValue("@p_ShopNumber", _localeRow.ShopNumber);
            _sqlRequestSave.SetParametrValue("@p_Name", _localeRow.Name);
            _sqlRequestSave.SetParametrValue("@p_Address", _localeRow.Address);
            _sqlRequestSave.SetParametrValue("@p_Phone", _localeRow.Phone);
            _sqlRequestSave.SetParametrValue("@p_ReffTimeRow", _localeRow.ReffTimeRow);
            _sqlRequestSave.SetParametrValue("@p_Active", _localeRow.Active);
            _sqlRequestSave.SetParametrValue("@p_isAddresRegister", _localeRow.IsAddresRegister);
            _sqlRequestSave.SetParametrValue("@p_Сountry", _localeRow.Country);
            _sqlRequestSave.SetParametrValue("@p_City", _localeRow.City);
            _sqlRequestSave.SetParametrValue("@p_Administrative", _localeRow.Administrative);
            _sqlRequestSave.SetParametrValue("@p_Street", _localeRow.Street);
            _sqlRequestSave.SetParametrValue("@p_Housenumber", _localeRow.Housenumber);
            _sqlRequestSave.SetParametrValue("@p_PostCode", _localeRow.PostCode);
            _sqlRequestSave.SetParametrValue("@p_Lat", _localeRow.Lat);
            _sqlRequestSave.SetParametrValue("@p_Lng", _localeRow.Lng);

            _sqlRequestSave.ComplexRequest(get_save_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSave.SqlAnswer.result;
        }
    }
}
