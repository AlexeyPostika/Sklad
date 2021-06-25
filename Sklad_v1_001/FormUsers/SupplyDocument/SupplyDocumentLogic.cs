using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SupplyDocument
{
    public class LocalFilter : INotifyPropertyChanged
    {
        private string search;
        private string screenTypeGrid;

        private String createdByUserID;
        private String lastModifiedByUserID;
        private String status;

        private Double quantityMin;
        private Double quantityMax;
        private Double tagPriceVATRUSMin;
        private Double tagPriceVATRUSMax;

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

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged("Status");
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

        public Double QuantityMin
        {
            get
            {
                return quantityMin;
            }

            set
            {
                quantityMin = value;
                OnPropertyChanged("QuantityMin");
            }
        }

        public Double QuantityMax
        {
            get
            {
                return quantityMax;
            }

            set
            {
                quantityMax = value;
                OnPropertyChanged("QuantityMax");
            }
        }

        public Double TagPriceVATRUSMin
        {
            get
            {
                return tagPriceVATRUSMin;
            }

            set
            {
                tagPriceVATRUSMin = value;
                OnPropertyChanged("TagPriceVATRUSMin");
            }
        }

        public Double TagPriceVATRUSMax
        {
            get
            {
                return tagPriceVATRUSMax;
            }

            set
            {
                tagPriceVATRUSMax = value;
                OnPropertyChanged("TagPriceVATRUSMax");
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

        public LocalFilter()
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
            Sort = true;
            SortColumn = "ID";

            Status = "All";
            CreatedByUserID = "All";
            LastModifiedByUserID = "All";
        }

    }

    public class LocalRow : INotifyPropertyChanged
    {
        private Int32 iD;
        private Int32 nameClientID;
        private Int32 status;
        private String delivery;
        private String managerName;
        private String tTN;
        private String invoice;
        private DateTime? createdDate;
        private String createdDateString;
        private Int32 createdUserID;
        private String createdUserIDString;
        private DateTime? lastModificatedDate;
        private String lastModificatedDateString;
        private Int32 lastModificatedUserID;
        private String lastModificatedUserIDString;
        private Int32 count;
        private Decimal amount;

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

        public Int32 NameClientID
        {
            get
            {
                return nameClientID;
            }

            set
            {
                nameClientID = value;
                OnPropertyChanged("NameClientID");
            }
        }

        public Int32 Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public string Delivery
        {
            get
            {
                return delivery;
            }

            set
            {
                delivery = value;
                OnPropertyChanged("Delivery");
            }
        }

        public string ManagerName
        {
            get
            {
                return managerName;
            }

            set
            {
                managerName = value;
                OnPropertyChanged("ManagerName");
            }
        }

        public string TTN
        {
            get
            {
                return tTN;
            }

            set
            {
                tTN = value;
                OnPropertyChanged("TTN");
            }
        }

        public string Invoice
        {
            get
            {
                return invoice;
            }

            set
            {
                invoice = value;
                OnPropertyChanged("Invoice");
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
        public Int32 Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public Decimal Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Summary : INotifyPropertyChanged
    {
        private Int32 summaryQuantityLine;
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

    public class SupplyDocumentLogic
    {
        string get_store_procedure = "xp_GetSupplyDocumentTable";
        string get_filters_procedure = "xp_GetSupplyDocumentFilter";
        string get_summary_procedure = "xp_GetSupplyDocumentSummary";

        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlRequestSelectFilters = null;
        SQLCommanSelect _sqlRequestSelectSummary = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;

        public SupplyDocumentLogic()
        {
            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSelectFilters = new SQLCommanSelect();
            _sqlRequestSelectSummary = new SQLCommanSelect();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 40);
            _sqlRequestSelect.SetParametrValue("@@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.AddParametr("@p_Search", SqlDbType.NVarChar, 40);
            _sqlRequestSelect.SetParametrValue("@p_Search", "");

            _sqlRequestSelect.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_ID", 0);
                      
            _sqlRequestSelect.AddParametr("@p_CreatedUserID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_CreatedUserID", 0);

            _sqlRequestSelect.AddParametr("@p_LastModifiedUserID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID", 0);

            _sqlRequestSelect.AddParametr("@p_Status", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_Status", "");

            _sqlRequestSelect.AddParametr("@p_Quantity_Min", SqlDbType.Money);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Min", 0);

            _sqlRequestSelect.AddParametr("@p_Quantity_Max", SqlDbType.Money);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Max", SqlMoney.MaxValue);
           
            _sqlRequestSelect.AddParametr("@p_TagPriceVATRUS_Min", SqlDbType.Money);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Min", System.Data.SqlTypes.SqlMoney.MaxValue);

            _sqlRequestSelect.AddParametr("@p_TagPriceVATRUS_Max", SqlDbType.Money);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Max", System.Data.SqlTypes.SqlMoney.MaxValue);

            _sqlRequestSelect.AddParametr("@p_FromCreatedDate", SqlDbType.DateTime);
            _sqlRequestSelect.SetParametrValue("@p_FromCreatedDate", SqlDateTime.MinValue);

            _sqlRequestSelect.AddParametr("@p_ToCreatedDate", SqlDbType.DateTime);
            _sqlRequestSelect.SetParametrValue("@p_ToCreatedDate", DateTime.Now);

            _sqlRequestSelect.AddParametr("@p_FromLastModifiedDate", SqlDbType.DateTime);
            _sqlRequestSelect.SetParametrValue("@p_FromLastModifiedDate", SqlDateTime.MinValue);

            _sqlRequestSelect.AddParametr("@p_ToLastModifiedDate", SqlDbType.DateTime);
            _sqlRequestSelect.SetParametrValue("@p_ToLastModifiedDate", DateTime.Now);

            _sqlRequestSelect.AddParametr("@p_PageNumber", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_PageNumber", 0);

            _sqlRequestSelect.AddParametr("@p_PagerowCount", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_PagerowCount", 0);

            _sqlRequestSelect.AddParametr("@p_SortColumn", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", 0);

            _sqlRequestSelect.AddParametr("@p_Sort", SqlDbType.Bit);
            _sqlRequestSelect.SetParametrValue("@p_Sort", 0);
            //----------------------------------------------------------------------------

            _sqlRequestSelectSummary.AddParametr("@p_Search", SqlDbType.Int);
            _sqlRequestSelectSummary.SetParametrValue("@p_Search", "");

            _sqlRequestSelectSummary.AddParametr("@p_CreatedUserID", SqlDbType.Int);
            _sqlRequestSelectSummary.SetParametrValue("@p_CreatedUserID", 0);

            _sqlRequestSelectSummary.AddParametr("@p_LastModifiedUserID", SqlDbType.Int);
            _sqlRequestSelectSummary.SetParametrValue("@p_LastModifiedUserID", 0);

            _sqlRequestSelectSummary.AddParametr("@p_Status", SqlDbType.NVarChar);
            _sqlRequestSelectSummary.SetParametrValue("@p_Status", "");

            _sqlRequestSelectSummary.AddParametr("@p_FromCreatedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromCreatedDate", SqlDateTime.MinValue);

            _sqlRequestSelectSummary.AddParametr("@p_ToCreatedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToCreatedDate", DateTime.Now);

            _sqlRequestSelectSummary.AddParametr("@p_FromLastModifiedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromLastModifiedDate", SqlDateTime.MinValue);

            _sqlRequestSelectSummary.AddParametr("@p_ToLastModifiedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToLastModifiedDate", DateTime.Now);

            _sqlRequestSelectSummary.AddParametr("@p_Quantity_Min", SqlDbType.Money);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Min", 0);

            _sqlRequestSelectSummary.AddParametr("@p_Quantity_Max", SqlDbType.Money);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Max", SqlMoney.MaxValue);

            _sqlRequestSelectSummary.AddParametr("@p_TagPriceVATRUS_Min", SqlDbType.Money);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Min", System.Data.SqlTypes.SqlMoney.MaxValue);

            _sqlRequestSelectSummary.AddParametr("@p_TagPriceVATRUS_Max", SqlDbType.Money);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Max", System.Data.SqlTypes.SqlMoney.MaxValue);
            //----------------------------------------------------------------------------
        }

        public DataTable FillGrid()
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGrid(Int32 id)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);
            _sqlRequestSelect.SetParametrValue("@p_ID", id);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGrid(LocalFilter _localFilter)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelect.SetParametrValue("@p_TypeScreen" , ScreenType.ScreenTypeGrid);
            _sqlRequestSelect.SetParametrValue("@p_Search", _localFilter.Search);
            _sqlRequestSelect.SetParametrValue("@p_CreatedUserID ", _localFilter.CreatedByUserID);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID ", _localFilter.LastModifiedByUserID);
            _sqlRequestSelect.SetParametrValue("@p_Status", _localFilter.Status);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Min", _localFilter.QuantityMin);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Max ", _localFilter.QuantityMax);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Min", _localFilter.TagPriceVATRUSMin);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Max ", _localFilter.TagPriceVATRUSMax);
            _sqlRequestSelect.SetParametrValue("@p_FromCreatedDate ", _localFilter.FromCreatedDate);
            _sqlRequestSelect.SetParametrValue("@p_ToCreatedDate ", _localFilter.ToCreatedDate);
            _sqlRequestSelect.SetParametrValue("@p_FromLastModifiedDate ", _localFilter.FromLastModifiedDate);
            _sqlRequestSelect.SetParametrValue("@p_ToLastModifiedDate ", _localFilter.ToLastModifiedDate);
            _sqlRequestSelect.SetParametrValue("@p_PageNumber ", _localFilter.PageNumber);
            _sqlRequestSelect.SetParametrValue("@p_PagerowCount ", _localFilter.PagerowCount);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn ", _localFilter.SortColumn);
            _sqlRequestSelect.SetParametrValue("@p_Sort", _localFilter.Sort);

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

        public DataTable FillSummary(LocalFilter _localFilter)
        {
            _sqlRequestSelectSummary.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelectSummary.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);
            _sqlRequestSelectSummary.SetParametrValue("@p_Search", _localFilter.Search);
            _sqlRequestSelectSummary.SetParametrValue("@p_CreatedUserID ", _localFilter.CreatedByUserID);
            _sqlRequestSelectSummary.SetParametrValue("@p_LastModifiedUserID ", _localFilter.LastModifiedByUserID);
            _sqlRequestSelectSummary.SetParametrValue("@p_Status", _localFilter.Status);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Min", _localFilter.QuantityMin);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Max ", _localFilter.QuantityMax);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Min", _localFilter.TagPriceVATRUSMin);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Max ", _localFilter.TagPriceVATRUSMax);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromCreatedDate ", _localFilter.FromCreatedDate);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToCreatedDate ", _localFilter.ToCreatedDate);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromLastModifiedDate ", _localFilter.FromLastModifiedDate);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToLastModifiedDate ", _localFilter.ToLastModifiedDate);

            _sqlRequestSelectSummary.ComplexRequest(get_summary_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelectSummary.SqlAnswer.datatable;
            return _data;
        }
    }
}
