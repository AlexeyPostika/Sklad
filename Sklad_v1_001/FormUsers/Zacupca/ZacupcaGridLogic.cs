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

namespace Sklad_v1_001.FormUsers.Zacupca
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
        private Int64 trackingNumber;
        private DateTime? whenOrdered;
        private DateTime? whenItComes;
        private Int32 quantity;
        private Double summaSum;
        private Int32 paymentType;
        private Int32 paymentReceipt;
        private Int32 typeBelivery;
        private String adress;
        private String numvberPhon;
        private String namaMeneger;
        private String nameBelivery;
        private String numberBeliveryTracking;
        private String nameManegerBelivery;
        private String dopNameCompanyBelivery;
        private String dopNameSkladCompany;
        private Double dopTagPriceOrder;
        private DateTime? dopWhenDateOrder;

        private DateTime? dopWhereDateOrder;
        private Int32 dopTypePayment;
        private Int32 dopPaymentReceipt;
        private String dopNameManegerCompany;
        private String dopNumberPhoneManeger;
        private DateTime? dateCreate;
        private Int32 createUseras;

        private DateTime? dataLastModifide;
        private Int32 lastModifideUsers;

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

        public long TrackingNumber
        {
            get
            {
                return trackingNumber;
            }

            set
            {
                trackingNumber = value;
                OnPropertyChanged("TrackingNumber");
            }
        }

        public DateTime? WhenOrdered
        {
            get
            {
                return whenOrdered;
            }

            set
            {
                whenOrdered = value;
                OnPropertyChanged("WhenOrdered");
            }
        }

        public DateTime? WhenItComes
        {
            get
            {
                return whenItComes;
            }

            set
            {
                whenItComes = value;
                OnPropertyChanged("WhenItComes");
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public double SummaSum
        {
            get
            {
                return summaSum;
            }

            set
            {
                summaSum = value;
                OnPropertyChanged("SummaSum");
            }
        }

        public int PaymentType
        {
            get
            {
                return paymentType;
            }

            set
            {
                paymentType = value;
                OnPropertyChanged("PaymentType");
            }
        }

        public int PaymentReceipt
        {
            get
            {
                return paymentReceipt;
            }

            set
            {
                paymentReceipt = value;
                OnPropertyChanged("PaymentReceipt");
            }
        }

        public int TypeBelivery
        {
            get
            {
                return typeBelivery;
            }

            set
            {
                typeBelivery = value;
                OnPropertyChanged("TypeBelivery");
            }
        }

        public string Adress
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

        public string NumvberPhon
        {
            get
            {
                return numvberPhon;
            }

            set
            {
                numvberPhon = value;
                OnPropertyChanged("NumvberPhon");
            }
        }

        public string NamaMeneger
        {
            get
            {
                return namaMeneger;
            }

            set
            {
                namaMeneger = value;
                OnPropertyChanged("NamaMeneger");
            }
        }

        public string NameBelivery
        {
            get
            {
                return nameBelivery;
            }

            set
            {
                nameBelivery = value;
                OnPropertyChanged("NameBelivery");
            }
        }

        public string NumberBeliveryTracking
        {
            get
            {
                return numberBeliveryTracking;
            }

            set
            {
                numberBeliveryTracking = value;
                OnPropertyChanged("NumberBeliveryTracking");
            }
        }

        public string NameManegerBelivery
        {
            get
            {
                return nameManegerBelivery;
            }

            set
            {
                nameManegerBelivery = value;
                OnPropertyChanged("NameManegerBelivery");
            }
        }

        public string DopNameCompanyBelivery
        {
            get
            {
                return dopNameCompanyBelivery;
            }

            set
            {
                dopNameCompanyBelivery = value;
                OnPropertyChanged("DopNameCompanyBelivery");
            }
        }

        public string DopNameSkladCompany
        {
            get
            {
                return dopNameSkladCompany;
            }

            set
            {
                dopNameSkladCompany = value;
                OnPropertyChanged("DopNameSkladCompany");
            }
        }

        public double DopTagPriceOrder
        {
            get
            {
                return dopTagPriceOrder;
            }

            set
            {
                dopTagPriceOrder = value;
                OnPropertyChanged("DopTagPriceOrder");
            }
        }

        public DateTime? DopWhenDateOrder
        {
            get
            {
                return dopWhenDateOrder;
            }

            set
            {
                dopWhenDateOrder = value;
                OnPropertyChanged("DopWhenDateOrder");
            }
        }

        public DateTime? DopWhereDateOrder
        {
            get
            {
                return dopWhereDateOrder;
            }

            set
            {
                dopWhereDateOrder = value;
                OnPropertyChanged("DopWhereDateOrder");
            }
        }

        public int DopTypePayment
        {
            get
            {
                return dopTypePayment;
            }

            set
            {
                dopTypePayment = value;
                OnPropertyChanged("DopTypePayment");
            }
        }

        public int DopPaymentReceipt
        {
            get
            {
                return dopPaymentReceipt;
            }

            set
            {
                dopPaymentReceipt = value;
                OnPropertyChanged("DopPaymentReceipt");
            }
        }

        public string DopNameManegerCompany
        {
            get
            {
                return dopNameManegerCompany;
            }

            set
            {
                dopNameManegerCompany = value;
                OnPropertyChanged("DopNameManegerCompany");
            }
        }

        public string DopNumberPhoneManeger
        {
            get
            {
                return dopNumberPhoneManeger;
            }

            set
            {
                dopNumberPhoneManeger = value;
                OnPropertyChanged("DopNumberPhoneManeger");
            }
        }

        public DateTime? DateCreate
        {
            get
            {
                return dateCreate;
            }

            set
            {
                dateCreate = value;
                OnPropertyChanged("DateCreate");
            }
        }

        public int CreateUseras
        {
            get
            {
                return createUseras;
            }

            set
            {
                createUseras = value;
                OnPropertyChanged("CreateUseras");
            }
        }

        public DateTime? DataLastModifide
        {
            get
            {
                return dataLastModifide;
            }

            set
            {
                dataLastModifide = value;
                OnPropertyChanged("DataLastModifide");
            }
        }

        public int LastModifideUsers
        {
            get
            {
                return lastModifideUsers;
            }

            set
            {
                lastModifideUsers = value;
                OnPropertyChanged("lastModifideUsers");
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
   
    public class ZacupcaGridLogic
    {
        SQLCommanSelect _sqlSting;
        SQLCommanSelect _sqlSave;

        LocalRow localrow;
        
        String _getSelectPurchaseTable = "xp_GetSelectPurchaseDocument";      //хранимка
        String _savePurchaseDocument = "xp_SavePurchaseDocument";      //хранимка

        DataTable _table;

        ConvertData convertData;
        
        public ZacupcaGridLogic()
        {
            //объявили подключение
            _sqlSting = new SQLCommanSelect();
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

            //объявляем переменные для хранимой процедуры save
            _sqlSave.AddParametr("@p_IDCommand", SqlDbType.Int);
            _sqlSave.SetParametrValue("@p_IDCommand", 0);

            _sqlSave.AddParametr("@p_IDProduct", SqlDbType.Int);
            _sqlSave.SetParametrValue("@p_IDProduct", 0);

            _sqlSave.AddParametr("@p_TrackingNumber", SqlDbType.BigInt);
            _sqlSave.SetParametrValue("@p_TrackingNumber", 0);

            _sqlSave.AddParametr("@p_WhenOrdered", SqlDbType.DateTime);
            _sqlSave.SetParametrValue("@p_WhenOrdered", DateTime.Now);
            /*
             *@ int,
	@ int = 0,
	@ bigint,
	@ datetime,
	@p_WhenItComes datetime,
	@p_Quantity int,
	@p_SummaSum money,
	@p_PaymentType int,
	@p_PaymentReceipt image,
	@p_TypeBelivery int,
	@p_Adress nvarchar(255),
	@p_NumvberPhon bigint ,
	@p_NamaMeneger nvarchar(255) ,
	@p_NameBelivery nvarchar(255),
	@p_NumberBeliveryTracking nvarchar(255),
	@p_NameManegerBelivery nvarchar(255),
	@p_DopNameCompanyBelivery nvarchar(255),
	@p_DopNameSkladCompany nvarchar(255) ,
	@p_DopTagPriceOrder money,
	@p_DopWhenDateOrder datetime,
	@p_DopWhereDateOrder datetime,
	@p_DopTypePayment int,
	@p_DopPaymentReceipt image,
	@p_DopNameManegerCompany nvarchar(255) ,
	@p_DopNumberPhoneManeger nvarchar(255),
	@p_DateCreate datetime,
	@p_CreateUseras int,
	@p_DataLastModifide datetime,
	@p_LastModifideUsers int
             */
        }
        public DataTable Select(LocalFilter filterlocal)
        {
            _sqlSting.SqlAnswer.datatable.Clear();
            _table.Clear();

            _sqlSting.SetParametrValue("@p_rowcountpage", filterlocal.RowsCountPage);
            _sqlSting.SetParametrValue("@p_pagecountrow", filterlocal.PageCountRows);

            _sqlSting.ComplexRequest(_getSelectPurchaseTable, CommandType.StoredProcedure, null);
            _table = _sqlSting.SqlAnswer.datatable;

            return _table;
        }

        public LocalRow Convert(DataRow _row, LocalRow localrow)
        {
            VetrinaList listVetrina = new VetrinaList();
            convertData = new ConvertData(_row, localrow);

            localrow.ID = convertData.ConvertDataInt32("ID");          
            localrow.TrackingNumber= convertData.ConvertDataInt64("TrackingNumber");
            localrow.WhenOrdered= convertData.ConvertDataDateTime("WhenOrdered");
            localrow.WhenItComes= convertData.ConvertDataDateTime("WhenItComes");
            localrow.Quantity= convertData.ConvertDataInt32("Quantity");
            localrow.SummaSum= convertData.ConvertDataDouble("SummaSum");
            localrow.PaymentType= convertData.ConvertDataInt32("PaymentType");
            localrow.PaymentReceipt= convertData.ConvertDataInt32("PaymentReceipt");
            localrow.TypeBelivery = convertData.ConvertDataInt32("TypeBelivery");
            localrow.Adress= convertData.ConvertDataString("Adress");
            localrow.NumvberPhon= convertData.ConvertDataString("NumvberPhon");
            localrow.NamaMeneger= convertData.ConvertDataString("NamaMeneger");
            localrow.NameBelivery= convertData.ConvertDataString("NameBelivery");
            localrow.NumberBeliveryTracking= convertData.ConvertDataString("NumberBeliveryTracking");
            localrow.NameManegerBelivery= convertData.ConvertDataString("NameManegerBelivery");
            localrow.DopNameCompanyBelivery= convertData.ConvertDataString("DopNameCompanyBelivery");
            localrow.DopNameSkladCompany= convertData.ConvertDataString("DopNameSkladCompany");
            localrow.DopTagPriceOrder= convertData.ConvertDataDouble("DopTagPriceOrder");
            localrow.DopWhenDateOrder = convertData.ConvertDataDateTime("DopWhenDateOrder");
            localrow.DopWhereDateOrder= convertData.ConvertDataDateTime("DopWhereDateOrder");
            localrow.DopTypePayment= convertData.ConvertDataInt32("DopTypePayment");
            localrow.DopPaymentReceipt= convertData.ConvertDataInt32("DopPaymentReceipt");
            localrow.DopNameManegerCompany= convertData.ConvertDataString("DopNameManegerCompany");
            localrow.DopNumberPhoneManeger= convertData.ConvertDataString("DopNumberPhoneManeger");
            localrow.DateCreate= convertData.ConvertDataDateTime("DateCreate");
            localrow.CreateUseras= convertData.ConvertDataInt32("CreateUseras");
            localrow.DataLastModifide= convertData.ConvertDataDateTime("DataLastModifide");
            localrow.LastModifideUsers= convertData.ConvertDataInt32("LastModifideUsers");
         
            return localrow;
        }

        public LocalRow ConvertSummary(DataRow _row, RowSummary _localrow)
        {
            //_localrow.PageCount = Int32.Parse(_row["CountROWS"].ToString());


            return localrow;
        }
    }
}
