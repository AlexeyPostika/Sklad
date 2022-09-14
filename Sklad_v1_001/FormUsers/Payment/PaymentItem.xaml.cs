using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.GlobalList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.FormUsers.SupplyDocumentPayment
{
    /// <summary>
    /// Логика взаимодействия для NewAddProductItem.xaml
    /// </summary>
    public partial class NewSupplyDocumentPaymentItem : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewSupplyDocumentPaymentItem), new PropertyMetadata(MessageBoxResult.Cancel));
        //AmountMax
        public static readonly DependencyProperty AmountMaxProperty = DependencyProperty.Register(
                   "AmountMax",
                   typeof(Double),
                  typeof(NewSupplyDocumentPaymentItem));

        public static readonly DependencyProperty StatusDocumentProperty = DependencyProperty.Register(
                    "StatusDocument",
                    typeof(Boolean),
                   typeof(NewSupplyDocumentPaymentItem), new PropertyMetadata(false));

        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }

        public Double AmountMax
        {
            get { return (Double)GetValue(AmountMaxProperty); }
            set { SetValue(AmountMaxProperty, value); }
        }

        public Boolean StatusDocument
        {
            get { return (Boolean)GetValue(StatusDocumentProperty); }
            set { SetValue(StatusDocumentProperty, value); }
        }

        LocaleRow paymentLocalRow;
        public LocaleRow PaymentLocalRow
        {
            get
            {
                return paymentLocalRow;
            }

            set
            {
                paymentLocalRow = value;

                Product.IsEnabled = StatusDocument;
                this.OK.IsEnabled = StatusDocument;
                this.Cancel.IsEnabled = StatusDocument;

                this.Product.DataContext = PaymentLocalRow;
                OnPropertyChanged("ProductLocalRow");
            }
        }

        public NewSupplyDocumentPaymentItem()
        {
            InitializeComponent();
            
            OperationTypeTypeList operationTypeTypeList = new OperationTypeTypeList();
            PaymentTypeList paymentTypeList = new PaymentTypeList();

            PaymentLocalRow = new LocaleRow();
            
            OperationTypeName.ComboBoxElement.ItemsSource = operationTypeTypeList.innerList;
            OperationTypeName.ComboBoxElement.SelectedValue = operationTypeTypeList.innerList.First().ID;
            
            StatusName.ComboBoxElement.ItemsSource = paymentTypeList.innerList;
            StatusName.ComboBoxElement.SelectedValue = paymentTypeList.innerList.First().ID;

            this.Product.DataContext = PaymentLocalRow;
        }

        private void ToolBarButton_ButtonClick()
        {
            if (FieldVerify())
            {
                IsClickButtonOK = MessageBoxResult.OK;
                Window win = Parent as Window;
                win.Close();
            }
        }
        private Boolean FieldVerify()
        {
            FlexMessageBox mb;

            if (String.IsNullOrEmpty(PaymentLocalRow.Amount.ToString()))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, AmounPayment.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                AmounPayment.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(PaymentLocalRow.OpertionType.ToString()))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, OperationTypeName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                OperationTypeName.ComboBoxElement.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(PaymentLocalRow.Description))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, DescriptionPayment.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                DescriptionPayment.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(PaymentLocalRow.RRN))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, RRN.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                RRN.TextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(PaymentLocalRow.Status.ToString()))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, StatusName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                StatusName.ComboBoxElement.Focus();
                return false;
            }           
            return true;
        }

        private void RRN_ButtonAddClick()
        {
            PaymentLocalRow.RRNDocumentByte = this.RRN.ByteFaile;
        }

        private void RRN_ButtonLoopClick()
        {

        }

        private void RRN_ButtonClearClick()
        {

        }
    }
}
