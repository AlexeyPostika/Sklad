using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.GlobalList;
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
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.FormUsers.SupplyDocumentPayment
{
    /// <summary>
    /// Логика взаимодействия для NewAddProductItem.xaml
    /// </summary>
    public partial class NewSupplyDocumentPaymentItem : Page
    {
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewSupplyDocumentPaymentItem), new PropertyMetadata(MessageBoxResult.Cancel));
        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }
        LocaleRow paymentLocalRow;
        public LocaleRow PaymentLocalRow { get => paymentLocalRow; set => paymentLocalRow = value; }

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

            if (String.IsNullOrEmpty(PaymentLocalRow.OpertionTypeString))
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

            if (String.IsNullOrEmpty(PaymentLocalRow.StatusString))
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

        }

        private void RRN_ButtonLoopClick()
        {

        }

        private void RRN_ButtonClearClick()
        {

        }
    }
}
