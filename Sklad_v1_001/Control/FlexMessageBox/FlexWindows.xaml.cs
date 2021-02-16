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
using System.Windows.Shapes;

namespace Sklad_v1_001.Control.FlexMessageBox
{
    /// <summary>
    /// Логика взаимодействия для FlexWindows.xaml
    /// </summary>
    public partial class FlexWindows : Window
    {
        public FlexWindows(string title)
        {
            InitializeComponent();
            this.Title = title;
        }

        private void WindowName_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Window window = sender as Window;
            //if (MainWindow.AppWindow.DataChanged.Count > 0)
            //{
            //    if (MainWindow.AppWindow.DataChanged.ContainsKey(window.Content.ToString()))
            //    {
            //        if (MainWindow.AppWindow.DataChanged[window.Content.ToString()] == true)
            //        {
            //            POS.FlexControl.FlexMessageBox.FlexMessageBox mb = new POS.FlexControl.FlexMessageBox.FlexMessageBox();
            //            if (mb.Show(Properties.Resources.WarningClose, Properties.Resources.WarningCloseTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning, 2) == MessageBoxResult.Yes)
            //            {
            //                MainWindow.AppWindow.DataChanged[window.Content.ToString()] = false;
            //                e.Cancel = false;
            //            }
            //            else
            //                e.Cancel = true;
            //        }
            //    }
            //    else
            //    {
            //        MainWindow.AppWindow.DataChanged.Add(window.Content.ToString(), false);
                    e.Cancel = false;
            //    }
            //}
        }
    }
}
