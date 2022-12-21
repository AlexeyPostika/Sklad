using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMaps_Test.YandexMap
{
    public class Document
    {
        public Location location { get; set; }
        public String mapType { get; set; }
        public Boolean showTraffic { get; set; }
        public String searchText { get; set; }
        public Boolean oldBrowser { get; set; }
        public String tld { get; set; }
        public String oid { get; set; }
        public Boolean isMobile { get; set; }
        public Boolean hasVisitedCookie { get; set; }
    }
    public class Location
    {
       public Double[] center { get; set; }
       public Int32 zoom { get; set; }
    }   
}
