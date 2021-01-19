using CDEK.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
//using System.Text.Json.JsonSerializer;

namespace CDEK
{
    #region калькулятор
    public class Tariff
    {
        ServicesTariffsCDEKList servicesTariffsCDEKList;
        Int32 priority;
        Int32 iD;
        Int32 modeld;
        List<ServicesTariffsCDEK> servicesTariffsCDEKs;

        public int Priority { get => priority; set => priority = value; }
        public int ID { get => iD; set => iD = value; }
        public int Modeld { get => modeld; set => modeld = value; }
        public List<ServicesTariffsCDEK> ServicesTariffsCDEKs { get => servicesTariffsCDEKs; set => servicesTariffsCDEKs = value; }

        public Tariff()
        {
            ServicesTariffsCDEKs = new List<ServicesTariffsCDEK>();
        }

    }
    //форма для отправки данных
    public class AdressRowDispatch
    {
        //наименование переменной -- описание -- тип -- обяз.для запонения
        String version;// -- Версия используемого API -- “1.0” -- string -- да
        String authLogin;// Идентификатор ИМ(логин)    string нет
        String secure;// Ключ    string нет
        DateTime dateExecute;// Планируемая дата отправки заказа в формате “ГГГГ-ММ-ДД”	date нет
        String lang;// Локализация названий городов.По умолчанию "rus"	string(3)   нет
        //основные данные
        String senderCountryCode;// Код страны отправителя в формате ISO_3166-1_alpha-2 (см. “Общероссийский классификатор стран мира”). По умолчанию - ru. string (2)	нет
        String receiverCountryCode;// Код страны получателя в формате ISO_3166-1_alpha-2 (см. “Общероссийский классификатор стран мира”). По умолчанию - ru.  string (2)	нет
        Int32 senderCityId;// Код города отправителя из базы СДЭК(https://cdek.ru/storage/source/document/1/CDEK_city.zip)	integer	да
        String senderCity;// Наименование города отправителя string нет
        Int32 senderCityPostCode;// Индекс города отправителя из базы СДЭК (https://cdek.ru/storage/source/document/1/CDEK_city.zip)	integer	да
        Int32 receiverCityId;// Код города получателя из базы СДЭК (https://cdek.ru/storage/source/document/1/CDEK_city.zip)	integer	да
        Int32 receiverCityPostCode;// Индекс города получателя из базы СДЭК (https://cdek.ru/storage/source/document/1/CDEK_city.zip)	integer	да
        String receiverCity;// Наименование города получателя  string нет
        Double senderLongitude;// Долгота города отправителя  float нет
        Double receiverLongitude;// Долгота города получателя   float нет
        Double senderLatitude;// Широта города отправителя   float нет
        Double receiverLatitude;// Широта города получателя    float нет
        //тарифы
        List<Tariff> tariffId;//Код выбранного тарифа (подробнее см. приложение 1)  integer да
        //посылка
        List<Good> goods;// Габаритные характеристики упаковки да      
        //услуги
        List<Services> services;// Список передаваемых дополнительных услуг(подробнее см. приложение 2) нет      

        public string Version { get => version; set => version = value; }
        public string AuthLogin { get => authLogin; set => authLogin = value; }
        public string Secure { get => secure; set => secure = value; }
        public DateTime DateExecute { get => dateExecute; set => dateExecute = value; }
        public string Lang { get => lang; set => lang = value; }
        public string SenderCountryCode { get => senderCountryCode; set => senderCountryCode = value; }
        public string ReceiverCountryCode { get => receiverCountryCode; set => receiverCountryCode = value; }
        public int SenderCityId { get => senderCityId; set => senderCityId = value; }
        public string SenderCity { get => senderCity; set => senderCity = value; }
        public int SenderCityPostCode { get => senderCityPostCode; set => senderCityPostCode = value; }
        public int ReceiverCityId { get => receiverCityId; set => receiverCityId = value; }
        public int ReceiverCityPostCode { get => receiverCityPostCode; set => receiverCityPostCode = value; }
        public string ReceiverCity { get => receiverCity; set => receiverCity = value; }
        public double SenderLongitude { get => senderLongitude; set => senderLongitude = value; }
        public double ReceiverLongitude { get => receiverLongitude; set => receiverLongitude = value; }
        public double SenderLatitude { get => senderLatitude; set => senderLatitude = value; }
        public double ReceiverLatitude { get => receiverLatitude; set => receiverLatitude = value; }
        public List<Tariff> TariffId { get => tariffId; set => tariffId = value; }
        public List<Good> Goods { get => goods; set => goods = value; }
        public List<Services> Services { get => services; set => services = value; }

        public AdressRowDispatch()
        {
            Goods = new List<Good>();
            TariffId = new List<Tariff>();
            Services = new List<Services>();
        }
    }

    //форма для получения ответа
    public class AdressRowReceive
    {
        List<CalculatorError> error;// Массив ошибок при их возникновении(подробнее см. приложение 9)	   
        String price;// Сумма за доставку в рублях  double
        String deliveryPeriodMin;// Минимальное время доставки в днях   integer
        String deliveryPeriodMax;// Максимальное время доставки в днях  integer
        String deliveryDateMin;// Минимальная дата доставки, формате 'ГГГГ-ММ-ДД', например “2018-07-29”	string
        String deliveryDateMax;// Максимальная дата доставки, формате 'ГГГГ-ММ-ДД', например “2018-07-30”	string
        String tariffId;//Код тарифа, по которому рассчитана сумма доставки integer
        String cashOnDelivery;// Ограничение оплаты наличными, появляется только если оно есть   float
        String priceByCurrency;// Цена в валюте взаиморасчетов.Валюта определяется по authLogin и secure.    float
        String currency;//Валюта интернет-магазина (подробнее см. приложение 3)   string
        String percentVAT;// Размер ставки НДС для данного клиента.Появляется в случае, если переданы authLogin и secure, по ним же определяется ставка ИМ.Если ставка НДС не предусмотрена условиями договора, данный параметр не будет отображен.integer
        List<Services> services;//Список передаваемых дополнительных услуг (подробнее см. приложение 2)       

        public List<CalculatorError> Error { get => error; set => error = value; }

        public String Price { get => price; set => price = value; }
        public String DeliveryPeriodMin { get => deliveryPeriodMin; set => deliveryPeriodMin = value; }
        public String DeliveryPeriodMax { get => deliveryPeriodMax; set => deliveryPeriodMax = value; }
        public String DeliveryDateMin { get => deliveryDateMin; set => deliveryDateMin = value; }
        public String DeliveryDateMax { get => deliveryDateMax; set => deliveryDateMax = value; }
        public String TariffId { get => tariffId; set => tariffId = value; }
        public String CashOnDelivery { get => cashOnDelivery; set => cashOnDelivery = value; }
        public String PriceByCurrency { get => priceByCurrency; set => priceByCurrency = value; }
        public String Currency { get => currency; set => currency = value; }
        public String PercentVAT { get => percentVAT; set => percentVAT = value; }
        public List<Services> Services { get => services; set => services = value; }

        public AdressRowReceive()
        {
            Services = new List<Services>();
            Error = new List<CalculatorError>();
        }
    }
    //форма для ответа
    public class ResultOUT
    {
        private AdressRowReceive result;

        public AdressRowReceive Result { get => result; set => result = value; }
        public ResultOUT()
        {
            Result = new AdressRowReceive();
        }
    }
    //калькулятор
    public class CalculatorPriority
    {
        static String url;
        public static String Url { get => url; set => url = value; }
        public static AdressRowReceive Calculator(AdressRowDispatch _row)
        {
            Url = "http://api.cdek.ru/calculator/calculate_price_by_json.php";

            string json = JsonConvert.SerializeObject(_row, Formatting.Indented);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Создаём объект WebClient

            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = webClient.UploadString(new Uri(url), "POST", JsonConvert.SerializeObject(_row, Formatting.Indented));
                ResultOUT resultOUT = JsonConvert.DeserializeObject<ResultOUT>(response);
                if (resultOUT != null)
                    return resultOUT.Result;
            }
            return null;
        }
    }
    #endregion

    #region информация по заказам
    //заголовок документа
    public class InfoRequest
    {
        DateTime date;
        String account;
        String secure;

        public DateTime Date { get => date; set => date = value; }
        public string Account { get => account; set => account = value; }
        public string Secure { get => secure; set => secure = value; }
    }
    //дополнительная информация по изменению доставки
    public class ChangePeriod
    {
        DateTime dateBeg;
        DateTime dateEnd;

        public DateTime DateBeg { get => dateBeg; set => dateBeg = value; }
        public DateTime DateEnd { get => dateEnd; set => dateEnd = value; }
    }
    //город отправитель
    public class SenderCity
    {
        Int32 code;
        String postCode;
        String name;

        public int Code { get => code; set => code = value; }
        public string PostCode { get => postCode; set => postCode = value; }
        public string Name { get => name; set => name = value; }
    }
    //город получатель
    public class RecCity
    {
        Int32 code;
        String postCode;
        String name;

        public int Code { get => code; set => code = value; }
        public string PostCode { get => postCode; set => postCode = value; }
        public string Name { get => name; set => name = value; }
    }
    //Дополнительные услуги к заказам
    public class AddedService
    {
        Int32 serviceCode;
        Decimal sum;
        public int ServiceCode { get => serviceCode; set => serviceCode = value; }
        public decimal Sum { get => sum; set => sum = value; }
    }
    //Упаковка (все упаковки передаются в разных тэгах Package)
    public class Package
    {
        String number;
        String barCode;
        Int32 weight;
        Int32 volumeWeight;
        Int32 sizeA;
        Int32 sizeB;
        Int32 sizeC;

        public string Number { get => number; set => number = value; }
        public string BarCode { get => barCode; set => barCode = value; }
        public int Weight { get => weight; set => weight = value; }
        public int VolumeWeight { get => volumeWeight; set => volumeWeight = value; }
        public int SizeA { get => sizeA; set => sizeA = value; }
        public int SizeB { get => sizeB; set => sizeB = value; }
        public int SizeC { get => sizeC; set => sizeC = value; }
    }
    //Вложение (товар)
    public class Item
    {
        String wareKey;
        String marking;
        String comment;
        Int32 amount;
        Decimal delivAmount;
        Double cost;
        Decimal payment;
        String vATRate;
        Decimal vATSum;
        Int32 weight;

        public string WareKey { get => wareKey; set => wareKey = value; }
        public string Marking { get => marking; set => marking = value; }
        public string Comment { get => comment; set => comment = value; }
        public int Amount { get => amount; set => amount = value; }
        public decimal DelivAmount { get => delivAmount; set => delivAmount = value; }
        public double Cost { get => cost; set => cost = value; }
        public decimal Payment { get => payment; set => payment = value; }
        public string VATRate { get => vATRate; set => vATRate = value; }
        public decimal VATSum { get => vATSum; set => vATSum = value; }
        public int Weight { get => weight; set => weight = value; }
    }
    //ReciverOrder
    public class Order
    {

        String number;
        DateTime date;
        Int32 dispatchNumber;
        Int32 tariffTypeCode;
        Double weight;
        Double deliverySum;
        DateTime dateLastChange;
        Double cashOnDeliv;
        Double cashOnDelivFact;
        String cashOnDelivType;
        Int32 deliveryMode;
        String pvzCode;
        String deliveryVariant;
        //город отправитель
        SenderCity senderCity;
        //город получатель
        RecCity recCity;
        //Дополнительные услуги к заказам
        AddedService addedService;
        //Упаковка (все упаковки передаются в разных тэгах Package)
        Package package;
        //Вложение (товар)
        Item item;

        public string Number { get => number; set => number = value; }
        public DateTime Date { get => date; set => date = value; }
        public int DispatchNumber { get => dispatchNumber; set => dispatchNumber = value; }
        public int TariffTypeCode { get => tariffTypeCode; set => tariffTypeCode = value; }
        public double Weight { get => weight; set => weight = value; }
        public double DeliverySum { get => deliverySum; set => deliverySum = value; }
        public DateTime DateLastChange { get => dateLastChange; set => dateLastChange = value; }
        public double CashOnDeliv { get => cashOnDeliv; set => cashOnDeliv = value; }
        public double CashOnDelivFact { get => cashOnDelivFact; set => cashOnDelivFact = value; }
        public string CashOnDelivType { get => cashOnDelivType; set => cashOnDelivType = value; }
        public int DeliveryMode { get => deliveryMode; set => deliveryMode = value; }
        public string PvzCode { get => pvzCode; set => pvzCode = value; }
        public string DeliveryVariant { get => deliveryVariant; set => deliveryVariant = value; }
        public SenderCity SenderCity { get => senderCity; set => senderCity = value; }
        public RecCity RecCity { get => recCity; set => recCity = value; }
        public AddedService AddedService { get => addedService; set => addedService = value; }
        public Package Package { get => package; set => package = value; }
        public Item Item { get => item; set => item = value; }
        public Order()
        {
            SenderCity = new SenderCity();
            RecCity = new RecCity();
            AddedService = new AddedService();
            Package = new Package();
            Item = new Item();
        }
    }
    //форма отправки данных
    public class AdressRowReportOrder
    {
        InfoRequest infoRequest;
        ChangePeriod changePeriod;
        Order order;

        public InfoRequest InfoRequest { get => infoRequest; set => infoRequest = value; }
        public ChangePeriod ChangePeriod { get => changePeriod; set => changePeriod = value; }
        public Order Order { get => order; set => order = value; }
        public AdressRowReportOrder()
        {
            InfoRequest = new InfoRequest();
            ChangePeriod = new ChangePeriod();
            Order = new Order();
        }
    }
    //форма для получения данных 
    public class ResponseReportOrder
    {
        InfoRequest infoRequest;
        ChangePeriod changePeriod;
        Order order;
        public InfoRequest InfoRequest { get => infoRequest; set => infoRequest = value; }
        public ChangePeriod ChangePeriod { get => changePeriod; set => changePeriod = value; }
        public Order Order { get => order; set => order = value; }
        public ResponseReportOrder()
        {
            InfoRequest = new InfoRequest();
            ChangePeriod = new ChangePeriod();
            Order = new Order();
        }
    }

    //result
    public class ResponeResultOrder
    {
        ResponseReportOrder responseReportOrder;

        public ResponseReportOrder ResponseReportOrder { get => responseReportOrder; set => responseReportOrder = value; }
        public ResponeResultOrder()
        {
            ResponseReportOrder = new ResponseReportOrder();
        }
    }

    //отчет "информация по заказам"
    public class ReportOrder
    {

        static String url;
        public static String Url { get => url; set => url = value; }
        public static ResponseReportOrder Calculator(AdressRowReportOrder _row)
        {
            Url = "https://integration.edu.cdek.ru/info_report.php";

            string json = JsonConvert.SerializeObject(_row, Formatting.Indented);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Создаём объект WebClient

            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                string response = webClient.UploadString(new Uri(url), "POST", JsonConvert.SerializeObject(_row, Formatting.Indented));
                ResponeResultOrder resultOUT = JsonConvert.DeserializeObject<ResponeResultOrder>(response);
                if (resultOUT != null)
                    return resultOUT.ResponseReportOrder;
            }
            return null;
        }

    }
    #endregion

}
