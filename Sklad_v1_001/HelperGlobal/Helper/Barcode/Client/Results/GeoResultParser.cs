// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.GeoResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class GeoResultParser : ResultParser
  {
    private static Regex GEO_URL_PATTERN = new Regex("geo:([\\-0-9.]+),([\\-0-9.]+)(?:,([\\-0-9.]+))?(?:\\?(.*))?", RegexOptions.IgnoreCase);

    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      Match match = GeoResultParser.GEO_URL_PATTERN.Match(massagedText);
      if (!match.Success)
        return (ParsedResult) null;
      string query = match.Groups[4].Value;
      double latitude;
      double longitude;
      double altitude;
      try
      {
        latitude = double.Parse(match.Groups[1].Value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
        if (latitude > 90.0 || latitude < -90.0)
          return (ParsedResult) null;
        longitude = double.Parse(match.Groups[2].Value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
        if (longitude > 180.0 || longitude < -180.0)
          return (ParsedResult) null;
        if (string.IsNullOrEmpty(match.Groups[3].Value))
        {
          altitude = 0.0;
        }
        else
        {
          altitude = double.Parse(match.Groups[3].Value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
          if (altitude < 0.0)
            return (ParsedResult) null;
        }
      }
      catch (Exception ex)
      {
        return (ParsedResult) null;
      }
      return (ParsedResult) new GeoParsedResult(latitude, longitude, altitude, query);
    }
  }
}
