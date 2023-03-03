// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.GeoParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class GeoParsedResult : ParsedResult
  {
    private readonly double latitude;
    private readonly double longitude;
    private readonly double altitude;
    private readonly string query;

    internal GeoParsedResult(double latitude, double longitude, double altitude, string query)
      : base(ParsedResultType.Geo)
    {
      this.latitude = latitude;
      this.longitude = longitude;
      this.altitude = altitude;
      this.query = query;
    }

    public string GetGeoURI()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("geo:");
      stringBuilder.Append(this.latitude);
      stringBuilder.Append(',');
      stringBuilder.Append(this.longitude);
      if (this.altitude > 0.0)
      {
        stringBuilder.Append(',');
        stringBuilder.Append(this.altitude);
      }
      if (this.query != null)
      {
        stringBuilder.Append('?');
        stringBuilder.Append(this.query);
      }
      return stringBuilder.ToString();
    }

    public double GetLatitude() => this.latitude;

    public double GetLongitude() => this.longitude;

    public double GetAltitude() => this.altitude;

    public string GetQuery() => this.query;

    public override string DisplayResult
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder(20);
        stringBuilder.Append(this.latitude);
        stringBuilder.Append(", ");
        stringBuilder.Append(this.longitude);
        if (this.altitude > 0.0)
        {
          stringBuilder.Append(", ");
          stringBuilder.Append(this.altitude);
          stringBuilder.Append('m');
        }
        if (this.query != null)
        {
          stringBuilder.Append(" (");
          stringBuilder.Append(this.query);
          stringBuilder.Append(')');
        }
        return stringBuilder.ToString();
      }
    }
  }
}
