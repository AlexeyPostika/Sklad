using Sklad_v1_001.FormUsers.Category;
using Sklad_v1_001.FormUsers.CategoryDetails;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.DeliveryDetails;
using Sklad_v1_001.FormUsers.Manufacturer;
using Sklad_v1_001.FormUsers.ShowCase;
using Sklad_v1_001.FormUsers.Userss;
using Sklad_v1_001.GlobalList;
using Sklad_v1_001.HelperGlobal;
using Sklad_v1_001.SQLCommand;
using Sklad_v1_001.SQLCommand.ShemaStorаgeTableAdapters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.GlobalAttributes
{
    public class users
    {
        public Int32 ID { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public Int32 RoleID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String SecondName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public Boolean Active { get; set; }
        public String ShortName { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
    public class Attributes
    {
        ConvertData convertData;
        //работа с схемой БД 
        ShemaStorаge shemaStorage;
        GetCategoryTableTableAdapter getCategoryTableTableAdapter;
        GetDeliveryCompanyTableAdapter getDeliveryCompanyTableAdapter;        

        //Объекты
        //категории 
        CategoryLogic categoryLogic;
        CategoryDetailsLigic categoryDetails;
        public ObservableCollection<Category> datalistCategory;
        public ObservableCollection<CategoryDetails> datalistCategoryDetails;

        //Объекты
        //Витрины
        ShowCaseLogic showcaseLogic;
        public ObservableCollection<FormUsers.ShowCase.LocaleRow> datalistShowCase;

        //Объекты
        //Производитель
        ManufacturerLogic manufacturerLogic;
        public ObservableCollection<FormUsers.Manufacturer.LocaleRow> datalistManufacturer;

        //Delivery and DeliveryDetails
        DeliveryLogic deliverLogic;
        DeliveryDetailsLogic deliveryDetailsLogic;
        public ObservableCollection<DeliveryCompany> datalistDeliveryCompany;
        public ObservableCollection<DeliveryCompanyDetails> datalistDeliveryDetailsCompany;

        //работаем с пользователями
        public ObservableCollection<users> datalistUsers;
        public Attributes()
        {
            shemaStorage = new ShemaStorаge();
            getCategoryTableTableAdapter = new GetCategoryTableTableAdapter();
            getDeliveryCompanyTableAdapter = new GetDeliveryCompanyTableAdapter();          

            categoryLogic = new CategoryLogic();
            categoryDetails = new CategoryDetailsLigic();
            datalistCategory = new ObservableCollection<Category>();
            datalistCategoryDetails = new ObservableCollection<CategoryDetails>();
            FillCategory();
            FillCategoryDetails();

            deliverLogic = new DeliveryLogic();
            deliveryDetailsLogic = new DeliveryDetailsLogic();
            datalistDeliveryCompany = new ObservableCollection<DeliveryCompany>();
            datalistDeliveryDetailsCompany = new ObservableCollection<DeliveryCompanyDetails>();
            FillDeliverycompany();
            FillDeliveryCompanyDetails();

            //загрузка витрин          
            datalistShowCase = new ObservableCollection<FormUsers.ShowCase.LocaleRow>();
            FillShowCase();

            //загрузка производителей
            datalistManufacturer = new ObservableCollection<FormUsers.Manufacturer.LocaleRow>();
            FillManufacturer();

            //загружаем пользователей
            datalistUsers = new ObservableCollection<users>();
            FillUsers();

        }

        //заполним Category и CategoryDetails
        public void FillCategory()
        {
            datalistCategory.Clear();
            getCategoryTableTableAdapter.FillCategoryTable(shemaStorage.GetCategoryTable, "grid", String.Empty);
            //getCategoryTableTableAdapter.GetCategoryTable("Grid", "");
            foreach(DataRow row in shemaStorage.GetCategoryTable)
            {
                datalistCategory.Add(categoryLogic.ConvertCategory(row, new Category()));
            }
        }
        public void FillCategoryDetails()
        {
            datalistCategoryDetails.Clear();
            getCategoryTableTableAdapter.FillCategoryTable(shemaStorage.GetCategoryTable, "ingrid", String.Empty);
            //getCategoryTableTableAdapter.GetCategoryTable("Grid", "");
            foreach (DataRow row in shemaStorage.GetCategoryTable)
            {
                datalistCategoryDetails.Add(categoryDetails.ConvertCategoryDetails(row, new CategoryDetails()));
            }
        }

        //заполним Delivery и DeliveryDetails
        public void FillDeliverycompany()
        {
            datalistDeliveryCompany.Clear();
            getDeliveryCompanyTableAdapter.FillDeliveryCompany(shemaStorage.GetDeliveryCompanyTable, "Grid");
            
            foreach (DataRow row in shemaStorage.GetDeliveryCompanyTable)
            {
                datalistDeliveryCompany.Add(deliverLogic.ConvertDelivery(row, new DeliveryCompany()));            
            }
        }
        public void FillDeliveryCompanyDetails()
        {
            datalistDeliveryDetailsCompany.Clear();
            getDeliveryCompanyTableAdapter.FillDeliveryCompany(shemaStorage.GetDeliveryCompanyTable, "ingrid");            
            foreach (DataRow row in shemaStorage.GetDeliveryCompanyTable)
            {
                datalistDeliveryDetailsCompany.Add(deliveryDetailsLogic.ConvertDeliveryDetails(row, new DeliveryCompanyDetails()));
            }
        }
        public void FillShowCase()
        {
            showcaseLogic = new ShowCaseLogic();
            FormUsers.ShowCase.LocalFilter localFilter = new FormUsers.ShowCase.LocalFilter();
            localFilter.IsActive = "1"; // только активные витрины
            DataTable dataTable = showcaseLogic.FillGrid(localFilter);
            foreach (DataRow row in dataTable.Rows)
            {
                datalistShowCase.Add(showcaseLogic.ConvertComboBox(row, new FormUsers.ShowCase.LocaleRow()));
            }
        }

        public void FillManufacturer()
        {
            manufacturerLogic = new ManufacturerLogic();
            FormUsers.Manufacturer.LocalFilter localFilter = new FormUsers.Manufacturer.LocalFilter();
            localFilter.IsActive = "1"; // только активные витрины
            DataTable dataTable = manufacturerLogic.FillGrid(localFilter);
            foreach (DataRow row in dataTable.Rows)
            {
                datalistManufacturer.Add(manufacturerLogic.ConvertComboBox(row, new FormUsers.Manufacturer.LocaleRow()));
            }
        }

        public void FillUsers()
        {
            UserListLogic userListLogic = new UserListLogic();
            DataTable dataTable = userListLogic.FillGrid();
            foreach (DataRow row in dataTable.Rows)
            {
                datalistUsers.Add(userListLogic.ConvertToUsers(row, new users()));
            }
        }
    }
}
