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

namespace Sklad_v1_001.Control.FlexEditBox
{
    /// <summary>
    /// Логика взаимодействия для EditBoxUser.xaml
    /// </summary>
    public partial class EditBoxUser : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(EditBoxUser), new UIPropertyMetadata(String.Empty));

        //VisibilityImage
        // свойство зависимостей
        public static readonly DependencyProperty VisibilityImageProperty = DependencyProperty.Register(
                        "VisibilityImage",
                        typeof(Visibility),
                        typeof(EditBoxUser), new UIPropertyMetadata(Visibility.Visible));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility VisibilityImage
        {
            get { return (Visibility)GetValue(VisibilityImageProperty); }
            set { SetValue(VisibilityImageProperty, value); }
        }

        public EditBoxUser()
        {
            InitializeComponent();
            image.Source = ImageHelper.GenerateImage("IconCategory.png");
        }

        public event Action ButtonClearClick;
        public event Action ButtonTextChangedClick;

        public Boolean IsReadOnly
        {
            get
            {
                return this.TextField.IsReadOnly;
            }

            set
            {
                this.TextField.IsReadOnly = value;
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClearClick?.Invoke();
        }

        private void TextField_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonTextChangedClick?.Invoke();
        }
    }
}
