using Sklad_v1_001.Crypto;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sklad_v1_001.FormUsers.Users
{
    public class LocaleFilter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        string typeScreen;
        private string search;
        private Int32 iD;
        private String massRoleID;
        private String login;
        private String massIsActive;
        private String sort;

        public String TypeScreen
        {
            get
            {
                return typeScreen;
            }

            set
            {
                typeScreen = value;
                OnPropertyChanged("TypeScreen");
            }
        }
        public String Search
        {
            get
            {
                return search;
            }

            set
            {
                search = value;
                OnPropertyChanged("Search");
            }
        }
        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
                OnPropertyChanged("ID");
            }
        }
        public String MassRoleID
        {
            get
            {
                return massRoleID;
            }

            set
            {
                massRoleID = value;
                OnPropertyChanged("MassRoleID");
            }
        }
        public String Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public String MassIsActive
        {
            get
            {
                return massIsActive;
            }

            set
            {
                massIsActive = value;
                OnPropertyChanged("MassIsActive");
            }
        }

        public String Sort
        {
            get
            {
                return sort;
            }

            set
            {
                sort = value;
                OnPropertyChanged("sort");
            }
        }
        public LocaleFilter()
        {
            TypeScreen = ScreenType.ScreenTypeGrid;
            MassIsActive = "All";
            MassRoleID = "All";
            Sort = "ID";
        }
    }

    public class LocalRow : INotifyPropertyChanged
    {        
        private Int32 iD;
        private Int32 userID;
        private Int32 number;
        private String firstName;
        private String lastName;       
        private String secondName;
        private String iNN;
        private Int32 roleID;
        private String phone;
        private String email;
        private Boolean active;
        private String login;
        private String password;
        private String description;
        private DateTime? createdDate;
        private String createdDateString;
        private DateTime? lastModifiedDate;
        private String lastModifiedDateString;
        private Int32 createdByUserID;
        private Int32 lastModifiedByUserID;
        private DateTime? birthday;
        private Int32 genderID;
        private Int32 companyID;

        private Byte[] photoUserByte;
        private ImageSource photoUserImage;

        private DateTime? syncDate;
        private Int32 syncStatus;
        private Int32 reffID;

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
                OnPropertyChanged("ID");
            }
        }
        
        public int UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string SecondName
        {
            get
            {
                return secondName;
            }

            set
            {
                secondName = value;
                OnPropertyChanged("Otchestvo");
            }
        }

        public string INN
        {
            get
            {
                return iNN;
            }

            set
            {
                iNN = value;
                OnPropertyChanged("INN");
            }
        }

        public Int32 RoleID
        {
            get
            {
                return roleID;
            }

            set
            {
                roleID = value;
                OnPropertyChanged("RoleID");
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public Boolean Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
                OnPropertyChanged("Active");
            }
        }

        public String Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public String Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public String Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public DateTime? CreatedDate
        {
            get
            {
                return createdDate;
            }

            set
            {
                createdDate = value;
                OnPropertyChanged("CreatedDate");
            }
        }

        public String CreatedDateString
        {
            get
            {
                return createdDateString;
            }

            set
            {
                createdDateString = value;
                OnPropertyChanged("CreatedDateString");
            }
        }

        public DateTime? LastModifiedDate
        {
            get
            {
                return lastModifiedDate;
            }

            set
            {
                lastModifiedDate = value;
                OnPropertyChanged("LastModifiedDate");
            }
        }

        public String LastModifiedDateString
        {
            get
            {
                return lastModifiedDateString;
            }

            set
            {
                lastModifiedDateString = value;
                OnPropertyChanged("LastModifiedDateString");
            }
        }

        public Int32 CreatedByUserID
        {
            get
            {
                return createdByUserID;
            }

            set
            {
                createdByUserID = value;
                OnPropertyChanged("CreatedByUserID");
            }
        }

        public Int32 LastModifiedByUserID
        {
            get
            {
                return lastModifiedByUserID;
            }

            set
            {
                lastModifiedByUserID = value;
                OnPropertyChanged("LastModifiedByUserID");
            }
        }
        public Int32 CompanyID
        {
            get
            {
                return companyID;
            }

            set
            {
                companyID = value;
                OnPropertyChanged("CompanyID");
            }
        }
        public DateTime? Birthday
        {
            get
            {
                return birthday;
            }

            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public Int32 GenderID
        {
            get
            {
                return genderID;
            }

            set
            {
                genderID = value;
                OnPropertyChanged("GenderID");
            }
        }

        public byte[] PhotoUserByte
        {
            get
            {
                return photoUserByte;
            }

            set
            {
                photoUserByte = value;
                OnPropertyChanged("PhotoUserByte");
            }
        }
        public ImageSource PhotoUserImage
        {
            get
            {
                return photoUserImage;
            }

            set
            {
                photoUserImage = value;
                OnPropertyChanged("PhotoUserImage");
            }
        }

        public DateTime? SyncDate
        {
            get
            {
                return syncDate;
            }

            set
            {
                syncDate = value;
                OnPropertyChanged("SyncDate");
            }
        }

        public Int32 SyncStatus
        {
            get
            {
                return syncStatus;
            }

            set
            {
                syncStatus = value;
                OnPropertyChanged("SyncStatus");
            }
        }
        //
        public Int32 ReffID
        {
            get
            {
                return reffID;
            }

            set
            {
                reffID = value;
                OnPropertyChanged("ReffID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public LocalRow()
        {
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
    }
    public class UserLogic
    {
        Attributes attributes;
        CryptDecrypt cryptoLogic;
        string get_store_procedure = "xp_GetUser";
        string get_save_procedure = "xp_SaveUsers";

        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlRequestSave = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;
       
        public UserLogic(Attributes _attributes)
        {
            this.attributes = _attributes;
            cryptoLogic = new CryptDecrypt();
            //объявили подключение
            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSave = new SQLCommanSelect();

            _data = new DataTable();
            _datarow = new DataTable();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.AddParametr("@p_search", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_search", "");

            _sqlRequestSelect.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_ID", 0);

            _sqlRequestSelect.AddParametr("@p_MassRoleID", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_MassRoleID", "");

            _sqlRequestSelect.AddParametr("@p_Login", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_Login", "");

            _sqlRequestSelect.AddParametr("@p_MassIsActive", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_MassIsActive", "");

            _sqlRequestSelect.AddParametr("@p_sort", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_sort", "");
            
            //----------------------------------------------------------------------------
            _sqlRequestSave.AddParametr("@p_AddUserID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_AddUserID", attributes.numeric.userEdit.AddUserID);
            
            _sqlRequestSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ID", 0);
         
            _sqlRequestSave.AddParametr("@p_Number", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_Number", 0);
           
            _sqlRequestSave.AddParametr("@p_FirstName", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_FirstName", "");
            
            _sqlRequestSave.AddParametr("@p_LastName", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_LastName", "");
         
            _sqlRequestSave.AddParametr("@p_SecondName", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_SecondName", "");
           
            _sqlRequestSave.AddParametr("@p_INN", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_INN", "");

            _sqlRequestSave.AddParametr("@p_RoleID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_RoleID", 0);

            _sqlRequestSave.AddParametr("@p_Phone", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Phone", "");

            _sqlRequestSave.AddParametr("@p_Email", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Email", "");

            _sqlRequestSave.AddParametr("@p_Active", SqlDbType.Bit);
            _sqlRequestSave.SetParametrValue("@p_Active", 0);

            _sqlRequestSave.AddParametr("@p_Login", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Login", "");

            _sqlRequestSave.AddParametr("@p_Password", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Password", "");

            _sqlRequestSave.AddParametr("@p_Birthday", SqlDbType.DateTime);
            _sqlRequestSave.SetParametrValue("@p_Birthday", DateTime.Now);

            _sqlRequestSave.AddParametr("@p_GenderID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_GenderID", 0);

            _sqlRequestSave.AddParametr("@p_PhotoUser", SqlDbType.VarBinary);
            _sqlRequestSave.SetParametrValue("@p_PhotoUser", null);

            _sqlRequestSave.AddParametr("@p_SyncDate", SqlDbType.DateTime);
            _sqlRequestSave.SetParametrValue("@p_SyncDate", DateTime.Now);

            _sqlRequestSave.AddParametr("@p_SyncStatus", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_SyncStatus", 0);

            _sqlRequestSave.AddParametr("@p_ReffID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ReffID", 0);
        }

        public DataTable FillGrid(LocaleFilter localeFilter)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);
            _sqlRequestSelect.SetParametrValue("@p_search", localeFilter.Search);
            _sqlRequestSelect.SetParametrValue("@p_ID", localeFilter.ID);
            _sqlRequestSelect.SetParametrValue("@p_MassRoleID", localeFilter.MassRoleID);
            _sqlRequestSelect.SetParametrValue("@p_Login", localeFilter.Login);
            _sqlRequestSelect.SetParametrValue("@p_MassIsActive", localeFilter.MassIsActive);
            _sqlRequestSelect.SetParametrValue("@p_sort", localeFilter.Sort);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;

            return _data;
        }

        public DataTable FillGrid(string login)
        {
            _sqlRequestSelect.SqlAnswer.datatablerow.Clear();
            _datarow.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);
            _sqlRequestSelect.SetParametrValue("@p_Login", login);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _datarow = _sqlRequestSelect.SqlAnswer.datatable;
            return _datarow;
        }
        public Int32 SaveRow(LocalRow row)
        {
            _sqlRequestSave.SetParametrValue("@p_Number", row.Number);
            _sqlRequestSave.SetParametrValue("@p_FirstName", row.FirstName);
            _sqlRequestSave.SetParametrValue("@p_LastName", row.LastName);
            _sqlRequestSave.SetParametrValue("@p_SecondName", row.SecondName);
            _sqlRequestSave.SetParametrValue("@p_INN", row.INN);
            _sqlRequestSave.SetParametrValue("@p_RoleID", row.RoleID);
            _sqlRequestSave.SetParametrValue("@p_Phone", row.Phone);
            _sqlRequestSave.SetParametrValue("@p_Email", row.Email);
            _sqlRequestSave.SetParametrValue("@p_Active", row.Active);
            _sqlRequestSave.SetParametrValue("@p_Login", row.Login);
            _sqlRequestSave.SetParametrValue("@p_Password", row.Password);
            _sqlRequestSave.SetParametrValue("@p_Birthday", row.Birthday);
            _sqlRequestSave.SetParametrValue("@p_GenderID", row.GenderID);
            _sqlRequestSave.SetParametrValue("@p_PhotoUser", row.PhotoUserByte);
            _sqlRequestSave.SetParametrValue("@p_SyncDate", row.SyncDate);
            _sqlRequestSave.SetParametrValue("@p_SyncStatus", row.SyncStatus);
            _sqlRequestSave.SetParametrValue("@p_ReffID", row.ID);

            _sqlRequestSave.ComplexRequest(get_save_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSave.SqlAnswer.result;
        }
        public LocalRow Convert(DataRow _row, LocalRow localrow)
        {
            VetrinaList listVetrina = new VetrinaList();
            ImageSql imageSql = new ImageSql();
            ConvertData convertData = new ConvertData(_row, localrow);          

            localrow.ID = convertData.ConvertDataInt32("ID");
            localrow.UserID= convertData.ConvertDataInt32("ID"); ;
            localrow.Number = convertData.ConvertDataInt32("Number");
            localrow.FirstName = convertData.ConvertDataString("FirstName");
            localrow.LastName = convertData.ConvertDataString("LastName");
            localrow.SecondName = convertData.ConvertDataString("SecondName");
            localrow.INN = convertData.ConvertDataString("INN");
            localrow.Phone = convertData.ConvertDataString("Phone");
            localrow.Email = convertData.ConvertDataString("Email");
            localrow.Active = convertData.ConvertDataBoolean("Active");
            localrow.Login = convertData.ConvertDataString("Login");
            localrow.Description = convertData.ConvertDataString("Login");
            localrow.Password = cryptoLogic.Decrypt(convertData.ConvertDataString("Password"));
            localrow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            localrow.LastModifiedDate = convertData.ConvertDataDateTime("LastModifiedDate");
            localrow.CreatedDateString = convertData.DateTimeConvertShortString(localrow.CreatedDate);
            localrow.LastModifiedDateString = convertData.DateTimeConvertShortString(localrow.LastModifiedDate);
            localrow.CreatedByUserID = convertData.ConvertDataInt32("CreatedByUserID");
            localrow.LastModifiedByUserID = convertData.ConvertDataInt32("LastModifiedByUserID");
            localrow.Birthday = convertData.ConvertDataDateTime("Birthday");
            localrow.GenderID = convertData.ConvertDataInt32("ID");
            if (_row["PhotoUser"] as byte[] != null)
                localrow.PhotoUserImage = imageSql.BytesToImageSource(_row["PhotoUser"] as byte[]);
            else
                localrow.PhotoUserImage = ImageHelper.GenerateImage("admin1.png");
            localrow.SyncDate = convertData.ConvertDataDateTime("SyncDate");
            localrow.SyncStatus = convertData.ConvertDataInt32("SyncStatus");
            localrow.RoleID = convertData.ConvertDataInt32("RoleID");
            localrow.ReffID = convertData.ConvertDataInt32("ReffID");

            return localrow;
        }
        ////users
        //public users ConvertToUsers(DataRow _row, users _users)
        //{
        //    VetrinaList listVetrina = new VetrinaList();
        //    convertData = new ConvertData(_row, _users);
        //    _users.ID = convertData.ConvertDataInt32("ID");
        //    _users.FirstName = convertData.ConvertDataString("FirstName");
        //    _users.LastName = convertData.ConvertDataString("LastName");
        //    _users.SecondName = convertData.ConvertDataString("SecondName");
        //    _users.Login = convertData.ConvertDataString("Login");
        //    _users.Password = convertData.ConvertDataString("Password");
        //    _users.Email = convertData.ConvertDataString("Email");
        //    _users.RoleID = convertData.ConvertDataInt32("RoleID");
        //    _users.Phone = convertData.ConvertDataString("Phone");
        //    _users.Active = convertData.ConvertDataBoolean("Active");
        //    if (_users.LastName!="" && _users.FirstName!="" && _users.SecondName != "")
        //    {
        //        _users.Name = _users.LastName + " " + _users.FirstName + " " + _users.SecondName;
        //        _users.ShortName = _users.LastName + " " + _users.FirstName.Substring(0, 1) + "." + _users.SecondName.Substring(0, 1);
        //        _users.Description = _users.ShortName;
        //    }
           
        //    return _users;
        //}
    }
}
