using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.GlobalAttributes
{
    public class GMap
    {
        public OpenStreetMap openStreet { get; set; }
        public GMap()
        {
            openStreet = new OpenStreetMap();
        }
    }
    public class OpenStreetMap
    {
        public String url { get; set; }
        public String urlStartPosition { get; set; }
        public String startPosion { get; set; }
        public Tag tag { get; set; }
        public String dedupe { get; set; }
        public String format { get; set; }
        public String lastRow { get; set; }

        public OpenStreetMap()
        {
            url = @"https://nominatim.openstreetmap.org/search.php?q=";
            dedupe = "&dedupe=0";
            format = "&format=jsonv2";
            startPosion = "Россия";
            urlStartPosition = url + startPosion;
            lastRow = dedupe + format;
        }
    }

    public enum Tag
    {
        q,
        city
    }
}
