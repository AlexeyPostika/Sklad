using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sklad_v1_001.HelperGlobal.GMapIntegration.OpenStreetMap
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
}
