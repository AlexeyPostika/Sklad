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

namespace Sklad_v1_001.Control.FlexMenu
{
    /// <summary>
    /// Логика взаимодействия для frameMenuPage.xaml
    /// </summary>
    public partial class frameMenuPage : Page
    {
        public event Action ButtonProductOpen;
        public event Action ButtonProductEditOpen;
        public event Action ButtonSaleDocumentOpen;
        public event Action ButtonTransferDocumentOpen;
        public event Action ButtonDeliveryOpen;
        public event Action ButtonSettingsOpen;
        public event Action ButtonExiteOpen;
        public frameMenuPage()
        {
            InitializeComponent();
        }

        private void FrameMenu_ButtonProductOpen()
        {
            ButtonProductOpen?.Invoke();
        }

        private void FrameMenu_ButtonProductEditOpen()
        {
            ButtonProductEditOpen?.Invoke();
        }

        private void FrameMenu_ButtonSaleDocumentOpen()
        {
            ButtonSaleDocumentOpen?.Invoke();
        }

        private void FrameMenu_ButtonTransferDocumentOpen()
        {
            ButtonTransferDocumentOpen?.Invoke();
        }

        private void FrameMenu_ButtonDeliveryOpen()
        {
            ButtonDeliveryOpen?.Invoke();
        }

        private void FrameMenu_ButtonSettingsOpen()
        {
            ButtonSettingsOpen?.Invoke();
        }

        private void FrameMenu_ButtonExiteOpen()
        {
            ButtonExiteOpen?.Invoke();
        }        
    }
}
