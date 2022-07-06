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

namespace Sklad_v1_001.FormUsers.Product
{
    public class DataContextSpy : Freezable
    {
        public DataContextSpy()
        {
            // This binding allows the spy to inherit a DataContext.
            BindingOperations.SetBinding(this, DataContextProperty, new Binding());
        }

        public object DataContext
        {
            get { return GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        // Borrow the DataContext dependency property from FrameworkElement.
        public static readonly DependencyProperty DataContextProperty = FrameworkElement
            .DataContextProperty.AddOwner(typeof(DataContextSpy));

        protected override Freezable CreateInstanceCore()
        {
            // We are required to override this abstract method.
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Логика взаимодействия для ProductInGrid.xaml
    /// </summary>
    public partial class ProductInGrid : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        Attributes attributes;
        ConvertData convertData;

        ProductLogic productLogic;

        LocalRow localRowProduct;
        LocalFilter localFilter;

        RowSummary summary;

        ObservableCollection<LocalRow> datalist;

        private String search;
        DataTable filterCreatedByUserID;
        DataTable filterLastModifiedByUserID;
        DataTable filterStatus;
        DataTable filterShowcase;
        DataTable filterProcreator;
        DataTable filterCategory;
        DataTable filterCategoryDetails;

        Double quantityMin;
        Double quantityMax;
        Double defaultquantityMin;
        Double defaultquantityMax;

        Double tagPriceWithVATMin;
        Double tagPriceWithVATMax;
        Double defaultTagPriceWithVATMin;
        Double defaultTagPriceWithVATMax;

        ImageSource clearfilterCategoryDetails;
        ImageSource clearFilterQuantity;
        ImageSource clearFilterTagPriceWithVAT;

        Boolean isAllowFilter;
        Boolean isPaginator;

        Int32 currentPage;
        Int32 totalCount;
        Int32 pageCount;

        public LocalRow LocalRowProduct
        {
            get
            {
                return localRowProduct;
            }

            set
            {
                localRowProduct = value;             
            }
        }

        public String Search
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
        public DataTable FilterStatus
        {
            get
            {
                return filterStatus;
            }

            set
            {
                filterStatus = value;
                OnPropertyChanged("FilterStatus");
            }
        }
        public DataTable FilterShowcase
        {
            get
            {
                return filterShowcase;
            }

            set
            {
                filterShowcase = value;
                OnPropertyChanged("FilterShowcase");
            }
        }

        public DataTable FilterProcreator
        {
            get
            {
                return filterProcreator;
            }

            set
            {
                filterProcreator = value;
                OnPropertyChanged("FilterProcreator");
            }
        }

        public DataTable FilterCategory
        {
            get
            {
                return filterCategory;
            }

            set
            {
                filterCategory = value;
                OnPropertyChanged("FilterCategory");
            }
        }

        public DataTable FilterCategoryDetails
        {
            get
            {
                return filterCategoryDetails;
            }

            set
            {
                filterCategoryDetails = value;
                OnPropertyChanged("FilterCategoryDetails");
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

        public double TagPriceWithVATMin
        {
            get
            {
                return tagPriceWithVATMin;
            }

            set
            {
                tagPriceWithVATMin = value;
                OnPropertyChanged("TagPriceWithVATMin");
            }
        }
        public double TagPriceWithVATMax
        {
            get
            {
                return tagPriceWithVATMax;
            }

            set
            {
                tagPriceWithVATMax = value;
                OnPropertyChanged("TagPriceWithVATMax");
            }
        }

        public double DefaultTagPriceWithVATMin
        {
            get
            {
                return defaultTagPriceWithVATMin;
            }

            set
            {
                defaultTagPriceWithVATMin = value;
                OnPropertyChanged("DefaultTagPriceWithVATMin");
            }
        }
        public double DefaultTagPriceWithVATMax
        {
            get
            {
                return defaultTagPriceWithVATMax;
            }

            set
            {
                defaultTagPriceWithVATMax = value;
                OnPropertyChanged("DefaultTagPriceWithVATMax");
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

       
        public ImageSource ClearfilterCategoryDetails
        {
            get
            {
                return clearfilterCategoryDetails;
            }

            set
            {
                clearfilterCategoryDetails = value;
                OnPropertyChanged("ClearfilterCategoryDetails");
            }
        }
       
        public ImageSource ClearFilterQuantity
        {
            get
            {
                return clearFilterQuantity;
            }

            set
            {
                clearFilterQuantity = value;
                OnPropertyChanged("ClearFilterQuantity");
            }
        }

        public ImageSource ClearFilterTagPriceWithVAT
        {
            get
            {
                return clearFilterTagPriceWithVAT;
            }

            set
            {
                clearFilterTagPriceWithVAT = value;
                OnPropertyChanged("ClearFilterTagPriceWithVAT");
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


        public ProductInGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
            FilterCreatedByUserID = new DataTable();
            FilterLastModifiedByUserID = new DataTable();
            FilterStatus = new DataTable();
            FilterShowcase = new DataTable();
            FilterProcreator = new DataTable();
            FilterCategory = new DataTable();
            FilterCategoryDetails = new DataTable();

            FilterCreatedByUserID.Columns.Add("ID");
            FilterCreatedByUserID.Columns.Add("IsChecked");
            FilterCreatedByUserID.Columns.Add("Description");

            FilterLastModifiedByUserID.Columns.Add("ID");
            FilterLastModifiedByUserID.Columns.Add("IsChecked");
            FilterLastModifiedByUserID.Columns.Add("Description");

            FilterStatus.Columns.Add("ID");
            FilterStatus.Columns.Add("IsChecked");
            FilterStatus.Columns.Add("Description");

            FilterShowcase.Columns.Add("ID");
            FilterShowcase.Columns.Add("IsChecked");
            FilterShowcase.Columns.Add("Description");

            FilterProcreator.Columns.Add("ID");
            FilterProcreator.Columns.Add("IsChecked");
            FilterProcreator.Columns.Add("Description");

            FilterCategory.Columns.Add("ID");
            FilterCategory.Columns.Add("IsChecked");
            FilterCategory.Columns.Add("Description");

            FilterCategoryDetails.Columns.Add("ID");
            FilterCategoryDetails.Columns.Add("IsChecked");
            FilterCategoryDetails.Columns.Add("Description");

            convertData = new ConvertData();

            productLogic = new ProductLogic(attributes);

            localFilter = new LocalFilter();

            datalist = new ObservableCollection<LocalRow>();

            summary = new RowSummary();

            productLogic.InitFilters();
            InitFilters();

            this.ProductGrid.ItemsSource = datalist;
            Refresh();
        }

        #region фильтры
        void InitFilters()
        {
            ProductStatusList productStatusList = new ProductStatusList();
            ClearfilterCategoryDetails = ImageHelper.GenerateImage("IconFilter.png");
            ClearFilterQuantity = ImageHelper.GenerateImage("IconFilter.png");
            ClearFilterTagPriceWithVAT = ImageHelper.GenerateImage("IconFilter.png");

            Search = String.Empty;

            FilterCreatedByUserID.Clear();
            if (productLogic.GetFilter("CreatedByUserID") != null)
            {
                foreach (DataRow row in productLogic.GetFilter("CreatedByUserID").Rows)
                {
                    DataRow newrow = FilterCreatedByUserID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterCreatedByUserID.Rows.Add(newrow);
                }
            }

            FilterLastModifiedByUserID.Clear();
            if (productLogic.GetFilter("LastModifiedByUserID") != null)
            {
                foreach (DataRow row in productLogic.GetFilter("LastModifiedByUserID").Rows)
                {
                    DataRow newrow = FilterLastModifiedByUserID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterLastModifiedByUserID.Rows.Add(newrow);
                }
            }

            FilterStatus.Clear();
            if (productLogic.GetFilter("Status") != null)
            {
                foreach (DataRow row in productLogic.GetFilter("Status").Rows)
                {
                    DataRow newrow = FilterStatus.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = productStatusList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(row["ID"].ToString())) != null ?
                                            productStatusList.innerList.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(row["ID"].ToString())).Description : Properties.Resources.UndefindField;
                    FilterStatus.Rows.Add(newrow);
                }
            }

            FilterShowcase.Clear();
            if (productLogic.GetFilter("Showcase") != null)
            {
                foreach (DataRow row in productLogic.GetFilter("Showcase").Rows)
                {
                    DataRow newrow = FilterShowcase.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterShowcase.Rows.Add(newrow);
                }
            }

            FilterProcreator.Clear();
            if (productLogic.GetFilter("Procreator") != null)
            {
                foreach (DataRow row in productLogic.GetFilter("Procreator").Rows)
                {
                    DataRow newrow = FilterProcreator.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterProcreator.Rows.Add(newrow);
                }
            }

            FilterCategory.Clear();
            if (productLogic.GetFilter("Category") != null)
            {
                foreach (DataRow row in productLogic.GetFilter("Category").Rows)
                {
                    DataRow newrow = FilterCategory.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterCategory.Rows.Add(newrow);
                }
            }

            FilterCategoryDetails.Clear();
            if (productLogic.GetFilter("CategoryDetails") != null)
            {
                foreach (DataRow row in productLogic.GetFilter("CategoryDetails").Rows)
                {
                    DataRow newrow = FilterCategoryDetails.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterCategoryDetails.Rows.Add(newrow);
                }
            }

            QuantityMin = productLogic.GetFromToFilter("Quantity").min;
            QuantityMax = productLogic.GetFromToFilter("Quantity").max;
            DefaultquantityMin = productLogic.GetFromToFilter("Quantity").min;
            DefaultquantityMax = productLogic.GetFromToFilter("Quantity").max;
            localFilter.QuantityMin = productLogic.GetFromToFilter("Quantity").min;
            localFilter.QuantityMax = productLogic.GetFromToFilter("Quantity").max;

            TagPriceWithVATMin = productLogic.GetFromToFilter("TagPriceWithVAT").min;
            TagPriceWithVATMax = productLogic.GetFromToFilter("TagPriceWithVAT").max;
            DefaultTagPriceWithVATMin = productLogic.GetFromToFilter("TagPriceWithVAT").min;
            DefaultTagPriceWithVATMax = productLogic.GetFromToFilter("TagPriceWithVAT").max;
            localFilter.TagPriceVATRUS_Min = productLogic.GetFromToFilter("TagPriceWithVAT").min;
            localFilter.TagPriceVATRUS_Max = productLogic.GetFromToFilter("TagPriceWithVAT").max;

        }

        private void FilterType_ButtonApplyClick(string text)
        {
            localFilter.Category = text;
            Refresh();
        }

        private void FilterWeight_ButtonApplyClick()
        {
            localFilter.QuantityMax = QuantityMax;
            localFilter.QuantityMin = QuantityMin;
            Refresh();
        }

        private void FilterTagPriceWithVAT_ButtonApplyClick()
        {
            localFilter.TagPriceVATRUS_Max = TagPriceWithVATMax;
            localFilter.TagPriceVATRUS_Min = TagPriceWithVATMin;
            Refresh();
        }
        private void ScrabToolbar_ButtonClearFiltersClick()
        {
            InitFilters();
            Refresh();
        }
        private void SearchFilter_ButtonClearClick()
        {
            Search = String.Empty;
            localFilter.Search = String.Empty;
            Refresh();
        }

        private void SearchFilter_ButtonTextChangedClick()
        {
            localFilter.Search = Search;
            Refresh();
        }

        #endregion

        #region refresh
        private void Refresh()
        {
            datalist.Clear();
            DataTable dataTable = productLogic.FillGrid(localFilter);
            foreach (DataRow row in dataTable.Rows)
            {
                LocalRow localRow = new LocalRow();
                datalist.Add(productLogic.Convert(row, localRow));              
            }

            CalculateSummary();

            TotalCount = summary.CountID;
            PageCount = localFilter.PagerowCount;
            CurrentPage = localFilter.PageNumber;

        }
        #endregion

        #region CalculateSummary
        public void CalculateSummary()
        {
            DataTable datatable1 = productLogic.FillSummary(localFilter);
            foreach (DataRow row in datatable1.Rows)
            {
                productLogic.ConvertSummary(row, summary);
            }
        }
        #endregion

        private void RelatedProductDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        private void RelatedProductDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LocalRow local = ProductGrid.SelectedItem as LocalRow;
            if (local != null)
            {
                LocalRowProduct = local;            
                Window win = Parent as Window;
                win.Close();
            }
        }

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

       
       
    }
}
