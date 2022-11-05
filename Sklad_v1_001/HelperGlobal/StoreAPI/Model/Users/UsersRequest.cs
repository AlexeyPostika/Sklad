using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal.StoreAPI.Model.Users
{
    public class Users
    {
        public Int32 iD { get; set; }
        public Int32 userID { get; set; }
        public Int32 number { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String secondName { get; set; }
        public String iNN { get; set; }
        public Int32 roleID { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public Boolean active { get; set; }
        public String login { get; set; }
        public String password { get; set; }
        public String description { get; set; }
        public DateTime? createdDate { get; set; }
        public String createdDateString { get; set; }
        public DateTime? lastModifiedDate { get; set; }
        public String lastModifiedDateString { get; set; }
        public Int32 createdByUserID { get; set; }
        public Int32 lastModifiedByUserID { get; set; }
        public DateTime? birthday { get; set; }
        public Int32 genderID { get; set; }
        public Int32 companyID { get; set; }
        public Byte[] photoUserByte { get; set; }
        public DateTime? syncDate { get; set; }
        public Int32 syncStatus { get; set; }
    }
    public class UsersRequest
    {
    }
}
