using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Windows;
using PosBackup.FlexControls.FlexProgressBar;

namespace PosBackup.FlexControls
{
    /// <summary>
    /// Логика взаимодействия для FlexWindow.xaml
    /// </summary>
    public partial class ShopBackups
    {
        Shop shop;
        public Shop Shop { get => shop; set => shop = value; }

        public ShopBackups(Shop _shop)
        {
            InitializeComponent();
            Shop = _shop;
        }

        private void WindowName_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridBackup.ItemsSource = Shop.ListBackup;
        }

        private void DataGridBackup_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShopBackup shopBackup = DataGridBackup.SelectedItem as ShopBackup;
            if (shop != null)
            {
                GetBackup(shopBackup);
                this.Close();
            }
        }

        public void GetBackup(ShopBackup shopBackup)
        {
            FlexProgressBarData progressBarData = new FlexProgressBarData();
            progressBarData.IsIndeterminate = true;
            progressBarData.Delegate = new Action(() => {
                DelegateRestoreBackup(shopBackup);
            });
            new PosBackup.FlexControls.FlexProgressBar.FlexProgressBar(progressBarData);
        }

        public void GetMassBackup(ShopBackup shopBackup)
        {
            DelegateRestoreBackup(shopBackup);
        }

        void DelegateRestoreBackup(ShopBackup shopBackup)
        {
            String tempDir = @"C:\Temp";
            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);

            String backupFile = shopBackup.BackupName.Replace(".zip", ".bak");
            String backupFilePath = tempDir + @"\" + backupFile;

            PosBackup.Helper.Ftp.FtpClient ftpClient = new PosBackup.Helper.Ftp.FtpClient(shopBackup.BackupPath, "miuzshop", "bKwu3e0Kb+CgsdyVkPoJyQ==");
            ftpClient.DownloadFile(shopBackup.BackupName, tempDir + @"\" + shopBackup.BackupName);
            if (File.Exists(backupFilePath))
            {
                File.Delete(backupFilePath);
            }
            ZipFile.ExtractToDirectory(tempDir + @"\" + shopBackup.BackupName, tempDir);

            String requestString = String.Concat(
                @"USE [master]",
                Environment.NewLine,
                "ALTER DATABASE [JDB_POS] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                Environment.NewLine,
                @" RESTORE DATABASE [JDB_POS] FROM DISK = '" + backupFilePath + "' WITH FILE = 1, NOUNLOAD, NORECOVERY, REPLACE, STATS = 10",
                Environment.NewLine,
                @" RESTORE DATABASE [JDB_POS] WITH RECOVERY",
                Environment.NewLine,
                @" ALTER DATABASE [JDB_POS] SET MULTI_USER"
            );

            GlobalVariable.DataBaseData localGlobalDataBase = new GlobalVariable.DataBaseData();
            localGlobalDataBase._database = "JDB_POS";
            localGlobalDataBase._servername = @".\SQLEXPRESS";

            PosBackup.Sql.SqlConnectionMiuz _sqlRequestSelect = new Sql.SqlConnectionMiuz(localGlobalDataBase);
            _sqlRequestSelect.ComplexRequest(requestString, CommandType.Text, null, 0);
        }
    }
}
