using Aspose.Pdf.Drawing;
using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using GMap.NET;
using GMap.NET.MapProviders;
using Sklad_v1_001.GlobalVariable;

namespace Sklad_v1_001.FormUsers.Shops
{
    /// <summary>
    /// Логика взаимодействия для NewShopsItem.xaml
    /// </summary>   
    public partial class NewShopsItem : Page
    {
        Attributes attributes;


        //объект Shop
        LocaleRow newShopRow;
        LocaleFilter filter;
        ShopsLogic shopsLogic;
       
        public LocaleRow NewShopRow
        {
            get
            {
                return newShopRow;
            }

            set
            {
                newShopRow = value;
            }
        }
        public LocaleFilter Filter
        {
            get
            {
                return filter;
            }

            set
            {
                filter = value;
            }
        }
        public NewShopsItem(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;

            shopsLogic = new ShopsLogic(attributes);

            NewShopRow = new LocaleRow();
            Filter = new LocaleFilter();

            this.DataContext = NewShopRow;
        }

        #region ToolBar
        private void toolbar_ButtonSave()
        {
            Save(NewShopRow);
        }

        private void toolbar_ButtonSaveclose()
        {
            if (Save(NewShopRow) > 0)
                MainWindow.AppWindow.ButtonListShopsOpenF(Filter);
        }

        private void toolbar_ButtonListCancel()
        {

        }

        private void toolbar_ButtonRgister()
        {

        }

        private void toolbar_ButtonCancel()
        {

        }
        #endregion

        #region Save

        private Int32 Save(LocaleRow localeRow)
        {
            NewShopRow.Country = gmap.Сountry;
            NewShopRow.City = gmap.City;
            NewShopRow.Administrative = gmap.Administrative;
            NewShopRow.Street = gmap.Street;
            NewShopRow.Housenumber = gmap.Housenumber;
            NewShopRow.PostCode = gmap.PostCode;
            NewShopRow.Address = gmap.Text;
            NewShopRow.Lat = gmap.Lat;
            NewShopRow.Lng = gmap.Lng;
            Int32 tempID =  shopsLogic.SaveRow(localeRow);

            return tempID;
        }

        #endregion
    }
}
