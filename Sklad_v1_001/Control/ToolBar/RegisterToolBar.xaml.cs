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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sklad_v1_001.Control.ToolBar
{
    /// <summary>
    /// Логика взаимодействия для ToolBarZakupkaxaml.xaml
    /// </summary>
    public partial class RegisterToolBar : UserControl
    {
        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
           "IsEnableAdd",
           typeof(Boolean),
           typeof(RegisterToolBar), new UIPropertyMetadata(false));

        public static readonly DependencyProperty IsEnableEditProperty = DependencyProperty.Register(
         "IsEnableEdit",
         typeof(Boolean),
         typeof(RegisterToolBar), new UIPropertyMetadata(false));

        public static readonly DependencyProperty IsEnableDeletedProperty = DependencyProperty.Register(
          "IsEnableDeleted",
          typeof(Boolean),
          typeof(RegisterToolBar), new UIPropertyMetadata(false));

        public static readonly DependencyProperty SearchProperty = DependencyProperty.Register(
         "Search",
         typeof(String),
         typeof(RegisterToolBar), new UIPropertyMetadata(""));

        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }

        public Boolean IsEnableEdit
        {
            get { return (Boolean)GetValue(IsEnableEditProperty); }
            set { SetValue(IsEnableEditProperty, value); }
        }

        public Boolean IsEnableDeleted
        {
            get { return (Boolean)GetValue(IsEnableDeletedProperty); }
            set { SetValue(IsEnableDeletedProperty, value); }
        }

        public String Search
        {
            get { return (String)GetValue(SearchProperty); }
            set { SetValue(SearchProperty, value); }
        }

        public delegate void ButtonScanHandler(String text);

        public event Action ButtonAdd;        
        public event Action ButtonEdit;
        public event Action ButtonDelete;
        public event ButtonScanHandler ButtonScan;
        public event Action ButtonClean;
        public event Action ButtonClear;
        public event Action ButtonRefresh;

        public RegisterToolBar()
        {
            InitializeComponent();
            //NewButton.Image.Source= ImageHelper.GenerateImage("IconAddList_X32.png");
            EditButton.Image.Source = ImageHelper.GenerateImage("IconDetails_X30.png");
            DeletedButton.Image.Source = ImageHelper.GenerateImage("IconDelete_X32.png");
            ClearButton.Image.Source = ImageHelper.GenerateImage("IconClearAllFilters_X32.png");
            RefreshButton.Image.Source = ImageHelper.GenerateImage("IconRefresh_X32.png");
        }


        private void NewButton_ButtonClick()
        {
            ButtonAdd?.Invoke();
        }

        private void EditButton_ButtonClick()
        {
            ButtonEdit?.Invoke();
        }

        private void DeletedButton_ButtonClick()
        {
            ButtonDelete?.Invoke();
        }

        private void ClearButton_ButtonClick()
        {
            ButtonClear?.Invoke();
        }      

        private void Scan_ButtonClearClick()
        {
            ButtonClean?.Invoke();
        }

        private void Scan_ButtonTextChangedClick()
        {
            ButtonScan?.Invoke(Search);
        }

        private void RefreshButton_ButtonClick()
        {
            ButtonRefresh?.Invoke();
        }
    }
}
