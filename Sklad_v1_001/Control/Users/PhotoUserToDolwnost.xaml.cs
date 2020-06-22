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

namespace Sklad_v1_001.Control.Users
{
    /// <summary>
    /// Interaction logic for PhotoUserToDolwnost.xaml
    /// </summary>
    public partial class PhotoUserToDolwnost : UserControl
    {
        public static readonly DependencyProperty DolwnostPageProperty = DependencyProperty.Register(
        "Dolwnost",
        typeof(String),
        typeof(PhotoUserToDolwnost), new UIPropertyMetadata(Properties.Resources.Dolwnost));

        public String Dolwnost
        {
            get { return (String)GetValue(DolwnostPageProperty); }
            set { SetValue(DolwnostPageProperty, value); }
        }

        public static readonly DependencyProperty FirstNamePageProperty = DependencyProperty.Register(
        "FirstName1",
        typeof(String),
        typeof(PhotoUserToDolwnost), new UIPropertyMetadata(""));

        public String FirstName1
        {
            get { return (String)GetValue(FirstNamePageProperty); }
            set { SetValue(FirstNamePageProperty, value); }
        }
        public PhotoUserToDolwnost()
        {
            InitializeComponent();
        }
    }
}
