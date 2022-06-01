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
    /// Логика взаимодействия для DataViewToolNewSupplyDocument.xaml
    /// </summary>
    public partial class NewSaleDocumentGridToolBar : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(String.Empty));

        // свойство зависимостей
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(String.Empty));

        // свойство зависимостей
        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(
                        "LabelWidth",
                        typeof(Int32),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(230));
      

        // свойство зависимостей
        public static readonly DependencyProperty IsEnabledButton1Property = DependencyProperty.Register(
                        "IsEnabledButton1",
                        typeof(Boolean),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(true));

        // свойство зависимостей
        public static readonly DependencyProperty IsEnabledButton7Property = DependencyProperty.Register(
                        "IsEnabledButton7",
                        typeof(Boolean),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(true));

        // свойство зависимостей
        public static readonly DependencyProperty IsVisibleButton1Property = DependencyProperty.Register(
                        "IsVisibleButton1",
                        typeof(Visibility),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(Visibility.Visible));

        // свойство зависимостей
        public static readonly DependencyProperty IsVisibleButton7Property = DependencyProperty.Register(
                        "IsVisibleButton7",
                        typeof(Visibility),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(Visibility.Visible));

        // свойство зависимостей
        public static readonly DependencyProperty IsEnabledTextBoxScanProperty = DependencyProperty.Register(
                        "IsEnabledTextBoxScan",
                        typeof(Boolean),
                        typeof(NewSaleDocumentGridToolBar), new UIPropertyMetadata(true));


        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 LabelWidth
        {
            get { return (Int32)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }
      
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsEnabledButton1
        {
            get { return (Boolean)GetValue(IsEnabledButton1Property); }
            set { SetValue(IsEnabledButton1Property, value); }
        }
     
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsEnabledButton7
        {
            get { return (Boolean)GetValue(IsEnabledButton7Property); }
            set { SetValue(IsEnabledButton7Property, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsVisibleButton1
        {
            get { return (Visibility)GetValue(IsVisibleButton1Property); }
            set { SetValue(IsVisibleButton1Property, value); }
        }    
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsVisibleButton7
        {
            get { return (Visibility)GetValue(IsVisibleButton7Property); }
            set { SetValue(IsVisibleButton7Property, value); }
        }
        //IsEnabledTextBoxScan
        public Boolean IsEnabledTextBoxScan
        {
            get { return (Boolean)GetValue(IsEnabledTextBoxScanProperty); }
            set { SetValue(IsEnabledTextBoxScanProperty, value); }
        }
        public NewSaleDocumentGridToolBar()
        {
            InitializeComponent();
            ButtonNewProduct.Image.Source = ImageHelper.GenerateImage("IconAddProduct.png");
            ButtonDelete.Image.Source = ImageHelper.GenerateImage("IconDelete.png");
        }

        public event Action ButtonNewProductClick;      
        public event Action ButtonDeleteClick;
        public event Action ButtonScanClick;

        private void ButtonNewProduct_ButtonClick()
        {
            ButtonNewProductClick?.Invoke();
        }

        private void ButtonDelete_ButtonClick()
        {
            ButtonDeleteClick?.Invoke();
        }

        private void Scan_ButtonClick()
        {
            ButtonScanClick?.Invoke();
        }
    }
}
