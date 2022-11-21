using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sklad_v1_001.FormUsers.Users;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.SQL;
using System.Data.SqlTypes;
using Sklad_v1_001.HelperGlobal.StoreAPI.Model.Company;

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
        private Int32 lineDocument;
        private String fullCompanyName;
        private String shortCompanyName;
        private String adress;
        private String phone;
        private Byte[] logo;
        private Boolean active;       
        private FormUsers.Users.LocalRow generalDirectory;
        private FormUsers.Users.LocalRow seniorAccount;
        private Shops.LocaleRow shop;

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

        private DateTime? createdDate;
        private String createdDateString;
        private DateTime? lastModifiedDate;
        private String lastModifiedDateString;
        private Int32 createdByUserID;
        private Int32 lastModifiedByUserID;
        public DateTime? syncDate;
        public Int32 syncStatus;
        public Int32 reffID;

        public String description;

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
        public int LineDocument
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
        public Users.LocalRow SeniorAccount
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
       
        public Shops.LocaleRow Shop
        {
            get
            {
                return shop;
            }

            set
            {
                shop = value;
                OnPropertyChanged("Shop");
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
        public Int32 ReffID
        {
            get
            {
                return reffID;
            }

            set
            {
                reffID = value;
                OnPropertyChanged("ReffID");
            }
        }
        public LocaleRow()
        {
            GeneralDirectory = new LocalRow();
            SeniorAccount = new LocalRow();
            Shop = new Shops.LocaleRow();
            CreatedDate = DateTime.Now;
            LastModifiedDate= DateTime.Now;
            ReffID = 0;
        }
    }
    public class CompanyLogic
    {
        Attributes attributes;
        ConvertData convertData;

        string get_store_procedure = "xp_GetCompanyTable";
        string get_save_procedure = "xp_SaveCompany";

        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlResponseSave = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;

        public CompanyLogic(Attributes _attributes)
        {
            this.attributes = _attributes;

            _sqlRequestSelect = new SQLCommanSelect();
            _sqlResponseSave = new SQLCommanSelect();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.AddParametr("@p_Search", SqlDbType.NVarChar, 40);
            _sqlRequestSelect.SetParametrValue("@p_Search", "");

            _sqlRequestSelect.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_ID", 0);

            _sqlRequestSelect.AddParametr("@p_CreatedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_CreatedUserID", "");

            _sqlRequestSelect.AddParametr("@p_LastModifiedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID", "");

            _sqlRequestSelect.AddParametr("@p_Status", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_Status", "");

            _sqlRequestSelect.AddParametr("@p_ManagerUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_ManagerUserID", "");

            _sqlRequestSelect.AddParametr("@p_DeliveryID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_DeliveryID", "");

            _sqlRequestSelect.AddParametr("@p_Quantity_Min", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Min", 0);

            _sqlRequestSelect.AddParametr("@p_Quantity_Max", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Max", SqlInt32.MaxValue);

            _sqlRequestSelect.AddParametr("@p_TagPriceVATRUS_Min", SqlDbType.Decimal);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Min", SqlDecimal.MaxValue);

            _sqlRequestSelect.AddParametr("@p_TagPriceVATRUS_Max", SqlDbType.Decimal);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Max", SqlDecimal.MaxValue);

            //----------------------------------------------------------------------------
            _sqlResponseSave.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlResponseSave.SetParametrValue("@p_AddUserID", attributes.numeric.userEdit.AddUserID);

            _sqlResponseSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlResponseSave.SetParametrValue("@p_ID", 0);

            _sqlResponseSave.AddParametr("@p_INN", SqlDbType.NVarChar, 255);
            _sqlResponseSave.SetParametrValue("@p_INN", "");

            _sqlResponseSave.AddParametr("@p_KPP", SqlDbType.NVarChar, 255);
            _sqlResponseSave.SetParametrValue("@p_KPP", "");

            _sqlResponseSave.AddParametr("@p_FullCompanyName", SqlDbType.NVarChar, 50);
            _sqlResponseSave.SetParametrValue("@p_FullCompanyName", "");

            _sqlResponseSave.AddParametr("@p_ShortCompanyNane", SqlDbType.NVarChar, 50);
            _sqlResponseSave.SetParametrValue("@p_ShortCompanyNane", "");

            _sqlResponseSave.AddParametr("@p_Adress", SqlDbType.NVarChar, 255);
            _sqlResponseSave.SetParametrValue("@p_Adress", "");

            _sqlResponseSave.AddParametr("@p_GeneralDirectory", SqlDbType.Int);
            _sqlResponseSave.SetParametrValue("@p_GeneralDirectory", 0);

            _sqlResponseSave.AddParametr("@p_Phone", SqlDbType.NVarChar, 50);
            _sqlResponseSave.SetParametrValue("@p_Phone", "");

            _sqlResponseSave.AddParametr("@p_CorrespondentAccount", SqlDbType.NVarChar, 128);
            _sqlResponseSave.SetParametrValue("@p_CorrespondentAccount", "");

            _sqlResponseSave.AddParametr("@p_SeniorAccount", SqlDbType.NVarChar, 128);
            _sqlResponseSave.SetParametrValue("@p_SeniorAccount", "");

            _sqlResponseSave.AddParametr("@p_SentlementAccount", SqlDbType.NVarChar, 128);
            _sqlResponseSave.SetParametrValue("@p_SentlementAccount", "");

            _sqlResponseSave.AddParametr("@p_RCBIC", SqlDbType.NVarChar, 50);
            _sqlResponseSave.SetParametrValue("@p_RCBIC", "");

            _sqlResponseSave.AddParametr("@p_BankName", SqlDbType.NVarChar, 50);
            _sqlResponseSave.SetParametrValue("@p_BankName", "");

            _sqlResponseSave.AddParametr("@p_BankAdress", SqlDbType.NVarChar, 255);
            _sqlResponseSave.SetParametrValue("@p_BankAdress", "");

            _sqlResponseSave.AddParametr("@p_TaxRate", SqlDbType.Int);
            _sqlResponseSave.SetParametrValue("@p_TaxRate", 0);

            _sqlResponseSave.AddParametr("@p_CurrencyName", SqlDbType.NVarChar, 50);
            _sqlResponseSave.SetParametrValue("@p_CurrencyName", "");

            _sqlResponseSave.AddParametr("@p_CurrencyCode", SqlDbType.Int);
            _sqlResponseSave.SetParametrValue("@p_CurrencyCode", 0);

            _sqlResponseSave.AddParametr("@p_Description", SqlDbType.NVarChar, 255);
            _sqlResponseSave.SetParametrValue("@p_Description", "");

            _sqlResponseSave.AddParametr("@p_CreatedUserID", SqlDbType.Int);
            _sqlResponseSave.SetParametrValue("@p_CreatedUserID", 0);

            _sqlResponseSave.AddParametr("@p_LastModificatedDate", SqlDbType.DateTime);
            _sqlResponseSave.SetParametrValue("@p_LastModificatedDate", DateTime.Now);

            _sqlResponseSave.AddParametr("@p_LastModificatedUserID", SqlDbType.Int);
            _sqlResponseSave.SetParametrValue("@p_LastModificatedUserID", 0);

            _sqlResponseSave.AddParametr("@p_logo", SqlDbType.VarBinary);
            _sqlResponseSave.SetParametrValue("@p_logo", null);

            _sqlResponseSave.AddParametr("@p_Active", SqlDbType.Bit);
            _sqlResponseSave.SetParametrValue("@p_Active", 1);
        }

        public DataTable FillGrid(Int32 id)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ItemByStatus);
            _sqlRequestSelect.SetParametrValue("@p_ID", id);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        //public DataTable FillGrid(LocalFilter _localFilter)
        //{
        //    _sqlRequestSelect.SqlAnswer.datatable.Clear();
        //    _data.Clear();

        //    _sqlRequestSelect.SetParametrValue("@p_TypeScreen", _localFilter.ScreenTypeGrid);
        //    _sqlRequestSelect.SetParametrValue("@p_Search", _localFilter.Search);
        //    _sqlRequestSelect.SetParametrValue("@p_ID", _localFilter.ID);
        //    _sqlRequestSelect.SetParametrValue("@p_CreatedUserID", _localFilter.CreatedByUserID);
        //    _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID", _localFilter.LastModifiedByUserID);
        //    _sqlRequestSelect.SetParametrValue("@p_Status", _localFilter.Status);
        //    _sqlRequestSelect.SetParametrValue("@p_ManagerUserID", _localFilter.ManagerUserID);
        //    _sqlRequestSelect.SetParametrValue("@p_DeliveryID", _localFilter.DeliveryID);
        //    _sqlRequestSelect.SetParametrValue("@p_Quantity_Min", _localFilter.QuantityMin);
        //    _sqlRequestSelect.SetParametrValue("@p_Quantity_Max", _localFilter.QuantityMax);
        //    _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Min", _localFilter.AmountMin);
        //    _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Max", _localFilter.AmountMax);
        //    //_sqlRequestSelect.SetParametrValue("@p_FromCreatedDate", _localFilter.FromCreatedDate);
        //    //_sqlRequestSelect.SetParametrValue("@p_ToCreatedDate", _localFilter.ToCreatedDate);
        //    //_sqlRequestSelect.SetParametrValue("@p_FromLastModifiedDate", _localFilter.FromLastModifiedDate);
        //    //_sqlRequestSelect.SetParametrValue("@p_ToLastModifiedDate", _localFilter.ToLastModifiedDate);
        //    _sqlRequestSelect.SetParametrValue("@p_PageNumber", _localFilter.PageNumber);
        //    _sqlRequestSelect.SetParametrValue("@p_PagerowCount", _localFilter.PagerowCount);
        //    _sqlRequestSelect.SetParametrValue("@p_SortColumn", _localFilter.SortColumn);
        //    _sqlRequestSelect.SetParametrValue("@p_Sort", _localFilter.Sort); //тест github

        //    _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
        //    _data = _sqlRequestSelect.SqlAnswer.datatable;
        //    return _data;
        //}

        public Int32 SaveRow(LocaleRow row)
        {
            _sqlResponseSave.SetParametrValue("@p_ID", row.ID);
            _sqlResponseSave.SetParametrValue("@p_INN", row.INN);
            _sqlResponseSave.SetParametrValue("@p_KPP", row.KPP);
            _sqlResponseSave.SetParametrValue("@p_FullCompanyName", row.FullCompanyName);
            _sqlResponseSave.SetParametrValue("@p_ShortCompanyNane", row.ShortCompanyName);
            _sqlResponseSave.SetParametrValue("@p_Adress", row.Adress);
            _sqlResponseSave.SetParametrValue("@p_GeneralDirectory", row.GeneralDirectory.ID);
            _sqlResponseSave.SetParametrValue("@p_Phone", row.Phone);
            _sqlResponseSave.SetParametrValue("@p_CorrespondentAccount", row.CorrespondentAccount);
            _sqlResponseSave.SetParametrValue("@p_SeniorAccount", row.SeniorAccount.ID);
            _sqlResponseSave.SetParametrValue("@p_SentlementAccount", row.SenttlementAccount);
            _sqlResponseSave.SetParametrValue("@p_RCBIC", row.RCBIC);
            _sqlResponseSave.SetParametrValue("@p_BankName", row.BancName);
            _sqlResponseSave.SetParametrValue("@p_BankAdress", row.BancAdress);
            _sqlResponseSave.SetParametrValue("@p_TaxRate", row.TaxRate);
            _sqlResponseSave.SetParametrValue("@p_CurrencyName", row.CurrentName);
            _sqlResponseSave.SetParametrValue("@p_CurrencyCode", row.CurrentCode);

            _sqlResponseSave.SetParametrValue("@p_Description", row.Description);
            _sqlResponseSave.SetParametrValue("@p_CreatedUserID", row.CreatedByUserID);
            _sqlResponseSave.SetParametrValue("@p_LastModificatedDate", row.LastModifiedDate);
            _sqlResponseSave.SetParametrValue("@p_LastModificatedUserID", row.LastModifiedByUserID);
            _sqlResponseSave.SetParametrValue("@p_logo", row.Logo);
            _sqlResponseSave.SetParametrValue("@p_Active", row.Active);

            _sqlResponseSave.ComplexRequest(get_save_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlResponseSave.SqlAnswer.result;
        }

        public LocaleRow Convert(DataRow _dataRow, LocaleRow _localeRow)
        {
            //SupplyTypeList supplyTypeList = new SupplyTypeList();
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);          
            _localeRow.ID = convertData.ConvertDataInt32("ID");
            _localeRow.INN = convertData.ConvertDataString("INN");           
            _localeRow.LineDocument = convertData.ConvertDataInt32("RowNumber");
            _localeRow.KPP = convertData.ConvertDataString("KPP");
             _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            _localeRow.LastModifiedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModifiedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModifiedDate);
            _localeRow.CreatedByUserID = convertData.ConvertDataInt32("CreatedByUserID");
            _localeRow.LastModifiedByUserID = convertData.ConvertDataInt32("LastModifiedByUserID");
            _localeRow.FullCompanyName = convertData.ConvertDataString("FullCompanyName");
            _localeRow.Adress = convertData.ConvertDataString("Adress");
            
            _localeRow.Phone = convertData.ConvertDataString("Phone");
            _localeRow.CorrespondentAccount = convertData.ConvertDataString("CorrespondentAccount");
           
            _localeRow.SenttlementAccount = convertData.ConvertDataString("SenttlementAccount");
            _localeRow.RCBIC = convertData.ConvertDataString("RCBIC");
            _localeRow.BancName = convertData.ConvertDataString("BankName");
            _localeRow.BancAdress = convertData.ConvertDataString("BankAdress");
            _localeRow.TaxRate = convertData.ConvertDataInt32("TaxRate");
            _localeRow.CurrentName = convertData.ConvertDataString("CurrencyName"); 
            _localeRow.CurrentCode = convertData.ConvertDataInt32("CurrencyCode");
            _localeRow.Description = convertData.ConvertDataString("Description");
            _localeRow.Active = convertData.ConvertDataBoolean("Active");
            // _localeRow.TimeRow = convertData.ConvertDataString("TimeRow");
            //_localeRow.logo = convertData.ConvertDataString("logo");
            // _localeRow.SeniorAccount = convertData.ConvertDataString("SeniorAccount");
            //_localeRow.GeneralDirectory = convertData.ConvertDataString("GeneralDirectory");
            //_localeRow.StatusString = supplyTypeList.innerList.FirstOrDefault(x => x.ID == _localeRow.Status) != null ?
            //                                supplyTypeList.innerList.FirstOrDefault(x => x.ID == _localeRow.Status).Description : Properties.Resources.UndefindField;

            //_localeRow.SupplyDocumentNumberString = "";
            //if (_localeRow.SupplyDocumentNumber > 0)
            //    _localeRow.SupplyDocumentNumberString = _localeRow.SupplyDocumentNumber.ToString();




            return _localeRow;
        }
        public CompanyRequest Convert(LocaleRow _localeRow, CompanyRequest _companyRequest)
        {
            _companyRequest.company.active = _localeRow.Active;
            _companyRequest.company.addUserID = attributes.numeric.userEdit.AddUserID;
            _companyRequest.company.adress = _localeRow.Adress;
            _companyRequest.company.bancAdress = _localeRow.BancAdress;
            _companyRequest.company.bancName = _localeRow.BancName;
            _companyRequest.company.correspondentAccount = _localeRow.CorrespondentAccount;
            _companyRequest.company.currentCode = _localeRow.CurrentCode;
            _companyRequest.company.currentName = _localeRow.CurrentName;
            _companyRequest.company.description = _localeRow.Description;
            _companyRequest.company.fullCompanyName = _localeRow.FullCompanyName;
            _companyRequest.company.generalDirectory.active = _localeRow.GeneralDirectory.Active;
            _companyRequest.company.generalDirectory.birthday = _localeRow.GeneralDirectory.Birthday;
            _companyRequest.company.generalDirectory.companyID = _localeRow.GeneralDirectory.CompanyID;
            _companyRequest.company.generalDirectory.createdByUserID = _localeRow.CreatedByUserID;
            _companyRequest.company.generalDirectory.createdDate = _localeRow.CreatedDate;
            _companyRequest.company.generalDirectory.createdDateString = _localeRow.CreatedDateString;
            _companyRequest.company.generalDirectory.description = _localeRow.GeneralDirectory.Description;
            _companyRequest.company.generalDirectory.email = _localeRow.GeneralDirectory.Email;
            _companyRequest.company.generalDirectory.firstName = _localeRow.GeneralDirectory.FirstName;
            _companyRequest.company.generalDirectory.genderID = _localeRow.GeneralDirectory.GenderID;
            _companyRequest.company.generalDirectory.iD = _localeRow.GeneralDirectory.ID;
            _companyRequest.company.generalDirectory.iNN = _localeRow.GeneralDirectory.INN;
            _companyRequest.company.generalDirectory.lastModifiedByUserID = _localeRow.GeneralDirectory.LastModifiedByUserID;
            _companyRequest.company.generalDirectory.lastModifiedDate = _localeRow.GeneralDirectory.LastModifiedDate;
            _companyRequest.company.generalDirectory.lastModifiedDateString = _localeRow.GeneralDirectory.LastModifiedDateString;
            _companyRequest.company.generalDirectory.lastName = _localeRow.GeneralDirectory.LastName;
            _companyRequest.company.generalDirectory.login = _localeRow.GeneralDirectory.Login;
            _companyRequest.company.generalDirectory.number = _localeRow.GeneralDirectory.Number;
            _companyRequest.company.generalDirectory.password = _localeRow.GeneralDirectory.Password;
            _companyRequest.company.generalDirectory.phone = _localeRow.GeneralDirectory.Phone;
            _companyRequest.company.generalDirectory.roleID = _localeRow.GeneralDirectory.RoleID;
            _companyRequest.company.generalDirectory.secondName = _localeRow.GeneralDirectory.SecondName;
            _companyRequest.company.generalDirectory.userID = _localeRow.GeneralDirectory.UserID;
            _companyRequest.company.iD = _localeRow.ID;
            _companyRequest.company.iNN = _localeRow.INN;
            _companyRequest.company.kPP = _localeRow.KPP;
            _companyRequest.company.logo = _localeRow.Logo;
            _companyRequest.company.phone = _localeRow.Phone;
            _companyRequest.company.rCBIC = _localeRow.RCBIC;           
            _companyRequest.company.seniorAccount.active = _localeRow.SeniorAccount.Active;
            _companyRequest.company.seniorAccount.birthday = _localeRow.SeniorAccount.Birthday;
            _companyRequest.company.seniorAccount.companyID = _localeRow.SeniorAccount.CompanyID;
            _companyRequest.company.seniorAccount.createdByUserID = _localeRow.SeniorAccount.CreatedByUserID;
            _companyRequest.company.seniorAccount.createdDate = _localeRow.SeniorAccount.CreatedDate;
            _companyRequest.company.seniorAccount.createdDateString = _localeRow.SeniorAccount.CreatedDateString;
            _companyRequest.company.seniorAccount.description = _localeRow.SeniorAccount.Description;
            _companyRequest.company.seniorAccount.email = _localeRow.SeniorAccount.Email;
            _companyRequest.company.seniorAccount.firstName = _localeRow.SeniorAccount.FirstName;
            _companyRequest.company.seniorAccount.genderID = _localeRow.SeniorAccount.GenderID;
            _companyRequest.company.seniorAccount.iD = _localeRow.SeniorAccount.ID;
            _companyRequest.company.seniorAccount.iNN = _localeRow.SeniorAccount.INN;
            _companyRequest.company.seniorAccount.lastModifiedByUserID = _localeRow.SeniorAccount.LastModifiedByUserID;
            _companyRequest.company.seniorAccount.lastModifiedDate = _localeRow.SeniorAccount.LastModifiedDate;
            _companyRequest.company.seniorAccount.lastModifiedDateString = _localeRow.SeniorAccount.LastModifiedDateString;
            _companyRequest.company.seniorAccount.lastName = _localeRow.SeniorAccount.LastName;
            _companyRequest.company.seniorAccount.login = _localeRow.SeniorAccount.Login;
            _companyRequest.company.seniorAccount.number = _localeRow.SeniorAccount.Number;
            _companyRequest.company.seniorAccount.password = _localeRow.SeniorAccount.Password;
            _companyRequest.company.seniorAccount.phone = _localeRow.SeniorAccount.Phone;
            _companyRequest.company.seniorAccount.roleID = _localeRow.SeniorAccount.RoleID;
            _companyRequest.company.seniorAccount.secondName = _localeRow.SeniorAccount.SecondName;
            _companyRequest.company.seniorAccount.userID = _localeRow.SeniorAccount.UserID;
            _companyRequest.company.senttlementAccount = _localeRow.SenttlementAccount;
            _companyRequest.company.shop.address = _localeRow.Shop.Address;
            _companyRequest.company.shop.addUserID = attributes.numeric.userEdit.AddUserID;
            _companyRequest.company.shop.companyID = _localeRow.Shop.CompanyID;
            _companyRequest.company.shop.createdUserID = _localeRow.Shop.CreatedByUserID;
            _companyRequest.company.shop.iD = _localeRow.Shop.ID;
            _companyRequest.company.shop.lastModificatedDate = _localeRow.Shop.LastModifiedDate;
            _companyRequest.company.shop.lastModificatedUserID = _localeRow.Shop.LastModifiedByUserID;
            _companyRequest.company.shop.Name = _localeRow.Shop.Name;
            _companyRequest.company.shop.phone = _localeRow.Shop.Phone;
            _companyRequest.company.shop.shopNumber = _localeRow.Shop.ShopNumber;
            _companyRequest.company.shop.TimeRow = _localeRow.Shop.TimeRow;
            _companyRequest.company.shortCompanyName = _localeRow.ShortCompanyName;
            _companyRequest.company.taxRate = _localeRow.TaxRate;
            return _companyRequest;
        }

        public LocaleRow Convert(CompanyRequest _companyRequest, LocaleRow _localeRow)
        {
            _localeRow.Active = _companyRequest.company.active;
            _localeRow.Adress = _companyRequest.company.adress;
            _localeRow.BancAdress = _companyRequest.company.bancAdress;
            _localeRow.BancName = _companyRequest.company.bancName;
            _localeRow.CorrespondentAccount = _companyRequest.company.correspondentAccount;
            _localeRow.CurrentCode = _companyRequest.company.currentCode;
            _localeRow.CurrentName = _companyRequest.company.currentName;
            _localeRow.Description = _companyRequest.company.description;
            _localeRow.FullCompanyName = _companyRequest.company.fullCompanyName;
            _localeRow.GeneralDirectory.Active = _companyRequest.company.generalDirectory.active;
            _localeRow.GeneralDirectory.Birthday = _companyRequest.company.generalDirectory.birthday;
            _localeRow.GeneralDirectory.CompanyID = _companyRequest.company.generalDirectory.companyID;
            _localeRow.GeneralDirectory.CreatedByUserID = _companyRequest.company.generalDirectory.createdByUserID;
            _localeRow.GeneralDirectory.CreatedDate = _companyRequest.company.generalDirectory.createdDate;
            _localeRow.GeneralDirectory.CreatedDateString = _companyRequest.company.generalDirectory.createdDateString;
            _localeRow.GeneralDirectory.Description = _companyRequest.company.generalDirectory.description;
            _localeRow.GeneralDirectory.Email = _companyRequest.company.generalDirectory.email;
            _localeRow.GeneralDirectory.FirstName = _companyRequest.company.generalDirectory.firstName;
            _localeRow.GeneralDirectory.GenderID = _companyRequest.company.generalDirectory.genderID;
            _localeRow.GeneralDirectory.ID = _companyRequest.company.generalDirectory.iD;
            _localeRow.GeneralDirectory.INN = _companyRequest.company.generalDirectory.iNN;
            _localeRow.GeneralDirectory.LastModifiedByUserID = _companyRequest.company.generalDirectory.lastModifiedByUserID;
            _localeRow.GeneralDirectory.LastModifiedDate = _companyRequest.company.generalDirectory.lastModifiedDate;
            _localeRow.GeneralDirectory.LastModifiedDateString = _companyRequest.company.generalDirectory.lastModifiedDateString;
            _localeRow.GeneralDirectory.LastName = _companyRequest.company.generalDirectory.lastName;
            _localeRow.GeneralDirectory.Login = _companyRequest.company.generalDirectory.login;
            _localeRow.GeneralDirectory.Number = _companyRequest.company.generalDirectory.number;
            _localeRow.GeneralDirectory.Password = _companyRequest.company.generalDirectory.password;
            _localeRow.GeneralDirectory.Phone = _companyRequest.company.generalDirectory.phone;
            _localeRow.GeneralDirectory.RoleID = _companyRequest.company.generalDirectory.roleID;
            _localeRow.GeneralDirectory.SecondName = _companyRequest.company.generalDirectory.secondName;
            _localeRow.GeneralDirectory.UserID = _companyRequest.company.generalDirectory.userID;
            _localeRow.ID = _companyRequest.company.iD;
            _localeRow.INN = _companyRequest.company.iNN;
            _localeRow.KPP = _companyRequest.company.kPP;
            _localeRow.Logo = _companyRequest.company.logo;
            _localeRow.Phone = _companyRequest.company.phone;
            _localeRow.RCBIC = _companyRequest.company.rCBIC;
            _localeRow.SeniorAccount.Active = _companyRequest.company.seniorAccount.active;
            _localeRow.SeniorAccount.Birthday = _companyRequest.company.seniorAccount.birthday;
            _localeRow.SeniorAccount.CompanyID = _companyRequest.company.seniorAccount.companyID;
            _localeRow.SeniorAccount.CreatedByUserID = _companyRequest.company.seniorAccount.createdByUserID;
            _localeRow.SeniorAccount.CreatedDate = _companyRequest.company.seniorAccount.createdDate;
            _localeRow.SeniorAccount.CreatedDateString = _companyRequest.company.seniorAccount.createdDateString;
            _localeRow.SeniorAccount.Description = _companyRequest.company.seniorAccount.description;
            _localeRow.SeniorAccount.Email = _companyRequest.company.seniorAccount.email;
            _localeRow.SeniorAccount.FirstName = _companyRequest.company.seniorAccount.firstName;
            _localeRow.SeniorAccount.GenderID = _companyRequest.company.seniorAccount.genderID;
            _localeRow.SeniorAccount.ID = _companyRequest.company.seniorAccount.iD;
            _localeRow.SeniorAccount.INN = _companyRequest.company.seniorAccount.iNN;
            _localeRow.SeniorAccount.LastModifiedByUserID = _companyRequest.company.seniorAccount.lastModifiedByUserID;
            _localeRow.SeniorAccount.LastModifiedDate = _companyRequest.company.seniorAccount.lastModifiedDate;
            _localeRow.SeniorAccount.LastModifiedDateString = _companyRequest.company.seniorAccount.lastModifiedDateString;
            _localeRow.SeniorAccount.LastName = _companyRequest.company.seniorAccount.lastName;
            _localeRow.SeniorAccount.Login = _companyRequest.company.seniorAccount.login;
            _localeRow.SeniorAccount.Number = _companyRequest.company.seniorAccount.number;
            _localeRow.SeniorAccount.Password = _companyRequest.company.seniorAccount.password;
            _localeRow.SeniorAccount.Phone = _companyRequest.company.seniorAccount.phone;
            _localeRow.SeniorAccount.RoleID = _companyRequest.company.seniorAccount.roleID;
            _localeRow.SeniorAccount.SecondName = _companyRequest.company.seniorAccount.secondName;
            _localeRow.SeniorAccount.UserID = _companyRequest.company.seniorAccount.userID;
            _localeRow.SenttlementAccount = _companyRequest.company.senttlementAccount;
            _localeRow.Shop.Address = _companyRequest.company.shop.address;
            _localeRow.Shop.AddUserID = attributes.numeric.userEdit.AddUserID;
            _localeRow.Shop.CompanyID = _companyRequest.company.shop.companyID;         
            _localeRow.Shop.ID = _companyRequest.company.shop.iD;
            _localeRow.Shop.LastModifiedDate = _companyRequest.company.shop.lastModificatedDate;
            _localeRow.Shop.LastModifiedByUserID = _companyRequest.company.shop.lastModificatedUserID;
            _localeRow.Shop.Name = _companyRequest.company.shop.Name;
            _localeRow.Shop.Phone = _companyRequest.company.shop.phone;
            _localeRow.Shop.ShopNumber = _companyRequest.company.shop.shopNumber;
            _localeRow.Shop.TimeRow = _companyRequest.company.shop.TimeRow;
            _localeRow.ShortCompanyName = _companyRequest.company.shortCompanyName;
            _localeRow.TaxRate = _companyRequest.company.taxRate;
            return _localeRow;
        }
    }
}
