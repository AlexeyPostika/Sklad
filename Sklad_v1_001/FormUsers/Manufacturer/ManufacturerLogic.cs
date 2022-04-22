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

namespace Sklad_v1_001.FormUsers.Manufacturer
{
    public class LocalFilter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string screenTypeGrid;
        private string search;
        private string isActive;

        private String sortColumn;
        private Boolean sort;

        public string ScreenTypeGrid
        {
            get
            {
                return screenTypeGrid;
            }

            set
            {
                screenTypeGrid = value;
                OnPropertyChanged("ScreenTypeGrid");
            }
        }

        public string Search
        {
            get
            {
                return search;
            }

            set
            {
                search = value;
                OnPropertyChanged("Search");
            }
        }

        public string IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public string SortColumn
        {
            get
            {
                return sortColumn;
            }

            set
            {
                sortColumn = value;
                OnPropertyChanged("SortColumn");
            }
        }

        public Boolean Sort
        {
            get
            {
                return sort;
            }

            set
            {
                sort = value;
                OnPropertyChanged("Sort");
            }
        }

        public LocalFilter()
        {
            ScreenTypeGrid = ScreenType.ScreenTypeGrid;

            Search = String.Empty;
            IsActive = "All";

            Sort = true;
            SortColumn = "Name";
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
        private String name;
        private String description;
        private Boolean isActive;

        private Int32 manufacturerID;

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

        public Int32 ManufacturerID
        {
            get
            {
                return manufacturerID;
            }

            set
            {
                manufacturerID = value;
                OnPropertyChanged("ManufacturerID");
            }
        }
        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged("Name");
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
        public Boolean IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }
    }
    public class ManufacturerLogic
    {
        ConvertData convertData;

        string get_store_procedure = "xp_GetManufacturerTable";
        string save_store_procedure = "xp_SaveManufacturer";

        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlRequestSave = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;

        public ManufacturerLogic()
        {
            convertData = new ConvertData();
            _data = new DataTable();
            _datarow = new DataTable();

            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSave = new SQLCommanSelect();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.AddParametr("@p_Search", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_Search", "");

            _sqlRequestSelect.AddParametr("@p_IsActive", SqlDbType.NVarChar);
            _sqlRequestSelect.SetParametrValue("@p_IsActive", "");

            _sqlRequestSelect.AddParametr("@p_SortColumn", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", 0);

            _sqlRequestSelect.AddParametr("@p_Sort", SqlDbType.Bit);
            _sqlRequestSelect.SetParametrValue("@p_Sort", 0);
        }

        public DataTable FillGrid(LocalFilter localFilter)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);
            _sqlRequestSelect.SetParametrValue("@p_Search", localFilter.Search);
            _sqlRequestSelect.SetParametrValue("@p_IsActive", localFilter.IsActive);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", localFilter.SortColumn);
            _sqlRequestSelect.SetParametrValue("@p_Sort", localFilter.Sort);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public LocaleRow Convert(DataRow _dataRow, LocaleRow _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ManufacturerID = convertData.ConvertDataInt32("ID");
            _localeRow.Name = convertData.ConvertDataString("Name");
            _localeRow.Description = convertData.ConvertDataString("Description");
            _localeRow.IsActive = convertData.ConvertDataBoolean("IsActive");

            return _localeRow;
        }

        public LocaleRow ConvertComboBox(DataRow _dataRow, LocaleRow _localeRow)
        {
            convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("ID");
            _localeRow.Name = convertData.ConvertDataString("Description");
            _localeRow.Description = convertData.ConvertDataString("Name");
            _localeRow.IsActive = convertData.ConvertDataBoolean("IsActive");

            return _localeRow;
        }
    }
}
