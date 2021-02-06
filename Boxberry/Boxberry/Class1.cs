using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Boxberry
{    
    //класс отправителя 
    public class SitySender
    {    
        String code;//"00979",
        String name;//"Мирный",
        String cityCode; //"116",
        //String address; //"197227, Санкт-Петербург г, Комендантская пл, д.8, литер А, оф. 5Н",
        //String phone; //"+7(812)930-09-15 ",
        //String workShedule; //"пн-пт: 11.00-20.00",
        //String tripDescription; //"Метро  -  \"Комендантский проспект\".\nПримерное расстояние от метро до отделения - 400 метров.\nЖилой дом.\nВход через магазин \"Цветы\".\nЦокольный этаж.\n",
        //String deliveryPeriod; //1,
        //String cityName; //"Санкт-Петербург",
        //String tariffZone; //"2",
        //String settlement; //"Санкт-Петербург",
        //String area; //"Санкт-Петербург",
        //String country; //"РОССИЯ",
        //String gPS; //"60.005783,30.258888",
        //String addressReduce; //"Комендантская пл, д.8, литер А, оф. 5Н",
        //String acquiring; //"Yes",
        //String digitalSignature; //"No",
        //String typeOfOffice; //"СПВЗ",
        //String metro; //"Комендантский проспект",
        //String loadLimit; //"15",
        //String volumeLimit; //"0.48",
        //String countryCode; //"643",
        //String nalKD; //"No",
        //String onlyPrepaidOrders; //"No"}

        public string CityCode { get => cityCode; set => cityCode = value; }
        //public string Address { get => address; set => address = value; }
        //public string Phone { get => phone; set => phone = value; }
        //public string WorkShedule { get => workShedule; set => workShedule = value; }
        //public string TripDescription { get => tripDescription; set => tripDescription = value; }
        //public string DeliveryPeriod { get => deliveryPeriod; set => deliveryPeriod = value; }
        //public string CityName { get => cityName; set => cityName = value; }
        //public string TariffZone { get => tariffZone; set => tariffZone = value; }
        //public string Settlement { get => settlement; set => settlement = value; }
        //public string Area { get => area; set => area = value; }
        //public string Country { get => country; set => country = value; }
        //public string GPS { get => gPS; set => gPS = value; }
        //public string AddressReduce { get => addressReduce; set => addressReduce = value; }
        //public string Acquiring { get => acquiring; set => acquiring = value; }
        //public string DigitalSignature { get => digitalSignature; set => digitalSignature = value; }
        //public string TypeOfOffice { get => typeOfOffice; set => typeOfOffice = value; }
        //public string Metro { get => metro; set => metro = value; }
        //public string LoadLimit { get => loadLimit; set => loadLimit = value; }
        //public string VolumeLimit { get => volumeLimit; set => volumeLimit = value; }
        //public string CountryCode { get => countryCode; set => countryCode = value; }
        //public string NalKD { get => nalKD; set => nalKD = value; }
        //public string OnlyPrepaidOrders { get => onlyPrepaidOrders; set => onlyPrepaidOrders = value; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
    }

    public class SityRecipient
    {
        String code;//"00979",
        String name;//"Мирный",
        String cityCode; //"116",
        String address; //"197227, Санкт-Петербург г, Комендантская пл, д.8, литер А, оф. 5Н",
        String phone; //"+7(812)930-09-15 ",
        String workShedule; //"пн-пт: 11.00-20.00",
        String tripDescription; //"Метро  -  \"Комендантский проспект\".\nПримерное расстояние от метро до отделения - 400 метров.\nЖилой дом.\nВход через магазин \"Цветы\".\nЦокольный этаж.\n",
        String deliveryPeriod; //1,
        String cityName; //"Санкт-Петербург",
        String tariffZone; //"2",
        String settlement; //"Санкт-Петербург",
        String area; //"Санкт-Петербург",
        String country; //"РОССИЯ",
        String gPS; //"60.005783,30.258888",
        String addressReduce; //"Комендантская пл, д.8, литер А, оф. 5Н",
        String acquiring; //"Yes",
        String digitalSignature; //"No",
        String typeOfOffice; //"СПВЗ",
        String metro; //"Комендантский проспект",
        String loadLimit; //"15",
        String volumeLimit; //"0.48",
        String countryCode; //"643",
        String nalKD; //"No",
        String onlyPrepaidOrders; //"No"}

        public string CityCode { get => cityCode; set => cityCode = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string WorkShedule { get => workShedule; set => workShedule = value; }
        public string TripDescription { get => tripDescription; set => tripDescription = value; }
        public string DeliveryPeriod { get => deliveryPeriod; set => deliveryPeriod = value; }
        public string CityName { get => cityName; set => cityName = value; }
        public string TariffZone { get => tariffZone; set => tariffZone = value; }
        public string Settlement { get => settlement; set => settlement = value; }
        public string Area { get => area; set => area = value; }
        public string Country { get => country; set => country = value; }
        public string GPS { get => gPS; set => gPS = value; }
        public string AddressReduce { get => addressReduce; set => addressReduce = value; }
        public string Acquiring { get => acquiring; set => acquiring = value; }
        public string DigitalSignature { get => digitalSignature; set => digitalSignature = value; }
        public string TypeOfOffice { get => typeOfOffice; set => typeOfOffice = value; }
        public string Metro { get => metro; set => metro = value; }
        public string LoadLimit { get => loadLimit; set => loadLimit = value; }
        public string VolumeLimit { get => volumeLimit; set => volumeLimit = value; }
        public string CountryCode { get => countryCode; set => countryCode = value; }
        public string NalKD { get => nalKD; set => nalKD = value; }
        public string OnlyPrepaidOrders { get => onlyPrepaidOrders; set => onlyPrepaidOrders = value; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
    }
    //Город отправитель
    public class BoxberrySitySender
    {
        public static Dictionary<String, SitySender> codeSitySender = new Dictionary<String, SitySender>();
        static string urlSender;

        public static string UrlSender { get => urlSender; set => urlSender = value; }

        //  -- Получить список пунктов приема посылок.
        public static Boolean? SitySenderSelect()
        {
            UrlSender = "https://api.boxberry.ru/json.php?token=d6f33e419c16131e5325cbd84d5d6000&method=PointsForParcels";   //  -- Получить список пунктов приема посылок.

            ServicePointManager.Expect100Continue = true;  
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;  //установили защищенный канал
            using (var webClient = new WebClient())//создаем связь
            {
                webClient.Encoding = Encoding.UTF8; //установили кадировку UTF8
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var responseSender = webClient.DownloadString(urlSender);
                if (responseSender != null)
                {
                    String[] text = responseSender.Split('[', '{', '}', ']');

                    //Создаем шаблон для слова, которое начинается с буквы "M"

                    string pattern = @"[A-Za-zА-Яа-я0-9_\-\ ().]*";
                    // Создаем экземпляр Regex  
                    Regex rg = new Regex(pattern);
                    // Получаем все совпадения  
                    foreach (String rowStr in text)
                    {
                        List<String> rowOUT = new List<String>();
                        MatchCollection matchedAuthors = rg.Matches(rowStr);
                        if (matchedAuthors.Count > 1)
                        {
                            for (int count = 0; count < matchedAuthors.Count; count++)
                                if (!String.IsNullOrEmpty(matchedAuthors[count].Value) && matchedAuthors[count].Value != ":" && matchedAuthors[count].Value != "," && matchedAuthors[count].Value != "." && matchedAuthors[count].Value != "")
                                    rowOUT.Add(matchedAuthors[count].Value);

                            if (rowOUT.Count > 0)
                            {
                                String Code = "";
                                SitySender sitySender = new SitySender();
                                for (int i = 0; i < rowOUT.Count; i++)
                                {

                                    if (rowOUT[i] == "Code")
                                    {
                                        Code = rowOUT[i + 1];
                                        sitySender.Code = rowOUT[i + 1];
                                    }
                                    if (rowOUT[i] == "Name")
                                    {
                                        sitySender.Name = rowOUT[i + 1];
                                    }
                                    if (rowOUT[i] == "CityCode")
                                    {
                                        sitySender.CityCode = rowOUT[i + 1];
                                    }                                 
                                }
                                if (Code != "")
                                {
                                    codeSitySender.Add(Code, sitySender);
                                }
                            }
                        }
                    }
                    if (codeSitySender.Count > 1)
                    {
                        return true;
                    }
                    else return null;
                }
            }
            return null;
        }
    }
    //Город получатель
    public class BoxberrySityRecipient
    {
        public static Dictionary<String, SityRecipient> codeSityRecipient = new Dictionary<String, SityRecipient>();
        static string urlSender;
        public static string UrlSender { get => urlSender; set => urlSender = value; }
        public static Boolean? SityRecipientSelect()
        {
            UrlSender = "https://api.boxberry.ru/json.php?token=d6f33e419c16131e5325cbd84d5d6000&method=ListPoints&prepaid=1";   //  -- Получить информацию о всех точках выдачи заказов.
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;  //установили защищенный канал
            using (var webClient = new WebClient())//создаем связь
            {
                webClient.Encoding = Encoding.UTF8; //установили кадировку UTF8
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var responseSender = webClient.DownloadString(urlSender);
                if (responseSender != null)
                {
                    String[] text = responseSender.Split('[', '{', '}', ']');

                    //Создаем шаблон для слова, которое начинается с буквы "M"

                    string pattern = @"[A-Za-zА-Яа-я0-9_\-\,\ ().:]*";
                    // Создаем экземпляр Regex  
                    Regex rg = new Regex(pattern);
                    // Получаем все совпадения  
                    foreach (String rowStr in text)
                    {
                        List<String> rowOUT = new List<String>();
                        MatchCollection matchedAuthors = rg.Matches(rowStr);
                        if (matchedAuthors.Count > 1)
                        {
                            for (int count = 0; count < matchedAuthors.Count; count++)
                                if (!String.IsNullOrEmpty(matchedAuthors[count].Value) && matchedAuthors[count].Value != ":" && matchedAuthors[count].Value != "," && matchedAuthors[count].Value != "." && matchedAuthors[count].Value != "")
                                    rowOUT.Add(matchedAuthors[count].Value);

                            if (rowOUT.Count > 0)
                            {
                                String Code = "";
                                SityRecipient sityRecipient = new SityRecipient();
                                for (int i = 0; i < rowOUT.Count; i++)
                                {

                                    if (rowOUT[i] == "Code")
                                    {
                                        Code = rowOUT[i + 1];
                                        sityRecipient.Code = rowOUT[i + 1];
                                    }
                                    if (rowOUT[i] == "Name")
                                    {
                                        sityRecipient.Name = rowOUT[i + 1];
                                    }
                                    if (rowOUT[i] == "CityCode")
                                    {
                                        sityRecipient.CityCode = rowOUT[i + 1];
                                    }
                                    if (rowOUT[i] == "Address")
                                    {
                                        sityRecipient.Address = rowOUT[i + 1];
                                    }
                                    if (rowOUT[i] == "GPS")
                                    {
                                        sityRecipient.GPS = rowOUT[i + 1];
                                    }
                                    //WorkShedule
                                    if (rowOUT[i] == "WorkShedule")
                                    {
                                        sityRecipient.WorkShedule = rowOUT[i + 1];
                                    }
                                    //DeliveryPeriod
                                    if (rowOUT[i] == "DeliveryPeriod")
                                    {
                                        sityRecipient.DeliveryPeriod = rowOUT[i + 1];
                                    }
                                    //CityName
                                    if (rowOUT[i] == "CityName")
                                    {
                                        sityRecipient.CityName = rowOUT[i + 1];
                                    }
                                    //TariffZone
                                    if (rowOUT[i] == "TariffZone")
                                    {
                                        sityRecipient.TariffZone = rowOUT[i + 1];
                                    }
                                    //Area
                                    if (rowOUT[i] == "Area")
                                    {
                                        sityRecipient.Area = rowOUT[i + 1];
                                    }
                                    //Country
                                    if (rowOUT[i] == "Country")
                                    {
                                        sityRecipient.Country = rowOUT[i + 1];
                                    }
                                    //AddressReduce
                                    if (rowOUT[i] == "AddressReduce")
                                    {
                                        sityRecipient.AddressReduce = rowOUT[i + 1];
                                    }
                                    //Acquiring
                                    if (rowOUT[i] == "Acquiring")
                                    {
                                        sityRecipient.Acquiring = rowOUT[i + 1];
                                    }
                                    //DigitalSignature
                                    if (rowOUT[i] == "DigitalSignature")
                                    {
                                        sityRecipient.DigitalSignature = rowOUT[i + 1];
                                    }
                                    //TypeOfOffice
                                    if (rowOUT[i] == "TypeOfOffice")
                                    {
                                        sityRecipient.TypeOfOffice = rowOUT[i + 1];
                                    }
                                    //Metro
                                    if (rowOUT[i] == "Metro")
                                    {
                                        sityRecipient.Metro = rowOUT[i + 1];
                                    }
                                    //LoadLimit
                                    if (rowOUT[i] == "LoadLimit")
                                    {
                                        sityRecipient.LoadLimit = rowOUT[i + 1];
                                    }
                                    //VolumeLimit
                                    if (rowOUT[i] == "VolumeLimit")
                                    {
                                        sityRecipient.VolumeLimit = rowOUT[i + 1];
                                    }
                                    //CountryCode
                                    if (rowOUT[i] == "CountryCode")
                                    {
                                        sityRecipient.CountryCode = rowOUT[i + 1];
                                    }
                                    //NalKD
                                    if (rowOUT[i] == "NalKD")
                                    {
                                        sityRecipient.NalKD = rowOUT[i + 1];
                                    }
                                    //OnlyPrepaidOrders
                                    if (rowOUT[i] == "OnlyPrepaidOrders")
                                    {
                                        sityRecipient.OnlyPrepaidOrders = rowOUT[i + 1];
                                    }
                                    // Phone
                                    if (rowOUT[i] == "Phone")
                                    {
                                        sityRecipient.Phone = rowOUT[i + 1];
                                    }
                                }
                                if (Code != "")
                                {
                                    codeSityRecipient.Add(Code, sityRecipient);
                                }
                            }
                        }
                    }
                    if (codeSityRecipient.Count > 1)
                    {
                        return true;
                    }
                    else return null;
                }
            }
            return null;
        }
    }

    // калькулятор Склад-склад
    public class CalculatorBoxberry
    {
        static Decimal priceTotal;
        static Decimal priceBase;
        static Decimal priceService;
        static Int32 deliveryPeriod;
        static String url;
        public static decimal PriceTotal { get => priceTotal; set => priceTotal = value; }
        public static decimal PriceBase { get => priceBase; set => priceBase = value; }
        public static decimal PriceService { get => priceService; set => priceService = value; }
        public static Int32 DeliveryPeriod { get => deliveryPeriod; set => deliveryPeriod = value; }      
        public static  String Url { get => url; set => url = value; }
        public static Boolean? Calculator(  Double _weight,
                                            String _targetstart, 
                                            Int32 _target ,
                                            Int32 _ordersum ,
                                            Decimal _deliverysum, 
                                            Decimal _paysum,
                                            Double _height,
                                            Double _width,
                                            Double _depth
                                            //String _zip
            )
        {
            Url= "https://api.boxberry.ru/json.php?token=d6f33e419c16131e5325cbd84d5d6000&method=DeliveryCosts&weight=" + _weight + "targetstart=" + _targetstart + "& target=" + _target + "& ordersum=" + _ordersum + "" +
                "& deliverysum=" + _deliverysum + "& height=" + _height + " & width=" + _width + "& depth=" + _depth + " & paysum=" + _paysum;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Создаём объект WebClient

            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
               if (!String.IsNullOrEmpty(response.ToString()))
                {
                    String[] str = response.Split(',');
                    foreach(String row in str)
                    {
                        string name = row.Split(':')[0].Split('"')[1];
                        switch (name) {
                            case "price":
                                PriceTotal =  Convert.ToDecimal(DropSTR(row.Split(':')[1]));
                                break;
                            case "price_base":
                                PriceBase = Convert.ToDecimal(DropSTR(row.Split(':')[1]));
                                break;
                            case "price_service":
                                PriceService = Convert.ToDecimal(DropSTR(row.Split(':')[1]));
                                break;
                            case "delivery_period":
                                DeliveryPeriod = Convert.ToInt32(DropSTR(row.Split(':')[1].Split('"')[1]));
                                break;
                        }                     
                    }
                    return true;
                }             
            }
            return null;
        }

        private static String DropSTR(String _str)
        {
            string[] str = _str.Split('.');
            switch (str.Length)
            {
                case 1:
                    return str[0];
                    break;
                case 2:
                    return str[0] + "," + str[1];
                    break;
                default:
                    return "0";
                    break;
            }            
        }

    }

    // калькулятор Склад-КД
    public class CalculatorKDBoxberry
    {
        static Decimal priceTotal;
        static Decimal priceBase;
        static Decimal priceService;
        static Int32 deliveryPeriod;
        static String url;
        public static decimal PriceTotal { get => priceTotal; set => priceTotal = value; }
        public static decimal PriceBase { get => priceBase; set => priceBase = value; }
        public static decimal PriceService { get => priceService; set => priceService = value; }
        public static Int32 DeliveryPeriod { get => deliveryPeriod; set => deliveryPeriod = value; }
        public static String Url { get => url; set => url = value; }
        public static Boolean? Calculator(Double _weight,
                                            String _targetstart,                                           
                                            Int32 _ordersum,
                                            Decimal _deliverysum,
                                            Decimal _paysum,
                                            Double _height,
                                            Double _width,
                                            Double _depth,
                                            String _zip
            )
        {
            Url = "https://api.boxberry.ru/json.php?token=d6f33e419c16131e5325cbd84d5d6000&method=DeliveryCosts&weight=" + _weight + "targetstart=" + _targetstart + "& ordersum=" + _ordersum + "" +
                "& deliverysum=" + _deliverysum + " & paysum=" + _paysum + "& height=" + _height + " & width=" + _width + "& depth=" + _depth + " & zip=" + _zip;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Создаём объект WebClient

            using (var webClient = new WebClient())
            {
                 webClient.Encoding = Encoding.UTF8; //установили кадировку UTF8
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                if (!String.IsNullOrEmpty(response.ToString()))
                {
                    String[] str = response.Split(',');
                    foreach (String row in str)
                    {
                        string name = row.Split(':')[0].Split('"')[1];
                        switch (name)
                        {
                            case "price":
                                PriceTotal = Convert.ToDecimal(DropSTR(row.Split(':')[1]));
                                break;
                            case "price_base":
                                PriceBase = Convert.ToDecimal(DropSTR(row.Split(':')[1]));
                                break;
                            case "price_service":
                                PriceService = Convert.ToDecimal(DropSTR(row.Split(':')[1]));
                                break;
                            case "delivery_period":
                                DeliveryPeriod = Convert.ToInt32(DropSTR(row.Split(':')[1].Split('"')[1]));
                                break;
                        }
                    }
                    return true;
                }
            }
            return null;
        }

        private static String DropSTR(String _str)
        {
            string[] str = _str.Split('.');
            switch (str.Length)
            {
                case 1:
                    return str[0];
                    break;
                case 2:
                    return str[0] + "," + str[1];
                    break;
                default:
                    return "0";
                    break;
            }
        }

    }

    //Стиатус заказа
    public class OrderStatus
    {
        //static String token;     
        //static String lmld;
        static String url;
        static String str;

        //public static string Token { get => token; set => token = value; }
        //public static string Lmld { get => lmld; set => lmld = value; }
        public static string Url { get => url; set => url = value; }
        public static string Str { get => str; set => str = value; }

        public static Boolean? OrderStatusSelect(String _token, String _lmld)
        {
            Url = "https://api.boxberry.ru/json.php?method=ListStatuses&token=" + _token + "&ImId=" + _lmld;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Создаём объект WebClient

            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8; //установили кадировку UTF8
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                if (!String.IsNullOrEmpty(response.ToString()))
                {
                    Str = response.ToString();
                    return true;
                }
            }
            return null;
        }
    }

    public class OrderStatusLinke
    {
        //static String token;     
        //static String lmld;
        static String url;
        static String str;

        //public static string Token { get => token; set => token = value; }
        //public static string Lmld { get => lmld; set => lmld = value; }
        public static string Url { get => url; set => url = value; }
        public static string Str { get => str; set => str = value; }

        public static Boolean? OrderStatusSelect(String _numberorder)
        {
            Url = "https://boxberry.ru/departure_track/?id=" + _numberorder;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Создаём объект WebClient

            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8; //установили кадировку UTF8
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                if (!String.IsNullOrEmpty(response.ToString()))
                {
                    Str = response.ToString();
                    return true;
                }
            }
            return null;
        }
    }
}
