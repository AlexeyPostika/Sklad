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
    public partial class ToolBarProductEdit : UserControl
    {
        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
           "IsEnableAdd",
           typeof(Boolean),
           typeof(ToolBarProductEdit), new UIPropertyMetadata(true));

        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }

        public event Action ButtonSaveClose;
        public event Action ButtonSave;
        public event Action ButtonListDocument;
        public event Action ButtonApply;
        public ToolBarProductEdit()
        {
            InitializeComponent();
            SaveButton.Image.Source = ImageHelper.GenerateImage("save_32px.png");
            SaveCloseButton.Image.Source = ImageHelper.GenerateImage("IconSaveClose.png");
            ListDocumentButton.Image.Source = ImageHelper.GenerateImage("IconArchiveListOfParts.png");
            ApplyButton.Image.Source = ImageHelper.GenerateImage("IconOk.png");           
        }

        private void SaveButton_Click()
        {
            ButtonSave?.Invoke();
        }

        private void SaveCloseButton_ButtonClick()
        {
            ButtonSaveClose?.Invoke();
        }

        private void ListDocumentButton_ButtonClick()
        {
            ButtonListDocument?.Invoke();
        }

        private void ApplyButton_ButtonClick()
        {
            ButtonApply?.Invoke();
        }
    }
}
