using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SaleDocument
{
    public class LocalFilter
    {
        Int32 iD;
        Int32 userID;

        public Int32 ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        public Int32 UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
    }

    public class LocalRow
    {

    }
    class SaleDocumentLogic
    {
    }
}
