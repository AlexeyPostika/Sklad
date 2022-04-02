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

namespace Sklad_v1_001.Control.Contener
{
    /// <summary>
    /// Логика взаимодействия для ContenerRowDescription.xaml
    /// </summary>
    public partial class ContenerRowDescription : UserControl
    {

        public static readonly DependencyProperty PhotoImageProperty = DependencyProperty.Register(
                        "PhotoImage",
                        typeof(ImageSource),
                       typeof(ContenerRowDescription));
        // свойство зависимостей
        public static readonly DependencyProperty TextValue1Property = DependencyProperty.Register(
                        "TextValue1",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextValue2Property = DependencyProperty.Register(
                        "TextValue2",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextEdit1Property = DependencyProperty.Register(
                        "TextEdit1",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextEdit2Property = DependencyProperty.Register(
                        "TextEdit2",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextEdit3Property = DependencyProperty.Register(
                        "TextEdit3",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextEdit4Property = DependencyProperty.Register(
                        "TextEdit4",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextEdit5Property = DependencyProperty.Register(
                        "TextEdit5",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextEdit6Property = DependencyProperty.Register(
                        "TextEdit6",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        // свойство зависимостей
        public static readonly DependencyProperty TextEdit7Property = DependencyProperty.Register(
                        "TextEdit7",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));       
        // свойство зависимостей
        public static readonly DependencyProperty TextCount1Property = DependencyProperty.Register(
                        "TextCount1",
                        typeof(String),
                        typeof(ContenerRowDescription), new UIPropertyMetadata(""));
        public ImageSource PhotoImage
        {
            get { return (ImageSource)GetValue(PhotoImageProperty); }
            set { SetValue(PhotoImageProperty, value); }          
        }

        public String TextValue1
        {
            get { return (String)GetValue(TextValue1Property); }
            set { SetValue(TextValue1Property, value); }
        }
        public String TextValue2
        {
            get { return (String)GetValue(TextValue2Property); }
            set { SetValue(TextValue2Property, value); }
        }
        public String TextEdit1
        {
            get { return (String)GetValue(TextEdit1Property); }
            set { SetValue(TextEdit1Property, value); }
        }
        public String TextEdit2
        {
            get { return (String)GetValue(TextEdit2Property); }
            set { SetValue(TextEdit2Property, value); }
        }
        public String TextEdit3
        {
            get { return (String)GetValue(TextEdit3Property); }
            set { SetValue(TextEdit3Property, value); }
        }
        public String TextEdit4
        {
            get { return (String)GetValue(TextEdit4Property); }
            set { SetValue(TextEdit4Property, value); }
        }
        public String TextEdit5
        {
            get { return (String)GetValue(TextEdit5Property); }
            set { SetValue(TextEdit5Property, value); }
        }
        public String TextEdit6
        {
            get { return (String)GetValue(TextEdit6Property); }
            set { SetValue(TextEdit6Property, value); }
        }
        public String TextEdit7
        {
            get { return (String)GetValue(TextEdit7Property); }
            set { SetValue(TextEdit7Property, value); }
        }
        public String TextCount1
        {
            get { return (String)GetValue(TextCount1Property); }
            set { SetValue(TextCount1Property, value); }
        }

        public event Action ButtonAddClick;

        public ContenerRowDescription()
        {
            InitializeComponent();
            ButtonAdd.Image.Source = ImageHelper.GenerateImage("IconBasket_X24.png");
            ButtonAdd.Text = Properties.Resources.TOBASKET;
        }

        private void ButtonAdd_ButtonClick()
        {
            ButtonAddClick?.Invoke();
        }
    }
}
