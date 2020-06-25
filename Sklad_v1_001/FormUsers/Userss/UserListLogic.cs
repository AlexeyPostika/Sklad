using Sklad_v1_001.GlobalList;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Sklad_v1_001.FormUsers.Userss
{
    public class LocalRow : INotifyPropertyChanged
    {
        private Int32 iD;
        private String lastName;
        private String firstName;
        private String otchestvo;
        private Int32 otdel;
        private String phone;
        private Int32 numberSotrudnika;
        private BitmapImage photo;
        private String dolwnost;

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

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string Otchestvo
        {
            get
            {
                return otchestvo;
            }

            set
            {
                otchestvo = value;
                OnPropertyChanged("Otchestvo");
            }
        }

        public int Otdel
        {
            get
            {
                return otdel;
            }

            set
            {
                otdel = value;
                OnPropertyChanged("Otdel");
            }
        }

        public string Phone
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

        public int NumberSotrudnika
        {
            get
            {
                return numberSotrudnika;
            }

            set
            {
                numberSotrudnika = value;
                OnPropertyChanged("NumberSotrudnika");
            }
        }

        public BitmapImage Photo
        {
            get
            {
                return photo;
            }

            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public string Dolwnost
        {
            get
            {
                return dolwnost;
            }

            set
            {
                dolwnost = value;
                OnPropertyChanged("Dolwnost");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class UserListLogic
    {
        SQLCommanSelect _sqlSting;

        LocalRow localrow;

        String _getSelectProductTable = "xp_GetSelectUsersTable";      //хранимка

        DataTable _table;

        ConvertData convertData;

        public UserListLogic()
        {
            
            //объявили подключение
            _sqlSting = new SQLCommanSelect();
            //объявили localRow
            localrow = new LocalRow();
            //объявили таблицу куда будем записывать все
            _table = new DataTable();
            //за правильную конвертацию данных
            convertData = new ConvertData();
        }

        public DataTable Select()
        {
            _sqlSting.SqlAnswer.datatable.Clear();
            _table.Clear();

            //_sqlSting.SetParametrValue("@p_rowcountpage", filterlocal.RowsCountPage);
            //_sqlSting.SetParametrValue("@p_pagecountrow", filterlocal.PageCountRows);

            _sqlSting.ComplexRequest(_getSelectProductTable, CommandType.StoredProcedure, null);
            _table = _sqlSting.SqlAnswer.datatable;

            return _table;
        }

        public LocalRow Convert(DataRow _row, LocalRow localrow)
        {
            VetrinaList listVetrina = new VetrinaList();
            convertData = new ConvertData(_row, localrow);
            localrow.ID = convertData.ConvertDataInt32("ID");
            localrow.FirstName = convertData.ConvertDataString("FirstName");
            localrow.LastName = convertData.ConvertDataString("LastName");
            localrow.Otchestvo = convertData.ConvertDataString("Otchestvo");
            localrow.Otdel = convertData.ConvertDataInt32("Otdel");
            localrow.Phone = convertData.ConvertDataString("NumberPhone");
            localrow.NumberSotrudnika = convertData.ConvertDataInt32("NumberSotrudnika");
           // localrow.Photo = @"..\..\Icone\tovar\picture_80px.png";
            localrow.Dolwnost = convertData.ConvertDataString("Dolwnost");
          
            return localrow;
        }
    }
}
