using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sklad_v1_001.Control
{
    public class DialogWindow : Window
    {
        public DialogWindow()
        {
            // это что то фиксило(возможно выход из системы)
            Thread thread = Thread.CurrentThread;
            if (thread.ManagedThreadId <= 1)
            {
                try
                {
                    this.Owner = MainWindow.AppWindow;
                    this.ShowInTaskbar = false;
                }
                catch (Exception)
                {
                }              
            }
            else
                this.ShowInTaskbar = true;
            this.KeyDown += FrameworkElement_KeyPress;
        }

        void FrameworkElement_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
