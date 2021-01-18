using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boxberry;

namespace TestMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            Console.WriteLine("-------------Город отправителя-------------------------------"); //данные получаем
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            if (Boxberry.BoxberrySitySender.SitySenderSelect() != null)
            {
                foreach(var row in Boxberry.BoxberrySitySender.codeSitySender)
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

            if (Boxberry.CalculatorBoxberry.Calculator(500, "10.012", 78031,0,0,100,120,80,50) != null)
            {                
                    Console.WriteLine("Стоимость={0}, Базовая стоимость = {1}, Стоимость сервиса = {2}, время в пути={3}",Boxberry.CalculatorBoxberry.PriceTotal, Boxberry.CalculatorBoxberry.PriceBase, Boxberry.CalculatorBoxberry.PriceService, Boxberry.CalculatorBoxberry.DeliveryPeriod); //данные получаем                
            }

            Console.WriteLine("-------------------------------------------------------------"); //данные получаем
            Console.WriteLine("-------------------Калькулятор склад  - КД-------------------"); //данные получаем
            Console.WriteLine("-------------------------------------------------------------"); //данные получаем

            if (Boxberry.CalculatorKDBoxberry.Calculator(500, "010", 0, 0,  100, 120, 80, 50, "624000") != null)
            {
                Console.WriteLine("Стоимость={0}, Базовая стоимость = {1}, Стоимость сервиса = {2}, время в пути={3}", Boxberry.CalculatorKDBoxberry.PriceTotal, Boxberry.CalculatorKDBoxberry.PriceBase, Boxberry.CalculatorKDBoxberry.PriceService, Boxberry.CalculatorKDBoxberry.DeliveryPeriod); //данные получаем                
            }
        }
    }
}
