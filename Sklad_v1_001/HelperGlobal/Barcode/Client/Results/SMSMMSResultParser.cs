// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.SMSMMSResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class SMSMMSResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      string massagedText = ResultParser.GetMassagedText(result);
      if (!massagedText.StartsWith("sms:") && !massagedText.StartsWith("SMS:") && !massagedText.StartsWith("mms:") && !massagedText.StartsWith("MMS:"))
        return (ParsedResult) null;
      IDictionary<string, string> nameValuePairs = ResultParser.ParseNameValuePairs(massagedText);
      string subject = (string) null;
      string body = (string) null;
      bool flag = false;
      if (nameValuePairs != null && nameValuePairs.Count != 0)
      {
        subject = nameValuePairs["subject"];
        body = nameValuePairs["body"];
        flag = true;
      }
      int num1 = massagedText.IndexOf('?', 4);
      string str = num1 < 0 || !flag ? massagedText.Substring(4) : massagedText.Substring(4, num1 - 4);
      int num2 = -1;
      List<string> stringList1 = new List<string>(1);
      List<string> stringList2 = new List<string>(1);
      int num3;
      for (; (num3 = str.IndexOf(',', num2 + 1)) > num2; num2 = num3)
      {
        string numberPart = str.Substring(num2 + 1, num3 - (num2 + 1));
        SMSMMSResultParser.AddNumberVia((ICollection<string>) stringList1, (ICollection<string>) stringList2, numberPart);
      }
      SMSMMSResultParser.AddNumberVia((ICollection<string>) stringList1, (ICollection<string>) stringList2, str.Substring(num2 + 1));
      return (ParsedResult) new SMSParsedResult(ResultParser.ToStringArray(stringList1), ResultParser.ToStringArray(stringList2), subject, body);
    }

    private static void AddNumberVia(
      ICollection<string> numbers,
      ICollection<string> vias,
      string numberPart)
    {
      int length = numberPart.IndexOf(';');
      if (length < 0)
      {
        numbers.Add(numberPart);
        vias.Add((string) null);
      }
      else
      {
        numbers.Add(numberPart.Substring(0, length));
        string str1 = numberPart.Substring(length + 1);
        string str2 = !str1.StartsWith("via=") ? (string) null : str1.Substring(4);
        vias.Add(str2);
      }
    }
  }
}
