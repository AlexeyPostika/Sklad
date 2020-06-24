using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Sklad_v1_001.FormUsers.Userss
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Page, INotifyPropertyChanged
    {
        UserListLogic userListLogic;

        ObservableCollection<LocalRow> dataUser;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public UserList()
        {
            InitializeComponent();
            dataUser = new ObservableCollection<LocalRow>();

            userListLogic = new UserListLogic(); ;

            //filterLocal = new LocalFilter();
            //filterLocal.Page = 0;
            //filterLocal.PageCountRows = 0;
            //filterLocal.RowsCountPage = 7;

            //sammary = new RowSummary();

            this.DataGrid.ItemsSource = dataUser;
           // this.ToolbarNextPageData.DataContext = this;
           // Page = false;
            Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

#       region Refresh
        private void Refresh()
        {
            //if (Page == false)
            //{
            //    filterLocal.PageCountRows = 0;
            //}
            //почистил данные
            dataUser.Clear();

            //получили данные
            DataTable table = userListLogic.Select();

            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                dataUser.Add(userListLogic.Convert(row, new LocalRow()));
                //logicTovarZona.ConvertSummary(row, sammary);
            }
            //TextOnWhatPage = Properties.Resources.PAGE + " " + (filterLocal.Page + 1).ToString() + " " + Properties.Resources.OF + " " + Math.Ceiling((double)sammary.PageCount / filterLocal.RowsCountPage).ToString();

            //IsEnableBack = filterLocal.Page != 0;
            //IsEnableBackIn = filterLocal.Page != 0;
            //NumberPage = (Int32)(Math.Ceiling((double)sammary.PageCount / filterLocal.RowsCountPage) - 1);
            //IsEnableNext = filterLocal.Page != NumberPage;
            //IsEnableNextEnd = filterLocal.Page != NumberPage;
        }
        #endregion

        #region Pagenator
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
    }
}
