using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.Delivery
{
    public class LocaleRow : IAbstractRow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Int32 iD;
        private String nameCompany;
        private String phones;
        private String adress;
        private DateTime? createdDate;
        private Int32 createdUserID;
        private DateTime? lastModicatedDate;
        private Int32 lastModicatedUserID;

        private String invoice;
        private Byte[] invoiceDocumentByte;
        private String tTN;
        private Byte[] tTNDocumentByte;

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

        public string NameCompany
        {
            get
            {
                return nameCompany;
            }

            set
            {
                nameCompany = value;
                OnPropertyChanged("NameCompany");
            }
        }
        public string Phones
        {
            get
            {
                return phones;
            }

            set
            {
                phones = value;
                OnPropertyChanged("Phones");
            }
        }
        public string Adress
        {
            get
            {
                return adress;
            }

            set
            {
                adress = value;
                OnPropertyChanged("Adress");
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
        public int CreatedUserID
        {
            get
            {
                return createdUserID;
            }

            set
            {
                createdUserID = value;
                OnPropertyChanged("CreatedUserID");
            }
        }
        public DateTime? LastModicatedDate
        {
            get
            {
                return lastModicatedDate;
            }

            set
            {
                lastModicatedDate = value;
                OnPropertyChanged("LastModicatedDate");
            }
        }
        public int LastModicatedUserID
        {
            get
            {
                return lastModicatedUserID;
            }

            set
            {
                lastModicatedUserID = value;
                OnPropertyChanged("LastModicatedUserID");
            }
        }
        public string Invoice
        {
            get
            {
                return invoice;
            }

            set
            {
                invoice = value;
                OnPropertyChanged("Invoice");
            }
        }
        public byte[] InvoiceDocumentByte
        {
            get
            {
                return invoiceDocumentByte;
            }

            set
            {
                invoiceDocumentByte = value;
                OnPropertyChanged("InvoiceDocumentByte");
            }
        }
        public string TTN
        {
            get
            {
                return tTN;
            }

            set
            {
                tTN = value;
                OnPropertyChanged("TTN");
            }
        }
        public byte[] TTNDocumentByte
        {
            get
            {
                return tTNDocumentByte;
            }

            set
            {
                tTNDocumentByte = value;
                OnPropertyChanged("TTNDocumentByte");
            }
        }
    }
    public class DeliveryLogic
    {
    }
}
