using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sklad_v1_001.FormUsers.SupplyDocumentPayment
{
    public class LocaleRow : IAbstractRow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Int32 iD;
        private Decimal amount;
        private Int32 opertionType;
        private String opertionTypeString;
        private String description;     
        private Int32 status;
        private String statusString;

        private DateTime? createdDate;
        private Int32 createdUserID;
        private DateTime? lastModicatedDate;
        private String lastModifiadDateText;
        private Int32 lastModicatedUserID;

        private String rRN;
        private Byte[] rRNDocumentByte;
        private ImageSource imageSourceRRN;

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
      
        public Int32 OpertionType
        {
            get
            {
                return opertionType;
            }

            set
            {
                opertionType = value;
                OnPropertyChanged("OpertionType");
            }
        }
        public String OpertionTypeString
        {
            get
            {
                return opertionTypeString;
            }

            set
            {
                opertionTypeString = value;
                if (!String.IsNullOrEmpty(opertionType.ToString()))
                {
                    OperationTypeTypeList operationTypeTypeList = new OperationTypeTypeList();
                    opertionTypeString = operationTypeTypeList.innerList.FirstOrDefault(x => x.ID == opertionType) != null ? operationTypeTypeList.innerList.FirstOrDefault(x => x.ID == opertionType).Description : Properties.Resources.UndefindField;
                }
                OnPropertyChanged("OpertionTypeString");
            }
        }
        public string Description
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
                if (!String.IsNullOrEmpty(value.Value.ToString()))
                {
                    ConvertData convertData = new ConvertData();
                    convertData.DateTimeConvertShortDateString(value);
                }
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
        //lastModifiadDateText
        public String LastModifiadDateText
        {
            get
            {
                return lastModifiadDateText;
            }

            set
            {
                lastModifiadDateText = value;
                OnPropertyChanged("LastModifiadDateText");
            }
        }
        public string RRN
        {
            get
            {
                return rRN;
            }

            set
            {
                rRN = value;
                OnPropertyChanged("RRN");
            }
        }
        public byte[] RRNDocumentByte
        {
            get
            {
                return rRNDocumentByte;
            }

            set
            {
                rRNDocumentByte = value;
                if (value != null)
                {
                    ImageSourceRRN = ImageHelper.GenerateImage("IconOK16.png");
                }
                else
                {
                    ImageSourceRRN = ImageHelper.GenerateImage("IconMinus.png");
                }
                OnPropertyChanged("RRNDocumentByte");
            }
        }


        public ImageSource ImageSourceRRN
        {
            get
            {
                return imageSourceRRN;
            }

            set
            {
                imageSourceRRN = value;
                OnPropertyChanged("ImageSourceRRN");
            }
        }    

        public Int32 Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                if (!String.IsNullOrEmpty(status.ToString()))
                {
                    PaymentTypeList зaymentTypeList = new PaymentTypeList();
                    statusString = зaymentTypeList.innerList.FirstOrDefault(x => x.ID == status) != null ? зaymentTypeList.innerList.FirstOrDefault(x => x.ID == status).Description : Properties.Resources.UndefindField;
                }
                OnPropertyChanged("Status");
            }
        }
        public String StatusString
        {
            get
            {
                return statusString;
            }

            set
            {
                statusString = value;
                OnPropertyChanged("StatusString");
            }
        }
        public LocaleRow()
        {
            ImageSourceRRN = ImageHelper.GenerateImage("IconMinus.png");      
        }
    }
    public class SupplyDocumentPaymentLogic
    {
    }
}
