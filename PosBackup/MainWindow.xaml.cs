using PosBackup.FlexControls;
using PosBackup.FlexControls.FlexProgressBar;
using PosBackup.GlobalVariable;
using PosBackup.Helper;
using PosBackup.Helper.Ftp;
using PosBackup.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PosBackup
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public class Shop
    {
        public Int32 Counter { get; set; }
        public String CompanyID { get; set; }
        public String ShopID { get; set; }
        public ShopBackup LastBackup { get; set; }
        public long LastBackupSize { get; set; }
        public String LastBackupString { get; set; }
        public DateTime LastBackupDateTime { get; set; }
        public Boolean BadShop { get; set; }
        public String CountBackup { get; set; }
        public DateTime LastActive { get; set; }
        public String Location { get; set; }
        public List<ShopBackup> ListBackup { get; set; }
    }

    public class ShopBackup
    {
        public String BackupName { get; set; }
        public String FullBackupPath { get; set; }
        public String BackupPath { get; set; }
        public long BackupSize { get; set; }
    }

    public class DataAdapter
    {
        public String _servername { get; set; }
        public String _database { get; set; }
        public String _serverlogin { get; set; }
        public String _serverpassword { get; set; }
    }

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        ObservableCollection<Shop> listShop;
        ConvertData convertData;

        DataAdapter workDataAdapter;
        DataAdapter devDataAdapter;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            convertData = new ConvertData();
            listShop = new ObservableCollection<Shop>();

            workDataAdapter = new DataAdapter();
            workDataAdapter._servername = "62.76.101.156";
            workDataAdapter._database = "MIUZ_POS";
            workDataAdapter._serverlogin = "MIUZShop";
            workDataAdapter._serverpassword = "Kexibq9Gfhjkm6LkzBlbjnf3";

            devDataAdapter = new DataAdapter();
            devDataAdapter._servername = "62.76.101.156";
            devDataAdapter._database = "MIUZ_POS_Dev";
            devDataAdapter._serverlogin = "MIUZDeveloper";
            devDataAdapter._serverpassword = "To1Jlby2Jnkbxysq3Gfhjkm4";
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = 800;
            this.Height = 600;

            TextBoxRequestString.Text = String.Concat(
                "Declare @ShopNumber int = (SELECT Value FROM [JDB_POS].[dbo].[Attributes] where Name = 'Номер магазина')"
                ,Environment.NewLine
                ,"SELECT @ShopNumber as ShopNum, [ReffID] FROM[JDB_POS].[dbo].[RelatedProductDocument]"
            );
        }

        void GetData(DataBaseData dataBaseData, DataGrid dataGrid, Boolean workServer)
        {
            FlexProgressBarData progressBarData = new FlexProgressBarData();
            progressBarData.IsIndeterminate = true;
            progressBarData.Delegate = new Action(() => {

                listShop.Clear();
                String ftpDir = "";
                if (workServer)
                {
                    ftpDir = "BackupShop";
                }
                else
                {
                    ftpDir = "BackupShopDev";
                }

                Int32 count = 1;

                FtpClient ftpClient = new FtpClient("ftp://62.76.101.133:2121/"+ ftpDir + "/", "miuzshop", "bKwu3e0Kb+CgsdyVkPoJyQ==");
                listShop = ftpClient.ListDirectoryShop();
                foreach (Shop shop in listShop)
                {
                    String pathToDir = "ftp://62.76.101.133:2121/" + ftpDir + "/" + shop.CompanyID + "_" + shop.ShopID + "/";
                    ftpClient = new FtpClient(pathToDir, "miuzshop", "bKwu3e0Kb+CgsdyVkPoJyQ==");

                    String[] listBackup = ftpClient.ListDirectory();
                    if (listBackup.Count() > 0)
                    {
                        shop.ListBackup = new List<ShopBackup>();
                        foreach (String nameBackup in listBackup)
                        {
                            ShopBackup shopBackup = new ShopBackup();
                            shopBackup.BackupName = nameBackup;
                            shopBackup.FullBackupPath = String.Concat(pathToDir, nameBackup);
                            shopBackup.BackupPath = pathToDir;

                            FtpClient ftpClientSize = new FtpClient(shopBackup.BackupPath, "miuzshop", "bKwu3e0Kb+CgsdyVkPoJyQ==");
                            shopBackup.BackupSize = ftpClientSize.GetFileSize(shopBackup.BackupName);

                            shop.ListBackup.Add(shopBackup);
                        }

                        shop.CountBackup = listBackup.Count().ToString();
                        shop.LastBackup = shop.ListBackup[shop.ListBackup.Count() - 1];
                        shop.LastBackupString = listBackup[listBackup.Length - 1];
                        shop.LastBackupSize = shop.LastBackup.BackupSize;
                        Int32 yearString = Int32.Parse(shop.LastBackup.BackupName.Substring(20, 4));
                        Int32 montString = Int32.Parse(shop.LastBackup.BackupName.Substring(24, 2));
                        Int32 dayString = Int32.Parse(shop.LastBackup.BackupName.Substring(26, 2));
                        shop.LastBackupDateTime = new DateTime(yearString, montString, dayString);
                        if (shop.LastBackupDateTime.ToShortDateString() != DateTime.Now.ToShortDateString())
                        {
                            shop.BadShop = true;
                        }
                    }
                    else
                    {
                        shop.CountBackup = "0";
                        shop.LastBackupString = "Nothing";
                        shop.LastBackupDateTime = DateTime.Now;
                        shop.BadShop = true;
                    }

                    shop.Counter = count;

                    //Для тестирования
                    //if (testsCount > 1)
                    //{
                    //    break;
                    //}
                    count++;
                }

                SqlConnectionMiuz sqlConnectionMiuz = new SqlConnectionMiuz(dataBaseData, false);
                String sqlRequest = "SELECT DISTINCT(uah_outer.[ShopID]) as [ShopID],(select MAX(uah_inner.[CreatedDate]) FROM " + dataBaseData._database +
                    ".[dbo].[UserActiveHistory] as uah_inner where uah_inner.[ShopID] = uah_outer.[ShopID]) as LastActive,(SELECT TOP 1 [Description] FROM " + dataBaseData._database +
                    ".[dbo].[Shop] where [ShopNumber] = uah_outer.[ShopID]) as Location FROM " + dataBaseData._database +
                    ".[dbo].[UserActiveHistory] as uah_outer order by uah_outer.ShopID asc";
                DataTable dataTable = new DataTable();
                sqlConnectionMiuz.ComplexRequest(sqlRequest, CommandType.Text, null);
                dataTable = sqlConnectionMiuz.SqlAnswer.datatable;
                foreach (DataRow row in dataTable.Rows)
                {
                    Shop shop = listShop.FirstOrDefault<Shop>(x => x.ShopID == row[0].ToString().Trim());
                    if (shop != null)
                    {
                        shop.LastActive = (DateTime)convertData.FlexConvertDataDateTime(row[1].ToString());
                        shop.Location = row[2].ToString();
                    }
                }

            });
            new PosBackup.FlexControls.FlexProgressBar.FlexProgressBar(progressBarData);
            DataGridShop.ItemsSource = listShop;
        }

        private void ButtonGetFilesWorkFtp_Click(object sender, RoutedEventArgs e)
        {
            DataBaseData dataBaseData = new DataBaseData();
            dataBaseData._servername = workDataAdapter._servername;
            dataBaseData._database = workDataAdapter._database;
            dataBaseData._serverlogin = workDataAdapter._serverlogin;
            dataBaseData._serverpassword = workDataAdapter._serverpassword;
            GetData(dataBaseData, DataGridShop, true);
        }

        private void ButtonGetFilesDevFtp_Click(object sender, RoutedEventArgs e)
        {
            DataBaseData dataBaseData = new DataBaseData();
            dataBaseData._servername = devDataAdapter._servername;
            dataBaseData._database = devDataAdapter._database;
            dataBaseData._serverlogin = devDataAdapter._serverlogin;
            dataBaseData._serverpassword = devDataAdapter._serverpassword;
            GetData(dataBaseData, DataGridShop, false);
        }

        private void TextBox_Status_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                textBox_Status.Focus();
                textBox_Status.Select(textBox_Status.Text.Length, 0);
            }));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            textBox_Status.Text = "";
        }

        private void DataGridShop_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Shop shop = DataGridShop.SelectedItem as Shop;
            if (shop!=null)
            {
                ShopBackups shopBackups = new ShopBackups(shop);
                shopBackups.ShowDialog();
            }
        }

        private void ButtonGetBackups_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Shop> listShopProcess = new ObservableCollection<Shop>();
            foreach (Shop shop in DataGridShop.SelectedItems)
            {
                listShopProcess.Add(shop);
            }

            String textBoxRequestString = TextBoxRequestString.Text;
            String collectString = "";

            FlexProgressBarData progressBarData = new FlexProgressBarData();
            progressBarData.IsIndeterminate = true;
            progressBarData.Delegate = new Action(() => {
                foreach (Shop shop in listShopProcess)
                {
                    if (shop.ListBackup.Count > 0)
                    {
                        ShopBackups shopBackups = new ShopBackups(shop);
                        shopBackups.GetMassBackup(shop.ListBackup[shop.ListBackup.Count - 1]);

                        String requestString = String.Concat(
                            @"USE [JDB_POS]",
                            Environment.NewLine,
                            textBoxRequestString
                        );

                        GlobalVariable.DataBaseData localGlobalDataBase = new GlobalVariable.DataBaseData();
                        localGlobalDataBase._database = "JDB_POS";
                        localGlobalDataBase._servername = @".\SQLEXPRESS";
                        PosBackup.Sql.SqlConnectionMiuz _sqlRequestSelect = new Sql.SqlConnectionMiuz(localGlobalDataBase);
                    
                        _sqlRequestSelect.ComplexRequest(requestString, CommandType.Text, null, 0);
                        DataTable _data = _sqlRequestSelect.SqlAnswer.datatable;

                        String rowString = "";
                        foreach (DataRow row in _data.Rows)
                        {
                            rowString = "";
                            foreach (DataColumn dataColumn in _data.Columns)
                            {
                                rowString = String.Concat(rowString, " | ", row[dataColumn].ToString());
                            }
                            collectString = String.Concat(collectString, rowString, Environment.NewLine);
                        }
                    }
                }
            });
            new PosBackup.FlexControls.FlexProgressBar.FlexProgressBar(progressBarData);

            textBox_Status.Text = collectString;
        }

        private void ButtonDeleteBackups_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

// Получение всех сопутствующих документов которых нет в магазинах
//Declare @ShopNumber int = (SELECT Value FROM [JDB_POS].[dbo].[Attributes] where Name = 'Номер магазина')
//DECLARE @LastActiveDate datetime
//SELECT  @LastActiveDate = MAX(createddate)  FROM[DataAdapter].Miuz_POS.dbo.UserActiveHistory
//WHERE ShopID = @ShopNumber
//select @ShopNumber as ShopNumber, [TransferDocumentNumber], CreatedDate, @LastActiveDate AS LastActiveDate from [DataAdapter].Miuz_POS.dbo.[TransferRelatedProductDocument]
//where[ReceiverID] = @ShopNumber
//AND[TransferDocumentNumber] NOT IN
//(
//SELECT[ReffID] FROM[JDB_POS].[dbo].[RelatedProductDocument]
//)
//AND CAST(createddate AS DATE) < DATEADD(day, -14, CAST(GETDATE() AS DATE))

// Получение остатков из магазинов
//Declare @ShopNumber int = (SELECT Value FROM [JDB_POS].[dbo].[Attributes] where Name = 'Номер магазина')
//Declare @Date datetime = (SELECT MAX([CreatedDate]) FROM[JDB_POS].[dbo].[UserActiveHistory])
//INSERT INTO MyDb.dbo.ProductTable(ShopNum, ID, TagPriceWithVat,[Date])
//SELECT @ShopNumber as ShopNum, [ID],[TagPriceWithVat], @Date as [Date] FROM[JDB_POS].[dbo].[Product] WHERE Status in (0,2)
