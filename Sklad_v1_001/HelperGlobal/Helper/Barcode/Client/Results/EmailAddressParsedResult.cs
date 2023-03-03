// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.EmailAddressParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class EmailAddressParsedResult : ParsedResult
  {
    private readonly string emailAddress;
    private readonly string subject;
    private readonly string body;
    private readonly string mailtoURI;

    internal EmailAddressParsedResult(
      string emailAddress,
      string subject_1,
      string body_2,
      string mailtoURI_3)
      : base(ParsedResultType.EmailAddress)
    {
      this.emailAddress = emailAddress;
      this.subject = subject_1;
      this.body = body_2;
      this.mailtoURI = mailtoURI_3;
    }

    public string GetEmailAddress() => this.emailAddress;

    public string GetSubject() => this.subject;

    public string GetBody() => this.body;

    public string GetMailtoURI() => this.mailtoURI;

    public override string DisplayResult
    {
      get
      {
        StringBuilder result = new StringBuilder(30);
        ParsedResult.MaybeAppend(this.emailAddress, result);
        ParsedResult.MaybeAppend(this.subject, result);
        ParsedResult.MaybeAppend(this.body, result);
        return result.ToString();
      }
    }
  }
}
