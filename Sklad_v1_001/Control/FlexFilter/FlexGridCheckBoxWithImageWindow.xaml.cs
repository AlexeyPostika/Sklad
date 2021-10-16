using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;


namespace Sklad_v1_001.Control.FlexFilter
{
    /// <summary>
    /// Логика взаимодействия для FlexGridCheckBox.xaml
    /// </summary>
    ///
    public partial class FlexGridCheckBoxWithImageWindow : INotifyPropertyChanged, IAbstractGridFilter
    {
        Boolean needRefresh;
        Boolean needResize;

        public static readonly DependencyProperty CheckAllProperty = DependencyProperty.Register(
                        "CheckAll",
                        typeof(Boolean),
                        typeof(FlexGridCheckBoxWithImageWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей      

        public Boolean CheckAll
        {
            get
            {
                return (Boolean)GetValue(CheckAllProperty);
            }
            set
            {

                SetValue(CheckAllProperty, value);
                OnPropertyChanged("CheckAll");
            }
        }


        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(FlexGridCheckBoxWithImageWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String LabelText
        {
            get
            {
                return (String)GetValue(LabelTextProperty);
            }
            set
            {

                SetValue(LabelTextProperty, value);
                OnPropertyChanged("LabelText");
            }
        }

        public static readonly DependencyProperty FilterStatusAllProperty = DependencyProperty.Register(
                        "FilterStatusAll",
                        typeof(Boolean),
                        typeof(FlexGridCheckBoxWithImageWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean FilterStatusAll
        {
            get
            {
                return (Boolean)GetValue(FilterStatusAllProperty);
            }
            set
            {

                SetValue(FilterStatusAllProperty, value);
                OnPropertyChanged("FilterStatusAll");
            }
        }

        public static readonly DependencyProperty FilterStatusNoneProperty = DependencyProperty.Register(
                        "FilterStatusNone",
                        typeof(Boolean),
                        typeof(FlexGridCheckBoxWithImageWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean FilterStatusNone
        {
            get
            {
                return (Boolean)GetValue(FilterStatusNoneProperty);
            }
            set
            {

                SetValue(FilterStatusNoneProperty, value);
                OnPropertyChanged("FilterStatusNone");
            }
        }

        public static readonly DependencyProperty SearchProperty = DependencyProperty.Register(
                        "Search",
                        typeof(String),
                        typeof(FlexGridCheckBoxWithImageWindow), new PropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String Search
        {
            get
            {
                return (String)GetValue(SearchProperty);
            }
            set
            {

                SetValue(SearchProperty, value);
                OnPropertyChanged("Search");
            }
        }

        public static readonly DependencyProperty DataTableDataProperty = DependencyProperty.Register(
                        "DataTableData",
                        typeof(DataTable),
                        typeof(FlexGridCheckBoxWithImageWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public DataTable DataTableData
        {
            get
            {
                return (DataTable)GetValue(DataTableDataProperty);
            }
            set
            {
                SetValue(DataTableDataProperty, value);
                if (DataTableDataFull.Rows.Count == 0)
                {
                    DataTableDataFull = DataTableData.Copy();
                }
                ElementDataGrid.ItemsSource = DataTableData.DefaultView;
                OnPropertyChanged("DataTableData");
            }
        }

        public static readonly DependencyProperty DataTableDataFullProperty = DependencyProperty.Register(
                        "DataTableDataFull",
                        typeof(DataTable),
                        typeof(FlexGridCheckBoxWithImageWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public DataTable DataTableDataFull
        {
            get
            {
                return (DataTable)GetValue(DataTableDataFullProperty);
            }
            set
            {
                SetValue(DataTableDataFullProperty, value);
                OnPropertyChanged("DataTableDataFull");
            }
        }

        public string FilterText
        {
            get
            {
                return LabelText;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FilterValue
        {
            get
            {
                DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
                DataRow currentRow = null;
                if (currentRowView != null)
                {
                    currentRow = currentRowView.Row as DataRow;
                    return currentRow["ID"].ToString();
                }
                return "";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FilterDescription
        {
            get
            {
                DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
                DataRow currentRow = null;
                if (currentRowView != null)
                {
                    currentRow = currentRowView.Row as DataRow;
                    return currentRow["Description"].ToString();
                }
                return "";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Boolean IsChecked
        {
            get
            {
                DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
                DataRow currentRow = null;
                if (currentRowView != null)
                {
                    currentRow = currentRowView.Row as DataRow;
                    return convertdata.FlexDataConvertToBoolean(currentRow["IsChecked"].ToString());
                }
                return CheckAll;
            }

            set
            {
                throw new NotImplementedException();
            }
        }


        public Boolean NeedResize
        {
            get
            {
                return needResize;
            }

            set
            {
                needResize = value;
            }
        }

        public Boolean NeedRefresh
        {
            get
            {
                return needRefresh;
            }

            set
            {
                needRefresh = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action ButtonApplyClick;
        ConvertData convertdata;
        public FlexGridCheckBoxWithImageWindow()
        {
            InitializeComponent();
            convertdata = new ConvertData();
            DataTableDataFull = new DataTable();
            DataTableData = new DataTable();
            this.DataContext = this;
            this.ElementDataGrid.DataContext = this;
            NeedRefresh = false;
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.Activated += ContentActivated;
            Search = "";
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Search))
                MassUpdateCheckBox((Boolean)(sender as System.Windows.Controls.CheckBox).IsChecked);
            else
                MassUpdateCheckBoxWithSearch((Boolean)(sender as System.Windows.Controls.CheckBox).IsChecked);
        }

        private Boolean Apply()
        {
            Boolean IsCheckedOne = false;
            Boolean IsCheckedAll = true;

            foreach (DataRow rowtable in DataTableData.Rows)
            {
                if (rowtable["IsChecked"].ToString().ToLower() == "true")
                {
                    IsCheckedOne = true;
                }
                if (rowtable["IsChecked"].ToString().ToLower() == "false")
                {
                    IsCheckedAll = false;
                }
            }

            if (IsCheckedOne == false)
            {
                FilterStatusNone = true;
                FilterStatusAll = false;
            }
            else
            {
                FilterStatusAll = IsCheckedAll;
                FilterStatusNone = false;
            }

            if (!IsCheckedOne && DataTableDataFull.Rows.Count > 0)
            {
                FlexMessageBox.FlexMessageBox mb = new FlexMessageBox.FlexMessageBox();
                mb.Show(Properties.Resources.WarningNotChoosenFilter, GenerateTitle(TitleType.Warning, Properties.Resources.EmptyFilter), MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void ElementDataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
            DataRow currentRow = null;
            if (currentRowView != null)
            {
                currentRow = currentRowView.Row as DataRow;
                currentRow["IsChecked"] = !convertdata.FlexDataConvertToBoolean(currentRow["IsChecked"].ToString());
            }

            if (NeedRefresh)
            {
                Boolean IsCheckedOne = false;
                Boolean IsCheckedAll = true;
                foreach (DataRow rowtable in DataTableData.Rows)
                {
                    if (rowtable["IsChecked"].ToString().ToLower() == "true")
                    {
                        IsCheckedOne = true;
                    }
                    if (rowtable["IsChecked"].ToString().ToLower() == "false")
                    {
                        IsCheckedAll = false;
                    }
                }

                if (IsCheckedOne == false)
                    CheckAll = false;

                if (IsCheckedAll == true)
                    CheckAll = true;

                if (IsCheckedOne == false)
                {
                    FilterStatusNone = true;
                    FilterStatusAll = false;
                }
                else
                {
                    FilterStatusAll = IsCheckedAll;
                    FilterStatusNone = false;
                }
                ButtonApplyClick?.Invoke();
            }
        }

        private void control_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (Apply())
                    this.Visibility = Visibility.Hidden;
                e.Cancel = true;
            }
            catch
            {
            }
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }

        const Int32 WM_SYSCOMMAND = 0x0112;
        const Int32 SC_MOVE = 0xF010;

        private IntPtr WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref Boolean handled)
        {

            switch (msg)
            {
                case WM_SYSCOMMAND:
                    Int32 command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }

        private void Close_ButtonCloseClick()
        {
            if (Apply())
            {
                this.Visibility = Visibility.Hidden;
                this.Activated += ContentActivated;
                NeedResize = false;
            }
        }

        private void control_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            NeedRefresh = true;
        }

        private void ElementDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            NeedRefresh = false;
        }

        private void control_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            NeedRefresh = true;
        }

        private void ContentActivated(object sender, EventArgs e)
        {
            NeedResize = true;
            var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
            var frameWorkAreaActualWidth = MainWindow.AppWindow.frameWorkArea.ActualWidth;
            if (this.Left - this.ActualWidth <= frameWorkAreaX)
                this.Left = frameWorkAreaX;
            else
                this.Left = this.Left - this.ActualWidth;
            if (this.Left + this.ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth)
            {
                this.MaxWidth = frameWorkAreaActualWidth - this.Left;
            }
            this.Activated -= ContentActivated;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (NeedRefresh)
            {
                FilterStatusNone = false;
                FilterStatusAll = false;

                if (String.IsNullOrEmpty(Search))
                {

                    Boolean IsCheckedOne = false;
                    Boolean IsCheckedAll = true;
                    foreach (DataRow rowtable in DataTableData.Rows)
                    {
                        if (rowtable["IsChecked"].ToString().ToLower() == "true")
                        {
                            IsCheckedOne = true;
                        }
                        if (rowtable["IsChecked"].ToString().ToLower() == "false")
                        {
                            IsCheckedAll = false;
                        }
                    }

                    if (IsCheckedOne == false)
                        CheckAll = false;

                    if (IsCheckedAll == true)
                        CheckAll = true;

                    if (IsCheckedOne == false)
                    {
                        FilterStatusNone = true;
                        FilterStatusAll = false;
                    }
                    else
                    {
                        FilterStatusAll = IsCheckedAll;
                        FilterStatusNone = false;
                    }
                }
                ButtonApplyClick?.Invoke();
            }
        }

        private void MassUpdateCheckBox(Boolean _checked)
        {
            ElementDataGrid.SelectedItem = null;
            NeedRefresh = false;
            foreach (DataRow row in DataTableData.Rows)
            {
                row["IsChecked"] = _checked;
            }
            NeedRefresh = true;
            FilterStatusAll = _checked;
            FilterStatusNone = !_checked;
            ButtonApplyClick?.Invoke();
        }

        private void MassUpdateCheckBoxWithSearch(Boolean _checked)
        {
            ElementDataGrid.SelectedItem = null;
            NeedRefresh = false;
            foreach (DataRow row in DataTableData.Rows)
            {
                row["IsChecked"] = _checked;
            }
            NeedRefresh = true;
            FilterStatusAll = false;
            FilterStatusNone = false;
            ButtonApplyClick?.Invoke();
        }

        private void SearchFilter_ButtonClearClick()
        {
            Search = "";
            CheckAll = true;
            ElementDataGrid.SelectedItem = null;
            NeedRefresh = false;
            foreach (DataRow row in DataTableData.Rows)
            {
                row["IsChecked"] = true;
            }
            NeedRefresh = true;
            FilterStatusAll = true;
            FilterStatusNone = false;
            ButtonApplyClick?.Invoke();
        }

        private void SearchFilter_ButtonTextChangedClick()
        {
            if (String.IsNullOrEmpty(Search))
            {
                CheckAll = true;
                MassUpdateCheckBox(CheckAll);
                DataTableData.Rows.Clear();
                foreach (DataRow row in DataTableDataFull.Select())
                {
                    row["IsChecked"] = true;
                    DataTableData.ImportRow(row);
                }
            }
            else
            {
                if (CheckAll)
                {
                    CheckAll = false;
                    MassUpdateCheckBox(CheckAll);
                }
                DataTableData.Rows.Clear();
                string[] stringarray = Search.Split('+');
                String ComplexSearch = "";
                foreach (string search1 in stringarray)
                {
                    String temp = "Description like '%" + search1 + "%'";
                    if (String.IsNullOrEmpty(ComplexSearch))
                    {
                        ComplexSearch = ComplexSearch + temp;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(search1))
                        {
                            ComplexSearch = ComplexSearch + " or " + temp;
                        }
                    }
                }

                foreach (DataRow row in DataTableDataFull.Select(ComplexSearch))
                {
                    row["IsChecked"] = false;
                    DataTableData.ImportRow(row);
                }
            }
        }

        private void control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (NeedResize)
            {
                var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
                var frameWorkAreaActualWidth = MainWindow.AppWindow.frameWorkArea.ActualWidth;
                if (this.Left + this.ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth - 10)
                {
                    this.MaxWidth = frameWorkAreaActualWidth - this.Left + frameWorkAreaX - 10;
                }
            }
        }
    }
}
