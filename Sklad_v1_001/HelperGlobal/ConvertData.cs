using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 namespace Sklad_v1_001.HelperGlobal
{
    public class ConvertData
     {
        ImageSql imageSql;
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
            imageSql = new ImageSql();
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

        internal object FlexDataConvertToInt32(string v)
        {
            throw new NotImplementedException();
        }

        //private void ConvertDataToLocalRow(string name, object value)
        //{
        //    Screens.RelatedProductDocument.LocaleRow localeRow = localRow as Screens.RelatedProductDocument.LocaleRow;
        //    if (localeRow != null)
        //    {
        //        var field = typeof(Screens.RelatedProductDocument.LocaleRow).GetField(name);
        //        field.SetValue(localeRow, value);
        //    }
        //    Screens.RelatedProduct.LocaleRow localeRow1 = localRow as Screens.RelatedProduct.LocaleRow;
        //    if (localeRow1 != null)
        //    {
        //        var field = typeof(Screens.RelatedProduct.LocaleRow).GetField(name);
        //        field.SetValue(localeRow1, value);
        //    }// добавить другие LocalRow                    
        //}
        //public void Convert(string _columnname, ConvertDataTypes convertDataTypes)
        //{
        //    switch (convertDataTypes)
        //    {
        //        case ConvertDataTypes.Int32:
        //            {
        //                Int32 i = ConvertDataInt32(_columnname);
        //                ConvertDataToLocalRow(_columnname, i);
        //                break;
        //            }
        //        case ConvertDataTypes.Int64:
        //            {
        //                Int64 i = ConvertDataInt64(_columnname);
        //                ConvertDataToLocalRow(_columnname, i);
        //                break;
        //            }
        //        case ConvertDataTypes.Double:
        //            {
        //                Double i = ConvertDataDouble(_columnname);
        //                ConvertDataToLocalRow(_columnname, i);
        //                break;
        //            }
        //        case ConvertDataTypes.DateTime:
        //            {
        //                DateTime? i = ConvertDataDateTime(_columnname);
        //                ConvertDataToLocalRow(_columnname, i);
        //                break;
        //            }
        //        case ConvertDataTypes.String:
        //            {
        //                String i = ConvertDataString(_columnname);
        //                ConvertDataToLocalRow(_columnname, i);
        //                break;
        //            }
        //        case ConvertDataTypes.Boolean:
        //            {
        //                Boolean i = ConvertDataBoolean(_columnname);
        //                ConvertDataToLocalRow(_columnname, i);
        //                break;
        //            }
        //            // добавить аналогичные методы для других типов данных
        //    }
        //}
        /* public void Convert(string _columnname, string _rowcolumnname, ConvertDataTypes convertDataTypes)
     {
         switch (convertDataTypes)
         {
             case ConvertDataTypes.Int32:
                {
                    Int32 i = ConvertDataInt32(_columnname);
                    ConvertDataToLocalRow(_columnname, i);
                    break;
                }
              case ConvertDataTypes.Int64:
                {
                    Int64 i = ConvertDataInt64(_columnname);
                    ConvertDataToLocalRow(_columnname, i);
                    break;
                }
            case ConvertDataTypes.Double:
                {
                    Double i = ConvertDataDouble(_columnname);
                    ConvertDataToLocalRow(_columnname, i);
                    break;
                }
            case ConvertDataTypes.DateTime:
                {
                    DateTime? i = ConvertDataDateTime(_columnname);
                    ConvertDataToLocalRow(_columnname, i);
                    break;
    }
            case ConvertDataTypes.String:
                {
                    String i = ConvertDataString(_columnname);
                    ConvertDataToLocalRow(_columnname, i);
                    break;
                }
              case ConvertDataTypes.Boolean:
    {
                    Boolean i = ConvertDataBoolean(_columnname);
                    ConvertDataToLocalRow(_columnname, i);
                    break;
                }
                 // добавить аналогичные методы для других типов данных
           }
     }*/
        //private
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
           public String ConvertDataString(string _columnname)
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
           public String DateTimeConvertShortString(DateTime? _columnname)
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
    }
}
