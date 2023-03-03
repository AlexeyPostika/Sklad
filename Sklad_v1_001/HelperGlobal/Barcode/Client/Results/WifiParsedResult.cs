// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.WifiParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class WifiParsedResult : ParsedResult
  {
    private readonly string ssid;
    private readonly bool hidden;
    private readonly string networkEncryption;
    private readonly string password;

    public WifiParsedResult(string networkEncryption, string ssid, string password)
      : this(networkEncryption, ssid, password, false)
    {
    }

    public WifiParsedResult(string networkEncryption, string ssid, string password, bool hidden)
      : base(ParsedResultType.Wifi)
    {
      this.ssid = ssid;
      this.networkEncryption = networkEncryption;
      this.password = password;
      this.hidden = hidden;
    }

    public string GetSsid() => this.ssid;

    public string GetNetworkEncryption() => this.networkEncryption;

    public string GetPassword() => this.password;

    public bool IsHidden() => this.hidden;

    public override string DisplayResult
    {
      get
      {
        StringBuilder result = new StringBuilder(80);
        ParsedResult.MaybeAppend(this.ssid, result);
        ParsedResult.MaybeAppend(this.networkEncryption, result);
        ParsedResult.MaybeAppend(this.password, result);
        ParsedResult.MaybeAppend(this.hidden.ToString(), result);
        return result.ToString();
      }
    }
  }
}
