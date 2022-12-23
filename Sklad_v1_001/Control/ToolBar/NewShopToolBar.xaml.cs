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
    public partial class NewShopToolBar : UserControl
    {
        public static readonly DependencyProperty IsEnableApplyProperty = DependencyProperty.Register(
           "IsEnableApply",
           typeof(Boolean),
           typeof(NewShopToolBar), new UIPropertyMetadata(true));
        
        public static readonly DependencyProperty VisibilityApplyProperty = DependencyProperty.Register(
          "VisibilityApply",
          typeof(Visibility),
          typeof(NewShopToolBar), new UIPropertyMetadata(Visibility.Visible));

        public Boolean IsEnableApply
        {
            get { return (Boolean)GetValue(IsEnableApplyProperty); }
            set { SetValue(IsEnableApplyProperty, value); }
        }
        public Visibility VisibilityApply
        {
            get { return (Visibility)GetValue(VisibilityApplyProperty); }
            set { SetValue(VisibilityApplyProperty, value); }
        }


        public event Action ButtonSave;
        public event Action ButtonSaveclose;
        public event Action ButtonListCancel;
        public event Action ButtonCancel;
        public event Action ButtonRgister;
        public event Action ButtonAdjustment;
        public event Action ButtonApply;
        public NewShopToolBar()
        {
            InitializeComponent();
            ButtonSaveb.Image.Source = ImageHelper.GenerateImage("IconSave_x24.png");
            ButtonSaveClose.Image.Source = ImageHelper.GenerateImage("IconSaveClose_x24.png");
            ButtonListcansel.Image.Source = ImageHelper.GenerateImage("IconToList_x24.png");
            BottonApplyb.Image.Source = ImageHelper.GenerateImage("IconOK_x24.png");
            ButtonCancelSend.Image.Source = ImageHelper.GenerateImage("IconClose_X24.png");
            ButtonRegistrate.Image.Source = ImageHelper.GenerateImage("IconRegisterDocument_X24.png");//
            //Buttonadjustment.Image.Source = ImageHelper.GenerateImage("IconRepiterDocument_X24.png");//IconDetails_X32
        }

        private void ButtonSave_ButtonClick()
        {
            ButtonSave?.Invoke();
        }

        private void ButtonSaveClose_ButtonClick()
        {
            ButtonSaveclose?.Invoke();
        }

        private void ButtonListcansel_ButtonClick()
        {
            ButtonListCancel?.Invoke();
        }

        private void BottonApply_ButtonClick()
        {
            ButtonApply?.Invoke();
        }

        private void ButtonRegistrate_ButtonClick()
        {
            ButtonRgister?.Invoke();
        }

        private void ButtonCancelSend_ButtonClick()
        {
            ButtonCancel?.Invoke();
        }

        private void ButtonAdjustment_ButtonClick()
        {
            ButtonAdjustment?.Invoke();
        }
    }
}
