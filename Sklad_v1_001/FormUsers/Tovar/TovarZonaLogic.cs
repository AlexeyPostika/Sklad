using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sklad_v1_001.SQL;
using System.Data;

namespace Sklad_v1_001.FormUsers.Tovar
{
    public class LocalFilter : INotifyPropertyChanged
    {
        private Int32 pageCountRows;                    //страница
        private Int32 rowsCountPage;                    //количество строк на странице
        private Int32 page;
        public int PageCountRows
        {
            get
            {
                return pageCountRows;
            }

            set
            {
                pageCountRows = value;
                OnPropertyChanged("PageCountRows");
            }
        }

        public int RowsCountPage
        {
            get
            {
                return rowsCountPage;
            }

            set
            {
                rowsCountPage = value;
                OnPropertyChanged("RowsCountPage");
            }
        }

        public int Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
                OnPropertyChanged("Page");
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
        private String name;
        private String typeProduct;
        private String typeDescriptio;
        private Double cena;
        private Int32 vetrina;
        private String photoImage;
        private Int32 countPAGE;

        private String textOnWhatPage;
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

        public string Name
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

        public String TypeProduct
        {
            get
            {
                return typeProduct;
            }

            set
            {
                typeProduct = value;
                OnPropertyChanged("TypeProduct");
            }
        }

        public string TypeDescriptio
        {
            get
            {
                return typeDescriptio;
            }

            set
            {
                typeDescriptio = value;
                OnPropertyChanged("TypeDescriptio");
            }
        }

        public double Cena
        {
            get
            {
                return cena;
            }

            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        public int Vetrina
        {
            get
            {
                return vetrina;
            }

            set
            {
                vetrina = value;
                OnPropertyChanged("Vetrina");
            }
        }

        public string PhotoImage
        {
            get
            {
                return photoImage;
            }

            set
            {
                photoImage = value;
                OnPropertyChanged("PhotoImage");
            }
        }

        public string TextOnWhatPage
        {
            get
            {
                return textOnWhatPage;
            }

            set
            {
                textOnWhatPage = value;
                OnPropertyChanged("TextOnWhatPage");
            }
        }

        public int CountPAGE
        {
            get
            {
                return countPAGE;
            }

            set
            {
                countPAGE = value;
                OnPropertyChanged("CountPAGE");
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
   
    public class TovarZonaLogic
    {
        SQLCommanSelect _sqlSting;

        LocalRow localrow;
        
        String _getSelectProductTable = "xp_GetSelectProductTable";      //хранимка

        DataTable _table;

        
        public TovarZonaLogic()
        {
            //объявили подключение
            _sqlSting = new SQLCommanSelect();
            //объявили localRow
            localrow = new LocalRow();
            //объявили таблицу куда будем записывать все
            _table = new DataTable();
           
            //объявляем переменные для хранимой процедуры
            _sqlSting.AddParametr("@p_rowcountpage", SqlDbType.Int);
            _sqlSting.SetParametrValue("@p_rowcountpage", 0);

            _sqlSting.AddParametr("@p_pagecountrow", SqlDbType.Int);
            _sqlSting.SetParametrValue("@p_pagecountrow", 0);
        }
        public DataTable Select(LocalFilter filterlocal)
        {
            _sqlSting.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlSting.SetParametrValue("@p_rowcountpage", filterlocal.RowsCountPage);
            _sqlSting.SetParametrValue("@p_pagecountrow", filterlocal.PageCountRows);

            _sqlSting.ComplexRequest(_getSelectProductTable, CommandType.StoredProcedure, null);
            _table = _sqlSting.SqlAnswer.datatable;

            return _table;
        }

        public LocalRow Convert(DataRow _row, LocalRow localrow)
        {
            localrow.ID = Int32.Parse(_row["ID"].ToString());
            localrow.Name = _row["Name"].ToString();
            localrow.TypeProduct = _row["TypeDescription"].ToString();
            localrow.Cena = Int32.Parse(_row["Cena"].ToString());
            localrow.Vetrina = Int32.Parse(_row["IDVetrina"].ToString());
            localrow.PhotoImage = @"..\..\Icone\tovar\picture_80px.png";
            localrow.CountPAGE = Int32.Parse(_row["CountROWS"].ToString());
            return localrow;
        }

        public LocalRow ConvertSummary(DataRow _row, RowSummary _localrow)
        {
            _localrow.PageCount = Int32.Parse(_row["CountROWS"].ToString());


            return localrow;
        }
    }
}
