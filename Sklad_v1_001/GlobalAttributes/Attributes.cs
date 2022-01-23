using Sklad_v1_001.FormUsers.Category;
using Sklad_v1_001.FormUsers.CategoryDetails;
using Sklad_v1_001.FormUsers.Delivery;
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
        GetDeliveryCompanyTableTableAdapter getDeliveryCompanyTableTableAdapter;        

        //Объекты
        //категории 
        CategoryLogic categoryLogic;
        CategoryDetailsLigic categoryDetails;
        public ObservableCollection<Category> datalistCategory;
        public ObservableCollection<CategoryDetails> datalistCategoryDetails;


        //Delivery and DeliveryDetails
        DeliveryLogic deliverLogic;
        public ManagerDeliveryList managerDeliveryList;
        public ObservableCollection<FormUsers.Delivery.LocaleRow> datalistDelivery;

        public Attributes()
        {
            shemaStorage = new ShemaStorаge();
            getCategoryTableTableAdapter = new GetCategoryTableTableAdapter();
            getDeliveryCompanyTableTableAdapter = new GetDeliveryCompanyTableTableAdapter();          

            categoryLogic = new CategoryLogic();
            categoryDetails = new CategoryDetailsLigic();
            datalistCategory = new ObservableCollection<Category>();
            datalistCategoryDetails = new ObservableCollection<CategoryDetails>();
            FillCategory();
            FillCategoryDetails();

            deliverLogic = new DeliveryLogic();
            datalistDelivery = new ObservableCollection<FormUsers.Delivery.LocaleRow>();
            managerDeliveryList = new ManagerDeliveryList();
            FillDelivery();
            FillDeliveryGrid();

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
        private void FillDelivery()
        {
            datalistDelivery.Clear();
            getDeliveryCompanyTableTableAdapter.FillDeliveryCompanyTable(shemaStorage.GetDeliveryCompanyTable, "ingrid");
            //getCategoryTableTableAdapter.GetCategoryTable("Grid", "");
            foreach (DataRow row in shemaStorage.GetDeliveryCompanyTable)
            {
                datalistDelivery.Add(deliverLogic.Convert(row, new LocaleRow()));            
            }
        }
        private void FillDeliveryGrid()
        {
            managerDeliveryList.innerList.Clear();
            getDeliveryCompanyTableTableAdapter.FillDeliveryCompanyTable(shemaStorage.GetDeliveryCompanyTable, "Grid");
            //getCategoryTableTableAdapter.GetCategoryTable("Grid", "");
            foreach (DataRow row in shemaStorage.GetDeliveryCompanyTable)
            {               
                managerDeliveryList.innerList.Add(deliverLogic.ConvertDelivery(row, new ManagerDelivery()));
            }
        }
    }
}
