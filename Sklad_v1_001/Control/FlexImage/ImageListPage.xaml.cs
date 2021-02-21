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
        public static readonly DependencyProperty AddButtonVisibilityProperty = DependencyProperty.Register(
        "AddButtonVisibility",
        typeof(Visibility),
        typeof(ImageListPage), new UIPropertyMetadata(Visibility.Visible));
        public Visibility AddButtonVisibility
        {
            get { return (Visibility) GetValue(AddButtonVisibilityProperty); }
            set { SetValue(AddButtonVisibilityProperty, value); }
        }
        
        //
        public static readonly DependencyProperty DownLoadButtonVisibilityProperty = DependencyProperty.Register(
        "DownLoadButtonVisibility",
        typeof(Visibility),
        typeof(ImageListPage), new UIPropertyMetadata(Visibility.Visible));
        public Visibility DownLoadButtonVisibility
        {
            get { return (Visibility)GetValue(DownLoadButtonVisibilityProperty); }
            set { SetValue(DownLoadButtonVisibilityProperty, value); }
        }
        
        //
        public static readonly DependencyProperty SearchButtonVisibilityProperty = DependencyProperty.Register(
        "SearchButtonVisibility",
        typeof(Visibility),
        typeof(ImageListPage), new UIPropertyMetadata(Visibility.Visible));
        public Visibility SearchButtonVisibility
        {
            get { return (Visibility)GetValue(SearchButtonVisibilityProperty); }
            set { SetValue(SearchButtonVisibilityProperty, value); }
        }
        
        //
        public static readonly DependencyProperty ClearButtonVisibilityProperty = DependencyProperty.Register(
        "ClearButtonVisibility",
        typeof(Visibility),
        typeof(ImageListPage), new UIPropertyMetadata(Visibility.Visible));
        public Visibility ClearButtonVisibility
        {
            get { return (Visibility)GetValue(ClearButtonVisibilityProperty); }
            set { SetValue(ClearButtonVisibilityProperty, value); }
        }

        //
        public static readonly DependencyProperty BrackButtonVisibilityProperty = DependencyProperty.Register(
        "BrackButtonVisibility",
        typeof(Visibility),
        typeof(ImageListPage), new UIPropertyMetadata(Visibility.Hidden));
        public Visibility BrackButtonVisibility
        {
            get { return (Visibility)GetValue(BrackButtonVisibilityProperty); }
            set { SetValue(BrackButtonVisibilityProperty, value); }
        }
        
        //
        public static readonly DependencyProperty NextButtonVisibilityProperty = DependencyProperty.Register(
        "NextButtonVisibility",
        typeof(Visibility),
        typeof(ImageListPage), new UIPropertyMetadata(Visibility.Visible));
        public Visibility NextButtonVisibility
        {
            get { return (Visibility)GetValue(NextButtonVisibilityProperty); }
            set { SetValue(NextButtonVisibilityProperty, value); }
        }

       public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
       "Image",
       typeof(BitmapImage),
       typeof(ImageListPage), new UIPropertyMetadata(ImageHelper.GenerateImage("picture_80px.png")));
        public BitmapImage Image
        {
            get { return (BitmapImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        Int32 tempClick;
        public int TempClick { get => tempClick; set => tempClick = value; }

        //public event Action ButtonPreviewMouseMove;
        //public event Action ButtonMouseLeave;     
        public event Action ButtonSearchOpen;      
        public ImageListPage()
        {
            InitializeComponent();
            fileWork = new FileWork();
            TempClick = 0;
            if (Image != null)
                this.ImagStandart.Source = Image;
            else if (ListImageControl.Count > 0)
            {
                this.ImagStandart.Source = ListImageControl[0];
            }
            
        }

        private void BorderControll_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (BrackButtonVisibility != Visibility.Collapsed || NextButtonVisibility != Visibility.Collapsed)
            {
                this.Brack.Visibility = Visibility.Visible;
                this.Next.Visibility = Visibility.Visible;
                this.DownLoad.Visibility = Visibility.Visible;
                this.Clear.Visibility = Visibility.Visible;
                this.Search.Visibility = Visibility.Visible;
                this.AddButton.Visibility = Visibility.Visible;
            }
            //ButtonPreviewMouseMove?.Invoke();
        }

        private void BorderControll_MouseLeave(object sender, MouseEventArgs e)
        {
            if (BrackButtonVisibility != Visibility.Collapsed || NextButtonVisibility != Visibility.Collapsed)
            {
                this.Brack.Visibility = Visibility.Hidden;
                this.Next.Visibility = Visibility.Hidden;
                this.DownLoad.Visibility = Visibility.Hidden;
                this.Clear.Visibility = Visibility.Hidden;
                this.Search.Visibility = Visibility.Hidden;
                this.AddButton.Visibility = Visibility.Hidden;
            }
            //ButtonMouseLeave?.Invoke();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
           
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ButtonSearchOpen?.Invoke();
        }
    }
}
