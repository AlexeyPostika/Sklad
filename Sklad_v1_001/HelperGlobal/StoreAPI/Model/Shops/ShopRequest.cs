using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal.StoreAPI.Model.Shops
{
    public class Shop
    {
        public Int32 addUserID { get; set; }
        public Int32 iD { get; set; }
        public Int32 shopNumber { get; set; }
        public Int32 companyID { get; set; }
        public String Name { get; set; }
        public String address { get; set; }
        public String phone { get; set; }
        public Int32 createdUserID { get; set; }
        public DateTime? lastModificatedDate { get; set; }
        public Int32 lastModificatedUserID { get; set; }
        public DateTime? syncDate { get; set; }
        public Int32 syncStatus { get; set; }
        [Timestamp]
        public Byte[] TimeRow { get; set; }
    }
    public class ShopRequest
    {
        
    }
}
