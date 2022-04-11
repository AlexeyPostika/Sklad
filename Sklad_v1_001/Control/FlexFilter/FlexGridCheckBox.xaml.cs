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
        FlexGridCheckBoxWindow flexGridCheckBoxWindow;
        FlexGridCheckBoxWithImageWindow flexGridCheckBoxWithImageWindow;

        public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.Register(
                      "IsMultiSelect",
                       typeof(Boolean),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        public static readonly DependencyProperty IsRequiredroperty = DependencyProperty.Register(
                      "IsRequired",
                       typeof(Visibility),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                       "ImageSource",
                       typeof(ImageSource),
                       typeof(FlexGridCheckBox));
        
        public static readonly DependencyProperty IsHaveImageProperty = DependencyProperty.Register(
                      "IsHaveImage",
                      typeof(Boolean),
                      typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        public static readonly DependencyProperty FilterStatusProperty = DependencyProperty.Register(
                      "FilterStatus",
                      typeof(Boolean),
                      typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        public static readonly DependencyProperty CheckAllProperty = DependencyProperty.Register(
                       "CheckAll",
                       typeof(Boolean),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(false));

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                       "LabelText",
                       typeof(String),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(""));

        public static readonly DependencyProperty TableWidthProperty = DependencyProperty.Register(
                       "TableWidth",
                       typeof(Int32),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(0));

        public static readonly DependencyProperty DataTableDataProperty = DependencyProperty.Register(
                       "DataTableData",
                       typeof(DataTable),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(new DataTable()));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                       "Value",
                       typeof(String),
                       typeof(FlexGridCheckBox), new UIPropertyMetadata(String.Empty));

        public Visibility IsRequired
        {
            get
            {
                return (Visibility)GetValue(IsRequiredroperty);
            }
            set
            {
                SetValue(IsRequiredroperty, value);
                OnPropertyChanged("IsRequired");
            }
        }

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

        public double LabelWidth
        {
            get
            {
                return wrapPanel.Width;
            }

            set
            {
                wrapPanel.Width = value;
            }
        }

        public String Value
        {
            get
            {
                return (String)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
                OnPropertyChanged("Value");
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
       
        ConvertData convertdata;

        public FlexGridCheckBox()
        {
            InitializeComponent();
            convertdata = new ConvertData();
            this.DataContext = this;
        }

        private void ButtonFilter_ButtonClick()
        {
            Boolean CheckAllTemp = true;
            if (IsHaveImage)
            {
                if (flexGridCheckBoxWithImageWindow == null)
                {
                    flexGridCheckBoxWithImageWindow = new FlexGridCheckBoxWithImageWindow();
                    flexGridCheckBoxWithImageWindow.ButtonApplyClick += new Action(ButtonApplyClickWindow);
                }
                var location = this.PointToScreen(new Point(0, 0));
                flexGridCheckBoxWithImageWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                flexGridCheckBoxWindow.Width = 350;
                flexGridCheckBoxWindow.Height = 450;
                flexGridCheckBoxWithImageWindow.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;
                flexGridCheckBoxWithImageWindow.Top = location.Y + this.ActualHeight;
                flexGridCheckBoxWithImageWindow.AllowDrop = false;

                foreach (DataRow row in DataTableData.Rows)
                {
                    if (row["IsChecked"] != null && convertdata.FlexDataConvertToBoolean(row["IsChecked"].ToString()) == false)
                    {
                        CheckAllTemp = false;
                        break;
                    }
                }
                flexGridCheckBoxWithImageWindow.CheckAll = CheckAllTemp;
                flexGridCheckBoxWithImageWindow.LabelText = this.LabelText;
                flexGridCheckBoxWithImageWindow.DataTableData = this.DataTableData;
                flexGridCheckBoxWithImageWindow.Search = this.DataTableData.TableName;
                flexGridCheckBoxWithImageWindow.ShowDialog();
            }
            else
            {
                if (flexGridCheckBoxWindow == null)
                {
                    flexGridCheckBoxWindow = new FlexGridCheckBoxWindow();
                    flexGridCheckBoxWindow.ButtonApplyClick += new Action(ButtonApplyClickWindow);
                }

                var location = this.PointToScreen(new Point(0, 0));
                flexGridCheckBoxWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                flexGridCheckBoxWindow.Width = 350;
                flexGridCheckBoxWindow.Height = 450;
                flexGridCheckBoxWindow.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;
                flexGridCheckBoxWindow.Top =  location.Y + this.ActualHeight;
                flexGridCheckBoxWindow.AllowDrop = false;
                
                foreach (DataRow row in DataTableData.Rows)
                {
                    if (row["IsChecked"] != null && convertdata.FlexDataConvertToBoolean(row["IsChecked"].ToString()) == false)
                    {
                        CheckAllTemp = false;
                        break;
                    }
                }
                flexGridCheckBoxWindow.CheckAll = CheckAllTemp;
                flexGridCheckBoxWindow.LabelText = this.LabelText;
                flexGridCheckBoxWindow.DataTableData = this.DataTableData;
                flexGridCheckBoxWindow.Search = this.DataTableData.TableName;
                flexGridCheckBoxWindow.ShowDialog();
            }
        }


        public void ClearFilters()
        {
            flexGridCheckBoxWithImageWindow = null;
            flexGridCheckBoxWindow = null;
        }

        private void ButtonApplyClickWindow()
        {
            String data = "";
            if (IsHaveImage)
            {
                this.DataTableData = flexGridCheckBoxWithImageWindow.DataTableData;
                this.DataTableData.TableName = flexGridCheckBoxWithImageWindow.Search;
                if (flexGridCheckBoxWithImageWindow.FilterStatusAll)
                {
                    ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");
                    data = "All";
                    Value = data;
                    ButtonApplyClick?.Invoke(data);
                    return;
                }

                if (flexGridCheckBoxWithImageWindow.FilterStatusNone)
                {
                    ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");
                    data = "";
                    Value = data;
                    ButtonApplyClick?.Invoke(data);
                    return;
                }

                ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconClearFilter.png");
                foreach (DataRow row in DataTableData.Rows)
                {
                    if (convertdata.FlexDataConvertToBoolean(row["IsChecked"].ToString()))
                    {
                        data = data + row["ID"].ToString();
                    }
                }
            }
            else
            {
                this.DataTableData = flexGridCheckBoxWindow.DataTableData;
                this.DataTableData.TableName = flexGridCheckBoxWindow.Search;
                if (flexGridCheckBoxWindow.FilterStatusAll)
                {
                    ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");
                    data = "All";
                    Value = data;
                    ButtonApplyClick?.Invoke(data);
                    return;
                }

                if (flexGridCheckBoxWindow.FilterStatusNone)
                {
                    ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");
                    data = "";
                    Value = data;
                    ButtonApplyClick?.Invoke(data);
                    return;
                }

                ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconClearFilter.png");
                foreach (DataRow row in DataTableData.Rows)
                {
                    if (convertdata.FlexDataConvertToBoolean(row["IsChecked"].ToString()))
                    {
                        data = data + '\'' + row["ID"].ToString() + "'|";
                    }
                }
            }

            if (!String.IsNullOrEmpty(data) && data.Length > 1)
            {
                data = data.TrimEnd('|');
            }
            Value = data;
            ButtonApplyClick?.Invoke(data);
        }
    }
}
