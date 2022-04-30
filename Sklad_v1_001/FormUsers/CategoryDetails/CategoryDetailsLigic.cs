using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.CategoryDetails
{
    public class LocalRow : INotifyPropertyChanged
    {
        private Int32 iD;     
        private Int32 categoryID;
        private String categoryName;
        private String description;

        private DateTime? createdDate;
        private String createdDateString;
        private Int32 createdUserID;
        private String createdUserIDString;
        private DateTime? lastModificatedDate;
        private String lastModificatedDateString;
        private Int32 lastModificatedUserID;
        private String lastModificatedUserIDString;


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

        public Int32 CategoryID
        {
            get
            {
                return categoryID;
            }

            set
            {
                categoryID = value;
                OnPropertyChanged("CategoryID");
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

        public string CreatedDateString
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

        public Int32 CreatedUserID
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

        public string CreatedUserIDString
        {
            get
            {
                return createdUserIDString;
            }

            set
            {
                createdUserIDString = value;
                OnPropertyChanged("CreatedUserIDString");
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
                OnPropertyChanged("LastModificatedDate");
            }
        }

        public string LastModificatedDateString
        {
            get
            {
                return lastModificatedDateString;
            }

            set
            {
                lastModificatedDateString = value;
                OnPropertyChanged("LastModificatedDateString");
            }
        }

        public Int32 LastModificatedUserID
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

        public string LastModificatedUserIDString
        {
            get
            {
                return lastModificatedUserIDString;
            }

            set
            {
                lastModificatedUserIDString = value;
                OnPropertyChanged("LastModificatedUserIDString");
            }
        }

        public String CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CategoryDetailsLigic
    {
        Attributes attributes;
        ConvertData convertData;

        string save_store_procedure = "xp_SaveCategoryDetails";
        
        SQLCommanSelect _sqlRequestSave = null;
        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;

        public CategoryDetailsLigic(Attributes _attributes)
        {
            this.attributes = _attributes;

            _data = new DataTable();
            _datarow = new DataTable();

            _sqlRequestSave = new SQLCommanSelect();
            
            //----------------------------------------------------------------------------

            _sqlRequestSave.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_AddUserID", attributes.numeric.userEdit.AddUserID);

            _sqlRequestSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ID", 0);

            _sqlRequestSave.AddParametr("@p_CategoryID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_CategoryID", 0);

            _sqlRequestSave.AddParametr("@p_Name", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Name", "");

            _sqlRequestSave.AddParametr("@p_Description", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_Description", "");
        }

        public LocalRow Convert(DataRow _dataRow, LocalRow _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("CategoryID");
            _localeRow.CategoryName = convertData.ConvertDataString("CategoryName");
            _localeRow.Description = convertData.ConvertDataString("CategoryDescription"); // наименование подкатегории

            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModificatedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModificatedDate);
            _localeRow.CreatedUserID = convertData.ConvertDataInt32("CreatedUserID");
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("LastModificatedUserID");
            _localeRow.CreatedUserIDString = convertData.ConvertDataString("CreatedUserIDString");
            _localeRow.LastModificatedUserIDString = convertData.ConvertDataString("LastModificatedUserIDString");

            return _localeRow;
        }

        public GlobalList.CategoryDetails ConvertCategoryDetails(DataRow _dataRow, GlobalList.CategoryDetails _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("CategoryDetailsID");
            _localeRow.Description = convertData.ConvertDataString("CategoryDetailsName");
            _localeRow.CategoryID= convertData.ConvertDataInt32("CategoryID");
            _localeRow.CategoryIDString = convertData.ConvertDataString("CategoryName");
            _localeRow.Name = convertData.ConvertDataString("CategoryDetailsDescription");

            return _localeRow;
        }

        public Int32 SaveRow(GlobalList.CategoryDetails row)
        {
            _sqlRequestSave.SetParametrValue("@p_ID", row.ID);
            _sqlRequestSave.SetParametrValue("@p_CategoryID", row.CategoryID);
            _sqlRequestSave.SetParametrValue("@p_Description", row.Name);
            _sqlRequestSave.SetParametrValue("@p_Name", row.Description);

            _sqlRequestSave.ComplexRequest(save_store_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSave.SqlAnswer.result;
        }
    }
}
