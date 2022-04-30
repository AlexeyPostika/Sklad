using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.GlobalAttributes;
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

namespace Sklad_v1_001.FormUsers.Delivery
{
    /// <summary>
    /// Логика взаимодействия для NewDeliveryItem.xaml
    /// </summary>
    public partial class NewDeliveryItem : Page, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewDeliveryItem), new PropertyMetadata(MessageBoxResult.Cancel));
        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }



        Attributes attributes;

        GlobalList.DeliveryCompany deliveryCompanyRow;
        DeliveryLogic deliveryLogic;

        public GlobalList.DeliveryCompany DeliveryCompanyRow
        {
            get
            {
                return deliveryCompanyRow;
            }

            set
            {
                deliveryCompanyRow = value;
                this.DataContext = value;
                OnPropertyChanged("DeliveryCompanyRow");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewDeliveryItem(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            DeliveryCompanyRow = new GlobalList.DeliveryCompany();
            deliveryLogic = new DeliveryLogic(attributes);

            this.control.DataContext = DeliveryCompanyRow;
        }
        
        #region OKCancel   
        private void OK_ButtonClick()
        {
            if (FieldVerify())
            {
                DeliveryCompanyRow.ID = deliveryLogic.SaveRow(DeliveryCompanyRow);
                if (DeliveryCompanyRow.ID > 0)
                {
                    attributes.FillDeliverycompany();//обновить категории
                    IsClickButtonOK = MessageBoxResult.OK;
                    Window win = Parent as Window;
                    win.Close();
                }
            }
        }
        private void Cancel_ButtonClick()
        {          
            IsClickButtonOK = MessageBoxResult.Cancel;
            Window win = Parent as Window;
            win.Close();
        }
        #endregion

        private Boolean FieldVerify()
        {
            FlexMessageBox mb;

            if (String.IsNullOrEmpty(DeliveryCompanyRow.Description))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, NameCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                NameCompany.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryCompanyRow.Phones))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesCompany.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryCompanyRow.AdressCompany))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, AdressCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                AdressCompany.DescriptionInfo.Focus();
                return false;
            }
            return true;
        }

        
    }
}
