using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sklad_v1_001.Control.FlexComboBox
{
    /// <summary>
    /// Логика взаимодействия для ComboBoxAddEditToWithLabel.xaml
    /// </summary>
    public partial class ComboBoxWithlabelToAddEdit : UserControl, INotifyPropertyChanged
    {

        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(ComboBoxWithlabelToAddEdit), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 Value
        {
            get
            {
                return (Int32)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
                OnPropertyChanged("Value");
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(ComboBoxWithlabelToAddEdit), new UIPropertyMetadata(Visibility.Collapsed));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsRequired
        {
            get
            {
                return (Visibility)GetValue(IsRequiredProperty);
            }
            set
            {
                SetValue(IsRequiredProperty, value);
                OnPropertyChanged("IsRequired");
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
                        "Item",
                        typeof(object),
                        typeof(ComboBoxWithlabelToAddEdit));
        //Item

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public object Item
        {
            get
            {
                return (object)GetValue(ItemProperty);
            }
            set
            {
                SetValue(ItemProperty, value);
                OnPropertyChanged("Item");
            }
        }


        // свойство зависимостей
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
                        "Description",
                        typeof(String),
                        typeof(ComboBoxWithlabelToAddEdit), new UIPropertyMetadata("Description"));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String Description
        {
            get
            {
                return (String)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
                OnPropertyChanged("Description");
            }
        }


        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
                        "SourceAdd",
                        typeof(ImageSource),
                       typeof(ComboBoxWithlabelToAddEdit));
        //SourceLoop
        public static readonly DependencyProperty SourceLoopProperty = DependencyProperty.Register(
                        "SourceEdit",
                        typeof(ImageSource),
                       typeof(ComboBoxWithlabelToAddEdit));

        public static readonly DependencyProperty VisibilityEditProperty = DependencyProperty.Register(
                    "VisibilityEdit",
                    typeof(Visibility),
                   typeof(ComboBoxWithlabelToAddEdit), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty VisibilityAddProperty = DependencyProperty.Register(
                     "VisibilityAdd",
                     typeof(Visibility),
                    typeof(ComboBoxWithlabelToAddEdit), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsEnableEditProperty = DependencyProperty.Register(
                      "IsEnableEdit",
                      typeof(Boolean),
                     typeof(ComboBoxWithlabelToAddEdit), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
                     "IsEnableAdd",
                     typeof(Boolean),
                    typeof(ComboBoxWithlabelToAddEdit), new PropertyMetadata(true));

        public ImageSource SourceAdd
        {
            get
            {
                return (ImageSource)this.buttonAdd.Image.Source;
            }
            set
            {
                this.buttonAdd.Image.Source = value as ImageSource;
                OnPropertyChanged("SourceAdd");
            }
        }

        public ImageSource SourceLoop
        {
            get
            {
                return (ImageSource)this.buttonEdit.Image.Source;
            }
            set
            {
                this.buttonEdit.Image.Source = value as ImageSource;
                OnPropertyChanged("SourceLoop");
            }
        }

        public Visibility VisibilityEdit
        {
            get { return (Visibility)GetValue(VisibilityEditProperty); }
            set { SetValue(VisibilityEditProperty, value); }
        }
        //VisibilityAdd
        public Visibility VisibilityAdd
        {
            get { return (Visibility)GetValue(VisibilityAddProperty); }
            set { SetValue(VisibilityAddProperty, value); }
        }

        public Boolean IsEnableEdit
        {
            get { return (Boolean)GetValue(IsEnableEditProperty); }
            set { SetValue(IsEnableEditProperty, value); }
        }
        //VisibilityAdd
        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string LabelText
        {
            get
            {
                return this.label.Content.ToString();
            }

            set
            {
                this.label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return this.wrapPanel.Width;
            }

            set
            {
                this.wrapPanel.Width = value;
            }
        }

        public event Action ButtonAdd;
        public event Action ButtonEdit;

        public event Action SelectTextInput;
        public event Action DropDownClosed;
        public delegate void SelectionChangedHandler(object sender = null, RoutedEventArgs e = null);
        public event SelectionChangedHandler SelectionChanged;

        public ComboBoxWithlabelToAddEdit()
        {
            InitializeComponent();
            this.buttonAdd.Image.Source = ImageHelper.GenerateImage("IconAddProduct.png");
            this.buttonEdit.Image.Source = ImageHelper.GenerateImage("IconEdit_x16.png");
        }

        private void buttonAdd_ButtonClick()
        {
            ButtonAdd?.Invoke();
        }
        private void buttonEdit_ButtonClick()
        {
            ButtonEdit?.Invoke();
        }

        private void ComboBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanged?.Invoke(sender, e);
        }

        private void ComboBoxElement_TextInput(object sender, TextCompositionEventArgs e)
        {
            SelectTextInput?.Invoke();
        }
        private void ComboBoxElement_DropDownClosed(object sender, EventArgs e)
        {
            DropDownClosed?.Invoke();
        }

       
    }
}
