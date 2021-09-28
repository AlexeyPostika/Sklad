using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sklad_v1_001.Control.FlexFilter
{
    public class DataContextSpy : Freezable

    {
        public DataContextSpy()

        {
            // This binding allows the spy to inherit a DataContext
            BindingOperations.SetBinding(this, DataContextProperty, new Binding());
        }

        public object DataContext
        {
            get { return GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        // Borrow the DataContext dependency property from FrameworkElement.
        public static readonly DependencyProperty DataContextProperty = FrameworkElement
            .DataContextProperty.AddOwner(typeof(DataContextSpy));

        protected override Freezable CreateInstanceCore()

        {
            // We are required to override this abstract method.
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Логика взаимодействия для FlexGridCheckBox.xaml
    /// </summary>
    ///
    public partial class FlexGridCheckBox : UserControl, INotifyPropertyChanged, IAbstractGridFilter
    {
        public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.Register(
                      "IsMultiSelect",
                       typeof(Boolean),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsMultiSelect
        {
            get
            {
                return (Boolean)GetValue(IsMultiSelectProperty);
            }
            set
            {
                SetValue(IsMultiSelectProperty, value);
                OnPropertyChanged("IsMultiSelect");
            }
        }

        public static readonly DependencyProperty IsHaveImageProperty = DependencyProperty.Register(
                       "IsHaveImage",
                       typeof(Boolean),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsHaveImage
        {
            get
            {
                return (Boolean)GetValue(IsHaveImageProperty);
            }
            set
            {
                SetValue(IsHaveImageProperty, value);
                OnPropertyChanged("IsHaveImage");
            }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                        "ImageSource",
                        typeof(ImageSource),
                        typeof(FlexGridCheckBox));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
                OnPropertyChanged("ImageSource");
            }
        }

        public static readonly DependencyProperty FilterStatusProperty = DependencyProperty.Register(
                        "FilterStatus",
                        typeof(Boolean),
                        typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean FilterStatus
        {
            get
            {
                return (Boolean)GetValue(FilterStatusProperty);
            }
            set
            {
                SetValue(FilterStatusProperty, value);
                OnPropertyChanged("FilterStatus");
            }
        }

        public static readonly DependencyProperty CheckAllProperty = DependencyProperty.Register(
                        "CheckAll",
                        typeof(Boolean),
                        typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean CheckAll
        {
            get
            {
                return (Boolean)GetValue(CheckAllProperty);
            }
            set
            {
                SetValue(CheckAllProperty, value);
                OnPropertyChanged("CheckAll");
            }
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(FlexGridCheckBox), new UIPropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String LabelText
        {
            get
            {
                return (String)GetValue(LabelTextProperty);
            }
            set
            {
                SetValue(LabelTextProperty, value);
                OnPropertyChanged("LabelText");
            }
        }

        public static readonly DependencyProperty TableWidthProperty = DependencyProperty.Register(
                        "TableWidth",
                        typeof(Int32),
                        typeof(FlexGridCheckBox), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 TableWidth
        {
            get
            {
                return (Int32)GetValue(TableWidthProperty);
            }
            set
            {
                SetValue(TableWidthProperty, value);
                OnPropertyChanged("TableWidth");
            }
        }



        public static readonly DependencyProperty DataTableDataProperty = DependencyProperty.Register(

                        "DataTableData",

                        typeof(DataTable),

                        typeof(FlexGridCheckBox), new UIPropertyMetadata(new DataTable()));



        // Обычное свойство .NET  - обертка над свойством зависимостей
        public DataTable DataTableData
        {
            get
            {
                return (DataTable)GetValue(DataTableDataProperty);
            }
            set
            {
                SetValue(DataTableDataProperty, value);
            }
        }
        public string FilterText
        {
            get
            {
                return LabelText;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string FilterValue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string FilterDescription
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Boolean IsChecked
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //public delegate void ButtonStoneClickHandler(FlexFilterStonesLogic flexFilterStonesLogic = null);
        //public event ButtonStoneClickHandler ButtonStonesApplyClick;
        public delegate void ButtonClickHandler(String text = "");
        public event ButtonClickHandler ButtonApplyClick;
        public event Action ButtonStonesApplyClick; //получаем название события

        ConvertData convertdata;

        public FlexGridCheckBox()
        {
            InitializeComponent();
            convertdata = new ConvertData();
            this.DataContext = this;
        }

        private void ButtonFilter_ButtonClick()
        {
            ButtonApplyClick?.Invoke();//flexFilterStonesLogic    
        }

        public void ClearFilters()
        {

        }

        private void ButtonApplyClickWindow()
        {
            ButtonApplyClick?.Invoke();//flexFilterStonesLogic           
        }
    }
}
