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
    /// Логика взаимодействия для ImageListPage.xaml
    /// </summary>
    public partial class ImageListPage : UserControl
    {
        public event Action ButtonPreviewMouseMove;
        public event Action ButtonMouseLeave;      
        public ImageListPage()
        {
            InitializeComponent();
            this.ImagStandart.Source = ImageHelper.GenerateImage("picture_80px.png");
        }

        private void BorderControll_PreviewMouseMove(object sender, MouseEventArgs e)
        {           
            this.Brack.Visibility = Visibility.Visible;
            this.Next.Visibility = Visibility.Visible;
            this.DownLoad.Visibility = Visibility.Visible;
            this.Clear.Visibility = Visibility.Visible;
            this.Search.Visibility = Visibility.Visible;
            ButtonPreviewMouseMove?.Invoke();
        }

        private void BorderControll_MouseLeave(object sender, MouseEventArgs e)
        {          
            this.Brack.Visibility = Visibility.Hidden;
            this.Next.Visibility = Visibility.Hidden;
            this.DownLoad.Visibility = Visibility.Hidden;
            this.Clear.Visibility = Visibility.Hidden;
            this.Search.Visibility = Visibility.Hidden;
            ButtonMouseLeave?.Invoke();
        }
    }
}
