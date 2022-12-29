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
        private String companyID;
        private String shopNumber;
        private String postCode;
        private String city;
        private String street;
        private String country;

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
        public string CompanyID
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

        public string PostCode
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

        public string City
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

        public string Street
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
        
        public string Country
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

            Country = "All";
            CreatedByUserID = "All";
            LastModifiedByUserID = "All";
            ShopNumber = "All";
            Phone = "All";
            Active = "All";
            PostCode = "All";
            City = "All";
            Street = "All";
            CompanyID = "All";
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
        private DateTime? lastModificatedDate;
        private String lastModificatedDateString;
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
        private String displayNameUser;
        private String lDisplayNameUser;
        private String shortDisplayNameUser;
        private String shortLDisplayNameUser;
        private String fullCompanyName;
        private String shortCompanyName;

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

        public DateTime? LastModificatedDate
        {
            get
            {
                return lastModificatedDate;
            }

            set
            {
                lastModificatedDate = value;
                OnPropertyChanged("LastModifiedDate");
            }
        }

        public String LastModificatedDateString
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

        public String DisplayNameUser
        {
            get
            {
                return displayNameUser;
            }

            set
            {
                displayNameUser = value;
                OnPropertyChanged("DisplayNameUser");
            }
        }

        public String LDisplayNameUser
        {
            get
            {
                return lDisplayNameUser;
            }

            set
            {
                lDisplayNameUser = value;
                OnPropertyChanged("LDisplayNameUser");
            }
        }
 
        public String ShortDisplayNameUser
        {
            get
            {
                return shortDisplayNameUser;
            }

            set
            {
                shortDisplayNameUser = value;
                OnPropertyChanged("ShortDisplayNameUser");
            }
        }

        public String ShortLDisplayNameUser
        {
            get
            {
                return shortLDisplayNameUser;
            }

            set
            {
                shortLDisplayNameUser = value;
                OnPropertyChanged("ShortLDisplayNameUser");
            }
        }

        public String FullCompanyName
        {
            get
            {
                return fullCompanyName;
            }

            set
            {
                fullCompanyName = value;
                OnPropertyChanged("FullCompanyName");
            }
        }

        public String ShortCompanyName
        {
            get
            {
                return shortCompanyName;
            }

            set
            {
                shortCompanyName = value;
                OnPropertyChanged("ShortCompanyName");
            }
        }
        public LocaleRow()
        {
            CreatedDate = DateTime.Now;
            LastModificatedDate = DateTime.Now;
            SyncDate = DateTime.Now;
            IsAddresRegister = false;
        }
    }

    public class RowSummary : INotifyPropertyChanged
    {
        Int32 summaryQuantityLine;
       
        public Int32 SummaryQuantityLine
        {
            get
            {
                return summaryQuantityLine;
            }

            set
            {
                summaryQuantityLine = value;
                OnPropertyChanged("SummaryQuantityLine");
            }
        }     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class ShopsLogic
    {
        Attributes attributes;
        ConvertData convertData;

        public DataTable innerList1;
        public DataTable innerList2;
        public DataTable innerList3;
        public DataTable innerList4;
        public DataTable innerList5;
        public DataTable innerList6;
        public DataTable innerList7;
        public DataTable innerList8;
        public DataTable innerList9;

        string get_store_procedure = "xp_GetShopTable";
        string get_filters_procedure = "xp_GetShopFilter";
        string get_summary_procedure = "xp_GetShopSummary";

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

        public class Range
        {
            public double min;
            public double max;
        }

        Dictionary<String, DataTable> filters;
        Dictionary<String, Range> filtersFromTo;

        public ShopsLogic(Attributes _attributes)
        {
            this.attributes = _attributes;
            convertData = new ConvertData();
            filters = new Dictionary<string, DataTable>();

            innerList1 = new DataTable();
            innerList2 = new DataTable();
            innerList3 = new DataTable();
            innerList4 = new DataTable();
            innerList5 = new DataTable();
            innerList6 = new DataTable();
            innerList7 = new DataTable();
            innerList8 = new DataTable();
            innerList9 = new DataTable();

            innerList1.Columns.Add("ID");
            innerList1.Columns.Add("IsChecked");
            innerList1.Columns.Add("Description");

            innerList2.Columns.Add("ID");
            innerList2.Columns.Add("IsChecked");
            innerList2.Columns.Add("Description");

            innerList3.Columns.Add("ID");
            innerList3.Columns.Add("IsChecked");
            innerList3.Columns.Add("Description");

            innerList4.Columns.Add("ID");
            innerList4.Columns.Add("IsChecked");
            innerList4.Columns.Add("Description");

            innerList5.Columns.Add("ID");
            innerList5.Columns.Add("IsChecked");
            innerList5.Columns.Add("Description");

            innerList6.Columns.Add("ID");
            innerList6.Columns.Add("IsChecked");
            innerList6.Columns.Add("Description");

            innerList7.Columns.Add("ID");
            innerList7.Columns.Add("IsChecked");
            innerList7.Columns.Add("Description");

            innerList8.Columns.Add("ID");
            innerList8.Columns.Add("IsChecked");
            innerList8.Columns.Add("Description");

            innerList9.Columns.Add("ID");
            innerList9.Columns.Add("IsChecked");
            innerList9.Columns.Add("Description");

            filters.Add("CreatedByUserID", innerList1);
            filters.Add("LastModifiedByUserID", innerList2);
            filters.Add("CompanyID", innerList3);
            filters.Add("Phone", innerList4);
            filters.Add("Active", innerList5);
            filters.Add("PostCode", innerList6);
            filters.Add("City", innerList7);
            filters.Add("Street", innerList8);
            filters.Add("Country", innerList9);

            _data = new DataTable();
            _datarow = new DataTable();
            shemaStorаge = new ShemaStorаge();

            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSelectFilters = new SQLCommanSelect();
            _sqlRequestSelectSummary = new SQLCommanSelect();
            _sqlRequestSave = new SQLCommanSelect();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.AddParametr("@p_Search", SqlDbType.NVarChar, 40);
            _sqlRequestSelect.SetParametrValue("@p_Search", String.Empty);

            _sqlRequestSelect.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_ID", 0);

            _sqlRequestSelect.AddParametr("@p_CreatedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_CreatedUserID", String.Empty);

            _sqlRequestSelect.AddParametr("@p_LastModifiedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID", String.Empty);

            _sqlRequestSelect.AddParametr("@p_CompanyID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_CompanyID", String.Empty);

            _sqlRequestSelect.AddParametr("@p_Phone", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_Phone", String.Empty);

            _sqlRequestSelect.AddParametr("@p_Active", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_Active", String.Empty);

            _sqlRequestSelect.AddParametr("@p_PostCode", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_PostCode", String.Empty);

            _sqlRequestSelect.AddParametr("@p_City", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_City", String.Empty);

            _sqlRequestSelect.AddParametr("@p_Street", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_Street", String.Empty);

            _sqlRequestSelect.AddParametr("@p_Country", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_Country", 0);

            _sqlRequestSelect.AddParametr("@p_PageNumber", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_PageNumber", 0);

            _sqlRequestSelect.AddParametr("@p_PagerowCount", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_PagerowCount", 0);

            _sqlRequestSelect.AddParametr("@p_SortColumn", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", 0);

            _sqlRequestSelect.AddParametr("@p_Sort", SqlDbType.Bit);
            _sqlRequestSelect.SetParametrValue("@p_Sort", String.Empty);
            //----------------------------------------------------------------------------

            _sqlRequestSelectSummary.AddParametr("@p_Search", SqlDbType.NVarChar);
            _sqlRequestSelectSummary.SetParametrValue("@p_Search", "");

            _sqlRequestSelectSummary.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSelectSummary.SetParametrValue("@p_ID", 0);

            _sqlRequestSelectSummary.AddParametr("@p_CreatedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_CreatedUserID", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_LastModifiedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_LastModifiedUserID", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_CompanyID", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_CompanyID", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_Phone", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_Phone", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_Active", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_Active", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_PostCode", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_PostCode", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_City", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_City", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_Street", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_Street", String.Empty);

            _sqlRequestSelectSummary.AddParametr("@p_Country", SqlDbType.NVarChar, 255);
            _sqlRequestSelectSummary.SetParametrValue("@p_Country", String.Empty);

            //----------------------------------------------------------------------------

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
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", _localFilter.ScreenTypeGrid);
            _sqlRequestSelect.SetParametrValue("@p_Search", _localFilter.Search);

            _sqlRequestSelect.SetParametrValue("@p_ID", _localFilter.ID);
            _sqlRequestSelect.SetParametrValue("@p_Active", _localFilter.Active);
            _sqlRequestSelect.SetParametrValue("@p_Phone", _localFilter.Phone);
            _sqlRequestSelect.SetParametrValue("@p_CompanyID", _localFilter.CompanyID);
            //_sqlRequestSelect.SetParametrValue("@p_ShopNumber", _localFilter.ShopNumber);
            _sqlRequestSelect.SetParametrValue("@p_CreatedUserID", _localFilter.CreatedByUserID);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID", _localFilter.LastModifiedByUserID);

            _sqlRequestSelect.SetParametrValue("@p_PostCode", _localFilter.PostCode);
            _sqlRequestSelect.SetParametrValue("@p_City", _localFilter.City);
            _sqlRequestSelect.SetParametrValue("@p_Street", _localFilter.Street);
            _sqlRequestSelect.SetParametrValue("@p_Country", _localFilter.Country);

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
        public DataTable FillGridAllFilter()
        {
            _sqlRequestSelectFilters.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelectFilters.ComplexRequest(get_filters_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelectFilters.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillSummary(LocaleFilter _localFilter)
        {
            _sqlRequestSelectSummary.SqlAnswer.datatable.Clear();
            _data.Clear();
           
            _sqlRequestSelectSummary.SetParametrValue("@p_Search", _localFilter.Search);

            _sqlRequestSelectSummary.SetParametrValue("@p_ID", _localFilter.ID);
            _sqlRequestSelectSummary.SetParametrValue("@p_Active", _localFilter.Active);
            _sqlRequestSelectSummary.SetParametrValue("@p_Phone", _localFilter.Phone);
            _sqlRequestSelectSummary.SetParametrValue("@p_CompanyID", _localFilter.CompanyID);
            //_sqlRequestSelectSummary.SetParametrValue("@p_ShopNumber", _localFilter.ShopNumber);
            _sqlRequestSelectSummary.SetParametrValue("@p_CreatedUserID", _localFilter.CreatedByUserID);
            _sqlRequestSelectSummary.SetParametrValue("@p_LastModifiedUserID", _localFilter.LastModifiedByUserID);

            _sqlRequestSelectSummary.SetParametrValue("@p_PostCode", _localFilter.PostCode);
            _sqlRequestSelectSummary.SetParametrValue("@p_City", _localFilter.City);
            _sqlRequestSelectSummary.SetParametrValue("@p_Street", _localFilter.Street);
            _sqlRequestSelectSummary.SetParametrValue("@p_Country", _localFilter.Country);

            _sqlRequestSelectSummary.ComplexRequest(get_summary_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelectSummary.SqlAnswer.datatable;
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
        public LocaleRow Convert(DataRow _dataRow, LocaleRow _localeRow)
        {
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("ID");
            _localeRow.ShopNumber = convertData.ConvertDataInt32("ShopNumber");
            _localeRow.CompanyID = convertData.ConvertDataInt32("CompanyID");
            _localeRow.FullCompanyName = convertData.ConvertDataString("FullCompanyName");
            _localeRow.ShortCompanyName = convertData.ConvertDataString("ShortCompanyName");
            _localeRow.Name = convertData.ConvertDataString("Name");
           
            _localeRow.Address = convertData.ConvertDataString("Address");
            _localeRow.Phone = convertData.ConvertDataString("Phone");
            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModificatedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModificatedDate);
            _localeRow.ReffTimeRow = convertData.ConvertDataString("ReffTimeRow");
            _localeRow.Active = convertData.ConvertDataBoolean("Active");
            _localeRow.Country = convertData.ConvertDataString("Country");
            _localeRow.City = convertData.ConvertDataString("City");
            _localeRow.Administrative = convertData.ConvertDataString("Administrative");
            _localeRow.Street = convertData.ConvertDataString("Street");
            _localeRow.Housenumber = convertData.ConvertDataString("Housenumber");
            _localeRow.PostCode = convertData.ConvertDataInt64("PostCode");

            _localeRow.Lng = convertData.ConvertDataDouble("Lng");
            _localeRow.Lat = convertData.ConvertDataDouble("Lat");
            _localeRow.DisplayNameUser = convertData.ConvertDataString("DisplayNameUser");
            _localeRow.ShortDisplayNameUser = convertData.ConvertDataString("ShortDisplayNameUser");
            _localeRow.LDisplayNameUser = convertData.ConvertDataString("LDisplayNameUser");
            _localeRow.ShortLDisplayNameUser = convertData.ConvertDataString("ShortLDisplayNameUser");
            
            return _localeRow;
        }

        //данные для суммы
        public void ConvertSummary(DataRow _dataRow, RowSummary _localeRow)
        {
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);
            _localeRow.SummaryQuantityLine = convertData.ConvertDataInt32("SummaryQuantityLine");
        }

        public DataTable GetFilter(String filterName)
        {
            return filters.FirstOrDefault(x => x.Key == filterName).Value;
        }

        public Range GetFromToFilter(String filterName)
        {
            return filtersFromTo.FirstOrDefault(x => x.Key == filterName).Value;
        }

        public void InitFilters()
        {
            DataTable table = FillGridAllFilter();
            FillFilter("CreatedByUserID", table);
            FillFilter("LastModifiedByUserID", table);
            FillFilter("CompanyID", table);
            FillFilter("Phone", table);
            FillFilter("Active", table);
            FillFilter("PostCode", table);
            FillFilter("City", table);
            FillFilter("Street", table);
            FillFilter("Country", table);

            // RowsFilters rowsFilters;
            foreach (DataRow row in table.Rows)
            {
                //rowsFilters = new RowsFilters();
                //ConvertFilter(row, rowsFilters);
                //filtersFromTo["Quantity"].min = rowsFilters.FiltersQuantityMin;
                //filtersFromTo["Quantity"].max = rowsFilters.FiltersQuantityMax;
                //filtersFromTo["Amount"].min = rowsFilters.FiltersAmountMin;
                //filtersFromTo["Amount"].max = rowsFilters.FiltersAmountMax;
            }
        }

        public void FillFilter(String filterName, DataTable table)
        {
            DataTable current1 = filters.FirstOrDefault(x => x.Key == filterName).Value;
            current1.Clear();
            foreach (DataRow row in table.Rows)
            {
                ConvertData convert = new ConvertData(row, new LocaleRow());
                if (!String.IsNullOrEmpty(row[filterName].ToString()))
                {
                    String data = convert.ConvertDataString(filterName);
                    String[] dataarray = data.Split('|');
                    foreach (string curstr in dataarray.ToList())
                    {
                        String[] pair = curstr.Split(':');
                        DataRow newrow = current1.NewRow();
                        newrow["ID"] = pair[0];
                        newrow["IsChecked"] = false;
                        newrow["Description"] = pair[1];
                        current1.Rows.Add(newrow);
                    }
                }
            }
        }
    }
}
