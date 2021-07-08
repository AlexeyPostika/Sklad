using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public partial class DateTimeDelete : UserControl
    {
        public DateTimeDelete()
        {
            InitializeComponent();
            image.Source = ImageHelper.GenerateImage("IconDelete.png"); 
        }

        private Boolean isFromfilter;

        public Boolean IsFromfilter
        {
            get
            {
                return isFromfilter;
            }

            set
            {
                isFromfilter = value;
                if (value)
                    this.DatePicker.SelectedDate = (DateTime)SqlDateTime.MinValue;
                else
                {
                    this.DatePicker.SelectedDate = DateTime.Now;
                }
            }
        }
                
        public event Action SelectedDateChangedClick;
        public event Action ClearClick;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (isFromfilter)
                this.DatePicker.SelectedDate = (DateTime)SqlDateTime.MinValue;
            else
            {
                this.DatePicker.SelectedDate = DateTime.Now;
            }
            ClearClick?.Invoke();
        }       

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDateChangedClick?.Invoke();
        }

        private void CurrentDayButton_Click(object sender, RoutedEventArgs e)
        {
            this.DatePicker.SelectedDate = DateTime.Now;
        }
    }
}
