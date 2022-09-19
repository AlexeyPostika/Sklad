using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.SupplyDocumentPayment;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sklad_v1_001.FormUsers.RegisterDocumentPayment
{
    public class LocaleRow : IAbstractRow, INotifyPropertyChanged
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
        private String reffTimeRow;
        private Byte[] timeRow;
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
                else
                {
                    lastModificatedDate = DateTime.Now;
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

        public String ReffTimeRow
        {
            get
            {
                return reffTimeRow;
            }

            set
            {
                reffTimeRow = value;
                OnPropertyChanged("SupplyDocumentNumberString");
            }
        }

        public Byte[] TimeRow
        {
            get
            {
                return timeRow;
            }

            set
            {
                timeRow = value;
                OnPropertyChanged("TimeRow");
            }
        }
        public LocaleRow()
        {
            ImageSourceRRN = ImageHelper.GenerateImage("IconMinus.png");      
        }
    }
    public class RegisterDocumentPaymentLogic
    {
        Attributes attributes;

        string get_store_procedure = "xp_GetRegisterDocumentPaymentTable";

        // запрос
        SQLCommanSelect _sqlRequestSelect = null;

        // результаты запроса
        DataTable _data = null;
        DataTable _datarow = null;
        
        public RegisterDocumentPaymentLogic(Attributes _attributes)
        {
            this.attributes = _attributes;
            _sqlRequestSelect = new SQLCommanSelect();


            _data = new DataTable();
            _datarow = new DataTable();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);

            _sqlRequestSelect.AddParametr("@p_DocumentID", SqlDbType.BigInt);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", 0);

        }

        public DataTable FillGrid(Int32 _documentID)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", _documentID);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public LocaleRow Convert(DataRow _dataRow, LocaleRow _localeRow)
        {
            // SaleDocumentDetailsList statusList = new SaleDocumentDetailsList(); 
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);
            _localeRow.ID = convertData.ConvertDataInt32("ID");
            _localeRow.LineDocument = convertData.ConvertDataInt32("LineDocument");
            _localeRow.TempID = convertData.ConvertDataInt32("ID");
            _localeRow.DocumentID = convertData.ConvertDataInt32("DocumentID");
            _localeRow.Status = convertData.ConvertDataInt32("Status");
            _localeRow.OpertionType = convertData.ConvertDataInt32("OperationType");
            _localeRow.Amount = convertData.ConvertDataDecimal("Amount");
            _localeRow.Description = convertData.ConvertDataString("Description");
            _localeRow.RRN = convertData.ConvertDataString("RRN");

            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedUserID = convertData.ConvertDataInt32("ID");
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");           
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("ID");
            _localeRow.ReffTimeRow = convertData.ConvertDataString("ReffTimeRow");

            return _localeRow;
        }

        public SupplyDocumentPaymentRequest Convert(LocaleRow row, SupplyDocumentPaymentRequest _supplyDocumentPaymentRequest)
        {
            _supplyDocumentPaymentRequest.DocumentID = row.DocumentID;
            _supplyDocumentPaymentRequest.Status = row.Status;
            _supplyDocumentPaymentRequest.OperationType = row.OpertionType;
            _supplyDocumentPaymentRequest.Amount = row.Amount;
            _supplyDocumentPaymentRequest.Description = row.Description;
            _supplyDocumentPaymentRequest.RRN = row.RRN;
            _supplyDocumentPaymentRequest.DocRRN = row.RRNDocumentByte;
            _supplyDocumentPaymentRequest.CreatedDate = row.CreatedDate;
            _supplyDocumentPaymentRequest.CreatedUserID = row.CreatedUserID;
            _supplyDocumentPaymentRequest.LastModificatedDate = row.LastModificatedDate;
            _supplyDocumentPaymentRequest.LastModificatedUserID = row.LastModificatedUserID;
            _supplyDocumentPaymentRequest.TimeRow = row.ReffTimeRow;

            return _supplyDocumentPaymentRequest;
        }
    }
}
