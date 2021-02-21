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
using System.Windows.Media.Imaging;
using Sklad_v1_001.GlobalVariable;

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
        private Int64 extrRefShtrixCode;
        private String name;
        private String typeProduct;
        private String typeDescriptio;
        private Double cena;
        private Int32 vetrina;
        private String vetrinaString;
        private BitmapImage photoImage;
        private Byte[] photoImageByte;
        private Int32 countPAGE;
        private String description;
        private List<BitmapImage> listImage;
        private Int32 documentID;
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

        public BitmapImage PhotoImage
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

        public string VetrinaString
        {
            get
            {
                return vetrinaString;
            }

            set
            {
                vetrinaString = value;
                OnPropertyChanged("VetrinaString");
            }
        }

        public long ExtrRefShtrixCode
        {
            get
            {
                return extrRefShtrixCode;
            }

            set
            {
                extrRefShtrixCode = value;
                OnPropertyChanged("ExtrRefShtrixCode");
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

        public List<BitmapImage> ListImage
        {
            get
            {
                return listImage;
            }

            set
            {
                listImage = value;
                OnPropertyChanged("ListImage");
            }
        }

        public Int32 DocumentID
        {
            get
            {
                return documentID;
            }

            set
            {
                documentID = value;
                OnPropertyChanged("DocumentID");
            }
        }

        public byte[] PhotoImageByte { get => photoImageByte; set => photoImageByte = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public LocalRow()
        {
            ListImage = new List<BitmapImage>();          
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
        SQLCommanSelect _sqlImageSting;
        SQLCommanSelect _sqlSave;

        LocalRow localrow;
        
        String _getSelectProductTable = "xp_GetSelectProductTable";      //хранимка
        String _getSelectProductImageTable = "xp_GetSelectProductImageTable";      //хранимка
        String _getSaveProductImage = "xp_SaveProductImage";      //хранимка

        DataTable _table;

        ConvertData convertData;
        
        public TovarZonaLogic()
        {
            //объявили подключение
            _sqlSting = new SQLCommanSelect();
            _sqlImageSting = new SQLCommanSelect();
            _sqlSave = new SQLCommanSelect();
            //объявили localRow
            localrow = new LocalRow();
            //объявили таблицу куда будем записывать все
            _table = new DataTable();
            //за правильную конвертацию данных
            convertData = new ConvertData();

            //объявляем переменные для хранимой процедуры
            _sqlSting.AddParametr("@p_rowcountpage", SqlDbType.Int);
            _sqlSting.SetParametrValue("@p_rowcountpage", 0);

            _sqlSting.AddParametr("@p_pagecountrow", SqlDbType.Int);
            _sqlSting.SetParametrValue("@p_pagecountrow", 0);

            //объявляем переменные для хранимой процедуры
            _sqlImageSting.AddParametr("@p_ID", SqlDbType.Int);
            _sqlImageSting.SetParametrValue("@p_ID", 0);

            //SAVE
            //объявляем переменные для хранимой процедуры
            _sqlSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlSave.SetParametrValue("@p_ID", 0);

            _sqlSave.AddParametr("@p_DocumentID", SqlDbType.Int);
            _sqlSave.SetParametrValue("@p_DocumentID", 0);

            _sqlSave.AddParametr("@p_Image", SqlDbType.VarBinary);
            _sqlSave.SetParametrValue("@p_Image", 0);


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

        public DataTable Select(Int32 _documentID)
        {
            _sqlImageSting.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlImageSting.SetParametrValue("@p_ID", _documentID);

            _sqlImageSting.ComplexRequest(_getSelectProductImageTable, CommandType.StoredProcedure, null);
            _table = _sqlImageSting.SqlAnswer.datatable;

            return _table;
        }

        //SAVE
        public DataTable Save(LocalRow _localRow)
        {
            _sqlSave.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlSave.SetParametrValue("@p_ID", _localRow.ID);
            _sqlSave.SetParametrValue("@p_DocumentID", _localRow.DocumentID);
            _sqlSave.SetParametrValue("@p_Image", _localRow.PhotoImage);

            _sqlSave.ComplexRequest(_getSaveProductImage, CommandType.StoredProcedure, null);
            _table = _sqlSave.SqlAnswer.datatable;

            return _table;
        }

        public LocalRow Convert(DataRow _row, LocalRow localrow)
        {
            ImageSql imageSql = new ImageSql();
            VetrinaList listVetrina = new VetrinaList();
            convertData = new ConvertData(_row, localrow);
            localrow.ID = convertData.ConvertDataInt32("ID");
            localrow.Name = convertData.ConvertDataString("Name");
            localrow.TypeProduct = convertData.ConvertDataString("TypeDescription");
            localrow.Cena = convertData.ConvertDataDouble("Cena");
            localrow.Vetrina = convertData.ConvertDataInt32("IDVetrina");
            localrow.VetrinaString = convertData.ConvertDataString("VetrinaString");
            localrow.ExtrRefShtrixCode = convertData.ConvertDataInt64("ExtrRefShtrixCode");
            if (_row["PhotoImage"] as byte[] != null)
                localrow.PhotoImage = imageSql.BytesToImageSource(_row["PhotoImage"] as byte[]);
            else
                localrow.PhotoImage = ImageHelper.GenerateImage("picture_80px.png");

            localrow.CountPAGE = convertData.ConvertDataInt32("CountROWS");
            localrow.Description = convertData.ConvertDataString("Description");
            return localrow;
        }

        public LocalRow ConvertImage(DataRow _row, LocalRow _localrow)
        {
            ImageSql imageSql = new ImageSql();
            if (_row["Images"] as byte[] != null)
                _localrow.ListImage.Add(imageSql.BytesToImageSource(_row["Images"] as byte[]));
            return localrow;
        }

        public LocalRow ConvertSummary(DataRow _row, RowSummary _localrow)
        {
            _localrow.PageCount = Int32.Parse(_row["CountROWS"].ToString());


            return localrow;
        }
    }
}
