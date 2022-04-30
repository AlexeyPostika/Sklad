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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
        private Int32 deliveryID;
        private Int32 deliveryDetailsID;    
        private String nameCompany;
        private String adressCompany;
        private String phonesCompany;

        private String managerName;
        private String phonesManager;
       
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
        public LocaleRow()
        {
            ImageSourceTTN = ImageHelper.GenerateImage("IconMinus.png");
            ImageSourceInvoice = ImageHelper.GenerateImage("IconMinus.png");
        }
    }
    public class DeliveryLogic
    {
        Attributes attributes;
        ConvertData convertData;

        string get_store_procedure = "xp_GetDeliveryCompanyTable";
        string save_store_procedure = "xp_SaveDeliveryCompany";


        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlRequestSave = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;    

        public DeliveryLogic(Attributes _attributes)
        {
            this.attributes = _attributes;

            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSave = new SQLCommanSelect();

            convertData = new ConvertData();

            _data = new DataTable();
            _datarow = new DataTable();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeInGrid);
            //----------------------------------------------------------------------------

            _sqlRequestSave.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_AddUserID", attributes.numeric.userEdit.AddUserID);

            _sqlRequestSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ID", 0);

            _sqlRequestSave.AddParametr("@p_NameCompany", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_NameCompany", "");

            _sqlRequestSave.AddParametr("@p_Phones", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Phones", "");

            _sqlRequestSave.AddParametr("@p_Adress", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Adress", "");
            
            _sqlRequestSave.AddParametr("@p_Country", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Country", "");

            //----------------------------------------------------------------------------

        }
        public DataTable FillGrid()
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public Int32 SaveRow(GlobalList.DeliveryCompany row)
        {
            _sqlRequestSave.SetParametrValue("@p_ID", row.ID);
            _sqlRequestSave.SetParametrValue("@p_NameCompany", row.Description);
            _sqlRequestSave.SetParametrValue("@p_Phones", row.Phones);
            _sqlRequestSave.SetParametrValue("@p_Adress", row.AdressCompany);
            _sqlRequestSave.SetParametrValue("@p_Country", String.Empty);

            _sqlRequestSave.ComplexRequest(save_store_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSave.SqlAnswer.result;
        }


        public LocaleRow Convert(DataRow _dataRow, LocaleRow _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("DeliveryDetailsID");
            _localeRow.DeliveryDetailsID = convertData.ConvertDataInt32("DeliveryDetailsID");
            _localeRow.NameCompany = convertData.ConvertDataString("NameCompany");
            _localeRow.PhonesCompany = convertData.ConvertDataString("PhonesCompany");
            _localeRow.AdressCompany = convertData.ConvertDataString("AdressCompany");
            _localeRow.ManagerName = convertData.ConvertDataString("ManagerName");
            _localeRow.PhonesManager = convertData.ConvertDataString("PhonesManager");
            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");          
            _localeRow.CreatedUserID = convertData.ConvertDataInt32("CreatedUserID");
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("LastModificatedUserID");

            return _localeRow;
        }

        public DeliveryCompany ConvertDelivery(DataRow _dataRow, DeliveryCompany _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("DeliveryID");
            _localeRow.Description = convertData.ConvertDataString("NameCompany");
            _localeRow.ShortDescription = convertData.ConvertDataString("NameCompany");
            _localeRow.Phones= convertData.ConvertDataString("PhonesCompany");
            _localeRow.AdressCompany = convertData.ConvertDataString("AdressCompany");

            return _localeRow;
        }

    }
}
