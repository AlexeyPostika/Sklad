using GMap.NET;
using GMap.NET.MapProviders;
using Newtonsoft.Json;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal.GMapIntegration.OpenStreetMap;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        // свойство зависимостей
        public static readonly DependencyProperty GeopositionProperty = DependencyProperty.Register(
                        "Geoposition",
                        typeof(String),
                        typeof(FlexGmapSearch), new UIPropertyMetadata("0:0"));       
        // свойство зависимостей
        public static readonly DependencyProperty LatProperty = DependencyProperty.Register(
                        "Lat",
                        typeof(Double),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
        // свойство зависимостей
        public static readonly DependencyProperty LngProperty = DependencyProperty.Register(
                        "Lng",
                        typeof(Double),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
        // свойство зависимостей
        public static readonly DependencyProperty СountryProperty = DependencyProperty.Register(
                        "Сountry",
                        typeof(String),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
        // свойство зависимостей
        public static readonly DependencyProperty CityProperty = DependencyProperty.Register(
                        "City",
                        typeof(String),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
        // свойство зависимостей
        public static readonly DependencyProperty AdministrativeProperty = DependencyProperty.Register(
                        "Administrative",
                        typeof(String),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
        // свойство зависимостей
        public static readonly DependencyProperty StreetProperty = DependencyProperty.Register(
                        "Street",
                        typeof(String),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
        // свойство зависимостей
        public static readonly DependencyProperty HousenumberProperty = DependencyProperty.Register(
                        "Housenumber",
                        typeof(String),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
        // свойство зависимостей
        public static readonly DependencyProperty PostCodeProperty = DependencyProperty.Register(
                        "PostCode",
                        typeof(Int32),
                        typeof(FlexGmapSearch), new UIPropertyMetadata());
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
        //Geoposition
        public String Geoposition
        {
            get { return (String)GetValue(GeopositionProperty); }
            set { SetValue(GeopositionProperty, value); }
        }
        public Double Lat
        {
            get { return (Double)GetValue(LatProperty); }
            set { SetValue(LatProperty, value); }
        }
        
        public Double Lng
        {
            get { return (Double)GetValue(LngProperty); }
            set { SetValue(LngProperty, value); }
        }
        //
        public String Сountry
        {
            get { return (String)GetValue(СountryProperty); }
            set { SetValue(СountryProperty, value); }
        }
        //
        public String City
        {
            get { return (String)GetValue(CityProperty); }
            set { SetValue(CityProperty, value); }
        }
        //
        public String Administrative
        {
            get { return (String)GetValue(AdministrativeProperty); }
            set { SetValue(AdministrativeProperty, value); }
        }
        //
        public String Street
        {
            get { return (String)GetValue(StreetProperty); }
            set { SetValue(StreetProperty, value); }
        }
        //Housenumber
        public String Housenumber
        {
            get { return (String)GetValue(HousenumberProperty); }
            set { SetValue(HousenumberProperty, value); }
        }
        
        public Int32 PostCode
        {
            get { return (Int32)GetValue(PostCodeProperty); }
            set { SetValue(PostCodeProperty, value); }
        }

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
            if (!String.IsNullOrEmpty(TextSearch) && TextSearch.ToArray().Last().ToString()=="+")
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
                image.Source = ImageHelper.GenerateImage("IconGMap_X24.png");
                marker.Shape = image;
                gmaps.Markers.Add(marker);
                gmaps.Zoom = 25;
                gmaps.Position = new GMap.NET.PointLatLng(tempRow.lat, tempRow.lon);
                //Ox.Text = marker.Position.Lat.ToString();
                //Oy.Text = marker.Position.Lng.ToString();
                GetDetails(tempRow);
                Text = tempRow.display_name;
                IsOpenPopup = false;
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
            gmaps.MapProvider = GMapProviders.GoogleMap;//
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmaps.Position = new GMap.NET.PointLatLng(Lat, Lng);

            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;
        }

        private void GetRequest(String URL, String search = null)
        {
            rows.Clear();
            ListData.Clear();
            gmaps.Markers.Clear();
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
                    Geoposition = Lat + " : " + Lng;
                    Сountry = country.Text;
                    City = city.Text;
                    Administrative = administrative.Text;
                    Street = street.Text;
                    Housenumber = housenumber.Text;
                    PostCode = Convert.ToInt32(postcode.Text);
                    //this.DataContext = rows[0];
                }
            }
            catch (Exception ex)
            {


            }
        }
        private void GetDetails(Row row)
        {
            //https://nominatim.openstreetmap.org/details.php?osmtype=W&osmid=129704752&class=building&addressdetails=1&hierarchy=0&group_hierarchy=1&format=json
            String pathDetails = @"https://nominatim.openstreetmap.org/details.php";
            //osmtype=W&osmid=" + osmid + @"&class=building&addressdetails=1&hierarchy=0&group_hierarchy=1&format=json";

            try
            {
                using (var webClient = new WebClient())
                {
                    pathDetails += "?osmtype=" + row.osm_type.ToArray()[0].ToString().ToUpper() + "&osmid=" + row.osm_id.ToString() + "&class=" + row.category + "&addressdetails=1&hierarchy=0&group_hierarchy=1&format=json";
                    // Выполняем запрос по адресу и получаем ответ в виде строки
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    //webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                              "Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    string response = webClient.DownloadString(new Uri(pathDetails));
                    var temp = rows.FirstOrDefault(x => x.osm_id == row.osm_id);
                    if (temp != null)
                    {
                        temp.rowDetails = JsonConvert.DeserializeObject<RowDetails>(response, new JsonSerializerSettings()
                        {
                            Error = (sender, error) =>
                            {
                                //error.Add(error.ErrorContext.Error.Message);
                                error.ErrorContext.Handled = true;
                            }
                        });
                        this.DataContext = rows.FirstOrDefault(x=>x.osm_id==temp.osm_id);
                    }
                    Сountry = country.Text;
                    City = city.Text;
                    Administrative = administrative.Text;
                    Street = street.Text;
                    Housenumber = housenumber.Text;
                    PostCode = Convert.ToInt32(postcode.Text);
                }
            }
            catch (Exception ex)
            {


            }
           
        }
    }
}
