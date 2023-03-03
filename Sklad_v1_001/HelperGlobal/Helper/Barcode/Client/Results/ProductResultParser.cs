// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ProductResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.OneD;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class ProductResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      BarcodeFormat barcodeFormat = result.BarcodeFormat;
      switch (barcodeFormat)
      {
        case BarcodeFormat.UPCA:
        case BarcodeFormat.UPCE:
        case BarcodeFormat.EAN13:
        case BarcodeFormat.EAN8:
          string massagedText = ResultParser.GetMassagedText(result);
          int length = massagedText.Length;
          for (int index = 0; index < length; ++index)
          {
            char ch = massagedText[index];
            if (ch < '0' || ch > '9')
              return (ParsedResult) null;
          }
          string normalizedProductID = barcodeFormat != BarcodeFormat.UPCE ? massagedText : UPCEDecoder.ConvertUPCEtoUPCA(massagedText);
          return (ParsedResult) new ProductParsedResult(massagedText, normalizedProductID);
        default:
          return (ParsedResult) null;
      }
    }
  }
}
