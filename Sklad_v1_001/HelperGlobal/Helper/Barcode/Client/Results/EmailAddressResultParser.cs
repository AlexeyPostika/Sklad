// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.EmailAddressResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class EmailAddressResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (massagedText.StartsWith("mailto:") || massagedText.StartsWith("MAILTO:"))
      {
        string escaped = massagedText.Substring(7);
        int length = escaped.IndexOf('?');
        if (length >= 0)
          escaped = escaped.Substring(0, length);
        string emailAddress = ResultParser.UrlDecode(escaped);
        IDictionary<string, string> nameValuePairs = ResultParser.ParseNameValuePairs(massagedText);
        string subject_1 = (string) null;
        string body_2 = (string) null;
        if (nameValuePairs != null)
        {
          if (emailAddress.Length == 0)
            emailAddress = nameValuePairs["to"];
          subject_1 = nameValuePairs["subject"];
          body_2 = nameValuePairs["body"];
        }
        return (ParsedResult) new EmailAddressParsedResult(emailAddress, subject_1, body_2, massagedText);
      }
      if (!EmailDoCoMoResultParser.IsBasicallyValidEmailAddress(massagedText))
        return (ParsedResult) null;
      string emailAddress1 = massagedText;
      return (ParsedResult) new EmailAddressParsedResult(emailAddress1, (string) null, (string) null, "mailto:" + emailAddress1);
    }
  }
}
