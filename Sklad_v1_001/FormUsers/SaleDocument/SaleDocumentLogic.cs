using Sklad_v1_001.GlobalAttributes;
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
        Int32 basketShopUserID;

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
        public Int32 BasketShopUserID
        {
            get
            {
                return basketShopUserID;
            }
            set
            {
                basketShopUserID = value;
            }
        }
    }

    public class LocalRow
    {

    }
    public class SaleDocumentLogic
    {
        Attributes attributes;
        public SaleDocumentLogic(Attributes _attributs)
        {
            this.attributes = _attributs;
        }
    }
}
