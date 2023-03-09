using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Report
{
    public class ReportData
    {
        public String typeDocument;
        public String recieverShopNumber;
        public String documentNumber;

        public ReportData()
        {
            typeDocument = "";
            recieverShopNumber = "";
            documentNumber = "";
        }
    }
}
