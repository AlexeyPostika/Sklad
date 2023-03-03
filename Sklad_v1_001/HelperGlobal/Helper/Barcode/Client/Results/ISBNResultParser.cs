// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ISBNResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public class ISBNResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      if (result.BarcodeFormat != BarcodeFormat.EAN13)
        return (ParsedResult) null;
      string massagedText = ResultParser.GetMassagedText(result);
      if (massagedText.Length != 13)
        return (ParsedResult) null;
      return !massagedText.StartsWith("978") && !massagedText.StartsWith("979") ? (ParsedResult) null : (ParsedResult) new ISBNParsedResult(massagedText);
    }
  }
}
