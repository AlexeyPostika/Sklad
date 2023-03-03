// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.TelResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class TelResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("tel:") && !massagedText.StartsWith("TEL:"))
        return (ParsedResult) null;
      string telURI = massagedText.StartsWith("TEL:") ? "tel:" + massagedText.Substring(4) : massagedText;
      int num = massagedText.IndexOf('?', 4);
      return (ParsedResult) new TelParsedResult(num < 0 ? massagedText.Substring(4) : massagedText.Substring(4, num - 4), telURI, (string) null);
    }
  }
}
