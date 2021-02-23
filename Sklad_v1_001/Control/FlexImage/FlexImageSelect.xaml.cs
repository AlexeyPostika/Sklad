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

namespace Sklad_v1_001.Control.FlexImage
{
    /// <summary>
    /// Логика взаимодействия для FlexImageSelect.xaml
    /// </summary>
    public partial class FlexImageSelect : UserControl
    {
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
       "Image",
       typeof(BitmapImage),
       typeof(FlexImageSelect), new UIPropertyMetadata(ImageHelper.GenerateImage("picture_80px.png")));
        public BitmapImage Image
        {
            get { return (BitmapImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ButtonVisibilityProperty = DependencyProperty.Register(
       "ButtonVisibility",
       typeof(Visibility),
       typeof(FlexImageSelect), new UIPropertyMetadata(Visibility.Hidden));
        public Visibility ButtonVisibility
        {
            get { return (Visibility)GetValue(ButtonVisibilityProperty); }
            set { SetValue(ButtonVisibilityProperty, value); }
        }

        public event Action ButtonSelectImage;     
        public FlexImageSelect()
        {
            InitializeComponent();
        }
      
        private void BorderControll_MouseLeave(object sender, MouseEventArgs e)
        {
            this.SelectButton.Visibility = Visibility.Collapsed;
        }

        private void BorderControll_PreviewMouseMove(object sender, MouseEventArgs e)
        {

            this.SelectButton.Visibility = Visibility.Visible;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonSelectImage?.Invoke();
        }
    }
}
