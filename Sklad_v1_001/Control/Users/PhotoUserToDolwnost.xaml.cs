using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class PhotoUserToDolwnost : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static readonly DependencyProperty DolwnostPageProperty = DependencyProperty.Register(
        "Dolwnost",
        typeof(String),
        typeof(PhotoUserToDolwnost), new UIPropertyMetadata(Properties.Resources.Dolwnost));
         public String Dolwnost
        {
            get { return (String)GetValue(DolwnostPageProperty); }
            set { SetValue(DolwnostPageProperty, value); }
        }
         public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                         "ImageSource",
                         typeof(BitmapImage),
                         typeof(PhotoUserToDolwnost));
         // Обычное свойство .NET  - обертка над свойством зависимостей
         public BitmapImage ImageSource
         {
             get
             {
                 return (BitmapImage)GetValue(ImageSourceProperty);
             }
             set
             {
                 SetValue(ImageSourceProperty, value);
                 OnPropertyChanged("ImageSource");
             }
         }
        public PhotoUserToDolwnost()
        {
            InitializeComponent();
        }
    }
}
