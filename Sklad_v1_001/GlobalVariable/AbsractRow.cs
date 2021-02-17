using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.GlobalVariable
{
    public interface IAbstractRow
    {
        Int32 ID { get; set; }
    }

    public interface IAbstractButton
    {
        string Text { get; set; }
    }

    public interface IAbstractButtonFilter
    {
        string Text { get; set; }
        string From { get; set; }
        string To { get; set; }
    }

    public interface IAbstractGridFilter
    {
        string FilterText { get; set; }
        string FilterValue { get; set; }
        string FilterDescription { get; set; }
        Boolean IsChecked { get; set; }
    }

    public interface IAbstractMessageBox
    {
        Int32 ImageType { get; set; }
    }
}
