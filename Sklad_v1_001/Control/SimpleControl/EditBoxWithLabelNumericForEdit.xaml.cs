using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sklad_v1_001.Control.SimpleControl
{
    /* public class IntValidationRule : ValidationRule
     {
         private Int32 min = 0;
         private Int32 max = Int32.MaxValue;
         public Int32 Min
         {
             get { return min; }
             set { min = value; }
         }

         public Int32 Max
         {
             get { return max; }
             set { max = value; }
         }

         public override ValidationResult Validate(object value, System.Globalization.CultureInfo ci)
         {
             int price = 0;
             try
             {

                 price = Int32.Parse((string)value);
             }
             catch
             {
                 return new ValidationResult(false, "Недопустимые символы.");
             }

             if ((price < Min) || (price > Max))
             {
                 return new ValidationResult(false,
                   "Ошибка диапазона " + Min + " до " + Max + ".");
             }
             else
             {
                 return new ValidationResult(true, null);
             }
         }
     }    
     */
    
    public class StringToNullableIntConverter : IValueConverter
    {
        ConvertData convertdata;
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            Int32? d = (Int32?)value;
            if (d.HasValue)
                return d.Value.ToString(culture);
            else
                return String.Empty;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            convertdata = new ConvertData();
            string s = (string)value;
            if (String.IsNullOrEmpty(s.Replace(" ", "")))
                return 0;
            else
                return convertdata.FlexDataConvertToInt32(int.Parse(s, culture).ToString());
        }
    }

    /// <summary>
    /// Логика взаимодействия для EditBoxWithLabelNumeric.xaml
    /// </summary>
    public partial class EditBoxWithLabelNumericForEdit : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(EditBoxWithLabelNumericForEdit), new UIPropertyMetadata(0));

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
                       "MinValue",
                       typeof(Int32),
                       typeof(EditBoxWithLabelNumericForEdit), new UIPropertyMetadata(0));

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
                      "MaxValue",
                      typeof(Int32),
                      typeof(EditBoxWithLabelNumericForEdit), new UIPropertyMetadata(Int32.MaxValue));

        public EditBoxWithLabelNumericForEdit()
        {
            InitializeComponent();                   
        }        
        
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 Value
        {
            get { return (Int32)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 MinValue
        {
            get { return (Int32)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 MaxValue
        {
            get { return (Int32)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
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
        
        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ConvertData convertdata = new ConvertData();
            e.Handled = !(Char.IsDigit(e.Text, 0));
            if (!e.Handled)
            {
                String prev = this.TextBox.Text.Remove(this.TextBox.CaretIndex, this.TextBox.Text.Length - this.TextBox.CaretIndex);
                String next = this.TextBox.Text.Remove(0, this.TextBox.CaretIndex);

                String newvalue = String.Concat(prev, e.Text, next);
                //Int32 val = convertdata.FlexDataConvertToInt32(newvalue);
                //if (val < MinValue || val > MaxValue)
                //{
                //    e.Handled = true;
                //}
            }                
        }

    public event Action TextChanged;

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                Boolean res = false;
                String text = (String)e.DataObject.GetData(typeof(String));               
                Int32 val;
                res = !int.TryParse(text, out val);                

                if (res || !res && (val < MinValue || val > MaxValue))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke();
        }

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));  
        }
    }
}
