using System.Windows;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Data;
using System;
using System.Windows.Xps.Packaging;

namespace Sklad_v1_001.Control.FlexProgressBar
{
    interface iAbstractProgressWork : INotifyPropertyChanged
    {
        void LongProcess();
        int Percent { get; set; }
        XpsDocument XpsDocument { get; set; }
    }
}
