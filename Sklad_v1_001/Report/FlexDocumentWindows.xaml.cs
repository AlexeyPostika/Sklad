using Sklad_v1_001.Control;
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
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Sklad_v1_001.Report
{
    /// <summary>
    /// Логика взаимодействия для FlexDocumentWindows.xaml
    /// </summary>
    public partial class FlexDocumentWindows : DialogWindow
    {
        // свойство зависимостей
        public static readonly DependencyProperty DocumentXpsProperty = DependencyProperty.Register(
                        "DocumentXps",
                        typeof(FixedDocumentSequence),
                        typeof(FlexDocumentWindows), new UIPropertyMetadata(null));
        public FixedDocumentSequence DocumentXps
        {
            get { return (FixedDocumentSequence)GetValue(DocumentXpsProperty); }
            set { SetValue(DocumentXpsProperty, value); }

        }
        public FlexDocumentWindows()
        {
            InitializeComponent();
        }
    }
}
