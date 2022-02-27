using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sklad_v1_001.FormUsers.SupplyDocumentDelivery
{
    public class LocaleFilter : INotifyPropertyChanged
    {
        Int32 documentID;
        Int32 productID;
        string typeScreen;

        public Int32 DocumentID
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

        public string TypeScreen
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

        public int ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                productID = value;
                OnPropertyChanged("ProductID");
            }
        }

        public LocaleFilter()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
   
    public class LocaleRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Int32 iD;
        private Int32 lineDocument;
        private Int64 documentNumber;
        private Int32 tempID;
        private Int32 deliveryID;
        private Int32 deliveryDetailsID;

        private String nameCompany;
        private String managerName;
        private String phonesCompany;
        private String phonesManager;
        private String adressCompany;

        private Decimal amountUSA;
        private Decimal amountRUS;

        private String description;

        private DateTime? createdDate;
        private Int32 createdUserID;
        private DateTime? lastModificatedDate;
        private String lastModifiadDateText;
        private Int32 lastModificatedUserID;
        private Int32 status;
        private String statusString;

        private String invoice;
        private Byte[] invoiceDocumentByte;
        private ImageSource imageSourceInvoice;

        private String tTN;
        private Byte[] tTNDocumentByte;
        private ImageSource imageSourceTTN;

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
        //tempID
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
        public string ManagerName
        {
            get
            {
                return managerName;
            }

            set
            {
                managerName = value;
                OnPropertyChanged("ManagerName");
            }
        }

        public string PhonesCompany
        {
            get
            {
                return phonesCompany;
            }

            set
            {
                phonesCompany = value;
                OnPropertyChanged("PhonesCompany");
            }
        }
        //PhonesDetails
        public string PhonesManager
        {
            get
            {
                return phonesManager;
            }

            set
            {
                phonesManager = value;
                OnPropertyChanged("PhonesManager");
            }
        }
        public string AdressCompany
        {
            get
            {
                return adressCompany;
            }

            set
            {
                adressCompany = value;
                OnPropertyChanged("AdressCompany");
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
                if (lastModificatedDate!=null)
                    if (!String.IsNullOrEmpty(value.Value.ToString()))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.DateTimeConvertShortDateString(value);
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
                if (value != null)
                {
                    imageSourceInvoice = ImageHelper.GenerateImage("IconOK16.png");
                }
                else
                {
                    imageSourceInvoice = ImageHelper.GenerateImage("IconMinus.png");
                }
                OnPropertyChanged("InvoiceDocumentByte");
            }
        }


        public ImageSource ImageSourceInvoice
        {
            get
            {
                return imageSourceInvoice;
            }

            set
            {
                imageSourceInvoice = value;
                OnPropertyChanged("ImageSourceInvoice");
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
                if (value != null)
                {
                    imageSourceTTN = ImageHelper.GenerateImage("IconOK16.png");
                }
                else
                {
                    imageSourceTTN = ImageHelper.GenerateImage("IconMinus.png");
                }
                OnPropertyChanged("TTNDocumentByte");
            }
        }

        public ImageSource ImageSourceTTN
        {
            get
            {
                return imageSourceTTN;
            }

            set
            {
                imageSourceTTN = value;
                OnPropertyChanged("ImageSourceTTN");
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
                    DeliveryTypeList deliveryTypeList = new DeliveryTypeList();
                    statusString = deliveryTypeList.innerList.FirstOrDefault(x => x.ID == status) != null ? deliveryTypeList.innerList.FirstOrDefault(x => x.ID == status).Description : Properties.Resources.UndefindField;
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

        public int DeliveryID
        {
            get
            {
                return deliveryID;
            }

            set
            {
                deliveryID = value;
                OnPropertyChanged("DeliveryID");
            }
        }

        public int DeliveryDetailsID
        {
            get
            {
                return deliveryDetailsID;
            }

            set
            {
                deliveryDetailsID = value;
                OnPropertyChanged("DeliveryDetailsID");
            }
        }
      
        public Decimal AmountUSA
        {
            get
            {
                return amountUSA;
            }

            set
            {
                amountUSA = value;
                OnPropertyChanged("AmountUSA");
            }
        }
      
        public Decimal AmountRUS
        {
            get
            {
                return amountRUS;
            }

            set
            {
                amountRUS = value;
                OnPropertyChanged("AmountRUS");
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

        public LocaleRow()
        {
            ImageSourceTTN = ImageHelper.GenerateImage("IconMinus.png");
            ImageSourceInvoice = ImageHelper.GenerateImage("IconMinus.png");
            Description = "";
        }
    }

    public class SupplyDocumentDeliveryLogic
    {
        string get_store_procedure = "xp_GetSupplyDocumentDeliveryTable";

        // запрос
        SQLCommanSelect _sqlRequestSelect = null;
        
        // результаты запроса
        DataTable _data = null;
        DataTable _datarow = null;

        public SupplyDocumentDeliveryLogic()
        {
            _sqlRequestSelect = new SQLCommanSelect();


            _data = new DataTable();
            _datarow = new DataTable();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);

            _sqlRequestSelect.AddParametr("@p_DocumentID", SqlDbType.BigInt);
            _sqlRequestSelect.SetParametrValue("@p_DocumentID", 0);
        
        }

        public DataTable FillGrid()
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeName);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
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
            _localeRow.DeliveryID = convertData.ConvertDataInt32("DeliveryID");
            _localeRow.NameCompany = convertData.ConvertDataString("NameCompany");
            _localeRow.PhonesCompany = convertData.ConvertDataString("PhonesCompany");
            _localeRow.AdressCompany = convertData.ConvertDataString("AdressCompany");
            _localeRow.DeliveryDetailsID = convertData.ConvertDataInt32("DeliveryDetailsID");
            _localeRow.ManagerName = convertData.ConvertDataString("ManagerName");
            _localeRow.PhonesManager = convertData.ConvertDataString("PhonesManager");
            _localeRow.TTN = convertData.ConvertDataString("TTN");
            _localeRow.Invoice = convertData.ConvertDataString("Invoice");
            if (_dataRow["ImageTTN"] as byte[] != null)
            {
                _localeRow.TTNDocumentByte = _dataRow["ImageTTN"] as byte[];
            }                     
            if (_dataRow["ImageInvoice"] as byte[] != null)
            {
                _localeRow.InvoiceDocumentByte = _dataRow["ImageInvoice"] as byte[];
            }            
            _localeRow.AmountUSA = convertData.ConvertDataDecimal("AmountUSA");
            _localeRow.AmountRUS = convertData.ConvertDataDecimal("AmountRUS");
            _localeRow.Description = convertData.ConvertDataString("Description");
            _localeRow.DocumentNumber = convertData.ConvertDataInt64("DocumentNumber");

            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedUserID = convertData.ConvertDataInt32("ID");
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModifiadDateText = convertData.DateTimeConvertShortDateString(_localeRow.LastModificatedDate);
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("ID");
                                    
            return _localeRow;
        }


        ////
        //public LocaleRow ConvertDeliveryToSupplyDocumentDelivery(Delivery.LocaleRow _row, LocaleRow _localeRow)
        //{
        //    // SaleDocumentDetailsList statusList = new SaleDocumentDetailsList();           
        //    _localeRow.ID = _row.ID;
        //    _localeRow.NameCompany = _row.NameCompany;
        //    _localeRow.PhonesCompany = _row.PhonesCompany;
        //    _localeRow.AdressCompany = _row.AdressCompany;
        //    _localeRow.ManagerName = _row.ManagerName;
        //    _localeRow.PhonesManager = _row.PhonesManager;
        //    _localeRow.CreatedDate = _row.CreatedDate;
        //    _localeRow.CreatedUserID = _row.CreatedUserID;
        //    _localeRow.LastModificatedDate = _row.LastModificatedDate;
        //    _localeRow.LastModificatedUserID = _row.LastModificatedUserID;
        //    _localeRow.TTN = _row.TTN;
        //    _localeRow.ImageSourceTTN = _row.ImageSourceTTN;
        //    _localeRow.Invoice = _row.Invoice;
        //    _localeRow.ImageSourceInvoice = _row.ImageSourceInvoice;

        //    return _localeRow;
        //}

    }
}
