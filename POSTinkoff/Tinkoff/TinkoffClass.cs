using DualConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POSTinkoff.Tinkoff
{
    public enum OperationType : Int32
    {
        Sale = 1,
        Buy = 3,
        Return = 4,
        Money = 2,
    }
    public class ErrorCode
    {
        public Int32 ID { get; set; }     // номер RS232 порта 
        public String Description { get; set; }// скорость порта
        public String DescriptionRUS { get; set; }// скорость порта
    }
    public class ListErrorCode
    {
        public ObservableCollection<ErrorCode> innerList { get; set; }
        public ListErrorCode()
        {
            innerList = new ObservableCollection<ErrorCode>()
            {
                new ErrorCode{ ID = 0,  Description="OK", DescriptionRUS="ошибок нет"},
                new ErrorCode{ ID = 1,  Description="TIMEOUT", DescriptionRUS="истёк таймаут операции"},
                new ErrorCode{ ID = 2,  Description="LOG_ERROR", DescriptionRUS="ошибка создания LOG файла"},
                new ErrorCode{ ID = 3,  Description="SYSTEM_ERROR", DescriptionRUS="общая ошибка"},
                new ErrorCode{ ID = 4,  Description="REQUEST_ERROR", DescriptionRUS="ошибка данных запроса"},
                new ErrorCode{ ID = 6,  Description="CONFIG_NOT_FOUND", DescriptionRUS="не найден файл конфигурации"},
                new ErrorCode{ ID = 7,  Description="CONFIG_ERROR_FORMAT", DescriptionRUS="ошибка формата файла конфигурации"},
                new ErrorCode{ ID = 8,  Description="CONFIG_ERROR_LOG", DescriptionRUS="ошибка параметров логирования"},
                new ErrorCode{ ID = 9,  Description="CONFIG_ERROR_DEVICES", DescriptionRUS="ошибка в параметрах терминала"},
                new ErrorCode{ ID = 10, Description="CONFIG_ERROR_DUBLCOMPORTS", DescriptionRUS="ошибка настройки устройства на COM порт"},
                new ErrorCode{ ID = 11, Description="CONFIG_ERROR_OUTPUT", DescriptionRUS="ошибка в выходных параметрах"},
                new ErrorCode{ ID = 12, Description="PRINT_ERROR", DescriptionRUS="ошибка при передаче образа чека"},
                new ErrorCode{ ID = 13, Description="ERROR_CONNECT", DescriptionRUS="ошибка установки связи с устройством"},
                new ErrorCode{ ID = 14, Description="CONFIG_ERROR_GUI", DescriptionRUS="ошибка в параметрах настройки интерфейса взаимодействия с пользователем"},
                new ErrorCode{ ID = 15, Description="CANCEL_OPERATION", DescriptionRUS="отмена операции"},
            };
        }
    }
    public class Status
    {
        public Int32 ID { get; set; }     // номер RS232 порта 
        public String Description { get; set; }// скорость порта
    }
    public class ListStatus
    {
        public ObservableCollection<Status> innerList { get; set; }
        public ListStatus()
        {
            innerList = new ObservableCollection<Status>()
            {
                new Status{ ID = 0,  Description="Неопределенный статус"},
                new Status{ ID = 1,  Description="Одобрено"},
                new Status{ ID = 16, Description="Отказано"},
                new Status{ ID = 17, Description="Выполнено в OFFLINE"},
                new Status{ ID = 34, Description="Нет соединения"},
                new Status{ ID = 53, Description="Операция прервана"},
            };
        }
    }

    public class TinkoffClass
    {   //   методы     -   Назначение 
        //InitResources - Инициализация ресурсов 
        //Exchange      - Обмен информацией с терминалом 
        //FreeResources -  Освобождение ресурсов
        //SetChannelTerminalParam - Динамическая установка параметров связи с терминалом (значения файла параметров игнорируются) 
        //Cancel - Прерывание выполняемой на терминале операции

        #region SetChannelTerminalParam (nCOM, BaudRate, ByteSize, Parity, StopBits, FlowCtrl) - Динамическая установка параметров связи с терминалом (значения файла параметров игнорируются) 

        public Int64 nCom { get; set; }     // номер RS232 порта 
        public Int64 BaudeRate { get; set; }// скорость порта
        public Int64 ByteSize { get; set; } // размер байта (игнорируется, всегда 8); 
        public Int64 Parity { get; set; }   // чётность(игнорируется, всегда нет)
        public Int64 StopBits { get; set; } // количество cтоп-битов
        public Int64 FlowCtrl { get; set; } // контроль передачи (Игнорируется, всегда OFF)

        #endregion

        #region ErrorCode
        public Int32 ErrorCode { get; set; } // результат операции 
        public String ErrorDescription { get; set; }        // текстовое описание значения ErrorCode 
        public String Description { get; set; }        // текстовое описание значения ErrorCode 
        #endregion

        DualConnector.DCLink dCLink;
        DualConnector.ISAPacket query;
        DualConnector.ISAPacket response;
        DualConnector.DualConnectorInterface connectorInterface;



        public TinkoffClass()
        {
            nCom = 10;
            BaudeRate = 11520;
        }

        public Int32? SetChannelTerminalParam()
        {
            ListErrorCode listErrorCode = new ListErrorCode();
            dCLink = new DualConnector.DCLink();
            DualConnector.ISAPacket query = new DualConnector.SAPacket();
            DualConnector.ISAPacket response = new DualConnector.SAPacket();
            //query.Amount = "1000";
            //query.CurrencyCode = "643";
            query.OperationCode = 1;
            query.TerminalID = "10160942";           
            int res = dCLink.InitResources();
            if (res == 0)
            {
                //dCLink.SetCallback();
                ErrorCode = dCLink.Exchange(ref query, ref response, 18000);
                if (ErrorCode >= 0)
                {
                    ErrorDescription = listErrorCode.innerList.FirstOrDefault(x => x.ID == ErrorCode) != null ? listErrorCode.innerList.FirstOrDefault(x => x.ID == ErrorCode).Description : String.Empty;
                    Description = listErrorCode.innerList.FirstOrDefault(x => x.ID == ErrorCode) != null ? listErrorCode.innerList.FirstOrDefault(x => x.ID == ErrorCode).DescriptionRUS : String.Empty;
                    dCLink.Dispose();
                    return ErrorCode;
                }
            }
            dCLink.Dispose();
            return null;
        }
      
        public SAPacket Sale(SAPacket _sAPacket)
        {
            DualConnector.DCLink dclink = new DualConnector.DCLink(); 
            DualConnector.ISAPacket query = new DualConnector.SAPacket(); 
            DualConnector.ISAPacket response = new DualConnector.SAPacket();
            query.Amount = _sAPacket.Amount; 
            query.CurrencyCode = _sAPacket.CurrencyCode;
            query.OperationCode = _sAPacket.OperationCode; 
            query.TerminalID = _sAPacket.TerminalID;  
           
            //dclink.OnExchange += new DualConnector.OnExchangeHandler(dclink_OnExchange);
            int res = dclink.InitResources();  
            if ( res != 0 )  
            {  
                MessageBox.Show(string.Format("Init resource:{0}-{1}",res,dclink.ErrorDescription) );  
            }  
            res  = dclink.Exchange( ref query, ref response, 180000 );  
            if ( res != 0 )  {  
                MessageBox.Show(string.Format("Exchange:{0}-{1}",res,dclink.ErrorDescription));  
            }  
            dclink.Dispose();

            SAPacket sAPacket = new SAPacket();
            Convert(response, sAPacket);
            return sAPacket;
        }

        public SAPacket Return(SAPacket _sAPacket)
        {
            DualConnector.DCLink dclink = new DualConnector.DCLink();
            DualConnector.ISAPacket query = new DualConnector.SAPacket();
            DualConnector.ISAPacket response = new DualConnector.SAPacket();
            query.Amount = _sAPacket.Amount;
            query.CurrencyCode = _sAPacket.CurrencyCode;
            query.OperationCode = _sAPacket.OperationCode;
            query.TerminalID = _sAPacket.TerminalID;

            //dclink.OnExchange += new DualConnector.OnExchangeHandler(dclink_OnExchange);
            int res = dclink.InitResources();
            if (res != 0)
            {
                MessageBox.Show(string.Format("Init resource:{0}-{1}", res, dclink.ErrorDescription));
            }
            res = dclink.Exchange(ref query, ref response, 180000);
            if (res != 0)
            {
                MessageBox.Show(string.Format("Exchange:{0}-{1}", res, dclink.ErrorDescription));
            }
            dclink.Dispose();
            
            SAPacket sAPacket = new SAPacket();
            Convert(response, sAPacket);
            return sAPacket;
        }

        public SAPacket Buy(SAPacket _sAPacket)
        {
            DualConnector.DCLink dclink = new DualConnector.DCLink();
            DualConnector.ISAPacket query = new DualConnector.SAPacket();
            DualConnector.ISAPacket response = new DualConnector.SAPacket();
            query.Amount = _sAPacket.Amount;
            query.CurrencyCode = _sAPacket.CurrencyCode;
            query.OperationCode = _sAPacket.OperationCode;
            query.TerminalID = _sAPacket.TerminalID;

            //dclink.OnExchange += new DualConnector.OnExchangeHandler(dclink_OnExchange);
            int res = dclink.InitResources();
            if (res != 0)
            {
                MessageBox.Show(string.Format("Init resource:{0}-{1}", res, dclink.ErrorDescription));
            }
            res = dclink.Exchange(ref query, ref response, 180000);
            if (res != 0)
            {
                MessageBox.Show(string.Format("Exchange:{0}-{1}", res, dclink.ErrorDescription));
            }
            dclink.Dispose();
            SAPacket sAPacket = new SAPacket();
            Convert(response, sAPacket);
            return sAPacket;
        }

        private void Convert(ISAPacket response, SAPacket _sAPacket)
        {
            ListStatus listStatus = new ListStatus();

            _sAPacket.Amount = response.Amount;              // Сумма операции, выраженная в минимальных единицах валюты
            _sAPacket.AdditionalAmount = response.AdditionalAmount;    // Дополнительная сумма операции, выраженная в минимальных единицах валюты 
            _sAPacket.CurrencyCode = response.CurrencyCode;        // Код валюты операции 
            _sAPacket.DateTimeHost = response.DateTimeHost;        // Оригинальная дата и время совершения операции YYYYMMDDHHMMSS на хосте 
            _sAPacket.CardEntryMode = response.CardEntryMode;        // Способ ввода карты 
            _sAPacket.PINCodingMode = response.PINCodingMode;        // Способ кодировки PIN-блока1 
            _sAPacket.PAN = response.PAN;                 // Номер карты
            _sAPacket.CardExpiryDate = response.CardExpiryDate;      // Срок действия карты YYMM 
            _sAPacket.TRACK2 = response.TRACK2;              // Данные Track2 
            _sAPacket.AuthorizationCode = response.AuthorizationCode;   // Код авторизации  
            _sAPacket.ReferenceNumber = response.ReferenceNumber;     // Номер ссылки 
            _sAPacket.ResponseCodeHost = response.ResponseCodeHost;    // Код ответа 
            _sAPacket.PinBlock = response.PinBlock;            // Данные PIN-блока 
            _sAPacket.PinKey = response.PinKey;              // Рабочий ключ PIN 
            _sAPacket.WorkKey = response.WorkKey;             // Рабочий ключ  
            _sAPacket.TextResponse = response.TextResponse;        // Дополнительные данные ответа 
            _sAPacket.TerminalDateTime = response.TerminalDateTime;    // Оригинальная дата и время совершения операции YYYYMMDDHHMMSS на внешнем устройстве
            _sAPacket.TrxID = response.TrxID;                // Идентификатор транзакции в коммуникационном сервере  
            _sAPacket.OperationCode = response.OperationCode;        // Код операции  
            _sAPacket.TerminalTrxID = response.TerminalTrxID;        // Уникальный номер транзакции на стороне внешнего устройства  
            _sAPacket.TerminalID = response.TerminalID;          // Идентификатор внешнего устройства 
            _sAPacket.MerchantID = response.MerchantID;          // Идентификатор продавца 
            _sAPacket.DebitAmount = response.DebitAmount;         // Сумма дебетовых итогов  
            _sAPacket.DebitCount = response.DebitCount;        // Количество дебетовых итогов 
            _sAPacket.CreditAmount = response.CreditAmount;      // Сумма кредитовых итогов 
            _sAPacket.CreditCount = response.CreditCount;        // Количество кредитовых итогов  
            _sAPacket.OrigOperation = (OperationType)response.OrigOperation;  // Код оригинальной операции 
            _sAPacket.MAC = response.MAC;                 // Данные MAC  
            _sAPacket.Status = response.Status;               // Статус проведения транзакции
            _sAPacket.AdminTrack2 = response.AdminTrack2;         // Track2 карты администратора  
            _sAPacket.AdminPinBlock = response.AdminPinBlock;       // Данные PIN-блока карты администратора
            _sAPacket.AdminPAN = response.AdminPAN;            // Номер карты администратора  
            _sAPacket.AdminCardExpiryDate = response.AdminCardExpiryDate; // Срок действия карты администратора  
            _sAPacket.AdminCardEntryMode = response.AdminCardEntryMode;   // Способ ввода карты администратора   
            _sAPacket.VoidDebitAmount = response.VoidDebitAmount;     // Сумма дебетовых отмен  
            _sAPacket.VoidDebitCount = response.VoidDebitCount;      // Статус получения Dual Connector результата операции от терминала (при обмене с кассовым ПО не используется)
            _sAPacket.VoidCreditAmount = response.VoidCreditAmount;    // Статус получения кассовым ПО результата операции от терминала  
            _sAPacket.VoidCreditCount = response.VoidCreditCount;    // Номер слипа   
            _sAPacket.ProcessingFlag = response.ProcessingFlag;     // Флаг обработки операции  
            _sAPacket.HostTrxID = response.HostTrxID;       // Идентификатор транзакции на хосте
            _sAPacket.RecipientAddress = response.RecipientAddress;     // Адрес получателя 
            _sAPacket.CardWaitTimeout = response.CardWaitTimeout;    // Таймаут ожидания карты 
            _sAPacket.DeviceSerNumber = response.DeviceSerNumber;    // Серийный номер 
            _sAPacket.CommandMode = response.CommandMode;          // Режим выполнения команды 
            _sAPacket.CommandMode2 = response.CommandMode2;      // Режим выполнения команды 2 
            _sAPacket.CommandResult = response.CommandResult;       // Статус (результат) выполнения команды 
            _sAPacket.FileData = response.FileData;         // Данные (файл) 
            _sAPacket.MessageED = response.MessageED;      // Сообщение для вывода на экран ВУ  
            _sAPacket.CashierRequest = response.CashierRequest;   // Запрос к кассиру 
            _sAPacket.CashierResponse = response.CashierResponse;    // Ответ кассира 
            _sAPacket.AccountType = response.AccountType;       // Тип счёта клиента 
            _sAPacket.CommodityCode = response.CommodityCode;     // Код платежа  
            _sAPacket.PaymentDetails = response.PaymentDetails;     // Детали платежа  
            _sAPacket.ProviderCode = response.ProviderCode;     // Код провайдера  
            _sAPacket.Acquirer = response.Acquirer;          // Эквайер  
            _sAPacket.AdditionalData = response.AdditionalData;    // Дополнительные данные транзакции 
            _sAPacket.ModelNo = response.ModelNo;             // Наименование модели ВУ
            _sAPacket.ReceiptData = response.ReceiptData;       // Данные для печати на чеке  
            if (_sAPacket.Status > 0)
            {
                _sAPacket.StatusString = listStatus.innerList.FirstOrDefault(x => x.ID == _sAPacket.Status).Description;
            }         
        }

        public void test()
        {
            long result = -1; 
            string xmlString = File.ReadAllText(@"TextFile1.xml", Encoding.GetEncoding("windows-1251")); 
            using (WebClient client = new WebClient()) 
            { 
                string responseString = null; 
                try 
                { 
                    string serviceIpAddres = "127.0.0.1:6000"; 
                    string head = "http://" + serviceIpAddres; 
                    client.Encoding = Encoding.GetEncoding("windows-1251");
                    client.Headers.Add("Content-Type: text/xml;charset=" + client.Encoding.WebName); 
                    responseString = client.UploadString(head, xmlString); 
                    if (responseString != null) 
                    { 
                        result = responseString.Length; 
                    } 
                    else 
                    { 
                        result = -100; 
                    } 
                }
                catch (WebException e) 
                { 
                    Console.WriteLine(e.Message); 
                    result = -200; 
                } 
                if (result > 0) 
                { 
                    Console.WriteLine(responseString); 
                } 
                else 
                {
                    Console.WriteLine(result); 
                } 
            }
        }
    }

    public class SAPacket
    {
        public String Amount { get; set; }              // Сумма операции, выраженная в минимальных единицах валюты
        public String AdditionalAmount { get; set; }    // Дополнительная сумма операции, выраженная в минимальных единицах валюты 
        public String CurrencyCode { get; set; }        // Код валюты операции 
        public String DateTimeHost { get; set; }        // Оригинальная дата и время совершения операции YYYYMMDDHHMMSS на хосте 
        public Int32 CardEntryMode { get; set; }        // Способ ввода карты 
        public Int32 PINCodingMode { get; set; }        // Способ кодировки PIN-блока1 
        public String PAN { get; set; }                 // Номер карты
        public String CardExpiryDate { get; set; }      // Срок действия карты YYMM 
        public String TRACK2 { get; set; }              // Данные Track2 
        public String AuthorizationCode { get; set; }   // Код авторизации  
        public String ReferenceNumber { get; set; }     // Номер ссылки 
        public String ResponseCodeHost { get; set; }    // Код ответа 
        public String PinBlock { get; set; }            // Данные PIN-блока 
        public String PinKey { get; set; }              // Рабочий ключ PIN 
        public String WorkKey { get; set; }             // Рабочий ключ  
        public String TextResponse { get; set; }        // Дополнительные данные ответа 
        public String TerminalDateTime { get; set; }    // Оригинальная дата и время совершения операции YYYYMMDDHHMMSS на внешнем устройстве
        public Int32 TrxID { get; set; }                // Идентификатор транзакции в коммуникационном сервере  
        public Int32 OperationCode { get; set; }        // Код операции  
        public Int32 TerminalTrxID { get; set; }        // Уникальный номер транзакции на стороне внешнего устройства  
        public String TerminalID { get; set; }          // Идентификатор внешнего устройства 
        public String MerchantID { get; set; }          // Идентификатор продавца 
        public String DebitAmount { get; set; }         // Сумма дебетовых итогов  
        public String DebitCount { get; set; }          // Количество дебетовых итогов 
        public String CreditAmount { get; set; }        // Сумма кредитовых итогов 
        public String CreditCount { get; set; }         // Количество кредитовых итогов  
        public OperationType OrigOperation { get; set; }        // Код оригинальной операции 
        public String MAC { get; set; }                 // Данные MAC  
        public Int32 Status { get; set; }               // Статус проведения транзакции
        public String AdminTrack2 { get; set; }         // Track2 карты администратора  
        public String AdminPinBlock { get; set; }       // Данные PIN-блока карты администратора
        public String AdminPAN { get; set; }            // Номер карты администратора  
        public String AdminCardExpiryDate { get; set; } // Срок действия карты администратора  
        public Int32 AdminCardEntryMode { get; set; }   // Способ ввода карты администратора   
        public String VoidDebitAmount { get; set; }     // Сумма дебетовых отмен  
        public String VoidDebitCount { get; set; }      // Статус получения Dual Connector результата операции от терминала (при обмене с кассовым ПО не используется)
        public String VoidCreditAmount { get; set; }    // Статус получения кассовым ПО результата операции от терминала  
        public String VoidCreditCount { get; set; }     // Номер слипа   
        public Int32 ProcessingFlag { get; set; }       // Флаг обработки операции  
        public Int32 HostTrxID { get; set; }            // Идентификатор транзакции на хосте
        public Int32 RecipientAddress { get; set; }     // Адрес получателя 
        public Int32 CardWaitTimeout { get; set; }      // Таймаут ожидания карты 
        public String DeviceSerNumber { get; set; }     // Серийный номер 
        public Int32 CommandMode { get; set; }          // Режим выполнения команды 
        public Int32 CommandMode2 { get; set; }         // Режим выполнения команды 2 
        public Int32 CommandResult { get; set; }        // Статус (результат) выполнения команды 
        public String FileData { get; set; }            // Данные (файл) 
        public String MessageED { get; set; }           // Сообщение для вывода на экран ВУ  
        public String CashierRequest { get; set; }      // Запрос к кассиру 
        public String CashierResponse { get; set; }     // Ответ кассира 
        public String AccountType { get; set; }         // Тип счёта клиента 
        public String CommodityCode { get; set; }       // Код платежа  
        public String PaymentDetails { get; set; }      // Детали платежа  
        public String ProviderCode { get; set; }        // Код провайдера  
        public String Acquirer { get; set; }            // Эквайер  
        public String AdditionalData { get; set; }      // Дополнительные данные транзакции 
        public String ModelNo { get; set; }             // Наименование модели ВУ
        public String ReceiptData { get; set; }        // Данные для печати на чеке  
        public String StatusString { get; set; }
        public TerminalHost terminalHost { get; set; }
        public ErrorStatus errorStatus { get; set; }

        public SAPacket()
        {
            terminalHost = new TerminalHost();
            errorStatus = new ErrorStatus();
        }
    }

    public class TerminalHost
    {
        public Int64 nCom { get; set; }     // номер RS232 порта 
        public Int64 BaudeRate { get; set; }// скорость порта
        public Int64 ByteSize { get; set; } // размер байта (игнорируется, всегда 8); 
        public Int64 Parity { get; set; }   // чётность(игнорируется, всегда нет)
        public Int64 StopBits { get; set; } // количество cтоп-битов
        public Int64 FlowCtrl { get; set; } // контроль передачи (Игнорируется, всегда OFF)
    }

    public class ErrorStatus
    {
        public Int32 ErrorCode { get; set; } // результат операции 
        public String ErrorDescription { get; set; }        // текстовое описание значения ErrorCode 
        public String Description { get; set; }        // текстовое описание значения ErrorCode 
    }
}
