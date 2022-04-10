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

namespace Sklad_v1_001.Control.ToolBar
{
    /// <summary>
    /// Логика взаимодействия для ToolBarZakupkaxaml.xaml
    /// </summary>
    public partial class ProductToolBar : UserControl
    {
        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
           "IsEnableAdd",
           typeof(Boolean),
           typeof(ProductToolBar), new UIPropertyMetadata(true));
        
        public static readonly DependencyProperty FilterImageProperty = DependencyProperty.Register(
                      "FilterImage",
                      typeof(ImageSource),
                     typeof(ProductToolBar));

        public static readonly DependencyProperty EditImageProperty = DependencyProperty.Register(
                     "EditImage",
                     typeof(ImageSource),
                    typeof(ProductToolBar));

        public static readonly DependencyProperty SearchProperty = DependencyProperty.Register(
         "Search",
         typeof(String),
         typeof(ProductToolBar), new UIPropertyMetadata(""));

        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }

        public ImageSource FilterImage
        {
            get { return (ImageSource)GetValue(FilterImageProperty); }
            set { SetValue(FilterImageProperty, value); }
        }

        public ImageSource EditImage
        {
            get { return (ImageSource)GetValue(EditImageProperty); }
            set { SetValue(EditImageProperty, value); }
        }

        public String Search
        {
            get { return (String)GetValue(SearchProperty); }
            set { SetValue(SearchProperty, value); }
        }

        public delegate void ButtonScanHandler(String text);
        public event ButtonScanHandler ButtonScan;
        
        public event Action ButtonEdit;
        public event Action ButtonFilter;  
        public event Action ButtonBasket;
        public event Action ButtonClean;

        public ProductToolBar()
        {
            InitializeComponent();
            EditButton.Image.Source = ImageHelper.GenerateImage("IconDetails_X30.png");
            FilterButton.Image.Source = ImageHelper.GenerateImage("IconFilter_X30.png");
            BasketButton.Image.Source= ImageHelper.GenerateImage("IconBasket_X30.png");
        }      

        private void BasketButton_ButtonClick()
        {
            ButtonBasket?.Invoke();
        }

        private void FilterButton_ButtonClick()
        {
            ButtonFilter?.Invoke();
        }

        private void EditButton_ButtonClick()
        {
            ButtonEdit?.Invoke();
        }

        private void Scan_ButtonClearClick()
        {
            ButtonClean?.Invoke();
        }

        private void Scan_ButtonTextChangedClick()
        {
            ButtonScan?.Invoke(Search);
        }
    }
}
