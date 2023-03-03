// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.CharacterSetECI
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Common
{
  public sealed class CharacterSetECI : ECI
  {
    private static Dictionary<int, CharacterSetECI> ValuetoEci;
    private static Dictionary<string, CharacterSetECI> NameToEci;
    private string encodingName;

    public string EncodingName => this.encodingName;

    private static void Initialize()
    {
      CharacterSetECI.ValuetoEci = new Dictionary<int, CharacterSetECI>(29);
      CharacterSetECI.NameToEci = new Dictionary<string, CharacterSetECI>(29);
      CharacterSetECI.AddCharacterSet(0, "Cp437");
      CharacterSetECI.AddCharacterSet(1, new string[2]
      {
        "ISO-8859-1",
        "ISO8859_1"
      });
      CharacterSetECI.AddCharacterSet(2, "Cp437");
      CharacterSetECI.AddCharacterSet(3, new string[2]
      {
        "ISO-8859-1",
        "ISO8859_1"
      });
      CharacterSetECI.AddCharacterSet(4, new string[2]
      {
        "ISO-8859-2",
        "ISO8859_2"
      });
      CharacterSetECI.AddCharacterSet(5, new string[2]
      {
        "ISO-8859-3",
        "ISO8859_3"
      });
      CharacterSetECI.AddCharacterSet(6, new string[2]
      {
        "ISO-8859-4",
        "ISO8859_4"
      });
      CharacterSetECI.AddCharacterSet(7, new string[2]
      {
        "ISO-8859-5",
        "ISO8859_5"
      });
      CharacterSetECI.AddCharacterSet(8, new string[2]
      {
        "ISO-8859-6",
        "ISO8859_6"
      });
      CharacterSetECI.AddCharacterSet(9, new string[2]
      {
        "ISO-8859-7",
        "ISO8859_7"
      });
      CharacterSetECI.AddCharacterSet(10, new string[2]
      {
        "ISO-8859-8",
        "ISO8859_8"
      });
      CharacterSetECI.AddCharacterSet(11, new string[2]
      {
        "ISO-8859-9",
        "ISO8859_9"
      });
      CharacterSetECI.AddCharacterSet(12, new string[3]
      {
        "ISO-8859-4",
        "ISO-8859-10",
        "ISO8859_10"
      });
      CharacterSetECI.AddCharacterSet(13, new string[2]
      {
        "ISO-8859-11",
        "ISO8859_11"
      });
      CharacterSetECI.AddCharacterSet(15, new string[2]
      {
        "ISO-8859-13",
        "ISO8859_13"
      });
      CharacterSetECI.AddCharacterSet(16, new string[3]
      {
        "ISO-8859-1",
        "ISO-8859-14",
        "ISO8859_14"
      });
      CharacterSetECI.AddCharacterSet(17, new string[2]
      {
        "ISO-8859-15",
        "ISO8859_15"
      });
      CharacterSetECI.AddCharacterSet(18, new string[3]
      {
        "ISO-8859-3",
        "ISO-8859-16",
        "ISO8859_16"
      });
      CharacterSetECI.AddCharacterSet(20, new string[3]
      {
        "SHIFT-JIS",
        "SJIS",
        "Shift_JIS"
      });
      CharacterSetECI.AddCharacterSet(21, new string[2]
      {
        "windows-1250",
        "CP1250"
      });
      CharacterSetECI.AddCharacterSet(22, new string[2]
      {
        "windows-1251",
        "CP1251"
      });
      CharacterSetECI.AddCharacterSet(23, new string[2]
      {
        "windows-1252",
        "CP1252"
      });
      CharacterSetECI.AddCharacterSet(24, new string[2]
      {
        "windows-1256",
        "CP1256"
      });
      CharacterSetECI.AddCharacterSet(25, new string[2]
      {
        "UTF-16BE",
        "UnicodeBig"
      });
      CharacterSetECI.AddCharacterSet(26, new string[2]
      {
        "UTF-8",
        "UTF8"
      });
      CharacterSetECI.AddCharacterSet(27, "US-ASCII");
      CharacterSetECI.AddCharacterSet(170, "US-ASCII");
      CharacterSetECI.AddCharacterSet(28, "BIG5");
      CharacterSetECI.AddCharacterSet(29, new string[4]
      {
        "GB18030",
        "GB2312",
        "EUC_CN",
        "GBK"
      });
      CharacterSetECI.AddCharacterSet(30, new string[2]
      {
        "EUC-KR",
        "EUC_KR"
      });
    }

    private CharacterSetECI(int value, string encodingName)
      : base(value)
    {
      this.encodingName = encodingName;
    }

    private static void AddCharacterSet(int value, string encodingName)
    {
      CharacterSetECI characterSetEci = new CharacterSetECI(value, encodingName);
      CharacterSetECI.ValuetoEci[value] = characterSetEci;
      CharacterSetECI.NameToEci[encodingName] = characterSetEci;
    }

    private static void AddCharacterSet(int value, string[] encodingNames)
    {
      CharacterSetECI characterSetEci = new CharacterSetECI(value, encodingNames[0]);
      CharacterSetECI.ValuetoEci[value] = characterSetEci;
      for (int index = 0; index < encodingNames.Length; ++index)
        CharacterSetECI.NameToEci[encodingNames[index]] = characterSetEci;
    }

    public static CharacterSetECI GetCharacterSetECIByValue(int value)
    {
      if (CharacterSetECI.ValuetoEci == null)
        CharacterSetECI.Initialize();
      if (value < 0 || value >= 900)
        throw FormatException.Instance;
      return CharacterSetECI.ValuetoEci.ContainsKey(value) ? CharacterSetECI.ValuetoEci[value] : (CharacterSetECI) null;
    }

    public static CharacterSetECI GetCharacterSetECIByName(string name)
    {
      if (CharacterSetECI.NameToEci == null)
        CharacterSetECI.Initialize();
      return CharacterSetECI.NameToEci.ContainsKey(name) ? CharacterSetECI.NameToEci[name] : (CharacterSetECI) null;
    }
  }
}
