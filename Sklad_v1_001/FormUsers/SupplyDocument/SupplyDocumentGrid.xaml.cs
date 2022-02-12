using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.SupplyDocumentDetails;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
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

namespace Sklad_v1_001.FormUsers.SupplyDocument
{
    /// <summary>
    /// Логика взаимодействия для SupplyDocumentGrid.xaml
    /// </summary>
    public partial class SupplyDocumentGrid : Page, INotifyPropertyChanged
    {
        SupplyDocumentLogic supplyDocumentLogic;
        SupplyDocumentDetailsLogic supplyDocumentDetailsLogic;

        SupplyDocument.LocalFilter localFilter;
        LocalRow localRow;
        SupplyDocumentDetails.LocaleFilter filterDetails;

        ObservableCollection<LocalRow> datalist;
        ObservableCollection<SupplyDocumentDetails.LocaleRow> datalistDetails;

        RowSummary summary;

        ConvertData convertData;

        DataTable filterIDManagerName;
        DataTable filterIDDelivery;
        DataTable filterIDStatus;
        DataTable filterLastModifiedByUserID;

        Int32 filterDateIDLastModifiadDate;
        DateTime? fromLastModifiadDate;
        DateTime? toLastModifiadDate;

        Double quantityMin;
        Double quantityMax;
        Double defaultquantityMin;
        Double defaultquantityMax;
        
        Double amountMin;
        Double amountMax;
        Double defaultamountMin;
        Double defaultamountMax;

        Boolean isAllowFilter;
        Boolean isPaginator;


        BitmapImage clearfilterManagerNameID;
        BitmapImage clearfilterDeliveryID;
        BitmapImage clearfilterStatusID;
        BitmapImage clearfilterLastModifiedByUserID;       
        BitmapImage clearfilterAmount;

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

        public DataTable FilterIDManagerName {
            get
            {
                return filterIDManagerName;
            }

            set
            {
                filterIDManagerName = value;
                OnPropertyChanged("FilterIDManagerName");
            }          
        }
        public DataTable FilterIDDelivery {
            get
            {
                return filterIDDelivery;
            }

            set
            {
                filterIDDelivery = value;
                OnPropertyChanged("FilterIDDelivery");
            }
            
        }
        public DataTable FilterIDStatus
        {
            get
            {
                return filterIDStatus;
            }

            set
            {
                filterIDStatus = value;
                OnPropertyChanged("FilterIDStatus");
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
        public BitmapImage ClearfilterManagerNameID
        {
            get
            {
                return clearfilterManagerNameID;
            }

            set
            {
                clearfilterManagerNameID = value;
                OnPropertyChanged("ClearfilterManagerNameID");
            }
        }
        public BitmapImage ClearfilterDeliveryID
        {
            get
            {
                return clearfilterDeliveryID;
            }

            set
            {
                clearfilterDeliveryID = value;
                OnPropertyChanged("ClearfilterDeliveryID");
            }
        }
        public BitmapImage ClearfilterStatusID
        {
            get
            {
                return clearfilterStatusID;
            }

            set
            {
                clearfilterStatusID = value;
                OnPropertyChanged("ClearfilterStatusID");
            }
        }
        public BitmapImage ClearfilterLastModifiedByUserID
        {
            get
            {
                return clearfilterLastModifiedByUserID;
            }

            set
            {
                clearfilterLastModifiedByUserID = value;
                OnPropertyChanged("ClearfilterLastModifiedByUserID");
            }
        }
        public BitmapImage ClearfilterAmount
        {
            get
            {
                return clearfilterAmount;
            }

            set
            {
                clearfilterAmount = value;
                OnPropertyChanged("ClearfilterAmount");
            }
        }

        public double QuantityMin
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
        public double QuantityMax
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
        public double AmountMin
        {
            get
            {
                return amountMin;
            }

            set
            {
                amountMin = value;
                OnPropertyChanged("AmountMin");
            }
        }
        public double AmountMax
        {
            get
            {
                return amountMax;
            }

            set
            {
                amountMax = value;
                OnPropertyChanged("AmountMax");
            }
        }

        public double DefaultquantityMin
        {
            get
            {
                return defaultquantityMin;
            }

            set
            {
                defaultquantityMin = value;
                OnPropertyChanged("DefaultquantityMin");
            }
        }
        public double DefaultquantityMax
        {
            get
            {
                return defaultquantityMax;
            }

            set
            {
                defaultquantityMax = value;
                OnPropertyChanged("DefaultquantityMax");
            }
        }
        public double DefaultamountMin
        {
            get
            {
                return defaultamountMin;
            }

            set
            {
                defaultamountMin = value;
                OnPropertyChanged("DefaultamountMin");
            }
        }
        public double DefaultamountMax
        {
            get
            {
                return defaultamountMax;
            }

            set
            {
                defaultamountMax = value;
                OnPropertyChanged("DefaultamountMax");
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

        Attributes attributes;

        public LocaleFilter FilterProduct { get => filterDetails; set => filterDetails = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public SupplyDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            FilterIDManagerName = new DataTable();
            FilterIDDelivery = new DataTable();
            FilterIDStatus = new DataTable();
            FilterLastModifiedByUserID = new DataTable();

            FilterIDManagerName.Columns.Add("ID");
            FilterIDManagerName.Columns.Add("IsChecked");
            FilterIDManagerName.Columns.Add("Description");

            FilterIDDelivery.Columns.Add("ID");
            FilterIDDelivery.Columns.Add("IsChecked");
            FilterIDDelivery.Columns.Add("Description");

            FilterIDStatus.Columns.Add("ID");
            FilterIDStatus.Columns.Add("IsChecked");
            FilterIDStatus.Columns.Add("Description");

            FilterLastModifiedByUserID.Columns.Add("ID");
            FilterLastModifiedByUserID.Columns.Add("IsChecked");
            FilterLastModifiedByUserID.Columns.Add("Description");

            convertData = new ConvertData();

            supplyDocumentLogic = new SupplyDocumentLogic();
            supplyDocumentDetailsLogic = new SupplyDocumentDetailsLogic();

            localFilter = new LocalFilter();
            filterDetails = new SupplyDocumentDetails.LocaleFilter();

            localRow = new LocalRow();

            datalist = new ObservableCollection<LocalRow>();
            datalistDetails = new ObservableCollection<SupplyDocumentDetails.LocaleRow>();

            summary = new RowSummary();

            this.SypplyDocument.ItemsSource = datalist;
            //this.SypplyDocumentDetails.Items.Clear();
            this.SypplyDocumentDetails.ItemsSource = datalistDetails;

            supplyDocumentLogic.InitFilters();
            InitFilters();
            Refresh();
            IsAllowFilter = true;
        }

        #region фильтры
        void InitFilters()
        {
            ClearfilterDeliveryID = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterLastModifiedByUserID= ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterManagerNameID= ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterStatusID= ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterAmount = ImageHelper.GenerateImage("IconFilter.png");

            SupplyTypeList supplyTypeList = new SupplyTypeList();

            FilterIDManagerName.Clear();
            if (supplyDocumentLogic.GetFilter("ManagerName") != null)
            {
                foreach(DataRow row in supplyDocumentLogic.GetFilter("ManagerName").Rows)
                {
                    DataRow newrow = FilterIDManagerName.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterIDManagerName.Rows.Add(newrow);
                }
            }

            FilterIDDelivery.Clear();
            if (supplyDocumentLogic.GetFilter("Delivery") != null)
            {
                foreach (DataRow row in supplyDocumentLogic.GetFilter("Delivery").Rows)
                {
                    DataRow newrow = FilterIDDelivery.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterIDDelivery.Rows.Add(newrow);
                }
            }

            FilterIDStatus.Clear();
            if (supplyDocumentLogic.GetFilter("Status") != null)
            {
                foreach (DataRow row in supplyDocumentLogic.GetFilter("Status").Rows)
                {
                    DataRow newrow = FilterIDStatus.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = supplyTypeList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(row["ID"].ToString())) != null ?
                                            supplyTypeList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(row["ID"].ToString())).Description : Properties.Resources.UndefindField;                 
                    FilterIDStatus.Rows.Add(newrow);
                }
            }

            FilterLastModifiedByUserID.Clear();
            if (supplyDocumentLogic.GetFilter("LastModifiedByUserID") != null)
            {
                foreach (DataRow row in supplyDocumentLogic.GetFilter("LastModifiedByUserID").Rows)
                {
                    DataRow newrow = FilterLastModifiedByUserID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterLastModifiedByUserID.Rows.Add(newrow);
                }
            }

            QuantityMin = supplyDocumentLogic.GetFromToFilter("Quantity").min;
            QuantityMax = supplyDocumentLogic.GetFromToFilter("Quantity").max;
            DefaultquantityMin = supplyDocumentLogic.GetFromToFilter("Quantity").min;
            DefaultquantityMax = supplyDocumentLogic.GetFromToFilter("Quantity").max;
            localFilter.QuantityMin = supplyDocumentLogic.GetFromToFilter("Quantity").min;
            localFilter.QuantityMax = supplyDocumentLogic.GetFromToFilter("Quantity").max;

            AmountMin = supplyDocumentLogic.GetFromToFilter("Amount").min;
            AmountMax = supplyDocumentLogic.GetFromToFilter("Amount").max;            
            DefaultamountMin = supplyDocumentLogic.GetFromToFilter("Amount").min;
            DefaultamountMax = supplyDocumentLogic.GetFromToFilter("Amount").max;
            localFilter.AmountMin = supplyDocumentLogic.GetFromToFilter("Amount").min;
            localFilter.AmountMax = supplyDocumentLogic.GetFromToFilter("Amount").max;

        }


        private void FilterToDateLastModifiedDate_ButtonFilterSelected()
        {
            if (localFilter.FilterLastModifiedDate != FilterDateIDLastModifiadDate || FilterDateIDLastModifiadDate == 5 && this.IsMouseOver && IsAllowFilter)
            {
                localFilter.FilterLastModifiedDate = FilterDateIDLastModifiadDate;
                switch (FilterDateIDLastModifiadDate)
                {
                    case 0:
                        localFilter.FromLastModifiedDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                        localFilter.FromLastModifiedDate = localFilter.FromLastModifiedDate?.AddSeconds(10);
                        localFilter.ToLastModifiedDate = DateTime.Now;
                        break;
                    case 1:
                        localFilter.FromLastModifiedDate = DateTime.Now.Date;
                        localFilter.ToLastModifiedDate = DateTime.Now;
                        break;
                    case 2:
                        localFilter.FromLastModifiedDate = DateTime.Now.Date.AddDays(-1);
                        localFilter.ToLastModifiedDate = DateTime.Now.Date.AddSeconds(-1);
                        break;
                    case 3:
                        localFilter.FromLastModifiedDate = DateTime.Now.Date.AddDays(-((Int32)DateTime.Now.Date.DayOfWeek == 0 ? 6 : (Int32)DateTime.Now.Date.DayOfWeek - 1));
                        localFilter.ToLastModifiedDate = DateTime.Now;
                        break;
                    case 4:
                        localFilter.FromLastModifiedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        localFilter.ToLastModifiedDate = DateTime.Now;
                        break;
                    case 5:
                        FromToTimeFilter fromToTimeFilter = new FromToTimeFilter();
                        Window ret = new FlexWindows(Properties.Resources.TitleFromToDateFilter);
                        ret.Content = fromToTimeFilter;
                        fromToTimeFilter.Datefrom = localFilter.FromLastModifiedDate;
                        if (fromToTimeFilter.Datefrom == ((DateTime)System.Data.SqlTypes.SqlDateTime.MinValue))
                        {
                            // возможно прочитать минимальное значение из базы. Пока на начало года открутим 
                            fromToTimeFilter.Datefrom = new DateTime(DateTime.Now.Year, 1, 1);
                        }
                        fromToTimeFilter.Dateto = localFilter.ToLastModifiedDate;
                        ret.ShowDialog();
                        if (fromToTimeFilter.Apply)
                        {
                            localFilter.FromLastModifiedDate = fromToTimeFilter.Datefrom;
                            localFilter.ToLastModifiedDate = fromToTimeFilter.Dateto;
                        }
                        break;
                    default:
                        break;
                }

                //if (NeedRefresh)
                //    Refresh();
            }
        }

        private void FilterStatusID_ButtonApplyClick(string text)
        {
            localFilter.Status = text;
            Refresh();
        }

        private void FilterLastModifiedByUserID_ButtonApplyClick(string text)
        {
            //if (NeedRefresh)
            //{
            //    filter.LastModifiedByUserID = text;
            //    Refresh();
            //}
        }

        private void FilterDeliveryID_ButtonApplyClick(string text)
        {
            localFilter.DeliveryID = text;
            Refresh();
        }

        private void FilterManagerNameID_ButtonApplyClick(string text)
        {
            localFilter.ManagerUserID = text;
            Refresh();
        }

        private void FilterAmount_ButtonApplyClick()
        {
            localFilter.AmountMin = AmountMin;
            localFilter.AmountMax = AmountMax;
            Refresh();
        }
        #endregion

        #region Refresh
        public void Refresh()
        {
            DataTable datatable = supplyDocumentLogic.FillGrid(localFilter);
            datalist.Clear();

            foreach (DataRow row in datatable.Rows)
            {
                datalist.Add(supplyDocumentLogic.Convert(row, new LocalRow()));
            }

            CalculateSummary();

            TotalCount = summary.SummaryQuantityLine;
            PageCount = localFilter.PagerowCount;
            CurrentPage = localFilter.PageNumber;
        }

        #endregion

        #region CalculateSummary
        public void CalculateSummary()
        {
            DataTable datatable1 = supplyDocumentLogic.FillSummary(localFilter);
            foreach (DataRow row in datatable1.Rows)
            {
                supplyDocumentLogic.ConvertSummary(row, summary);
            }
          
        }
        #endregion

        #region Paginator

        private void ToolbarNextPageData_ButtonBackIn()
        {
            IsPaginator = true;
            localFilter.PageNumber = 0;
            Refresh();
        }
        private void ToolBarNextToBack_ButtonBack()
        {
            IsPaginator = true;
            localFilter.PageNumber--;
            Refresh();
        }
        private void ToolBarNextToBack_ButtonNext()
        {
            IsPaginator = true;
            localFilter.PageNumber++;
            Refresh();
        }

        private void ToolbarNextPageData_ButtonNextEnd()
        {
            IsPaginator = true;
            localFilter.PageNumber = (Int32)(Math.Ceiling((double)summary.SummaryQuantityLine / localFilter.PagerowCount) - 1);
            Refresh();
        }

        #endregion

        #region Edit
        public void EditDetails(LocalRow _localRow)
        {
            MainWindow.AppWindow.ButtonNewSupplyDocumentF(_localRow);
        }
        #endregion

        private void SypplyDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LocalRow currentrow = this.SypplyDocument.SelectedItem as LocalRow;
            if (currentrow != null)
            {
                //подготовили продукты 
                FilterProduct.TypeScreen = ScreenType.ScreenTypeName;
                FilterProduct.DocumentID = currentrow.ID;
                DataTable suppliDocumentDetailsList = supplyDocumentDetailsLogic.FillGrid(FilterProduct);
                
                //чистим все детали
                datalistDetails.Clear();
                
                //заливаем данные на формы
                if (suppliDocumentDetailsList != null && suppliDocumentDetailsList.Rows.Count > 0)
                {
                    foreach (DataRow row in suppliDocumentDetailsList.Rows)
                    {                                          
                        datalistDetails.Add(supplyDocumentDetailsLogic.Convert(row, new SupplyDocumentDetails.LocaleRow()));
                        //datalistDetailsAll.Add(saleDocumentDetailsLogic.ConvertProduct(curlocalrow, new SaleDocumentDetails.LocaleRow()));
                        //if (curlocalrow.IsUCSCardNumberActive)
                        //    isPartiallyPayment = true;
                    }
                }
            }
            
        }

        private void SupplyToolBar_ButtonAdd()
        {      
            MainWindow.AppWindow.ButtonNewSupplyDocument();
        }

        private void SupplyToolBar_ButtonEdit()
        {

            LocalRow currentrow = this.SypplyDocument.SelectedItem as LocalRow;
            if (currentrow != null)
            {
                EditDetails(currentrow);
            }
        }

        private void SypplyDocument_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LocalRow currentrow = this.SypplyDocument.SelectedItem as LocalRow;
            if (currentrow != null)
            {
                EditDetails(currentrow);
            }
        }
    }
}
