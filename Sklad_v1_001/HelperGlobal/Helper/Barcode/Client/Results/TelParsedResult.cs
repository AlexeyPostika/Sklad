// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.TelParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class TelParsedResult : ParsedResult
  {
    private readonly string number;
    private readonly string telURI;
    private readonly string title;

    public TelParsedResult(string number, string telURI, string title)
      : base(ParsedResultType.Tel)
    {
      this.number = number;
      this.telURI = telURI;
      this.title = title;
    }

    public string GetNumber() => this.number;

    public string GetTelURI() => this.telURI;

    public string GetTitle() => this.title;

    public override string DisplayResult
    {
      get
      {
        StringBuilder result = new StringBuilder(20);
        ParsedResult.MaybeAppend(this.number, result);
        ParsedResult.MaybeAppend(this.title, result);
        return result.ToString();
      }
    }
  }
}
