using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GMaps_Test.OpenStreetMap
{
    public class Row
    {       
        public Int64 place_id { get; set; }
        public String licence { get; set; }
        public String osm_type { get; set; }
        public Int64 osm_id { get; set; }
        public String[] boundingbox { get; set; }
        public Double lat { get; set; }
        public Double lon { get; set; }
        public String display_name { get; set; }
        public Int32 place_rank { get; set; }
        public String category { get; set; }
        public String type { get; set; }
        public Double importance { get; set; }
        public ImageSource icon { get; set; }
        public RowDetails rowDetails { get; set; }

        public Row()
        {

        }
    }

    public class Location
    {
        public List<Row> location { get; set; }
        public Location()
        {

        }
    }


    public class RowDetails
    {
        public Int64 place_id { get; set; }
        public Int64 parent_place_id { get; set; }
        public String osm_type { get; set; }
        public Int64 osm_id { get; set; }
        public String category { get; set; }
        public String type { get; set; }
        public Int32 admin_level { get; set; }
        public String localname { get; set; }
        public String[] names { get; set; }
        public Addresstags addresstags { get; set; }
        public String housenumber { get; set; }
        public String calculated_postcode { get; set; }
        public String country_code { get; set; }
        public String indexed_date { get; set; }
        public Int32 importance { get; set; }
        public Int32 calculated_importance { get; set; }
        public String calculated_wikipedia { get; set; }
        public Int32 rank_address { get; set; }
        public Int32 rank_search { get; set; }
        public Boolean isarea { get; set; }
        public Centroid centroid { get; set; }
        public Geometry geometry { get; set; }
        public List<Address> address { get; set; }

    }
    public class Addresstags
    {
        public String housenumber { get; set; }
        public String street { get; set; }
    }

    public class Centroid
    {
        public String type { get; set; }
        public Double[] coordinates { get; set; }
    }

    public class Geometry
    {
        public String type { get; set; }
        public Double[] coordinates { get; set; }
    }

    public class Address
    {
        public String localname { get; set; }
        public Int64? place_id { get; set; }
        public Int64? osm_id { get; set; }
        public String osm_type { get; set; }
        public String place_type { get; set; }
        public String Class { get; set; }
        public String type { get; set; }
        public String admin_level { get; set; }
        public String rank_address { get; set; }
        public Int64? distance { get; set; }
        public Boolean? isaddress { get; set; }

    }
}
