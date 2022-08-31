using Sklad_v1_001.FormUsers.SupplyDocument;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal.StoreAPI
{
    public class Response
    {
        public Int32 ErrorCode { get; set; }
        public String Message { get; set; }
        public String Description { get; set; }
        public String DescriptionEX { get; set; }
        public SupplyDocumentRequest SupplyDocumentOutput { get; set; }

        public SupplyDocumentRequestList SupplyDocumentListOutput { get; set; }

        public Response()
        {
            ErrorCode = 0;
            SupplyDocumentOutput = new SupplyDocumentRequest();
            SupplyDocumentListOutput = new SupplyDocumentRequestList();
        }
    }
}
