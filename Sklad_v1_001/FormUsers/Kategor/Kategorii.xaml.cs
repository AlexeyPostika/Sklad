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

namespace Sklad_v1_001.FormUsers.Kategor
{
    /// <summary>
    /// Interaction logic for Kategorii.xaml
    /// </summary>
    public partial class Kategorii : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static readonly DependencyProperty VisibilityGreenProperty = DependencyProperty.Register(
         "VisibilityZonaGreen",
         typeof(Visibility),
         typeof(Kategorii), new UIPropertyMetadata(Visibility.Collapsed));     
        public Visibility IsEnableZonaGreen
        {
            get { return (Visibility)GetValue(VisibilityGreenProperty); }
            set { SetValue(VisibilityGreenProperty, value); }
        }
        public static readonly DependencyProperty VisibilityZonaYellowProperty = DependencyProperty.Register(
        "VisibilityZonaYellow",
        typeof(Visibility),
        typeof(Kategorii), new UIPropertyMetadata(Visibility.Collapsed));
        public Visibility VisibilityZonaYellow
        {
            get { return (Visibility)GetValue(VisibilityZonaYellowProperty); }
            set { SetValue(VisibilityZonaYellowProperty, value); }
        }
        public static readonly DependencyProperty VisibilityRedProperty = DependencyProperty.Register(
        "VisibilityZonaRed",
        typeof(Visibility),
        typeof(Kategorii), new UIPropertyMetadata(Visibility.Collapsed));
        public Visibility IsEnableZonaRed
        {
            get { return (Visibility)GetValue(VisibilityRedProperty); }
            set { SetValue(VisibilityRedProperty, value); }
        }
        public Kategorii()
        {
            InitializeComponent();
        }
    }
}
