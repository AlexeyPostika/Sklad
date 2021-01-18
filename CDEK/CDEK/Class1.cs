using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDEK
{
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
        List<String> tariffId;//Код выбранного тарифа (подробнее см. приложение 1)  integer да
        Int32 tariffList;//Список тарифов да
        Int32 priorityTariffList;// Заданный приоритет integer да
        Int32 idTariffList;// Код тарифа(подробнее см. приложение 1) integer да

        Int32 modeId;// Режим доставки(подробнее см. приложение 1) integer нет

        Int32 goods;// Габаритные характеристики упаковки да
        Double weightGoods;// Вес упаковки(в килограммах)    float да
        Int32 lengthGoods;// Длина упаковки(в сантиметрах)  integer да
        Int32 widthGoods;// Ширина упаковки(в сантиметрах) integer да
        Int32 heightGoods;// Высота упаковки(в сантиметрах) integer да

        Int32 services;// Список передаваемых дополнительных услуг(подробнее см. приложение 2) нет
        Int32 idServices;//Идентификатор номера дополнительной услуги integer да
        Int32 param5Services;// Параметр дополнительной услуги, если необходимо integer нет

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
        public List<String> TariffId { get => tariffId; set => tariffId = value; }
        public int TariffList { get => tariffList; set => tariffList = value; }
        public int PriorityTariffList { get => priorityTariffList; set => priorityTariffList = value; }
        public int IdTariffList { get => idTariffList; set => idTariffList = value; }
        public int ModeId { get => modeId; set => modeId = value; }
        public int Goods { get => goods; set => goods = value; }
        public double WeightGoods { get => weightGoods; set => weightGoods = value; }
        public int LengthGoods { get => lengthGoods; set => lengthGoods = value; }
        public int WidthGoods { get => widthGoods; set => widthGoods = value; }
        public int HeightGoods { get => heightGoods; set => heightGoods = value; }
        public int Services { get => services; set => services = value; }
        public int IdServices { get => idServices; set => idServices = value; }
        public int Param5Services { get => param5Services; set => param5Services = value; }
    }

    //форма для получения ответа
    public class AdressRowReceive
    {
        List<String> error;// Массив ошибок при их возникновении(подробнее см. приложение 9)
	    Int32 codeError;// Код ошибки integer
        String textError;// Текст ошибки string
        Double price;// Сумма за доставку в рублях  double
        Int32 deliveryPeriodMin;// Минимальное время доставки в днях   integer
        Int32 deliveryPeriodMax;// Максимальное время доставки в днях  integer
        DateTime deliveryDateMin;// Минимальная дата доставки, формате 'ГГГГ-ММ-ДД', например “2018-07-29”	string
        DateTime deliveryDateMax;// Максимальная дата доставки, формате 'ГГГГ-ММ-ДД', например “2018-07-30”	string
        Int32 tariffId;//Код тарифа, по которому рассчитана сумма доставки integer
        Double cashOnDelivery;// Ограничение оплаты наличными, появляется только если оно есть   float
        Double priceByCurrency;// Цена в валюте взаиморасчетов.Валюта определяется по authLogin и secure.    float
        String currency;//Валюта интернет-магазина (подробнее см. приложение 3)   string
        Double percentVAT;// Размер ставки НДС для данного клиента.Появляется в случае, если переданы authLogin и secure, по ним же определяется ставка ИМ.Если ставка НДС не предусмотрена условиями договора, данный параметр не будет отображен.integer
        List<String>services;//Список передаваемых дополнительных услуг (подробнее см. приложение 2)
        Int32 idServices;//Идентификатор переданной услуги integer
        String titleServices;//Заголовок услуги string
        Double priceServices;// Стоимость услуги без учета НДС в рублях float
        Double rateServices;//Процент для расчета дополнительной услуги   float
    }
    public class CalculatorNOTPriority
    {
    }
}
