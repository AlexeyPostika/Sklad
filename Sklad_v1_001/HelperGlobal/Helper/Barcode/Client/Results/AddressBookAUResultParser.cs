// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.AddressBookAUResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class AddressBookAUResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.Contains("MEMORY") || !massagedText.Contains("\r\n"))
        return (ParsedResult) null;
      string value_ren = ResultParser.MatchSinglePrefixedField("NAME1:", massagedText, '\r', true);
      string pronunciation = ResultParser.MatchSinglePrefixedField("NAME2:", massagedText, '\r', true);
      string[] phoneNumbers = AddressBookAUResultParser.MatchMultipleValuePrefix("TEL", 3, massagedText, true);
      string[] emails = AddressBookAUResultParser.MatchMultipleValuePrefix("MAIL", 3, massagedText, true);
      string note = ResultParser.MatchSinglePrefixedField("MEMORY:", massagedText, '\r', false);
      string str = ResultParser.MatchSinglePrefixedField("ADD:", massagedText, '\r', true);
      string[] strArray;
      if (str != null)
        strArray = new string[1]{ str };
      else
        strArray = (string[]) null;
      string[] addresses = strArray;
      return (ParsedResult) new AddressBookParsedResult(ResultParser.MaybeWrap(value_ren), (string[]) null, pronunciation, phoneNumbers, (string[]) null, emails, (string[]) null, (string) null, note, addresses, (string[]) null, (string) null, (string) null, (string) null, (string[]) null, (string[]) null);
    }

    private static string[] MatchMultipleValuePrefix(
      string prefix,
      int max,
      string rawText,
      bool trim)
    {
      List<string> strings = (List<string>) null;
      for (int index = 1; index <= max; ++index)
      {
        string str = ResultParser.MatchSinglePrefixedField(prefix + (object) index + (object) ':', rawText, '\r', trim);
        if (str != null)
        {
          if (strings == null)
            strings = new List<string>(max);
          strings.Add(str);
        }
        else
          break;
      }
      return strings == null ? (string[]) null : ResultParser.ToStringArray(strings);
    }
  }
}
