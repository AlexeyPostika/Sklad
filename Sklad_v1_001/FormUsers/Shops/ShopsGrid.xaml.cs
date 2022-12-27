using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
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

        DataTable filterIDManagerName;
        DataTable filterIDDelivery;
        DataTable filterIDStatus;
        DataTable filterInputUserID;
        DataTable filterLastModifiedByUserID;
        DataTable filterIDShop;

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


        BitmapImage clearfilterQuantity;
        BitmapImage clearfilterShopID;
        BitmapImage clearfilterStatusID;
        BitmapImage clearfilterInputUserName;
        BitmapImage clearfilterUserName;
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

        public DataTable FilterIDManagerName
        {
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
        public DataTable FilterIDDelivery
        {
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

        public DataTable FilterInputUserID
        {
            get
            {
                return filterInputUserID;
            }

            set
            {
                filterInputUserID = value;
                OnPropertyChanged("FilterInputUserID");
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

        public DataTable FilterIDShop
        {
            get
            {
                return filterIDShop;
            }

            set
            {
                filterIDShop = value;
                OnPropertyChanged("FilterIDShop");
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
        public BitmapImage ClearfilterQuantity
        {
            get
            {
                return clearfilterQuantity;
            }

            set
            {
                clearfilterQuantity = value;
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
        //ClearfilterInputUserName
        public BitmapImage ClearfilterInputUserName
        {
            get
            {
                return clearfilterInputUserName;
            }

            set
            {
                clearfilterInputUserName = value;
                OnPropertyChanged("ClearfilterInputUserName");
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

        public ShopsGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;

            FilterIDManagerName = new DataTable();
            FilterIDDelivery = new DataTable();
            FilterIDStatus = new DataTable();
            FilterInputUserID = new DataTable();
            FilterLastModifiedByUserID = new DataTable();
            FilterIDShop = new DataTable();

            FilterIDManagerName.Columns.Add("ID");
            FilterIDManagerName.Columns.Add("IsChecked");
            FilterIDManagerName.Columns.Add("Description");

            FilterIDDelivery.Columns.Add("ID");
            FilterIDDelivery.Columns.Add("IsChecked");
            FilterIDDelivery.Columns.Add("Description");

            FilterIDStatus.Columns.Add("ID");
            FilterIDStatus.Columns.Add("IsChecked");
            FilterIDStatus.Columns.Add("Description");

            FilterInputUserID.Columns.Add("ID");
            FilterInputUserID.Columns.Add("IsChecked");
            FilterInputUserID.Columns.Add("Description");

            FilterLastModifiedByUserID.Columns.Add("ID");
            FilterLastModifiedByUserID.Columns.Add("IsChecked");
            FilterLastModifiedByUserID.Columns.Add("Description");

            FilterIDShop.Columns.Add("ID");
            FilterIDShop.Columns.Add("IsChecked");
            FilterIDShop.Columns.Add("Description");

        }
    }
}
