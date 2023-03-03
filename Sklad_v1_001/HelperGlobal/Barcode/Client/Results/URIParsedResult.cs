// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.URIParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;
using System.Text.RegularExpressions;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class URIParsedResult : ParsedResult
  {
    private static readonly Regex USER_IN_HOST = new Regex(":/*([^/@]+)@[^/]+");
    private readonly string uri;
    private readonly string title;

    public string URI => this.uri;

    public string Title => this.title;

    public bool PossiblyMaliciousURI => URIParsedResult.USER_IN_HOST.Match(this.uri).Success;

    public override string DisplayResult
    {
      get
      {
        StringBuilder result = new StringBuilder(30);
        ParsedResult.MaybeAppend(this.title, result);
        ParsedResult.MaybeAppend(this.uri, result);
        return result.ToString();
      }
    }

    public URIParsedResult(string uri, string title)
      : base(ParsedResultType.Uri)
    {
      this.uri = URIParsedResult.MassageURI(uri);
      this.title = title;
    }

    private static string MassageURI(string uri)
    {
      int protocolEnd = uri.IndexOf(':');
      if (protocolEnd < 0)
        uri = "http://" + uri;
      else if (URIParsedResult.isColonFollowedByPortNumber(uri, protocolEnd))
        uri = "http://" + uri;
      return uri;
    }

    private static bool isColonFollowedByPortNumber(string uri, int protocolEnd)
    {
      int num = uri.IndexOf('/', protocolEnd + 1);
      if (num < 0)
        num = uri.Length;
      if (num <= protocolEnd + 1)
        return false;
      for (int index = protocolEnd + 1; index < num; ++index)
      {
        if (uri[index] < '0' || uri[index] > '9')
          return false;
      }
      return true;
    }
  }
}
