// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.CalendarParsedResult
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class CalendarParsedResult : ParsedResult
  {
    private const string DATE_FORMAT = "yyyyMMdd";
    private const string DATE_TIME_FORMAT = "yyyyMMdd'T'HHmmss";
    private static readonly Regex RFC2445_DURATION = new Regex("P(?:(\\d+)W)?(?:(\\d+)D)?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+)S)?)?");
    private static readonly long[] RFC2445_DURATION_FIELD_UNITS = new long[5]
    {
      604800000L,
      86400000L,
      3600000L,
      60000L,
      1000L
    };
    private static readonly Regex DATE_TIME = new Regex("[0-9]{8}(T[0-9]{6}Z?)?");
    private readonly string summary;
    private readonly DateTime start;
    private readonly bool startAllDay;
    private readonly DateTime? end;
    private readonly bool endAllDay;
    private readonly string location;
    private readonly string organizer;
    private readonly string[] attendees;
    private readonly string description;
    private readonly double latitude;
    private readonly double longitude;

    public CalendarParsedResult(
      string summary,
      string startString,
      string endString,
      string durationString,
      string location,
      string organizer,
      string[] attendees,
      string description,
      double latitude,
      double longitude)
      : base(ParsedResultType.Calendar)
    {
      this.summary = summary;
      try
      {
        this.start = CalendarParsedResult.ParseDate(startString);
      }
      catch (Exception ex)
      {
        throw new ArgumentException(ex.ToString());
      }
      if (endString == null)
      {
        long durationMs = CalendarParsedResult.ParseDurationMS(durationString);
        this.end = durationMs < 0L ? new DateTime?() : new DateTime?(this.start + new TimeSpan(0, 0, 0, 0, (int) durationMs));
      }
      else
      {
        try
        {
          this.end = new DateTime?(CalendarParsedResult.ParseDate(endString));
        }
        catch (Exception ex)
        {
          throw new ArgumentException(ex.ToString());
        }
      }
      this.startAllDay = startString.Length == 8;
      this.endAllDay = endString != null && endString.Length == 8;
      this.location = location;
      this.organizer = organizer;
      this.attendees = attendees;
      this.description = description;
      this.latitude = latitude;
      this.longitude = longitude;
    }

    public string Summary => this.summary;

    public DateTime Start => this.start;

    public bool IsStartAllDay() => this.startAllDay;

    public DateTime? End => this.end;

    public bool IsEndAllDay => this.endAllDay;

    public string Location => this.location;

    public string Organizer => this.organizer;

    public string[] Attendees => this.attendees;

    public string Description => this.description;

    public double Latitude => this.latitude;

    public double Longitude => this.longitude;

    public override string DisplayResult
    {
      get
      {
        StringBuilder result = new StringBuilder(100);
        ParsedResult.MaybeAppend(this.summary, result);
        ParsedResult.MaybeAppend(CalendarParsedResult.Format(this.startAllDay, new DateTime?(this.start)), result);
        ParsedResult.MaybeAppend(CalendarParsedResult.Format(this.endAllDay, this.end), result);
        ParsedResult.MaybeAppend(this.location, result);
        ParsedResult.MaybeAppend(this.organizer, result);
        ParsedResult.MaybeAppend(this.attendees, result);
        ParsedResult.MaybeAppend(this.description, result);
        return result.ToString();
      }
    }

    private static DateTime ParseDate(string when)
    {
      if (!CalendarParsedResult.DATE_TIME.Match(when).Success)
        throw new ArgumentException(string.Format("no date Format: {0}", (object) when));
      return when.Length == 8 ? DateTime.ParseExact(when, "yyyyMMdd", (IFormatProvider) CultureInfo.InvariantCulture) : (when.Length != 16 || when[15] != 'Z' ? DateTime.ParseExact(when, "yyyyMMdd'T'HHmmss", (IFormatProvider) CultureInfo.InvariantCulture) : TimeZoneInfo.ConvertTime(DateTime.ParseExact(when.Substring(0, 15), "yyyyMMdd'T'HHmmss", (IFormatProvider) CultureInfo.InvariantCulture), TimeZoneInfo.Local));
    }

    private static string Format(bool allDay, DateTime? date)
    {
      if (!date.HasValue)
        return (string) null;
      return allDay ? date.Value.ToString("D", (IFormatProvider) CultureInfo.CurrentCulture) : date.Value.ToString("F", (IFormatProvider) CultureInfo.CurrentCulture);
    }

    private static long ParseDurationMS(string durationString)
    {
      if (durationString == null)
        return -1;
      Match match = CalendarParsedResult.RFC2445_DURATION.Match(durationString);
      if (!match.Success)
        return -1;
      long durationMs = 0;
      for (int index = 0; index < CalendarParsedResult.RFC2445_DURATION_FIELD_UNITS.Length; ++index)
      {
        string s = match.Groups[index + 1].Value;
        if (!string.IsNullOrEmpty(s))
          durationMs += CalendarParsedResult.RFC2445_DURATION_FIELD_UNITS[index] * (long) int.Parse(s);
      }
      return durationMs;
    }
  }
}
