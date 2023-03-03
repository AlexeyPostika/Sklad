// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.FieldParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class FieldParser
  {
    private static readonly object VariableLength = new object();
    private static readonly object[][] TwoDigitDataLength = new object[24][]
    {
      new object[2]{ (object) "00", (object) 18 },
      new object[2]{ (object) "01", (object) 14 },
      new object[2]{ (object) "02", (object) 14 },
      new object[3]
      {
        (object) "10",
        FieldParser.VariableLength,
        (object) 20
      },
      new object[2]{ (object) "11", (object) 6 },
      new object[2]{ (object) "12", (object) 6 },
      new object[2]{ (object) "13", (object) 6 },
      new object[2]{ (object) "15", (object) 6 },
      new object[2]{ (object) "17", (object) 6 },
      new object[2]{ (object) "20", (object) 2 },
      new object[3]
      {
        (object) "21",
        FieldParser.VariableLength,
        (object) 20
      },
      new object[3]
      {
        (object) "22",
        FieldParser.VariableLength,
        (object) 29
      },
      new object[3]
      {
        (object) "30",
        FieldParser.VariableLength,
        (object) 8
      },
      new object[3]
      {
        (object) "37",
        FieldParser.VariableLength,
        (object) 8
      },
      new object[3]
      {
        (object) "90",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "91",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "92",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "93",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "94",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "95",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "96",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "97",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "98",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "99",
        FieldParser.VariableLength,
        (object) 30
      }
    };
    private static readonly object[][] ThreeDigitDataLength = new object[23][]
    {
      new object[3]
      {
        (object) "240",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "241",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "242",
        FieldParser.VariableLength,
        (object) 6
      },
      new object[3]
      {
        (object) "250",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "251",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "253",
        FieldParser.VariableLength,
        (object) 17
      },
      new object[3]
      {
        (object) "254",
        FieldParser.VariableLength,
        (object) 20
      },
      new object[3]
      {
        (object) "400",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "401",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[2]{ (object) "402", (object) 17 },
      new object[3]
      {
        (object) "403",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[2]{ (object) "410", (object) 13 },
      new object[2]{ (object) "411", (object) 13 },
      new object[2]{ (object) "412", (object) 13 },
      new object[2]{ (object) "413", (object) 13 },
      new object[2]{ (object) "414", (object) 13 },
      new object[3]
      {
        (object) "420",
        FieldParser.VariableLength,
        (object) 20
      },
      new object[3]
      {
        (object) "421",
        FieldParser.VariableLength,
        (object) 15
      },
      new object[2]{ (object) "422", (object) 3 },
      new object[3]
      {
        (object) "423",
        FieldParser.VariableLength,
        (object) 15
      },
      new object[2]{ (object) "424", (object) 3 },
      new object[2]{ (object) "425", (object) 3 },
      new object[2]{ (object) "426", (object) 3 }
    };
    private static readonly object[][] ThreeDigitPlusDigitDataLength = new object[57][]
    {
      new object[2]{ (object) "310", (object) 6 },
      new object[2]{ (object) "311", (object) 6 },
      new object[2]{ (object) "312", (object) 6 },
      new object[2]{ (object) "313", (object) 6 },
      new object[2]{ (object) "314", (object) 6 },
      new object[2]{ (object) "315", (object) 6 },
      new object[2]{ (object) "316", (object) 6 },
      new object[2]{ (object) "320", (object) 6 },
      new object[2]{ (object) "321", (object) 6 },
      new object[2]{ (object) "322", (object) 6 },
      new object[2]{ (object) "323", (object) 6 },
      new object[2]{ (object) "324", (object) 6 },
      new object[2]{ (object) "325", (object) 6 },
      new object[2]{ (object) "326", (object) 6 },
      new object[2]{ (object) "327", (object) 6 },
      new object[2]{ (object) "328", (object) 6 },
      new object[2]{ (object) "329", (object) 6 },
      new object[2]{ (object) "330", (object) 6 },
      new object[2]{ (object) "331", (object) 6 },
      new object[2]{ (object) "332", (object) 6 },
      new object[2]{ (object) "333", (object) 6 },
      new object[2]{ (object) "334", (object) 6 },
      new object[2]{ (object) "335", (object) 6 },
      new object[2]{ (object) "336", (object) 6 },
      new object[2]{ (object) "340", (object) 6 },
      new object[2]{ (object) "341", (object) 6 },
      new object[2]{ (object) "342", (object) 6 },
      new object[2]{ (object) "343", (object) 6 },
      new object[2]{ (object) "344", (object) 6 },
      new object[2]{ (object) "345", (object) 6 },
      new object[2]{ (object) "346", (object) 6 },
      new object[2]{ (object) "347", (object) 6 },
      new object[2]{ (object) "348", (object) 6 },
      new object[2]{ (object) "349", (object) 6 },
      new object[2]{ (object) "350", (object) 6 },
      new object[2]{ (object) "351", (object) 6 },
      new object[2]{ (object) "352", (object) 6 },
      new object[2]{ (object) "353", (object) 6 },
      new object[2]{ (object) "354", (object) 6 },
      new object[2]{ (object) "355", (object) 6 },
      new object[2]{ (object) "356", (object) 6 },
      new object[2]{ (object) "357", (object) 6 },
      new object[2]{ (object) "360", (object) 6 },
      new object[2]{ (object) "361", (object) 6 },
      new object[2]{ (object) "362", (object) 6 },
      new object[2]{ (object) "363", (object) 6 },
      new object[2]{ (object) "364", (object) 6 },
      new object[2]{ (object) "365", (object) 6 },
      new object[2]{ (object) "366", (object) 6 },
      new object[2]{ (object) "367", (object) 6 },
      new object[2]{ (object) "368", (object) 6 },
      new object[2]{ (object) "369", (object) 6 },
      new object[3]
      {
        (object) "390",
        FieldParser.VariableLength,
        (object) 15
      },
      new object[3]
      {
        (object) "391",
        FieldParser.VariableLength,
        (object) 18
      },
      new object[3]
      {
        (object) "392",
        FieldParser.VariableLength,
        (object) 15
      },
      new object[3]
      {
        (object) "393",
        FieldParser.VariableLength,
        (object) 18
      },
      new object[3]
      {
        (object) "703",
        FieldParser.VariableLength,
        (object) 30
      }
    };
    private static readonly object[][] FourDigitDataLength = new object[18][]
    {
      new object[2]{ (object) "7001", (object) 13 },
      new object[3]
      {
        (object) "7002",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[2]{ (object) "7003", (object) 10 },
      new object[2]{ (object) "8001", (object) 14 },
      new object[3]
      {
        (object) "8002",
        FieldParser.VariableLength,
        (object) 20
      },
      new object[3]
      {
        (object) "8003",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "8004",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[2]{ (object) "8005", (object) 6 },
      new object[2]{ (object) "8006", (object) 18 },
      new object[3]
      {
        (object) "8007",
        FieldParser.VariableLength,
        (object) 30
      },
      new object[3]
      {
        (object) "8008",
        FieldParser.VariableLength,
        (object) 12
      },
      new object[2]{ (object) "8018", (object) 18 },
      new object[3]
      {
        (object) "8020",
        FieldParser.VariableLength,
        (object) 25
      },
      new object[2]{ (object) "8100", (object) 6 },
      new object[2]{ (object) "8101", (object) 10 },
      new object[2]{ (object) "8102", (object) 2 },
      new object[3]
      {
        (object) "8110",
        FieldParser.VariableLength,
        (object) 70
      },
      new object[3]
      {
        (object) "8200",
        FieldParser.VariableLength,
        (object) 70
      }
    };

    private FieldParser()
    {
    }

    internal static string ParseFieldsInGeneralPurpose(string rawInformation)
    {
      if (rawInformation.Length == 0)
        return "";
      string str1 = rawInformation.Length >= 2 ? rawInformation.Substring(0, 2) : throw NotFoundException.Instance;
      for (int index = 0; index < FieldParser.TwoDigitDataLength.Length; ++index)
      {
        if (FieldParser.TwoDigitDataLength[index][0].Equals((object) str1))
          return FieldParser.TwoDigitDataLength[index][1] == FieldParser.VariableLength ? FieldParser.ProcessVariableAI(2, (int) FieldParser.TwoDigitDataLength[index][2], rawInformation) : FieldParser.ProcessFixedAI(2, (int) FieldParser.TwoDigitDataLength[index][1], rawInformation);
      }
      string str2 = rawInformation.Length >= 3 ? rawInformation.Substring(0, 3) : throw NotFoundException.Instance;
      for (int index = 0; index < FieldParser.ThreeDigitDataLength.Length; ++index)
      {
        if (FieldParser.ThreeDigitDataLength[index][0].Equals((object) str2))
          return FieldParser.ThreeDigitDataLength[index][1] == FieldParser.VariableLength ? FieldParser.ProcessVariableAI(3, (int) FieldParser.ThreeDigitDataLength[index][2], rawInformation) : FieldParser.ProcessFixedAI(3, (int) FieldParser.ThreeDigitDataLength[index][1], rawInformation);
      }
      for (int index = 0; index < FieldParser.ThreeDigitPlusDigitDataLength.Length; ++index)
      {
        if (FieldParser.ThreeDigitPlusDigitDataLength[index][0].Equals((object) str2))
          return FieldParser.ThreeDigitPlusDigitDataLength[index][1] == FieldParser.VariableLength ? FieldParser.ProcessVariableAI(4, (int) FieldParser.ThreeDigitPlusDigitDataLength[index][2], rawInformation) : FieldParser.ProcessFixedAI(4, (int) FieldParser.ThreeDigitPlusDigitDataLength[index][1], rawInformation);
      }
      string str3 = rawInformation.Length >= 4 ? rawInformation.Substring(0, 4) : throw NotFoundException.Instance;
      for (int index = 0; index < FieldParser.FourDigitDataLength.Length; ++index)
      {
        if (FieldParser.FourDigitDataLength[index][0].Equals((object) str3))
          return FieldParser.FourDigitDataLength[index][1] == FieldParser.VariableLength ? FieldParser.ProcessVariableAI(4, (int) FieldParser.FourDigitDataLength[index][2], rawInformation) : FieldParser.ProcessFixedAI(4, (int) FieldParser.FourDigitDataLength[index][1], rawInformation);
      }
      throw NotFoundException.Instance;
    }

    private static string ProcessFixedAI(int aiSize, int fieldSize, string rawInformation)
    {
      if (rawInformation.Length < aiSize)
        throw NotFoundException.Instance;
      string str1 = rawInformation.Substring(0, aiSize);
      if (rawInformation.Length < aiSize + fieldSize)
        throw NotFoundException.Instance;
      string str2 = rawInformation.Substring(aiSize, aiSize + fieldSize - aiSize);
      string rawInformation1 = rawInformation.Substring(aiSize + fieldSize);
      return '('.ToString() + str1 + (object) ')' + str2 + FieldParser.ParseFieldsInGeneralPurpose(rawInformation1);
    }

    private static string ProcessVariableAI(
      int aiSize,
      int variableFieldSize,
      string rawInformation)
    {
      string str1 = rawInformation.Substring(0, aiSize);
      int startIndex = rawInformation.Length >= aiSize + variableFieldSize ? aiSize + variableFieldSize : rawInformation.Length;
      string str2 = rawInformation.Substring(aiSize, startIndex - aiSize);
      string rawInformation1 = rawInformation.Substring(startIndex);
      return '('.ToString() + str1 + (object) ')' + str2 + FieldParser.ParseFieldsInGeneralPurpose(rawInformation1);
    }
  }
}
