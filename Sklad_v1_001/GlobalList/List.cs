using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sklad_v1_001.GlobalList
{
    public class List
    {
    }
    public class DateTimeItem
    {
        public Int32 ID { get; set; }
        public String Description { get; set; }
    }
    //отвечает за ветрины
    public class Vetrina
    {
        public Int32 ID { get; set; }
        public String Description { get; set; }
    }
    //отвечает за категории
    public class Category
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
    //отвечает за подкатегории
    public class CategoryDetails
    {
        public Int32 ID { get; set; }
        public Int32 CategoryID { get; set; }
        public String CategoryIDString { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }

    //отвечает за подкатегории
    public class PaymentType
    {
        public Int32 ID { get; set; }
        public String Description { get; set; }
    }

    //отвечает за документы supply
    public class SupplyType
    {
        public Int32 ID { get; set; }
        public String Description { get; set; }
    }

    public class ProductStatus
    {
        public Int32 ID { get; set; }
        public String Description { get; set; }
    }

    public class DeliveryType
    {
        public Int32 ID { get; set; }
        public String Description { get; set; }
    }

    public class OperationType
    {
        public Int32 ID { get; set; }
        public String Description { get; set; }
    }

    public class GlobalPaymentTypeItem
    {
        public Int32 ID { get; set; }
        public string Description { get; set; }
    }

    public class DeliveryCompany
    {
        public Int32 ID { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public String Phones { get; set; }
        public String AdressCompany { get; set; }
    }
    public class DeliveryCompanyDetails
    {
        public Int32 ID { get; set; }       
        public Int32 DeliveryID { get; set; }
        public String DeliveryIDString { get; set; }
        public String ManagerName { get; set; }
        public String Phones { get; set; }
        public String Description { get; set; } // ManagerName
    }

    //отвечает за категории
    public class CompanyStatus
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
    //Используем для всплывающих popup при поиске
    public class RowListView
    {
        public Int64 ID { get; set; }
        public String Description { get; set; }
        public ImageSource Icon { get; set; }
    }

    public class DateTimeListFilter
    {
        public ObservableCollection<DateTimeItem> innerList { get; set; }
        public DateTimeListFilter()
        {
            innerList = new ObservableCollection<DateTimeItem>()
            {
                new DateTimeItem {ID=1,Description=Properties.Resources.DateTimeDescriptionAll },
                new DateTimeItem {ID=2,Description=Properties.Resources.DateTimeDescription1 },
                new DateTimeItem {ID=3,Description=Properties.Resources.DateTimeDescription2 },
                new DateTimeItem {ID=4,Description=Properties.Resources.DateTimeDescription3 },
                new DateTimeItem {ID=5,Description=Properties.Resources.DateTimeDescription4 },
            };
        }
    }

    public class VetrinaList
    {
        public ObservableCollection<Vetrina> innerList { get; set; }
        public VetrinaList()
        {
            innerList = new ObservableCollection<Vetrina>()
            {
                new Vetrina {ID=0,Description=Properties.Resources.Vitrina0 },
                new Vetrina {ID=1,Description=Properties.Resources.Vitrina1 },
                new Vetrina {ID=2,Description=Properties.Resources.Vitrina2 },
                new Vetrina {ID=3,Description=Properties.Resources.Vitrina3 },
                new Vetrina {ID=4,Description=Properties.Resources.Vitrina4 },
                new Vetrina {ID=5,Description=Properties.Resources.Vitrina5 },
                new Vetrina {ID=6,Description=Properties.Resources.Vitrina6 },
                new Vetrina {ID=7,Description=Properties.Resources.Vitrina7 },
                new Vetrina {ID=8,Description=Properties.Resources.Vitrina8 },
                new Vetrina {ID=9,Description=Properties.Resources.Vitrina9 },
                new Vetrina {ID=10,Description=Properties.Resources.Vitrina10 },
            };
        }
    }

    public class CategoryList
    {
        public ObservableCollection<Category> innerList { get; set; }
        public CategoryList()
        {
            innerList = new ObservableCollection<Category>()
            {
                new Category {ID=0,Description=Properties.Resources.Category0 },
                new Category {ID=1,Description=Properties.Resources.Category1 },
                new Category {ID=2,Description=Properties.Resources.Category2 },
            };
        }
    }

    public class CategoryDetailsList
    {
        public ObservableCollection<CategoryDetails> innerList { get; set; }
        public CategoryDetailsList()
        {
            innerList = new ObservableCollection<CategoryDetails>()
            {
                new CategoryDetails {ID=0,Description=Properties.Resources.CategoryDetails0 },
                new CategoryDetails {ID=1,Description=Properties.Resources.CategoryDetails1 },
                new CategoryDetails {ID=2,Description=Properties.Resources.CategoryDetails2 },
            };
        }
    }

    public class PaymentTypeList
    {
        public ObservableCollection<PaymentType> innerList { get; set; }
        public PaymentTypeList()
        {
            innerList = new ObservableCollection<PaymentType>()
            {
                new PaymentType {ID=0,Description=Properties.Resources.Payment2 },
                new PaymentType {ID=1,Description=Properties.Resources.Payment1 }
                            
            };
        }
    }

    public class SupplyTypeList
    {
        public ObservableCollection<SupplyType> innerList { get; set; }
        public SupplyTypeList()
        {
            innerList = new ObservableCollection<SupplyType>()
            {
                new SupplyType {ID=0,Description=Properties.Resources.SupplyDocument1 },
                new SupplyType {ID=1,Description=Properties.Resources.SupplyDocument2 },
                new SupplyType {ID=2,Description=Properties.Resources.SupplyDocument3 },           
                new SupplyType {ID=3,Description=Properties.Resources.SupplyDocument5 },
                new SupplyType {ID=4,Description=Properties.Resources.SupplyDocument4 },
                new SupplyType {ID=6,Description=Properties.Resources.SupplyDocument6 },
                new SupplyType {ID=7,Description=Properties.Resources.SupplyDocument7 },
                new SupplyType {ID=8,Description=Properties.Resources.SupplyDocument8 }               
            };
        }
    }

    //DeliveryType
    public class DeliveryTypeList
    {
        public ObservableCollection<DeliveryType> innerList { get; set; }
        public DeliveryTypeList()
        {
            innerList = new ObservableCollection<DeliveryType>()
            {
                new DeliveryType {ID=0,Description=Properties.Resources.DeliveryType1 },
                new DeliveryType {ID=1,Description=Properties.Resources.DeliveryType2 },
                new DeliveryType {ID=2,Description=Properties.Resources.DeliveryType3 },
                new DeliveryType {ID=2,Description=Properties.Resources.DeliveryType4 },
            };
        }
    }

    public class OperationTypeTypeList
    {
        public ObservableCollection<OperationType> innerList { get; set; }
        public OperationTypeTypeList()
        {
            innerList = new ObservableCollection<OperationType>()
            {
                new OperationType {ID=0,Description=Properties.Resources.OperationType1 },
                new OperationType {ID=1,Description=Properties.Resources.OperationType2 },
                new OperationType {ID=2,Description=Properties.Resources.OperationType3 },
                new OperationType {ID=3,Description=Properties.Resources.OperationType4 },
            };
        }
    }
    public class ShortGlobalPaymentTypeList
    {
        public ObservableCollection<GlobalPaymentTypeItem> innerList { get; set; }
        public ShortGlobalPaymentTypeList()
        {
            innerList = new ObservableCollection<GlobalPaymentTypeItem>()
            {
                 new GlobalPaymentTypeItem { ID=0, Description = Properties.Resources.Payment1 },
                 //new GlobalPaymentTypeItem { ID=2, Description=Properties.Resources.Payment10 }, // закрыть кредит
                 new GlobalPaymentTypeItem { ID = 1, Description = Properties.Resources.Payment2 } // банковская карта
            };
        }
    }

    public class ProductStatusList
    {
        public ObservableCollection<ProductStatus> innerList { get; set; }
        public ProductStatusList()
        {
            innerList = new ObservableCollection<ProductStatus>()
            {
                new ProductStatus {ID=0,Description=Properties.Resources.ProductStatus0 },//в магазине
                new ProductStatus {ID=1,Description=Properties.Resources.ProductStatus1 },//резерв(продажа)
                new ProductStatus {ID=2,Description=Properties.Resources.ProductStatus2 },//резервх(возврат)
                new ProductStatus {ID=3,Description=Properties.Resources.ProductStatus3 },//Покупка
                new ProductStatus {ID=4,Description=Properties.Resources.ProductStatus4 },//Отправлено
                new ProductStatus {ID=5,Description=Properties.Resources.ProductStatus5 },//Перемещение
                new ProductStatus {ID=6,Description=Properties.Resources.ProductStatus6 },//Возврат
                new ProductStatus {ID=9,Description=Properties.Resources.ProductStatus9 },//Продажа
            };
        }
    }

    //Company
    public class CompanyStatusList
    {
        public ObservableCollection<CompanyStatus> innerList { get; set; }
        public CompanyStatusList()
        {
            innerList = new ObservableCollection<CompanyStatus>()
            {
                new CompanyStatus {ID=0,Description=Properties.Resources.CompanyStatus0 },//черновик
                new CompanyStatus {ID=1,Description=Properties.Resources.CompanyStatus1 },//на регистрации
                new CompanyStatus {ID=2,Description=Properties.Resources.CompanyStatus2 },//Зарегистрирован
                new CompanyStatus {ID=3,Description=Properties.Resources.CompanyStatus3 },//Отказано
            };
        }
    }
}
