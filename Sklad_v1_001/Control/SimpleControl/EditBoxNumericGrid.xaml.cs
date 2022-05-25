using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
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

namespace Sklad_v1_001.Control.SimpleControl
{
    /// <summary>
    /// Логика взаимодействия для EditBoxNumericGrid.xaml
    /// </summary>
    public partial class EditBoxNumericGrid : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(EditBoxNumericGrid), new UIPropertyMetadata(0));

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(
                       "MinValue",
                       typeof(Int32),
                       typeof(EditBoxNumericGrid), new UIPropertyMetadata(0));

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
                      "MaxValue",
                      typeof(Int32),
                      typeof(EditBoxNumericGrid), new UIPropertyMetadata(Int32.MaxValue));

        public EditBoxNumericGrid()
        {
            InitializeComponent();
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 Value
        {
            get { return (Int32)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
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

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ConvertData convertdata = new ConvertData();
            if (!String.IsNullOrEmpty(e.Text))
                e.Handled = !(Char.IsDigit(e.Text, 0));
            /*if (!e.Handled)
            {
                String prev = TextBox.Text.Remove(TextBox.CaretIndex, TextBox.Text.Length - TextBox.CaretIndex);
                String next = TextBox.Text.Remove(0, TextBox.CaretIndex);

                String newvalue = String.Concat(prev, e.Text, next);
                Int64 val = convertdata.FlexDataConvertToInt64(newvalue);
                if (val < MinValue || val > MaxValue)
                {
                    e.Handled = true;
                }
            }*/
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

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Text))
                e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
