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
    /// <summary>
    /// Логика взаимодействия для FromToTimeFilter.xaml
    /// </summary>
    public partial class FlexGridFromToWindowInt : DialogWindow, IAbstractButtonFilter, INotifyPropertyChanged
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
                        typeof(Int32),
                        typeof(FlexGridFromToWindowInt));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 DefaultMin
        {
            get
            {
                return (Int32)GetValue(DefaultMinProperty);
            }
            set
            {
                SetValue(DefaultMinProperty, value);
                OnPropertyChanged("DefaultMin");
            }
        }

        public static readonly DependencyProperty DefaultMaxProperty = DependencyProperty.Register(
                        "DefaultMax",
                        typeof(Int32),
                        typeof(FlexGridFromToWindowInt));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 DefaultMax
        {
            get
            {
                return (Int32)GetValue(DefaultMaxProperty);
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
                        typeof(FlexGridFromToWindowInt));

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
                       typeof(Int32),
                       typeof(FlexGridFromToWindowInt), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 From
        {
            get
            {
                return (Int32)GetValue(FromProperty);
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
                      typeof(Int32),
                      typeof(FlexGridFromToWindowInt), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 To
        {
            get
            {
                return (Int32)GetValue(ToProperty);
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

        string IAbstractButtonFilter.Text
        {
            get
            {
                return Properties.Resources.FilterTime;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IAbstractButtonFilter.From
        {
            get
            {
                return DateFrom.Value.ToString();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IAbstractButtonFilter.To
        {
            get
            {
                return DateTo.Value.ToString();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(FlexGridFromToWindowInt));

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
                        typeof(FlexGridFromToWindowInt));

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

        public FlexGridFromToWindowInt()
        {
            InitializeComponent();
            this.Activated += ContentActivated;
            DefaultStyle = this.DateTo.TextBox.Style;
            needrefresh = true;
        }

        public event Action ButtonApplyClick;

        private void ContentActivated(object sender, EventArgs e)
        {
            var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
            this.Left = Left - this.ActualWidth;
            if (this.Left <= frameWorkAreaX)
                this.Left = frameWorkAreaX;
            this.Activated -= ContentActivated;
        }

        private void control_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                this.Visibility = Visibility.Hidden;
                e.Cancel = true;
            }
            catch
            {
            }
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
                    this.DateTo.TextBox.Style = (Style)MainWindow.AppWindow.TryFindResource("TextBoxErrorStyle");
                }
                else
                {
                    this.DateTo.TextBox.Style = DefaultStyle;
                    this.DateFrom.TextBox.Style = DefaultStyle;
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
                    this.DateTo.TextBox.Style = (Style)MainWindow.AppWindow.TryFindResource("TextBoxErrorStyle");
                }
                else
                {
                    this.DateTo.TextBox.Style = DefaultStyle;
                    this.DateFrom.TextBox.Style = DefaultStyle;
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
            this.DateTo.TextBox.Style = DefaultStyle;
            this.DateFrom.TextBox.Style = DefaultStyle;
            ButtonApplyClick?.Invoke();
            needrefresh = true;
        }

        private void Close_ButtonCloseClick()
        {
            FlexMessageBox.FlexMessageBox mb = new FlexMessageBox.FlexMessageBox();
            if (To < From)
            {
                mb.Show(Properties.Resources.ErrorFilterToSmallerFrom, GenerateTitle(TitleType.Error, Properties.Resources.BadRange), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Visibility = Visibility.Hidden;
                this.Activated += ContentActivated;
            }
        }
    }
}
