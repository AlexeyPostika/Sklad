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
        String commanString = "Select * From Product";
        String getSelectProductTable = "xp_GetSelectProductTable";
        DataTable table;
        public DataTable Select()
        {
            table = new DataTable();
            table = sqlSting.SQLCommandrs(getSelectProductTable);
            //foreach(DataRow row in table.Rows)
            //{
            //    localrow.ID = Int32.Parse(row["ID"].ToString());
            //    localrow.Name = row["Name"].ToString();
            //    localrow.TypeProduct = Int32.Parse(row["IDTypeProduct"].ToString());
            //    localrow.Cena = Int32.Parse(row["Cena"].ToString());
            //    localrow.Vetrina= Int32.Parse(row["IDVetrina"].ToString());

            //}
            return table;
        }
    }
}
