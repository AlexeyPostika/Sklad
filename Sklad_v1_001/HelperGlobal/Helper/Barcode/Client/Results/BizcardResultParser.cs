// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.BizcardResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class BizcardResultParser : AbstractDoCoMoResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("BIZCARD:"))
        return (ParsedResult) null;
      string value_ren1 = BizcardResultParser.BuildName(AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("N:", massagedText, true), AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("X:", massagedText, true));
      string title = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("T:", massagedText, true);
      string org = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("C:", massagedText, true);
      string[] addresses = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("A:", massagedText, true);
      string number1 = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("B:", massagedText, true);
      string number2 = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("M:", massagedText, true);
      string number3 = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("F:", massagedText, true);
      string value_ren2 = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("E:", massagedText, true);
      return (ParsedResult) new AddressBookParsedResult(ResultParser.MaybeWrap(value_ren1), (string[]) null, (string) null, BizcardResultParser.BuildPhoneNumbers(number1, number2, number3), (string[]) null, ResultParser.MaybeWrap(value_ren2), (string[]) null, (string) null, (string) null, addresses, (string[]) null, org, (string) null, title, (string[]) null, (string[]) null);
    }

    private static string[] BuildPhoneNumbers(string number1, string number2, string number3)
    {
      List<string> strings = new List<string>(3);
      if (number1 != null)
        strings.Add(number1);
      if (number2 != null)
        strings.Add(number2);
      if (number3 != null)
        strings.Add(number3);
      return strings.Count == 0 ? (string[]) null : ResultParser.ToStringArray(strings);
    }

    private static string BuildName(string firstName, string lastName)
    {
      if (firstName == null)
        return lastName;
      return lastName != null ? firstName + (object) ' ' + lastName : firstName;
    }
  }
}
