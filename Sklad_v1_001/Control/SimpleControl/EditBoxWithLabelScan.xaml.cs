using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// <summary>
    /// Логика взаимодействия для EditBoxWithLabelScan.xaml
    /// </summary>
    public partial class EditBoxWithLabelScan : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(EditBoxWithLabelScan), new UIPropertyMetadata(String.Empty));
        // свойство зависимостей
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(EditBoxWithLabelScan), new UIPropertyMetadata(Properties.Resources.Scan));

        // свойство зависимостей
        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(
                        "LabelWidth",
                        typeof(Int32),
                        typeof(EditBoxWithLabelScan), new UIPropertyMetadata(120));

        // свойство зависимостей
        public static readonly DependencyProperty IsEnabledTextBoxProperty = DependencyProperty.Register(
                        "IsEnabledTextBox",
                        typeof(Boolean),
                        typeof(EditBoxWithLabelScan), new UIPropertyMetadata(true));

        // свойство зависимостей
        public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register(
                        "AcceptReturn",
                        typeof(Boolean),
                        typeof(EditBoxWithLabelScan), new UIPropertyMetadata(false));
        //MaxLengthInt
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
                       "MaxLength",
                       typeof(Int32),
                       typeof(EditBoxWithLabelScan), new UIPropertyMetadata(13));

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxWithLabelScan), new UIPropertyMetadata(Visibility.Collapsed));
        //VisibilitySpinner
        public static readonly DependencyProperty VisibilitySpinnerProperty = DependencyProperty.Register(
                       "VisibilitySpinner",
                       typeof(Visibility),
                       typeof(EditBoxWithLabelScan), new UIPropertyMetadata(Visibility.Collapsed));

        public Visibility VisibilitySpinner
        {
            get
            {
                return (Visibility)GetValue(VisibilitySpinnerProperty);
            }
            set
            {
                SetValue(VisibilitySpinnerProperty, value);
                OnPropertyChanged("VisibilitySpinner");
            }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsRequired
        {
            get
            {
                return (Visibility)GetValue(IsRequiredProperty);
            }
            set
            {
                SetValue(IsRequiredProperty, value);
                OnPropertyChanged("IsRequired");
            }
        }

        String cardNumber;
        String cardDate;
        String rawString;
        Boolean isUcsScanCard;

        [DllImport("user32.dll")]
        public static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);

        public EditBoxWithLabelScan()
        {
            InitializeComponent();
            RawString = "";
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 MaxLength
        {
            get { return (Int32)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 LabelWidth
        {
            get { return (Int32)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsEnabledTextBox
        {
            get { return (Boolean)GetValue(IsEnabledTextBoxProperty); }
            set { SetValue(IsEnabledTextBoxProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean AcceptsReturn
        {
            get { return (Boolean)GetValue(AcceptsReturnProperty); }
            set
            {
                SetValue(AcceptsReturnProperty, value);
            }
        }

        public event Action ButtonClick;

        public Boolean IsReadOnly
        {
            get
            {
                return ScanTextBox.IsReadOnly;
            }

            set
            {
                ScanTextBox.IsReadOnly = value;
            }
        }

        public string CardNumber
        {
            get
            {
                return cardNumber;
            }

            set
            {
                cardNumber = value;
            }
        }

        public string CardDate
        {
            get
            {
                return cardDate;
            }

            set
            {
                cardDate = value;
            }
        }

        public string RawString
        {
            get
            {
                return rawString;
            }

            set
            {
                rawString = value;
            }
        }

        public Boolean IsUcsScanCard
        {
            get
            {
                return isUcsScanCard;
            }

            set
            {
                isUcsScanCard = value;
            }
        }

        private void ScanTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            RawString = RawString + e.Text;
            if (!String.IsNullOrEmpty(e.Text))
                e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void ScanTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var a = e.Key;
            if (e.Key == Key.Enter)
            {
                Regex regexValue;
                regexValue = new Regex(@".*([\d]{19})=([\d]{4})([\d]{13}).*");
                IsUcsScanCard = false;
                foreach (Match match in regexValue.Matches(RawString))
                {
                    if (match.Groups[1].Length == 19 && (match.Groups[2].Length == 4 || match.Groups[2].Length == 17))
                    {
                        CardNumber = match.Groups[1].ToString();
                        CardDate = match.Groups[2].ToString().Substring(0, 4);
                        IsUcsScanCard = true;
                    }
                }
                RawString = "";
                ButtonClick?.Invoke();
            }
        }
    }
}
