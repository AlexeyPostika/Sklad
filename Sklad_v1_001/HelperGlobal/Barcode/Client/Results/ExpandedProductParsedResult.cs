// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ExpandedProductParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Client.Results
{
  public class ExpandedProductParsedResult : ParsedResult
  {
    public const string KILOGRAM = "KG";
    public const string POUND = "LB";
    private readonly string rawText;
    private readonly string productID;
    private readonly string sscc;
    private readonly string lotNumber;
    private readonly string productionDate;
    private readonly string packagingDate;
    private readonly string bestBeforeDate;
    private readonly string expirationDate;
    private readonly string weight;
    private readonly string weightType;
    private readonly string weightIncrement;
    private readonly string price;
    private readonly string priceIncrement;
    private readonly string priceCurrency;
    private readonly Dictionary<string, string> uncommonAIs;

    public ExpandedProductParsedResult(
      string rawText,
      string productID,
      string sscc,
      string lotNumber,
      string productionDate,
      string packagingDate,
      string bestBeforeDate,
      string expirationDate,
      string weight,
      string weightType,
      string weightIncrement,
      string price,
      string priceIncrement,
      string priceCurrency,
      Dictionary<string, string> uncommonAIs)
      : base(ParsedResultType.Product)
    {
      this.rawText = rawText;
      this.productID = productID;
      this.sscc = sscc;
      this.lotNumber = lotNumber;
      this.productionDate = productionDate;
      this.packagingDate = packagingDate;
      this.bestBeforeDate = bestBeforeDate;
      this.expirationDate = expirationDate;
      this.weight = weight;
      this.weightType = weightType;
      this.weightIncrement = weightIncrement;
      this.price = price;
      this.priceIncrement = priceIncrement;
      this.priceCurrency = priceCurrency;
      this.uncommonAIs = uncommonAIs;
    }

    public override bool Equals(object o)
    {
      if (!(o is ExpandedProductParsedResult))
        return false;
      ExpandedProductParsedResult productParsedResult = (ExpandedProductParsedResult) o;
      return ExpandedProductParsedResult.EqualsOrNull((object) this.productID, (object) productParsedResult.productID) && ExpandedProductParsedResult.EqualsOrNull((object) this.sscc, (object) productParsedResult.sscc) && ExpandedProductParsedResult.EqualsOrNull((object) this.lotNumber, (object) productParsedResult.lotNumber) && ExpandedProductParsedResult.EqualsOrNull((object) this.productionDate, (object) productParsedResult.productionDate) && ExpandedProductParsedResult.EqualsOrNull((object) this.bestBeforeDate, (object) productParsedResult.bestBeforeDate) && ExpandedProductParsedResult.EqualsOrNull((object) this.expirationDate, (object) productParsedResult.expirationDate) && ExpandedProductParsedResult.EqualsOrNull((object) this.weight, (object) productParsedResult.weight) && ExpandedProductParsedResult.EqualsOrNull((object) this.weightType, (object) productParsedResult.weightType) && ExpandedProductParsedResult.EqualsOrNull((object) this.weightIncrement, (object) productParsedResult.weightIncrement) && ExpandedProductParsedResult.EqualsOrNull((object) this.price, (object) productParsedResult.price) && ExpandedProductParsedResult.EqualsOrNull((object) this.priceIncrement, (object) productParsedResult.priceIncrement) && ExpandedProductParsedResult.EqualsOrNull((object) this.priceCurrency, (object) productParsedResult.priceCurrency) && ExpandedProductParsedResult.EqualsOrNull((object) this.uncommonAIs, (object) productParsedResult.uncommonAIs);
    }

    private static bool EqualsOrNull(object o1, object o2) => o1 != null ? o1.Equals(o2) : o2 == null;

    public override int GetHashCode() => 0 ^ ExpandedProductParsedResult.HashNotNull((object) this.productID) ^ ExpandedProductParsedResult.HashNotNull((object) this.sscc) ^ ExpandedProductParsedResult.HashNotNull((object) this.lotNumber) ^ ExpandedProductParsedResult.HashNotNull((object) this.productionDate) ^ ExpandedProductParsedResult.HashNotNull((object) this.bestBeforeDate) ^ ExpandedProductParsedResult.HashNotNull((object) this.expirationDate) ^ ExpandedProductParsedResult.HashNotNull((object) this.weight) ^ ExpandedProductParsedResult.HashNotNull((object) this.weightType) ^ ExpandedProductParsedResult.HashNotNull((object) this.weightIncrement) ^ ExpandedProductParsedResult.HashNotNull((object) this.price) ^ ExpandedProductParsedResult.HashNotNull((object) this.priceIncrement) ^ ExpandedProductParsedResult.HashNotNull((object) this.priceCurrency) ^ ExpandedProductParsedResult.HashNotNull((object) this.uncommonAIs);

    private static int HashNotNull(object o) => o != null ? o.GetHashCode() : 0;

    public string GetRawText() => this.rawText;

    public string GetProductID() => this.productID;

    public string GetSscc() => this.sscc;

    public string GetLotNumber() => this.lotNumber;

    public string GetProductionDate() => this.productionDate;

    public string GetPackagingDate() => this.packagingDate;

    public string GetBestBeforeDate() => this.bestBeforeDate;

    public string GetExpirationDate() => this.expirationDate;

    public string GetWeight() => this.weight;

    public string GetWeightType() => this.weightType;

    public string GetWeightIncrement() => this.weightIncrement;

    public string GetPrice() => this.price;

    public string GetPriceIncrement() => this.priceIncrement;

    public string GetPriceCurrency() => this.priceCurrency;

    public Dictionary<string, string> GetUncommonAIs() => this.uncommonAIs;

    public override string DisplayResult => this.rawText.ToString();
  }
}
