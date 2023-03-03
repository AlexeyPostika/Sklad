// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.BookmarkDoCoMoResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class BookmarkDoCoMoResultParser : AbstractDoCoMoResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string text = result.Text;
      if (!text.StartsWith("MEBKM:"))
        return (ParsedResult) null;
      string title = AbstractDoCoMoResultParser.MatchSingleDoCoMoPrefixedField("TITLE:", text, true);
      string[] strArray = AbstractDoCoMoResultParser.MatchDoCoMoPrefixedField("URL:", text, true);
      if (strArray == null)
        return (ParsedResult) null;
      string uri = strArray[0];
      return !URIResultParser.IsBasicallyValidURI(uri) ? (ParsedResult) null : (ParsedResult) new URIParsedResult(uri, title);
    }
  }
}
