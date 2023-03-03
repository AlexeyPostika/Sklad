// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.SMSParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class SMSParsedResult : ParsedResult
  {
    private readonly string[] numbers;
    private readonly string[] vias;
    private readonly string subject;
    private readonly string body;

    public SMSParsedResult(string number, string via, string subject, string body)
      : base(ParsedResultType.Sms)
    {
      this.numbers = new string[1]{ number };
      this.vias = new string[1]{ via };
      this.subject = subject;
      this.body = body;
    }

    public SMSParsedResult(string[] numbers, string[] vias, string subject, string body)
      : base(ParsedResultType.Sms)
    {
      this.numbers = numbers;
      this.vias = vias;
      this.subject = subject;
      this.body = body;
    }

    public string GetSMSURI()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("sms:");
      bool flag1 = true;
      for (int index = 0; index < this.numbers.Length; ++index)
      {
        if (flag1)
          flag1 = false;
        else
          stringBuilder.Append(',');
        stringBuilder.Append(this.numbers[index]);
        if (this.vias != null && this.vias[index] != null)
        {
          stringBuilder.Append(";via=");
          stringBuilder.Append(this.vias[index]);
        }
      }
      bool flag2 = this.body != null;
      bool flag3 = this.subject != null;
      if (flag2 || flag3)
      {
        stringBuilder.Append('?');
        if (flag2)
        {
          stringBuilder.Append("body=");
          stringBuilder.Append(this.body);
        }
        if (flag3)
        {
          if (flag2)
            stringBuilder.Append('&');
          stringBuilder.Append("subject=");
          stringBuilder.Append(this.subject);
        }
      }
      return stringBuilder.ToString();
    }

    public string[] GetNumbers() => this.numbers;

    public string[] GetVias() => this.vias;

    public string GetSubject() => this.subject;

    public string GetBody() => this.body;

    public override string DisplayResult
    {
      get
      {
        StringBuilder result = new StringBuilder(100);
        ParsedResult.MaybeAppend(this.numbers, result);
        ParsedResult.MaybeAppend(this.subject, result);
        ParsedResult.MaybeAppend(this.body, result);
        return result.ToString();
      }
    }
  }
}
