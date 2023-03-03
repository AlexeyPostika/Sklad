// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.WifiResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class WifiResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("WIFI:"))
        return (ParsedResult) null;
      string ssid = ResultParser.MatchSinglePrefixedField("S:", massagedText, ';', false);
      if (ssid == null || ssid.Length == 0)
        return (ParsedResult) null;
      string password = ResultParser.MatchSinglePrefixedField("P:", massagedText, ';', false);
      string networkEncryption = ResultParser.MatchSinglePrefixedField("T:", massagedText, ';', false) ?? "nopass";
      bool result1;
      bool.TryParse(ResultParser.MatchSinglePrefixedField("H:", massagedText, ';', false), out result1);
      return (ParsedResult) new WifiParsedResult(networkEncryption, ssid, password, result1);
    }
  }
}
