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
    public partial class FlexGridFromToWindowTime : IAbstractButtonFilter, INotifyPropertyChanged
    {
        private Boolean needrefresh;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty DefaultMinProperty = DependencyProperty.Register(
                        "DefaultMin",
                        typeof(String),
                        typeof(FlexGridFromToWindowTime));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String DefaultMin
        {
            get
            {
                return (String)GetValue(DefaultMinProperty);
            }
            set
            {
                SetValue(DefaultMinProperty, value);
                OnPropertyChanged("DefaultMin");
            }
        }

        public static readonly DependencyProperty DefaultMaxProperty = DependencyProperty.Register(
                        "DefaultMax",
                        typeof(String),
                        typeof(FlexGridFromToWindowTime));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String DefaultMax
        {
            get
            {
                return (String)GetValue(DefaultMaxProperty);
            }
            set
            {
                SetValue(DefaultMaxProperty, value);
                OnPropertyChanged("DefaultMax");
            }
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(FlexGridFromToWindowTime));

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

        public static readonly DependencyProperty FilterStatusProperty = DependencyProperty.Register(
                        "FilterStatus",
                        typeof(Boolean),
                        typeof(FlexGridFromToWindowTime));

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
                       typeof(String),
                       typeof(FlexGridFromToWindowTime), new UIPropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String From
        {
            get
            {
                return (String)GetValue(FromProperty);
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
                      typeof(String),
                      typeof(FlexGridFromToWindowTime), new UIPropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String To
        {
            get
            {
                return (String)GetValue(ToProperty);
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
                return "Временной фильтр";
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

        public FlexGridFromToWindowTime()
        {
            InitializeComponent();
            //this.Activated += ContentActivated;
            needrefresh = true;
        }

        public event Action ButtonApplyClick;

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

        private void Close_ButtonClearFiltersClick()
        {
            needrefresh = false;
            From = DefaultMin;
            To = DefaultMax;
            DateFrom.Value = DefaultMin;
            DateTo.Value = DefaultMax;
            FilterStatus = false;
            ButtonApplyClick?.Invoke();
            needrefresh = true;
        }

        private void Close_ButtonCloseClick()
        {
            this.Visibility = Visibility.Hidden;
            //this.Activated += ContentActivated;
        }

        private void ContentActivated(object sender, EventArgs e)
        {
            //needrefresh = false;
            //DateFrom.Value = From;
            //DateTo.Value = To;
            //var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
            //this.Left = Left - this.ActualWidth;
            //if (this.Left <= frameWorkAreaX)
            //    this.Left = frameWorkAreaX;
            //this.Activated -= ContentActivated;
            //needrefresh = true;
        }

        private void Date_TextChanged()
        {
            if (needrefresh)
            {
                if (To != DefaultMax || From != DefaultMin)
                    FilterStatus = true;
                else
                    FilterStatus = false;
                ButtonApplyClick?.Invoke();
            }
        }
    }
}
