using Sklad_v1_001.GlobalAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentPayment
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
        [Timestamp]
        public Byte[] ReffTimeRow { get; set; }
        public SupplyDocumentPaymentRequest(Attributes _attributes)
        {
            CompanyID = 1;
            ShopID = 100;
        }
    }
}
