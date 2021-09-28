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
    /// Логика взаимодействия для EditBoxDelete.xaml
    /// </summary>
    public partial class EditBoxDeleteWithLabel : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(EditBoxDeleteWithLabel), new UIPropertyMetadata(String.Empty));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }


        public EditBoxDeleteWithLabel()
        {
            InitializeComponent();
        }

        public event Action ButtonClearClick;
        public event Action ButtonTextChangedClick;

        public Boolean IsReadOnly
        {
            get
            {
                return this.EditBoxDelete.TextField.IsReadOnly;
            }

            set
            {
                this.EditBoxDelete.TextField.IsReadOnly = value;
            }
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
                return this.label.Width;
            }

            set
            {
                this.label.Width = value;
            }
        }

        private void EditBoxDelete_ButtonClearClick()
        {
            ButtonClearClick?.Invoke();
        }

        private void EditBoxDelete_ButtonTextChangedClick()
        {
            ButtonTextChangedClick?.Invoke();
        }
    }
}
