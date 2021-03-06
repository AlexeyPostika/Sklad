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
using Sklad_v1_001.Control.FlexMessageBox;

namespace Sklad_v1_001.FormUsers.Tovar
{
    /// <summary>
    /// Interaction logic for TovarZona.xaml
    /// </summary>
    public partial class TovarZona : UserControl, INotifyPropertyChanged
    {      
        public event Action ButtonInTovar;

        private Boolean page;
        private Boolean isEnableBack;
        private Boolean isEnableNext;
        private Boolean isEnableBackIn;
        private Boolean isEnableNextEnd;
        private String textOnWhatPage;
        private Int32 numberPage;

        TovarItemZona tovarItemZona;
        Window tovarItemZonaWindow; 

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
        public TovarZona()
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

            this.DataGrid.ItemsSource = dataProduct;
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
            localDocument = DataGrid.SelectedItem as Tovar.LocalRow;
            this.Informer1.DataContext = localDocument;
            this.Informe.DataContext = localDocument;
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
            DataTable table = logicTovarZona.Select(filterLocal);
  
            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                dataProduct.Add(logicTovarZona.Convert(row, new LocalRow()));
                logicTovarZona.ConvertSummary(row, sammary);
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
       
        private void ToolBarZakupkaxaml_ButtonSave()
        {
            if (localDocument != null)
                Save(localDocument);
        }

        private void Save(LocalRow _localRow)
        {
            _localRow.DocumentID = _localRow.ID;
            _localRow.ID = 0;
            foreach (BitmapImage bitmapImage in _localRow.ListImage)
            {
                _localRow.PhotoImageByte = ImageSql.ConvertToBytes(bitmapImage);           
                logicTovarZona.Save(_localRow);
            }
        }

        #region Paginator
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

        private void ToolbarNextPageData_ButtonBackIn()
        {
            Page = true;
            filterLocal.PageCountRows = 0;
            filterLocal.Page = 0;
            Refresh();
        }

        private void ToolbarNextPageData_ButtonNextEnd()
        {
            Page = true;
            filterLocal.PageCountRows = NumberPage;
            filterLocal.Page = NumberPage;
            Refresh();
        }
        #endregion

        private void ToolBarZakupkaxaml_ButtonEdit()
        {
            localDocument = DataGrid.SelectedItem as Tovar.LocalRow;
            if (localDocument != null)
            {
                EditRow();
                MainWindow.AppWindow.ButtonProductEditOpenF(localDocument);
            }
        }
        private void EditRow()
        {
            tovarItemZona = new TovarItemZona();

            localDocument = DataGrid.SelectedItem as Tovar.LocalRow;
            tovarItemZona.Imagelist.ListImageControl.Clear();
            if (localDocument != null && localDocument.ID > 0)
            {
                DataTable datatable = logicTovarZona.Select(localDocument.ID);
                foreach (DataRow currentrow in datatable.Rows)
                {
                    logicTovarZona.ConvertImage(currentrow, localDocument);
                }
                tovarItemZona.ListImage = localDocument.ListImage;
                tovarItemZonaWindow = new FlexWindows(Properties.Resources.ProductItemScreenTitle);
                tovarItemZonaWindow.Content = tovarItemZona;
                tovarItemZonaWindow.ShowDialog(); 
            }
        }

        private void ImageListPage_ButtonSearchOpen()
        {
            EditRow();
        }

        private void FlexImageSelect_ButtonSelectImage()
        {
            EditRow();
        }
    }
}
