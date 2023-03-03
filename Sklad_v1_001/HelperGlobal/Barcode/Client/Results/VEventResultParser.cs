// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.VEventResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class VEventResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string text = result.Text;
      if (text == null)
        return (ParsedResult) null;
      if (text.IndexOf("BEGIN:VEVENT") < 0)
        return (ParsedResult) null;
      string summary = VEventResultParser.MatchSingleVCardPrefixedField("SUMMARY", text, true);
      string startString = VEventResultParser.MatchSingleVCardPrefixedField("DTSTART", text, true);
      if (startString == null)
        return (ParsedResult) null;
      string endString = VEventResultParser.MatchSingleVCardPrefixedField("DTEND", text, true);
      string durationString = VEventResultParser.MatchSingleVCardPrefixedField("DURATION", text, true);
      string location = VEventResultParser.MatchSingleVCardPrefixedField("LOCATION", text, true);
      string organizer = VEventResultParser.StripMailto(VEventResultParser.MatchSingleVCardPrefixedField("ORGANIZER", text, true));
      string[] attendees = VEventResultParser.matchVCardPrefixedField("ATTENDEE", text, true);
      if (attendees != null)
      {
        for (int index = 0; index < attendees.Length; ++index)
          attendees[index] = VEventResultParser.StripMailto(attendees[index]);
      }
      string description = VEventResultParser.MatchSingleVCardPrefixedField("DESCRIPTION", text, true);
      string str = VEventResultParser.MatchSingleVCardPrefixedField("GEO", text, true);
      double result1;
      double result2;
      if (str == null)
      {
        result1 = double.NaN;
        result2 = double.NaN;
      }
      else
      {
        int length = str.IndexOf(';');
        if (!double.TryParse(str.Substring(0, length), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
          return (ParsedResult) null;
        if (!double.TryParse(str.Substring(length + 1), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
          return (ParsedResult) null;
      }
      try
      {
        return (ParsedResult) new CalendarParsedResult(summary, startString, endString, durationString, location, organizer, attendees, description, result1, result2);
      }
      catch (ArgumentException ex)
      {
        return (ParsedResult) null;
      }
    }

    private static string MatchSingleVCardPrefixedField(string prefix, string rawText, bool trim)
    {
      List<string> stringList = VCardResultParser.MatchSingleVCardPrefixedField(prefix, rawText, trim, false);
      return stringList != null && stringList.Count != 0 ? stringList[0] : (string) null;
    }

    private static string[] matchVCardPrefixedField(string prefix, string rawText, bool trim)
    {
      List<List<string>> stringListList = VCardResultParser.MatchVCardPrefixedField(prefix, rawText, trim, false);
      if (stringListList == null || stringListList.Count == 0)
        return (string[]) null;
      int count = stringListList.Count;
      string[] strArray = new string[count];
      for (int index = 0; index < count; ++index)
        strArray[index] = stringListList[index][0];
      return strArray;
    }

    private static string StripMailto(string s)
    {
      if (s != null && (s.StartsWith("mailto:") || s.StartsWith("MAILTO:")))
        s = s.Substring(7);
      return s;
    }
  }
}
