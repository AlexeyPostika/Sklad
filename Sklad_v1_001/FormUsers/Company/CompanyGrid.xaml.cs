using Sklad_v1_001.GlobalAttributes;
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

namespace Sklad_v1_001.FormUsers.Company
{
    /// <summary>
    /// Логика взаимодействия для CompanyGrid.xaml
    /// </summary>
    public partial class CompanyGrid : Page
    {
        Attributes attributes;
        public CompanyGrid(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;
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
            //DataTable datatable = supplyDocumentLogic.FillGrid(localFilter);
            //datalist.Clear();

            //foreach (DataRow row in datatable.Rows)
            //{
            //    datalist.Add(supplyDocumentLogic.Convert(row, new LocalRow()));
            //}

            //CalculateSummary();

            //TotalCount = summary.SummaryQuantityLine;
            //PageCount = localFilter.PagerowCount;
            //CurrentPage = localFilter.PageNumber;
        }
        #endregion
    }
}
