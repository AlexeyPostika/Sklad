using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace POSTinkoff.Tinkoff
{
    public enum TypeOperation
    {
       Sale,
       Return,
       Cancel,
       CloseWorkShift,
       PrintListOperation,
       Menu,
       CancelOperationNumberChek
    }

    public enum Command:Int32
    {
        PAYMENT_FOR_GOODS = 1,                          // 1,1,ОПЛАТА ТОВАРА
        CANCEL_LAST=2,                                  // 2,3,ОТМЕНА ПОСЛ.
        PURCHASE_RETURNS = 3,                           // 1,11,ВОЗВРАТ ТОВАРА
        UNIVERSAL_CANCEL = 4,                           // 1,5,УНИВЕРСАЛЬНАЯ ОТМЕНА
        PRE_AUTHORIZATION = 5,                          // 1,3,ПРЕАВТОРИЗАЦИЯ
        COMPLETION_OF_THE_CALCULATION = 6,              // 1,4,ЗАВЕРШЕНИЕ РАСЧЕТА
        COMPLETE_MAGAZINE = 7,                          // 2,0,ПОЛНЫЙ ЖУРНАЛ - сверка итогов
        SUMMARY = 8,                                    // 2,10,КРАТКИЙ ОТЧЕТ
        CREDIT_VOUCHER = 9,                             // 1,15,CREDIT_VOUCHER
        REVIEW_OF_RESULTS1 = 10,                        // 2,1,СВЕРКА ИТОГОВ - не работает терминал перезагружается
        REVIEW_OF_RESULTS2 = 11,                        // 2,1,СВЕРКА ИТОГОВ - работает
        REVIEW_OF_RESULTS3 = 12,                        // 2,1,СВЕРКА ИТОГОВ - не работает терминал перезагружается
        UNIVERSAL_CANCEL_ADVICE = 13,                   // 1,10,УНИВЕРСАЛЬНАЯ ОТМЕНА_ADVICE
        SENDING_A_DELAYED_CANCEL = 14,                  // 2,19,ОТПРАВКА ОТЛОЖЕННОЙ ОТМЕНЫ

//      #===============БАНК - ПУНКТ ВЫДАЧИ НАЛИЧНЫХ
        CASH_WITHDRAWAL=21,                             // 1,12, ВЫДАЧА НАЛ.
        CASH_WITHDRAWAL1 = 22,                          // 1,13, ВЫДАЧА НАЛ.
        CREDIT_ACCOUNT_CARD=23,                         // 1,14,КРЕДИТ СЧЕТА КАРТЫ
        BALANCE_REQUEST = 24,                           // 1,7,ЗАПРОС БАЛАНСА

//      #============== ПЕРЕВОДЫ ПЛАТЕЖИ
        UTILITY_PAYMENT = 30,                           // 1,16,ПЛАТЕЖ UTILITY_PAYMENT,	
        BILLING_PAYMENT_SMS = 31,                       // 1,17,ПЛАТЕЖ BILLING_PAYMENT_SMS,
        BILLING_PAYMENT_DUAL = 32,                      // 1,18,ПЛАТЕЖ  BILLING_PAYMENT_DUAL	
        Card2Card = 33,                                 // 1,40,ПЕРЕВОД Card2Card
        Cash2Card = 34,                                 // 1,41,ПЕРЕВОД Cash2Card
//      #=============== СПЕЦИАЛЬНЫЕ ОПЕРАЦИИ
        READING_MS_CARD = 51,                           // 2,4,ЧТЕНИЕ КАРТЫ MS
        READ_MAP_MS_SV = 52,                            // 9,2,ЧТЕНИЕ КАРТЫ MS(SV)
        READ_MAP_MS_ANY_TRACK = 53,                     // 9,3,ЧТЕНИЕ КАРТЫ MS(ANY TRACK)
        CARD_READING_CHIP_MS=54,                        // 9,7,ЧТЕНИЕ КАРТЫ (CHIP MS)
        MAP_READING_HOLD_CHIP = 55,                     // 9,8,ЧТЕНИЕ КАРТЫ (HOLD CHIP)
                                                        //#56=2,20,ЗАПРОС ШТРИХКОДА           

//      #=============== ДОПОЛНИТЕЛЬНЫЕ ОПЕРАЦИИ
//      # Такая операция уже доступна для всех софтов NEWWAY : с версии  4.0.2.446
        PRINTING_A_DUPLICATE_OF_THE_LAST_RECEIPT = 60,  // 2,27, ПЕЧАТЬ ДУБЛИКАТА ПОСЛЕДНЕНО ЧЕКА

//      #commandlinetool.exe /qNNN
        PRINTING_A_DUPLICATE_NNN_CHECK = 61,            // 2,14,ПЕЧАТЬ ДУБЛИКАТА ЧЕКА NNN
        PRINT_CHECK_BY_NUMBER_CASHIER_ENTRY = 62,       // 2,26,ПЕЧАТЬ ЧЕКА ПО НОМЕРУ, ВВОД КАССИРА 

//      #В случае если софт Мультихостовый и мы настроили ISO и SPDH/TPTP.
//      # То команда 63=2,1,СВЕРКА ИТОГОВ (ПАКЕТ) закроет ДЕНЬ (как самую большую сущность) для всех виртуальных терминалов.
        VERIFICATION_OF_RESULTS_PACKAGE = 63,           // 2,1,СВЕРКА ИТОГОВ (ПАКЕТ)
        VERIFICATION_OF_RESULTS_CHANGE = 64,            // 2,25,СВЕРКА ИТОГОВ (СМЕНА)
        REVIEW_DAY = 65,                                // 2,22,СВЕРКА ИТОГОВ (ДЕНЬ)
        REPORT_PACKAGE = 66,                            // 2,0,ОТЧЕТ (ПАКЕТ)
        REPORT_CHANGE = 67,                             // 2,23,ОТЧЕТ (СМЕНА)
        REPORT_DAY = 68,                                // 2,24,ОТЧЕТ (ДЕНЬ)

//      #=================== СЕРВИСНЫЕ ФУНКЦИИ
        TIME_SYNCHRONIZATION = 70,                      // 3,1,СИНХРОНИЗАЦИЯ ВРЕМЕНИ
        TIME_SYNCHRONIZATION1 = 71,                     // 3,2,СИНХРОНИЗАЦИЯ ВРЕМЕНИ
        CHECKING_PIN_PAD_CONNECTION = 72,               // 9,6,ПРОВЕРКА ПОДКЛЮЧЕНИЯ ПИН-ПАДА

//      #===============АДМИНИСТРАТИВНЫЕ ОПЕРАЦИИ И МЕНЮ
        HOST_CONNECTION_TEST = 95,                      // 2,6,ТЕСТ СОЕДИНЕНИЯ С ХОСТОМ
        EXCHANGE_COMMUNICATION_TEST = 96,               // 2,18, ТЕСТ СВЯЗИ EXCHANGE
        ST_CASSIR_PASSWORD = 97,                        // 2,9,СТ.КАССИР ПАРОЛЬ:
        CASHIER_MENU = 98,                              // 2,2,МЕНЮ КАССИРА
        ADMINISTRATOR_MENU = 99,                        // 3,5,МЕНЮ АДМИНИСТРАТОРА
        TMS_SESSION = 100,                              // 3,4,СЕССИЯ ТМС

//      #=================  СЕРВИСНЫЕ ФУНКЦИИ - ДОПОЛНЕНИЯ      
        PING = 201,                                     // 9,6,PING - посылает эхо на терминал.
        Sending_a_delayed_message = 202,                // 2,19,Отправка отложенного сообщения 
        TERMINAL_INFO = 203,                            // 2,21,ИНФО ТЕРМИНАЛА

//      #Данная команда 2,21, доступна только с 464 версии ПО
//      #==================== ОПЦИОНАЛЬНО 
//      # ======= КОНТРОЛЬ КАЧЕСТВА ОБСЛУЖИВАНИЯ ====  ( OW)
        QUALITY_CONTROL_CASH = 210,                     // 2,28,КОНТРОЛЬ КАЧЕСТВА НАЛИЧНЫЕ
        QUALITY_CONTROL_RETURN = 211                    // 2,29,КОНТРОЛЬ КАЧЕСТВА ВОЗРАТ         
    }

    public enum Currency : Int32
    {
        RUS = 643,
        USD = 840,
        EUR = 978
    }

    public class CommandOperation
    {
        public Int32 ID { get; set; }
        public String Command { get; set; }
        public String Description { get; set; }
    }

    public class CommandOperationList
    {
        public ObservableCollection<CommandOperation> innerList { get; set; }
        public CommandOperationList()
        {
            innerList = new ObservableCollection<CommandOperation>()
            {
                  new CommandOperation { ID=0, Command="/0", Description="Код операции ККМ" },
                  new CommandOperation { ID=1, Command="/c", Description="Код валюты" },
                  new CommandOperation { ID=2, Command="/a", Description="Сумма" },
                  new CommandOperation { ID=3, Command="/i", Description="Индентификатор терминала" },
                  new CommandOperation { ID=4, Command="/v", Description="Код авторизации" },
                  new CommandOperation { ID=5, Command="/r", Description="Ссылка" },
                  new CommandOperation { ID=6, Command="/e", Description="Данные карты" },
                  new CommandOperation { ID=7, Command="/t", Description="Трек 2" }
            };
        }
    }

    public class RC
    {
        public string Code { get; set; }
    }
    public class Cheq
    {
        public String Description { get; set; }
        public String ErrorCode { get; set; }
        public String Message { get; set; }
        public String CheckPrint { get; set; }
    }

    public class Chek
    {
        public String StatusCode { get; set; }
        public String CardNumber { get; set; }
        public String TerminalID { get; set; }
        public String AuthorizationCode { get; set; }
        public String TypeCard { get; set; } 
        public String Message { get; set; }
        public String Amount { get; set; }
        public String RRN { get; set; }
    }

    public class OutputEx
    {
        public String RC { get; set; }
        public String EXPIRE { get; set; }
        public String DATE { get; set; }
        public String TIME { get; set; }
        public String TVR { get; set; }
        public String TEXT_MSG { get; set; }
    }

    public class Output
    {
        public RC rc { get; set; }
        public Cheq cheq { get; set; }
        public OutputEx outputEx { get; set; }
        public Chek chek { get; set; }

        public Output()
        {
            rc = new RC();
            cheq = new Cheq();
            outputEx = new OutputEx();
            chek = new Chek();
        }
    }

    public class Input
    {
        public Int32 currency { get; set; }
        public Int32 command { get; set; }
        public Int32 amount { get; set; }
        public Int32 original_amount { get; set; }
        public Int32 rfu { get; set; }
        public Int32 terminal_id { get; set; }
        public Int32 ARCUS_ID { get; set; }
        public Int32 auth_code { get; set; }
        public Int64 rrn { get; set; }
        public Int32 enc_data { get; set; }
        public Int32 trace_id { get; set; }
        public DateTime original_date_time { get; set; }
    }

    public class Arcus
    {      
        public Arcus()
        {
                  
        }

        #region Arcus
        public void GetAdmin()
        {
            System.Diagnostics.Process.Start("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe", "/o" + (Int32)Command.ADMINISTRATOR_MENU);    //команда для получения админских прав  
        }

        public Output GetLinkHost()
        {
            //почистим все файлы
            WriteOutputFile();
            //HOST_CONNECTION_TEST

            Process proc = new Process();
            proc.StartInfo.FileName = String.Concat( @"DLL\Arcus\CommandLineTool\CommandLineTool.exe ");//, " '/o" , (Int32)Command.HOST_CONNECTION_TEST,"'"
            proc.StartInfo.Arguments = String.Concat(" /o", (Int32)Command.HOST_CONNECTION_TEST);
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            //proc.WaitForExit();

            ///System.Diagnostics.Process.Start("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe", " /o" + (Int32)Command.HOST_CONNECTION_TEST);    //команда для получения админских прав  
            //ждем результат выполнения команды
            Output output = new Output();
            //RC rc = new RC();
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            //ждем результата
            while (output.rc.Code == "999" || String.IsNullOrEmpty(output.rc.Code))
            {
                output.rc = ReaderOutputFile("rc.out").rc;
                Thread.Sleep(500);
            }

            //парсим резальтат выполнения команды
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            output.outputEx = ReaderOutputFile("output_ex.dat").outputEx;
            Thread.Sleep(500);

            output.chek = ReaderOutputFile("chek.out").chek;
            Thread.Sleep(500);

            output.cheq = ReaderOutputFile("cheq.out", output.rc.Code).cheq;
            Thread.Sleep(500);

            proc.Close();

            return output;
        }
        public Output GetSale(Input _input)
        {
            //почистим все файлы
            WriteOutputFile();

            //запустим выполнение команды

            Process proc = new Process();
            proc.StartInfo.FileName = String.Concat("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe");
            proc.StartInfo.Arguments = String.Concat("/o", _input.command, " /c", _input.currency, " /a", _input.amount);
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            //System.Diagnostics.Process.Start("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe", "/o" + _input.command + " /c" + _input.currency + " /a" + _input.amount);    //команда продажи  

            //ждем результат выполнения команды
            Output output = new Output();
            //RC rc = new RC();
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            //ждем результата
            while (output.rc.Code == "999" || String.IsNullOrEmpty(output.rc.Code))
            {
                output.rc = ReaderOutputFile("rc.out").rc;
                Thread.Sleep(500);
            }

            //парсим резальтат выполнения команды
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            output.outputEx = ReaderOutputFile("output_ex.dat").outputEx;
            Thread.Sleep(500);

            output.chek = ReaderOutputFile("chek.out").chek;
            Thread.Sleep(500);

            output.cheq = ReaderOutputFile("cheq.out", output.rc.Code).cheq;
            Thread.Sleep(500);

            proc.Close();

            return output;
        }

        public Output GetReturn(Input _input)
        {
            //почистим все файлы
            WriteOutputFile();

            //запустим выполнение команды
            Process proc = new Process();
            proc.StartInfo.FileName = String.Concat("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe");
            proc.StartInfo.Arguments = String.Concat("/o" + _input.command + " /c" + _input.currency + " /a" + _input.amount + " /r" + _input.rrn);
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();

            //System.Diagnostics.Process.Start("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe", "/o" + _input.command + " /c" + _input.currency + " /a" + _input.amount + " /r" + _input.rrn);    //команда продажи  

            //ждем результат выполнения команды
            Output output = new Output();
            //RC rc = new RC();
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            //ждем результата
            while (output.rc.Code == "999" || String.IsNullOrEmpty(output.rc.Code))
            {
                output.rc = ReaderOutputFile("rc.out").rc;
                Thread.Sleep(500);
            }

            //парсим резальтат выполнения команды
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            output.outputEx = ReaderOutputFile("output_ex.dat").outputEx;
            Thread.Sleep(500);

            output.chek = ReaderOutputFile("chek.out").chek;
            Thread.Sleep(500);

            output.cheq = ReaderOutputFile("cheq.out", output.rc.Code).cheq;
            Thread.Sleep(500);

            proc.Close();

            return output;
        }

        public Output GetCompleteMagazine(Input _input)
        {
            //почистим все файлы
            WriteOutputFile();

            //запустим выполнение команды
            Process proc = new Process();
            proc.StartInfo.FileName = String.Concat("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe");
            proc.StartInfo.Arguments = String.Concat("/o" + _input.command);
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();

           // System.Diagnostics.Process.Start("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe", "/o" + _input.command);    //команда продажи  

            //ждем результат выполнения команды
            Output output = new Output();
            //RC rc = new RC();
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            //ждем результата
            while (output.rc.Code == "999" || String.IsNullOrEmpty(output.rc.Code))
            {
                output.rc = ReaderOutputFile("rc.out").rc;
                Thread.Sleep(500);
            }

            //парсим резальтат выполнения команды
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            output.outputEx = ReaderOutputFile("output_ex.dat").outputEx;
            Thread.Sleep(500);

            output.chek = ReaderOutputFile("chek.out").chek;
            Thread.Sleep(500);

            output.cheq = ReaderOutputFile("cheq.out", output.rc.Code).cheq;
            Thread.Sleep(500);

            proc.Close();

            return output;
        }

        public Output GetSearchRRN(Input _input)
        {
            //почистим все файлы
            WriteOutputFile();

            //запустим выполнение команды
            Process proc = new Process();
            proc.StartInfo.FileName = String.Concat("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe");
            proc.StartInfo.Arguments = String.Concat("/o" + _input.command + " /r" + _input.rrn);
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();

            //System.Diagnostics.Process.Start("DLL\\Arcus\\CommandLineTool\\CommandLineTool.exe", "/o" + _input.command + " /r" + _input.rrn);    //команда продажи  

            //ждем результат выполнения команды
            Output output = new Output();
            //RC rc = new RC();
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            //ждем результата
            while (output.rc.Code == "999" || String.IsNullOrEmpty(output.rc.Code))
            {
                output.rc = ReaderOutputFile("rc.out").rc;
                Thread.Sleep(500);
            }

            //парсим резальтат выполнения команды
            output.rc = ReaderOutputFile("rc.out").rc;
            Thread.Sleep(500);

            output.outputEx = ReaderOutputFile("output_ex.dat").outputEx;
            Thread.Sleep(500);

            output.chek = ReaderOutputFile("chek.out").chek;
            Thread.Sleep(500);

            output.cheq = ReaderOutputFile("cheq.out", output.rc.Code).cheq;
            Thread.Sleep(500);
           
            proc.Close();

            return output;
        }


        private bool WriteOutputFile()
        {
            try
            {   //Open the File
                FileStream fs = new FileStream("DLL\\Arcus\\Output\\chek.out", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();

                FileStream fs1 = new FileStream("DLL\\Arcus\\Output\\cheq.out", FileMode.Create, FileAccess.Write);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.Close();

                FileStream fs2 = new FileStream("DLL\\Arcus\\Output\\output.dat", FileMode.Create, FileAccess.Write);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.Close();

                FileStream fs3 = new FileStream("DLL\\Arcus\\Output\\rc.out", FileMode.Create, FileAccess.Write);
                StreamWriter sw3 = new StreamWriter(fs3);
                sw3.Close();

            }
         catch(Exception e)
            {
                
            }

            return false;
        }


        private Output ReaderOutputFile(String _name, String _code=null)
        {
            String line = null;
            Output temp = new Output();
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("DLL\\Arcus\\Output\\"+_name, Encoding.Default);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    switch (_name)
                    {
                        case "rc.out":
                            temp.rc.Code = line;
                            break;
                        case "cheq.out":
                            if (temp.rc.Code != "000" && _code==null)
                            {
                                if (temp.cheq.Description == null)
                                {
                                    temp.cheq.Description = line;
                                    break;
                                }
                                if (temp.cheq.ErrorCode == null)
                                {
                                    temp.cheq.ErrorCode = line.Split(':')[1];
                                    break;
                                }
                                if (temp.cheq.Message == null)
                                {
                                    temp.cheq.Message = line;
                                    break;
                                }
                            }
                            else
                            {
                                temp.cheq.CheckPrint = line + Environment.NewLine + temp.cheq.CheckPrint;
                            }
                            break;
                        case "chek.out":
                            if (temp.chek.StatusCode == null)
                            {
                                if (line != "000")
                                {
                                    if (line.Split(':').Length > 1)
                                    {
                                        temp.chek.StatusCode = line.Split(':')[1];
                                    }
                                    else
                                        temp.chek.StatusCode = line;//line.Split(':')[1]
                                }
                                else
                                {
                                    temp.chek.StatusCode = line;
                                }
                                break;
                            }
                            if (temp.chek.CardNumber == null)
                            {
                                temp.chek.CardNumber = line;
                                break;
                            }
                            if (temp.chek.TerminalID == null)
                            {
                                temp.chek.TerminalID = line;
                                break;
                            }
                            if (temp.chek.AuthorizationCode == null)
                            {
                                temp.chek.AuthorizationCode = line;
                                break;
                            }
                            if (temp.chek.TypeCard == null)
                            {
                                temp.chek.TypeCard = line;
                                break;
                            }
                            if (temp.chek.Message == null)
                            {
                                temp.chek.Message = line;
                                break;
                            }
                            if (temp.chek.Amount == null)
                            {
                                temp.chek.Amount = line;
                                break;
                            }
                            if (temp.chek.RRN == null)
                            {
                                temp.chek.RRN = line;
                                break;
                            }
                            break;
                        case "output_ex.dat":
                            if (line.Split('=')[0] == "RC=000")
                                temp.outputEx.RC = line.Split('=')[1];
                            if (line.Split('=')[0] == "EXPIRE=000000")
                                temp.outputEx.EXPIRE = line.Split('=')[1];
                            if (line.Split('=')[0] == "DATE=0000")
                                temp.outputEx.DATE = line.Split('=')[1];
                            if (line.Split('=')[0] == "TIME=000000")
                                temp.outputEx.TIME = line.Split('=')[1];
                            if (line.Split('=')[0] == "TVR=0000000000")
                                temp.outputEx.TVR = line.Split('=')[1];
                            if (line.Split('=')[0] == "TEXT_MSG=Успешно")
                                temp.outputEx.TEXT_MSG = line.Split('=')[1];
                            break;
                    }                  
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            return temp;
        }

        #endregion

    }
}
