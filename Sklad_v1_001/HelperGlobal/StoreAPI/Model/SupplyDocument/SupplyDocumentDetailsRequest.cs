using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentDetails
{
    public class SupplyDocumentDetailsRequest
    {
        public Int32 ID { get; set; }
        public Int64 DocumentID { get; set; }
        public String Name { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal TagPriceUSA { get; set; }
        public Decimal TagPriceRUS { get; set; }
        public Decimal SaleTagPriceUSA { get; set; }
        public Decimal SaleTagPriceRUS { get; set; }
        public Int32 CategoryID { get; set; }
        public Int32 CategoryDetailsID { get; set; }
        public Byte[] ImageProduct { get; set; }
        public String BarcodesShop { get; set; }
        public String Model { get; set; }
        public String SizeProduct { get; set; }
        public Boolean Size { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Int32 CreatedUserID { get; set; }
        public DateTime? LastModificatedDate { get; set; }
        public Int32 LastModificatedUserID { get; set; }
        public Int32 CompanyID { get; set; }
        public Int32 ShopID { get; set; }
        public SupplyDocumentDetailsRequest()
        {
            CompanyID = 1;
            ShopID = 100;
        }     
    }
}
