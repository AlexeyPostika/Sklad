using Sklad_v1_001.GlobalVariable;
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

namespace Sklad_v1_001.FormUsers.Delivery
{
    /// <summary>
    /// Логика взаимодействия для NewDeliveryItem.xaml
    /// </summary>
    public partial class NewDeliveryItem : Page
    {
        private DeliveryLogic deliveryLogic;
        private LocaleRow localeRow;
        FileWork fileWork;

        public DeliveryLogic DeliveryLogic { get => deliveryLogic; set => deliveryLogic = value; }
        public LocaleRow LocaleRow { get => localeRow; set => localeRow = value; }

        public NewDeliveryItem()
        {
            InitializeComponent();
            DeliveryLogic = new DeliveryLogic();
            LocaleRow = new LocaleRow();

            this.delivery.DataContext = this;
            DataContext = this;
        }
        #region Load Invoice
        private async void Invoice_ButtonAddClick()
        {
            fileWork = new FileWork();
            fileWork.OpenPDFtoImage();
            fileWork = await LoadInvoiceAsync(fileWork);
            if (fileWork.Source!=null)
            {
                LocaleRow.InvoiceDocumentByte = fileWork.BufferDocument;
                this.Invoice.Source = ImageHelper.GenerateImage("IconClose.png");
            }
            else
                this.Invoice.Source = ImageHelper.GenerateImage("IconAddProduct.png");
        }

        #endregion

        #region TTN
        private async void TTN_ButtonAddClick()
        {
            fileWork = new FileWork();
            fileWork.OpenPDFtoImage();
            fileWork = await LoadInvoiceAsync(fileWork);
            if (fileWork.Source != null)
            {
                LocaleRow.TTNDocumentByte = fileWork.BufferDocument;
                this.TTN.Source = ImageHelper.GenerateImage("IconClose.png");
            }
            else
                this.TTN.Source = ImageHelper.GenerateImage("IconAddProduct.png");
        }
        #endregion

        #region загрузка данных
        static async Task<FileWork> LoadInvoiceAsync(FileWork _fileWork)
        {
            return await Task.Run(() => LoadInvoice(_fileWork));
        }

        static FileWork LoadInvoice(FileWork _fileWork)
        {
            _fileWork.PDFTo();
            return _fileWork;
        }
        #endregion
    }
}
