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

namespace Sklad_v1_001.Control.FlexRadio
{
    /// <summary>
    /// Логика взаимодействия для FlexRadioButton.xaml
    /// </summary>
    public partial class FlexRadioButton : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty IsRadioProperty = DependencyProperty.Register(
                        "IsRadio",
                        typeof(Boolean),
                        typeof(FlexRadioButton), new UIPropertyMetadata(true));

        public static readonly DependencyProperty IsRadioTypeProperty = DependencyProperty.Register(
                        "IsRadioType",
                        typeof(Int32),
                        typeof(FlexRadioButton), new UIPropertyMetadata(2));

        public Boolean IsRadio
        {
            get { return (Boolean)GetValue(IsRadioProperty); }
            set
            {

                SetValue(IsRadioProperty, value);                
            }
        }
        public Int32 IsRadioType
        {
            get { return (Int32)GetValue(IsRadioTypeProperty); }
            set
            {             
                SetValue(IsRadioTypeProperty, value);
                Status(value);
            }
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
        public string YESLabelText
        {
            get
            {
                return this.YESlabel.Content.ToString();
            }

            set
            {
                this.YESlabel.Content = value;
            }
        }

        public string NOLabelText
        {
            get
            {
                return this.NOlabel.Content.ToString();
            }

            set
            {
                this.NOlabel.Content = value;
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
        public FlexRadioButton()
        {
            InitializeComponent();
            NO.IsChecked = IsRadio;
        }

        private void YES_Checked(object sender, RoutedEventArgs e)
        {
            NO.IsChecked = false;
            IsRadioType = 1;

        }

        private void NO_Checked(object sender, RoutedEventArgs e)
        {
            YES.IsChecked = false;
            IsRadioType = 2;
        }

        private void Status(Int32 _isRadioType)
        {
            switch (IsRadioType)
            {
                case 1:
                    IsRadio = false;
                    break;
                case 2:
                    IsRadio = true;
                    break;
            }
        }
    }
}
