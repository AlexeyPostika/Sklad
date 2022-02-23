using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;
using System;
using System.ComponentModel;
using System.Windows;

namespace Sklad_v1_001.Control.FlexFilter
{
    /// <summary>
    /// Логика взаимодействия для FlexGridCheckBox.xaml
    /// </summary>
    ///
    /// <summary>
    /// Логика взаимодействия для FromToTimeFilter.xaml
    /// </summary>
    public partial class FlexGridFromToWindow : DialogWindow, INotifyPropertyChanged
    {
        private Boolean needrefresh;
        private Boolean needwarning;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty DefaultMinProperty = DependencyProperty.Register(
                        "DefaultMin",
                        typeof(Double),
                        typeof(FlexGridFromToWindow));

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
                        typeof(FlexGridFromToWindow));

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

        public static readonly DependencyProperty FilterStatusProperty = DependencyProperty.Register(
                        "FilterStatus",
                        typeof(Boolean),
                        typeof(FlexGridFromToWindow));

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

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
                       "From",
                       typeof(Double),
                       typeof(FlexGridFromToWindow), new UIPropertyMetadata(0.0));

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

                if (To != DefaultMax || From != DefaultMin)
                    FilterStatus = true;
                else
                    FilterStatus = false;
            }
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
                      "To",
                      typeof(Double),
                      typeof(FlexGridFromToWindow), new UIPropertyMetadata(0.0));

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

                if (To != DefaultMax || From != DefaultMin)
                    FilterStatus = true;
                else
                    FilterStatus = false;
            }
        }

        //string IAbstractButtonFilter.Text
        //{
        //    get
        //    {
        //        return Properties.Resources.FilterTime;
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //string IAbstractButtonFilter.From
        //{
        //    //get
        //    //{
        //    //    return DateFrom.Value.ToString();
        //    //}

        //    //set
        //    //{
        //    //    throw new NotImplementedException();
        //    //}
        //}

        //string IAbstractButtonFilter.To
        //{
        //    //get
        //    //{
        //    //    return DateTo.Value.ToString();
        //    //}

        //    //set
        //    //{
        //    //    throw new NotImplementedException();
        //    //}
        //}

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(FlexGridFromToWindow));

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

        public static readonly DependencyProperty DefaultStyleProperty = DependencyProperty.Register(
                        "DefaultStyle",
                        typeof(Style),
                        typeof(FlexGridFromToWindow));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Style DefaultStyle
        {
            get
            {

                return (Style)GetValue(DefaultStyleProperty);
            }
            set
            {

                SetValue(DefaultStyleProperty, value);
                OnPropertyChanged("DefaultStyle");
            }
        }

        public FlexGridFromToWindow()
        {
            InitializeComponent();
            Activated += ContentActivated;
            //DefaultStyle = DateTo.TextBox.Style;
            needrefresh = true;
        }

        public event Action ButtonApplyClick;

        private void control_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Visibility = Visibility.Hidden;
                Activated += ContentActivated;
                e.Cancel = true;
            }
            catch
            {
            }
        }

        private void ContentActivated(object sender, EventArgs e)
        {
            var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
            var frameWorkAreaActualWidth = MainWindow.AppWindow.frameWorkArea.ActualWidth;
            if (Left - ActualWidth <= frameWorkAreaX)
            {
                Left = frameWorkAreaX;
            }
            else
            {
                Left = Left - ActualWidth;
            }
            if (Left + ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth)
            {
                Left = frameWorkAreaX + frameWorkAreaActualWidth - ActualWidth;
            }
            if (Left + ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth)
            {
                MaxWidth = frameWorkAreaActualWidth - Left + frameWorkAreaX;
            }
            Activated -= ContentActivated;
        }

        private void DateTo_TextChanged()
        {
            if (needrefresh)
            {
                if (To != DefaultMax || From != DefaultMin)
                    FilterStatus = true;
                else
                    FilterStatus = false;
                if (needwarning && From > To)
                {
                   // DateTo.TextBox.Style = (Style)MainWindow.AppWindow.TryFindResource("TextBoxErrorStyle");
                }
                else
                {
                    //DateTo.TextBox.Style = DefaultStyle;
                    //DateFrom.TextBox.Style = DefaultStyle;
                    ButtonApplyClick?.Invoke();
                }
            }
        }

        private void DateFrom_TextChanged()
        {
            if (needrefresh)
            {
                if (To != DefaultMax || From != DefaultMin)
                    FilterStatus = true;
                else
                    FilterStatus = false;
                if (needwarning && From > To)
                {
                    //DateFrom.TextBox.Style = (Style)MainWindow.AppWindow.TryFindResource("TextBoxErrorStyle");
                }
                else
                {
                    //DateTo.TextBox.Style = DefaultStyle;
                    //DateFrom.TextBox.Style = DefaultStyle;
                    ButtonApplyClick?.Invoke();
                }
                needwarning = true;
            }
        }

        private void Close_ButtonClearFiltersClick()
        {
            needrefresh = false;
            From = DefaultMin;
            To = DefaultMax;
            FilterStatus = false;
            //DateTo.TextBox.Style = DefaultStyle;
            //DateFrom.TextBox.Style = DefaultStyle;
            ButtonApplyClick?.Invoke();
            needrefresh = true;
        }

        private void Close_ButtonCloseClick()
        {
            Control.FlexMessageBox.FlexMessageBox mb = new Control.FlexMessageBox.FlexMessageBox();
            if (To < From)
            {
                mb.Show(Properties.Resources.ErrorFilterToSmallerFrom, GenerateTitle(TitleType.Error, Properties.Resources.BadRange), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Visibility = Visibility.Hidden;
                Activated += ContentActivated;
            }
        }
    }
}
