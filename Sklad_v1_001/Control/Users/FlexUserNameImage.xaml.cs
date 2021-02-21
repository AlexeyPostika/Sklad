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

namespace Sklad_v1_001.Control.Users
{
    /// <summary>
    /// Логика взаимодействия для FlexUserNameImage.xaml
    /// </summary>
    public partial class FlexUserNameImage : UserControl
    {
        public static readonly DependencyProperty PostProperty = DependencyProperty.Register(
         "Post",
         typeof(String),
         typeof(FlexUserNameImage), new UIPropertyMetadata(String.Empty));

        public String Post
        {
            get { return (String)GetValue(PostProperty); }
            set { SetValue(PostProperty, value); }
        }

        public static readonly DependencyProperty LastNmaeProperty = DependencyProperty.Register(
          "LastNmae",
          typeof(String),
          typeof(FlexUserNameImage), new UIPropertyMetadata(String.Empty));

        public String LastNmae
        {
            get { return (String)GetValue(LastNmaeProperty); }
            set { SetValue(LastNmaeProperty, value); }
        }
        public static readonly DependencyProperty ImageControlProperty = DependencyProperty.Register(
        "ImageControl",
        typeof(BitmapImage),
        typeof(FlexUserNameImage), new UIPropertyMetadata(ImageHelper.GenerateImage("admin1.png")));
        public BitmapImage ImageControl
        {
            get { return (BitmapImage)GetValue(ImageControlProperty); }
            set { SetValue(ImageControlProperty, value); }
        }


        public FlexUserNameImage()
        {
            InitializeComponent();
            ImageControl = ImageHelper.GenerateImage("admin1.png");
        }
    }
}
