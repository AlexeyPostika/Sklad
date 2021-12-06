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

namespace Sklad_v1_001.FormUsers.DeliveryDetails
{
    public class LocaleRow : IAbstractRow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Int32 iD;
        private String invoice;
        private Byte[] invoiceDocumentByte;
        private String tTN;
        private Byte[] tTNDocumentByte;

        private String nameCompany;
        private String phones;
        private String adress;
        private DateTime? createdDate;
        private Int32 createdUserID;
        private DateTime? lastModicatedDate;
        private Int32 lastModicatedUserID;

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
    public class DeliveryDetailsLogic
    {
        ConvertData convertData;

        string get_store_procedure = "xp_GetDeliveryCompanyDetailsTable";

        SQLCommanSelect _sqlRequestSelect = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;
        public DeliveryDetailsLogic()
        {
            convertData = new ConvertData();
            _data = new DataTable();
            _datarow = new DataTable();

            _sqlRequestSelect = new SQLCommanSelect();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);
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

        public LocaleRow Convert(DataRow _dataRow, LocaleRow _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("ID");
            //_localeRow.CategoryName = convertData.ConvertDataString("CategoryName");
            //_localeRow.CategoryDescription = convertData.ConvertDataString("CategoryDescription");
            //_localeRow.Description = convertData.ConvertDataString("Description");
            //_localeRow.CategoryID = convertData.ConvertDataInt32("CategoryID");

            //_localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            //_localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            //_localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            //_localeRow.LastModificatedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModificatedDate);
            //_localeRow.CreatedUserID = convertData.ConvertDataInt32("CreatedUserID");
            //_localeRow.LastModificatedUserID = convertData.ConvertDataInt32("LastModificatedUserID");
            //_localeRow.CreatedUserIDString = convertData.ConvertDataString("CreatedUserIDString");
            //_localeRow.LastModificatedUserIDString = convertData.ConvertDataString("LastModificatedUserIDString");

            return _localeRow;
        }
        public ManagerDelivery ConvertDelivery(DataRow _dataRow, ManagerDelivery _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("ID");
            _localeRow.Description = convertData.ConvertDataString("ManagerName");
            _localeRow.ShortDescription = convertData.ConvertDataString("ManagerName");

            return _localeRow;
        }
    }
}
