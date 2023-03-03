// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MessagingToolkit.Barcode.Client.Results
{
  public abstract class ResultParser
  {
    private static readonly ResultParser[] PARSERS = new ResultParser[19]
    {
      (ResultParser) new BookmarkDoCoMoResultParser(),
      (ResultParser) new AddressBookDoCoMoResultParser(),
      (ResultParser) new EmailDoCoMoResultParser(),
      (ResultParser) new AddressBookAUResultParser(),
      (ResultParser) new VCardResultParser(),
      (ResultParser) new BizcardResultParser(),
      (ResultParser) new VEventResultParser(),
      (ResultParser) new EmailAddressResultParser(),
      (ResultParser) new SMTPResultParser(),
      (ResultParser) new TelResultParser(),
      (ResultParser) new SMSMMSResultParser(),
      (ResultParser) new SmsToMmsToResultParser(),
      (ResultParser) new GeoResultParser(),
      (ResultParser) new WifiResultParser(),
      (ResultParser) new URLTOResultParser(),
      (ResultParser) new URIResultParser(),
      (ResultParser) new ISBNResultParser(),
      (ResultParser) new ProductResultParser(),
      (ResultParser) new ExpandedProductResultParser()
    };
    private static readonly Regex DIGITS = new Regex("\\d*");
    private static readonly Regex ALPHANUM = new Regex("[a-zA-Z0-9]*");
    private static readonly Regex AMPERSAND = new Regex("&");
    private static readonly Regex EQUALS = new Regex("=");
    private static readonly string BYTE_ORDER_MARK = "\uFEFF";

    public abstract ParsedResult Parse(Result theResult);

    protected internal static string GetMassagedText(Result result)
    {
      string massagedText = result.Text;
      if (massagedText.StartsWith(ResultParser.BYTE_ORDER_MARK))
        massagedText = massagedText.Substring(1);
      return massagedText;
    }

    public static ParsedResult ParseResult(Result theResult)
    {
      foreach (ResultParser resultParser in ResultParser.PARSERS)
      {
        ParsedResult result = resultParser.Parse(theResult);
        if (result != null)
          return result;
      }
      return (ParsedResult) new TextParsedResult(theResult.Text, (string) null);
    }

    protected internal static void MaybeAppend(string val, StringBuilder result)
    {
      if (val == null)
        return;
      result.Append('\n');
      result.Append(val);
    }

    protected internal static void MaybeAppend(string[] val, StringBuilder result)
    {
      if (val == null)
        return;
      foreach (string str in val)
      {
        result.Append('\n');
        result.Append(str);
      }
    }

    protected internal static string[] MaybeWrap(string value_ren)
    {
      if (value_ren == null)
        return (string[]) null;
      return new string[1]{ value_ren };
    }

    protected internal static string UnescapeBackslash(string escaped)
    {
      int charCount = escaped.IndexOf('\\');
      if (charCount < 0)
        return escaped;
      int length = escaped.Length;
      StringBuilder stringBuilder = new StringBuilder(length - 1);
      stringBuilder.Append(escaped.ToCharArray(), 0, charCount);
      bool flag = false;
      for (int index = charCount; index < length; ++index)
      {
        char ch = escaped[index];
        if (flag || ch != '\\')
        {
          stringBuilder.Append(ch);
          flag = false;
        }
        else
          flag = true;
      }
      return stringBuilder.ToString();
    }

    protected internal static int ParseHexDigit(char c)
    {
      if (c >= '0' && c <= '9')
        return (int) c - 48;
      if (c >= 'a' && c <= 'f')
        return 10 + ((int) c - 97);
      return c >= 'A' && c <= 'F' ? 10 + ((int) c - 65) : -1;
    }

    protected internal static bool IsStringOfDigits(string val, int length) => val != null && length == val.Length && ResultParser.DIGITS.Match(val).Success;

    protected internal static bool IsSubstringOfDigits(string val, int offset, int length)
    {
      if (val == null)
        return false;
      int num = offset + length;
      return val.Length >= num && ResultParser.DIGITS.Match(val.Substring(offset, length)).Success;
    }

    protected internal static bool IsSubstringOfAlphaNumeric(string val, int offset, int length)
    {
      if (val == null)
        return false;
      int num = offset + length;
      return val.Length >= num && ResultParser.ALPHANUM.Match(val.Substring(offset, length)).Success;
    }

    internal static IDictionary<string, string> ParseNameValuePairs(string uri)
    {
      int num = uri.IndexOf('?');
      if (num < 0)
        return (IDictionary<string, string>) null;
      Dictionary<string, string> result = new Dictionary<string, string>(3);
      foreach (string keyValue in ResultParser.AMPERSAND.Split(uri.Substring(num + 1)))
        ResultParser.AppendKeyValue(keyValue, (IDictionary<string, string>) result);
      return (IDictionary<string, string>) result;
    }

    private static void AppendKeyValue(string keyValue, IDictionary<string, string> result)
    {
      string[] strArray = ResultParser.EQUALS.Split(keyValue, 2);
      if (strArray.Length != 2)
        return;
      string key = strArray[0];
      string escaped = strArray[1];
      try
      {
        string str = ResultParser.UrlDecode(escaped);
        result.Add(key, str);
      }
      catch
      {
      }
    }

    protected static string UrlDecode(string escaped)
    {
      if (escaped == null)
        return (string) null;
      char[] charArray = escaped.ToCharArray();
      int firstEscape = ResultParser.FindFirstEscape(charArray);
      if (firstEscape < 0)
        return escaped;
      int length = charArray.Length;
      StringBuilder stringBuilder = new StringBuilder(length - 2);
      stringBuilder.Append(charArray, 0, firstEscape);
      for (int index = firstEscape; index < length; ++index)
      {
        char ch = charArray[index];
        switch (ch)
        {
          case '%':
            if (index >= length - 2)
            {
              stringBuilder.Append('%');
              break;
            }
            int num;
            int hexDigit1 = ResultParser.ParseHexDigit(charArray[num = index + 1]);
            int hexDigit2 = ResultParser.ParseHexDigit(charArray[index = num + 1]);
            if (hexDigit1 < 0 || hexDigit2 < 0)
            {
              stringBuilder.Append('%');
              stringBuilder.Append(charArray[index - 1]);
              stringBuilder.Append(charArray[index]);
            }
            stringBuilder.Append((char) ((hexDigit1 << 4) + hexDigit2));
            break;
          case '+':
            stringBuilder.Append(' ');
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
      }
      return stringBuilder.ToString();
    }

    private static int FindFirstEscape(char[] escapedArray)
    {
      int length = escapedArray.Length;
      for (int firstEscape = 0; firstEscape < length; ++firstEscape)
      {
        switch (escapedArray[firstEscape])
        {
          case '%':
          case '+':
            return firstEscape;
          default:
            continue;
        }
      }
      return -1;
    }

    internal static string[] MatchPrefixedField(
      string prefix,
      string rawText,
      char endChar,
      bool trim)
    {
      List<string> strings = (List<string>) null;
      int startIndex1 = 0;
      int length = rawText.Length;
      while (startIndex1 < length)
      {
        int num1 = rawText.IndexOf(prefix, startIndex1);
        if (num1 >= 0)
        {
          startIndex1 = num1 + prefix.Length;
          int startIndex2 = startIndex1;
          bool flag = true;
          while (flag)
          {
            int num2 = rawText.IndexOf(endChar, startIndex1);
            if (num2 < 0)
            {
              startIndex1 = rawText.Length;
              flag = false;
            }
            else if (rawText[num2 - 1] == '\\')
            {
              startIndex1 = num2 + 1;
            }
            else
            {
              if (strings == null)
                strings = new List<string>(3);
              string str = ResultParser.UnescapeBackslash(rawText.Substring(startIndex2, num2 - startIndex2));
              if (trim)
                str = str.Trim();
              if (str.Length > 0)
                strings.Add(str);
              startIndex1 = num2 + 1;
              flag = false;
            }
          }
        }
        else
          break;
      }
      return strings == null || strings.Count == 0 ? (string[]) null : ResultParser.ToStringArray(strings);
    }

    internal static string MatchSinglePrefixedField(
      string prefix,
      string rawText,
      char endChar,
      bool trim)
    {
      return ResultParser.MatchPrefixedField(prefix, rawText, endChar, trim)?[0];
    }

    internal static string[] ToStringArray(List<string> strings)
    {
      int count = strings.Count;
      string[] stringArray = new string[count];
      for (int index = 0; index < count; ++index)
        stringArray[index] = strings[index];
      return stringArray;
    }
  }
}
