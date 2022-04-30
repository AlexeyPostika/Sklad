using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.GlobalAttributes
{
    public class UserEdit
    {
        public Int32 AddUserID { get; set; }
        public Int32 RoleID { get; set; }
    }
    public class Numeric
    {
        public UserEdit userEdit { get; set; }

        public Numeric()
        {
            userEdit = new UserEdit();
        }
    }
}
