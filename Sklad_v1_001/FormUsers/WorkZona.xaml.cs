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

            tovarZona = new TovarZona();

            PageframeMenuLevel1 = new frameMenuPage();
            this.frameMenuOpen.Navigate(PageframeMenuLevel1);


            PageframeMenuLevel1.ButtonProductOpen += new Action(ButtonProductOpen);
            PageframeMenuLevel1.ButtonSaleDocumentOpen += new Action(ButtonSaleDocumentOpen);
            //ButtonPlanFactClick
            //Аналитика 
            PageframeMenuLevel1.ButtonTransferDocumentOpen += new Action(ButtonTransferDocumentOpen);
            PageframeMenuLevel1.ButtonDeliveryOpen += new Action(ButtonDeliveryOpen);
            //продажи           
            PageframeMenuLevel1.ButtonSettingsOpen += new Action(ButtonSettingsOpen);
            PageframeMenuLevel1.ButtonExiteOpen += new Action(ButtonExiteOpen);

        }
        #region Product
        private void ButtonProductOpen()
        {
            Docker1.Navigate(new TovarZona()); // открытие страницы
        }
        #endregion

        #region Продажи
        private void ButtonSaleDocumentOpen()
        {
            
        }
        #endregion


        private void ButtonTransferDocumentOpen()
        {
            
        }

        private void ButtonDeliveryOpen()
        {
            Docker1.Navigate(new Zacupca.ZacupcaGrid()); // открытие страницы
        }


        public void ShowViewModel()
        {
            MessageBox.Show(ViewModel);
        }  

        private void ButtonSettingsOpen()
        {
            
        }

        private void ButtonExiteOpen()
        {
            
        }

    }
}
