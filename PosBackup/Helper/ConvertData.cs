using System;
using System.Data;
using System.Text;

namespace PosBackup.Helper
{
    public class ConvertData
    {
        public enum ConvertDataTypes
        {
            Int32 = 0,
            Int64 = 1,
            Double = 2,
            DateTime = 3,
            String = 4,
            Boolean = 5
        }

        DataRow localdatarow;
        object localRow;

        public DataRow Localdatarow
        {
            get
            {
                return localdatarow;
            }

            set
            {
                localdatarow = value;
            }
        }

        public object LocalRow
        {
            get
            {
                return localRow;
            }

            set
            {
                localRow = value;
            }
        }

        public ConvertData()
        {
        }

        public ConvertData(DataRow _row, object _localRow)
        {
            localdatarow = _row;
            localRow = _localRow;
        }

        public Int32 ConvertDataInt32(string _columnname)
        {
            Int32 i = 0;
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                if (Int32.TryParse(localdatarow[_columnname].ToString(), out i))
                    return i;
                else
                    return 0;
            }
            return 0;
        }

        public Int64 ConvertDataInt64(string _columnname)
        {
            Int64 i = 0;
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                if (Int64.TryParse(localdatarow[_columnname].ToString(), out i))
                    return i;
                else
                    return 0;
            }
            return 0;
        }
        public Double ConvertDataDouble(string _columnname)
        {
            Double i = 0.0;
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                if (Double.TryParse(localdatarow[_columnname].ToString(), out i))
                    return i;
                else
                    return 0.0;
            }
            return 0.0;
        }
        public DateTime? ConvertDataDateTime(string _columnname)
        {
            DateTime i = new DateTime();
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                if (DateTime.TryParse(localdatarow[_columnname].ToString(), out i))
                    return i;
                else
                    return null;
            }
            return null;
        }


        public Boolean ConvertDataBoolean(string _columnname)
        {
            Boolean i;
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                if (Boolean.TryParse(localdatarow[_columnname].ToString(), out i))
                    return i;
                else
                    return false;
            }
            return false;
        }

        public Version ConvertDataVersion(string _columnname)
        {
            Version i;
            if (_columnname != null)
            {
                if (Version.TryParse(_columnname, out i))
                    return i;
                else
                    return new Version("0.0.0.0");
            }
            return new Version("0.0.0.0");
        }

        public String ConvertDataString(string _columnname)
        {
            String i = "";
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                i = localdatarow[_columnname].ToString().Trim();
            }
            return i;
        }

        public String ConvertDataStringWithoutTrim(string _columnname)
        {
            String i = "";
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                i = localdatarow[_columnname].ToString();
            }
            return i;
        }

        public String DateTimeConvertString(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = date.ToLongDateString() + "  " + date.ToShortTimeString();
            }
            return i;
        }

        public String DateTimeConvertStringDateColon(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = String.Format("{0:dd.MM.yyyy}", date);
            }
            return i;
        }

        public String DateTimeConvertShortString(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = date.ToString();
            }
            return i;
        }

        public String DateTimeConvertLongString(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = date.ToLongDateString();
            }
            return i;
        }

        public String DateConvertShortString(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = date.ToShortDateString();
            }
            return i;
        }

        public String DateTimeConvertStringDate(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = String.Format("{0:ddMMyyyy}", date);
            }
            return i;
        }

        public DateTime DateTimeConvertShortDate(string _columnname)
        {
            DateTime i = DateTime.MinValue;
            if (_columnname != null && localdatarow.Table.Columns.Contains(_columnname) && localdatarow[_columnname].ToString() != null && !String.IsNullOrEmpty(localdatarow[_columnname].ToString()))
            {
                if (DateTime.TryParse(localdatarow[_columnname].ToString(), out i))
                    return i.Date;
            }
            return i.Date;
        }

        public DateTime? FlexConvertDataDateTime(string _columnname)
        {
            DateTime i = new DateTime();
            if (DateTime.TryParse(_columnname, out i))
                return i;
            else
                return null;
        }

        public String DateTimeConvertShortDateString(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = date.ToString("dd.MM.yyyy");
            }
            return i;
        }

        public String FlexDataConvertToString(String _columnname)
        {
            if (String.IsNullOrEmpty(_columnname))
                return "";
            else
                return _columnname;
        }

        public Version FlexDataConvertToVersion(String _columnname)
        {
            Version i;
            if (_columnname != null)
            {
                if (Version.TryParse(_columnname, out i))
                    return i;
                else
                    return new Version("0.0.0.0");
            }
            return new Version("0.0.0.0");
        }

        public Int32 FlexDataConvertToInt32(String _columnname)
        {
            Int32 i = 0;
            if (_columnname != null)
            {
                if (Int32.TryParse(_columnname, out i))
                    return i;
                else
                    return 0;
            }
            return 0;
        }

        public Int64 FlexDataConvertToInt64(string _columnname)
        {
            Int64 i = 0;
            if (_columnname != null)
            {
                if (Int64.TryParse(_columnname, out i))
                    return i;
                else
                    return 0;
            }
            return 0;
        }

        public Double FlexDataConvertToDouble(string _columnname)
        {
            Double i = 0.0;
            if (_columnname != null)
            {
                if (Double.TryParse(_columnname.ToString().Replace(".", ","), out i))
                    return i;
                else
                    return 0.0;
            }
            return 0.0;
        }

        public String FlexDataConvertToDateString(DateTime? _columnname)
        {
            String i = "";
            DateTime date;
            if (_columnname.HasValue)
            {
                date = _columnname.GetValueOrDefault();
                i = date.ToString("dd.MM.yyyy");
            }
            return i;
        }

        public Boolean FlexDataConvertToBoolean(string _columnname)
        {
            Boolean i = false;
            if (_columnname != null)
            {
                if (Boolean.TryParse(_columnname, out i))
                    return i;
                else
                    return false;
            }
            return false;
        }
    }
}
