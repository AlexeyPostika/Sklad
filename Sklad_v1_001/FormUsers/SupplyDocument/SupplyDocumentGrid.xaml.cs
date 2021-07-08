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
        SupplyDocument.LocalFilter localFilter;
        LocalRow localRow;

        ObservableCollection<LocalRow> datalist;

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


        BitmapImage clearfilterManagerNameID;
        BitmapImage clearfilterDeliveryID;
        BitmapImage clearfilterStatusID;
        BitmapImage clearfilterLastModifiedByUserID;
        BitmapImage clearfilterTagPrice;

        private Boolean isEnableBack;
        private Boolean isEnableNext;
        private Boolean isEnableBackIn;
        private Boolean isEnableNextEnd;
        private String textOnWhatPage;

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
        public BitmapImage ClearfilterTagPrice
        {
            get
            {
                return clearfilterTagPrice;
            }

            set
            {
                clearfilterTagPrice = value;
                OnPropertyChanged("ClearfilterTagPrice");
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public SupplyDocumentGrid()
        {
            InitializeComponent();

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
            localFilter = new LocalFilter();
            localRow = new LocalRow();

            summary = new RowSummary();

            this.SypplyDocument.ItemsSource = datalist;

            supplyDocumentLogic.InitFilters();
            InitFilters();
        }

        #region фильтры
        void InitFilters()
        {
            ClearfilterDeliveryID = ImageHelper.GenerateImage("");
            ClearfilterLastModifiedByUserID= ImageHelper.GenerateImage("");
            ClearfilterManagerNameID= ImageHelper.GenerateImage("");
            ClearfilterStatusID= ImageHelper.GenerateImage("");

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
                    newrow["Description"] = supplyTypeList.innerList.FirstOrDefault(x => x.ID == convertData.ConvertDataInt32(row["ID"].ToString())) != null ?
                                            supplyTypeList.innerList.FirstOrDefault(x => x.ID == convertData.ConvertDataInt32(row["ID"].ToString())).Description : Properties.Resources.UndefindField;                 
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

        private void FilterToDateLastModifiedDate_ButtonFilterSelected()
        {

        }

        private void FilterStatusID_ButtonApplyClick(string text)
        {

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

        }

        private void FilterManagerNameID_ButtonApplyClick(string text)
        {

        }

        private void FilterAmount_ButtonApplyClick()
        {

        }
    }
}
