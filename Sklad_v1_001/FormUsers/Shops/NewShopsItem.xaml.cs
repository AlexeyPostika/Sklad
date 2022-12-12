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
            //Настройки для компонента GMAP
            gmaps.Bearing = 0;
            //Перетаскивание левой кнопки мыши
            gmaps.CanDragMap = true;
            //перетаскивание карты левой кнопкой мыши
            gmaps.DragButton = MouseButton.Left;
            // макисмальное приближение
            gmaps.MaxZoom = 18;
            //Минимальное приближение
            gmaps.MinZoom = 2;
            //курсор мыши в центре карты
            gmaps.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

            //Скрытие внешней сетки карты
            gmaps.ShowTileGridLines = false;
            // При загрузке 10-кратное увеличение
            gmaps.Zoom = 10;
            //Убрать красный крестик по центру
            gmaps.ShowCenter = false;

            //Чья карта используется
            gmaps.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmaps.Position = new GMap.NET.PointLatLng(47.2229, 38.9095);

            //Для запросов
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            //Инициализация маркера
            GMap.NET.WindowsPresentation.GMapMarker marker = new GMap.NET.WindowsPresentation.GMapMarker(new PointLatLng(47.2229, 38.8855908));

            //Пример как добавить маркер на карту            
            Image image = new Image();
            image.Source = ImageHelper.GenerateImage("IconShops_X24.png"); 
            
            marker.Shape = image;

            gmaps.Markers.Add(marker);
        }
    }
}
