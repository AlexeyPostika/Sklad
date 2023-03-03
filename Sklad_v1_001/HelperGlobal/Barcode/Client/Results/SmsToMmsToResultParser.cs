// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.SmsToMmsToResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class SmsToMmsToResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("smsto:") && !massagedText.StartsWith("SMSTO:") && !massagedText.StartsWith("mmsto:") && !massagedText.StartsWith("MMSTO:"))
        return (ParsedResult) null;
      string number = massagedText.Substring(6);
      string body = (string) null;
      int length = number.IndexOf(':');
      if (length >= 0)
      {
        body = number.Substring(length + 1);
        number = number.Substring(0, length);
      }
      return (ParsedResult) new SMSParsedResult(number, (string) null, (string) null, body);
    }
  }
}
