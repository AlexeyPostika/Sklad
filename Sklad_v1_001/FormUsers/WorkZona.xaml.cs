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
using Sklad_v1_001.FormUsers.Kategor;
using Sklad_v1_001.FormUsers.Prixod;
using Sklad_v1_001.FormUsers.Tovar;

namespace Sklad_v1_001.FormUsers
{
    /// <summary>
    /// Interaction logic for WorkZona.xaml
    /// </summary>
    /// </summary>
    /// </summary>  
    public partial class WorkZona : UserControl
    {
        public string ViewModel { get; set; }
        public WorkZona()
        {
            InitializeComponent();       
        }
        public void ShowViewModel()
        {
            MessageBox.Show(ViewModel);
        }

        private void Docker1_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void FrameMenu_ButtonProductOpen()
        {
            Docker1.Navigate(new TovarZona()); // открытие страницы
        }

        private void FrameMenu_ButtonSaleDocumentOpen()
        {

        }

        private void FrameMenu_ButtonTransferDocumentOpen()
        {

        }

        private void FrameMenu_ButtonDeliveryOpen()
        {
            Docker1.Navigate(new Zacupca.ZacupcaGrid()); // открытие страницы
        }

        private void FrameMenu_ButtonSettingsOpen()
        {

        }

        private void FrameMenu_ButtonExiteOpen()
        {

        }
    }
}
