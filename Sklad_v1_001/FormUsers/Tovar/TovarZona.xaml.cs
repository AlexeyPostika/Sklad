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
using Sklad_v1_001.FormUsers.Tovar;
using System.Collections.ObjectModel;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sklad_v1_001.FormUsers.Tovar
{
    /// <summary>
    /// Interaction logic for TovarZona.xaml
    /// </summary>
    public partial class TovarZona : UserControl, INotifyPropertyChanged
    {
        private Boolean page;
        private Boolean isEnableBack;
        private Boolean isEnableNext;
        private String textOnWhatPage;

        Tovar.TovarZonaLogic logicTovarZona;
        ObservableCollection<Tovar.LocalRow> dataProduct;

        Tovar.LocalFilter filterLocal;

        Tovar.RowSummary sammary;

        public bool Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
            }
        }

        public bool IsEnableBack
        {
            get
            {
                return isEnableBack;
            }

            set
            {
                isEnableBack = value;
                OnPropertyChanged("IsEnableBack");
            }
        }

        public bool IsEnableNext
        {
            get
            {
                return isEnableNext;
            }

            set
            {
                isEnableNext = value;
                OnPropertyChanged("IsEnableNext");
            }
        }

        public String TextOnWhatPage
        {
            get
            {
                return textOnWhatPage;
            }

            set
            {
                textOnWhatPage = value;
                OnPropertyChanged("TextOnWhatPage");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public TovarZona()
        {
            InitializeComponent();
            dataProduct = new ObservableCollection<LocalRow>();

            logicTovarZona = new TovarZonaLogic();

            filterLocal = new LocalFilter();
            filterLocal.Page = 0;
            filterLocal.PageCountRows = 0;
            filterLocal.RowsCountPage = 7;

            sammary = new RowSummary();

            this.DataGrid.ItemsSource = dataProduct;
            this.ToolbarNextPageData.DataContext = this;
            //this.DataGrid.DataContext = localrow;
            Page = false;
            Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Refresh()
        {
            if (Page == false)
            {
                filterLocal.PageCountRows = 0;
            }
            //почистил данные
            dataProduct.Clear();
            
            //получили данные
            DataTable table = table = logicTovarZona.Select(filterLocal);
  
            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                dataProduct.Add(logicTovarZona.Convert(row, new LocalRow()));
                logicTovarZona.ConvertSummary(row, sammary);
            }
            TextOnWhatPage = Properties.Resources.PAGE + " " + (filterLocal.Page + 1).ToString() + " " + Properties.Resources.OF + " " + Math.Ceiling((double)sammary.PageCount / filterLocal.RowsCountPage).ToString();
         
            IsEnableBack = filterLocal.Page != 0;        
            IsEnableNext = filterLocal.Page != (Int32)(Math.Ceiling((double)sammary.PageCount / filterLocal.RowsCountPage) - 1);
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToolBarNextToBack_ButtonBack()
        {
            Page = true;
            filterLocal.PageCountRows = (filterLocal.Page - 1) * filterLocal.RowsCountPage;
            filterLocal.Page--;
            Refresh();
        }

        private void ToolBarNextToBack_ButtonNext()
        {
            Page = true;           
            filterLocal.PageCountRows = (filterLocal.Page + 1) * filterLocal.RowsCountPage;
            filterLocal.Page++;
            Refresh();
        }
    }
}
