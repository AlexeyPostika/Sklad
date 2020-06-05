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
    public class LocalRow : INotifyPropertyChanged
    {
        private Int32 iD;
        private String name;
        private String typeProduct;
        private String typeDescriptio;
        private Double cena;
        private Int32 vetrina;
        private String photoImage;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    
    public class TovarZonaLogic
    {
        SQLCommanSelect sqlSting = new SQLCommanSelect();
        LocalRow localrow = new LocalRow();
        String getSelectProductTable = "xp_GetSelectProductTable";
        DataTable table;
        public DataTable Select()
        {
            table = new DataTable();
            table = sqlSting.SQLCommandrs(getSelectProductTable);
            return table;
        }

        public LocalRow Convert(DataRow _row, LocalRow localrow)
        {
            localrow.ID = Int32.Parse(_row["ID"].ToString());
            localrow.Name = _row["Name"].ToString();
            localrow.TypeProduct = _row["TypeDescription"].ToString();
            localrow.Cena = Int32.Parse(_row["Cena"].ToString());
            localrow.Vetrina = Int32.Parse(_row["IDVetrina"].ToString());
            localrow.PhotoImage = @"..\..\Icone\tovar\picture_80px.png";
            localrow.TextOnWhatPage = "25/100";
            return localrow;
        }
    }
}
