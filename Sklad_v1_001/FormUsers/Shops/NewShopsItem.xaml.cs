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

namespace Sklad_v1_001.FormUsers.Shops
{
    /// <summary>
    /// Логика взаимодействия для NewShopsItem.xaml
    /// </summary>   
    public partial class NewShopsItem : Page
    {
        Attributes attributes;
        public NewShopsItem(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;
        }

        private void checkBoxShop_ButtonCheckedClick(object sender, RoutedEventArgs e)
        {

        }

        private void checkBoxShop_ButtonUnCheckedClick(object sender, RoutedEventArgs e)
        {

        }

        private void gmaps_Loaded(object sender, RoutedEventArgs e)
        {
            gmaps.Bearing = 0;
            gmaps.CanDragMap = true;
            gmaps.DragButton = MouseButton.Left;
            gmaps.MaxZoom = 18;
            gmaps.MinZoom = 2;
            gmaps.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gmaps.ShowTileGridLines = false;
            gmaps.Zoom = 10;
            gmaps.ShowCenter = false;
            gmaps.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmaps.Position = new GMap.NET.PointLatLng(47.2229, 38.9095);

            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
        }
    }
}
