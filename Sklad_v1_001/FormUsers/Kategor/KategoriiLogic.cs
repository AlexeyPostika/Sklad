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
    }

    public class KategoryType
    {
        private Int32 iD;
        private string kategoryName;
        public ObservableCollection<TypeCategory> Category;
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
        SQLCommanSelect _sqlTreeViewSting;

        LocalRow localrow;
        
        String _getSelectCategoryTable = "xp_GetCategoryComboBox";      //хранимка xp_GetCategoryDetailsTable
        String _getSelectCategoryTreeView = "xp_GetCategoryDetailsTable";
        DataTable _table;

        ConvertData convertData;
        
        public KategoriiLogic()
        {
            //объявили подключение
            _sqlSting = new SQLCommanSelect();
            _sqlTreeViewSting = new SQLCommanSelect();
            //объявили localRow
            localrow = new LocalRow();
            //объявили таблицу куда будем записывать все
            _table = new DataTable();
            //за правильную конвертацию данных
            convertData = new ConvertData();
           
            //объявляем переменные для хранимой процедуры
           _sqlSting.AddParametr("@p_typeTable", SqlDbType.Int);
            _sqlSting.SetParametrValue("@p_typeTable", 0);

            /* _sqlSting.AddParametr("@p_pagecountrow", SqlDbType.Int);
            _sqlSting.SetParametrValue("@p_pagecountrow", 0);*/
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
            _sqlTreeViewSting.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlTreeViewSting.ComplexRequest(_getSelectCategoryTreeView, CommandType.StoredProcedure, null);
            _table = _sqlTreeViewSting.SqlAnswer.datatable;

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
