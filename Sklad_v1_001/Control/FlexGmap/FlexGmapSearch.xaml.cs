using GMap.NET;
using GMap.NET.MapProviders;
using Newtonsoft.Json;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal.GMapIntegration.OpenStreetMap;
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

namespace Sklad_v1_001.Control.FlexGmap
{
    /// <summary>
    /// Логика взаимодействия для FlexGmapSearch.xaml
    /// </summary>
    public partial class FlexGmapSearch : UserControl
    {
        public Double Lat { get; set; }
        public Double Lng { get; set; }

        public String startPosition { get; set; }
        public String lastUrl { get; set; }
        public List<Row> rows { get; set; }
        public FlexGmapSearch()
        {
            InitializeComponent();
            startPosition = Attributes.globalData.gMap.openStreet.urlStartPosition;
            lastUrl = Attributes.globalData.gMap.openStreet.lastRow;
        }

        private void search_ButtonSearch()
        {

        }

        private void search_ButtonSelectClick(long text)
        {

        }

        private void gmaps_Loaded(object sender, RoutedEventArgs e)
        {
            GetRequest(startPosition);

            gmaps.Bearing = 0;
            gmaps.CanDragMap = true;
            gmaps.DragButton = MouseButton.Left;
            gmaps.MaxZoom = 18;
            gmaps.MinZoom = 2;
            gmaps.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gmaps.ShowTileGridLines = false;
            gmaps.Zoom = 0;
            gmaps.ShowCenter = false;
            gmaps.MapProvider = GMapProviders.WikiMapiaMap;//
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmaps.Position = new GMap.NET.PointLatLng(Lat, Lng);

            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
        }

        private void GetRequest(String URL)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    webClient.Encoding = System.Text.Encoding.UTF8;
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                                          "Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    String temp = URL + lastUrl;
                    string response = webClient.DownloadString(new Uri(URL + lastUrl));

                    rows = JsonConvert.DeserializeObject<List<Row>>(response);

                    Lat = rows[0].lat;
                    Lng = rows[0].lon;
                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
