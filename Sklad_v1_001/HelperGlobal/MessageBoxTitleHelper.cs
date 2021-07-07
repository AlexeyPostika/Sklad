using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad_v1_001.HelperGlobal
{
    public static class MessageBoxTitleHelper
    {
        public static string GenerateTitle(TitleType titleType, string text, string fieldName = null)
        {
            string ret;
            string titleTypeString;

            switch ((Int32)titleType)
            {
                case 2:
                    titleTypeString = Properties.Resources.ErrorTitle;
                    break;
                case 3:
                    titleTypeString = Properties.Resources.HandTitle;
                    break;
                case 4:
                    titleTypeString = Properties.Resources.StopTitle;
                    break;
                case 5:
                    titleTypeString = Properties.Resources.QuestionTitle;
                    break;
                case 6:
                    titleTypeString = Properties.Resources.ExclamationTitle;
                    break;
                case 7:
                    titleTypeString = Properties.Resources.WarningTitle;
                    break;
                case 8:
                    titleTypeString = Properties.Resources.AsteriskTitle;
                    break;
                case 9:
                    titleTypeString = Properties.Resources.InformationTitle;
                    break;
                default:
                    titleTypeString = "";
                    break;
            }

            ret = String.Concat(titleTypeString, String.IsNullOrEmpty(titleTypeString) ? "(" : " (", text, String.IsNullOrEmpty(fieldName) ? ")" : " - " + fieldName + ")");
            return ret;
        }

        public enum TitleType
        {
            Error = 2,
            Hand = 3,
            Stop = 4,
            Question = 5,
            Exclamation = 6,
            Warning = 7,
            Asterisk = 8,
            Information = 9,
        }
    }
}
