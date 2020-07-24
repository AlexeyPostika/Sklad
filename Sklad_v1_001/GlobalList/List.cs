﻿using System;
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
    //отвечает за ветрины
    public class Category
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
                new DateTimeItem {ID=0,Description=Properties.Resources.DateTimeDescription1 },
                new DateTimeItem {ID=0,Description=Properties.Resources.DateTimeDescription2 },
                new DateTimeItem {ID=0,Description=Properties.Resources.DateTimeDescription3 },
                new DateTimeItem {ID=0,Description=Properties.Resources.DateTimeDescription4 },
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
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina0 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina1 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina2 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina3 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina4 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina5 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina6 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina7 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina8 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina9 },
                new Vetrina {ID=0,Description=Properties.Resources.Vetrina10 },
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
                new Category {ID=0,Description=Properties.Resources.Category1 },          
            };
        }
    }
}
