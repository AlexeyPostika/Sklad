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
}
