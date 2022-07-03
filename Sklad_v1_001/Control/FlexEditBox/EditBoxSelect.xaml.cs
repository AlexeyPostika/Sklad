using POS.GlobalVariable;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace POS.FlexControls.FlexEditBox
{
    /// <summary>
    /// Логика взаимодействия для EditBoxDelete.xaml
    /// </summary>
    public partial class EditBoxSelect : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(EditBoxSelect), new UIPropertyMetadata(String.Empty));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public EditBoxSelect()
        {
            InitializeComponent();
            image.Source = ImageHelper.GenerateImage("IconSelect.png");
        }

        public event Action ButtonSelectClick;
        public event Action ButtonLostFocusClick;

        public Boolean IsReadOnly
        {
            get
            {
                return TextField.IsReadOnly;
            }

            set
            {
                TextField.IsReadOnly = value;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!TextField.IsReadOnly)
                ButtonSelectClick?.Invoke();
        }

        private void TextField_LostFocus(object sender, RoutedEventArgs e)
        {
            ButtonLostFocusClick?.Invoke();
        }

        private void TextField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                ButtonSelectClick?.Invoke();
            }
        }

        private void TextField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text == Properties.Resources.BadData)
            {
                TextField.BorderBrush = Brushes.Red;
                TextField.Opacity = 0.5;
            }
            else
            {
                TextField.Opacity = 1;
                TextField.Style = (Style)MainWindow.AppWindow.TryFindResource("TextBoxStyle");
                SetterBaseCollection test = TextField.Style.Setters;
                foreach (Setter test1 in test)
                {
                    if (test1.Property.ToString() == "BorderBrush")
                        TextField.BorderBrush = (Brush)test1.Value;

                }
            }
        }

        private void TextField_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Text == Properties.Resources.BadData)
            {
                Text = "";
            }
        }
    }
}
