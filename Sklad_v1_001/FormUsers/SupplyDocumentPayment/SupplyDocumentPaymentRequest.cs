using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SupplyDocumentPayment
{
    public class SupplyDocumentPaymentRequest
    {
        public Int32 ID { get; set; }
        public Int64 DocumentID { get; set; }
        public Int32 Status { get; set; }
        public Int32 OperationType { get; set; }
        public Decimal Amount { get; set; }
        public String Description { get; set; }
        public String RRN { get; set; }
        public Byte[] DocRRN { get; set; }
        public Int32 ShopID { get; set; }
        public Int32 CompanyID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Int32 CreatedUserID { get; set; }
        public DateTime? LastModificatedDate { get; set; }
        public Int32 LastModificatedUserID { get; set; }
        public SupplyDocumentPaymentRequest(Attributes _attributes)
        {
            CompanyID = 1;
            ShopID = 100;
        }

        public SupplyDocumentPaymentRequest Convert(LocaleRow row, SupplyDocumentPaymentRequest _supplyDocumentPaymentRequest)
        {
            _supplyDocumentPaymentRequest.ID = row.ID;
            _supplyDocumentPaymentRequest.DocumentID = row.DocumentID;
            _supplyDocumentPaymentRequest.Status = row.Status;
            _supplyDocumentPaymentRequest.OperationType = row.OpertionType;
            _supplyDocumentPaymentRequest.Amount = row.Amount;
            _supplyDocumentPaymentRequest.Description = row.Description;
            _supplyDocumentPaymentRequest.RRN = row.RRN;
            _supplyDocumentPaymentRequest.DocRRN = row.RRNDocumentByte;         
            _supplyDocumentPaymentRequest.CreatedDate = row.CreatedDate;
            _supplyDocumentPaymentRequest.CreatedUserID = row.CreatedUserID;
            _supplyDocumentPaymentRequest.LastModificatedDate = row.LastModificatedDate;
            _supplyDocumentPaymentRequest.LastModificatedUserID = row.LastModificatedUserID;

            return _supplyDocumentPaymentRequest;
        }
    }
}
