using Sklad_v1_001.GlobalVariable;
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

namespace Sklad_v1_001.Control.FlexImage
{
    /// <summary>
    /// Логика взаимодействия для ImageListPage.xaml
    /// </summary>
    public partial class ImageListPage : UserControl, INotifyPropertyChanged
    {
        FileWork fileWork;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static readonly DependencyProperty ListImageControlProperty = DependencyProperty.Register(
         "ListImageControl",
         typeof(List<BitmapImage>),
         typeof(ImageListPage), new UIPropertyMetadata(new List<BitmapImage>()));
        public List<BitmapImage> ListImageControl
        {
            get { return (List<BitmapImage>)GetValue(ListImageControlProperty); }
            set { SetValue(ListImageControlProperty, value); }
        }       
        Int32 tempClick;
        public int TempClick { get => tempClick; set => tempClick = value; }

        //public event Action ButtonPreviewMouseMove;
        //public event Action ButtonMouseLeave;      
        public ImageListPage()
        {
            InitializeComponent();
            fileWork = new FileWork();
            TempClick = 0;
        }

        private void BorderControll_PreviewMouseMove(object sender, MouseEventArgs e)
        {           
            this.Brack.Visibility = Visibility.Visible;
            this.Next.Visibility = Visibility.Visible;
            this.DownLoad.Visibility = Visibility.Visible;
            this.Clear.Visibility = Visibility.Visible;
            this.Search.Visibility = Visibility.Visible;
            this.AddButton.Visibility = Visibility.Visible;
            //ButtonPreviewMouseMove?.Invoke();
        }

        private void BorderControll_MouseLeave(object sender, MouseEventArgs e)
        {          
            this.Brack.Visibility = Visibility.Hidden;
            this.Next.Visibility = Visibility.Hidden;
            this.DownLoad.Visibility = Visibility.Hidden;
            this.Clear.Visibility = Visibility.Hidden;
            this.Search.Visibility = Visibility.Hidden;
            this.AddButton.Visibility = Visibility.Hidden;
            //ButtonMouseLeave?.Invoke();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.ImagStandart.Source = ImageHelper.GenerateImage("picture_80px.png");
        }

        private void Brack_Click(object sender, RoutedEventArgs e)
        {
            if (ListImageControl.Count > 0)
            {
                TempClick++;
                if (Math.Abs(TempClick) < ListImageControl.Count - 1)
                {                   
                    ImagStandart.Source = ListImageControl[Math.Abs(TempClick)];
                    Next.IsEnabled = true;
                }
                else
                {
                    Next.IsEnabled = true;
                    Brack.IsEnabled = false;
                }
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (ListImageControl.Count > 0)
            {
                TempClick--;
                if (TempClick > 0 && Math.Abs(TempClick) < ListImageControl.Count - 1)
                {
                    ImagStandart.Source = ListImageControl[Math.Abs(TempClick)];
                    Brack.IsEnabled = true;
                }
                else
                {
                    Next.IsEnabled = false;
                    Brack.IsEnabled = true;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TempClick = 0;
            fileWork.OpenFile();
            if (fileWork.ListImage != null && fileWork.ListImage.Count > 0)
            {
                ListImageControl = fileWork.ListImage;
                ImagStandart.Source = ListImageControl[Math.Abs(TempClick)];
            }

        }
    }
}
