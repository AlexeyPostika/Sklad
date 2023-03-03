// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.URLTOResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class URLTOResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("urlto:") && !massagedText.StartsWith("URLTO:"))
        return (ParsedResult) null;
      int num = massagedText.IndexOf(':', 6);
      if (num < 0)
        return (ParsedResult) null;
      string title = num <= 6 ? (string) null : massagedText.Substring(6, num - 6);
      return (ParsedResult) new URIParsedResult(massagedText.Substring(num + 1), title);
    }
  }
}
