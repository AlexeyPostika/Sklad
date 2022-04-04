using Sklad_v1_001.GlobalList;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sklad_v1_001.FormUsers.Product
{
    public class LocalFilter : INotifyPropertyChanged
    {
        private string screenTypeGrid;

        private Int32 rowCountPage;
        private Int32 pageCountRow;

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

        public Int32 PageCountRow
        {
            get
            {
                return pageCountRow;
            }

            set
            {
                pageCountRow = value;
                OnPropertyChanged("PageNumber");
            }
        }

        public Int32 RowCountPage
        {
            get
            {
                return rowCountPage;
            }

            set
            {
                rowCountPage = value;
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
            PageCountRow = 0;
            RowCountPage = 16;
        }

    }
    public class LocalRow : IAbstractRow, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Int32 iD;
        private String name;
        private Int32 categoryID;
        private String categoryName;
        private String categoryDescription;
        private Int32 categoryDetailsID;
        private String categoryDetailsName;
        private String categoryDetailsDescription;
        private Int32 procreatorID; //производитель
        private String procreatorIDName;
        private Int32 showcaseID;
        private String showcaseIDName;
        private String description;

        private String model;
        private String barCodeString;
        private Int32 quantity;
        private Decimal tagPriceUSA;
        private Decimal tagPriceRUS;
        private String sizeProduct;
        private Boolean package;
        private Int32 radioType;
        private DateTime? createdDate;
        private String createdDateString;
        private Int32 createdUserID;
        private String displayNameUser;
        private String shortDisplayNameUser;
        private String lDisplayNameUser;
        private String shortLDisplayNameUser;

        private DateTime? lastModicatedDate;
        private String lastModificatedDateText;
        private Int32 lastModificatedUserID;
        private Int32 status;
        private String statusString;

        private String invoice;
        private Byte[] invoiceDocumentByte;
        private ImageSource imageSourceInvoice;

        private String tTN;
        private Byte[] tTNDocumentByte;
        private ImageSource imageSourceTTN;
        
        private Byte[] photoImageByte;
        private ImageSource photoImage;

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
        //categoryID
        public Int32 CategoryID
        {
            get
            {
                return categoryID;
            }

            set
            {
                categoryID = value;
                OnPropertyChanged("CategoryID");
            }
        }
        public String CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public string CategoryDescription
        {
            get
            {
                return categoryDescription;
            }

            set
            {
                categoryDescription = value;
                OnPropertyChanged("CategoryDescription");
            }
        }
        public int CategoryDetailsID
        {
            get
            {
                return categoryDetailsID;
            }

            set
            {
                categoryDetailsID = value;
                OnPropertyChanged("CategoryDetailsID");
            }
        }
        public string CategoryDetailsName
        {
            get
            {
                return categoryDetailsName;
            }

            set
            {
                categoryDetailsName = value;
                OnPropertyChanged("CategoryDetailsName");
            }
        }
        public string CategoryDetailsDescription
        {
            get
            {
                return categoryDetailsDescription;
            }

            set
            {
                categoryDetailsDescription = value;
                OnPropertyChanged("CategoryDetailsDescription");
            }
        }

        public string Model
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

        public string BarCodeString
        {
            get
            {
                return barCodeString;
            }

            set
            {
                barCodeString = value;
                OnPropertyChanged("BarCodeString");
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
        //PhonesDetails
        public Decimal TagPriceUSA
        {
            get
            {
                return tagPriceUSA;
            }

            set
            {
                tagPriceUSA = value;
                OnPropertyChanged("TagPriceUSA");
            }
        }
        public Decimal TagPriceRUS
        {
            get
            {
                return tagPriceRUS;
            }

            set
            {
                tagPriceRUS = value;
                OnPropertyChanged("TagPriceRUS");
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

        public Boolean Package
        {
            get
            {
                return package;
            }

            set
            {
                package = value;
                OnPropertyChanged("Package");
            }
        }

        public Int32 RadioType
        {
            get
            {
                return radioType;
            }

            set
            {
                radioType = value;
                OnPropertyChanged("RadioType");
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

        public  String CreatedDateString
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
        public int CreatedUserID
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
        public DateTime? LastModicatedDate
        {
            get
            {
                return lastModicatedDate;
            }

            set
            {
                lastModicatedDate = value;
                if (value != null)
                {
                    if (!String.IsNullOrEmpty(value.Value.ToString()))
                    {
                        ConvertData convertData = new ConvertData();
                        convertData.DateTimeConvertShortDateString(value);
                    }
                }
                OnPropertyChanged("LastModicatedDate");
            }
        }
        public int LastModificatedUserID
        {
            get
            {
                return lastModificatedUserID;
            }

            set
            {
                lastModificatedUserID = value;
                OnPropertyChanged("LastModicatedUserID");
            }
        }

        public String DisplayNameUser
        {
            get
            {
                return displayNameUser;
            }

            set
            {
                displayNameUser = value;
                OnPropertyChanged("DisplayNameUser");
            }
        }

        public String ShortDisplayNameUser
        {
            get
            {
                return shortDisplayNameUser;
            }

            set
            {
                shortDisplayNameUser = value;
                OnPropertyChanged("ShortDisplayNameUser");
            }
        }

        public String LDisplayNameUser
        {
            get
            {
                return lDisplayNameUser;
            }

            set
            {
                lDisplayNameUser = value;
                OnPropertyChanged("LDisplayNameUser");
            }
        }

        public String ShortLDisplayNameUser
        {
            get
            {
                return shortLDisplayNameUser;
            }

            set
            {
                shortLDisplayNameUser = value;
                OnPropertyChanged("ShortLDisplayNameUser");
            }
        }

        //lastModifiadDateText
        public String LastModificatedDateText
        {
            get
            {
                return lastModificatedDateText;
            }

            set
            {
                lastModificatedDateText = value;
                OnPropertyChanged("LastModifiadDateText");
            }
        }
        public string Invoice
        {
            get
            {
                return invoice;
            }

            set
            {
                invoice = value;
                OnPropertyChanged("Invoice");
            }
        }
        public byte[] InvoiceDocumentByte
        {
            get
            {
                return invoiceDocumentByte;
            }

            set
            {
                invoiceDocumentByte = value;
                if (value != null)
                {
                    imageSourceInvoice = ImageHelper.GenerateImage("IconOK16.png");
                }
                else
                {
                    imageSourceInvoice = ImageHelper.GenerateImage("IconMinus.png");
                }
                OnPropertyChanged("InvoiceDocumentByte");
            }
        }
        //photoImageByte

        public byte[] PhotoImageByte
        {
            get
            {
                return photoImageByte;
            }

            set
            {
                photoImageByte = value;
                if (value != null)
                {
                    //PhotoImage = ImageHelper.GenerateImage("IconNotCamera_X80.png");
                }
                else
                {
                    PhotoImage = ImageHelper.GenerateImage("IconNotCamera_X80.png");
                }
                OnPropertyChanged("PhotoImageByte");
            }
        }
        public ImageSource PhotoImage
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

        public ImageSource ImageSourceInvoice
        {
            get
            {
                return imageSourceInvoice;
            }

            set
            {
                imageSourceInvoice = value;
                OnPropertyChanged("ImageSourceInvoice");
            }
        }
        public string TTN
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
        public byte[] TTNDocumentByte
        {
            get
            {
                return tTNDocumentByte;
            }

            set
            {
                tTNDocumentByte = value;
                if (value != null)
                {
                    imageSourceTTN = ImageHelper.GenerateImage("IconOK16.png");
                }
                else
                {
                    imageSourceTTN = ImageHelper.GenerateImage("IconMinus.png");
                }
                OnPropertyChanged("TTNDocumentByte");
            }
        }

        public ImageSource ImageSourceTTN
        {
            get
            {
                return imageSourceTTN;
            }

            set
            {
                imageSourceTTN = value;
                OnPropertyChanged("ImageSourceTTN");
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
                if (!String.IsNullOrEmpty(status.ToString()))
                {
                    DeliveryTypeList deliveryTypeList = new DeliveryTypeList();
                    statusString = deliveryTypeList.innerList.FirstOrDefault(x => x.ID == status) != null ? deliveryTypeList.innerList.FirstOrDefault(x => x.ID == status).Description : Properties.Resources.UndefindField;
                }
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
     
        public Int32 ProcreatorID
        {
            get
            {
                return procreatorID;
            }

            set
            {
                procreatorID = value;
                OnPropertyChanged("ProcreatorID");
            }
        }

        public String ProcreatorIDName
        {
            get
            {
                return procreatorIDName;
            }

            set
            {
                procreatorIDName = value;
                OnPropertyChanged("ProcreatorIDName");
            }
        }
        public String Description
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
        
        public Int32 ShowcaseID
        {
            get
            {
                return showcaseID;
            }

            set
            {
                showcaseID = value;
                OnPropertyChanged("ShowcaseID");
            }
        }

        public String ShowcaseIDName
        {
            get
            {
                return showcaseIDName;
            }

            set
            {
                showcaseIDName = value;
                OnPropertyChanged("ShowcaseIDName");
            }
        }

        public LocalRow()
        {
            ImageSourceTTN = ImageHelper.GenerateImage("IconMinus.png");
            ImageSourceInvoice = ImageHelper.GenerateImage("IconMinus.png");
        }
    }
    public class ProductLogic
    {
        ConvertData convertData;

        string get_store_procedure = "xp_GetProductTable";

        SQLCommanSelect _sqlRequestSelect = null;

        //результат запроса
        DataTable _data = null;
        DataTable _datarow = null;
        public ProductLogic()
        {
            convertData = new ConvertData();

            _data = new DataTable();
            _datarow = new DataTable();

            _sqlRequestSelect = new SQLCommanSelect();

            _sqlRequestSelect.AddParametr("@p_RowCountPage", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_RowCountPage", 0);

            _sqlRequestSelect.AddParametr("@p_PageCountRow", SqlDbType.Int);
            _sqlRequestSelect.SetParametrValue("@p_PageCountRow", 0);

        }
        public DataTable FillGrid(LocalFilter localFilter)
        {
            _sqlRequestSelect.SqlAnswer.datatable.Clear();
            _data.Clear();
            _sqlRequestSelect.SetParametrValue("@p_RowCountPage", localFilter.RowCountPage);
            _sqlRequestSelect.SetParametrValue("@p_PageCountRow", localFilter.PageCountRow);

            _sqlRequestSelect.ComplexRequest(get_store_procedure, CommandType.StoredProcedure, null);
            _data = _sqlRequestSelect.SqlAnswer.datatable;
            return _data;
        }

        public LocalRow Convert(DataRow _dataRow, LocalRow _localeRow)
        {
            ProductStatusList statusList = new ProductStatusList();
            ConvertData convertData = new ConvertData(_dataRow, _localeRow);

            _localeRow.ID = convertData.ConvertDataInt32("ID");         
            _localeRow.Status = convertData.ConvertDataInt32("Status");
            _localeRow.StatusString = statusList.innerList.FirstOrDefault(x => x.ID == _localeRow.Status) != null ?
                                            statusList.innerList.FirstOrDefault(x => x.ID == _localeRow.Status).Description : Properties.Resources.UndefindField;
           
            _localeRow.Name= convertData.ConvertDataString("Name");
            _localeRow.CategoryID = convertData.ConvertDataInt32("CategoryID");
            _localeRow.CategoryName= convertData.ConvertDataString("CategoryName");
            _localeRow.CategoryDetailsID = convertData.ConvertDataInt32("CategoryDetailsID");
            _localeRow.CategoryDetailsName = convertData.ConvertDataString("CategoryDetailsName");

            _localeRow.BarCodeString = convertData.ConvertDataString("BarCodeString");
            _localeRow.ProcreatorID = convertData.ConvertDataInt32("ProcreatorID");
            _localeRow.ProcreatorIDName= convertData.ConvertDataString("ProcreatorIDName");

            _localeRow.Model = convertData.ConvertDataString("Model");
            _localeRow.TagPriceUSA = convertData.ConvertDataDecimal("TagPriceUSA");
            _localeRow.TagPriceRUS = convertData.ConvertDataDecimal("TagPriceRUS");
            _localeRow.Description = convertData.ConvertDataString("Description");
            _localeRow.Quantity = convertData.ConvertDataInt32("Quantity");
            _localeRow.ShowcaseID = convertData.ConvertDataInt32("ShowcaseID");
            _localeRow.ShowcaseIDName = convertData.ConvertDataString("ShowcaseIDName");
            _localeRow.SizeProduct= convertData.ConvertDataString("SizeProduct");

            _localeRow.CreatedUserID = convertData.ConvertDataInt32("CreatedUserID");
            _localeRow.LastModificatedUserID = convertData.ConvertDataInt32("LastModificatedUserID");

            _localeRow.DisplayNameUser = convertData.ConvertDataString("DisplayNameUser");
            _localeRow.ShortDisplayNameUser = convertData.ConvertDataString("ShortDisplayNameUser");
            _localeRow.LDisplayNameUser = convertData.ConvertDataString("LDisplayNameUser");
            _localeRow.ShortLDisplayNameUser = convertData.ConvertDataString("ShortLDisplayNameUser");

            _localeRow.CreatedDate = convertData.ConvertDataDateTime("CreatedDate");
            _localeRow.CreatedDateString = convertData.DateTimeConvertShortString(_localeRow.CreatedDate);
            _localeRow.LastModicatedDate = convertData.ConvertDataDateTime("LastModificatedDate");
            _localeRow.LastModificatedDateText = convertData.DateTimeConvertShortString(_localeRow.LastModicatedDate);
            _localeRow.PhotoImageByte = null;

            return _localeRow;
        }
    }
}
