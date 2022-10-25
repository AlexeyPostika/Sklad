using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sklad_v1_001.FormUsers.Users;

namespace Sklad_v1_001.FormUsers.Company
{
    public class LocaleRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private Int32 iD;
        private String fullCompanyName;
        private String shortCompanyName;
        private String adress;
        private String phone;
        private Byte[] logo;
        private Boolean active;

        private FormUsers.Users.LocalRow generalDirectory;
        private FormUsers.Users.LocalRow seniorAccount;
        private String senttlementAccount;

        private String iNN;
        private String kPP;
        private Int32 taxRate;
        private String bancName;
        private String bancAdress;
        private Int32 currentCode;
        private String currentName;
        private String rCBIC;
        private String correspondentAccount;

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
        public String FullCompanyName
        {
            get
            {
                return fullCompanyName;
            }

            set
            {
                fullCompanyName = value;
                OnPropertyChanged("FullCompanyName");
            }
        }
        public String ShortCompanyName
        {
            get
            {
                return shortCompanyName;
            }

            set
            {
                shortCompanyName = value;
                OnPropertyChanged("ShortCompanyName");
            }
        }
        public String Adress
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
        public Byte[] Logo
        {
            get
            {
                return logo;
            }

            set
            {
                logo = value;
                OnPropertyChanged("Logo");
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
        public FormUsers.Users.LocalRow GeneralDirectory
        {
            get
            {
                return generalDirectory;
            }

            set
            {
                generalDirectory = value;
                OnPropertyChanged("GeneralDirectory");
            }
        }
        public FormUsers.Users.LocalRow SeniorAccount
        {
            get
            {
                return seniorAccount;
            }

            set
            {
                seniorAccount = value;
                OnPropertyChanged("SeniorAccount");
            }
        }
        public String SenttlementAccount
        {
            get
            {
                return senttlementAccount;
            }

            set
            {
                senttlementAccount = value;
                OnPropertyChanged("SenttlementAccount");
            }
        }
        public String INN
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
        public String KPP
        {
            get
            {
                return kPP;
            }

            set
            {
                kPP = value;
                OnPropertyChanged("KPP");
            }
        }
        public Int32 TaxRate
        {
            get
            {
                return taxRate;
            }

            set
            {
                taxRate = value;
                OnPropertyChanged("TaxRate");
            }
        }
        public String BancName
        {
            get
            {
                return bancName;
            }

            set
            {
                bancName = value;
                OnPropertyChanged("BancName");
            }
        }
        public String BancAdress
        {
            get
            {
                return bancAdress;
            }

            set
            {
                bancAdress = value;
                OnPropertyChanged("BancAdress");
            }
        }
        public int CurrentCode
        {
            get
            {
                return currentCode;
            }

            set
            {
                currentCode = value;
                OnPropertyChanged("CurrentCode");
            }
        }
        public String CurrentName
        {
            get
            {
                return currentName;
            }

            set
            {
                currentName = value;
                OnPropertyChanged("CurrentName");
            }
        }
        public String RCBIC
        {
            get
            {
                return rCBIC;
            }

            set
            {
                rCBIC = value;
                OnPropertyChanged("RCBIC");
            }
        }
        public String CorrespondentAccount
        {
            get
            {
                return correspondentAccount;
            }

            set
            {
                correspondentAccount = value;
                OnPropertyChanged("CorrespondentAccount");
            }
        }
    }
    public class CompanyLogic
    {
    }
}
