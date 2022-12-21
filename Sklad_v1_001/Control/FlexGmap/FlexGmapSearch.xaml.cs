using GMap.NET;
using GMap.NET.MapProviders;
using Newtonsoft.Json;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
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
    public static class Extensions
    {
        public static List<RowListView> ConvertToListView(this List<Row> list)
        {
            List<RowListView> listOUT = new List<RowListView>();
            foreach(Row row in list)
            {
                RowListView local = new RowListView();
                local.ID = row.osm_id;
                local.Description = row.display_name;
                local.Icon = row.icon;
                listOUT.Add(local);
            }
            return listOUT;
        }
    }
    /// <summary>
    /// Логика взаимодействия для FlexGmapSearch.xaml
    /// </summary>
    public partial class FlexGmapSearch : UserControl
    {
        //Text
        //SearchText
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(FlexGmapSearch), new UIPropertyMetadata(String.Empty));
        //Text
        // свойство зависимостей
        public static readonly DependencyProperty TextSearchProperty = DependencyProperty.Register(
                        "TextSearch",
                        typeof(string),
                        typeof(FlexGmapSearch), new UIPropertyMetadata(String.Empty));
        //
        // свойство зависимостей
        public static readonly DependencyProperty IsOpenPopupProperty = DependencyProperty.Register(
                        "IsOpenPopup",
                        typeof(Boolean),
                        typeof(FlexGmapSearch), new UIPropertyMetadata(false));

        // свойство зависимостей
        public static readonly DependencyProperty ListDataProperty = DependencyProperty.Register(
                        "ListData",
                        typeof(List<RowListView>),
                        typeof(FlexGmapSearch), new UIPropertyMetadata(new List<RowListView>()));

        // Обычное свойство .NET  - обертка над свойством зависимостей     
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей  
        public string TextSearch
        {
            get { return (string)GetValue(TextSearchProperty); }
            set { SetValue(TextSearchProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей     
        public Boolean IsOpenPopup
        {
            get { return (Boolean)GetValue(IsOpenPopupProperty); }
            set { SetValue(IsOpenPopupProperty, value); }
        }
        public List<RowListView> ListData
        {
            get { return (List<RowListView>)GetValue(ListDataProperty); }
            set { SetValue(ListDataProperty, value); }
        }

        public Double Lat { get; set; }
        public Double Lng { get; set; }

        public String startPosition { get; set; }
        public String url { get; set; }
        public String lastUrl { get; set; }
        public List<Row> rows { get; set; }
        public FlexGmapSearch()
        {
            InitializeComponent();
            url= Attributes.globalData.gMap.openStreet.url;
            startPosition = Attributes.globalData.gMap.openStreet.urlStartPosition;
            lastUrl = Attributes.globalData.gMap.openStreet.lastRow;
            rows = new List<Row>();
        }

        private void search_ButtonSearch()
        {
            if (!String.IsNullOrEmpty(TextSearch))
                GetRequest(url, TextSearch);

            if (rows.Count() > 1)
            {
                IsOpenPopup = true;
            }
        }

        private void search_ButtonSelectClick(long text)
        {
            var tempRow = rows.FirstOrDefault(x => x.osm_id == text);
            if (tempRow != null)
            {
                GMap.NET.WindowsPresentation.GMapMarker marker = new GMap.NET.WindowsPresentation.GMapMarker(new PointLatLng(tempRow.lat, tempRow.lon));
                marker.Offset = new Point(-10, -20);
                Image image = new Image();
                image.Source = tempRow.icon!=null? tempRow.icon:ImageHelper.GenerateImage("IconGMap_X24.png");
                marker.Shape = image;
                gmaps.Markers.Add(marker);
                gmaps.Zoom = 25;
                gmaps.Position = new GMap.NET.PointLatLng(tempRow.lat, tempRow.lon);
                //Ox.Text = marker.Position.Lat.ToString();
                //Oy.Text = marker.Position.Lng.ToString();
                GetDetails(tempRow.osm_id);
            }
           
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

        private void GetRequest(String URL, String search = null)
        {
            rows.Clear();
            ListData.Clear();
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
                    String temp = URL +search+ lastUrl;
                    string response = webClient.DownloadString(new Uri(temp));

                    rows = JsonConvert.DeserializeObject<List<Row>>(response);
                    ListData = rows.ConvertToListView();
                    Lat = rows[0].lat;
                    Lng = rows[0].lon;
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void GetDetails(Int64 osmid)
        {
            //https://nominatim.openstreetmap.org/details.php?osmtype=W&osmid=129704752&class=building&addressdetails=1&hierarchy=0&group_hierarchy=1&format=json
            String pathDetails = @"https://nominatim.openstreetmap.org/details.php?osmtype=W&osmid=" + osmid + @"&class=building&addressdetails=1&hierarchy=0&group_hierarchy=1&format=json";

            try
            {
                using (var webClient = new WebClient())
                {
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    //webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                              "Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    string response = webClient.DownloadString(new Uri(pathDetails));
                    var temp = rows.FirstOrDefault(x => x.osm_id == osmid);
                    if (temp != null)
                        temp.rowDetails = JsonConvert.DeserializeObject<RowDetails>(response);
                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
