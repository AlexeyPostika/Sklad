using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DualConnector;

namespace Tinkoff_POS_S920.Command
{
    public class ErrorCode
    {
        public Int32 ID { get; set; }     // номер RS232 порта 
        public String Message { get; set; }// скорость порта
        public String Description { get; set; }// скорость порта
    }
    public class ListErrorCode
    {
        public ObservableCollection<ErrorCode> innerList { get; set; }
        public ListErrorCode()
        {
            innerList = new ObservableCollection<ErrorCode>()
            {
                new ErrorCode{ ID = 0,  Message="OK", Description="ошибок нет"},
                new ErrorCode{ ID = 1,  Message="TIMEOUT", Description="истёк таймаут операции"},
                new ErrorCode{ ID = 2,  Message="LOG_ERROR", Description="ошибка создания LOG файла"},
                new ErrorCode{ ID = 3,  Message="SYSTEM_ERROR", Description="общая ошибка"},
                new ErrorCode{ ID = 4,  Message="REQUEST_ERROR", Description="ошибка данных запроса"},
                new ErrorCode{ ID = 6,  Message="CONFIG_NOT_FOUND", Description="не найден файл конфигурации"},
                new ErrorCode{ ID = 7,  Message="CONFIG_ERROR_FORMAT", Description="ошибка формата файла конфигурации"},
                new ErrorCode{ ID = 8,  Message="CONFIG_ERROR_LOG", Description="ошибка параметров логирования"},
                new ErrorCode{ ID = 9,  Message="CONFIG_ERROR_DEVICES", Description="ошибка в параметрах терминала"},
                new ErrorCode{ ID = 10, Message="CONFIG_ERROR_DUBLCOMPORTS", Description="ошибка настройки устройства на COM порт"},
                new ErrorCode{ ID = 11, Message="CONFIG_ERROR_OUTPUT", Description="ошибка в выходных параметрах"},
                new ErrorCode{ ID = 12, Message="PRINT_ERROR", Description="ошибка при передаче образа чека"},
                new ErrorCode{ ID = 13, Message="ERROR_CONNECT", Description="ошибка установки связи с устройством"},
                new ErrorCode{ ID = 14, Message="CONFIG_ERROR_GUI", Description="ошибка в параметрах настройки интерфейса взаимодействия с пользователем"},
                new ErrorCode{ ID = 15, Message="CANCEL_OPERATION", Description="отмена операции"},
            };
        }
    }

    public class Response
    {
        public ISAPacket responsePOS { get; set; }
        public Int32 ErrorCode { get; set; }
        public String MessageErrorCode { get; set; }
        public String DescriptionErrorCode { get; set; }

        public Response()
        {
            responsePOS = (ISAPacket) new SAPacket();
            ErrorCode = -1;
            MessageErrorCode = String.Empty;
            DescriptionErrorCode = String.Empty;
        }
    }
}
