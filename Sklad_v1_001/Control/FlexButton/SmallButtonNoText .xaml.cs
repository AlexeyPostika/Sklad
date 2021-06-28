using Sklad_v1_001.GlobalVariable;
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

namespace Sklad_v1_001.Control.FlexButton
{
    /// <summary>
    /// Логика взаимодействия для LargeButton.xaml
    /// </summary>
    /// 
    public partial class SmallButtonNoText : UserControl, IAbstractButton
    {
        public SmallButtonNoText()
        {
            InitializeComponent();        
        }

        public event Action ButtonClick;
        public string text;

        public String ImageSource
        {
            set
            {
                this.Image.Source = new BitmapImage(new Uri(value, UriKind.Relative));
            }
            get
            {
                return null;
            }         
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick();
            }
        }        
    }
}
