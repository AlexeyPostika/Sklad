using Sklad_v1_001.GlobalAttributes;
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

namespace Sklad_v1_001.FormUsers.Payment
{
    public class LocaleRow: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Int32 iD;
        private Int32 lineDocument;
        private Int32 tempID;
        private Int32 documentID;
        private Decimal amount;
        private Int32 opertionType;
        private String opertionTypeString;
        private String description;
        private Int32 status;
        private String statusString;

        private DateTime? createdDate;
        private Int32 createdUserID;
        private DateTime? lastModificatedDate;
        private String lastModifiadDateText;
        private Int32 lastModificatedUserID;

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
        public Int32 LineDocument
        {
            get
            {
                return lineDocument;
            }

            set
            {
                lineDocument = value;
                OnPropertyChanged("LineDocument");
            }
        }

        public int TempID
        {
            get
            {
                return tempID;
            }

            set
            {
                tempID = value;
                OnPropertyChanged("TempID");
            }
        }
        public int DocumentID
        {
            get
            {
                return documentID;
            }

            set
            {
                documentID = value;
                OnPropertyChanged("DocumentID");
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
                if (!String.IsNullOrEmpty(opertionType.ToString()))
                {
                    OperationTypeTypeList operationTypeTypeList = new OperationTypeTypeList();
                    opertionTypeString = operationTypeTypeList.innerList.FirstOrDefault(x => x.ID == opertionType) != null ? operationTypeTypeList.innerList.FirstOrDefault(x => x.ID == opertionType).Description : Properties.Resources.UndefindField;
                }
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
        public DateTime? LastModificatedDate
        {
            get
            {
                return lastModificatedDate;
            }

            set
            {
                lastModificatedDate = value;
                if (!String.IsNullOrEmpty(value.Value.ToString()))
                {
                    ConvertData convertData = new ConvertData();
                    lastModifiadDateText = convertData.DateTimeConvertShortDateString(value);
                }
                OnPropertyChanged("LastModificatedDate");
            }
        }
        public int LastModificatedUserID
        {
            get
            {
                return lastModificatedUserID;
            }

            set
            {
                lastModificatedUserID = value;
                OnPropertyChanged("LastModificatedUserID");
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
    public class PaymentLogic
    {
        Attributes attributes;

        public PaymentLogic(Attributes _attributes)
        {
            this.attributes = _attributes;
        }

        public LocaleRow ConvertSupplyDocumentToPayment(SupplyDocumentPayment.LocaleRow _supplyPayment , LocaleRow _localeRow)
        {
            _localeRow.Amount = _supplyPayment.Amount;
            _localeRow.CreatedDate = _supplyPayment.CreatedDate;
            _localeRow.CreatedUserID = _supplyPayment.CreatedUserID;
            _localeRow.Description = _supplyPayment.Description;
            _localeRow.DocumentID = _supplyPayment.DocumentID;
            _localeRow.ID = _supplyPayment.ID;
            _localeRow.ImageSourceRRN = _supplyPayment.ImageSourceRRN;
            _localeRow.LastModifiadDateText = _supplyPayment.LastModifiadDateText;
            _localeRow.LastModificatedDate = _supplyPayment.LastModificatedDate;
            _localeRow.LastModificatedUserID = _supplyPayment.LastModificatedUserID;
            _localeRow.LineDocument = _supplyPayment.LineDocument;
            _localeRow.OpertionType = _supplyPayment.OpertionType;
            _localeRow.OpertionTypeString = _supplyPayment.OpertionTypeString;
            _localeRow.RRN = _supplyPayment.RRN;
            _localeRow.RRNDocumentByte = _supplyPayment.RRNDocumentByte;
            _localeRow.Status = _supplyPayment.Status;
            _localeRow.StatusString = _supplyPayment.StatusString;
            _localeRow.TempID = _supplyPayment.TempID;
            return _localeRow;
        }

        public LocaleRow ConvertRegisterDocumentToPayment(RegisterDocumentPayment.LocaleRow _supplyPayment, LocaleRow _localeRow)
        {
            _localeRow.Amount = _supplyPayment.Amount;
            _localeRow.CreatedDate = _supplyPayment.CreatedDate;
            _localeRow.CreatedUserID = _supplyPayment.CreatedUserID;
            _localeRow.Description = _supplyPayment.Description;
            _localeRow.DocumentID = _supplyPayment.DocumentID;
            _localeRow.ID = _supplyPayment.ID;
            _localeRow.ImageSourceRRN = _supplyPayment.ImageSourceRRN;
            _localeRow.LastModifiadDateText = _supplyPayment.LastModifiadDateText;
            _localeRow.LastModificatedDate = _supplyPayment.LastModificatedDate;
            _localeRow.LastModificatedUserID = _supplyPayment.LastModificatedUserID;
            _localeRow.LineDocument = _supplyPayment.LineDocument;
            _localeRow.OpertionType = _supplyPayment.OpertionType;
            _localeRow.OpertionTypeString = _supplyPayment.OpertionTypeString;
            _localeRow.RRN = _supplyPayment.RRN;
            _localeRow.RRNDocumentByte = _supplyPayment.RRNDocumentByte;
            _localeRow.Status = _supplyPayment.Status;
            _localeRow.StatusString = _supplyPayment.StatusString;
            _localeRow.TempID = _supplyPayment.TempID;
            return _localeRow;
        }

        public SupplyDocumentPayment.LocaleRow ConvertPaymentToSupplyDocument(LocaleRow _localeRow, SupplyDocumentPayment.LocaleRow _supplyPayment)
        {
            _supplyPayment.Amount = _localeRow.Amount;
            _supplyPayment.CreatedDate = _localeRow.CreatedDate;
            _supplyPayment.CreatedUserID = _localeRow.CreatedUserID;
            _supplyPayment.Description = _localeRow.Description;
            _supplyPayment.DocumentID = _localeRow.DocumentID;
            _supplyPayment.ID = _localeRow.ID;
            _supplyPayment.ImageSourceRRN = _localeRow.ImageSourceRRN;
            _supplyPayment.LastModifiadDateText = _localeRow.LastModifiadDateText;
            _supplyPayment.LastModificatedDate = _localeRow.LastModificatedDate;
            _supplyPayment.LastModificatedUserID = _localeRow.LastModificatedUserID;
            _supplyPayment.LineDocument = _localeRow.LineDocument;
            _supplyPayment.OpertionType = _localeRow.OpertionType;
            _supplyPayment.OpertionTypeString = _localeRow.OpertionTypeString;
            _supplyPayment.RRN = _localeRow.RRN;
            _supplyPayment.RRNDocumentByte = _localeRow.RRNDocumentByte;
            _supplyPayment.Status = _localeRow.Status;
            _supplyPayment.StatusString = _localeRow.StatusString;
            _supplyPayment.TempID = _localeRow.TempID;
            return _supplyPayment;
        }

        public RegisterDocumentPayment.LocaleRow ConvertPaymentToRegisterDocument(LocaleRow _localeRow, RegisterDocumentPayment.LocaleRow _registerPayment)
        {
            _registerPayment.Amount = _localeRow.Amount;
            _registerPayment.CreatedDate = _localeRow.CreatedDate;
            _registerPayment.CreatedUserID = _localeRow.CreatedUserID;
            _registerPayment.Description = _localeRow.Description;
            _registerPayment.DocumentID = _localeRow.DocumentID;
            _registerPayment.ID = _localeRow.ID;
            _registerPayment.ImageSourceRRN = _localeRow.ImageSourceRRN;
            _registerPayment.LastModifiadDateText = _localeRow.LastModifiadDateText;
            _registerPayment.LastModificatedDate = _localeRow.LastModificatedDate;
            _registerPayment.LastModificatedUserID = _localeRow.LastModificatedUserID;
            _registerPayment.LineDocument = _localeRow.LineDocument;
            _registerPayment.OpertionType = _localeRow.OpertionType;
            _registerPayment.OpertionTypeString = _localeRow.OpertionTypeString;
            _registerPayment.RRN = _localeRow.RRN;
            _registerPayment.RRNDocumentByte = _localeRow.RRNDocumentByte;
            _registerPayment.Status = _localeRow.Status;
            _registerPayment.StatusString = _localeRow.StatusString;
            _registerPayment.TempID = _localeRow.TempID;
            return _registerPayment;
        }
    }
}
