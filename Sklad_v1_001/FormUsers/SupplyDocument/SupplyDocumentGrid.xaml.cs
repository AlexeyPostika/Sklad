using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Sklad_v1_001.FormUsers.SupplyDocument
{
    /// <summary>
    /// Логика взаимодействия для SupplyDocumentGrid.xaml
    /// </summary>
    public partial class SupplyDocumentGrid : Page, INotifyPropertyChanged
    {
        DataTable filterIDManagerName;
        DataTable filterIDDelivery;
        DataTable filterIDStatus;
        DataTable filterLastModifiedByUserID;

        Int32 filterDateIDLastModifiadDate;
        DateTime? fromLastModifiadDate;
        DateTime? toLastModifiadDate;

        BitmapImage clearfilterManagerNameID;
        BitmapImage clearfilterDeliveryID;
        BitmapImage clearfilterStatusID;
        BitmapImage ClearfilterLastModifiedByUserID;

        BitmapImage clearfilterTagPrice;

        private Boolean isEnableBack;
        private Boolean isEnableNext;
        private Boolean isEnableBackIn;
        private Boolean isEnableNextEnd;
        private String textOnWhatPage;

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

        public SupplyDocumentGrid()
        {
            InitializeComponent();
        }

        #region Paginator
        private void ToolBarNextToBack_ButtonBack()
        {

        }

        private void ToolBarNextToBack_ButtonNext()
        {

        }

        private void ToolbarNextPageData_ButtonBackIn()
        {

        }

        private void ToolbarNextPageData_ButtonNextEnd()
        {

        }
        #endregion

        private void FilterToDateLastModifiedDate_ButtonFilterSelected()
        {

        }

        private void FilterStatusID_ButtonApplyClick(string text)
        {

        }

        private void FilterLastModifiedByUserID_ButtonApplyClick(string text)
        {
            //if (NeedRefresh)
            //{
            //    filter.LastModifiedByUserID = text;
            //    Refresh();
            //}
        }

        private void FilterDeliveryID_ButtonApplyClick(string text)
        {

        }

        private void FilterManagerNameID_ButtonApplyClick(string text)
        {

        }

        private void FilterAmount_ButtonApplyClick()
        {

        }
    }
}
