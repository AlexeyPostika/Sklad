using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public String Description { get; set; }
    }
    //отвечает за подкатегории
    public class CategoryDetails
    {
        public Int32 ID { get; set; }
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
    public class DateTimeListFilter
    {
        public ObservableCollection<DateTimeItem> innerList { get; set; }
        public DateTimeListFilter()
        {
            innerList = new ObservableCollection<DateTimeItem>()
            {
                new DateTimeItem {ID=0,Description=Properties.Resources.DateTimeDescriptionAll },
                new DateTimeItem {ID=1,Description=Properties.Resources.DateTimeDescription1 },
                new DateTimeItem {ID=2,Description=Properties.Resources.DateTimeDescription2 },
                new DateTimeItem {ID=3,Description=Properties.Resources.DateTimeDescription3 },
                new DateTimeItem {ID=4,Description=Properties.Resources.DateTimeDescription4 },
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
                new PaymentType {ID=0,Description=Properties.Resources.Payment1 },
                new PaymentType {ID=1,Description=Properties.Resources.Payment2 },
                new PaymentType {ID=2,Description=Properties.Resources.Payment3 },
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
                new SupplyType {ID=2,Description=Properties.Resources.SupplyDocument4 },
            };
        }
    }
}
