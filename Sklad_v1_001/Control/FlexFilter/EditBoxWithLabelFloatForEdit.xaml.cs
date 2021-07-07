using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace POS.FlexControls.SimpleControl
{
    public class StringToNullableDoubleConverter : IValueConverter
    {
        Int32 decimalPlace = 2;

        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            Double? d = (Double?)value;
            if (d.HasValue)
                return d.Value.ToString("N" + decimalPlace.ToString(), culture);
            else
                return String.Empty;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string s = (string)value;
            if (String.IsNullOrEmpty(s.Replace(" ", "")))
            {
                return 0;
            }
            else
            {
                if (s.Length > decimalPlace + 1 && s.IndexOf(".") < 0)
                {
                    s = s.Insert(s.Length - decimalPlace, ".");
                }
                s = s.Replace(" ", "").Replace(",", "");
                return (Double)double.Parse(s, culture);
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для EditBoxWithLabelNumeric.xaml
    /// </summary>
    public partial class EditBoxWithLabelFloatForEdit : UserControl
    {
        Int32 decimalPlace = 2;

        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Double),
                        typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata());

        // свойство зависимостей
        public static readonly DependencyProperty LabelTextStringProperty = DependencyProperty.Register(
                        "LabelTextString",
                        typeof(String),
                        typeof(EditBoxWithLabelScan), new UIPropertyMetadata());

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
                      "MinValue",
                      typeof(Double),
                      typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata(Double.MinValue));

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
                      "MaxValue",
                      typeof(Double),
                      typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata(Double.MaxValue));

        public EditBoxWithLabelFloatForEdit()
        {
            InitializeComponent();                   
        }        
        
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Double MinValue
        {
            get { return (Double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Double MaxValue
        {
            get { return (Double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string LabelTextString
        {
            get { return (string)GetValue(LabelTextStringProperty); }
            set { SetValue(LabelTextStringProperty, value); }
        }

        public string LabelText
        {
            get
            {
                return this.label.Content.ToString();
            }

            set
            {
                this.label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return this.label.Width;
            }

            set
            {
                this.label.Width = value;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return this.TextBox.IsReadOnly;
            }

            set
            {
                this.TextBox.IsReadOnly = value;                
            }
        }

        public event Action TextChanged;

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "." || e.Text == ",")
            {
                TextBox curent = sender as TextBox;
                curent.CaretIndex = curent.Text.Length - decimalPlace;
            }

            e.Handled = !((Char.IsDigit(e.Text, 0) || ((e.Text == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString()) && (DS_Count(((TextBox)sender).Text) < 1))));

            /*if (!e.Handled)
            {
                String prev = this.TextBox.Text.Remove(this.TextBox.CaretIndex, this.TextBox.Text.Length - this.TextBox.CaretIndex);
                String next = this.TextBox.Text.Remove(0, this.TextBox.CaretIndex);

                String newvalue = String.Concat(prev, e.Text, next).Replace(',', ' ').Replace('.', ',');
                Double val = Double.Parse(newvalue);
                if (val < MinValue || val > MaxValue)
                {
                    e.Handled = true;
                }
            }*/
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                Boolean res = false;
                String text = (String)e.DataObject.GetData(typeof(String));               
                float val;
                res = !float.TryParse(text, out val);

                if (res)
                {
                    e.CancelCommand();
                }
                else
                {
                    if (val < MinValue || val > MaxValue)
                    {
                        e.CancelCommand();
                    }
                }                
            }
            else
            {
                e.CancelCommand();
            }
        }

        public Int32 DS_Count(string s)
        {
            string substr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString();
            Int32 count = (s.Length - s.Replace(substr, "").Length) / substr.Length;
            return count;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox current = sender as TextBox;
            if (current != null)
            {
                if (Value < MinValue)
                {
                    Value = MinValue;
                    e.Handled = true;
                }

                if (Value > MaxValue)
                {
                    Value = MaxValue;
                    e.Handled = true;
                }
                TextChanged?.Invoke();
            }
        }        
    }
}
