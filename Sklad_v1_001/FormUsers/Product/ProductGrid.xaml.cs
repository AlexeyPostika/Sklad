using Sklad_v1_001.Control.Contener;
using Sklad_v1_001.Control.FlexFilter;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
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
    /// <summary>
    /// Логика взаимодействия для ProductGrid.xaml
    /// </summary>
    public partial class ProductGrid : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Attributes attributes;
        ConvertData convertData;

        ProductLogic productLogic;

        LocalFilter localFilter;

        RowSummary summary;

        ObservableCollection<LocalRow> datalist;

        FlexFilterContenerProductWindows flexFilterContenerProductWindows;

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

        Boolean isAllowFilter;
        Boolean isPaginator;

        Int32 currentPage;
        Int32 totalCount;
        Int32 pageCount;


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

        public ProductGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;
            
            FilterCreatedByUserID = new DataTable();
            filterLastModifiedByUserID = new DataTable();
            FilterStatus = new DataTable();
            FilterShowcase = new DataTable();
            FilterProcreator = new DataTable();
            FilterCategory = new DataTable();
            FilterCategoryDetails = new DataTable();

            FilterCreatedByUserID.Columns.Add("ID");
            FilterCreatedByUserID.Columns.Add("IsChecked");
            FilterCreatedByUserID.Columns.Add("Description");

            filterLastModifiedByUserID.Columns.Add("ID");
            filterLastModifiedByUserID.Columns.Add("IsChecked");
            filterLastModifiedByUserID.Columns.Add("Description");

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

            flexFilterContenerProductWindows = new FlexFilterContenerProductWindows();

            productLogic = new ProductLogic();

            localFilter = new LocalFilter();

            datalist = new ObservableCollection<LocalRow>();

            summary = new RowSummary();

            productLogic.InitFilters();
            InitFilters();
            //Refresh();
            IsAllowFilter = true;
        }

        private void AddVisibilityControl(Panel PanelToAdd, Object VisibilityRow, UIElement ElementValue)
        {
            Boolean visibility = false;
            Boolean.TryParse(VisibilityRow.ToString(), out visibility);
            if (visibility == true)
                PanelToAdd.Children.Add(ElementValue);           
            
        }

        private void Refresh()
        {
            this.Column1.Children.Clear();

            DataTable dataTable = productLogic.FillGrid(localFilter);
            foreach(DataRow row in dataTable.Rows)
            {
                LocalRow localRow = new LocalRow();
                datalist.Add(productLogic.Convert(row, localRow));

                ContenerRowDescription contenerRowDescription = new ContenerRowDescription();
                contenerRowDescription.KeyRow = localRow.ID.ToString(); //ключ для строки
                contenerRowDescription.ButtonAddClick += ContenerRowDescription_ButtonAddClick;
                contenerRowDescription.ButtonEditClick += ContenerRowDescription_ButtonEditClick;

                contenerRowDescription.PhotoImage = localRow.PhotoImage;
                contenerRowDescription.TextValue1 = localRow.Name;
                contenerRowDescription.TextValue2 = "Описание: " + localRow.Description;

                contenerRowDescription.TextEdit1 = "Категория товара: " + localRow.CategoryName;
                contenerRowDescription.TextEdit2 = "Подкатегория товара: " + localRow.CategoryDetailsName;
                contenerRowDescription.TextEdit3 = "Витрина: " + localRow.ShowcaseIDName;
                contenerRowDescription.TextEdit4 = "Размер упаковки: " + localRow.SizeProduct;
                contenerRowDescription.TextEdit5 = "Штрих-код: " + localRow.BarCodeString;
                contenerRowDescription.Description6.Visibility = Visibility.Collapsed;
                contenerRowDescription.Description7.Visibility = Visibility.Collapsed;

                contenerRowDescription.TextCount1 = "Количество на складе: "+localRow.Quantity.ToString();
                contenerRowDescription.TagPriceRUS = localRow.TagPriceRUS;
                AddVisibilityControl(Column1, true, contenerRowDescription);
            }

            CalculateSummary();

            TotalCount = summary.CountID;
            PageCount = localFilter.PagerowCount;
            CurrentPage = localFilter.PageNumber;

        }

        private void ContenerRowDescription_ButtonEditClick(object sender, RoutedEventArgs e)
        {
            ContenerRowDescription contenerRowDescription = sender as ContenerRowDescription;
            if (contenerRowDescription != null)
            {
                String rowID = datalist.FirstOrDefault(x => x.ID.ToString() == contenerRowDescription.KeyRow) != null ? datalist.FirstOrDefault(x => x.ID.ToString() == contenerRowDescription.KeyRow).ID.ToString() : String.Empty;
                if (rowID != "")
                {

                }
            }
        }

        private void ContenerRowDescription_ButtonAddClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            localFilter.PagerowCount = (Int32)(page.ActualHeight) / 210;
            Refresh();
        }

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
            localFilter.PageNumber = (Int32)(Math.Ceiling((double)summary.CountID / localFilter.PagerowCount) - 1);
            Refresh();
        }
        #endregion

        #region фильтры
        void InitFilters()
        {
            ProductStatusList productStatusList = new ProductStatusList();

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
        private void toolBarProduct_ButtonFilter()
        {
            var location = PointToScreen(new Point(0, 0));
            flexFilterContenerProductWindows.WindowStartupLocation = WindowStartupLocation.Manual;
            flexFilterContenerProductWindows.Left = toolBarProduct.FilterButton.PointToScreen(new Point(0, 0)).X + toolBarProduct.FilterButton.ActualWidth + 10;
            flexFilterContenerProductWindows.Top = location.Y = 70;
            flexFilterContenerProductWindows.AllowDrop = false;

            flexFilterContenerProductWindows.Value1 = "All";
            flexFilterContenerProductWindows.Value2 = "All";
            flexFilterContenerProductWindows.Value3 = "All";
            flexFilterContenerProductWindows.Value4 = "All";
            flexFilterContenerProductWindows.Value5 = "All";
            flexFilterContenerProductWindows.Value6 = "All";
            flexFilterContenerProductWindows.Value7 = "All";

            flexFilterContenerProductWindows.DataTableFilter1 = FilterStatus;
            flexFilterContenerProductWindows.DataTableFilter2 = FilterProcreator;
            flexFilterContenerProductWindows.DataTableFilter3 = FilterShowcase;
            flexFilterContenerProductWindows.DataTableFilter4 = FilterCategory;
            flexFilterContenerProductWindows.DataTableFilter5 = FilterCategoryDetails;
            flexFilterContenerProductWindows.DataTableFilter6 = FilterCreatedByUserID;
            flexFilterContenerProductWindows.DataTableFilter7 = FilterLastModifiedByUserID;
            flexFilterContenerProductWindows.TagPrice_Min = DefaultTagPriceWithVATMin;
            flexFilterContenerProductWindows.TagPrice_Max = DefaultTagPriceWithVATMax;
            flexFilterContenerProductWindows.Quantity_Min = DefaultquantityMin;
            flexFilterContenerProductWindows.Quantity_Max = DefaultquantityMax;

            flexFilterContenerProductWindows.ButtonFilter1Click += FlexFilterContenerProductWindows_ButtonFilter1Click;
            flexFilterContenerProductWindows.ButtonFilter2Click += FlexFilterContenerProductWindows_ButtonFilter2Click;
            flexFilterContenerProductWindows.ButtonFilter3Click += FlexFilterContenerProductWindows_ButtonFilter3Click;
            flexFilterContenerProductWindows.ButtonFilter4Click += FlexFilterContenerProductWindows_ButtonFilter4Click;
            flexFilterContenerProductWindows.ButtonFilter5Click += FlexFilterContenerProductWindows_ButtonFilter5Click;
            flexFilterContenerProductWindows.ButtonFilter6Click += FlexFilterContenerProductWindows_ButtonFilter6Click;
            flexFilterContenerProductWindows.ButtonFilter7Click += FlexFilterContenerProductWindows_ButtonFilter7Click;

            flexFilterContenerProductWindows.Show();
        }

        private void FlexFilterContenerProductWindows_ButtonFilter1Click(string text)
        {
            localFilter.Status = text;
            Refresh();
        }

        private void FlexFilterContenerProductWindows_ButtonFilter2Click(string text)
        {
            localFilter.Procreator = text;
            Refresh();
        }
        private void FlexFilterContenerProductWindows_ButtonFilter3Click(string text)
        {
            localFilter.Showcase = text;
            Refresh();
        }

        private void FlexFilterContenerProductWindows_ButtonFilter4Click(string text)
        {
            localFilter.Category = text;
            Refresh();
        }

        private void FlexFilterContenerProductWindows_ButtonFilter5Click(string text)
        {
            localFilter.CategoryDetails = text;
            Refresh();
        }

        private void FlexFilterContenerProductWindows_ButtonFilter6Click(string text)
        {
            localFilter.CreatedUserID = text;
            Refresh();
        }

        private void FlexFilterContenerProductWindows_ButtonFilter7Click(string text)
        {
            localFilter.LastModifiedUserID = text;
            Refresh();
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
    }
}
