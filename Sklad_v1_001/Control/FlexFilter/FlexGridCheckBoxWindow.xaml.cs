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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Sklad_v1_001.Control.FlexFilter
{ 
    /// <summary>
    /// Логика взаимодействия для FlexGridCheckBox.xaml
    /// </summary>
    ///
    public partial class FlexGridCheckBoxWindow : INotifyPropertyChanged, IAbstractGridFilter
    {
        Boolean needRefresh; 
        public static readonly DependencyProperty CheckAllProperty = DependencyProperty.Register(
                        "CheckAll",
                        typeof(Boolean),
                        typeof(FlexGridCheckBoxWindow));

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
                        typeof(FlexGridCheckBoxWindow));

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
                        typeof(FlexGridCheckBoxWindow));

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
                        typeof(FlexGridCheckBoxWindow));

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

        public static readonly DependencyProperty DataTableDataProperty = DependencyProperty.Register(
                        "DataTableData",
                        typeof(DataTable),
                        typeof(FlexGridCheckBoxWindow));

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
                OnPropertyChanged("DataTableData");
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
                //DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
                //DataRow currentRow = null;
                //if (currentRowView != null)
                //{
                //    currentRow = currentRowView.Row as DataRow;
                //    return convertdata.FlexDataConvertToBoolean(currentRow["IsChecked"].ToString());
                //}
                return CheckAll;
            }

            set
            {
                throw new NotImplementedException();
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

        public FlexGridCheckBoxWindow()
        {            
            InitializeComponent();
            convertdata = new ConvertData();
            this.DataContext = this;            
            this.ElementDataGrid.DataContext = this;
            NeedRefresh = false;
            //this.Activated += ContentActivated;
        }

        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            ElementDataGrid.SelectedItem = null;
            NeedRefresh = false;
            foreach (DataRow row in DataTableData.Rows)
            {
                row["IsChecked"] = (Boolean)(sender as CheckBox).IsChecked;
            }
            NeedRefresh = true;
            FilterStatusAll = (Boolean)(sender as CheckBox).IsChecked;
            FilterStatusNone = !(Boolean)(sender as CheckBox).IsChecked;
            ButtonApplyClick?.Invoke();
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

            if (!IsCheckedOne && DataTableData.Rows.Count > 0)
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
                //currentRow["IsChecked"] = !convertdata.FlexDataConvertToBoolean(currentRow["IsChecked"].ToString());
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

        private void Close_ButtonCloseClick()
        {
            if (Apply())
            {
                this.Visibility = Visibility.Hidden;
               //this.Activated += ContentActivated;
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
            var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
            //this.Left = this.Left - this.ActualWidth;
            //if (this.Left <= frameWorkAreaX)
            //    this.Left = frameWorkAreaX;
            //this.Activated -= ContentActivated;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
