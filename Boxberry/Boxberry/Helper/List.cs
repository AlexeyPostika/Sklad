using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxberry.Helper
{

    public class StatusOrderStraight
    {
        public Int32 ID { set; get; }
        public string Description { set; get; }
    }

    public class StatusOrderReturnable
    {
        public Int32 ID { set; get; }
        public string Description { set; get; }
    }
    //Статусы прямого потока:
    public class StatusOrderStraightList
    {
        public ObservableCollection<StatusOrderStraight> innerList { get; set; }
        public StatusOrderStraightList()
        {
            innerList = new ObservableCollection<StatusOrderStraight>()
            {
                  new StatusOrderStraight { ID=0, Description=Properties.Resource.LoadedIMRegistry },
                  new StatusOrderStraight { ID=1, Description=Properties.Resource.AcceptedForDelivery },
                  new StatusOrderStraight { ID=2, Description=Properties.Resource.SubmittedForSorting },
                  new StatusOrderStraight { ID=3, Description=Properties.Resource.SentToTheSortingTerminal },
                  new StatusOrderStraight { ID=4, Description=Properties.Resource.SentToDestinationCity },
                  new StatusOrderStraight { ID=5, Description=Properties.Resource.TransferredForDeliveryToThePointOfIssue },
                  new StatusOrderStraight { ID=6, Description=Properties.Resource.TransferredToCourierDelivery },
                  new StatusOrderStraight { ID=7, Description=Properties.Resource.ReceivedAtThePointOfIssue },
                  new StatusOrderStraight { ID=8, Description=Properties.Resource.IssuedBy }                
            };
        }
    }
    //Статусы возвратного потока:
    public class StatusOrderReturnableList
    {
        public ObservableCollection<StatusOrderReturnable> innerList { get; set; }
        public StatusOrderReturnableList()
        {
            innerList = new ObservableCollection<StatusOrderReturnable>()
            {
                  new StatusOrderReturnable { ID=0, Description=Properties.Resource.ReturnedWithCourierDelivery },
                  new StatusOrderReturnable { ID=1, Description=Properties.Resource.PreparingToReturn },
                  new StatusOrderReturnable { ID=2, Description=Properties.Resource.SentToTheCollectionPoint },
                  new StatusOrderReturnable { ID=3, Description=Properties.Resource.ReturnedToCollectionPoint },
                  new StatusOrderReturnable { ID=4, Description=Properties.Resource.ReturnedToIM }               
           };
        }
    }
    class List
    {
    }
}
