using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    
    /// <summary>
    /// Interaction logic for FlexGridFromTo.xaml
    /// </summary>
    public partial class FlexGridFromTo : UserControl, INotifyPropertyChanged
    {
        FlexGridFromToWindow flexGridFromToWindow;
        FlexGridFromToWindowInt flexGridFromToWindowInt;
        FlexGridFromToWindowTime flexGridFromToWindowTime;
        private Boolean needrefresh;


        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                       "ImageSource",
                       typeof(BitmapImage),
                       typeof(FlexGridFromTo));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public BitmapImage ImageSource
        {
            get
            {
                return (BitmapImage)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
                OnPropertyChanged("ImageSource");
            }
        }


        public static readonly DependencyProperty DefaultMinProperty = DependencyProperty.Register(
                        "DefaultMin",
                        typeof(Double),
                        typeof(FlexGridFromTo));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Double DefaultMin
        {
            get
            {
                return (Double)GetValue(DefaultMinProperty);
            }
            set
            {

                SetValue(DefaultMinProperty, value);
                OnPropertyChanged("DefaultMin");
            }
        }


        public static readonly DependencyProperty DefaultMaxProperty = DependencyProperty.Register(
                        "DefaultMax",
                        typeof(Double),
                        typeof(FlexGridFromTo));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Double DefaultMax
        {
            get
            {
                return (Double)GetValue(DefaultMaxProperty);
            }
            set
            {

                SetValue(DefaultMaxProperty, value);
                OnPropertyChanged("DefaultMax");
            }
        }

        public static readonly DependencyProperty DefaultMinTimeProperty = DependencyProperty.Register(
                        "DefaultMinTime",
                        typeof(String),
                        typeof(FlexGridFromTo));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String DefaultMinTime
        {
            get
            {
                return (String)GetValue(DefaultMinTimeProperty);
            }
            set
            {

                SetValue(DefaultMinTimeProperty, value);
                OnPropertyChanged("DefaultMinTime");
            }
        }


        public static readonly DependencyProperty DefaultMaxTimeProperty = DependencyProperty.Register(
                        "DefaultMaxTime",
                        typeof(String),
                        typeof(FlexGridFromTo));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String DefaultMaxTime
        {
            get
            {
                return (String)GetValue(DefaultMaxTimeProperty);
            }
            set
            {

                SetValue(DefaultMaxTimeProperty, value);
                OnPropertyChanged("DefaultMaxTime");
            }
        }

        public static readonly DependencyProperty FilterStatusProperty = DependencyProperty.Register(
                           "FilterStatus",
                           typeof(Boolean),
                           typeof(FlexGridFromTo), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей 
        public Boolean FilterStatus
        {
            get
            {
                return (Boolean)GetValue(FilterStatusProperty);
            }
            set
            {

                SetValue(FilterStatusProperty, value);
                OnPropertyChanged("FilterStatus");
            }
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(FlexGridFromTo), new UIPropertyMetadata(""));

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


        public static readonly DependencyProperty TableWidthProperty = DependencyProperty.Register(
                        "TableWidth",
                        typeof(Int32),
                        typeof(FlexGridFromTo), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 TableWidth
        {
            get
            {
                return (Int32)GetValue(TableWidthProperty);
            }
            set
            {

                SetValue(TableWidthProperty, value);
                OnPropertyChanged("TableWidth");
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
                /*DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
                DataRow currentRow = null;
                if (currentRowView != null)
                {
                    currentRow = currentRowView.Row as DataRow;
                    return currentRow["ID"].ToString();
                }*/
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
                /*DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
                DataRow currentRow = null;
                if (currentRowView != null)
                {
                    currentRow = currentRowView.Row as DataRow;
                    return currentRow["Description"].ToString();
                }*/
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
                /*DataRowView currentRowView = this.ElementDataGrid.SelectedItem as DataRowView;
                DataRow currentRow = null;
                if (currentRowView != null)
                {
                    currentRow = currentRowView.Row as DataRow;
                    return Boolean.Parse(currentRow["IsChecked"].ToString());
                }*/
                return false;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
                        "From",
                        typeof(Double),
                        typeof(FlexGridFromTo), new UIPropertyMetadata(0.0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Double From
        {
            get
            {
                return (Double)GetValue(FromProperty);
            }
            set
            {

                SetValue(FromProperty, value);
                OnPropertyChanged("From");
            }
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
                       "To",
                       typeof(Double),
                       typeof(FlexGridFromTo), new UIPropertyMetadata(0.0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Double To
        {
            get
            {
                return (Double)GetValue(ToProperty);
            }
            set
            {

                SetValue(ToProperty, value);
                OnPropertyChanged("To");
            }
        }

        public static readonly DependencyProperty FromTimeProperty = DependencyProperty.Register(
                        "FromTime",
                        typeof(String),
                        typeof(FlexGridFromTo), new UIPropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String FromTime
        {
            get
            {
                return (String)GetValue(FromTimeProperty);
            }
            set
            {

                SetValue(FromTimeProperty, value);
                OnPropertyChanged("FromTime");
            }
        }

        public static readonly DependencyProperty ToTimeProperty = DependencyProperty.Register(
                       "ToTime",
                       typeof(String),
                       typeof(FlexGridFromTo), new UIPropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String ToTime
        {
            get
            {
                return (String)GetValue(ToTimeProperty);
            }
            set
            {

                SetValue(ToTimeProperty, value);
                OnPropertyChanged("ToTime");
            }
        }

        public static readonly DependencyProperty IsTypeIntProperty = DependencyProperty.Register(
                       "IsTypeInt",
                       typeof(Boolean),
                       typeof(FlexGridFromTo), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsTypeInt
        {
            get
            {
                return (Boolean)GetValue(IsTypeIntProperty);
            }
            set
            {

                SetValue(IsTypeIntProperty, value);
                OnPropertyChanged("IsTypeInt");
            }
        }

        public static readonly DependencyProperty IsTypeTimeProperty = DependencyProperty.Register(
                       "IsTypeTime",
                       typeof(Boolean),
                       typeof(FlexGridFromTo), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsTypeTime
        {
            get
            {
                return (Boolean)GetValue(IsTypeTimeProperty);
            }
            set
            {

                SetValue(IsTypeTimeProperty, value);
                OnPropertyChanged("IsTypeTime");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action ButtonApplyClick;
        public FlexGridFromTo()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void ButtonFilter_ButtonClick()
        {
            needrefresh = false;

            var location = this.PointToScreen(new Point(0, 0));

            if (flexGridFromToWindow == null)
            {
                flexGridFromToWindow = new FlexGridFromToWindow();
                flexGridFromToWindow.ButtonApplyClick += new Action(ButtonApplyClickWindow);
            }

            if (flexGridFromToWindowInt == null)
            {
                flexGridFromToWindowInt = new FlexGridFromToWindowInt();
                flexGridFromToWindowInt.ButtonApplyClick += new Action(ButtonApplyClickWindowInt);
            }

            if (flexGridFromToWindowTime == null)
            {
                flexGridFromToWindowTime = new FlexGridFromToWindowTime();
                flexGridFromToWindowTime.ButtonApplyClick += new Action(ButtonApplyClickWindowTime);
            }

            if (IsTypeInt)
            {
                //flexGridFromToWindowInt.WindowStartupLocation = WindowStartupLocation.Manual;
                //flexGridFromToWindowInt.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;
                //flexGridFromToWindowInt.Top = location.Y + this.ActualHeight;
                flexGridFromToWindowInt.AllowDrop = false;
                flexGridFromToWindowInt.LabelText = Properties.Resources.FilterTitle + " " + this.LabelText;
                flexGridFromToWindowInt.DefaultMax = (Int32)DefaultMax;
                flexGridFromToWindowInt.DefaultMin = (Int32)DefaultMin;
                flexGridFromToWindowInt.From = (Int32)From;
                flexGridFromToWindowInt.To = (Int32)To;

                needrefresh = true;
                //flexGridFromToWindowInt.ShowDialog();
            }
            else if (IsTypeTime)
            {
                //flexGridFromToWindowTime.WindowStartupLocation = WindowStartupLocation.Manual;
                //flexGridFromToWindowTime.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;
                //flexGridFromToWindowTime.Top = location.Y + this.ActualHeight;
                flexGridFromToWindowTime.AllowDrop = false;
                flexGridFromToWindowTime.LabelText = Properties.Resources.FilterTitle + " " + this.LabelText;
                flexGridFromToWindowTime.DefaultMax = DefaultMaxTime;
                flexGridFromToWindowTime.DefaultMin = DefaultMinTime;
                flexGridFromToWindowTime.From = FromTime;
                flexGridFromToWindowTime.To = ToTime;

                needrefresh = true;
                //flexGridFromToWindowTime.ShowDialog();
            }
            else
            {
                //flexGridFromToWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                //flexGridFromToWindow.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;
                //flexGridFromToWindow.Top = location.Y + this.ActualHeight;
                flexGridFromToWindow.AllowDrop = false;
                flexGridFromToWindow.LabelText = Properties.Resources.FilterTitle + " " + this.LabelText;
                flexGridFromToWindow.DefaultMax = DefaultMax;
                flexGridFromToWindow.DefaultMin = DefaultMin;
                flexGridFromToWindow.From = From;
                flexGridFromToWindow.To = To;

                needrefresh = true;
               // flexGridFromToWindow.ShowDialog();
            }

        }
        private void ButtonApplyClickWindow()
        {
            if (needrefresh)
            {
                From = (double)flexGridFromToWindow.From;
                To = (double)flexGridFromToWindow.To;
                //if (flexGridFromToWindow.FilterStatus)
                //    ImageSource = ImageHelper.GenerateImage("IconClearFilter.png");
                //else
                //    ImageSource = ImageHelper.GenerateImage("IconFilter.png");
                this.FilterStatus = flexGridFromToWindow.FilterStatus;
                ButtonApplyClick?.Invoke();
            }
        }

        private void ButtonApplyClickWindowInt()
        {
            if (needrefresh)
            {
                From = (Int32)flexGridFromToWindowInt.From;
                To = (Int32)flexGridFromToWindowInt.To;
                //if (flexGridFromToWindowInt.FilterStatus)
                //    ImageSource = ImageHelper.GenerateImage("IconClearFilter.png");
                //else
                //    ImageSource = ImageHelper.GenerateImage("IconFilter.png");
                this.FilterStatus = flexGridFromToWindowInt.FilterStatus;
                ButtonApplyClick?.Invoke();
            }
        }

        private void ButtonApplyClickWindowTime()
        {
            if (needrefresh)
            {
                FromTime = (String)flexGridFromToWindowTime.From;
                ToTime = (String)flexGridFromToWindowTime.To;
                //if (flexGridFromToWindowTime.FilterStatus)
                //    ImageSource = ImageHelper.GenerateImage("IconClearFilter.png");
                //else
                //    ImageSource = ImageHelper.GenerateImage("IconFilter.png");
                this.FilterStatus = flexGridFromToWindowTime.FilterStatus;
                ButtonApplyClick?.Invoke();
            }
        }
    }
}
