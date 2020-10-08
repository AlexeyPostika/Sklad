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

namespace Sklad_v1_001.FormUsers.Zacupca
{
    /// <summary>
    /// Interaction logic for TovarZona.xaml
    /// </summary>
    public partial class ZacupcaGrid : UserControl, INotifyPropertyChanged
    {
        private Boolean page;
        private Boolean isEnableBack;
        private Boolean isEnableNext;
        private Boolean isEnableBackIn;
        private Boolean isEnableNextEnd;
        private String textOnWhatPage;
        private Int32 numberPage;

        Zacupca.ZacupcaGridLogic logicPurchase;
        ObservableCollection<Zacupca.LocalRow> dataPurchase;

        Zacupca.LocalFilter filterLocal;

        Zacupca.RowSummary sammary;

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

        public int NumberPage
        {
            get
            {
                return numberPage;
            }

            set
            {
                numberPage = value;
            }
        }

        public bool IsEnableBackIn
        {
            get
            {
                return isEnableBackIn;
            }

            set
            {
                isEnableBackIn = value;
                OnPropertyChanged("IsEnableBackIn");
            }
        }

        public bool IsEnableNextEnd
        {
            get
            {
                return isEnableNextEnd;
            }

            set
            {
                isEnableNextEnd = value;
                OnPropertyChanged("IsEnableNextEnd");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ZacupcaGrid()
        {
            InitializeComponent();
            dataPurchase = new ObservableCollection<LocalRow>();
            logicPurchase = new ZacupcaGridLogic();
            filterLocal = new LocalFilter();
            filterLocal.RowsCountPage = 7;
            sammary = new RowSummary();

            this.DataDocument.ItemsSource = dataPurchase;
            this.ToolbarNextPageData.DataContext = this;
            Page = false;
            Refresh();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Zacupca.LocalRow local= DataGrid.SelectedItem as Zacupca.LocalRow;
            //this.Informer1.DataContext = local;
           // this.Informe.DataContext = local;
        }
        private void Refresh()
        {
            if (Page == false)
            {
                filterLocal.PageCountRows = 0;
            }
            //почистил данные
            dataPurchase.Clear();

            //получили данные
            DataTable table = logicPurchase.Select(filterLocal);

            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                dataPurchase.Add(logicPurchase.Convert(row, new LocalRow()));
                logicPurchase.ConvertSummary(row, sammary);
            }
            TextOnWhatPage = Properties.Resources.PAGE + " " + (filterLocal.Page + 1).ToString() + " " + Properties.Resources.OF + " " + Math.Ceiling((double)sammary.PageCount / filterLocal.RowsCountPage).ToString();

            IsEnableBack = filterLocal.Page != 0;
            IsEnableBackIn = filterLocal.Page != 0;
            NumberPage = (Int32)(Math.Ceiling((double)sammary.PageCount / filterLocal.RowsCountPage) - 1);
            IsEnableNext = filterLocal.Page != NumberPage;
            IsEnableNextEnd = filterLocal.Page != NumberPage;
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToolBarNextToBack_ButtonBack()
        {
            Page = true;
            //filterLocal.PageCountRows = (filterLocal.Page - 1) * filterLocal.RowsCountPage;
            //filterLocal.Page--;
            Refresh();
        }

        private void ToolBarNextToBack_ButtonNext()
        {
            Page = true;           
            //filterLocal.PageCountRows = (filterLocal.Page + 1) * filterLocal.RowsCountPage;
            //filterLocal.Page++;
            Refresh();
        }

        private void ToolbarNextPageData_ButtonBackIn()
        {
            Page = true;
            //filterLocal.PageCountRows = 0;
            //filterLocal.Page = 0;
            Refresh();
        }

        private void ToolbarNextPageData_ButtonNextEnd()
        {
            Page = true;
            //filterLocal.PageCountRows = NumberPage;
            //filterLocal.Page= NumberPage;
            Refresh();
        }
    }
}
