using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

namespace Sklad_v1_001.Control.ToolBar
{
    /// <summary>
    /// Логика взаимодействия для ToolBarNextToBack.xaml
    /// </summary>
    public partial class ToolBarNextToBack : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                      "LabelText",
                      typeof(String),
                      typeof(ToolBarNextToBack), new UIPropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String LabelText
        {
            get { return (String)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(
                     "CurrentPage",
                     typeof(Int32),
                     typeof(ToolBarNextToBack), new UIPropertyMetadata(1, new PropertyChangedCallback(CurrentPageChanget)));

        //// Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 CurrentPage
        {
            get
            {
                return (Int32)GetValue(CurrentPageProperty);
            }
            set
            {
                SetValue(CurrentPageProperty, value);
                ChangeLabelString();
                OnPropertyChanged("CurrentPage");
            }
        }

        public static readonly DependencyProperty TotalCountProperty = DependencyProperty.Register(
                       "TotalCount",
                       typeof(Int32),
                       typeof(ToolBarNextToBack), new UIPropertyMetadata(0, new PropertyChangedCallback(CurrentPageChanget)));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 TotalCount
        {
            get
            {
                return (Int32)GetValue(TotalCountProperty);
            }
            set
            {
                SetValue(TotalCountProperty, value);
                OnPropertyChanged("TotalCount");
            }
        }

        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register(
                       "PageCount",
                       typeof(Int32),
                       typeof(ToolBarNextToBack), new UIPropertyMetadata(0, new PropertyChangedCallback(CurrentPageChanget)));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 PageCount
        {
            get
            {
                return (Int32)GetValue(PageCountProperty);
            }
            set
            {
                SetValue(PageCountProperty, value);
                OnPropertyChanged("PageCount");
            }
        }

        public static void CurrentPageChanget(DependencyObject depObject, DependencyPropertyChangedEventArgs args)
        {
            ToolBarNextToBack paginator = (ToolBarNextToBack)depObject;
            paginator.ChangeLabelString();
        }

        public static readonly DependencyProperty PrevPageEnableProperty = DependencyProperty.Register(
                       "PrevPageEnable",
                       typeof(Boolean),
                       typeof(ToolBarNextToBack), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean PrevPageEnable
        {
            get
            {
                return (Boolean)GetValue(PrevPageEnableProperty);
            }
            set
            {
                SetValue(PrevPageEnableProperty, value);
                OnPropertyChanged("PrevPageEnable");
            }
        }
        public static readonly DependencyProperty HomePageEnableProperty = DependencyProperty.Register(
                      "HomePageEnable",
                      typeof(Boolean),
                      typeof(ToolBarNextToBack), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean HomePageEnable
        {
            get
            {
                return (Boolean)GetValue(HomePageEnableProperty);
            }
            set
            {
                SetValue(HomePageEnableProperty, value);
                OnPropertyChanged("HomePageEnable");
            }
        }

        public static readonly DependencyProperty NextPageEnableProperty = DependencyProperty.Register(
                       "NextPageEnable",
                       typeof(Boolean),
                       typeof(ToolBarNextToBack), new UIPropertyMetadata(true));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean NextPageEnable
        {
            get
            {
                return (Boolean)GetValue(NextPageEnableProperty);
            }
            set
            {
                SetValue(NextPageEnableProperty, value);
                OnPropertyChanged("NextPageEnable");
            }
        }

        public static readonly DependencyProperty EndPageEnableProperty = DependencyProperty.Register(
                       "EndPageEnable",
                       typeof(Boolean),
                       typeof(ToolBarNextToBack), new UIPropertyMetadata(true));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean EndPageEnable
        {
            get
            {
                return (Boolean)GetValue(EndPageEnableProperty);
            }
            set
            {
                SetValue(EndPageEnableProperty, value);
                OnPropertyChanged("EndPageEnable");
            }
        }



        private void ChangeLabelString()
        {
            PrevPageEnable = CurrentPage != 0;
            HomePageEnable = CurrentPage != 0;
            if (TotalCount != 0)
            {
                if (PageCount != 0)
                {
                    LabelText = Properties.Resources.PAGE + " " + (CurrentPage + 1).ToString("N0", CultureInfo.GetCultureInfo("en-US")) + " " + Properties.Resources.PageIn + " " + Math.Ceiling((double)TotalCount / PageCount).ToString("N0", CultureInfo.GetCultureInfo("en-US"));
                    NextPageEnable = CurrentPage >= (Int32)(Math.Ceiling((double)TotalCount / PageCount) - 1) ? false : true;
                    EndPageEnable = CurrentPage >= (Int32)(Math.Ceiling((double)TotalCount / PageCount) - 1) ? false : true;
                }
            }
            else
            {
                LabelText = Properties.Resources.PAGE + " 0 " + Properties.Resources.PageIn + " 0";
                HomePageEnable = false;
                EndPageEnable = false;
                NextPageEnable = false;
                PrevPageEnable = false;
            }
        }

        public static readonly DependencyProperty TextOnWhatPageProperty = DependencyProperty.Register(
          "TextOnWhatPage",
          typeof(String),
          typeof(ToolBarNextToBack), new UIPropertyMetadata(String.Empty));

        public String TextOnWhatPage
        {
            get { return (String)GetValue(TextOnWhatPageProperty); }
            set { SetValue(TextOnWhatPageProperty, value); }
        }

        public static readonly DependencyProperty IsEnableNextProperty = DependencyProperty.Register(
          "IsEnableNext",
          typeof(Boolean),
          typeof(ToolBarNextToBack), new UIPropertyMetadata(true));

        public Boolean IsEnableNext
        {
            get { return (Boolean)GetValue(IsEnableNextProperty); }
            set { SetValue(IsEnableNextProperty, value); }
        }

        public static readonly DependencyProperty IsEnableNextEndProperty = DependencyProperty.Register(
        "IsEnableNextEnd",
        typeof(Boolean),
        typeof(ToolBarNextToBack), new UIPropertyMetadata(true));

        public Boolean IsEnableNextEnd
        {
            get { return (Boolean)GetValue(IsEnableNextEndProperty); }
            set { SetValue(IsEnableNextEndProperty, value); }
        }

        public static readonly DependencyProperty IsEnableBackProperty = DependencyProperty.Register(
         "IsEnableBack",
         typeof(Boolean),
         typeof(ToolBarNextToBack), new UIPropertyMetadata(false));

        public Boolean IsEnableBack
        {
            get { return (Boolean)GetValue(IsEnableBackProperty); }
            set { SetValue(IsEnableBackProperty, value); }
        }

       public static readonly DependencyProperty IsEnableBackInProperty = DependencyProperty.Register(
       "IsEnableBackIn",
       typeof(Boolean),
       typeof(ToolBarNextToBack), new UIPropertyMetadata(false));

        public Boolean IsEnableBackIn
        {
            get { return (Boolean)GetValue(IsEnableBackInProperty); }
            set { SetValue(IsEnableBackInProperty, value); }
        }

        public ToolBarNextToBack()
        {
            InitializeComponent();
        }

        public event Action ButtonNext;
        public event Action ButtonNextEnd;
        public event Action ButtonBack;
        public event Action ButtonBackIn;
        //кнопка назад
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonBack?.Invoke();
        }

        private void NextButoon_Click(object sender, RoutedEventArgs e)
        {
            ButtonNext?.Invoke();
        }

        private void BackInButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonBackIn?.Invoke();
        }
        
        private void NextEndButoon_Click(object sender, RoutedEventArgs e)
        {
            ButtonNextEnd?.Invoke();
        }
    }
}
