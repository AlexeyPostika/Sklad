// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Client.Results.ExpandedProductResultParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.Client.Results
{
  public sealed class ExpandedProductResultParser : ResultParser
  {
    public override ParsedResult Parse(Result result)
    {
      if (result.BarcodeFormat != BarcodeFormat.RSSExpanded)
        return (ParsedResult) null;
      string massagedText = ResultParser.GetMassagedText(result);
      if (massagedText == null)
        return (ParsedResult) null;
      string productID = (string) null;
      string sscc = (string) null;
      string lotNumber = (string) null;
      string productionDate = (string) null;
      string packagingDate = (string) null;
      string bestBeforeDate = (string) null;
      string expirationDate = (string) null;
      string weight = (string) null;
      string weightType = (string) null;
      string weightIncrement = (string) null;
      string price = (string) null;
      string priceIncrement = (string) null;
      string priceCurrency = (string) null;
      Dictionary<string, string> uncommonAIs = new Dictionary<string, string>();
      int i1 = 0;
      while (i1 < massagedText.Length)
      {
        string aivalue = ExpandedProductResultParser.FindAIvalue(i1, massagedText);
        if (aivalue == null)
          return (ParsedResult) null;
        int i2 = i1 + (aivalue.Length + 2);
        string str = ExpandedProductResultParser.FindValue(i2, massagedText);
        i1 = i2 + str.Length;
        if ("00".Equals(aivalue))
          sscc = str;
        else if ("01".Equals(aivalue))
          productID = str;
        else if ("10".Equals(aivalue))
          lotNumber = str;
        else if ("11".Equals(aivalue))
          productionDate = str;
        else if ("13".Equals(aivalue))
          packagingDate = str;
        else if ("15".Equals(aivalue))
          bestBeforeDate = str;
        else if ("17".Equals(aivalue))
          expirationDate = str;
        else if ("3100".Equals(aivalue) || "3101".Equals(aivalue) || "3102".Equals(aivalue) || "3103".Equals(aivalue) || "3104".Equals(aivalue) || "3105".Equals(aivalue) || "3106".Equals(aivalue) || "3107".Equals(aivalue) || "3108".Equals(aivalue) || "3109".Equals(aivalue))
        {
          weight = str;
          weightType = "KG";
          weightIncrement = aivalue.Substring(3);
        }
        else if ("3200".Equals(aivalue) || "3201".Equals(aivalue) || "3202".Equals(aivalue) || "3203".Equals(aivalue) || "3204".Equals(aivalue) || "3205".Equals(aivalue) || "3206".Equals(aivalue) || "3207".Equals(aivalue) || "3208".Equals(aivalue) || "3209".Equals(aivalue))
        {
          weight = str;
          weightType = "LB";
          weightIncrement = aivalue.Substring(3);
        }
        else if ("3920".Equals(aivalue) || "3921".Equals(aivalue) || "3922".Equals(aivalue) || "3923".Equals(aivalue))
        {
          price = str;
          priceIncrement = aivalue.Substring(3);
        }
        else if ("3930".Equals(aivalue) || "3931".Equals(aivalue) || "3932".Equals(aivalue) || "3933".Equals(aivalue))
        {
          if (str.Length < 4)
            return (ParsedResult) null;
          price = str.Substring(3);
          priceCurrency = str.Substring(0, 3);
          priceIncrement = aivalue.Substring(3);
        }
        else
          uncommonAIs.Add(aivalue, str);
      }
      return (ParsedResult) new ExpandedProductParsedResult(massagedText, productID, sscc, lotNumber, productionDate, packagingDate, bestBeforeDate, expirationDate, weight, weightType, weightIncrement, price, priceIncrement, priceCurrency, uncommonAIs);
    }

    private static string FindAIvalue(int i, string rawText)
    {
      if (rawText[i] != '(')
        return (string) null;
      string str = rawText.Substring(i + 1);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < str.Length; ++index)
      {
        char ch = str[index];
        switch (ch)
        {
          case ')':
            return stringBuilder.ToString();
          case '0':
          case '1':
          case '2':
          case '3':
          case '4':
          case '5':
          case '6':
          case '7':
          case '8':
          case '9':
            stringBuilder.Append(ch);
            continue;
          default:
            return (string) null;
        }
      }
      return stringBuilder.ToString();
    }

    private static string FindValue(int i, string rawText)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string rawText1 = rawText.Substring(i);
      for (int index = 0; index < rawText1.Length; ++index)
      {
        char ch = rawText1[index];
        if (ch == '(')
        {
          if (ExpandedProductResultParser.FindAIvalue(index, rawText1) == null)
            stringBuilder.Append('(');
          else
            break;
        }
        else
          stringBuilder.Append(ch);
      }
      return stringBuilder.ToString();
    }
  }
}
