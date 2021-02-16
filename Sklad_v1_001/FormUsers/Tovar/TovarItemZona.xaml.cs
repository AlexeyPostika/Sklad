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
using Sklad_v1_001.HelperGlobal;

namespace Sklad_v1_001.FormUsers.Tovar
{
    /// <summary>
    /// Interaction logic for TovarZona.xaml
    /// </summary>
    public partial class TovarItemZona : UserControl, INotifyPropertyChanged
    {      
        private Boolean page;
        private Boolean isEnableBack;
        private Boolean isEnableNext;
        private Boolean isEnableBackIn;
        private Boolean isEnableNextEnd;
        private String textOnWhatPage;
        private Int32 numberPage;

        Tovar.TovarZonaLogic logicTovarZona;
        ObservableCollection<Tovar.LocalRow> dataProduct;

        Tovar.LocalRow localDocument;
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
        public TovarItemZona()
        {
            InitializeComponent();           

            dataProduct = new ObservableCollection<LocalRow>();

            logicTovarZona = new TovarZonaLogic();

            localDocument = new LocalRow();
            filterLocal = new LocalFilter();
            filterLocal.Page = 0;
            filterLocal.PageCountRows = 0;
            filterLocal.RowsCountPage = 7;

            sammary = new RowSummary();
            Page = false;
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
