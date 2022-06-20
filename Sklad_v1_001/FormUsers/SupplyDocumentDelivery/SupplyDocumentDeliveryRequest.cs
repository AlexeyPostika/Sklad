using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SupplyDocumentDelivery
{
    public class SupplyDocumentDeliveryRequest
    {
        public Int32 ID { get; set; }
        public Int64 DocumentID { get; set; }
        public Int32 DeliveryID { get; set; }
        public Int32 DeliveryDetailsID { get; set; }
        public String DeliveryTTN { get; set; }
        public Byte[] ImageTTN { get; set; }
        public String Invoice { get; set; }
        public Byte[] ImageInvoice { get; set; }
        public Decimal AmountUSA { get; set; }
        public Decimal AmountRUS { get; set; }
        public String Description { get; set; }
        public Int32 CompanyID { get; set; }
        public Int32 ShopID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Int32 CreatedUserID { get; set; }
        public DateTime? LastModificatedDate { get; set; }
        public Int32 LastModificatedUserID { get; set; }
        public SupplyDocumentDeliveryRequest(Attributes _attributes)
        {
            CompanyID = 1;
            ShopID = 100;
        }

        public SupplyDocumentDeliveryRequest Convert(LocaleRow row, SupplyDocumentDeliveryRequest _supplyDocumentDeliveryRequest)
        {
            _supplyDocumentDeliveryRequest.ID = row.ID;
            _supplyDocumentDeliveryRequest.DocumentID = row.DocumentID;
            _supplyDocumentDeliveryRequest.DeliveryID = row.DeliveryID;
            _supplyDocumentDeliveryRequest.DeliveryDetailsID = row.DeliveryDetailsID;
            _supplyDocumentDeliveryRequest.DeliveryTTN = row.TTN;
            _supplyDocumentDeliveryRequest.ImageTTN = row.InvoiceDocumentByte;
            _supplyDocumentDeliveryRequest.Invoice = row.Invoice;
            _supplyDocumentDeliveryRequest.ImageInvoice = row.InvoiceDocumentByte;
            _supplyDocumentDeliveryRequest.AmountUSA = row.AmountUSA;
            _supplyDocumentDeliveryRequest.AmountRUS = row.AmountRUS;
            _supplyDocumentDeliveryRequest.Description = row.Description;
            //_supplyDocumentDetailsRequest.Size =row.
            _supplyDocumentDeliveryRequest.CreatedDate = row.CreatedDate;
            _supplyDocumentDeliveryRequest.CreatedUserID = row.CreatedUserID;
            _supplyDocumentDeliveryRequest.LastModificatedDate = row.LastModificatedDate;
            _supplyDocumentDeliveryRequest.LastModificatedUserID = row.LastModificatedUserID;

            return _supplyDocumentDeliveryRequest;
        }
    }
}

