using MessagingToolkit.Barcode;
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

namespace Sklad_v1_001.Control.Stickers
{
    /// <summary>
    /// Логика взаимодействия для FlexStickers20x10.xaml
    /// </summary>
    public partial class FlexStickers20x10 : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
                        "Description",
                        typeof(string),
                        typeof(FlexStickers20x10), new UIPropertyMetadata(String.Empty));

        // свойство зависимостей
        public static readonly DependencyProperty NumericProperty = DependencyProperty.Register(
                        "Numeric",
                        typeof(string),
                        typeof(FlexStickers20x10), new UIPropertyMetadata(String.Empty));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Numeric
        {
            get { return (string)GetValue(NumericProperty); }
            set { SetValue(NumericProperty, value); if (!String.IsNullOrEmpty(value)) { GeneratBareCode(); } }
        }
        public FlexStickers20x10()
        {
            InitializeComponent();            
        }

        private void GeneratBareCode()
        {


            BarcodeEncoder barcodeEncoder = new BarcodeEncoder();
            barcodeEncoder.Width = Int32.Parse(((Int32)control.Width).ToString());
            barcodeEncoder.Height = Int32.Parse(((Int32)(body.Height.Value * 50)).ToString());
            Canvas barcode = barcodeEncoder.EncodeVector(BarcodeFormat.Code128, Numeric);
            barecode.Content = barcode;
            barecode.Height = barcodeEncoder.Height;
            barecode.Width = barcodeEncoder.Width;
        }
    }
}
