using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace Sklad_v1_001.Control.SimpleControl
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
            string s = value.ToString();
            if (String.IsNullOrEmpty(s.Replace(" ", "")))
            {
                return 0;
            }
            else
            {
                Double output;
                try
                {
                    if (s.Length > decimalPlace + 1 && s.IndexOf(".") < 0)
                    {
                        s = s.Insert(s.Length - decimalPlace, ".");
                    }
                    s = s.Replace(" ", "").Replace(",", "");
                    s = Regex.Replace(s, @"\p{Z}", "");
                    int i = s.IndexOf(".");
                    if (i > 0)
                    {
                        if (s.Length > i + 3)
                            s = s.Substring(0, i + 3);
                    }

                    output = (Double)double.Parse(s, culture);
                }
                catch
                {
                    output = 0.0;
                }
                return output;
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для EditBoxWithLabelNumeric.xaml
    /// </summary>
    public partial class EditBoxWithLabelFloatForEdit : UserControl, INotifyPropertyChanged
    {
        Int32 decimalPlace = 2;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata(Visibility.Collapsed));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsRequired
        {
            get { return (Visibility)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }          
        }

        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Double),
                        typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata());

        // свойство зависимостей
        public static readonly DependencyProperty LabelTextStringProperty = DependencyProperty.Register(
                        "LabelTextString",
                        typeof(String),
                        typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata());

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
                      "MinValue",
                      typeof(Double),
                      typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
                      "MaxValue",
                      typeof(Double),
                      typeof(EditBoxWithLabelFloatForEdit), new UIPropertyMetadata(SqlMoney.MaxValue.ToDouble() - 1));

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
                return label.Content.ToString();
            }

            set
            {
                label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return wrapPanel.Width;
            }

            set
            {
                wrapPanel.Width = value;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return TextBox.IsReadOnly;
            }

            set
            {
                TextBox.IsReadOnly = value;
            }
        }

        public event Action TextChanged;

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox current = sender as TextBox;

            if (e.Text == "." || e.Text == ",")
            {
                current.CaretIndex = current.Text.Length - decimalPlace;
            }

            if (!String.IsNullOrEmpty(e.Text))
                e.Handled = !((Char.IsDigit(e.Text, 0) || ((e.Text == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString()) && (DS_Count(((TextBox)sender).Text) < 1))));
            if (e.Handled)
                current.Text = current.Text.Replace(e.Text, "");
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                Boolean res = false;
                String text = (String)e.DataObject.GetData(typeof(String));
                string dec_sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                text = text.Replace(",", dec_sep).Replace(".", dec_sep);
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
                if (MinValue > MaxValue)
                {
                    MinValue = 0;
                    MaxValue = 0;
                    Value = 0;
                    e.Handled = true;
                }

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
            }
            TextChanged?.Invoke();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox current = sender as TextBox;

            if (current.Text.Length > 3 && current.Text.Contains("."))
            {
                if (current.CaretIndex > 1 && e.Key == Key.Back && current.Text.ToCharArray()[current.CaretIndex - 1] == '.')
                {
                    current.CaretIndex = current.Text.Length - decimalPlace - 1;
                    e.Handled = true;
                }

                if (current.CaretIndex < current.Text.Length && e.Key == Key.Delete && current.Text.ToCharArray()[current.CaretIndex] == '.')
                {
                    current.CaretIndex = current.CaretIndex + 1;
                    e.Handled = true;
                }
            }
        }
    }
}
