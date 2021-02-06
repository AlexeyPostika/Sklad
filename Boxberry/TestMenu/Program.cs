using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxberry;
using CDEK;
using CDEK.Helper;

namespace TestMenu
{
    class Program
    {
        static void Main(string[] args)
        {

            OrderStatusLinke orderStatusLinke = new OrderStatusLinke();
            OrderStatusLinke.OrderStatusSelect("0000164226759");

            AdressRowDispatch adressRowDispatch = new AdressRowDispatch();
            adressRowDispatch.Version = "1.0";
            adressRowDispatch.DateExecute = DateTime.Now;
            adressRowDispatch.SenderCityId = 44;
            adressRowDispatch.ReceiverCityId = 44;
            adressRowDispatch.TariffId.Add(new Tariff() { ID = 1 });
            adressRowDispatch.Goods.Add(new Good() { Height = 10, Length = 5, Width = 20, Weight = 0.3 });
            adressRowDispatch.Services.Add(new Services() { ID = 7 });
            AdressRowReceive result = new AdressRowReceive();
            result = CalculatorPriority.Calculator(adressRowDispatch);
            if (result != null)
            {
                if (result.Error != null && result.Error.Count>0)
                {
                    foreach (CalculatorError row1 in result.Error)
                    {
                        Console.WriteLine("ErrorID = {0}; Description={1};", row1.ID, row1.Description);
                    }

                }
                else
                {
                    Console.WriteLine("price = {0}; deliveryPeriodMin={1}; deliveryPeriodMax={2}; deliveryDateMin = {3}, deliveryDateMax = {4}; tariffId={5}; cashOnDelivery = {6}; priceByCurrency={7}; currency = {8};"
                        , result.Price, result.DeliveryPeriodMin, result.DeliveryPeriodMax, result.DeliveryDateMin, result.DeliveryDateMax, result.TariffId, result.CashOnDelivery, result.PriceByCurrency, result.Currency);

                }
            }

            //AdressRowReportOrder adressRowReportOrder = new AdressRowReportOrder();
            //adressRowReportOrder.InfoRequest.Account = "account";
            //adressRowReportOrder.InfoRequest.Secure = "secure";
            //adressRowReportOrder.InfoRequest.Date = DateTime.Now;
            //adressRowReportOrder.Order.DispatchNumber = 1000000000;

            //ResponseReportOrder resultReportOrder = new ResponseReportOrder();
            //resultReportOrder = ReportOrder.Calculator(adressRowReportOrder);
            //if (resultReportOrder != null)
            //{

            //}

            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            Console.WriteLine("-------------Город отправителя-------------------------------"); //данные получаем
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            if (Boxberry.BoxberrySitySender.SitySenderSelect() != null)
            {
                foreach (var row in Boxberry.BoxberrySitySender.codeSitySender)
                {
                    Console.WriteLine(row.Key + "---" + row.Value.Name); //данные получаем
                }
            }
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            Console.WriteLine("-------------Город получателя--------------------------------"); //данные получаем
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            if (Boxberry.BoxberrySityRecipient.SityRecipientSelect() != null)
            {
                foreach (var row in BoxberrySityRecipient.codeSityRecipient)
                {
                    Console.WriteLine(row.Key + "---" + row.Value.Name); //данные получаем
                }
            }

            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            Console.WriteLine("-----------------Калькулятор склад  - склад------------------"); //данные получаем
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем

            if (Boxberry.CalculatorBoxberry.Calculator(500, "10.012", 78031, 0, 0, 100, 120, 80, 50) != null)
            {
                Console.WriteLine("Стоимость={0}, Базовая стоимость = {1}, Стоимость сервиса = {2}, время в пути={3}", Boxberry.CalculatorBoxberry.PriceTotal, Boxberry.CalculatorBoxberry.PriceBase, Boxberry.CalculatorBoxberry.PriceService, Boxberry.CalculatorBoxberry.DeliveryPeriod); //данные получаем                
            }

            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            Console.WriteLine("-------------------Калькулятор склад  - КД-------------------"); //данные получаем
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем

            if (Boxberry.CalculatorKDBoxberry.Calculator(500, "010", 0, 0, 100, 120, 80, 50, "624000") != null)
            {
                Console.WriteLine("Стоимость={0}, Базовая стоимость = {1}, Стоимость сервиса = {2}, время в пути={3}", Boxberry.CalculatorKDBoxberry.PriceTotal, Boxberry.CalculatorKDBoxberry.PriceBase, Boxberry.CalculatorKDBoxberry.PriceService, Boxberry.CalculatorKDBoxberry.DeliveryPeriod); //данные получаем                
            }
        }
    }
}
