// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.TextParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class TextParsedResult : ParsedResult
  {
    private readonly string text;
    private readonly string language;

    public TextParsedResult(string text, string language)
      : base(ParsedResultType.Text)
    {
      this.text = text;
      this.language = language;
    }

    public string GetText() => this.text;

    public string GetLanguage() => this.language;

    public override string DisplayResult => this.text;
  }
}
