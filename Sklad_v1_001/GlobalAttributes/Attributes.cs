using Sklad_v1_001.FormUsers.Category;
using Sklad_v1_001.FormUsers.CategoryDetails;
using Sklad_v1_001.FormUsers.Delivery;
using Sklad_v1_001.FormUsers.DeliveryDetails;
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


        //Delivery and DeliveryDetails
        DeliveryLogic deliverLogic;
        DeliveryDetailsLogic deliveryDetailsLogic;
        public ObservableCollection<DeliveryCompany> datalistDeliveryCompany;
        public ObservableCollection<DeliveryCompanyDetails> datalistDeliveryDetailsCompany;

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
    }
}
