using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.GlobalVariable
{
    public class DataBaseData
    {
        public static DataBaseData globalData;

        public String _database = "";
        public String _servername = "";
        public String _serverlogin = "";
        public String _serverpassword = "";
        public String _connectTimeout = "9999";

        public String _dataadapterserver = "";
        public String _dataadapterdataBase = "";

        public Boolean isSuccess;

        public DataBaseData()
        {
            globalData = this;
            isSuccess = false;
        }

        public DataBaseData(String _servername, String _database, String _serverlogin, String _serverpassword)
        {
            globalData = this;
            isSuccess = false;

            this._servername = _servername;
            this._database = _database;
            this._serverlogin = _serverlogin;
            this._serverpassword = _serverpassword;
        }
    }
}
