using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SupplyDocumentDetails
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
        public SupplyDocumentDetailsRequest(Attributes _attributes)
        {
            CompanyID = 1;
            ShopID = 100;
        }

        public SupplyDocumentDetailsRequest Convert(LocaleRow row, SupplyDocumentDetailsRequest _supplyDocumentDetailsRequest)
        {
            _supplyDocumentDetailsRequest.ID = row.ID;
            _supplyDocumentDetailsRequest.DocumentID = row.DocumentID;
            _supplyDocumentDetailsRequest.Name = row.Name;
            _supplyDocumentDetailsRequest.Quantity = row.Quantity;
            _supplyDocumentDetailsRequest.TagPriceUSA = row.TagPriceUSA;
            _supplyDocumentDetailsRequest.TagPriceRUS = row.TagPriceRUS;
            _supplyDocumentDetailsRequest.SaleTagPriceUSA = 0;
            _supplyDocumentDetailsRequest.SaleTagPriceRUS = 0;
            _supplyDocumentDetailsRequest.CategoryID = row.CategoryID;
            _supplyDocumentDetailsRequest.CategoryDetailsID = row.CategoryDetailsID;
            _supplyDocumentDetailsRequest.ImageProduct = row.ImageProduct;
            _supplyDocumentDetailsRequest.BarcodesShop = row.BarCodeString;
            _supplyDocumentDetailsRequest.Model = row.Model;
            _supplyDocumentDetailsRequest.SizeProduct = row.SizeProduct;
            //_supplyDocumentDetailsRequest.Size =row.
            _supplyDocumentDetailsRequest.CreatedDate = row.CreatedDate;
            _supplyDocumentDetailsRequest.CreatedUserID = row.CreatedUserID;
            _supplyDocumentDetailsRequest.LastModificatedDate = row.LastModificatedDate;
            _supplyDocumentDetailsRequest.LastModificatedUserID = row.LastModificatedUserID;
        
            return _supplyDocumentDetailsRequest;
        }
    }
}
