using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal.StoreAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Sklad_v1_001.FormUsers.Company
{
    /// <summary>
    /// Логика взаимодействия для NewCompanyGrid.xaml
    /// </summary>
    public partial class NewCompanyGrid : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        Attributes attributes;
        LocaleRow localeRowCompany;

        Request request;
        public LocaleRow LocaleRowCompany
        {
            get
            {
                return localeRowCompany;
            }

            set
            {
                localeRowCompany = value;
                OnPropertyChanged("LocaleRowCompany");
            }
        }
        public NewCompanyGrid(Attributes _attributes)
        {
            InitializeComponent();
            LocaleRowCompany = new LocaleRow();

            this.attributes = _attributes;
            this.DataContext = LocaleRowCompany;
        }
        #region ToolBar
        private void toolbarCompany_ButtonSave()
        {

        }

        private void toolbarCompany_ButtonSaveclose()
        {

        }

        private void toolbarCompany_ButtonListCancel()
        {

        }

        private void toolbarCompany_ButtonApply()
        {
            request = new Request(attributes);
            request.companyRequest.company.active = LocaleRowCompany.Active;
            request.companyRequest.company.addUserID = attributes.numeric.userEdit.AddUserID;
            request.companyRequest.company.adress = LocaleRowCompany.Adress;
            request.companyRequest.company.bancAdress = LocaleRowCompany.BancAdress;
            request.companyRequest.company.bancName = LocaleRowCompany.BancName;
            request.companyRequest.company.correspondentAccount = LocaleRowCompany.CorrespondentAccount;
            request.companyRequest.company.currentCode = LocaleRowCompany.CurrentCode;
            request.companyRequest.company.currentName = LocaleRowCompany.CurrentName;
            //request.companyRequest.company.description = LocaleRowCompany.Description;
            request.companyRequest.company.fullCompanyName = LocaleRowCompany.FullCompanyName;
            request.companyRequest.company.generalDirectory.active = LocaleRowCompany.GeneralDirectory.Active;
            request.companyRequest.company.generalDirectory.birthday = LocaleRowCompany.GeneralDirectory.Birthday;
            //request.companyRequest.company.generalDirectory.companyID = LocaleRowCompany.GeneralDirectory.CompanyID;
            request.companyRequest.company.generalDirectory.createdByUserID = LocaleRowCompany.CreatedByUserID;
            request.companyRequest.company.generalDirectory.createdDate = LocaleRowCompany.CreatedDate;
            request.companyRequest.company.generalDirectory.createdDateString = LocaleRowCompany.CreatedDateString;
            request.companyRequest.company.generalDirectory.description = LocaleRowCompany.GeneralDirectory.Description;
            request.companyRequest.company.generalDirectory.email = LocaleRowCompany.GeneralDirectory.Email;
            request.companyRequest.company.generalDirectory.firstName = LocaleRowCompany.GeneralDirectory.FirstName;
            request.companyRequest.company.generalDirectory.genderID = LocaleRowCompany.GeneralDirectory.GenderID;
            request.companyRequest.company.generalDirectory.iD = LocaleRowCompany.GeneralDirectory.ID;
            request.companyRequest.company.generalDirectory.iNN = LocaleRowCompany.GeneralDirectory.INN;
            request.companyRequest.company.generalDirectory.lastModifiedByUserID = LocaleRowCompany.GeneralDirectory.LastModifiedByUserID;
            request.companyRequest.company.generalDirectory.lastModifiedDate = LocaleRowCompany.GeneralDirectory.LastModifiedDate;
            request.companyRequest.company.generalDirectory.lastModifiedDateString = LocaleRowCompany.GeneralDirectory.LastModifiedDateString;
            request.companyRequest.company.generalDirectory.lastName = LocaleRowCompany.GeneralDirectory.LastName;
            request.companyRequest.company.generalDirectory.login = LocaleRowCompany.GeneralDirectory.Login;
            request.companyRequest.company.generalDirectory.number = LocaleRowCompany.GeneralDirectory.Number;
            request.companyRequest.company.generalDirectory.password = LocaleRowCompany.GeneralDirectory.Password;
            request.companyRequest.company.generalDirectory.phone = LocaleRowCompany.GeneralDirectory.Phone;
            request.companyRequest.company.generalDirectory.roleID = LocaleRowCompany.GeneralDirectory.RoleID;
            request.companyRequest.company.generalDirectory.secondName = LocaleRowCompany.GeneralDirectory.SecondName;
            request.companyRequest.company.generalDirectory.userID = LocaleRowCompany.GeneralDirectory.UserID;          
            request.companyRequest.company.iD = LocaleRowCompany.ID;
            request.companyRequest.company.iNN = LocaleRowCompany.INN;
            request.companyRequest.company.kPP = LocaleRowCompany.KPP;
            request.companyRequest.company.logo = LocaleRowCompany.Logo;
            request.companyRequest.company.phone = LocaleRowCompany.Phone;
            request.companyRequest.company.rCBIC = LocaleRowCompany.RCBIC;
           //request.companyRequest.company.seniorAccount = LocaleRowCompany.SeniorAccount;
            request.companyRequest.company.seniorAccount.active = LocaleRowCompany.SeniorAccount.Active;
            request.companyRequest.company.seniorAccount.birthday = LocaleRowCompany.SeniorAccount.Birthday;
            //request.companyRequest.company.generalDirectory.companyID = LocaleRowCompany.GeneralDirectory.CompanyID;
            request.companyRequest.company.seniorAccount.createdByUserID = LocaleRowCompany.SeniorAccount.CreatedByUserID;
            request.companyRequest.company.seniorAccount.createdDate = LocaleRowCompany.SeniorAccount.CreatedDate;
            request.companyRequest.company.seniorAccount.createdDateString = LocaleRowCompany.SeniorAccount.CreatedDateString;
            request.companyRequest.company.seniorAccount.description = LocaleRowCompany.SeniorAccount.Description;
            request.companyRequest.company.seniorAccount.email = LocaleRowCompany.SeniorAccount.Email;
            request.companyRequest.company.seniorAccount.firstName = LocaleRowCompany.SeniorAccount.FirstName;
            request.companyRequest.company.seniorAccount.genderID = LocaleRowCompany.SeniorAccount.GenderID;
            request.companyRequest.company.seniorAccount.iD = LocaleRowCompany.SeniorAccount.ID;
            request.companyRequest.company.seniorAccount.iNN = LocaleRowCompany.SeniorAccount.INN;
            request.companyRequest.company.seniorAccount.lastModifiedByUserID = LocaleRowCompany.SeniorAccount.LastModifiedByUserID;
            request.companyRequest.company.seniorAccount.lastModifiedDate = LocaleRowCompany.SeniorAccount.LastModifiedDate;
            request.companyRequest.company.seniorAccount.lastModifiedDateString = LocaleRowCompany.SeniorAccount.LastModifiedDateString;
            request.companyRequest.company.seniorAccount.lastName = LocaleRowCompany.SeniorAccount.LastName;
            request.companyRequest.company.seniorAccount.login = LocaleRowCompany.SeniorAccount.Login;
            request.companyRequest.company.seniorAccount.number = LocaleRowCompany.SeniorAccount.Number;
            request.companyRequest.company.seniorAccount.password = LocaleRowCompany.SeniorAccount.Password;
            request.companyRequest.company.seniorAccount.phone = LocaleRowCompany.SeniorAccount.Phone;
            request.companyRequest.company.seniorAccount.roleID = LocaleRowCompany.SeniorAccount.RoleID;
            request.companyRequest.company.seniorAccount.secondName = LocaleRowCompany.SeniorAccount.SecondName;
            request.companyRequest.company.seniorAccount.userID = LocaleRowCompany.SeniorAccount.UserID;
            request.companyRequest.company.senttlementAccount = LocaleRowCompany.SenttlementAccount;
            request.companyRequest.company.shop.address = LocaleRowCompany.Shop.Address;
            request.companyRequest.company.shop.addUserID = attributes.numeric.userEdit.AddUserID;
            request.companyRequest.company.shop.companyID = LocaleRowCompany.Shop.CompanyID;
            request.companyRequest.company.shop.createdUserID = LocaleRowCompany.Shop.CreatedByUserID;
            request.companyRequest.company.shop.iD = LocaleRowCompany.Shop.ID;
            request.companyRequest.company.shop.lastModificatedDate = LocaleRowCompany.Shop.LastModifiedDate;
            request.companyRequest.company.shop.lastModificatedUserID = LocaleRowCompany.Shop.LastModifiedByUserID;
            request.companyRequest.company.shop.Name = LocaleRowCompany.Shop.Name;
            request.companyRequest.company.shop.phone = LocaleRowCompany.Shop.Phone;
            request.companyRequest.company.shop.shopNumber = LocaleRowCompany.Shop.ShopNumber;
            request.companyRequest.company.shop.TimeRow = LocaleRowCompany.Shop.TimeRow;            
            request.companyRequest.company.shortCompanyName = LocaleRowCompany.ShortCompanyName;
            request.companyRequest.company.taxRate = LocaleRowCompany.TaxRate;
            //request.companyRequest.company.TimeRow = LocaleRowCompany.RowTime;          
            Response response = request.GetCommand(4);
            if (response != null && response.ErrorCode == 0)
            {
                //Document.Status = response.SupplyDocumentOutput.Document.Status;
                //Document.ReffID = 0;
                //Document.ReffDate = response.SupplyDocumentOutput.Document.SyncDate;
                //if (SaveRequest() == 0)
                //{
                //}
            }
        }
        #endregion
    }
}
