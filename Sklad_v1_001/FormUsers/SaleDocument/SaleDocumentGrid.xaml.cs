using Sklad_v1_001.GlobalAttributes;
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

namespace Sklad_v1_001.FormUsers.SaleDocument
{
    /// <summary>
    /// Логика взаимодействия для SaleDocumentGrid.xaml
    /// </summary>
    public partial class SaleDocumentGrid : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        Attributes attributes;

        DataTable filterUserIDName;

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


        BitmapImage clearfilterUserName;
        BitmapImage clearfilterAmount;
        BitmapImage clearfilterQuantity;

        Int32 currentPage;
        Int32 totalCount;
        Int32 pageCount;
        
        public DataTable FilterUserIDName
        {
            get
            {
                return filterUserIDName;
            }

            set
            {
                filterUserIDName = value;
                OnPropertyChanged("FilterUserName");
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

        public BitmapImage ClearfilterUserName
        {
            get
            {
                return clearfilterUserName;
            }

            set
            {
                clearfilterUserName = value;
                OnPropertyChanged("ClearfilterUserName");
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

        public BitmapImage ClearfilterQuantity
        {
            get
            {
                return clearfilterQuantity;
            }

            set
            {
                clearfilterQuantity = value;
                OnPropertyChanged("ClearfilterQuantity");
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

        public SaleDocumentGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
        }

        #region DataGrid SaleDocument
        private void saleDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void saleDocument_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void saleDocument_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Filters
        private void FilterAmount_ButtonApplyClick()
        {

        }
        private void FilterUserID_ButtonApplyClick(string text)
        {

        }
        private void FilterQuantity_ButtonApplyClick()
        {

        }


        #endregion

        #region TooBar
        private void ToolBarSaleDocument_ButtonAdd()
        {

        }

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
        #endregion

        #region нижняя грида - детали документа
        private void DataProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DataPayment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

       
    }
}
