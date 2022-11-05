using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal.StoreAPI.Model.Company
{
    public class Company
    {
        public Int32 iD { get; set; }
        public Int32 addUserID { get; set; }
        public String fullCompanyName { get; set; }
        public String shortCompanyName { get; set; }
        public String adress { get; set; }
        public String phone { get; set; }
        public Byte[] logo { get; set; }
        public Boolean active { get; set; }
        public Users.Users generalDirectory { get; set; }       
        public Users.Users seniorAccount { get; set; }
        public Shops.Shop shop { get; set; }
        public String senttlementAccount { get; set; }
        public String iNN { get; set; }
        public String kPP { get; set; }
        public Int32 taxRate { get; set; }
        public String bancName { get; set; }
        public String bancAdress { get; set; }
        public Int32 currentCode { get; set; }
        public String currentName { get; set; }
        public String rCBIC { get; set; }
        public String correspondentAccount { get; set; }
        public String description { get; set; }
        [Timestamp]
        public Byte[] TimeRow { get; set; }
    }
    public class CompanyRequest
    {
        public Company company { get; set; }
    }
}
