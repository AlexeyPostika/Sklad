// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.EmailDoCoMoResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text.RegularExpressions;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class EmailDoCoMoResultParser : AbstractDoCoMoResultParser
  {
    private static Regex ATEXT_ALPHANUMERIC = new Regex("^[a-zA-Z0-9@.!#$%&'*+\\-/=?^_`{|}~]+$");

    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("MATMSG:"))
        return (ParsedResult) null;
      string[] strArray = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("TO:", massagedText, true);
      if (strArray == null)
        return (ParsedResult) null;
      string str = strArray[0];
      if (!EmailDoCoMoResultParser.IsBasicallyValidEmailAddress(str))
        return (ParsedResult) null;
      string subject_1 = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("SUB:", massagedText, false);
      string body_2 = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("BODY:", massagedText, false);
      return (ParsedResult) new EmailAddressParsedResult(str, subject_1, body_2, "mailto:" + str);
    }

    internal static bool IsBasicallyValidEmailAddress(string email) => email != null && EmailDoCoMoResultParser.ATEXT_ALPHANUMERIC.Match(email).Success && email.IndexOf('@') >= 0;
  }
}
