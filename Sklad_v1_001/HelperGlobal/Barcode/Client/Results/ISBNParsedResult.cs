// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ISBNParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class ISBNParsedResult : ParsedResult
  {
    private readonly string isbn;

    internal ISBNParsedResult(string isbn)
      : base(ParsedResultType.Isbn)
    {
      this.isbn = isbn;
    }

    public string ISBN => this.isbn;

    public override string DisplayResult => this.isbn;
  }
}
