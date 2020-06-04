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
    /// Логика взаимодействия для ToolBarNextToBack.xaml
    /// </summary>
    public partial class ToolBarNextToBack : UserControl
    {
        public static readonly DependencyProperty WidthOnWhatPageProperty =
          DependencyProperty.Register("WidthOnWhatPage", typeof(Int32),
          typeof(ToolBarNextToBack), new UIPropertyMetadata(50));
        public Int32 WidthOnWhatPage
        {
            get { return (Int32)GetValue(WidthOnWhatPageProperty); }
            set { SetValue(WidthOnWhatPageProperty, value); }
        }

        public static readonly DependencyProperty TextOnWhatPageProperty =
          DependencyProperty.Register("TextOnWhatPage", typeof(String),
          typeof(ToolBarNextToBack), new UIPropertyMetadata(""));
        public String TextOnWhatPage
        {
            get { return (String)GetValue(WidthOnWhatPageProperty); }
            set { SetValue(WidthOnWhatPageProperty, value); }
        }
        public ToolBarNextToBack()
        {
            InitializeComponent();
        }
    }
}
