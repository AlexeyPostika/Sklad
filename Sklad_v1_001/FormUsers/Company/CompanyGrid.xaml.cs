using Sklad_v1_001.FormUsers.Users;
using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace Sklad_v1_001.FormUsers.Company
{
    /// <summary>
    /// Логика взаимодействия для CompanyGrid.xaml
    /// </summary>
    public partial class CompanyGrid : Page
    {
        Attributes attributes;
        UserLogic userLogic;

        CompanyLogic companyLogic;

        LocaleRow localeRow;
        LocaleFilter localeFilter;

        ObservableCollection<LocaleRow> datalist;

        public CompanyGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            companyLogic = new CompanyLogic(attributes);

            localeFilter = new LocaleFilter();
            datalist = new ObservableCollection<LocaleRow>();

            listCompany.ItemsSource = datalist;
                    
            Refresh();

        }
        #region Toolbar
        private void ToolBarCompany_ButtonAdd()
        {

        }

        private void ToolBarCompany_ButtonDelete()
        {

        }

        private void ToolBarCompany_ButtonRefresh()
        {

        }

        private void ToolBarCompany_ButtonScan(string text)
        {

        }

        private void ToolBarCompany_ButtonClean()
        {

        }
        #endregion

        #region DataGrid
        private void listCompany_Loaded(object sender, RoutedEventArgs e)
        {
           // localFilter.PagerowCount = (Int32)(SypplyDocumentList.ActualHeight) / 40;
            Refresh();
        }

        private void listCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion
        #region Refresh
        public void Refresh()
        {
            List<DataTable> listData = companyLogic.FillGrid(localeFilter);
            DataTable datatable = listData.FirstOrDefault(x => x.TableName == "Company");
            DataTable datatablyDirectory = listData.FirstOrDefault(x => x.TableName == "GeneralDirectory");
            DataTable datatablySeniorAccount = listData.FirstOrDefault(x => x.TableName == "SeniorAccount");

            datalist.Clear();

            foreach (DataRow row in datatable.Rows)
            {
                LocaleRow localeRow = companyLogic.Convert(row, new LocaleRow());
                userLogic = new UserLogic(attributes);
                DataRow[] rowDirector = datatablyDirectory.Select("ID  = " + localeRow.GeneralDirectoryID);
                if (rowDirector!=null && rowDirector.Count()>0)
                    localeRow.GeneralDirectory = userLogic.Convert(rowDirector[0], new LocalRow());
                DataRow[] rowSeniorAccount = datatablySeniorAccount.Select("ID  = " + localeRow.SeniorAccountID);
                if (rowSeniorAccount != null && rowSeniorAccount.Count() > 0)
                    localeRow.GeneralDirectory = userLogic.Convert(rowSeniorAccount[0], new LocalRow());
                datalist.Add(localeRow);


            }

            //CalculateSummary();

            //TotalCount = summary.SummaryQuantityLine;
            //PageCount = localFilter.PagerowCount;
            //CurrentPage = localFilter.PageNumber;
        }
        #endregion
    }
}
