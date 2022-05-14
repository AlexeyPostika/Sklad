using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using POSTinkoff.Tinkoff;
using POSTinkoff.Tinkoff.Pax;

namespace POSTinkoff
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TinkoffClass tinkoff;
        public MainWindow()
        {
            InitializeComponent();
            tinkoff = new TinkoffClass();

        }

        private void signalcheck_Click(object sender, RoutedEventArgs e)
        {
            //tinkoff.test();
            Int32? temp = tinkoff.SetChannelTerminalParam();
            if (temp != null)
            {
                if (String.IsNullOrEmpty(checkprint.Text))
                    checkprint.Text = "Статус операции: " + tinkoff.ErrorCode + ", Описание: " + tinkoff.ErrorDescription + " - " + tinkoff.Description;
                else
                    checkprint.Text += Environment.NewLine + "Статус операции: " + tinkoff.ErrorCode + ", Описание: " + tinkoff.ErrorDescription + " - " + tinkoff.Description;
            }
        }

        private void sale_Click(object sender, RoutedEventArgs e)
        {
            SAPacket sAPacket = new SAPacket();
            sAPacket.Amount = (Convert.ToDouble(taxPricePax.Text) * 100).ToString();
            sAPacket.CurrencyCode = "643";
            sAPacket.OperationCode = (Int32)OperationType.Sale;
            sAPacket.TerminalID = "10160942";
            tinkoff = new TinkoffClass();

            SAPacket res = tinkoff.Sale(sAPacket);

            if (String.IsNullOrEmpty(checkprint.Text))
                checkprint.Text = "Тип операции: " + res.OperationCode + ", Описание: " + res.Status + " - " + res.StatusString + Environment.NewLine + res.ReceiptData;
            else
                checkprint.Text += Environment.NewLine + "Тип операции: " + res.OperationCode + ", Описание: " + res.Status + " - " + res.StatusString + Environment.NewLine + res.ReceiptData; ;
            taxPriceRPax.Text = (Convert.ToDouble(res.Amount) / 100).ToString();
            rrnNumberRPax.Text = res.ReferenceNumber;
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            SAPacket sAPacket = new SAPacket();
            sAPacket.Amount = (Convert.ToDouble(taxPriceRPax.Text) * 100).ToString();
            sAPacket.ReferenceNumber = rrnNumberRPax.Text;
            sAPacket.CurrencyCode = "643";
            sAPacket.OperationCode = (Int32)OperationType.Return;
            sAPacket.TerminalID = "10160942";
            tinkoff = new TinkoffClass();

            SAPacket res = tinkoff.Return(sAPacket);

            if (String.IsNullOrEmpty(checkprint.Text))
                checkprint.Text = "Тип операции: " + res.OperationCode + ", Описание: " + res.Status + " - " + res.StatusString + Environment.NewLine + res.ReceiptData;
            else
                checkprint.Text += Environment.NewLine + "Тип операции: " + res.OperationCode + ", Описание: " + res.Status + " - " + res.StatusString + Environment.NewLine + res.ReceiptData; ;

        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            SAPacket sAPacket = new SAPacket();
            sAPacket.Amount = "100";
            sAPacket.CurrencyCode = "643";
            sAPacket.OperationCode = (Int32)OperationType.Buy;
            sAPacket.TerminalID = "10160942";
            tinkoff = new TinkoffClass();

            SAPacket res = tinkoff.Buy(sAPacket);

            if (String.IsNullOrEmpty(checkprint.Text))
                checkprint.Text = "Тип операции: " + res.OperationCode + ", Описание: " + res.Status + " - " + res.StatusString + Environment.NewLine + res.ReceiptData;
            else
                checkprint.Text += Environment.NewLine + "Тип операции: " + res.OperationCode + ", Описание: " + res.Status + " - " + res.StatusString + Environment.NewLine + res.ReceiptData; ;

        }

        private void signalcheckArcus_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Output output = arcus.GetLinkHost();

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Гудок, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Гудок, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Гудок, Status: " + output.cheq.CheckPrint + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Гудок, Status: " + output.cheq.CheckPrint + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
            //SAPacket sAPacket = new SAPacket();
            //Request request = new Request(sAPacket);
        }

        private void saleArcus_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();
            input.currency = ((int)Currency.RUS);
            input.command = ((int)Command.PAYMENT_FOR_GOODS);
            input.amount = (Int32)Convert.ToDouble(taxPrice.Text) * 100;
            Output output = arcus.GetSale(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Sale, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Sale, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }               
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Sale, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Sale, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
            taxPriceR.Text = (Convert.ToDouble(output.chek.Amount) / 100).ToString();
            rrnNumber.Text = output.chek.RRN;
            taxPrice.Text = String.Empty;
        }

        private void returnArcus_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();
            input.currency = ((int)Currency.RUS);
            input.command = ((int)Command.PURCHASE_RETURNS);//CANCEL_LAST  -- PURCHASE_RETURNS
            input.amount = (Int32)Convert.ToDouble(taxPriceR.Text) * 100; 
            input.rrn = Convert.ToInt64(rrnNumber.Text);
            Output output = arcus.GetReturn(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Return, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Return, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Return, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Return, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
        }

        private void completeMagazine_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();
            input.command = ((int)Command.COMPLETE_MAGAZINE);           
            Output output = arcus.GetCompleteMagazine(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Полный отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Полный отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Полный отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Полный отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }

        }

        private void briefReport_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();
            input.currency = ((int)Currency.RUS);
            input.command = ((int)Command.SUMMARY);
            Output output = arcus.GetCompleteMagazine(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Краткий отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Краткий отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Краткий отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Краткий отчет, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
        }

        private void report1_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();
            input.currency = ((int)Currency.RUS);
            input.command = ((int)Command.REVIEW_OF_RESULTS1);
            Output output = arcus.GetCompleteMagazine(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
        }

        private void report2_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();
            input.currency = ((int)Currency.RUS);
            input.command = ((int)Command.REVIEW_OF_RESULTS2);
            Output output = arcus.GetCompleteMagazine(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
        }

        private void report3_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();
            input.currency = ((int)Currency.RUS);
            input.command = ((int)Command.REVIEW_OF_RESULTS3);
            Output output = arcus.GetCompleteMagazine(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Сверка итогов, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            Arcus arcus = new Arcus();
            Input input = new Input();                     
            input.command = ((int)Command.PRINTING_A_DUPLICATE_NNN_CHECK); //поиск по ссылки          
            input.rrn = Convert.ToInt64(textSearchRRN.Text);
            Output output = arcus.GetSearchRRN(input);

            if (String.IsNullOrEmpty(checkprint.Text))
            {
                if (output.rc.Code != "000")
                    checkprint.Text = "Тип операции: Search RRN, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        output.chek.TypeCard + " " + output.chek.Message;
                else
                    checkprint.Text = "Тип операции: Return, Search RRN: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
            }
            else
            {
                if (output.rc.Code != "000")
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Search RRN, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                    output.chek.TypeCard + " " + output.chek.Message;
                }
                else
                {
                    checkprint.Text += Environment.NewLine + "Тип операции: Search RRN, Status: " + output.cheq.ErrorCode + " - " + output.cheq.Description + ", Описание: " + output.cheq.Message + Environment.NewLine +
                        "CodeOperatin: " + output.chek.StatusCode + Environment.NewLine +
                        ", CardNumber: " + output.chek.CardNumber + Environment.NewLine +
                        ", TerminalID: " + output.chek.TerminalID + Environment.NewLine +
                        ", Код а-ции: " + output.chek.AuthorizationCode + Environment.NewLine +
                        ", TypeCade: " + output.chek.TypeCard + Environment.NewLine
                        + ", RRN: " + output.chek.RRN + Environment.NewLine + output.cheq.CheckPrint;
                }
            }
        }
    }
}
