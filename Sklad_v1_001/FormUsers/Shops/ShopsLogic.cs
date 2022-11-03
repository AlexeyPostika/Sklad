using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.Shops
{
    public class LocaleRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Int32 addUserID;
        public Int32 iD ;
        public Int32 shopNumber ;
        public Int32 companyID ;
        public String name ;
        public String address ;
        public String phone ;
        private DateTime? createdDate;
        private String createdDateString;
        private DateTime? lastModifiedDate;
        private String lastModifiedDateString;
        private Int32 createdByUserID;
        private Int32 lastModifiedByUserID;
        public DateTime? syncDate ;
        public Int32 syncStatus ;
        [Timestamp]
        public Byte[] TimeRow { get; set; }
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
        public int AddUserID
        {
            get
            {
                return addUserID;
            }

            set
            {
                addUserID = value;
                OnPropertyChanged("AddUserID");
            }
        }
        public int ShopNumber
        {
            get
            {
                return shopNumber;
            }

            set
            {
                shopNumber = value;
                OnPropertyChanged("ShopNumber");
            }
        }
        public int CompanyID
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
        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public String Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public String Phone
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
        public int SyncStatus
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
    }
    class ShopsLogic
    {
    }
}
