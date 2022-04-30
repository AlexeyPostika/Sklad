using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
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
    public class LocalRow : INotifyPropertyChanged
    {
        /*
         * 
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[]
      ,[PhotoUser]
         */

        private Int32 iD;
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
        
        private DateTime? createdDate;
        private String createdDateString;
        private DateTime? lastModifiedDate;
        private String lastModifiedDateString;
        private Int32 createdByUserID;
        private Int32 lastModifiedByUserID;
        private DateTime? birthday;
        private Int32 genderID;

        private Byte[] photoUserByte;
        private ImageSource photoUserImage;
    
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
        /*
         *  private String ;
        private String ;
        
        private DateTime? ;
        private String ;
        private DateTime? ;
        private String ;
        private Int32 ;
        private Int32 ;
        private DateTime? ;
        private Int32 genderID;

        private Byte[] ;
        private ImageSource ;
         */
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class UserLogic
    {
        SQLCommanSelect _sqlSting;

        LocalRow localrow;

        String _getSelectProductTable = "xp_GetSelectUsersTable";      //хранимка

        DataTable _table;

        ConvertData convertData;

        public UserLogic()
        {
            
            //объявили подключение
            _sqlSting = new SQLCommanSelect();
            //объявили localRow
            localrow = new LocalRow();
            //объявили таблицу куда будем записывать все
            _table = new DataTable();
            //за правильную конвертацию данных
            convertData = new ConvertData();
        }

        public DataTable FillGrid()
        {
            _sqlSting.SqlAnswer.datatable.Clear();
            _table.Clear();      

            _sqlSting.ComplexRequest(_getSelectProductTable, CommandType.StoredProcedure, null);
            _table = _sqlSting.SqlAnswer.datatable;

            return _table;
        }

        public LocalRow Convert(DataRow _row, LocalRow localrow)
        {
            VetrinaList listVetrina = new VetrinaList();
            convertData = new ConvertData(_row, localrow);
            localrow.ID = convertData.ConvertDataInt32("ID");
            localrow.FirstName = convertData.ConvertDataString("FirstName");
            localrow.LastName = convertData.ConvertDataString("LastName");
            localrow.SecondName = convertData.ConvertDataString("Otchestvo");
           
            localrow.Phone = convertData.ConvertDataString("NumberPhone");
            localrow.Number = convertData.ConvertDataInt32("NumberSotrudnika");
          
          
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
