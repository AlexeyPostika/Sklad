using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Sklad_v1_001.FormUsers.Shops
{
    /// <summary>
    /// Логика взаимодействия для ShopsGrid.xaml
    /// </summary>
    public partial class ShopsGrid : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        Attributes attributes;

        ConvertData convertData;
        LocaleRow shopRow;
        LocaleFilter filter;
        ObservableCollection<LocaleRow> dataShopList;
        ShopsLogic shopsLogic;
        RowSummary summary;

        DataTable filterCreatedByUserID;
        DataTable filterLastModifiedByUserID;
        DataTable filterCompanyID;
        DataTable filterPhone;
        DataTable filterActive;
        DataTable filterPostCode;
        DataTable filterCity;
        DataTable filterStreet;
        DataTable filterCountry;

        Int32 filterDateIDLastModifiadDate;
        DateTime? fromLastModifiadDate;
        DateTime? toLastModifiadDate;

        Boolean isAllowFilter;
        Boolean isPaginator;

        BitmapImage clearfilterActive;
        BitmapImage clearfilterShopID;
        BitmapImage clearfilterPostCode;
        BitmapImage clearfilterUserName;
        BitmapImage clearfilterCityID;

        private Boolean isEnableBack;
        private Boolean isEnableNext;
        private Boolean isEnableBackIn;
        private Boolean isEnableNextEnd;
        private String textOnWhatPage;

        Int32 currentPage;
        Int32 totalCount;
        Int32 pageCount;

        public bool IsEnableBack
        {
            get
            {
                return isEnableBack;
            }

            set
            {
                isEnableBack = value;
                OnPropertyChanged("IsEnableBack");
            }
        }

        public bool IsEnableNext
        {
            get
            {
                return isEnableNext;
            }

            set
            {
                isEnableNext = value;
                OnPropertyChanged("IsEnableNext");
            }
        }

        public String TextOnWhatPage
        {
            get
            {
                return textOnWhatPage;
            }

            set
            {
                textOnWhatPage = value;
                OnPropertyChanged("TextOnWhatPage");
            }
        }

        public bool IsEnableBackIn
        {
            get
            {
                return isEnableBackIn;
            }

            set
            {
                isEnableBackIn = value;
                OnPropertyChanged("IsEnableBackIn");
            }
        }

        public bool IsEnableNextEnd
        {
            get
            {
                return isEnableNextEnd;
            }

            set
            {
                isEnableNextEnd = value;
                OnPropertyChanged("IsEnableNextEnd");
            }
        }

        public DataTable FilterCreatedByUserID
        {
            get
            {
                return filterCreatedByUserID;
            }

            set
            {
                filterCreatedByUserID = value;
                OnPropertyChanged("FilterCreatedByUserID");
            }
        }
        public DataTable FilterLastModifiedByUserID
        {
            get
            {
                return filterLastModifiedByUserID;
            }

            set
            {
                filterLastModifiedByUserID = value;
                OnPropertyChanged("FilterLastModifiedByUserID");
            }

        }
        public DataTable FilterCompanyID
        {
            get
            {
                return filterCompanyID;
            }

            set
            {
                filterCompanyID = value;
                OnPropertyChanged("FilterCompanyID");
            }
        }

        public DataTable FilterPhone
        {
            get
            {
                return filterPhone;
            }

            set
            {
                filterPhone = value;
                OnPropertyChanged("FilterPhone");
            }
        }
        public DataTable FilterActive
        {
            get
            {
                return filterActive;
            }

            set
            {
                filterActive = value;
                OnPropertyChanged("FilterActive");
            }
        }

        public DataTable FilterPostCode
        {
            get
            {
                return filterPostCode;
            }

            set
            {
                filterPostCode = value;
                OnPropertyChanged("FilterPostCode");
            }
        }
        public DataTable FilterCity
        {
            get
            {
                return filterCity;
            }

            set
            {
                filterCity = value;
                OnPropertyChanged("FilterCity");
            }
        }
        public DataTable FilterStreet
        {
            get
            {
                return filterStreet;
            }

            set
            {
                filterStreet = value;
                OnPropertyChanged("FilterStreet");
            }
        }
        public DataTable FilterCountry
        {
            get
            {
                return filterCountry;
            }

            set
            {
                filterCountry = value;
                OnPropertyChanged("FilterCountry");
            }
        }
        public int FilterDateIDLastModifiadDate
        {
            get
            {
                return filterDateIDLastModifiadDate;
            }

            set
            {
                filterDateIDLastModifiadDate = value;
                OnPropertyChanged("FilterDateIDLastModifiadDate");
            }
        }
        public DateTime? FromLastModifiadDate
        {
            get
            {
                return fromLastModifiadDate;
            }

            set
            {
                fromLastModifiadDate = value;
                OnPropertyChanged("FromLastModifiadDate");
            }
        }
        public DateTime? ToLastModifiadDate
        {
            get
            {
                return toLastModifiadDate;
            }

            set
            {
                toLastModifiadDate = value;
                OnPropertyChanged("ToLastModifiadDate");
            }
        }

        public Boolean IsAllowFilter
        {
            get
            {
                return isAllowFilter;
            }

            set
            {
                isAllowFilter = value;
                OnPropertyChanged("IsAllowFilter");
            }
        }
        public BitmapImage ClearfilterActive
        {
            get
            {
                return clearfilterActive;
            }

            set
            {
                clearfilterActive = value;
                OnPropertyChanged("ClearfilterManagerNameID");
            }
        }
        public BitmapImage ClearfilterShopID
        {
            get
            {
                return clearfilterShopID;
            }

            set
            {
                clearfilterShopID = value;
                OnPropertyChanged("ClearfilterDeliveryID");
            }
        }
        public BitmapImage ClearfilterPostCode
        {
            get
            {
                return clearfilterPostCode;
            }

            set
            {
                clearfilterPostCode = value;
                OnPropertyChanged("ClearfilterStatusID");
            }
        }
        public BitmapImage ClearfilterUserName
        {
            get
            {
                return clearfilterUserName;
            }

            set
            {
                clearfilterUserName = value;
                OnPropertyChanged("ClearfilterLastModifiedByUserID");
            }
        }

        public BitmapImage ClearfilterCityID
        {
            get
            {
                return clearfilterCityID;
            }

            set
            {
                clearfilterCityID = value;
                OnPropertyChanged("ClearfilterAmount");
            }
        }

        public Boolean IsPaginator
        {
            get
            {
                return isPaginator;
            }

            set
            {
                isPaginator = value;
                OnPropertyChanged("IsPaginator");
            }
        }

        public Int32 CurrentPage
        {
            get
            {
                return currentPage;
            }

            set
            {
                currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public Int32 TotalCount
        {
            get
            {
                return totalCount;
            }

            set
            {
                totalCount = value;
                OnPropertyChanged("TotalCount");
            }
        }

        public Int32 PageCount
        {
            get
            {
                return pageCount;
            }

            set
            {
                pageCount = value;
                OnPropertyChanged("PageCount");
            }
        }

        public LocaleRow ShopRow
        {
            get
            {
                return shopRow;
            }

            set
            {
                shopRow = value;
                OnPropertyChanged("ShopRow");
            }
        }
 
        public LocaleFilter Filter
        {
            get
            {
                return filter;
            }

            set
            {
                filter = value;
                OnPropertyChanged("Filter");
            }
        }
        public ShopsGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;

            shopsLogic = new ShopsLogic(attributes);

            FilterCreatedByUserID = new DataTable();
            FilterLastModifiedByUserID = new DataTable();
            FilterCompanyID = new DataTable();
            FilterPhone = new DataTable();
            FilterActive = new DataTable();            
            FilterPostCode = new DataTable();
            FilterCity = new DataTable();
            FilterStreet = new DataTable();
            FilterCountry = new DataTable();

            FilterCreatedByUserID.Columns.Add("ID");
            FilterCreatedByUserID.Columns.Add("IsChecked");
            FilterCreatedByUserID.Columns.Add("Description");

            FilterLastModifiedByUserID.Columns.Add("ID");
            FilterLastModifiedByUserID.Columns.Add("IsChecked");
            FilterLastModifiedByUserID.Columns.Add("Description");

            FilterCompanyID.Columns.Add("ID");
            FilterCompanyID.Columns.Add("IsChecked");
            FilterCompanyID.Columns.Add("Description");

            FilterPhone.Columns.Add("ID");
            FilterPhone.Columns.Add("IsChecked");
            FilterPhone.Columns.Add("Description");

            FilterActive.Columns.Add("ID");
            FilterActive.Columns.Add("IsChecked");
            FilterActive.Columns.Add("Description");

            FilterPostCode.Columns.Add("ID");
            FilterPostCode.Columns.Add("IsChecked");
            FilterPostCode.Columns.Add("Description");

            FilterCity.Columns.Add("ID");
            FilterCity.Columns.Add("IsChecked");
            FilterCity.Columns.Add("Description");

            FilterStreet.Columns.Add("ID");
            FilterStreet.Columns.Add("IsChecked");
            FilterStreet.Columns.Add("Description");

            FilterCountry.Columns.Add("ID");
            FilterCountry.Columns.Add("IsChecked");
            FilterCountry.Columns.Add("Description");
           
            dataShopList = new ObservableCollection<LocaleRow>();

            ShopRow = new LocaleRow();
            Filter = new LocaleFilter();
            summary = new RowSummary();

            this.shopList.ItemsSource = dataShopList;
           
            shopsLogic.InitFilters();
            InitFilters();
            Refresh();
            IsAllowFilter = true;

        }
        #region DataGrid
        private void saleDocument_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void saleDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void saleDocument_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Filters DataGrid
        void InitFilters()
        {
            ClearfilterShopID = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterUserName = ImageHelper.GenerateImage("IconFilter.png");           
            ClearfilterActive = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterPostCode = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterCityID = ImageHelper.GenerateImage("IconFilter.png");
           
            FilterCreatedByUserID.Clear();
            if (shopsLogic.GetFilter("CreatedByUserID") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("CreatedByUserID").Rows)
                {
                    DataRow newrow = FilterCreatedByUserID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterCreatedByUserID.Rows.Add(newrow);
                }
            }

            FilterLastModifiedByUserID.Clear();
            if (shopsLogic.GetFilter("LastModifiedByUserID") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("LastModifiedByUserID").Rows)
                {
                    DataRow newrow = FilterLastModifiedByUserID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterLastModifiedByUserID.Rows.Add(newrow);
                }
            }

            FilterCompanyID.Clear();
            if (shopsLogic.GetFilter("CompanyID") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("CompanyID").Rows)
                {
                    DataRow newrow = FilterCompanyID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterCompanyID.Rows.Add(newrow);
                }
            }

            FilterPhone.Clear();
            if (shopsLogic.GetFilter("Phone") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("Phone").Rows)
                {
                    DataRow newrow = FilterPhone.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterPhone.Rows.Add(newrow);
                }
            }

            FilterActive.Clear();
            if (shopsLogic.GetFilter("Active") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("Active").Rows)
                {
                    DataRow newrow = FilterActive.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = String.Empty;
                    FilterActive.Rows.Add(newrow);
                }
            }

            FilterPostCode.Clear();
            if (shopsLogic.GetFilter("PostCode") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("PostCode").Rows)
                {
                    DataRow newrow = FilterPostCode.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterPostCode.Rows.Add(newrow);
                }
            }

            FilterCity.Clear();
            if (shopsLogic.GetFilter("City") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("City").Rows)
                {
                    DataRow newrow = FilterCity.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterCity.Rows.Add(newrow);
                }
            }

            FilterStreet.Clear();
            if (shopsLogic.GetFilter("Street") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("Street").Rows)
                {
                    DataRow newrow = FilterStreet.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"]; 
                    FilterStreet.Rows.Add(newrow);
                }
            }

            FilterCountry.Clear();
            if (shopsLogic.GetFilter("Country") != null)
            {
                foreach (DataRow row in shopsLogic.GetFilter("Country").Rows)
                {
                    DataRow newrow = FilterCountry.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterCountry.Rows.Add(newrow);
                }
            }
        }

        private void FilterShopID_ButtonApplyClick(string text)
        {

        }

        private void FilterQuantity_ButtonApplyClick()
        {

        }

        private void FilterAmount_ButtonApplyClick()
        {

        }

        private void filterIdUserInput_ButtonApplyClick(string text)
        {

        }

        private void FilterUserID_ButtonApplyClick(string text)
        {

        }

        private void FilterActive_ButtonApplyClick(string text)
        {

        }

        private void FilterPostCode_ButtonApplyClick(string text)
        {

        }
        #endregion

        #region Paginator
        private void ToolBarNextToBack_ButtonBack()
        {

        }

        private void ToolBarNextToBack_ButtonNext()
        {

        }

        private void ToolbarNextPageData_ButtonBackIn()
        {

        }

        private void ToolbarNextPageData_ButtonNextEnd()
        {

        }
        #endregion

        #region ToolBar
        private void ToolBarSaleDocument_ButtonEdit()
        {

        }

        private void ToolBarSaleDocument_ButtonDelete()
        {

        }

        private void ToolBarSaleDocument_ButtonClear()
        {

        }

        private void ToolBarSaleDocument_ButtonScan(string text)
        {

        }

        private void ToolBarSaleDocument_ButtonClean()
        {

        }

        private void ToolBarSaleDocument_ButtonRefresh()
        {

        }
        #endregion

        #region Refresh
        public void Refresh()
        {
            DataTable datatable = shopsLogic.FillGrid(Filter);
            dataShopList.Clear();

            foreach (DataRow row in datatable.Rows)
            {
                dataShopList.Add(shopsLogic.Convert(row, new LocaleRow()));
            }

            CalculateSummary();

            TotalCount = summary.SummaryQuantityLine;
            PageCount = Filter.PagerowCount;
            CurrentPage = Filter.PageNumber;
        }

        #endregion

        #region CalculateSummary
        public void CalculateSummary()
        {
            DataTable datatable = shopsLogic.FillSummary(Filter);
            foreach (DataRow row in datatable.Rows)
            {
                shopsLogic.ConvertSummary(row, summary);
            }
        }
        #endregion

       
    }
}
