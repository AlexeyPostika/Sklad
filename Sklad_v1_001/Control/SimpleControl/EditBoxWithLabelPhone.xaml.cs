using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// <summary>
    /// Логика взаимодействия для EditBoxWithLabelPhone.xaml
    /// </summary>
    public partial class EditBoxWithLabelPhone : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        String textCodeCountryValue;
        String textCodeOperatorValue;
        String textPhoneValue1;
        String textPhoneValue2;
        String textPhoneValue3;

        public String TextCodeCountryValue
        {
            get
            {
                return textCodeCountryValue;
            }

            set
            {
                textCodeCountryValue = value;
                OnPropertyChanged("TextCodeCountryValue");
                if (String.IsNullOrEmpty(value))
                    TextCodeCountry.BorderBrush = Brushes.Black;
                else
                    TextCodeCountry.BorderBrush = Brushes.Transparent;

                //CollectString();
            }
        }

        public String TextCodeOperatorValue
        {
            get
            {
                return textCodeOperatorValue;
            }

            set
            {
                textCodeOperatorValue = value;
                OnPropertyChanged("TextCodeOperatorValue");
                if (String.IsNullOrEmpty(value))
                    TextCodeOperator.BorderBrush = Brushes.Black;
                else
                    TextCodeOperator.BorderBrush = Brushes.Transparent;

                //CollectString();
            }
        }

        public String TextPhoneValue1
        {
            get
            {
                return textPhoneValue1;
            }

            set
            {
                textPhoneValue1 = value;
                OnPropertyChanged("TextPhoneValue1");
                if (String.IsNullOrEmpty(value))
                    TextPhone1.BorderBrush = Brushes.Black;
                else
                    TextPhone1.BorderBrush = Brushes.Transparent;

                //CollectString();
            }
        }

        public String TextPhoneValue2
        {
            get
            {
                return textPhoneValue2;
            }

            set
            {
                textPhoneValue2 = value;
                OnPropertyChanged("TextPhoneValue2");
                if (String.IsNullOrEmpty(value))
                    TextPhone2.BorderBrush = Brushes.Black;
                else
                    TextPhone2.BorderBrush = Brushes.Transparent;

                //CollectString();
            }
        }

        public String TextPhoneValue3
        {
            get
            {
                return textPhoneValue3;
            }

            set
            {
                textPhoneValue3 = value;
                OnPropertyChanged("TextPhoneValue3");
                if (String.IsNullOrEmpty(value))
                    TextPhone3.BorderBrush = Brushes.Black;
                else
                    TextPhone3.BorderBrush = Brushes.Transparent;

                //CollectString();
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(EditBoxWithLabelPhone), new UIPropertyMetadata(String.Empty, new PropertyChangedCallback(TextChanget)));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged("Text");
            }
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
                return label.Width;
            }

            set
            {
                label.Width = value;
            }
        }

        public EditBoxWithLabelPhone()
        {
            InitializeComponent();
            TextCodeCountryValue = TextCodeOperatorValue = TextPhoneValue1 = TextPhoneValue2 = TextPhoneValue3 = "";
        }

        public static void TextChanget(DependencyObject depObject, DependencyPropertyChangedEventArgs args)
        {
            EditBoxWithLabelPhone editBoxWithLabelPhone = (EditBoxWithLabelPhone)depObject;
            editBoxWithLabelPhone.CollectString();
        }

        void CollectString()
        {
            Regex regexValue = new Regex(@"[+]([\d].*)\([^\d]*(\d+)[^\d]*\)+[(. ]?([\d]*)[-.]+[-. ]?([\d]*)[-.]+[-. ]?([\d]*)");
            MatchCollection matchCollection = regexValue.Matches(Text);
            if (matchCollection.Count == 1)
            {
                foreach (Match match in matchCollection)
                {
                    TextCodeCountryValue = match.Groups[1].ToString();
                    TextCodeOperatorValue = match.Groups[2].ToString();
                    TextPhoneValue1 = match.Groups[3].ToString();
                    TextPhoneValue2 = match.Groups[4].ToString();
                    TextPhoneValue3 = match.Groups[5].ToString();
                }
            }
        }

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Text))
                e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
