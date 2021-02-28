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
using Sklad_v1_001.Control.FlexMenu;
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
        frameMenuPage _pageframeMenuLevel1;
        Tovar.TovarZona tovarZona;
        public string ViewModel { get; set; }
        public frameMenuPage PageframeMenuLevel1
        {
            get
            {
                return _pageframeMenuLevel1;
            }

            set
            {
                _pageframeMenuLevel1 = value;
            }
        }

        public WorkZona()
        {
            InitializeComponent();

            PageframeMenuLevel1 = new frameMenuPage();
            this.frameMenuOpen.Navigate(PageframeMenuLevel1);

            //продукты
            PageframeMenuLevel1.ButtonProductOpen += new Action(ButtonProductOpen);
            PageframeMenuLevel1.ButtonProductEditOpen += new Action(ButtonProductEditOpen);
            //продажи     
            PageframeMenuLevel1.ButtonSaleDocumentOpen += new Action(ButtonSaleDocumentOpen);     
            //перемещение
            PageframeMenuLevel1.ButtonTransferDocumentOpen += new Action(ButtonTransferDocumentOpen);
            //поставки
            PageframeMenuLevel1.ButtonDeliveryOpen += new Action(ButtonDeliveryOpen);
            //настройки                
            PageframeMenuLevel1.ButtonSettingsOpen += new Action(ButtonSettingsOpen);
            //выход
            PageframeMenuLevel1.ButtonExiteOpen += new Action(ButtonExiteOpen);

        }

        #region Product
        public void ButtonProductOpen()
        {
            Docker1.Navigate(new TovarZona()); // открытие страницы
        }

        public void ButtonProductEditOpen()
        {
            MessageBox.Show("пойдем гулять");
        }
        #endregion

        #region Продажи
        private void ButtonSaleDocumentOpen()
        {
            
        }
        #endregion

        #region перемещение
        private void ButtonTransferDocumentOpen()
        {
            
        }
        #endregion

        #region поставки
        private void ButtonDeliveryOpen()
        {
            Docker1.Navigate(new Zacupca.ZacupcaGrid()); // открытие страницы
        }
        #endregion

        #region настройки
        private void ButtonSettingsOpen()
        {
            
        }
        #endregion

        #region выход
        private void ButtonExiteOpen()
        {
            
        }
        #endregion
    }
}
