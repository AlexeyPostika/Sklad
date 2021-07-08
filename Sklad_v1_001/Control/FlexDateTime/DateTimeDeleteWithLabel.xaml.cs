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

namespace Sklad_v1_001.Control.FlexDateTime
{
    /// <summary>
    /// Логика взаимодействия для EditBoxDelete.xaml
    /// </summary>
    public partial class DateTimeDeleteWithLabel : UserControl
    {
        public DateTimeDeleteWithLabel()
        {
            InitializeComponent();            
        }

        public event Action ButtonClearClick;
        public event Action SelectedDateChangedClick;
        
        public string LabelText
        {
            get
            {
                return this.label.Content.ToString();
            }

            set
            {
                this.label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return this.label.Width;
            }

            set
            {
                this.label.Width = value;
            }
        }

        public Boolean IsFromfilter
        {
            get
            {
                return this.DateTimeDelete.IsFromfilter;
            }

            set
            {
                this.DateTimeDelete.IsFromfilter = value;
            }
        }

        private void EditBoxDelete_ButtonClearClick()
        {
            if (ButtonClearClick != null)
            {
                ButtonClearClick();
            }
        }

        private void DateTimeDelete_SelectedDateChangedClick()
        {
            if (SelectedDateChangedClick != null)
            {
                SelectedDateChangedClick();
            }
        }
    }
}
