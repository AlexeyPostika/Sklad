using Sklad_v1_001.FormUsers.SupplyDocumentDelivery;
using Sklad_v1_001.FormUsers.SupplyDocumentDetails;
using Sklad_v1_001.FormUsers.SupplyDocumentPayment;
using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SupplyDocument
{
    public class SupplyDocuments
    {
        public Int32 ID { get; set; }       
        public Int32 Count { get; set; }
        public Decimal Amount { get; set; }
        public Int64 SupplyDocumentNumber { get; set; }
        public Int32 ShopID { get; set; }
        public Int32 CompanyID { get; set; }
        public Int32 Status { get; set; }
        public Int32 SyncStatus { get; set; }
        public DateTime? SyncDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Int32 CreatedUserID { get; set; }
        public DateTime? LastModificatedDate { get; set; }
        public Int32 LastModificatedUserID { get; set; }
    }


    public class SupplyDocumentRequest
    {
        public SupplyDocuments Document { get; set; }
        public List<SupplyDocumentDeliveryRequest> Delivery { get; set; }
        public List<SupplyDocumentDetailsRequest> Details { get; set; }
        public List<SupplyDocumentPaymentRequest> Payment { get; set; }
        public SupplyDocumentRequest(Attributes _attributes)
        {
            Document = new SupplyDocuments();
            Delivery = new List<SupplyDocumentDeliveryRequest>();
            Details = new List<SupplyDocumentDetailsRequest>();
            Payment = new List<SupplyDocumentPaymentRequest>();
            Document.ShopID = 100;
            Document.CompanyID = 1;
        }
        public SupplyDocumentRequest()
        {
            Document = new SupplyDocuments();
            Delivery = new List<SupplyDocumentDeliveryRequest>();
            Details = new List<SupplyDocumentDetailsRequest>();
            Payment = new List<SupplyDocumentPaymentRequest>();          
        }

        public SupplyDocuments Convert(LocalRow row, SupplyDocuments _supplyDocumentRequest)
        {
            _supplyDocumentRequest.ID = row.ID;          
            _supplyDocumentRequest.Count = row.Count;
            _supplyDocumentRequest.Amount = row.Amount;
            _supplyDocumentRequest.SupplyDocumentNumber = row.SupplyDocumentNumber;
            _supplyDocumentRequest.Status = row.Status;
           
            //_supplyDocumentDetailsRequest.Size =row.
            _supplyDocumentRequest.CreatedDate = row.CreatedDate;
            _supplyDocumentRequest.CreatedUserID = row.CreatedUserID;
            _supplyDocumentRequest.LastModificatedDate = row.LastModificatedDate;
            _supplyDocumentRequest.LastModificatedUserID = row.LastModificatedUserID;

            return _supplyDocumentRequest;
        }
    }
}
