using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.FormUsers.SaleDocumentProduct
{
    public class LocalRow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Int32 iD;
        private Int32 lineDocument;
        private Int32 documentID;
        private String name;
        private String model;
        private String sizeProduct;
        private Int32 productID;
        private Int32 quantity;
        private Decimal tagPriceWithVAT;
        private Decimal tagPriceWithoutVAT;
        private Int32 discountType;
        private String discountDescription;
        private Int32 reasonReturnType;
        private String reasonReturnDescription;
        private Decimal salePriceWithVAT;
        private Decimal salePriceWithoutVAT;
        private String serviseCardNumber;
        private String rRN;
        private String slip;
        private DateTime? createdDate;
        private DateTime? lastModifiedDate;
        private Int32 createdByUserID;
        private String createdDateString;
        private Int32 lastModifiedByUserID;
        private String lastModificatedDateString;

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

        public String Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public String SizeProduct
        {
            get
            {
                return sizeProduct;
            }

            set
            {
                sizeProduct = value;
                OnPropertyChanged("SizeProduct");
            }
        }
        public Int32 ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                productID = value;
                OnPropertyChanged("ProductID");
            }
        }

        public Int32 Quantity
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

        public Decimal TagPriceWithVAT
        {
            get
            {
                return tagPriceWithVAT;
            }

            set
            {
                tagPriceWithVAT = value;
                OnPropertyChanged("TagPriceWithVAT");
            }
        }

        public Decimal TagPriceWithoutVAT
        {
            get
            {
                return tagPriceWithoutVAT;
            }

            set
            {
                tagPriceWithoutVAT = value;
                OnPropertyChanged("TagPriceWithoutVAT");
            }
        }

        public Int32 DiscountType
        {
            get
            {
                return discountType;
            }

            set
            {
                discountType = value;
                OnPropertyChanged("DiscountType");
            }
        }

        public String DiscountDescription
        {
            get
            {
                return discountDescription;
            }

            set
            {
                discountDescription = value;
                OnPropertyChanged("DiscountDescription");
            }
        }

        public Int32 ReasonReturnType
        {
            get
            {
                return reasonReturnType;
            }

            set
            {
                reasonReturnType = value;
                OnPropertyChanged("ReasonReturnType");
            }
        }

        public String ReasonReturnDescription
        {
            get
            {
                return reasonReturnDescription;
            }

            set
            {
                reasonReturnDescription = value;
                OnPropertyChanged("ReasonReturnDescription");
            }
        }

        public Decimal SalePriceWithVAT
        {
            get
            {
                return salePriceWithVAT;
            }

            set
            {
                salePriceWithVAT = value;
                OnPropertyChanged("SalePriceWithVAT");
            }
        }

        public Decimal SalePriceWithoutVAT
        {
            get
            {
                return salePriceWithoutVAT;
            }

            set
            {
                salePriceWithoutVAT = value;
                OnPropertyChanged("SalePriceWithoutVAT");
            }
        }

        public String ServiseCardNumber
        {
            get
            {
                return serviseCardNumber;
            }

            set
            {
                serviseCardNumber = value;
                OnPropertyChanged("ServiseCardNumber");
            }
        }

        public String RRN
        {
            get
            {
                return rRN;
            }

            set
            {
                rRN = value;
                OnPropertyChanged("RRN");
            }
        }

        public String Slip
        {
            get
            {
                return slip;
            }

            set
            {
                slip = value;
                OnPropertyChanged("Slip");
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

        public DateTime? LastModifiedDate
        {
            get
            {
                return lastModifiedDate;
            }

            set
            {
                lastModifiedDate = value;
                OnPropertyChanged("LastModifiedDate");
            }
        }

        public Int32 CreatedByUserID
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

        public Int32 LastModifiedByUserID
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
        public LocalRow()
        {

        }
    }
    public class SaleDocumentProductLogic
    {
    }
}
