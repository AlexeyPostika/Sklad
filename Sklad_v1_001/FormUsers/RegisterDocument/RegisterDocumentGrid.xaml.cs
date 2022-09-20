using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.FormUsers.RegisterDocumentDetails;
using Sklad_v1_001.FormUsers.RegisterDocumentPayment;
using Sklad_v1_001.FormUsers.RegisterDocumetnDelivery;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.HelperGlobal.StoreAPI;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocument;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentDelivery;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentDetails;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentPayment;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
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
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.FormUsers.RegisterDocument
{
    /// <summary>
    /// Логика взаимодействия для RegisterDocumentGrid.xaml
    /// </summary>
    public partial class RegisterDocumentGrid : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        Attributes attributes;
        LocalRow document;

        RegisterDocument.LocalFilter filter;

        RegisterDocumentLogic registerDocumentLogic;
        RegisterDocumentDetailsLogic registerDocumentDetailsLogic;
        RegisterDocumentDeliveryLogic registerDocumentDeliveryLogic;
        RegisterDocumentPaymentLogic registerDocumentPaymentLogic;

        ObservableCollection<LocalRow> datalist;
        ObservableCollection<RegisterDocumentDetails.LocaleRow> datalistDetails;
        ObservableCollection<RegisterDocumetnDelivery.LocaleRow> datalistDelivery;
        ObservableCollection<RegisterDocumentPayment.LocaleRow> datalistPayment;
       
        RowSummary summary;

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
        public LocalRow Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;             
            }
        }
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

        //схема структуры регистрации документов

        public RegisterDocumentGrid(Attributes _attributes)
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

            convertData = new ConvertData();

            registerDocumentLogic = new RegisterDocumentLogic(attributes);
            registerDocumentDetailsLogic = new RegisterDocumentDetailsLogic(attributes);
            registerDocumentDeliveryLogic = new RegisterDocumentDeliveryLogic(attributes);
            registerDocumentPaymentLogic = new RegisterDocumentPaymentLogic(attributes);

            datalist = new ObservableCollection<LocalRow>();
            datalistDetails = new ObservableCollection<RegisterDocumentDetails.LocaleRow>();
            datalistDelivery = new ObservableCollection<RegisterDocumetnDelivery.LocaleRow>();
            datalistPayment = new ObservableCollection<RegisterDocumentPayment.LocaleRow>();

            Document = new LocalRow();
            filter = new LocalFilter();

            summary = new RowSummary();

            this.registerDocument.ItemsSource = datalist;
            //this.SypplyDocumentDetails.Items.Clear();
            this.DataProduct.ItemsSource = datalistDetails;
            this.DataDelivery.ItemsSource = datalistDelivery;
            this.DataPayment.ItemsSource = datalistPayment;

            registerDocumentLogic.InitFilters();
            InitFilters();
            Refresh();
            IsAllowFilter = true;
        }

        #region Toolbar верхний
        private void ToolBarSaleDocument_ButtonEdit()
        {
            List<LocalRow> currentrow = this.registerDocument.SelectedItems.Cast<LocalRow>().ToList();
            if (currentrow != null && currentrow.Count() > 0)
            {
                EditDetails(currentrow.First());
            }
        }
        private void ToolBarSaleDocument_ButtonDelete()
        {

        }

        public void EditDetails(LocalRow document)
        {
            MainWindow.AppWindow.ButtonNewRegisterDocumentF(document);
        }

        private void ToolBarSaleDocument_ButtonClear()
        {
            InitFilters();
            filter = new LocalFilter();
            filter.Status = "All";
            filter.LastModifiedByUserID = "All";
            filter.Shop = "All";
            filter.CreatedByUserID = "All";
            filter.DeliveryID = "All";

            filter.AmountMin = AmountMin;
            filter.AmountMax = AmountMax;
            ClearfilterStatusID = ImageHelper.GenerateImage("IconFilter.png");
            Refresh();
        }

        private void ToolBarSaleDocument_ButtonScan(string text)
        {
            filter.Search = text;
            Refresh();
        }

        private void ToolBarSaleDocument_ButtonClean()
        {
            ToolBarRegisterDocument.Scan.Text = String.Empty;
            Refresh();
        }
        private void ToolBarSaleDocument_ButtonRefresh()
        {
            //List<DataTable> listDocument = registerDocumentLogic.GridComplexMultiple("SupplyDocument");
            //if (listDocument != null && listDocument.Count() > 0)
            //{
            //    registerDocumentLogic.SendRequest(listDocument);
            //    Refresh();
            //}
            Request request = new Request(attributes);
            request.supplyDocument.Document.Status = 6;//затягиваем документы, которые нужно подтвердить
            Response response = request.GetCommand(2);
            if (response != null && response.ErrorCode==0)
            {
                Save(response);
                InitFilters();
                Refresh();
            }
            else
            {
                FlexMessageBox mb2 = new FlexMessageBox();
                List<BitmapImage> ButtonImages = new List<BitmapImage>();
                ButtonImages.Add(ImageHelper.GenerateImage("IconAdd.png"));
                ButtonImages.Add(ImageHelper.GenerateImage("IconContinueWork.png"));
                List<string> ButtonText = new List<string>();
                ButtonText.Add(Properties.Resources.AddSmall);
                ButtonText.Add(Properties.Resources.MessageIgnore);

                mb2.Show("Ошибка: " + response.ErrorCode + " - " + response.DescriptionEX, GenerateTitle(TitleType.Error, Properties.Resources.ErrorSendAPITitle), MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        #endregion

        #region DataGrid список документов
        private void saleDocument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<LocalRow> currentrow = this.registerDocument.SelectedItems.Cast<LocalRow>().ToList();
            if (currentrow != null && currentrow.Count() > 0)
            {
                //if (currentrow.Last().SupplyDocumentNumber > 0)
                //    IsEnableDeleted = false;
                //else
                //    IsEnableDeleted = true;
                //чистим все детали
                datalistDetails.Clear();
                datalistDelivery.Clear();
                datalistPayment.Clear();

                DataTable dataTableSupplyDocumentDetails = registerDocumentDetailsLogic.FillGridDocument(currentrow.Last().ID);
                foreach (DataRow row in dataTableSupplyDocumentDetails.Rows)
                {
                    datalistDetails.Add(registerDocumentDetailsLogic.Convert(row, new RegisterDocumentDetails.LocaleRow()));
                }

                DataTable dataTableSupplyDocumentDelivery = registerDocumentDeliveryLogic.FillGrid(currentrow.Last().ID);
                foreach (DataRow row in dataTableSupplyDocumentDelivery.Rows)
                {
                    datalistDelivery.Add(registerDocumentDeliveryLogic.Convert(row, new RegisterDocumetnDelivery.LocaleRow()));
                }

                DataTable dataTableSupplyDocumentPayment = registerDocumentPaymentLogic.FillGrid(currentrow.Last().ID);
                foreach (DataRow row in dataTableSupplyDocumentPayment.Rows)
                {
                    datalistPayment.Add(registerDocumentPaymentLogic.Convert(row, new RegisterDocumentPayment.LocaleRow()));
                }

            }
        }
        private void saleDocument_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<LocalRow> currentrow = this.registerDocument.SelectedItems.Cast<LocalRow>().ToList();
            if (currentrow != null && currentrow.Count() > 0)
            {
                EditDetails(currentrow.First());
            }
        }
        private void saleDocument_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region RegisterDetails
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

        #region Filters
        void InitFilters()
        {
            ClearfilterShopID = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterUserName = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterInputUserName = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterQuantity = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterStatusID = ImageHelper.GenerateImage("IconFilter.png");
            ClearfilterAmount = ImageHelper.GenerateImage("IconFilter.png");
 
            SupplyTypeList supplyTypeList = new SupplyTypeList();
            FilterIDShop.Clear();
            if (registerDocumentLogic.GetFilter("ShopID") != null)
            {
                foreach (DataRow row in registerDocumentLogic.GetFilter("ShopID").Rows)
                {
                    DataRow newrow = FilterIDShop.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterIDShop.Rows.Add(newrow);
                }
            }

            FilterInputUserID.Clear();
            if (registerDocumentLogic.GetFilter("InputUserID") != null)
            {
                foreach (DataRow row in registerDocumentLogic.GetFilter("InputUserID").Rows)
                {
                    DataRow newrow = FilterInputUserID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterInputUserID.Rows.Add(newrow);
                }
            }

            FilterIDManagerName.Clear();
            if (registerDocumentLogic.GetFilter("ManagerName") != null)
            {
                foreach (DataRow row in registerDocumentLogic.GetFilter("ManagerName").Rows)
                {
                    DataRow newrow = FilterIDManagerName.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterIDManagerName.Rows.Add(newrow);
                }
            }

            FilterIDDelivery.Clear();
            if (registerDocumentLogic.GetFilter("Delivery") != null)
            {
                foreach (DataRow row in registerDocumentLogic.GetFilter("Delivery").Rows)
                {
                    DataRow newrow = FilterIDDelivery.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterIDDelivery.Rows.Add(newrow);
                }
            }

            FilterIDStatus.Clear();
            if (registerDocumentLogic.GetFilter("Status") != null)
            {
                foreach (DataRow row in registerDocumentLogic.GetFilter("Status").Rows)
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
            if (registerDocumentLogic.GetFilter("LastModifiedByUserID") != null)
            {
                foreach (DataRow row in registerDocumentLogic.GetFilter("LastModifiedByUserID").Rows)
                {
                    DataRow newrow = FilterLastModifiedByUserID.NewRow();
                    newrow["ID"] = row["ID"];
                    newrow["IsChecked"] = true;
                    newrow["Description"] = row["Description"];
                    FilterLastModifiedByUserID.Rows.Add(newrow);
                }
            }

            QuantityMin = registerDocumentLogic.GetFromToFilter("Quantity").min;
            QuantityMax = registerDocumentLogic.GetFromToFilter("Quantity").max;
            DefaultquantityMin = registerDocumentLogic.GetFromToFilter("Quantity").min;
            DefaultquantityMax = registerDocumentLogic.GetFromToFilter("Quantity").max;
            filter.QuantityMin = registerDocumentLogic.GetFromToFilter("Quantity").min;
            filter.QuantityMax = registerDocumentLogic.GetFromToFilter("Quantity").max;

            AmountMin = registerDocumentLogic.GetFromToFilter("Amount").min;
            AmountMax = registerDocumentLogic.GetFromToFilter("Amount").max;
            DefaultamountMin = registerDocumentLogic.GetFromToFilter("Amount").min;
            DefaultamountMax = registerDocumentLogic.GetFromToFilter("Amount").max;
            filter.AmountMin = registerDocumentLogic.GetFromToFilter("Amount").min;
            filter.AmountMax = registerDocumentLogic.GetFromToFilter("Amount").max;

        }

        private void FilterShopID_ButtonApplyClick(string text)
        {
            filter.Shop = text;
            Refresh();
        }

        private void FilterQuantity_ButtonApplyClick()
        {
            filter.QuantityMax = QuantityMax;
            filter.QuantityMin = QuantityMin;
            Refresh();
        }

        private void FilterAmount_ButtonApplyClick()
        {

            filter.AmountMax = AmountMax;
            filter.AmountMin = AmountMin;
            Refresh();
        }

        private void filterIdUserInput_ButtonApplyClick(string text)
        {
            filter.CreatedByUserID = text;
            Refresh();
        }

        private void FilterUserID_ButtonApplyClick(string text)
        {
            filter.LastModifiedByUserID = text;
            Refresh();
        }

        #endregion

        #region Refresh
        public void Refresh()
        {
            DataTable datatable = registerDocumentLogic.FillGrid(filter);
            datalist.Clear();

            foreach (DataRow row in datatable.Rows)
            {
                datalist.Add(registerDocumentLogic.Convert(row, new LocalRow()));
            }

            CalculateSummary();

            TotalCount = summary.SummaryQuantityLine;
            PageCount = filter.PagerowCount;
            CurrentPage = filter.PageNumber;
        }

        #endregion

        #region CalculateSummary
        public void CalculateSummary()
        {
            DataTable datatable1 = registerDocumentLogic.FillSummary(filter);
            foreach (DataRow row in datatable1.Rows)
            {
                registerDocumentLogic.ConvertSummary(row, summary);
            }

        }
        #endregion


        #region SAVE

        public static DataTable Generate(List<String> colNames, List<String> colTypes)
        {
            DataTable result = new DataTable();

            for (Int32 i = 0; i < Math.Min(colNames.Count, colTypes.Count); i++)

            {
                result.Columns.Add(new DataColumn { DataType = System.Type.GetType($"System.{colTypes[i]}"), ColumnName = colNames[i] });

            }
            return result;

        }
        public void Save(Response response)
        {
            DataTable updateDataTable = Generate(new List<String>
            {
                "DocumentID",
                "DeliveryID",
                "DeliveryDetailsID"

        }, new List<String>
            {
                "Int32",
                "Int32",
                "Int32",
                //"DateTime",
                //"Int64",
                //"Int64",
                //"DateTime",
                //"Int32",
                //"DateTime",
                //"Int32",
                //"Int32",
                //"Int32",
                //"Int32",
                //"String"
            });

            Document.ShemaStorаgeLocal.Clear();
            foreach (SupplyDocumentRequest rowResponse in response.listSupplyDocumentOutput.ListDocuments)
            {

                //DataRow dataRow = updateDataTable.NewRow();
                //dataRow["Count"] = rowResponse.Document.Count;
                //dataRow["Amount"] = rowResponse.Document.Amount;
                //dataRow["ReffID"] = rowResponse.Document.ID;
                //dataRow["ReffDate"] = SqlDateTime.MinValue.Value;
                //dataRow["RegisterDocumentNumber"] = 123453543;
                //dataRow["SupplyDocumentNumber"] = rowResponse.Document.SupplyDocumentNumber;
                //dataRow["CreatedDate"] = rowResponse.Document.CreatedDate == null ? DateTime.Now : rowResponse.Document.CreatedDate;
                //dataRow["CreatedUserID"] = rowResponse.Document.CreatedUserID;
                //dataRow["LastModificatedDate"] = DateTime.Now; //;
                //dataRow["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                //dataRow["Status"] = rowResponse.Document.Status == 6 ? 0 : rowResponse.Document.Status;
                //dataRow["ShopID"] = rowResponse.Document.ShopID;
                //dataRow["CompanyID"] = rowResponse.Document.CompanyID;
                //dataRow["ReffTimeRow"] = rowResponse.Document.TimeRow;
                //updateDataTable.Rows.Add(dataRow);

                DataRow row = Document.ShemaStorаgeLocal.RegisterDocument.NewRow();
                row["Count"] = rowResponse.Document.Count;
                row["Amount"] = rowResponse.Document.Amount;
                row["ReffID"] = rowResponse.Document.ID;
                row["ReffDate"] = SqlDateTime.MinValue.Value;
                //row["RegisterDocumentNumber"] = 23243432;
                row["SupplyDocumentNumber"] = rowResponse.Document.SupplyDocumentNumber;
                row["CreatedDate"] = rowResponse.Document.CreatedDate == null ? DateTime.Now : rowResponse.Document.CreatedDate;
                row["CreatedUserID"] = rowResponse.Document.CreatedUserID;
                row["LastModificatedDate"] = DateTime.Now;
                row["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                row["Status"] = rowResponse.Document.Status == 6 ? 0 : rowResponse.Document.Status;
                row["ShopID"] = rowResponse.Document.ShopID;
                row["CompanyId"] = rowResponse.Document.CompanyID;
                row["ReffTimeRow"] = rowResponse.Document.TimeRow;
                Document.ShemaStorаgeLocal.RegisterDocument.Rows.Add(row);

                foreach (SupplyDocumentDeliveryRequest rowDeliveryReff in rowResponse.Delivery)
                {                  
                    DataRow rowDelivery = Document.ShemaStorаgeLocal.RegisterDocumentDelivery.NewRow();
                    rowDelivery["DocumentID"] = rowDeliveryReff.DocumentID;
                    rowDelivery["DeliveryID"] = rowDeliveryReff.DeliveryID;
                    rowDelivery["DeliveryDetailsID"] = rowDeliveryReff.DeliveryDetailsID;
                    rowDelivery["DeliveryTTN"] = "";
                    if (rowDeliveryReff.ImageTTN != null)
                        rowDelivery["ImageTTN"] = rowDeliveryReff.ImageTTN;
                    rowDelivery["Invoice"] = rowDeliveryReff.Invoice;
                    if (rowDeliveryReff.ImageInvoice != null)
                        rowDelivery["ImageInvoice"] = rowDeliveryReff.ImageInvoice;
                    rowDelivery["AmountUSA"] = rowDeliveryReff.AmountUSA;
                    rowDelivery["AmountRUS"] = rowDeliveryReff.AmountRUS;
                    rowDelivery["Description"] = rowDeliveryReff.Description;
                    rowDelivery["DeliveryTTN"] = rowDeliveryReff.DeliveryTTN;
                    rowDelivery["CreatedDate"] = rowDeliveryReff.CreatedDate == null ? DateTime.Now : rowDeliveryReff.CreatedDate;
                    rowDelivery["CreatedUserID"] = rowDeliveryReff.CreatedUserID;
                    rowDelivery["LastModificatedDate"] = DateTime.Now;
                    rowDelivery["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                    rowDelivery["ShopID"] = rowDeliveryReff.ShopID;
                    rowDelivery["CompanyId"] = rowDeliveryReff.CompanyID;
                    rowDelivery["ReffTimeRow"] = rowDeliveryReff.TimeRow;
                    Document.ShemaStorаgeLocal.RegisterDocumentDelivery.Rows.Add(rowDelivery);
                }
                foreach (SupplyDocumentDetailsRequest rowDetailsReff in rowResponse.Details)
                {
                    DataRow rowDetails = Document.ShemaStorаgeLocal.RegisterDocumentDetails.NewRow();
                    rowDetails["DocumentID"] = rowDetailsReff.DocumentID;
                    rowDetails["Name"] = rowDetailsReff.Name;
                    rowDetails["Quantity"] = rowDetailsReff.Quantity;
                    rowDetails["TagPriceUSA"] = rowDetailsReff.TagPriceUSA;
                    rowDetails["TagPriceRUS"] = rowDetailsReff.TagPriceRUS;
                    rowDetails["CategoryID"] = rowDetailsReff.CategoryID;
                    rowDetails["CategoryDetailsID"] = rowDetailsReff.CategoryDetailsID;
                    if (rowDetailsReff.ImageProduct != null)
                        rowDetails["ImageProduct"] = rowDetailsReff.ImageProduct;
                    rowDetails["Barecodes"] = rowDetailsReff.BarcodesShop;
                    rowDetails["CreatedDate"] = rowDetailsReff.CreatedDate == null ? DateTime.Now : rowDetailsReff.CreatedDate;
                    rowDetails["CreatedUserID"] = rowDetailsReff.CreatedUserID;
                    rowDetails["LastModificatedDate"] = DateTime.Now;
                    rowDetails["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                    rowDetails["Model"] = rowDetailsReff.Model;
                    rowDetails["SizeProduct"] = rowDetailsReff.SizeProduct;
                    rowDetails["Size"] = rowDetailsReff.Size;
                    rowDetails["ShopID"] = rowDetailsReff.ShopID;
                    rowDetails["CompanyId"] = rowDetailsReff.CompanyID;
                    rowDetails["ReffTimeRow"] = rowDetailsReff.TimeRow;
                    Document.ShemaStorаgeLocal.RegisterDocumentDetails.Rows.Add(rowDetails);
                }
                foreach (SupplyDocumentPaymentRequest rowPaymentReff in rowResponse.Payment)
                {
                    DataRow rowPayment = Document.ShemaStorаgeLocal.RegisterDocumentPayment.NewRow();
                    rowPayment["DocumentID"] = rowPaymentReff.DocumentID;
                    rowPayment["Status"] = rowPaymentReff.Status;
                    rowPayment["OperationType"] = rowPaymentReff.OperationType;
                    rowPayment["Amount"] = rowPaymentReff.Amount;
                    rowPayment["Description"] = rowPaymentReff.Description;
                    rowPayment["RRN"] = rowPaymentReff.RRN;
                    rowPayment["CreatedDate"] = rowPaymentReff.CreatedDate == null ? DateTime.Now : rowPaymentReff.CreatedDate;
                    rowPayment["CreatedUserID"] = rowPaymentReff.CreatedUserID;
                    rowPayment["LastModificatedDate"] = DateTime.Now;
                    rowPayment["LastModificatedUserID"] = attributes.numeric.userEdit.AddUserID;
                    rowPayment["ShopID"] = rowPaymentReff.ShopID;
                    rowPayment["CompanyId"] = rowPaymentReff.CompanyID;
                    rowPayment["ReffTimeRow"] = rowPaymentReff.TimeRow;
                    Document.ShemaStorаgeLocal.RegisterDocumentPayment.Rows.Add(rowPayment);
                }
            }
            registerDocumentLogic.SaveRowTable(Document);
        }
        #endregion

        private void DataDelivery_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
