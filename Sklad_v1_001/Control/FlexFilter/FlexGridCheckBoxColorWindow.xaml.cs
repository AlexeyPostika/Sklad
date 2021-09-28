using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
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

namespace Sklad_v1_001.Control.FlexFilter
{
    // <summary>
    /// Логика взаимодействия для FlexGridCheckBox.xaml
    /// </summary>
    ///
    public partial class FlexGridCheckBoxColorWindow : INotifyPropertyChanged
    {
        Boolean needRefresh;
        Boolean needResize;

        public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.Register(
           "IsMultiSelect",
           typeof(Boolean),
           typeof(FlexGridCheckBoxColorWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей 
        public Boolean IsMultiSelect
        {
            get
            {
                return (Boolean)GetValue(IsMultiSelectProperty);
            }
            set
            {

                SetValue(IsMultiSelectProperty, value);
                OnPropertyChanged("IsMultiSelect");
            }
        }

        public static readonly DependencyProperty IsSortCheckBoxProperty = DependencyProperty.Register(
            "IsSortCheckBox",
            typeof(Boolean),
            typeof(FlexGridCheckBoxColorWindow), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей 
        public Boolean IsSortCheckBox
        {
            get
            {
                return (Boolean)GetValue(IsSortCheckBoxProperty);
            }
            set
            {

                SetValue(IsSortCheckBoxProperty, value);
                OnPropertyChanged("IsSortCheckBox");
            }
        }

        public static readonly DependencyProperty IsVisibilityHeaderCheckBoxProperty = DependencyProperty.Register(
           "IsVisibilityHeaderCheckBox",
           typeof(Visibility),
           typeof(FlexGridCheckBoxColorWindow), new UIPropertyMetadata(Visibility.Visible));

        // Обычное свойство .NET  - обертка над свойством зависимостей 
        public Visibility IsVisibilityHeaderCheckBox
        {
            get
            {
                return (Visibility)GetValue(IsVisibilityHeaderCheckBoxProperty);
            }
            set
            {

                SetValue(IsVisibilityHeaderCheckBoxProperty, value);
                OnPropertyChanged("IsVisibilityHeaderCheckBox");
            }
        }

        public static readonly DependencyProperty VisibleColorProperty = DependencyProperty.Register(
                        "VisibleColor",
                        typeof(Visibility),
                        typeof(FlexGridCheckBoxColorWindow), new UIPropertyMetadata(Visibility.Collapsed));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility VisibleColor
        {
            get
            {
                return (Visibility)GetValue(VisibleColorProperty);
            }
            set
            {

                SetValue(VisibleColorProperty, value);
                OnPropertyChanged("VisibleColor");
            }
        }

        public static readonly DependencyProperty SuccessProperty = DependencyProperty.Register(
                "Success",
                typeof(Boolean),
                typeof(FlexGridCheckBoxColorWindow), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей  
        public Boolean Success
        {
            get
            {
                return (Boolean)GetValue(SuccessProperty);
            }
            set
            {

                SetValue(SuccessProperty, value);
                OnPropertyChanged("Success");
            }
        }


        public static readonly DependencyProperty CheckAllProperty = DependencyProperty.Register(
                        "CheckAll",
                        typeof(Boolean),
                        typeof(FlexGridCheckBoxColorWindow));

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

        public static readonly DependencyProperty TitleTextProperty = DependencyProperty.Register(
                        "TitleText",
                        typeof(String),
                        typeof(FlexGridCheckBoxColorWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String TitleText
        {
            get
            {
                return (String)GetValue(TitleTextProperty);
            }
            set
            {

                SetValue(TitleTextProperty, value);
                OnPropertyChanged("TitleText");
            }
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(FlexGridCheckBoxColorWindow));

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
                        typeof(FlexGridCheckBoxColorWindow));

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
                        typeof(FlexGridCheckBoxColorWindow));

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
                        typeof(FlexGridCheckBoxColorWindow), new PropertyMetadata(""));

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
                        typeof(FlexGridCheckBoxColorWindow));

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
                ElementDataGrid.ItemsSource = DataTableData.DefaultView;
                if (DataTableDataFull.Rows.Count == 0)
                {
                    DataTableDataFull = DataTableData.Copy();
                }
                OnPropertyChanged("DataTableData");
            }
        }

        public static readonly DependencyProperty DataTableDataFullProperty = DependencyProperty.Register(
                        "DataTableDataFull",
                        typeof(DataTable),
                        typeof(FlexGridCheckBoxColorWindow));

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

        public FlexGridCheckBoxColorWindow()
        {
            InitializeComponent();
            convertdata = new ConvertData();
            this.DataContext = this;
            this.ElementDataGrid.DataContext = this;
            NeedRefresh = false;
            ButtonOk.Image.Source = ImageHelper.GenerateImage("MessageOK.png");
            ButtonCancel.Image.Source = ImageHelper.GenerateImage("MessageCancel.png");
            DataTableDataFull = new DataTable();
            //this.Activated += ContentActivated;
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
                //FlexControl.FlexMessageBox.FlexMessageBox mb = new FlexControl.FlexMessageBox.FlexMessageBox();
                //mb.Show(Properties.Resources.WarningNotChoosenFilter, GenerateTitle(TitleType.Warning, Properties.Resources.EmptyFilter), MessageBoxButton.OK, MessageBoxImage.Warning);
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
                FilterStatusNone = false;
                FilterStatusAll = false;

                if (String.IsNullOrEmpty(Search))
                {
                    if (IsMultiSelect)
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
                    else
                    {
                        foreach (DataRow rowtable in DataTableData.Rows)
                        {
                            rowtable["IsChecked"] = "False";
                        }
                        currentRow["IsChecked"] = !convertdata.FlexDataConvertToBoolean(currentRow["IsChecked"].ToString());
                    }
                }

                ButtonApplyClick?.Invoke();
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (NeedRefresh)
            {
                FilterStatusNone = false;
                FilterStatusAll = false;

                if (String.IsNullOrEmpty(Search))
                {
                    if (IsMultiSelect)
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
                    else
                    {
                        foreach (DataRow rowtable in DataTableData.Rows)
                        {
                            rowtable["IsChecked"] = "False";
                        }

                        CheckBox currentCheckBox = sender as CheckBox;
                        if (currentCheckBox != null)
                        {
                            currentCheckBox.IsChecked = true;
                        }
                    }
                }
                ButtonApplyClick?.Invoke();
            }
        }

        private void control_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (Apply())
                {
                    this.Visibility = Visibility.Hidden;
                    //this.Activated += ContentActivated;
                    NeedResize = false;
                }
                e.Cancel = true;
            }
            catch
            {
            }
        }

        private void Close_ButtonCloseClick()
        {
            if (Apply())
            {
                this.Visibility = Visibility.Hidden;
                //this.Activated += ContentActivated;
                NeedResize = false;
            }
        }

        private void control_MouseEnter(object sender, MouseEventArgs e)
        {
            NeedRefresh = true;
        }

        private void ElementDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            NeedRefresh = false;
        }

        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            NeedRefresh = true;
        }

        private void ContentActivated(object sender, EventArgs e)
        {
            NeedResize = true;

            if (IsSortCheckBox)
            {
                ElementDataGrid.Items.SortDescriptions.Clear();
                ElementDataGrid.Items.SortDescriptions.Add(new SortDescription("IsChecked", ListSortDirection.Descending));
                ElementDataGrid.Items.Refresh();
            }

            if (IsMultiSelect)
            {
                ButtonWrapPanel.Visibility = Visibility.Collapsed;
                ButtonRowDefinition.Height = new GridLength(0);
            }
            else
            {
                IsVisibilityHeaderCheckBox = Visibility.Collapsed;
                ButtonRowDefinition.Height = new GridLength(50);
            }

            var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
            var frameWorkAreaActualWidth = MainWindow.AppWindow.frameWorkArea.ActualWidth;
            //if (this.Left - this.ActualWidth <= frameWorkAreaX)
            //{
            //    this.Left = frameWorkAreaX;
            //}
            //else
            //{
            //    this.Left = this.Left - this.ActualWidth;
            //}
            //if (this.Left + this.ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth)
            //{
            //    this.Left = frameWorkAreaX + frameWorkAreaActualWidth - this.ActualWidth;
            //}
            //if (this.Left + this.ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth)
            //{
            //    this.MaxWidth = frameWorkAreaActualWidth - this.Left + frameWorkAreaX;
            //}
            //this.Activated -= ContentActivated;
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
                    var search1Modified = search1.Replace("[", "[[]").Replace("%", "[%]");
                    String temp = @"Description like '%" + search1Modified + "%'";
                    if (String.IsNullOrEmpty(ComplexSearch))
                    {
                        ComplexSearch = temp;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(search1Modified))
                        {
                            ComplexSearch = ComplexSearch + " or " + temp;
                        }
                    }
                }
                try
                {
                    foreach (DataRow row in DataTableDataFull.Select(ComplexSearch))
                    {
                        row["IsChecked"] = false;
                        DataTableData.ImportRow(row);
                    }
                }
                catch (Exception e)
                {

                }

            }
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Search))
                MassUpdateCheckBox((Boolean)(sender as CheckBox).IsChecked);
            else
                MassUpdateCheckBoxWithSearch((Boolean)(sender as CheckBox).IsChecked);
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

        private void control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (NeedResize)
            {
                var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
                var frameWorkAreaActualWidth = MainWindow.AppWindow.frameWorkArea.ActualWidth;
                //if (this.Left + this.ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth - 10)
                //{
                //   // this.MaxWidth = frameWorkAreaActualWidth - this.Left + frameWorkAreaX - 10;
                //}
            }
        }

        private void ButtonOk_ButtonClick()
        {
            Success = true;
            //this.Close();
        }

        private void ButtonCancel_ButtonClick()
        {
            Success = false;
           // this.Close();
        }

        private void ElementDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!IsMultiSelect)
            {
                Success = true;
                //this.Close();
            }
        }
    }
}
