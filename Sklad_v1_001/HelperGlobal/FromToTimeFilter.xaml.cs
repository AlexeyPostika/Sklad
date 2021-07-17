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
using System.Windows.Shapes;
using Sklad_v1_001.HelperGlobal;

namespace Sklad_v1_001.HelperGlobal
{
    /// <summary>
    /// Логика взаимодействия для FromToTimeFilter.xaml
    /// </summary>
    public partial class FromToTimeFilter : Page, IAbstractButtonFilter
    {
        Boolean apply;

        public DateTime? Datefrom
        {
            get
            {
                return this.DateFrom.SelectedDate;
            }

            set
            {
                this.DateFrom.SelectedDate = value;
            }
        }

        public DateTime? Dateto
        {
            get
            {
                return this.DateTo.SelectedDate;
            }

            set
            {
                this.DateTo.SelectedDate = value;
            }
        }

        public Boolean Apply
        {
            get
            {
                return apply;
            }

            set
            {
                apply = value;
            }
        }

        string IAbstractButtonFilter.Text
        {
            get
            {
                return Properties.Resources.TimeFilterMessage1;                
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IAbstractButtonFilter.From
        {
            get
            {
                return ((DateTime)DateFrom.SelectedDate).ToShortDateString();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        string IAbstractButtonFilter.To
        {
            get
            {
                return ((DateTime)DateTo.SelectedDate).ToShortDateString();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public FromToTimeFilter()
        {
            InitializeComponent();
            ButtonSave.Image.Source = ImageHelper.GenerateImage("IconApply.png"); 
            ButtonCancel.Image.Source = ImageHelper.GenerateImage("IconClose.png");
        }

        private void ButtonSave_ButtonClick()
        {
            Apply = true;
            Control.FlexMessageBox.FlexMessageBox mb = new Control.FlexMessageBox.FlexMessageBox();
            if (Dateto < Datefrom)
            {
                mb.Show(Properties.Resources.ErrorFilterToSmallerFrom, MessageBoxTitleHelper.GenerateTitle(MessageBoxTitleHelper.TitleType.Error, Properties.Resources.BadRange), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                Window win = this.Parent as Window;
                win.Close();
            }                        
        }

        private void ButtonCancel_ButtonClick()
        {
            Apply = false;
            Window win = this.Parent as Window;
            win.Close();           
        }

        private void DateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateFrom.SelectedDate == null)
            {
                DateFrom.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
        }

        private void DateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateTo.SelectedDate == null)
            {
                DateTo.SelectedDate = DateTime.Now;
            }           
        }
    }
}
