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
using Tinkoff_POS_S920.Command;

namespace Tinkoff_POS_S920
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sendTerminal_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            CommandS920 command = new CommandS920();
            command.com = 0;
            var temp =command.SendTerminal(request);
            if (temp.ErrorCode > 0)
                MessageBox.Show(temp.MessageErrorCode.ToString());
        }

        private void saleButton_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            CommandS920 command = new CommandS920();
            request.requestPOS.Amount = (Convert.ToDouble(tagprice.Text) * 100).ToString();
            request.requestPOS.CurrencyCode = "643";
            request.requestPOS.OperationCode = (Int32)OperationCode.Sale;
            request.requestPOS.TerminalID = "10160942";
            command.com = 1;
            command.CommandPOS(request);
            Response response = command.response;
            if (String.IsNullOrEmpty(description.Text))
            {
                description.Text= "Тип операции: " + response.responsePOS.OrigOperation + " - Sale, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
            else
            {
                description.Text += Environment.NewLine + "Тип операции: " + response.responsePOS.OrigOperation + " - Sale, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            CommandS920 command = new CommandS920();
            request.requestPOS.Amount = (Convert.ToDouble(returnTagPrice.Text) * 100).ToString();
            request.requestPOS.ReferenceNumber = returnRRN.Text;
            request.requestPOS.CurrencyCode = "643";
            request.requestPOS.OperationCode = (Int32)OperationCode.Return;
            request.requestPOS.TerminalID = "10160942";
            command.com = 2;
            command.CommandPOS(request);
            Response response = command.response;
            if (String.IsNullOrEmpty(description.Text))
            {
                description.Text = "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
            else
            {
                description.Text += Environment.NewLine + "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
        }

        private void terminal_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            CommandS920 command = new CommandS920();
            //request.requestPOS.Amount = "010";
            //request.requestPOS.ReferenceNumber = returnRRN.Text;
            //request.requestPOS.CurrencyCode = "643";
            request.requestPOS.OperationCode = 63;
            request.requestPOS.CommandMode2 = 22;
            request.requestPOS.CommandMode = 1;
            request.requestPOS.ReferenceNumber = "211829938241";
            request.requestPOS.TerminalID = "10160942";
            command.com = 3;
            command.CommandPOS(request);
            Response response = command.response;
            if (String.IsNullOrEmpty(description.Text))
            {
                description.Text = "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
            else
            {
                description.Text += Environment.NewLine + "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
        }

        private void shortReportTerminal_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            CommandS920 command = new CommandS920();          
            request.requestPOS.OperationCode = 63;
            request.requestPOS.CommandMode2 = 20;          
            command.com = 3;
            command.CommandPOS(request);
            Response response = command.response;
            if (String.IsNullOrEmpty(description.Text))
            {
                description.Text = "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
            else
            {
                description.Text += Environment.NewLine + "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }

        }

        private void longReportTerminal_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            CommandS920 command = new CommandS920();
            request.requestPOS.OperationCode = 63;
            request.requestPOS.CommandMode2 = 21;
            command.com = 3;
            command.CommandPOS(request);
            Response response = command.response;
            if (String.IsNullOrEmpty(description.Text))
            {
                description.Text = "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
            else
            {
                description.Text += Environment.NewLine + "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
        }

        private void totalReportTerminal_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            CommandS920 command = new CommandS920();
            request.requestPOS.OperationCode = (Int32)OperationCode.TotalCommand;
            request.requestPOS.TerminalID = "10160942";
            command.com = 3;
            command.CommandPOS(request);
            Response response = command.response;
            if (String.IsNullOrEmpty(description.Text))
            {
                description.Text = "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
            else
            {
                description.Text += Environment.NewLine + "Тип операции: " + response.responsePOS.OrigOperation + " - Return, Описание: " + response.responsePOS.CommandResult + ", " + Environment.NewLine + response.responsePOS.ReceiptData;
            }
        }
    }
}
