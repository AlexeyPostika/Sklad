using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using CDEK;
using System.Resources;

namespace CDEK.Helper
{
    //список ошибок калькулятора
    public class CalculatorError
    {
        public Int32 ID { set; get; }
        public string Description { set; get; }
    }
    //список ошибок неудачного прозвона на сервер сдек Ringing
    public class RingingError
    {
        public Int32 ID { set; get; }
        public string Description { set; get; }
    }

    //Причины задержки доставки -- Reasons for delivery delays
    public class ReasonsForDeliveryDelaysError
    {
        public Int32 ID { set; get; }
        public string Description { set; get; }
    }

    //Ставки НДС -- VAT
    public class VAT
    {
        public Int32 ID { set; get; }
        public string Description { set; get; }
    }

    //Код валюты -- Currency
    public class Currency
    {
        public String ID { set; get; }
        public string Description { set; get; }
    }

    //Дополнительные услуги -- Services
    public class Services
    {
        public int ID { set; get; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Rate { set; get; }
    }

    //Дополнительные услуги -- Services
    public class Good
    {
        public Double Weight { set; get; }
        public Double Length { get; set; }
        public Double Width { get; set; }
        public Double Height { set; get; }
    }
    //Режимы доставки -- Delivery modes
    public class DeliveryModes
    {
        public int ID { set; get; }
        public string Name { get; set; }
        public string Description { set; get; }
    }

    //Тарифы для обычной доставки -- Rates for regular delivery
    public class RatesForRegularDelivery
    {
        public int ID { set; get; }
        public string NameRate { get; set; }
        public string DeliveryMode { get; set; }
        public string Limitation { get; set; }
        public string Service { get; set; }
        public string Description { set; get; }
    }

    // Китайский экспресс -- Chinese express
    public class ChineseExpress
    {
        public int ID { set; get; }
        public string NameRate { get; set; }
        public string DeliveryMode { get; set; }
        public string Limitation { get; set; }
        public string Service { get; set; }
        public string Description { set; get; }
    }

    // Услуги (тарифы) и режимы доставки СДЭК -- Services (tariffs) and delivery modes of CDEK
    public class ServicesTariffsCDEK
    {
        public int ID { set; get; }
        public string NameRate { get; set; }
        public string DeliveryMode { get; set; }
        public string Limitation { get; set; }
        public string Service { get; set; }
        public string Description { set; get; }
        public Boolean Status { set; get; }
    }

    public class CalculatorErrorList
    {
        public ObservableCollection<CalculatorError> innerList { get; set; }
        public CalculatorErrorList()
        {
            innerList = new ObservableCollection<CalculatorError>()
            {
                  new CalculatorError { ID=0, Description=Resources.InternalServerError },
                  new CalculatorError { ID=1, Description=Resources.APIError },
                  new CalculatorError { ID=2, Description=Resources.AuthorisationError },
                  new CalculatorError { ID=3, Description=Resources.ItIsImpossibleToCarryOutDelivery },
                  new CalculatorError { ID=4, Description=Resources.ErrorWhileSpecifyingLocationParameters },
                  new CalculatorError { ID=5, Description=Resources.NoDepartureLocation },
                  new CalculatorError { ID=6, Description=Resources.TariffOrTariffList },
                  new CalculatorError { ID=7, Description=Resources.SenderCityNot },
                  new CalculatorError { ID=8, Description=Resources.DestinationCityNot },
                  new CalculatorError { ID=9, Description=Resources.WhenAuthorizingError },
                  new CalculatorError { ID=10, Description=Resources.DeliveryModeSettingError },
                  new CalculatorError { ID=11, Description=Resources.DataFormatSetIncorrectly },
                  new CalculatorError { ID=12, Description=Resources.DataDecodingError },
                  new CalculatorError { ID=13, Description=Resources.ThePpostalCode },
                  new CalculatorError { ID=14, Description=Resources.ItIsImpossiblePostalCodeError },
                  new CalculatorError { ID=15, Description=Resources.ThePostalCodeOfTheRecipientCity },
                  new CalculatorError { ID=16, Description=Resources.ItIsImpossibleToUniquelyIdentifyTheReceivingCity }
            };
        }
    }
    public class RingingErrorList
    {
        public ObservableCollection<RingingError> innerList { get; set; }
        public RingingErrorList()
        {
            innerList = new ObservableCollection<RingingError>()
            {
                  new RingingError { ID=1, Description=Resources.PhoneIsBusy },
                  new RingingError { ID=2, Description=Resources.TheSubscriberDoesNotPickUpThePhone },
                  new RingingError { ID=3, Description=Resources.SubscriberUnavailable },
                  new RingingError { ID=4, Description=Resources.WrongNumber },
                  new RingingError { ID=5, Description=Resources.PhoneNotSpecified },
                  new RingingError { ID=6, Description=Resources.Silence },
                  new RingingError { ID=7, Description=Resources.Reset },
                  new RingingError { ID=8, Description=Resources.IHungUp }
            };
        }
    }

    public class ReasonsForDeliveryDelaysErrorList
    {
        public ObservableCollection<ReasonsForDeliveryDelaysError> innerList { get; set; }
        public ReasonsForDeliveryDelaysErrorList()
        {
            innerList = new ObservableCollection<ReasonsForDeliveryDelaysError>()
            {
                  new ReasonsForDeliveryDelaysError { ID=1, Description=Resources.String1 },
                  new ReasonsForDeliveryDelaysError { ID=2, Description=Resources.String2 },
                  new ReasonsForDeliveryDelaysError { ID=3, Description=Resources.String3 },
                  new ReasonsForDeliveryDelaysError { ID=4, Description=Resources.String4 },
                  new ReasonsForDeliveryDelaysError { ID=5, Description=Resources.String5 },
                  new ReasonsForDeliveryDelaysError { ID=6, Description=Resources.String6 },
                  new ReasonsForDeliveryDelaysError { ID=7, Description=Resources.String7 },
                  new ReasonsForDeliveryDelaysError { ID=8, Description=Resources.String8 },
                  new ReasonsForDeliveryDelaysError { ID=9, Description=Resources.String9 },
                  new ReasonsForDeliveryDelaysError { ID=10, Description=Resources.String10 },
                  new ReasonsForDeliveryDelaysError { ID=11, Description=Resources.String11 },
                  new ReasonsForDeliveryDelaysError { ID=12, Description=Resources.String12 },
                  new ReasonsForDeliveryDelaysError { ID=13, Description=Resources.String13 },
                  new ReasonsForDeliveryDelaysError { ID=14, Description=Resources.String14 },
                  new ReasonsForDeliveryDelaysError { ID=15, Description=Resources.String15 },
                  new ReasonsForDeliveryDelaysError { ID=16, Description=Resources.String16 },
                  new ReasonsForDeliveryDelaysError { ID=17, Description=Resources.String17 },
                  new ReasonsForDeliveryDelaysError { ID=18, Description=Resources.String18 },
                  new ReasonsForDeliveryDelaysError { ID=19, Description=Resources.String19 },
                  new ReasonsForDeliveryDelaysError { ID=20, Description=Resources.String20 },
                  new ReasonsForDeliveryDelaysError { ID=21, Description=Resources.String21 },
                  new ReasonsForDeliveryDelaysError { ID=22, Description=Resources.String22 },
                  new ReasonsForDeliveryDelaysError { ID=23, Description=Resources.String23 },
                  new ReasonsForDeliveryDelaysError { ID=24, Description=Resources.String24 },
                  new ReasonsForDeliveryDelaysError { ID=25, Description=Resources.String25 },
                  new ReasonsForDeliveryDelaysError { ID=26, Description=Resources.String26 },
                  new ReasonsForDeliveryDelaysError { ID=27, Description=Resources.String27 },
                  new ReasonsForDeliveryDelaysError { ID=28, Description=Resources.String28 },
                  new ReasonsForDeliveryDelaysError { ID=29, Description=Resources.String29 },
                  new ReasonsForDeliveryDelaysError { ID=30, Description=Resources.String30 },
                  new ReasonsForDeliveryDelaysError { ID=31, Description=Resources.String31 },
                  new ReasonsForDeliveryDelaysError { ID=32, Description=Resources.String32 },
                  new ReasonsForDeliveryDelaysError { ID=33, Description=Resources.String33 },
                  new ReasonsForDeliveryDelaysError { ID=34, Description=Resources.String34 },
                  new ReasonsForDeliveryDelaysError { ID=35, Description=Resources.String35 },
                  new ReasonsForDeliveryDelaysError { ID=36, Description=Resources.String36 },
                  new ReasonsForDeliveryDelaysError { ID=37, Description=Resources.String37 },
                  new ReasonsForDeliveryDelaysError { ID=38, Description=Resources.String38 },
                  new ReasonsForDeliveryDelaysError { ID=39, Description=Resources.String39 },
                  new ReasonsForDeliveryDelaysError { ID=40, Description=Resources.String40 },
                  new ReasonsForDeliveryDelaysError { ID=41, Description=Resources.String41 },
                  new ReasonsForDeliveryDelaysError { ID=42, Description=Resources.String42 },
                  new ReasonsForDeliveryDelaysError { ID=43, Description=Resources.String43 },
                  new ReasonsForDeliveryDelaysError { ID=44, Description=Resources.String44 },
                  new ReasonsForDeliveryDelaysError { ID=45, Description=Resources.String45 },
                  new ReasonsForDeliveryDelaysError { ID=46, Description=Resources.String46 },
                  new ReasonsForDeliveryDelaysError { ID=47, Description=Resources.String47 },
                  new ReasonsForDeliveryDelaysError { ID=48, Description=Resources.String48 },
                  new ReasonsForDeliveryDelaysError { ID=49, Description=Resources.String49 },
                  new ReasonsForDeliveryDelaysError { ID=52, Description=Resources.String52 },
                  new ReasonsForDeliveryDelaysError { ID=53, Description=Resources.String53 },
                  new ReasonsForDeliveryDelaysError { ID=54, Description=Resources.String54 },
                  new ReasonsForDeliveryDelaysError { ID=55, Description=Resources.String55 },
                  new ReasonsForDeliveryDelaysError { ID=56, Description=Resources.String56 },
                  new ReasonsForDeliveryDelaysError { ID=57, Description=Resources.String57 }
            };
        }
    }

    public class VATList
    {
        public ObservableCollection<VAT> innerList { get; set; }
        public VATList()
        {
            innerList = new ObservableCollection<VAT>()
            {
                  new VAT { ID=1, Description=Resources.VATX },
                  new VAT { ID=2, Description=Resources.VAT0 },
                  new VAT { ID=3, Description=Resources.VAT10 },
                  new VAT { ID=4, Description=Resources.VAT20 },
            };
        }
    }

    //Currency
    public class CurrencyList
    {
        public ObservableCollection<Currency> innerList { get; set; }
        public CurrencyList()
        {
            innerList = new ObservableCollection<Currency>()
            {
                  new Currency { ID="RUB", Description=Resources.RUB },
                  new Currency { ID="USD", Description=Resources.USD },
                  new Currency { ID="EUR", Description=Resources.EUR },
                  new Currency { ID="KZT", Description=Resources.KZT },
                  new Currency { ID="GBP", Description=Resources.GBP },
                  new Currency { ID="CNY", Description=Resources.CNY },
                  new Currency { ID="BYN", Description=Resources.BYN },
                  new Currency { ID="UAH", Description=Resources.UAH },
                  new Currency { ID="AMD", Description=Resources.AMD },
                  new Currency { ID="KGS", Description=Resources.KGS },
                  new Currency { ID="TL", Description=Resources.TL },
                  new Currency { ID="THB", Description=Resources.THB },
                  new Currency { ID="KRW", Description=Resources.KRW },
                  new Currency { ID="AED", Description=Resources.AED },
                  new Currency { ID="UZS", Description=Resources.UZS },
                  new Currency { ID="MNT", Description=Resources.MNT }
            };
        }
    }

    //Services
    public class ServicesList
    {
        public ObservableCollection<Services> innerList { get; set; }
        public ServicesList()
        {
            innerList = new ObservableCollection<Services>()
            {
                  new Services { ID=2, Title =Resources.String54, Price=Resources.String84, Rate=Resources.String115 },
                  new Services { ID=3, Title =Resources.String55, Price=Resources.String85, Rate=Resources.String116 },
                  new Services { ID=5, Title =Resources.String57, Price=Resources.String87, Rate=Resources.String117 },
                  new Services { ID=6, Title =Resources.String58, Price=Resources.String88, Rate=Resources.String118 },
                  new Services { ID=7, Title =Resources.String59, Price=Resources.String89, Rate=Resources.String119 },
                  new Services { ID=8, Title =Resources.String60, Price=Resources.String90, Rate=Resources.String120 },
                  new Services { ID=9, Title =Resources.String61, Price=Resources.String91, Rate=Resources.String120 },
                  new Services { ID=10, Title =Resources.String62, Price=Resources.String92, Rate=Resources.String121 },
                  new Services { ID=13, Title =Resources.String63, Price=Resources.String93, Rate=Resources.String122 },
                  new Services { ID=14, Title =Resources.String64, Price=Resources.String94, Rate=Resources.String123 },
                  new Services { ID=15, Title =Resources.String65, Price=Resources.String95, Rate=Resources.String124 },
                  new Services { ID=16, Title =Resources.String66, Price=Resources.String96, Rate=Resources.String125 },
                  new Services { ID=17, Title =Resources.String67, Price=Resources.String97, Rate=Resources.String126 },
                  new Services { ID=20, Title =Resources.String68, Price=Resources.String98, Rate=Resources.String127 },
                  new Services { ID=23, Title =Resources.String69, Price=Resources.String99, Rate=Resources.String128 },
                  new Services { ID=24, Title =Resources.String70, Price=Resources.String100, Rate=Resources.String129 },
                  new Services { ID=25, Title =Resources.String71, Price=Resources.String101, Rate=Resources.String130 },
                  new Services { ID=26, Title =Resources.String72, Price=Resources.String102, Rate=Resources.String131 },
                  new Services { ID=27, Title =Resources.String73, Price=Resources.String103, Rate=Resources.String132 },
                  new Services { ID=30, Title =Resources.String74, Price=Resources.String104, Rate=Resources.String133 },
                  new Services { ID=32, Title =Resources.String75, Price=Resources.String105, Rate=Resources.String134 },
                  new Services { ID=33, Title =Resources.String76, Price=Resources.String106, Rate=Resources.String135 },
                  new Services { ID=34, Title =Resources.String77, Price=Resources.String107, Rate=Resources.String135 },
                  new Services { ID=35, Title =Resources.String78, Price=Resources.String108, Rate=Resources.String136 },
                  new Services { ID=36, Title =Resources.String79, Price=Resources.String109, Rate=Resources.String137 },
                  new Services { ID=37, Title =Resources.String80, Price=Resources.String110, Rate=Resources.String142 },
                  new Services { ID=40, Title =Resources.String81, Price=Resources.String111, Rate=Resources.String138 },
                  new Services { ID=41, Title =Resources.String82, Price=Resources.String112, Rate=Resources.String139 },
                  new Services { ID=42, Title =Resources.String83, Price=Resources.String113, Rate=Resources.String140 },
                  new Services { ID=48, Title =Resources.String84, Price=Resources.String114, Rate=Resources.String141 }
            };
        }
    }

    //DeliveryModes
    public class DeliveryModesList
    {
        public ObservableCollection<DeliveryModes> innerList { get; set; }
        public DeliveryModesList()
        {
            innerList = new ObservableCollection<DeliveryModes>()
            {
                  new DeliveryModes { ID=1, Name=Resources.DoorDoor, Description=Resources.DoorPostamatDecription },
                  new DeliveryModes { ID=2, Name=Resources.DoorWarehouse, Description=Resources.DoorWarehouseDescription },
                  new DeliveryModes { ID=3, Name=Resources.WarehouseDoor, Description=Resources.WarehouseDoorDescription },
                  new DeliveryModes { ID=4, Name=Resources.WarehouseWarehouse, Description=Resources.WarehouseWarehouseDescription },
                  new DeliveryModes { ID=6, Name=Resources.DoorPostamat, Description=Resources.DoorPostamatDecription },
                  new DeliveryModes { ID=7, Name=Resources.WarehousePostomat, Description=Resources.WarehousePostomatDescription },
            };
        }
    }

    //RatesForRegularDelivery
    public class RatesForRegularDeliveryList
    {
        public ObservableCollection<RatesForRegularDelivery> innerList { get; set; }
        public RatesForRegularDeliveryList()
        {
            innerList = new ObservableCollection<RatesForRegularDelivery>()
            {
                new RatesForRegularDelivery { ID=1, NameRate=Resources.ExpressLightDoorDoor, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightDescription },
                new RatesForRegularDelivery { ID=361, NameRate=Resources.ExpressLightDoorPostamat, DeliveryMode=Resources.DoorPostamat, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightDescription },
                new RatesForRegularDelivery { ID=363, NameRate=Resources.ExpressLightWarehousePostamat, DeliveryMode=Resources.WarehousePostomat, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightDescription },
                new RatesForRegularDelivery { ID=3, NameRate=Resources.ExpressВeliveryTo18, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.ExpressВeliveryDescription },
                new RatesForRegularDelivery { ID=5, NameRate=Resources.EconomyShippingWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Service=Resources.EconomyShipping, Description=Resources.EconomyShippingDescription },
                new RatesForRegularDelivery { ID=10, NameRate=Resources.ExpressLightInternationalWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightInternationalDescription },
                new RatesForRegularDelivery { ID=11, NameRate=Resources.ExpressLightInternationalWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightInternationalDescription },
                new RatesForRegularDelivery { ID=12, NameRate=Resources.ExpressLightInternationalDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightInternationalDescription},
                new RatesForRegularDelivery { ID=15, NameRate=Resources.ExpressSheavyweightsWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription },
                new RatesForRegularDelivery { ID=16, NameRate=Resources.ExpressSheavyweightsWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription },
                new RatesForRegularDelivery { ID=17, NameRate=Resources.ExpressSheavyweightsDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription },
                new RatesForRegularDelivery { ID=18, NameRate=Resources.ExpressSheavyweightsDoorDoor, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription },
                new RatesForRegularDelivery { ID=57, NameRate=Resources.SuperExpressTo9, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription },
                new RatesForRegularDelivery { ID=58, NameRate=Resources.SuperExpressTo10, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription },
                new RatesForRegularDelivery { ID=59, NameRate=Resources.SuperExpressTo12, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription },
                new RatesForRegularDelivery { ID=60, NameRate=Resources.SuperExpressTo14, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription },
                new RatesForRegularDelivery { ID=61, NameRate=Resources.SuperExpressTo16, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription },
                new RatesForRegularDelivery { ID=62, NameRate=Resources.MainExpressWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Service=Resources.EconomyShipping, Description=Resources.MainExpressDescription },
                new RatesForRegularDelivery { ID=63, NameRate=Resources.MainSuperExpressWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Service=Resources.EconomyShipping, Description=Resources.MainSuperExpressDescription },
                new RatesForRegularDelivery { ID=118, NameRate=Resources.EconomyExpressDoorDoor, DeliveryMode=Resources.DoorDoor, Service=Resources.EconomyShipping, Description=Resources.EconomyExpressDescription },
                new RatesForRegularDelivery { ID=119, NameRate=Resources.EconomyExpressWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Service=Resources.EconomyShipping, Description=Resources.EconomyExpressDescription },
                new RatesForRegularDelivery { ID=120, NameRate=Resources.EconomyExpressDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Service=Resources.EconomyShipping, Description=Resources.EconomyExpressDescription },
                new RatesForRegularDelivery { ID=121, NameRate=Resources.MainExpressDoorDoor, DeliveryMode=Resources.DoorDoor, Service=Resources.EconomyShipping, Description=Resources.MainExpressDescription1 },
                new RatesForRegularDelivery { ID=122, NameRate=Resources.MainExpressWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Service=Resources.EconomyShipping, Description=Resources.MainExpressDescription1 },
                new RatesForRegularDelivery { ID=123, NameRate=Resources.MainExpressDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Service=Resources.EconomyShipping, Description=Resources.MainExpressDescription1 },
                new RatesForRegularDelivery { ID=121, NameRate=Resources.MainSuperExpressDoorDoor, DeliveryMode=Resources.DoorDoor, Service=Resources.EconomyShipping, Description=Resources.MainSuperExpressDescription1 },
                new RatesForRegularDelivery { ID=122, NameRate=Resources.MainSuperExpressWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Service=Resources.EconomyShipping, Description=Resources.MainSuperExpressDescription1 },
                new RatesForRegularDelivery { ID=123, NameRate=Resources.MainSuperExpressDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Service=Resources.EconomyShipping, Description=Resources.MainSuperExpressDescription1 }
            };
        }
    }

    //ChineseExpress
    public class ChineseExpressList
    {
        public ObservableCollection<ChineseExpress> innerList { get; set; }
        public ChineseExpressList()
        {
            innerList = new ObservableCollection<ChineseExpress>()
            {
                new ChineseExpress { ID=243, NameRate=Resources.ChineseExpressWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Service=Resources.ChineseExpress, Description=Resources.ChineseExpressDescription },
                new ChineseExpress { ID=245, NameRate=Resources.ChineseExpressDoorDoor, DeliveryMode=Resources.DoorDoor, Service=Resources.ChineseExpress },
                new ChineseExpress { ID=246, NameRate=Resources.ChineseExpressWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Service=Resources.ChineseExpress },
                new ChineseExpress { ID=247, NameRate=Resources.ChineseExpressDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Service=Resources.ChineseExpress }
            };
        }
    }

    //ServicesTariffsCDEK
    public class ServicesTariffsCDEKList
    {
        public ObservableCollection<ServicesTariffsCDEK> innerList { get; set; }
        public ServicesTariffsCDEKList()
        {
            innerList = new ObservableCollection<ServicesTariffsCDEK>()
            {
                new ServicesTariffsCDEK { ID=136, NameRate=Resources.ExpressLightDoorDoor, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightDescription, Status=true },
                new ServicesTariffsCDEK { ID=137, NameRate=Resources.ExpressLightDoorPostamat, DeliveryMode=Resources.DoorPostamat, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightDescription, Status=true },
                new ServicesTariffsCDEK { ID=138, NameRate=Resources.ExpressLightWarehousePostamat, DeliveryMode=Resources.WarehousePostomat, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightDescription, Status=true },
                new ServicesTariffsCDEK { ID=139, NameRate=Resources.ExpressВeliveryTo18, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.ExpressВeliveryDescription, Status=true },
                new ServicesTariffsCDEK { ID=366, NameRate=Resources.EconomyShippingWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Service=Resources.EconomyShipping, Description=Resources.EconomyShippingDescription, Status=true },
                new ServicesTariffsCDEK { ID=368, NameRate=Resources.ExpressLightInternationalWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightInternationalDescription, Status=true },
                new ServicesTariffsCDEK { ID=178, NameRate=Resources.ExpressLightInternationalWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightInternationalDescription, Status=false},
                new ServicesTariffsCDEK { ID=179, NameRate=Resources.ExpressLightInternationalDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressLightInternationalDescription, Status=false},
                new ServicesTariffsCDEK { ID=180, NameRate=Resources.ExpressSheavyweightsWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription, Status=false},
                new ServicesTariffsCDEK { ID=181, NameRate=Resources.ExpressSheavyweightsWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription , Status=false},
                new ServicesTariffsCDEK { ID=182, NameRate=Resources.ExpressSheavyweightsDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription, Status=false },
                new ServicesTariffsCDEK { ID=183, NameRate=Resources.ExpressSheavyweightsDoorDoor, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.Express, Description=Resources.ExpressSheavyweightsDescription, Status=false },
                new ServicesTariffsCDEK { ID=231, NameRate=Resources.SuperExpressTo9, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription , Status=false},
                new ServicesTariffsCDEK { ID=232, NameRate=Resources.SuperExpressTo10, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription, Status=false },
                new ServicesTariffsCDEK { ID=233, NameRate=Resources.SuperExpressTo12, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription, Status=true },
                new ServicesTariffsCDEK { ID=234, NameRate=Resources.SuperExpressTo14, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription, Status=true },
                new ServicesTariffsCDEK { ID=376, NameRate=Resources.SuperExpressTo16, DeliveryMode=Resources.DoorDoor, Limitation=Resources.ExpressLightLimit, Service=Resources.ExpressВelivery, Description=Resources.SuperExpressDescription, Status=false },
                new ServicesTariffsCDEK { ID=378, NameRate=Resources.MainExpressWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Service=Resources.EconomyShipping, Description=Resources.MainExpressDescription, Status=true },
                new ServicesTariffsCDEK { ID=291, NameRate=Resources.MainSuperExpressWarehouseWarehouse, DeliveryMode=Resources.WarehouseWarehouse, Service=Resources.EconomyShipping, Description=Resources.MainSuperExpressDescription, Status=true },
                new ServicesTariffsCDEK { ID=293, NameRate=Resources.EconomyExpressDoorDoor, DeliveryMode=Resources.DoorDoor, Service=Resources.EconomyShipping, Description=Resources.EconomyExpressDescription, Status=true },
                new ServicesTariffsCDEK { ID=294, NameRate=Resources.EconomyExpressWarehouseDoor, DeliveryMode=Resources.WarehouseDoor, Service=Resources.EconomyShipping, Description=Resources.EconomyExpressDescription, Status=true },
                new ServicesTariffsCDEK { ID=295, NameRate=Resources.EconomyExpressDoorWarehouse, DeliveryMode=Resources.DoorWarehouse, Service=Resources.EconomyShipping, Description=Resources.EconomyExpressDescription, Status=true }
            };
        }
    }
    class List
    {
    }
}
