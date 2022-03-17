using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using Sklad_v1_001.SQLCommand;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SupplyDocument
{
    public class LocalFilter : INotifyPropertyChanged
    {
        private string search;
        private Int32 iD;
        private string screenTypeGrid;

        private String deliveryID;
        private String managerUserID;
        private String createdByUserID;
        private String lastModifiedByUserID;
        private String status;

        private Double quantityMin;
        private Double quantityMax;
        private Double amountMin;
        private Double amountMax;

        private Int32 filterCreatedDate;
        private DateTime? fromCreatedDate;
        private DateTime? toCreatedDate;

        private Int32 filterLastModifiedDate;
        private DateTime? fromLastModifiedDate;
        private DateTime? toLastModifiedDate;

        private String sortColumn;
        private Boolean sort;

        private Int32 pageNumber;
        private Int32 pagerowCount;

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

        public Int32 FilterCreatedDate
        {
            get
            {
                return filterCreatedDate;
            }

            set
            {
                filterCreatedDate = value;
                OnPropertyChanged("FilterCreatedDate");
            }
        }

        public DateTime? FromCreatedDate
        {
            get
            {
                return fromCreatedDate;
            }

            set
            {
                fromCreatedDate = value;
                OnPropertyChanged("FromCreatedDate");
            }
        }

        public DateTime? ToCreatedDate
        {
            get
            {
                return toCreatedDate;
            }

            set
            {
                toCreatedDate = value;
                OnPropertyChanged("ToCreatedDate");
            }
        }

        public Int32 FilterLastModifiedDate
        {
            get
            {
                return filterLastModifiedDate;
            }

            set
            {
                filterLastModifiedDate = value;
                OnPropertyChanged("FilterLastModifiedDate");
            }
        }

        public DateTime? FromLastModifiedDate
        {
            get
            {
                return fromLastModifiedDate;
            }

            set
            {
                fromLastModifiedDate = value;
                OnPropertyChanged("FromLastModifiedDate");
            }
        }

        public DateTime? ToLastModifiedDate
        {
            get
            {
                return toLastModifiedDate;
            }

            set
            {
                toLastModifiedDate = value;
                OnPropertyChanged("ToLastModifiedDate");
            }
        }
        //deliveryID
        public string DeliveryID
        {
            get
            {
                return deliveryID;
            }

            set
            {
                deliveryID = value;
                OnPropertyChanged("DeliveryID");
            }
        }
        //managerUserID
        public string ManagerUserID
        {
            get
            {
                return managerUserID;
            }

            set
            {
                managerUserID = value;
                OnPropertyChanged("ManagerUserID");
            }
        }
        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public string CreatedByUserID
        {
            get
            {
                return createdByUserID;
            }

            set
            {
                createdByUserID = value;
                OnPropertyChanged("CreatedByUserID");
            }
        }

        public string LastModifiedByUserID
        {
            get
            {
                return lastModifiedByUserID;
            }

            set
            {
                lastModifiedByUserID = value;
                OnPropertyChanged("LastModifiedByUserID");
            }
        }

        public Double QuantityMin
        {
            get
            {
                return quantityMin;
            }

            set
            {
                quantityMin = value;
                OnPropertyChanged("QuantityMin");
            }
        }

        public Double QuantityMax
        {
            get
            {
                return quantityMax;
            }

            set
            {
                quantityMax = value;
                OnPropertyChanged("QuantityMax");
            }
        }

        public Double AmountMin
        {
            get
            {
                return amountMin;
            }

            set
            {
                amountMin = value;
                OnPropertyChanged("AmountMin");
            }
        }

        public Double AmountMax
        {
            get
            {
                return amountMax;
            }

            set
            {
                amountMax = value;
                OnPropertyChanged("AmountMax");
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

        public Int32 PageNumber
        {
            get
            {
                return pageNumber;
            }

            set
            {
                pageNumber = value;
                OnPropertyChanged("PageNumber");
            }
        }

        public Int32 PagerowCount
        {
            get
            {
                return pagerowCount;
            }

            set
            {
                pagerowCount = value;
                OnPropertyChanged("PagerowCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LocalFilter()
        {
            ScreenTypeGrid = ScreenType.ScreenTypeGrid;
            Search = "";

            FromCreatedDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            ToCreatedDate = DateTime.Now;
            FromCreatedDate = FromCreatedDate?.AddSeconds(10);

            FromLastModifiedDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            ToLastModifiedDate = DateTime.Now;
            FromLastModifiedDate = FromLastModifiedDate?.AddSeconds(10);

            PageNumber = 0;
            PagerowCount = 16;
            Sort = true;
            SortColumn = "ID";
            
            Status = "All";
            CreatedByUserID = "All";
            LastModifiedByUserID = "All";
            ManagerUserID = "All";
            DeliveryID = "All";
        }

    }

    public class LocalRow : INotifyPropertyChanged
    {
        ShemaStorаge shemaStorаgeLocal;
        //SupplyDocument
        private Int32 addUserID;
        private Int32 userID;
        private Int32 iD;
        private Int32 lineDocument;
        private Int32 status;
        private String statusString;
        private Int32 count;
        private Decimal amount;
        private Int32 reffID;
        private String invoice;
        private String tTN;
        private String managerName;
        private String delivery;

        private DateTime reffDate;
        private Int64 supplyDocumentNumber;
        private DateTime? createdDate;
        private String createdDateString;
        private Int32 createdUserID;
        private String createdUserIDString;
        private DateTime? lastModificatedDate;
        private String lastModificatedDateString;
        private Int32 lastModificatedUserID;
        private String lastModificatedUserIDString;

        //Category
        private String massCategoryID;
        private String massName;
        private String massDescription;
        //CategoryDetails
        private String massCategoryDetailsID;
        private String massIDCategory;
        private String massCategoryDetailsName;
        private String massCategoryDetailsDescription;

        //Delivery
        private String massDeliveryID;
        private String massNameCompanyDelivery;
        private String massPhonesDelivery;
        private String massAdressDelivery;
        private String massCountryDelivery;
        //DeliveryDetails
        private String massDeliveryDetailsID;
        private String massIDDelivery;
        private String massManagerName;
        private String massPhonesDeliveryDetails;

        //SupplyDocumentDetails
        private String massSupplyDocumentDetailsID;
        private String massSupplyDocumentDetailsName;
        private String massSupplyDocumentDetailsQuantity;
        private String massSupplyDocumentDetailsTagPriceUSA;
        private String massSupplyDocumentDetailsTagPriceRUS;
        private String massSupplyDocumentDetailsCategoryID;
        private String massSupplyDocumentDetailsCategoryDetailsID;
        private String massSupplyDocumentDetailsImageProduct;
        private String massSupplyDocumentDetailsModel;
        private String massSupplyDocumentDetailsSizeProduct;
        private String massSupplyDocumentDetailsSize;
        private String massSupplyDocumentDetailsBarCode;     

        //SupplyDocumentDelivery
        private String massSupplyDocumentDeliveryID;
        private String massSupplyDocumentDeliveryDeliveryID;
        private String massSupplyDocumentDeliveryDeliveryDetailsID;
        //@p_MassSupplyDocumentDeliveryDocumentID nvarchar(MAX) = '',
        private String massSupplyDocumentDeliveryTTN;
        private String massSupplyDocumentDeliveryImageTTN;
        private String massSupplyDocumentDeliveryInvoice;
        private String massSupplyDocumentDeliveryImageInvoice;
        private String massSupplyDocumentDeliveryAmountUSA;
        private String massSupplyDocumentDeliveryAmountRUS;

        //SupplyDocumentPayment
        private String massSupplyDocumentPaymentID;
        private String massSupplyDocumentPaymentAmount;
        private String massSupplyDocumentPaymentStatus;
        private String massSupplyDocumentPaymentOperationType;
        private String massSupplyDocumentPaymentDescription;
        private String massSupplyDocumentPaymentRRN;

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
        public Int32 LineDocument
        {
            get
            {
                return lineDocument;
            }

            set
            {
                lineDocument = value;
                OnPropertyChanged("LineDocument");
            }
        }
        public Int32 UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }

        public Int32 Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public String StatusString
        {
            get
            {
                return statusString;
            }

            set
            {
                statusString = value;
                OnPropertyChanged("StatusString");
            }
        }

        public String Invoice
        {
            get
            {
                return invoice;
            }

            set
            {
                invoice = value;
                OnPropertyChanged("StatusString");
            }
        }
       
        public String TTN
        {
            get
            {
                return tTN;
            }

            set
            {
                tTN = value;
                OnPropertyChanged("TTN");
            }
        }
      
        public String ManagerName
        {
            get
            {
                return managerName;
            }

            set
            {
                managerName = value;
                OnPropertyChanged("ManagerName");
            }
        }
        
        public String Delivery
        {
            get
            {
                return delivery;
            }

            set
            {
                delivery = value;
                OnPropertyChanged("Delivery");
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
        public Int32 Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public Decimal Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public string MassCategoryID
        {
            get
            {
                return massCategoryID;
            }

            set
            {
                massCategoryID = value;
                OnPropertyChanged("MassCategoryID");
            }
        }
        public string MassName
        {
            get
            {
                return massName;
            }

            set
            {
                massName = value;
                OnPropertyChanged("MassName");
            }
        }
        public string MassDescription
        {
            get
            {
                return massDescription;
            }

            set
            {
                massDescription = value;
                OnPropertyChanged("MassDescription");
            }
        }
        public string MassCategoryDetailsID
        {
            get
            {
                return massCategoryDetailsID;
            }

            set
            {
                massCategoryDetailsID = value;
                OnPropertyChanged("MassCategoryDetailsID");
            }
        }
        public string MassIDCategory
        {
            get
            {
                return massIDCategory;
            }

            set
            {
                massIDCategory = value;
                OnPropertyChanged("MassIDCategory");
            }
        }
        public string MassCategoryDetailsName
        {
            get
            {
                return massCategoryDetailsName;
            }

            set
            {
                massCategoryDetailsName = value;
                OnPropertyChanged("MassCategoryDetailsName");
            }
        }
        public string MassCategoryDetailsDescription
        {
            get
            {
                return massCategoryDetailsDescription;
            }

            set
            {
                massCategoryDetailsDescription = value;
                OnPropertyChanged("MassCategoryDetailsDescription");
            }
        }
        public string MassDeliveryID
        {
            get
            {
                return massDeliveryID;
            }

            set
            {
                massDeliveryID = value;
                OnPropertyChanged("MassDeliveryID");
            }
        }
        public string MassNameCompanyDelivery
        {
            get
            {
                return massNameCompanyDelivery;
            }

            set
            {
                massNameCompanyDelivery = value;
                OnPropertyChanged("MassNameCompanyDelivery");
            }
        }
        public string MassPhonesDelivery
        {
            get
            {
                return massPhonesDelivery;
            }

            set
            {
                massPhonesDelivery = value;
                OnPropertyChanged("MassPhonesDelivery");
            }
        }
        public string MassAdressDelivery
        {
            get
            {
                return massAdressDelivery;
            }

            set
            {
                massAdressDelivery = value;
                OnPropertyChanged("MassAdressDelivery");
            }
        }
        public string MassCountryDelivery
        {
            get
            {
                return massCountryDelivery;
            }

            set
            {
                massCountryDelivery = value;
                OnPropertyChanged("MassCountryDelivery");
            }
        }
        public string MassDeliveryDetailsID
        {
            get
            {
                return massDeliveryDetailsID;
            }

            set
            {
                massDeliveryDetailsID = value;
                OnPropertyChanged("MassDeliveryDetailsID");
            }
        }
        public string MassIDDelivery
        {
            get
            {
                return massIDDelivery;
            }

            set
            {
                massIDDelivery = value;
                OnPropertyChanged("MassIDDelivery");
            }
        }
        public string MassManagerName
        {
            get
            {
                return massManagerName;
            }

            set
            {
                massManagerName = value;
                OnPropertyChanged("MassManagerName");
            }
        }
        public string MassPhonesDeliveryDetails
        {
            get
            {
                return massPhonesDeliveryDetails;
            }

            set
            {
                massPhonesDeliveryDetails = value;
                OnPropertyChanged("MassPhonesDeliveryDetails");
            }
        }
        public string MassSupplyDocumentDetailsID
        {
            get
            {
                return massSupplyDocumentDetailsID;
            }

            set
            {
                massSupplyDocumentDetailsID = value;
                OnPropertyChanged("MassSupplyDocumentDetailsID");
            }
        }
        public string MassSupplyDocumentDetailsName
        {
            get
            {
                return massSupplyDocumentDetailsName;
            }

            set
            {
                massSupplyDocumentDetailsName = value;
                OnPropertyChanged("MassSupplyDocumentDetailsName");
            }
        }
        public string MassSupplyDocumentDetailsQuantity
        {
            get
            {
                return massSupplyDocumentDetailsQuantity;
            }

            set
            {
                massSupplyDocumentDetailsQuantity = value;
                OnPropertyChanged("MassSupplyDocumentDetailsQuantity");
            }
        }
        public string MassSupplyDocumentDetailsTagPriceUSA
        {
            get
            {
                return massSupplyDocumentDetailsTagPriceUSA;
            }

            set
            {
                massSupplyDocumentDetailsTagPriceUSA = value;
                OnPropertyChanged("MassSupplyDocumentDetailsTagPriceUSA");
            }
        }
        public string MassSupplyDocumentDetailsTagPriceRUS
        {
            get
            {
                return massSupplyDocumentDetailsTagPriceRUS;
            }

            set
            {
                massSupplyDocumentDetailsTagPriceRUS = value;
                OnPropertyChanged("MassSupplyDocumentDetailsTagPriceRUS");
            }
        }
        public string MassSupplyDocumentDetailsCategoryID
        {
            get
            {
                return massSupplyDocumentDetailsCategoryID;
            }

            set
            {
                massSupplyDocumentDetailsCategoryID = value;
                OnPropertyChanged("MassSupplyDocumentDetailsCategoryID");
            }
        }
        public string MassSupplyDocumentDetailsCategoryDetailsID
        {
            get
            {
                return massSupplyDocumentDetailsCategoryDetailsID;
            }

            set
            {
                massSupplyDocumentDetailsCategoryDetailsID = value;
                OnPropertyChanged("MassSupplyDocumentDetailsCategoryDetailsID");
            }
        }
        public String MassSupplyDocumentDetailsImageProduct
        {
            get
            {
                return massSupplyDocumentDetailsImageProduct;
            }

            set
            {
                massSupplyDocumentDetailsImageProduct = value;
                OnPropertyChanged("MassSupplyDocumentDetailsImageProduct");
            }
        }

        public String MassSupplyDocumentDetailsModel
        {
            get
            {
                return massSupplyDocumentDetailsModel;
            }

            set
            {
                massSupplyDocumentDetailsModel = value;
                OnPropertyChanged("MassSupplyDocumentDetailsModel");
            }
        }

        public String MassSupplyDocumentDetailsSizeProduct
        {
            get
            {
                return massSupplyDocumentDetailsSizeProduct;
            }

            set
            {
                massSupplyDocumentDetailsSizeProduct = value;
                OnPropertyChanged("MassSupplyDocumentDetailsSizeProduct");
            }
        }
      
        public String MassSupplyDocumentDetailsSize
        {
            get
            {
                return massSupplyDocumentDetailsSize;
            }

            set
            {
                massSupplyDocumentDetailsSize = value;
                OnPropertyChanged("MassSupplyDocumentDetailsSize");
            }
        }

        public String MassSupplyDocumentDetailsBarCode
        {
            get
            {
                return massSupplyDocumentDetailsBarCode;
            }

            set
            {
                massSupplyDocumentDetailsBarCode = value;
                OnPropertyChanged("MassSupplyDocumentDetailsBarCode");
            }
        }

        public string MassSupplyDocumentDeliveryID
        {
            get
            {
                return massSupplyDocumentDeliveryID;
            }

            set
            {
                massSupplyDocumentDeliveryID = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryID");
            }
        }
        public string MassSupplyDocumentDeliveryDeliveryID
        {
            get
            {
                return massSupplyDocumentDeliveryDeliveryID;
            }

            set
            {
                massSupplyDocumentDeliveryDeliveryID = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryDeliveryID");
            }
        }
        public string MassSupplyDocumentDeliveryDeliveryDetailsID
        {
            get
            {
                return massSupplyDocumentDeliveryDeliveryDetailsID;
            }

            set
            {
                massSupplyDocumentDeliveryDeliveryDetailsID = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryDeliveryDetailsID");
            }
        }
        public string MassSupplyDocumentDeliveryTTN
        {
            get
            {
                return massSupplyDocumentDeliveryTTN;
            }

            set
            {
                massSupplyDocumentDeliveryTTN = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryTTN");
            }
        }
        public string MassSupplyDocumentDeliveryImageTTN
        {
            get
            {
                return massSupplyDocumentDeliveryImageTTN;
            }

            set
            {
                massSupplyDocumentDeliveryImageTTN = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryImageTTN");
            }
        }
        public string MassSupplyDocumentDeliveryInvoice
        {
            get
            {
                return massSupplyDocumentDeliveryInvoice;
            }

            set
            {
                massSupplyDocumentDeliveryInvoice = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryInvoice");
            }
        }
        public string MassSupplyDocumentDeliveryImageInvoice
        {
            get
            {
                return massSupplyDocumentDeliveryImageInvoice;
            }

            set
            {
                massSupplyDocumentDeliveryImageInvoice = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryImageInvoice");
            }
        }

        public string MassSupplyDocumentDeliveryAmountUSA
        {
            get
            {
                return massSupplyDocumentDeliveryAmountUSA;
            }

            set
            {
                massSupplyDocumentDeliveryAmountUSA = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryAmountUSA");
            }
        }
      
        public string MassSupplyDocumentDeliveryAmountRUS
        {
            get
            {
                return massSupplyDocumentDeliveryAmountRUS;
            }

            set
            {
                massSupplyDocumentDeliveryAmountRUS = value;
                OnPropertyChanged("MassSupplyDocumentDeliveryAmountRUS");
            }
        }
        public string MassSupplyDocumentPaymentStatus
        {
            get
            {
                return massSupplyDocumentPaymentStatus;
            }

            set
            {
                massSupplyDocumentPaymentStatus = value;
                OnPropertyChanged("MassSupplyDocumentPaymentStatus");
            }
        }
        public string MassSupplyDocumentPaymentID
        {
            get
            {
                return massSupplyDocumentPaymentID;
            }

            set
            {
                massSupplyDocumentPaymentID = value;
                OnPropertyChanged("MassSupplyDocumentPaymentID");
            }
        }
        public string MassSupplyDocumentPaymentAmount
        {
            get
            {
                return massSupplyDocumentPaymentAmount;
            }

            set
            {
                massSupplyDocumentPaymentAmount = value;
                OnPropertyChanged("MassSupplyDocumentPaymentAmount");
            }
        }
        public string MassSupplyDocumentPaymentOperationType
        {
            get
            {
                return massSupplyDocumentPaymentOperationType;
            }

            set
            {
                massSupplyDocumentPaymentOperationType = value;
                OnPropertyChanged("MassSupplyDocumentPaymentOperationType");
            }
        }
       
        public string MassSupplyDocumentPaymentDescription
        {
            get
            {
                return massSupplyDocumentPaymentDescription;
            }

            set
            {
                massSupplyDocumentPaymentDescription = value;
                OnPropertyChanged("MassSupplyDocumentPaymentDescription");
            }
        }

        public string MassSupplyDocumentPaymentRRN
        {
            get
            {
                return massSupplyDocumentPaymentRRN;
            }

            set
            {
                massSupplyDocumentPaymentRRN = value;
                OnPropertyChanged("MassSupplyDocumentPaymentRRN");
            }
        }

        public int ReffID
        {
            get
            {
                return reffID;
            }

            set
            {
                reffID = value;
                OnPropertyChanged("ReffID");
            }
        }
        public DateTime ReffDate
        {
            get
            {
                return reffDate;
            }

            set
            {
                reffDate = value;
                OnPropertyChanged("ReffDate");
            }
        }
        public long SupplyDocumentNumber
        {
            get
            {
                return supplyDocumentNumber;
            }

            set
            {
                supplyDocumentNumber = value;
                OnPropertyChanged("SupplyDocumentNumber");
            }
        }

        public ShemaStorаge ShemaStorаgeLocal { get => shemaStorаgeLocal; set => shemaStorаgeLocal = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LocalRow()
        {
            ReffDate = DateTime.Now;
            ShemaStorаgeLocal = new ShemaStorаge();
        }
    }

    public class RowSummary : INotifyPropertyChanged
    {
        Int32 summaryQuantityLine;
        Int32 summaryQuantityProduct;
        decimal summaryProductTagPriceUSA;
        decimal summaryProductTagPriceRUS;

        Int32 summaryDeliveryQuantity;
        decimal summaryAmountUSA;
        decimal summaryAmountRUS;

        decimal summaryPaymentBalans;
        decimal summaryPaymentRemains;
       
        public Int32 SummaryQuantityLine
        {
            get
            {
                return summaryQuantityLine;
            }

            set
            {
                summaryQuantityLine = value;
                OnPropertyChanged("SummaryQuantityLine");
            }
        }
        public Int32 SummaryQuantityProduct
        {
            get
            {
                return summaryQuantityProduct;
            }

            set
            {
                summaryQuantityProduct = value;
                OnPropertyChanged("SummaryQuantityProduct");
            }
        }

        public decimal SummaryProductTagPriceUSA
        {
            get
            {
                return summaryProductTagPriceUSA;
            }

            set
            {
                summaryProductTagPriceUSA = value;
                OnPropertyChanged("SummaryProductTagPriceUSA");
            }
        }
        public decimal SummaryProductTagPriceRUS
        {
            get
            {
                return summaryProductTagPriceRUS;
            }

            set
            {
                summaryProductTagPriceRUS = value;
                OnPropertyChanged("SummaryProductTagPriceRUS");
            }
        }
        public int SummaryDeliveryQuantity
        {
            get
            {
                return summaryDeliveryQuantity;
            }

            set
            {
                summaryDeliveryQuantity = value;
                OnPropertyChanged("SummaryDeliveryQuantity");
            }
        }
        public decimal SummaryAmountUSA
        {
            get
            {
                return summaryAmountUSA;
            }

            set
            {
                summaryAmountUSA = value;
                OnPropertyChanged("SummaryAmountUSA");
            }
        }
        public decimal SummaryAmountRUS
        {
            get
            {
                return summaryAmountRUS;
            }

            set
            {
                summaryAmountRUS = value;
                OnPropertyChanged("SummaryAmountRUS");
            }
        }
        public decimal SummaryPaymentBalans
        {
            get
            {
                return summaryPaymentBalans;
            }

            set
            {
                summaryPaymentBalans = value;
                OnPropertyChanged("SummaryPaymentBalans");
            }
        }
        public decimal SummaryPaymentRemains
        {
            get
            {
                return summaryPaymentRemains;
            }

            set
            {
                summaryPaymentRemains = value;
                OnPropertyChanged("SummaryPaymentRemains");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RowsFilters: INotifyPropertyChanged
    {
        private Double filtersQuantityMin;
        private Double filtersQuantityMax;
        private Double filtersAmountMin;
        private Double filtersAmountMax;

        public Double FiltersQuantityMin
        {
            get
            {
                return filtersQuantityMin;
            }

            set
            {
                filtersQuantityMin = value;
                OnPropertyChanged("FiltersQuantityMin");
            }
        }
        public Double FiltersQuantityMax
        {
            get
            {
                return filtersQuantityMax;
            }

            set
            {
                filtersQuantityMax = value;
                OnPropertyChanged("FiltersQuantityMax");
            }
        }
        public Double FiltersAmountMin
        {
            get
            {
                return filtersAmountMin;
            }

            set
            {
                filtersAmountMin = value;
                OnPropertyChanged("FiltersAmountMin");
            }
        }
        public Double FiltersAmountMax
        {
            get
            {
                return filtersAmountMax;
            }

            set
            {
                filtersAmountMax = value;
                OnPropertyChanged("FiltersAmountMax");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class SupplyDocumentLogic
    {
        ConvertData convertData;

        public DataTable innerList1;
        public DataTable innerList2;
        public DataTable innerList3;
        public DataTable innerList4;
        public DataTable innerList5;

        public class Range
        {
            public double min;
            public double max;
        }

        Dictionary<String, DataTable> filters;
        Dictionary<String, Range> filtersFromTo;

        string get_store_procedure = "xp_GetSupplyDocumentTable";
        string get_filters_procedure = "xp_GetSupplyDocumentFilter";
        string get_summary_procedure = "xp_GetSupplyDocumentSummary";

        string get_save_procedure = "xp_SaveSupplyDocument";
        string get_save_procedure_table = "xp_SaveSupplyDocumentTable";

        string set_store_procedure = "xp_SetSupplyDocumentID";

        SQLCommanSelect _sqlRequestSelect = null;
        SQLCommanSelect _sqlRequestSelectFilters = null;
        SQLCommanSelect _sqlRequestSelectSummary = null;
        SQLCommanSelect _sqlRequestSave = null;
        SQLCommanSelect _sqlRequestSaveTable = null;
        SQLCommanSelect _sqlRequestSet = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;

        //структура таблиц
        ShemaStorаge shemaStorаge;

        public SupplyDocumentLogic()
        {
            convertData = new ConvertData();

            _data = new DataTable();
            _datarow = new DataTable();
            shemaStorаge = new ShemaStorаge();

            filters = new Dictionary<string, DataTable>();
            filtersFromTo = new Dictionary<string, Range>();

            innerList1 = new DataTable();
            innerList2 = new DataTable();
            innerList3 = new DataTable();
            innerList4 = new DataTable();
            innerList5 = new DataTable();

            innerList1.Columns.Add("ID");
            innerList1.Columns.Add("IsChecked");
            innerList1.Columns.Add("Description");

            innerList2.Columns.Add("ID");
            innerList2.Columns.Add("IsChecked");
            innerList2.Columns.Add("Description");

            innerList3.Columns.Add("ID");
            innerList3.Columns.Add("IsChecked");
            innerList3.Columns.Add("Description");

            innerList4.Columns.Add("ID");
            innerList4.Columns.Add("IsChecked");
            innerList4.Columns.Add("Description");

            innerList5.Columns.Add("ID");
            innerList5.Columns.Add("IsChecked");
            innerList5.Columns.Add("Description");

            filters.Add("CreatedByUserID", innerList1);
            filters.Add("LastModifiedByUserID", innerList2);
            filters.Add("Delivery", innerList3);
            filters.Add("ManagerName", innerList4);
            filters.Add("Status", innerList5);

            Range QuantityRange = new Range();
            Range AmountRange = new Range();
            filtersFromTo.Add("Quantity", QuantityRange);
            filtersFromTo.Add("Amount", AmountRange);

            _sqlRequestSelect = new SQLCommanSelect();
            _sqlRequestSelectFilters = new SQLCommanSelect();
            _sqlRequestSelectSummary = new SQLCommanSelect();
            _sqlRequestSave = new SQLCommanSelect();
            _sqlRequestSaveTable = new SQLCommanSelect();
            _sqlRequestSet = new SQLCommanSelect();

            //----------------------------------------------------------------------------
            _sqlRequestSelect.AddParametr("@p_TypeScreen", SqlDbType.VarChar, 10);
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.AddParametr("@p_Search", SqlDbType.NVarChar, 40);
            _sqlRequestSelect.SetParametrValue("@p_Search", "");

            _sqlRequestSelect.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_ID", 0);

            _sqlRequestSelect.AddParametr("@p_CreatedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_CreatedUserID", "");

            _sqlRequestSelect.AddParametr("@p_LastModifiedUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID", "");

            _sqlRequestSelect.AddParametr("@p_Status", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_Status", "");

            _sqlRequestSelect.AddParametr("@p_ManagerUserID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_ManagerUserID", "");

            _sqlRequestSelect.AddParametr("@p_DeliveryID", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_DeliveryID", "");

            _sqlRequestSelect.AddParametr("@p_Quantity_Min", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Min", 0);

            _sqlRequestSelect.AddParametr("@p_Quantity_Max", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Max", SqlInt32.MaxValue);

            _sqlRequestSelect.AddParametr("@p_TagPriceVATRUS_Min", SqlDbType.Decimal);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Min", SqlDecimal.MaxValue);

            _sqlRequestSelect.AddParametr("@p_TagPriceVATRUS_Max", SqlDbType.Decimal);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Max", SqlDecimal.MaxValue);

            //_sqlRequestSelect.AddParametr("@p_FromCreatedDate", SqlDbType.DateTime);
            //_sqlRequestSelect.SetParametrValue("@p_FromCreatedDate", SqlDateTime.MinValue);

            //_sqlRequestSelect.AddParametr("@p_ToCreatedDate", SqlDbType.DateTime);
            //_sqlRequestSelect.SetParametrValue("@p_ToCreatedDate", DateTime.Now);

            //_sqlRequestSelect.AddParametr("@p_FromLastModifiedDate", SqlDbType.NVarChar);
            //_sqlRequestSelect.SetParametrValue("@p_FromLastModifiedDate", SqlDateTime.MinValue.ToString());

            //_sqlRequestSelect.AddParametr("@p_ToLastModifiedDate", SqlDbType.NVarChar);
            //_sqlRequestSelect.SetParametrValue("@p_ToLastModifiedDate", DateTime.Now.ToString());

            _sqlRequestSelect.AddParametr("@p_PageNumber", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_PageNumber", 0);

            _sqlRequestSelect.AddParametr("@p_PagerowCount", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_PagerowCount", 0);

            _sqlRequestSelect.AddParametr("@p_SortColumn", SqlDbType.NVarChar, 255);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", 0);

            _sqlRequestSelect.AddParametr("@p_Sort", SqlDbType.Bit);
            _sqlRequestSelect.SetParametrValue("@p_Sort", 0);
            //----------------------------------------------------------------------------

            _sqlRequestSelectSummary.AddParametr("@p_Search", SqlDbType.NVarChar);
            _sqlRequestSelectSummary.SetParametrValue("@p_Search", "");

            _sqlRequestSelectSummary.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSelectSummary.SetParametrValue("@p_ID", 0);

            _sqlRequestSelectSummary.AddParametr("@p_CreatedUserID", SqlDbType.NVarChar);
            _sqlRequestSelectSummary.SetParametrValue("@p_CreatedUserID", "");

            _sqlRequestSelectSummary.AddParametr("@p_LastModifiedUserID", SqlDbType.NVarChar);
            _sqlRequestSelectSummary.SetParametrValue("@p_LastModifiedUserID", "");

            _sqlRequestSelectSummary.AddParametr("@p_Status", SqlDbType.NVarChar);
            _sqlRequestSelectSummary.SetParametrValue("@p_Status", "");

            _sqlRequestSelectSummary.AddParametr("@p_FromCreatedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromCreatedDate", SqlDateTime.MinValue);

            _sqlRequestSelectSummary.AddParametr("@p_ToCreatedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToCreatedDate", DateTime.Now);

            _sqlRequestSelectSummary.AddParametr("@p_FromLastModifiedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromLastModifiedDate", SqlDateTime.MinValue);

            _sqlRequestSelectSummary.AddParametr("@p_ToLastModifiedDate", SqlDbType.DateTime);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToLastModifiedDate", DateTime.Now);

            _sqlRequestSelectSummary.AddParametr("@p_Quantity_Min", SqlDbType.Int);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Min", 0);

            _sqlRequestSelectSummary.AddParametr("@p_Quantity_Max", SqlDbType.Int);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Max", 0);

            _sqlRequestSelectSummary.AddParametr("@p_TagPriceVATRUS_Min", SqlDbType.Money);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Min", System.Data.SqlTypes.SqlMoney.MaxValue);

            _sqlRequestSelectSummary.AddParametr("@p_TagPriceVATRUS_Max", SqlDbType.Money);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Max", System.Data.SqlTypes.SqlMoney.MaxValue);
            //----------------------------------------------------------------------------

            _sqlRequestSave.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_AddUserID", 1);

            _sqlRequestSave.AddParametr("@p_UserID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_UserID", 0);

            _sqlRequestSave.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_ID", 0);

            _sqlRequestSave.AddParametr("@p_Status", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_Status", 0);

            _sqlRequestSave.AddParametr("@p_Count", SqlDbType.Int);
            _sqlRequestSave.SetParametrValue("@p_Count", 0);

            _sqlRequestSave.AddParametr("@p_Amount", SqlDbType.Money);
            _sqlRequestSave.SetParametrValue("@p_Amount", 0);

            //_sqlRequestSave.AddParametr("@p_ReffID", SqlDbType.Int);
            //_sqlRequestSave.SetParametrValue("@p_ReffID", 0);

            //_sqlRequestSave.AddParametr("@p_ReffDate", SqlDbType.DateTime);
            //_sqlRequestSave.SetParametrValue("@p_ReffDate", DateTime.Now);

            _sqlRequestSave.AddParametr("@p_SupplyDocumentNumber", SqlDbType.BigInt);
            _sqlRequestSave.SetParametrValue("@p_SupplyDocumentNumber", 0);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsID", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsName", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsName", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsQuantity", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsQuantity", String.Empty);
            
            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsTagPriceUSA", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsTagPriceUSA", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsTagPriceRUS", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsTagPriceRUS", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsCategoryID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsCategoryID", String.Empty);
           
            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsCategoryDetailsID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsCategoryDetailsID", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsImageProduct", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsImageProduct", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsModel", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsModel", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsSizeProduct", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsSizeProduct", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsSize", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsSize", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDetailsBarCode", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsBarCode", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryID", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryDeliveryID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryDeliveryID", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryDeliveryDetailsID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryDeliveryDetailsID", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryTTN", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryTTN", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryImageTTN", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryImageTTN", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryInvoice", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryInvoice", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryImageInvoice", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryImageInvoice", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryAmountUSA", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryAmountUSA", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentDeliveryAmountRUS", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryAmountRUS", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentPaymentID", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentID", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentPaymentAmount", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentAmount", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentPaymentOperationType", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentOperationType", String.Empty);
          
            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentPaymentStatus", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentStatus", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentPaymentDescription", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentDescription", String.Empty);

            _sqlRequestSave.AddParametr("@p_MassSupplyDocumentPaymentRRN", SqlDbType.NVarChar);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentRRN", String.Empty);

            //----------------------------------------------------------------------------
            _sqlRequestSaveTable.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlRequestSaveTable.SetParametrValue("@p_AddUserID", 1);

            _sqlRequestSaveTable.AddParametr("@p_UserID", SqlDbType.Int);
            _sqlRequestSaveTable.SetParametrValue("@p_UserID", 0);

            _sqlRequestSaveTable.AddParametr("@p_ID", SqlDbType.Int);
            _sqlRequestSaveTable.SetParametrValue("@p_ID", 0);

            _sqlRequestSaveTable.AddParametr("@p_Status", SqlDbType.Int);
            _sqlRequestSaveTable.SetParametrValue("@p_Status", 0);

            _sqlRequestSaveTable.AddParametr("@p_Count", SqlDbType.Int);
            _sqlRequestSaveTable.SetParametrValue("@p_Count", 0);

            _sqlRequestSaveTable.AddParametr("@p_Amount", SqlDbType.Money);
            _sqlRequestSaveTable.SetParametrValue("@p_Amount", 0);
           
            _sqlRequestSaveTable.AddParametr("@p_SupplyDocumentNumber", SqlDbType.BigInt);
            _sqlRequestSaveTable.SetParametrValue("@p_SupplyDocumentNumber", 0);

            _sqlRequestSaveTable.AddParametr("@p_tableDetails", SqlDbType.Structured);
            _sqlRequestSaveTable.SetParametrValue("@p_tableDetails", shemaStorаge.SupplyDocumentDetails);

            _sqlRequestSaveTable.AddParametr("@p_tableDelivery", SqlDbType.Structured);
            _sqlRequestSaveTable.SetParametrValue("@p_tableDelivery", shemaStorаge.SupplyDocumentDelivery);

            _sqlRequestSaveTable.AddParametr("@p_tablePayment", SqlDbType.Structured);
            _sqlRequestSaveTable.SetParametrValue("@p_tablePayment", shemaStorаge.SupplyDocumentPayment);

            //----------------------------------------------------------------------------
            _sqlRequestSet.AddParametr("@p_AddUserID", SqlDbType.Int);
            _sqlRequestSet.SetParametrValue("@p_AddUserID", 1);

            _sqlRequestSet.AddParametr("@p_DocumentID", SqlDbType.Int);
            _sqlRequestSet.SetParametrValue("@p_DocumentID", 0);

            _sqlRequestSet.AddParametr("@p_DocumentNumber", SqlDbType.BigInt);
            _sqlRequestSet.SetParametrValue("@p_DocumentNumber", 0);
        }

        public DataTable FillGrid()
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGrid(Int32 id)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);
            _sqlRequestSelect.SetParametrValue("@p_ID", id);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGrid(LocalFilter _localFilter)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelect.SetParametrValue("@p_TypeScreen", ScreenType.ScreenTypeGrid);
            _sqlRequestSelect.SetParametrValue("@p_Search", _localFilter.Search);
            _sqlRequestSelect.SetParametrValue("@p_CreatedUserID", _localFilter.CreatedByUserID);
            _sqlRequestSelect.SetParametrValue("@p_LastModifiedUserID", _localFilter.LastModifiedByUserID);
            _sqlRequestSelect.SetParametrValue("@p_Status", _localFilter.Status);
            _sqlRequestSelect.SetParametrValue("@p_ManagerUserID", _localFilter.ManagerUserID);
            _sqlRequestSelect.SetParametrValue("@p_DeliveryID", _localFilter.DeliveryID);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Min", _localFilter.QuantityMin);
            _sqlRequestSelect.SetParametrValue("@p_Quantity_Max", _localFilter.QuantityMax);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Min", _localFilter.AmountMin);
            _sqlRequestSelect.SetParametrValue("@p_TagPriceVATRUS_Max", _localFilter.AmountMax);
            //_sqlRequestSelect.SetParametrValue("@p_FromCreatedDate", _localFilter.FromCreatedDate);
            //_sqlRequestSelect.SetParametrValue("@p_ToCreatedDate", _localFilter.ToCreatedDate);
            //_sqlRequestSelect.SetParametrValue("@p_FromLastModifiedDate", _localFilter.FromLastModifiedDate);
            //_sqlRequestSelect.SetParametrValue("@p_ToLastModifiedDate", _localFilter.ToLastModifiedDate);
            _sqlRequestSelect.SetParametrValue("@p_PageNumber", _localFilter.PageNumber);
            _sqlRequestSelect.SetParametrValue("@p_PagerowCount", _localFilter.PagerowCount);
            _sqlRequestSelect.SetParametrValue("@p_SortColumn", _localFilter.SortColumn);
            _sqlRequestSelect.SetParametrValue("@p_Sort", _localFilter.Sort); //тест github

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillGridAllFilter()
        {
            _sqlRequestSelectFilters.SqlAnswer.datatable.Clear();
            _data.Clear();          

            _sqlRequestSelectFilters.ComplexRequest(get_filters_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelectFilters.SqlAnswer.datatable;
            return _data;
        }

        public DataTable FillSummary(LocalFilter _localFilter)
        {
            _sqlRequestSelectSummary.SqlAnswer.datatable.Clear();
            _data.Clear();

            _sqlRequestSelectSummary.SetParametrValue("@p_Search", _localFilter.Search);
            _sqlRequestSelectSummary.SetParametrValue("@p_ID", _localFilter.ID);
            _sqlRequestSelectSummary.SetParametrValue("@p_CreatedUserID", _localFilter.CreatedByUserID);
            _sqlRequestSelectSummary.SetParametrValue("@p_LastModifiedUserID", _localFilter.LastModifiedByUserID);
            _sqlRequestSelectSummary.SetParametrValue("@p_Status", _localFilter.Status);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromCreatedDate", _localFilter.FromCreatedDate);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToCreatedDate", _localFilter.ToCreatedDate);
            _sqlRequestSelectSummary.SetParametrValue("@p_FromLastModifiedDate", _localFilter.FromLastModifiedDate);
            _sqlRequestSelectSummary.SetParametrValue("@p_ToLastModifiedDate", _localFilter.ToLastModifiedDate);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Min", _localFilter.QuantityMin);
            _sqlRequestSelectSummary.SetParametrValue("@p_Quantity_Max", _localFilter.QuantityMax);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Min", _localFilter.AmountMin);
            _sqlRequestSelectSummary.SetParametrValue("@p_TagPriceVATRUS_Max", _localFilter.AmountMax);

            _sqlRequestSelectSummary.ComplexRequest(get_summary_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelectSummary.SqlAnswer.datatable;
            return _data;
        }
      
        public Int32 SaveRow(LocalRow row)
        {
            //SupplyDocument
            _sqlRequestSave.SetParametrValue("@p_UserID", row.UserID);
            _sqlRequestSave.SetParametrValue("@p_ID", row.ID);
            _sqlRequestSave.SetParametrValue("@p_Status", row.Status);
            _sqlRequestSave.SetParametrValue("@p_Count", row.Count);
            _sqlRequestSave.SetParametrValue("@p_Amount", row.Amount);
            //_sqlRequestSave.SetParametrValue("@p_ReffID", row.ReffID);
            //_sqlRequestSave.SetParametrValue("@p_ReffDate", row.ReffDate);
            _sqlRequestSave.SetParametrValue("@p_SupplyDocumentNumber", row.SupplyDocumentNumber);         

            //SupplyDocumentDetails
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsID", row.MassSupplyDocumentDetailsID);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsName", row.MassSupplyDocumentDetailsName);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsQuantity", row.MassSupplyDocumentDetailsQuantity);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsTagPriceUSA", row.MassSupplyDocumentDetailsTagPriceUSA);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsTagPriceRUS", row.MassSupplyDocumentDetailsTagPriceRUS);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsCategoryID", row.MassSupplyDocumentDetailsCategoryID);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsCategoryDetailsID", row.MassSupplyDocumentDetailsCategoryDetailsID);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsImageProduct", row.MassSupplyDocumentDetailsImageProduct);

            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsModel", row.MassSupplyDocumentDetailsModel);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsSizeProduct", row.MassSupplyDocumentDetailsSizeProduct);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailssize", row.MassSupplyDocumentDetailsSize);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDetailsBarCode", row.MassSupplyDocumentDetailsBarCode);

            //SupplyDocumentDeliverry
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryID", row.MassSupplyDocumentDeliveryID);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryDeliveryID", row.MassSupplyDocumentDeliveryDeliveryID);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryDeliveryDetailsID", row.MassSupplyDocumentDeliveryDeliveryDetailsID);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryTTN", row.MassSupplyDocumentDeliveryTTN);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryImageTTN", row.MassSupplyDocumentDeliveryImageTTN);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryInvoice", row.MassSupplyDocumentDeliveryInvoice);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryImageInvoice", row.MassSupplyDocumentDeliveryImageInvoice);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryAmountUSA", row.MassSupplyDocumentDeliveryAmountUSA);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentDeliveryAmountRUS", row.MassSupplyDocumentDeliveryAmountRUS);

            //SupplyDocumentPayment
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentID", row.MassSupplyDocumentPaymentID);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentAmount", row.MassSupplyDocumentPaymentAmount);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentOperationType", row.MassSupplyDocumentPaymentOperationType);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentStatus", row.MassSupplyDocumentPaymentStatus);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentDescription", row.MassSupplyDocumentPaymentDescription);
            _sqlRequestSave.SetParametrValue("@p_MassSupplyDocumentPaymentRRN", row.MassSupplyDocumentPaymentRRN);

            _sqlRequestSave.ComplexRequest(get_save_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSave.SqlAnswer.result;
        }

        public Int32 SaveRowTable(LocalRow row)
        {
            //SupplyDocument
            _sqlRequestSaveTable.SetParametrValue("@p_UserID", row.UserID);
            _sqlRequestSaveTable.SetParametrValue("@p_ID", row.ID);
            _sqlRequestSaveTable.SetParametrValue("@p_Status", row.Status);
            _sqlRequestSaveTable.SetParametrValue("@p_Count", row.Count);
            _sqlRequestSaveTable.SetParametrValue("@p_Amount", row.Amount);
            _sqlRequestSaveTable.SetParametrValue("@p_SupplyDocumentNumber", row.SupplyDocumentNumber);

            //SupplyDocumentDetails
            _sqlRequestSaveTable.SetParametrValue("@p_tableDetails", row.ShemaStorаgeLocal.SupplyDocumentDetails);

            //SupplyDocumentDeliverry
            _sqlRequestSaveTable.SetParametrValue("@p_tableDelivery", row.ShemaStorаgeLocal.SupplyDocumentDelivery);

            //SupplyDocumentPayment
            _sqlRequestSaveTable.SetParametrValue("@p_tablePayment", row.ShemaStorаgeLocal.SupplyDocumentPayment);


            _sqlRequestSaveTable.ComplexRequest(get_save_procedure_table, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSaveTable.SqlAnswer.result;
        }

        public Int32 SetRow(LocalRow row)
        {
            //SupplyDocument
            _sqlRequestSet.SetParametrValue("@p_AddUserID", row.UserID);
            _sqlRequestSet.SetParametrValue("@p_DocumentID", row.ID);
            _sqlRequestSet.SetParametrValue("@p_DocumentNumber", row.SupplyDocumentNumber);

            _sqlRequestSet.ComplexRequest(set_store_procedure, CommandType.StoredProcedure, null);
            return (Int32)_sqlRequestSet.SqlAnswer.result;
        }

        public RowsFilters ConvertFilter(DataRow _dataRow, RowsFilters _localeRow)
        {
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);
            _localeRow.FiltersQuantityMin = convertData.ConvertDataDouble("QuantityMin");
            _localeRow.FiltersQuantityMax = convertData.ConvertDataDouble("QuantityMax");
            _localeRow.FiltersAmountMin = convertData.ConvertDataDouble("AmountMin");
            _localeRow.FiltersAmountMax = convertData.ConvertDataDouble("AmountMax");
            return _localeRow;
        }

        public LocalRow Convert(DataRow _dataRow, LocalRow _localeRow)
        {
            SupplyTypeList supplyTypeList = new SupplyTypeList();
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("ID");
            _localeRow.SupplyDocumentNumber = convertData.ConvertDataInt64("SupplyDocumentNumber");
            _localeRow.LineDocument= convertData.ConvertDataInt32("LineDocument");
            _localeRow.Status = convertData.ConvertDataInt32("Status");
            _localeRow.StatusString = supplyTypeList.innerList.FirstOrDefault(x => x.ID == _localeRow.Status) != null ?
                                            supplyTypeList.innerList.FirstOrDefault(x => x.ID == _localeRow.Status).Description : Properties.Resources.UndefindField;
            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            _localeRow.LastModificatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModificatedDateString = convertData.DateTimeConvertShortString(_localeRow.LastModificatedDate);
            _localeRow.CreatedUserID = convertData.ConvertDataInt32("CreatedUserID");
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("LUserId");
            _localeRow.CreatedUserIDString= convertData.ConvertDataString("CreatedUserIDString");
            _localeRow.LastModificatedUserIDString = convertData.ConvertDataString("LastModificatedUserIDString");
            _localeRow.Invoice = convertData.ConvertDataString("Invoice");
            _localeRow.TTN = convertData.ConvertDataString("TTN");
            _localeRow.ManagerName = convertData.ConvertDataString("ManagerName");
            _localeRow.Delivery = convertData.ConvertDataString("Delivery");
            _localeRow.Amount = (Decimal)convertData.ConvertDataDouble("Amount");
            _localeRow.Count = convertData.ConvertDataInt32("Count");
         
            return _localeRow;
        }

        //данные для суммы
        public void ConvertSummary(DataRow _dataRow, RowSummary _localeRow)
        {
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);
            _localeRow.SummaryQuantityLine = convertData.ConvertDataInt32("QuantityLine");
            _localeRow.SummaryQuantityProduct = convertData.ConvertDataInt32("ProductQuantity");
            _localeRow.SummaryProductTagPriceUSA = convertData.ConvertDataDecimal("ProductTagPriceUSA");
            _localeRow.SummaryProductTagPriceRUS = convertData.ConvertDataInt32("ProductTagPriceRUS");
            _localeRow.SummaryDeliveryQuantity = convertData.ConvertDataInt32("DeliveryQuantity");
            _localeRow.SummaryAmountUSA = convertData.ConvertDataDecimal("AmountUSA");
            _localeRow.SummaryAmountRUS = convertData.ConvertDataInt32("AmountRUS");
            _localeRow.SummaryPaymentBalans = convertData.ConvertDataDecimal("PaymentAmountBalans");
            _localeRow.SummaryPaymentRemains = convertData.ConvertDataDecimal("PaymentAmountRemains");

        }

        public DataTable GetFilter(String filterName)
        {
            return filters.FirstOrDefault(x => x.Key == filterName).Value;
        }

        public Range GetFromToFilter(String filterName)
        {
            return filtersFromTo.FirstOrDefault(x => x.Key == filterName).Value;
        }

        public void InitFilters()
        {
            DataTable table = FillGridAllFilter();
            FillFilter("CreatedByUserID", table);
            FillFilter("LastModifiedByUserID", table);
            FillFilter("Delivery", table);
            FillFilter("ManagerName", table);
            FillFilter("Status", table);

            RowsFilters rowsFilters;
            foreach(DataRow row in table.Rows)
            {
                rowsFilters = new RowsFilters();
                ConvertFilter(row, rowsFilters);
                filtersFromTo["Quantity"].min = rowsFilters.FiltersQuantityMin;
                filtersFromTo["Quantity"].max = rowsFilters.FiltersQuantityMax;
                filtersFromTo["Amount"].min = rowsFilters.FiltersAmountMin;
                filtersFromTo["Amount"].max = rowsFilters.FiltersAmountMax;
            }
        }

        public void FillFilter(String filterName, DataTable table) 
        {
            DataTable current1 = filters.FirstOrDefault(x => x.Key == filterName).Value;
            current1.Clear();
            foreach(DataRow row in table.Rows)
            {
                ConvertData convert = new ConvertData(row, new LocalRow());
                if (!String.IsNullOrEmpty(row[filterName].ToString()))
                {
                    String data = convert.ConvertDataString(filterName);
                    String[] dataarray = data.Split('|');
                    foreach(string curstr in dataarray.ToList())
                    {
                        String[] pair = curstr.Split(':');
                        DataRow newrow = current1.NewRow();
                        newrow["ID"] = pair[0];
                        newrow["IsChecked"] = false;
                        newrow["Description"] = pair[1];
                        current1.Rows.Add(newrow);
                    }
                }
            }
        }
    }
}
