// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ProductParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class ProductParsedResult : ParsedResult
  {
    private string productID;
    private string normalizedProductID;

    public string ProductID => this.productID;

    public string NormalizedProductID => this.normalizedProductID;

    public override string DisplayResult => this.productID;

    internal ProductParsedResult(string productID)
      : this(productID, productID)
    {
    }

    internal ProductParsedResult(string productID, string normalizedProductID)
      : base(ParsedResultType.Product)
    {
      this.productID = productID;
      this.normalizedProductID = normalizedProductID;
    }
  }
}
