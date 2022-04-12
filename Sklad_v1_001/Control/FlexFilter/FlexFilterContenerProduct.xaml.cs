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
    /// Логика взаимодействия для FlexFilterContenerProduct.xaml
    /// </summary>
    public partial class FlexFilterContenerProduct : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty Value1Property = DependencyProperty.Register(
                      "Value1",
                       typeof(String),
                       typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty Value2Property = DependencyProperty.Register(
                     "Value2",
                      typeof(String),
                      typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty Value3Property = DependencyProperty.Register(
                     "Value3",
                      typeof(String),
                      typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty Value4Property = DependencyProperty.Register(
                     "Value4",
                      typeof(String),
                      typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty Value5Property = DependencyProperty.Register(
                     "Value5",
                      typeof(String),
                      typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty Value6Property = DependencyProperty.Register(
                     "Value6",
                      typeof(String),
                      typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty Value7Property = DependencyProperty.Register(
                     "Value7",
                      typeof(String),
                      typeof(FlexFilterContenerProduct), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty Quantity_MinProperty = DependencyProperty.Register(
                    "Quantity_Min",
                     typeof(Decimal),
                     typeof(FlexFilterContenerProduct), new UIPropertyMetadata(0));
        
        public static readonly DependencyProperty Quantity_MaxProperty = DependencyProperty.Register(
                    "Quantity_Max",
                     typeof(Decimal),
                     typeof(FlexFilterContenerProduct), new UIPropertyMetadata(0));
        
        public static readonly DependencyProperty TagPrice_MinProperty = DependencyProperty.Register(
                   "TagPrice_Min",
                    typeof(Decimal),
                    typeof(FlexFilterContenerProduct), new UIPropertyMetadata(0));
        
        public static readonly DependencyProperty TagPrice_MaxProperty = DependencyProperty.Register(
                   "TagPrice_Max",
                    typeof(Decimal),
                    typeof(FlexFilterContenerProduct), new UIPropertyMetadata(0));

        public String Value1
        {
            get
            {
                return (String)GetValue(Value1Property);
            }
            set
            {
                SetValue(Value1Property, value);
                OnPropertyChanged("Value1");
            }
        }

        public String Value2
        {
            get
            {
                return (String)GetValue(Value2Property);
            }
            set
            {
                SetValue(Value2Property, value);
                OnPropertyChanged("Value2");
            }
        }

        public String Value3
        {
            get
            {
                return (String)GetValue(Value3Property);
            }
            set
            {
                SetValue(Value3Property, value);
                OnPropertyChanged("Value3");
            }
        }

        public String Value4
        {
            get
            {
                return (String)GetValue(Value4Property);
            }
            set
            {
                SetValue(Value4Property, value);
                OnPropertyChanged("Value4");
            }
        }

        public String Value5
        {
            get
            {
                return (String)GetValue(Value5Property);
            }
            set
            {
                SetValue(Value5Property, value);
                OnPropertyChanged("Value5");
            }
        }

        public String Value6
        {
            get
            {
                return (String)GetValue(Value6Property);
            }
            set
            {
                SetValue(Value6Property, value);
                OnPropertyChanged("Value6");
            }
        }

        public String Value7
        {
            get
            {
                return (String)GetValue(Value7Property);
            }
            set
            {
                SetValue(Value7Property, value);
                OnPropertyChanged("Value7");
            }
        }

        public Decimal Quantity_Min
        {
            get
            {
                return (Decimal)GetValue(Quantity_MinProperty);
            }
            set
            {
                SetValue(Quantity_MinProperty, value);
                OnPropertyChanged("Quantity_Min");
            }
        }
   
        public Decimal Quantity_Max
        {
            get
            {
                return (Decimal)GetValue(Quantity_MaxProperty);
            }
            set
            {
                SetValue(Quantity_MaxProperty, value);
                OnPropertyChanged("Quantity_Max");
            }
        }

        public Decimal TagPrice_Min
        {
            get
            {
                return (Decimal)GetValue(TagPrice_MinProperty);
            }
            set
            {
                SetValue(TagPrice_MinProperty, value);
                OnPropertyChanged("TagPrice_Min");
            }
        }

        public Decimal TagPrice_Max
        {
            get
            {
                return (Decimal)GetValue(TagPrice_MaxProperty);
            }
            set
            {
                SetValue(TagPrice_MaxProperty, value);
                OnPropertyChanged("Value7");
            }
        }

        public FlexFilterContenerProduct()
        {
            InitializeComponent();
        }

        private void Filter1_ButtonApplyClick(string text)
        {
            Value1 = text;
        }

        private void Filter2_ButtonApplyClick(string text)
        {
            Value2 = text;
        }

        private void Filter3_ButtonApplyClick(string text)
        {
            Value3 = text;
        }

        private void Filter4_ButtonApplyClick(string text)
        {
            Value4 = text;
        }

        private void Filter5_ButtonApplyClick(string text)
        {
            Value5 = text;
        }

        private void Filter6_ButtonApplyClick(string text)
        {
            Value6 = text;
        }

        private void Filter7_ButtonApplyClick(string text)
        {
            Value7 = text;
        }

    }
}
