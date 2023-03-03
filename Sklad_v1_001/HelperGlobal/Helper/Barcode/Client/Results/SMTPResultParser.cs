// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.SMTPResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class SMTPResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("smtp:") && !massagedText.StartsWith("SMTP:"))
        return (ParsedResult) null;
      string emailAddress = massagedText.Substring(5);
      string subject_1 = (string) null;
      string body_2 = (string) null;
      int length1 = emailAddress.IndexOf(':');
      if (length1 >= 0)
      {
        subject_1 = emailAddress.Substring(length1 + 1);
        emailAddress = emailAddress.Substring(0, length1);
        int length2 = subject_1.IndexOf(':');
        if (length2 >= 0)
        {
          body_2 = subject_1.Substring(length2 + 1);
          subject_1 = subject_1.Substring(0, length2);
        }
      }
      string mailtoURI_3 = "mailto:" + emailAddress;
      return (ParsedResult) new EmailAddressParsedResult(emailAddress, subject_1, body_2, mailtoURI_3);
    }
  }
}
