using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sklad_v1_001
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public enum LogImageType
        {
            None = 1,
            Error = 2,
            Hand = 3,
            Stop = 4,
            Question = 5,
            Exclamation = 6,
            Warning = 7,
            Asterisk = 8,
            Information = 9,
            Action = 10,
            Filter = 11
        }
    }
}
