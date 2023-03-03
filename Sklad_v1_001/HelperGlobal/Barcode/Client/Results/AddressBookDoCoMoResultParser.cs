// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.AddressBookDoCoMoResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class AddressBookDoCoMoResultParser : AbstractDoCoMoResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("MECARD:"))
        return (ParsedResult) null;
      string[] strArray = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("N:", massagedText, true);
      if (strArray == null)
        return (ParsedResult) null;
      string name = AddressBookDoCoMoResultParser.ParseName(strArray[0]);
      string pronunciation = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("SOUND:", massagedText, true);
      string[] phoneNumbers = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("TEL:", massagedText, true);
      string[] emails = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("EMAIL:", massagedText, true);
      string note = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("NOTE:", massagedText, false);
      string[] addresses = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("ADR:", massagedText, true);
      string str = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("BDAY:", massagedText, true);
      if (str != null && !ResultParser.IsStringOfDigits(str, 8))
        str = (string) null;
      string[] urls = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("URL:", massagedText, true);
      string org = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("ORG:", massagedText, true);
      return (ParsedResult) new AddressBookParsedResult(ResultParser.MaybeWrap(name), (string[]) null, pronunciation, phoneNumbers, (string[]) null, emails, (string[]) null, (string) null, note, addresses, (string[]) null, org, str, (string) null, urls, (string[]) null);
    }

    private static string ParseName(string name)
    {
      int length = name.IndexOf(',');
      return length >= 0 ? name.Substring(length + 1) + (object) ' ' + name.Substring(0, length) : name;
    }
  }
}
