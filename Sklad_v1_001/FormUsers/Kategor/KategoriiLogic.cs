using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Sklad_v1_001.SQL;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.HelperGlobal;
using System.Collections.ObjectModel;

namespace Sklad_v1_001.FormUsers.Kategor
{
    public class LocalFilter : INotifyPropertyChanged
    {
        private Int32 typeTable;                    //страница
      
        public int TypeTable
        {
            get
            {
                return typeTable;
            }

            set
            {
                typeTable = value;
                OnPropertyChanged("TypeTable");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class LocalRow : INotifyPropertyChanged
    {
        private Int32 iD;
        private String description;

        private String massCategoryProduct;
        private String massCategoryIDProduct;
        private String massCategoryDescriptionProduct;
        private String massCategoryRelay;
        private String massCategoryIDRelay;
        private String massCategoryDescriptionRelay;

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

        public string MassCategoryProduct
        {
            get
            {
                return massCategoryProduct;
            }

            set
            {
                massCategoryProduct = value;
                OnPropertyChanged("MassCategoryProduct");
            }
        }

        public string MassCategoryIDProduct
        {
            get
            {
                return massCategoryIDProduct;
            }

            set
            {
                massCategoryIDProduct = value;
                OnPropertyChanged("MassCategoryIDProduct");
            }
        }

        public string MassCategoryDescriptionProduct
        {
            get
            {
                return massCategoryDescriptionProduct;
            }

            set
            {
                massCategoryDescriptionProduct = value;
                OnPropertyChanged("MassCategoryDescriptionProduct");
            }
        }

        public string MassCategoryRelay
        {
            get
            {
                return massCategoryRelay;
            }

            set
            {
                massCategoryRelay = value;
                OnPropertyChanged("MassCategoryRelay");
            }
        }

        public string MassCategoryIDRelay
        {
            get
            {
                return massCategoryIDRelay;
            }

            set
            {
                massCategoryIDRelay = value;
                OnPropertyChanged("MassCategoryIDRelay");
            }
        }

        public string MassCategoryDescriptionRelay
        {
            get
            {
                return massCategoryDescriptionRelay;
            }

            set
            {
                massCategoryDescriptionRelay = value;
                OnPropertyChanged("MassCategoryDescriptionRelay");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class RowSummary : INotifyPropertyChanged
    {
        private Int32 pageCount;

        public int PageCount
        {
            get
            {
                return pageCount;
            }

            set
            {
                pageCount = value;
                OnPropertyChanged("PageCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    //TypeCategory
    public class TypeCategory
    {
        private string title;
        private Int32 iD;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
            }
        }
    }

    public class KategoryType
    {
        private Int32 iD;
        private string kategoryName;
        public ObservableCollection<TypeCategory> Category { get; set; }
        private string typeCategoryName;

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

        public string KategoryName
        {
            get
            {
                return kategoryName;
            }

            set
            {
                kategoryName = value;
                OnPropertyChanged("KategoryName");
            }
        }

        public string TypeCategoryName
        {
            get
            {
                return typeCategoryName;
            }

            set
            {
                typeCategoryName = value;
                OnPropertyChanged("TypeCategoryName");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public KategoryType()
        {
            Category = new ObservableCollection<TypeCategory>();
        }
    }

    public class KategoriiLogic
    {
        SQLCommanSelect _sqlSting;
        SQLCommanSelect _sqlTreeViewProductSting;
        SQLCommanSelect _sqlTreeViewRelaySting;
        SQLCommanSelect _sqlSave;

        LocalRow localrow;
        
        String _getSelectCategoryTable = "xp_GetCategoryComboBox";      //хранимка xp_GetCategoryDetailsTable
        String _getSelectCategoryProductTreeView = "xp_GetCategoryDetailsTableProduct";
        String _getSelectCategoryRelayTreeView = "xp_GetCategoryDetailsTableRelay";
        String _getSaveCategory = "xp_SaveCategoryDetailsTableProduct";

        DataTable _table;

        ConvertData convertData;
        
        public KategoriiLogic()
        {
            //объявили подключение
            _sqlSting = new SQLCommanSelect();
            _sqlTreeViewProductSting = new SQLCommanSelect();
            _sqlTreeViewRelaySting = new SQLCommanSelect();
            _sqlSave = new SQLCommanSelect();

            //объявили localRow
            localrow = new LocalRow();
            //объявили таблицу куда будем записывать все
            _table = new DataTable();
            //за правильную конвертацию данных
            convertData = new ConvertData();
           
            //объявляем переменные для хранимой процедуры
           _sqlSting.AddParametr("@p_typeTable", SqlDbType.Int);
           _sqlSting.SetParametrValue("@p_typeTable", 0);

            //объявляем переменные для хранимой процедуры Save
            _sqlSave.AddParametr("@p_MassCategoryProduct", SqlDbType.NVarChar,255);
            _sqlSave.SetParametrValue("@p_MassCategoryProduct", "");

            _sqlSave.AddParametr("@p_MassCategoryIDProduct", SqlDbType.NVarChar, 255);
            _sqlSave.SetParametrValue("@p_MassCategoryIDProduct", "");

            _sqlSave.AddParametr("@p_MassDescriptionProduct", SqlDbType.NVarChar, 255);
            _sqlSave.SetParametrValue("@p_MassDescriptionProduct", "");

            _sqlSave.AddParametr("@p_MassCategoryRelay", SqlDbType.NVarChar, 255);
            _sqlSave.SetParametrValue("@p_MassCategoryRelay", "");

            _sqlSave.AddParametr("@p_MassDescriptionRelay", SqlDbType.NVarChar, 255);
            _sqlSave.SetParametrValue("@p_MassDescriptionRelay", "");

            _sqlSave.AddParametr("@p_MassCategoryIDRelay", SqlDbType.NVarChar, 255);
            _sqlSave.SetParametrValue("@p_MassCategoryIDRelay", "");
         
        }
        public DataTable SelectCategory( Int32 _typetab)
        {
            _sqlSting.SqlAnswer.datatable.Clear();
            _table.Clear();
            _sqlSting.SetParametrValue("@p_typeTable", _typetab);
            _sqlSting.ComplexRequest(_getSelectCategoryTable, CommandType.StoredProcedure, null);
            _table = _sqlSting.SqlAnswer.datatable;

            return _table;
        }
        public DataTable SelectCategory()
        {
            _sqlTreeViewProductSting.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlTreeViewProductSting.ComplexRequest(_getSelectCategoryProductTreeView, CommandType.StoredProcedure, null);
            _table = _sqlTreeViewProductSting.SqlAnswer.datatable;

            return _table;
        }

        public DataTable SelectCategoryRelay()
        {
            _sqlTreeViewRelaySting.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlTreeViewRelaySting.ComplexRequest(_getSelectCategoryRelayTreeView, CommandType.StoredProcedure, null);
            _table = _sqlTreeViewRelaySting.SqlAnswer.datatable;

            return _table;
        }

        public DataTable SetCategorySave(LocalRow localRow)
        {
            _sqlSave.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlSave.SetParametrValue("@p_MassCategoryProduct", localRow.MassCategoryProduct);
            _sqlSave.SetParametrValue("@p_MassCategoryIDProduct", localRow.MassCategoryIDProduct);
            _sqlSave.SetParametrValue("@p_MassDescriptionProduct", localRow.MassCategoryDescriptionProduct);
            _sqlSave.SetParametrValue("@p_MassCategoryRelay", localRow.MassCategoryRelay);
            _sqlSave.SetParametrValue("@p_MassDescriptionRelay", localRow.MassCategoryIDRelay);
            _sqlSave.SetParametrValue("@p_MassCategoryIDRelay", localRow.MassCategoryDescriptionRelay);

            _sqlSave.ComplexRequest(_getSaveCategory, CommandType.StoredProcedure, null);
            _table = _sqlSave.SqlAnswer.datatable;

            return _table;
        }

        public LocalRow ConvertCategory(DataRow _row, LocalRow localrow)
        {
            VetrinaList listVetrina = new VetrinaList();
            convertData = new ConvertData(_row, localrow);
            localrow.ID = convertData.ConvertDataInt32("ID");
            localrow.Description = convertData.ConvertDataString("Description");
            return localrow;
        }

        public KategoryType ConvertCategory(DataRow _row, KategoryType localrow)
        {
            convertData = new ConvertData(_row, localrow);
            localrow.ID = convertData.ConvertDataInt32("ID");
            localrow.KategoryName = convertData.ConvertDataString("KategoryName");
            localrow.TypeCategoryName = convertData.ConvertDataString("TypeCategoryName");
            return localrow;
        }

        public LocalRow ConvertSummary(DataRow _row, RowSummary _localrow)
        {
            _localrow.PageCount = Int32.Parse(_row["CountROWS"].ToString());


            return localrow;
        }
    }
}
