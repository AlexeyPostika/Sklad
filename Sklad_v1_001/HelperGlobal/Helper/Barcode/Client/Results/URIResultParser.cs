// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.URIResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text.RegularExpressions;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class URIResultParser : ResultParser
  {
    private const string ALPHANUM_PART = "[a-zA-Z0-9\\-]";
    private static readonly Regex URL_WITH_PROTOCOL_PATTERN = new Regex("[a-zA-Z0-9]{2,}:");
    private static readonly Regex URL_WITHOUT_PROTOCOL_PATTERN = new Regex("([a-zA-Z0-9\\-]+\\.)+[a-zA-Z0-9\\-]{2,}(:\\d{1,5})?(/|\\?|$)");

    public override ParsedResult Parse(Result result)
    {
      string text = result.Text;
      if (text.StartsWith("URL:") || text.StartsWith("URI:"))
        return (ParsedResult) new URIParsedResult(text.Substring(4).Trim(), (string) null);
      string uri = text.Trim();
      return !URIResultParser.IsBasicallyValidURI(uri) ? (ParsedResult) null : (ParsedResult) new URIParsedResult(uri, (string) null);
    }

    internal static bool IsBasicallyValidURI(string uri)
    {
      if (uri.Contains(" "))
        return false;
      Match match1 = URIResultParser.URL_WITH_PROTOCOL_PATTERN.Match(uri);
      if (match1.Success && match1.Index == 0)
        return true;
      Match match2 = URIResultParser.URL_WITHOUT_PROTOCOL_PATTERN.Match(uri);
      return match2.Success && match2.Index == 0;
    }
  }
}
