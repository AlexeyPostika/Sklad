using Sklad_v1_001.GlobalVariable;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ImageTest
{
    /// <summary>
    /// Логика взаимодействия для ImagePage.xaml
    /// </summary>
    public partial class ImagePage : Page
    {
        
        public ImagePage()
        {
            InitializeComponent();
            this.ImagStandart.Source = ImageHelper.GenerateImage("picture_80px.png");
        }

        private void BorderControll_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();           
            this.Brack.Visibility = Visibility.Visible;
            this.Next.Visibility = Visibility.Visible;
            this.DownLoad.Visibility = Visibility.Visible;
            this.Clear.Visibility = Visibility.Visible;
            this.Search.Visibility = Visibility.Visible;
        }

        private void BorderControll_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();           
            this.Brack.Visibility = Visibility.Hidden;
            this.Next.Visibility = Visibility.Hidden;
            this.DownLoad.Visibility = Visibility.Hidden;
            this.Clear.Visibility = Visibility.Hidden;
            this.Search.Visibility = Visibility.Hidden;
        }
    }
}
