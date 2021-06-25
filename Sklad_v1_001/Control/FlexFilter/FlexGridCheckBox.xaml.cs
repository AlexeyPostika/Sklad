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

            // This binding allows the spy to inherit a DataContext.

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

        //FlexGridCheckBoxWindow flexGridCheckBoxWindow;

        //FlexGridCheckBoxWithImageWindow flexGridCheckBoxWithImageWindow;

        //FlexGridCheckBoxWithImageStonesWindow flexGridCheckBoxWithImageStonesWindow;



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



        public delegate void ButtonStoneClickHandler(FlexFilterStonesLogic flexFilterStonesLogic = null);

        public event ButtonStoneClickHandler ButtonStonesApplyClick;



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

            if (IsMultiSelect)

            {

                if (flexGridCheckBoxWithImageStonesWindow == null)

                {

                    flexGridCheckBoxWithImageStonesWindow = new FlexGridCheckBoxWithImageStonesWindow();

                    flexGridCheckBoxWithImageStonesWindow.ButtonApplyClick += new Action(ButtonApplyClickWindow);

                }



                var location = this.PointToScreen(new Point(0, 0));

                flexGridCheckBoxWithImageStonesWindow.WindowStartupLocation = WindowStartupLocation.Manual;

                flexGridCheckBoxWithImageStonesWindow.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;

                flexGridCheckBoxWithImageStonesWindow.Top = location.Y + this.ActualHeight;

                flexGridCheckBoxWithImageStonesWindow.AllowDrop = false;

                flexGridCheckBoxWithImageStonesWindow.CheckAll = true;

                flexGridCheckBoxWithImageStonesWindow.LabelText = this.LabelText;

                flexGridCheckBoxWithImageStonesWindow.DataTableData = this.DataTableData;

                flexGridCheckBoxWithImageStonesWindow.Search = this.DataTableData.TableName; //сброс поиска

                flexGridCheckBoxWithImageStonesWindow.ShowDialog();

            }



            else

            {

                if (IsHaveImage)

                {

                    if (flexGridCheckBoxWithImageWindow == null)

                    {

                        flexGridCheckBoxWithImageWindow = new FlexGridCheckBoxWithImageWindow();

                        flexGridCheckBoxWithImageWindow.ButtonApplyClick += new Action(ButtonApplyClickWindow);

                    }



                    var location = this.PointToScreen(new Point(0, 0));

                    flexGridCheckBoxWithImageWindow.WindowStartupLocation = WindowStartupLocation.Manual;

                    flexGridCheckBoxWithImageWindow.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;

                    flexGridCheckBoxWithImageWindow.Top = location.Y + this.ActualHeight;

                    flexGridCheckBoxWithImageWindow.AllowDrop = false;

                    flexGridCheckBoxWithImageWindow.CheckAll = true;

                    flexGridCheckBoxWithImageWindow.LabelText = this.LabelText;

                    flexGridCheckBoxWithImageWindow.DataTableData = this.DataTableData;

                    flexGridCheckBoxWithImageWindow.Search = this.DataTableData.TableName;

                    flexGridCheckBoxWithImageWindow.ShowDialog();

                }

                else

                {

                    if (flexGridCheckBoxWindow == null)

                    {

                        //flexGridCheckBoxWindow = new FlexGridCheckBoxWindow();

                        flexGridCheckBoxWindow.ButtonApplyClick += new Action(ButtonApplyClickWindow);

                    }



                    var location = this.PointToScreen(new Point(0, 0));

                    flexGridCheckBoxWindow.WindowStartupLocation = WindowStartupLocation.Manual;

                    flexGridCheckBoxWindow.Left = this.ButtonFilter.PointToScreen(new Point(0, 0)).X + this.ButtonFilter.ActualWidth;

                    flexGridCheckBoxWindow.Top = location.Y + this.ActualHeight;

                    flexGridCheckBoxWindow.AllowDrop = false;

                    flexGridCheckBoxWindow.CheckAll = true;

                    flexGridCheckBoxWindow.LabelText = this.LabelText;

                    flexGridCheckBoxWindow.DataTableData = this.DataTableData;

                    flexGridCheckBoxWindow.Search = this.DataTableData.TableName;

                    flexGridCheckBoxWindow.ShowDialog();

                }

            }

        }



        public void ClearFilters()

        {

            if (flexGridCheckBoxWithImageStonesWindow != null)

            {

                DataTableData.Clear();

                DataTableData = flexGridCheckBoxWithImageStonesWindow.DataTableDataFull.Copy();

                flexGridCheckBoxWithImageStonesWindow = null;

            }



            flexGridCheckBoxWithImageWindow = null;

            flexGridCheckBoxWindow = null;

        }



        private void ButtonApplyClickWindow()

        {

            String data = "";

            //FlexFilterStonesLogic flexFilterStonesLogic = new FlexFilterStonesLogic();



            if (IsMultiSelect)

            {

                this.DataTableData.TableName = flexGridCheckBoxWithImageStonesWindow.Search;

                if (flexGridCheckBoxWithImageStonesWindow.FilterStatusAll)

                {

                    ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");

                    //flexFilterStonesLogic.Stones = "All";

                    ButtonStonesApplyClick?.Invoke();//flexFilterStonesLogic

                    return;

                }



                if (flexGridCheckBoxWithImageStonesWindow.FilterStatusNone)

                {

                    ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");

                    //flexFilterStonesLogic.Stones = "";

                    ButtonStonesApplyClick?.Invoke();//flexFilterStonesLogic

                    return;

                }



                ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconClearFilter.png");

                String tempWeightMin = "";

                String tempWeightMax = "";

                String tempQuantityMin = "";

                String tempQuantityMax = "";

                String tempShape = "";

                String tempSize = "";

                String tempColor = "";

                String tempClarity = "";

                String tempSetting = "";



                foreach (DataRow row in flexGridCheckBoxWithImageStonesWindow.DataTableData.Rows)

                {

                    if (convertdata.FlexDataConvertToBoolean(row["IsChecked"].ToString()))

                    {

                        tempWeightMin = tempWeightMin + row["WeightMin"] + "$";

                        tempWeightMax = tempWeightMax + row["WeightMax"] + "$";

                        tempQuantityMin = tempQuantityMin + row["QuantityMin"] + "$";

                        tempQuantityMax = tempQuantityMax + row["QuantityMax"] + "$";

                        tempShape = tempShape + row["Shape"] + "$";

                        tempSize = tempSize + row["Size"] + "$";

                        tempColor = tempColor + row["Color"] + "$";

                        tempClarity = tempClarity + row["Clarity"] + "$";

                        tempSetting = tempSetting + row["Setting"] + "$";

                        data = data + '\'' + row["Description"].ToString() + "\'|";

                    }

                }

                //flexFilterStonesLogic.StoneWeightMin = tempWeightMin;

                //flexFilterStonesLogic.StoneWeightMax = tempWeightMax;

                //flexFilterStonesLogic.StoneQuantityMin = tempQuantityMin;

                //flexFilterStonesLogic.StoneQuantityMax = tempQuantityMax;

                //flexFilterStonesLogic.StoneShape = tempShape;

                //flexFilterStonesLogic.StoneSize = tempSize;

                //flexFilterStonesLogic.StoneColor = tempColor;

                //flexFilterStonesLogic.StoneClarity = tempClarity;

                //flexFilterStonesLogic.StoneSetting = tempSetting;

                //flexFilterStonesLogic.Stones = data;

                //flexFilterStonesLogic.AllStones = flexGridCheckBoxWithImageStonesWindow.IsAndChoice;

                ButtonStonesApplyClick?.Invoke();//flexFilterStonesLogic

            }



            else

            {

                if (IsHaveImage)

                {

                    this.DataTableData = flexGridCheckBoxWithImageWindow.DataTableData;

                    this.DataTableData.TableName = flexGridCheckBoxWithImageWindow.Search;

                    if (flexGridCheckBoxWithImageWindow.FilterStatusAll)

                    {

                        ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");

                        data = "All";

                        ButtonApplyClick?.Invoke(data);

                        return;

                    }



                    if (flexGridCheckBoxWithImageWindow.FilterStatusNone)

                    {

                        ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");

                        data = "";

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

                        ButtonApplyClick?.Invoke(data);

                        return;

                    }



                    if (flexGridCheckBoxWindow.FilterStatusNone)

                    {

                        ButtonFilter.Image.Source = ImageHelper.GenerateImage("IconFilter.png");

                        data = "";

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

                ButtonApplyClick?.Invoke(data);

            }

        }
    }
}
