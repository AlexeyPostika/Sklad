using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SaleDocument
{
    public class LocalFilter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        Int32 iD;
        Int32 userID;
        Int32 basketShopUserID;
        private string screenTypeGrid;

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

        public String ScreenTypeGrid
        {
            get
            {
                return screenTypeGrid;
            }
            set
            {
                screenTypeGrid = value;
            }
        }
        public LocalFilter()
        {
            ScreenTypeGrid = ScreenType.ScreenTypeGrid;
        }
    }

    public class LocalRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Int32 rowNumber;
        private Int32 iD;
        private Int64 documentNumber;
        private Int32 type;
        private Int32 userID;
        private string clientCardNumber;
        private String clientPhone;
        private String clientEmail;
        private Boolean sendCheck;
        private Boolean sendPaperCheck;
        private Int32 currency;
        private DateTime? syncDate;
        private Int32 syncStatus;
        private String syncStatusString;
        private DateTime? createdDate;
        private String createsDatetString;
        private Int32 createdByUserID;
        private DateTime? lastModifiedDate;
        private String lastModifiedDateString;
        private Int32 count;
        private Decimal amount;
        private String fNDocumentNumber;
        private DateTime? fNDocumentDate;
        private String fNNumber;
        private String displayNameUser;
        private String shortDisplayNameUser;
        private String lDisplayNameUser;
        private String shortLDisplayNameUser;
        private String lDisplayNameUserID;
        private String shortLDisplayNameUserID;

        public Int32 RowNumber
        {
            get
            {
                return rowNumber;
            }

            set
            {
                rowNumber = value;
                OnPropertyChanged("RowNumber");
            }
        }
        public Int32 ID
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
        public Int64 DocumentNumber
        {
            get
            {
                return documentNumber;
            }

            set
            {
                documentNumber = value;
                OnPropertyChanged("DocumentNumber");
            }
        }
        public Int32 Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                OnPropertyChanged("Type");
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
                OnPropertyChanged("UserID");
            }
        }
        public String ClientCardNumber
        {
            get
            {
                return clientCardNumber;
            }

            set
            {
                clientCardNumber = value;
                OnPropertyChanged("ClientCardNumber");
            }
        }
        public String ClientPhone
        {
            get
            {
                return clientPhone;
            }

            set
            {
                clientPhone = value;
                OnPropertyChanged("PagerowCount");
            }
        }
        public String ClientEmail
        {
            get
            {
                return clientEmail;
            }

            set
            {
                clientEmail = value;
                OnPropertyChanged("ClientEmail");
            }
        }
        public Boolean SendCheck
        {
            get
            {
                return sendCheck;
            }

            set
            {
                sendCheck = value;
                OnPropertyChanged("SendCheck");
            }
        }
        public Boolean SendPaperCheck
        {
            get
            {
                return sendPaperCheck;
            }

            set
            {
                sendPaperCheck = value;
                OnPropertyChanged("SendPaperCheck");
            }
        }
        public Int32 Currency
        {
            get
            {
                return currency;
            }

            set
            {
                currency = value;
                OnPropertyChanged("Currency");
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
        public String SyncStatusString
        {
            get
            {
                return syncStatusString;
            }

            set
            {
                syncStatusString = value;
                OnPropertyChanged("SyncStatusString");
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
        public String CreatesDatetString
        {
            get
            {
                return createsDatetString;
            }

            set
            {
                createsDatetString = value;
                OnPropertyChanged("CreatesDatetString");
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
        public Int32 Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        public Decimal Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public String FNDocumentNumber
        {
            get
            {
                return fNDocumentNumber;
            }

            set
            {
                fNDocumentNumber = value;
                OnPropertyChanged("FNDocumentNumber");
            }
        }
        public DateTime? FNDocumentDate
        {
            get
            {
                return fNDocumentDate;
            }

            set
            {
                fNDocumentDate = value;
                OnPropertyChanged("FNDocumentDate");
            }
        }
        public String FNNumber
        {
            get
            {
                return fNNumber;
            }

            set
            {
                fNNumber = value;
                OnPropertyChanged("FNNumber");
            }
        }
        public String DisplayNameUser
        {
            get
            {
                return displayNameUser;
            }

            set
            {
                displayNameUser = value;
                OnPropertyChanged("DisplayNameUser");
            }
        }
        public String ShortDisplayNameUser
        {
            get
            {
                return shortDisplayNameUser;
            }

            set
            {
                shortDisplayNameUser = value;
                OnPropertyChanged("ShortDisplayNameUser");
            }
        }
        public String LDisplayNameUser
        {
            get
            {
                return lDisplayNameUser;
            }

            set
            {
                lDisplayNameUser = value;
                OnPropertyChanged("LDisplayNameUser");
            }
        }
        public String ShortLDisplayNameUser
        {
            get
            {
                return shortLDisplayNameUser;
            }

            set
            {
                shortLDisplayNameUser = value;
                OnPropertyChanged("ShortLDisplayNameUser");
            }
        }
        public String LDisplayNameUserID
        {
            get
            {
                return lDisplayNameUserID;
            }

            set
            {
                lDisplayNameUserID = value;
                OnPropertyChanged("LDisplayNameUserID");
            }
        }
        public String ShortLDisplayNameUserID
        {
            get
            {
                return shortLDisplayNameUserID;
            }

            set
            {
                shortLDisplayNameUserID = value;
                OnPropertyChanged("ShortLDisplayNameUserID");
            }
        }
        public LocalRow()
        {
            ClientCardNumber = String.Empty;
            ClientPhone = String.Empty;
            ClientEmail = String.Empty;
            SendCheck = false;
            SendPaperCheck = false;
            Currency = 0;
        }

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
