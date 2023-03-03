// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.EAN13
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  internal class EAN13 : BarcodeCommon, IBarcode
  {
    private string[] EAN_CodeA = new string[10]
    {
      "0001101",
      "0011001",
      "0010011",
      "0111101",
      "0100011",
      "0110001",
      "0101111",
      "0111011",
      "0110111",
      "0001011"
    };
    private string[] EAN_CodeB = new string[10]
    {
      "0100111",
      "0110011",
      "0011011",
      "0100001",
      "0011101",
      "0111001",
      "0000101",
      "0010001",
      "0001001",
      "0010111"
    };
    private string[] EAN_CodeC = new string[10]
    {
      "1110010",
      "1100110",
      "1101100",
      "1000010",
      "1011100",
      "1001110",
      "1010000",
      "1000100",
      "1001000",
      "1110100"
    };
    private string[] EAN_Pattern = new string[10]
    {
      "aaaaaa",
      "aababb",
      "aabbab",
      "aabbba",
      "abaabb",
      "abbaab",
      "abbbaa",
      "ababab",
      "ababba",
      "abbaba"
    };
    private Dictionary<string, string> CountryCodes = new Dictionary<string, string>();
    private string _Country_Assigning_Manufacturer_Code = "N/A";

    public EAN13(string input)
    {
      this.rawData = input;
      this.CheckDigit();
    }

    private string Encode_EAN13()
    {
      if (this.rawData.Length < 12 || this.rawData.Length > 13)
        this.Error("Data length invalid. (Length must be 12 or 13)");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      string str1 = this.EAN_Pattern[int.Parse(this.rawData[0].ToString())];
      string str2 = "101";
      for (int index = 0; index < 6; ++index)
      {
        if (str1[index] == 'a')
          str2 += this.EAN_CodeA[int.Parse(this.rawData[index + 1].ToString())];
        if (str1[index] == 'b')
          str2 += this.EAN_CodeB[int.Parse(this.rawData[index + 1].ToString())];
      }
      string str3 = str2 + "01010";
      int num = 1;
      while (num <= 5)
        str3 += this.EAN_CodeC[int.Parse(this.rawData[num++ + 6].ToString())];
      int index1 = int.Parse(this.rawData[this.rawData.Length - 1].ToString());
      string str4 = str3 + this.EAN_CodeC[index1] + "101";
      this.init_CountryCodes();
      this._Country_Assigning_Manufacturer_Code = "N/A";
      string key1 = this.rawData.Substring(0, 2);
      string key2 = this.rawData.Substring(0, 3);
      try
      {
        this._Country_Assigning_Manufacturer_Code = this.CountryCodes[key2].ToString();
      }
      catch
      {
        try
        {
          this._Country_Assigning_Manufacturer_Code = this.CountryCodes[key1].ToString();
        }
        catch
        {
          this.Error("Country assigning manufacturer code not found.");
        }
      }
      finally
      {
        this.CountryCodes.Clear();
      }
      return str4;
    }

    private void init_CountryCodes()
    {
      this.CountryCodes.Clear();
      this.CountryCodes.Add("00", "US / CANADA");
      this.CountryCodes.Add("01", "US / CANADA");
      this.CountryCodes.Add("02", "US / CANADA");
      this.CountryCodes.Add("03", "US / CANADA");
      this.CountryCodes.Add("04", "US / CANADA");
      this.CountryCodes.Add("05", "US / CANADA");
      this.CountryCodes.Add("06", "US / CANADA");
      this.CountryCodes.Add("07", "US / CANADA");
      this.CountryCodes.Add("08", "US / CANADA");
      this.CountryCodes.Add("09", "US / CANADA");
      this.CountryCodes.Add("10", "US / CANADA");
      this.CountryCodes.Add("11", "US / CANADA");
      this.CountryCodes.Add("12", "US / CANADA");
      this.CountryCodes.Add("13", "US / CANADA");
      this.CountryCodes.Add("20", "IN STORE");
      this.CountryCodes.Add("21", "IN STORE");
      this.CountryCodes.Add("22", "IN STORE");
      this.CountryCodes.Add("23", "IN STORE");
      this.CountryCodes.Add("24", "IN STORE");
      this.CountryCodes.Add("25", "IN STORE");
      this.CountryCodes.Add("26", "IN STORE");
      this.CountryCodes.Add("27", "IN STORE");
      this.CountryCodes.Add("28", "IN STORE");
      this.CountryCodes.Add("29", "IN STORE");
      this.CountryCodes.Add("30", "FRANCE");
      this.CountryCodes.Add("31", "FRANCE");
      this.CountryCodes.Add("32", "FRANCE");
      this.CountryCodes.Add("33", "FRANCE");
      this.CountryCodes.Add("34", "FRANCE");
      this.CountryCodes.Add("35", "FRANCE");
      this.CountryCodes.Add("36", "FRANCE");
      this.CountryCodes.Add("37", "FRANCE");
      this.CountryCodes.Add("40", "GERMANY");
      this.CountryCodes.Add("41", "GERMANY");
      this.CountryCodes.Add("42", "GERMANY");
      this.CountryCodes.Add("43", "GERMANY");
      this.CountryCodes.Add("44", "GERMANY");
      this.CountryCodes.Add("45", "JAPAN");
      this.CountryCodes.Add("46", "RUSSIAN FEDERATION");
      this.CountryCodes.Add("49", "JAPAN (JAN-13)");
      this.CountryCodes.Add("50", "UNITED KINGDOM");
      this.CountryCodes.Add("54", "BELGIUM / LUXEMBOURG");
      this.CountryCodes.Add("57", "DENMARK");
      this.CountryCodes.Add("64", "FINLAND");
      this.CountryCodes.Add("70", "NORWAY");
      this.CountryCodes.Add("73", "SWEDEN");
      this.CountryCodes.Add("76", "SWITZERLAND");
      this.CountryCodes.Add("80", "ITALY");
      this.CountryCodes.Add("81", "ITALY");
      this.CountryCodes.Add("82", "ITALY");
      this.CountryCodes.Add("83", "ITALY");
      this.CountryCodes.Add("84", "SPAIN");
      this.CountryCodes.Add("87", "NETHERLANDS");
      this.CountryCodes.Add("90", "AUSTRIA");
      this.CountryCodes.Add("91", "AUSTRIA");
      this.CountryCodes.Add("93", "AUSTRALIA");
      this.CountryCodes.Add("94", "NEW ZEALAND");
      this.CountryCodes.Add("99", "COUPONS");
      this.CountryCodes.Add("380", "BULGARIA");
      this.CountryCodes.Add("383", "SLOVENIJA");
      this.CountryCodes.Add("385", "CROATIA");
      this.CountryCodes.Add("387", "BOSNIA-HERZEGOVINA");
      this.CountryCodes.Add("460", "RUSSIA");
      this.CountryCodes.Add("461", "RUSSIA");
      this.CountryCodes.Add("462", "RUSSIA");
      this.CountryCodes.Add("463", "RUSSIA");
      this.CountryCodes.Add("464", "RUSSIA");
      this.CountryCodes.Add("465", "RUSSIA");
      this.CountryCodes.Add("466", "RUSSIA");
      this.CountryCodes.Add("467", "RUSSIA");
      this.CountryCodes.Add("468", "RUSSIA");
      this.CountryCodes.Add("469", "RUSSIA");
      this.CountryCodes.Add("471", "TAIWAN");
      this.CountryCodes.Add("474", "ESTONIA");
      this.CountryCodes.Add("475", "LATVIA");
      this.CountryCodes.Add("477", "LITHUANIA");
      this.CountryCodes.Add("479", "SRI LANKA");
      this.CountryCodes.Add("480", "PHILIPPINES");
      this.CountryCodes.Add("482", "UKRAINE");
      this.CountryCodes.Add("484", "MOLDOVA");
      this.CountryCodes.Add("485", "ARMENIA");
      this.CountryCodes.Add("486", "GEORGIA");
      this.CountryCodes.Add("487", "KAZAKHSTAN");
      this.CountryCodes.Add("489", "HONG KONG");
      this.CountryCodes.Add("520", "GREECE");
      this.CountryCodes.Add("528", "LEBANON");
      this.CountryCodes.Add("529", "CYPRUS");
      this.CountryCodes.Add("531", "MACEDONIA");
      this.CountryCodes.Add("535", "MALTA");
      this.CountryCodes.Add("539", "IRELAND");
      this.CountryCodes.Add("560", "PORTUGAL");
      this.CountryCodes.Add("569", "ICELAND");
      this.CountryCodes.Add("590", "POLAND");
      this.CountryCodes.Add("594", "ROMANIA");
      this.CountryCodes.Add("599", "HUNGARY");
      this.CountryCodes.Add("600", "SOUTH AFRICA");
      this.CountryCodes.Add("601", "SOUTH AFRICA");
      this.CountryCodes.Add("609", "MAURITIUS");
      this.CountryCodes.Add("611", "MOROCCO");
      this.CountryCodes.Add("613", "ALGERIA");
      this.CountryCodes.Add("619", "TUNISIA");
      this.CountryCodes.Add("622", "EGYPT");
      this.CountryCodes.Add("625", "JORDAN");
      this.CountryCodes.Add("626", "IRAN");
      this.CountryCodes.Add("627", "KUWAIT");
      this.CountryCodes.Add("628", "SAUDI ARABIA");
      this.CountryCodes.Add("629", "EMIRATES");
      this.CountryCodes.Add("690", "CHINA");
      this.CountryCodes.Add("691", "CHINA");
      this.CountryCodes.Add("692", "CHINA");
      this.CountryCodes.Add("693", "CHINA");
      this.CountryCodes.Add("694", "CHINA");
      this.CountryCodes.Add("695", "CHINA");
      this.CountryCodes.Add("729", "ISRAEL");
      this.CountryCodes.Add("740", "GUATEMALA");
      this.CountryCodes.Add("741", "EL SALVADOR");
      this.CountryCodes.Add("742", "HONDURAS");
      this.CountryCodes.Add("743", "NICARAGUA");
      this.CountryCodes.Add("744", "COSTA RICA");
      this.CountryCodes.Add("746", "DOMINICAN REPUBLIC");
      this.CountryCodes.Add("750", "MEXICO");
      this.CountryCodes.Add("759", "VENEZUELA");
      this.CountryCodes.Add("770", "COLOMBIA");
      this.CountryCodes.Add("773", "URUGUAY");
      this.CountryCodes.Add("775", "PERU");
      this.CountryCodes.Add("777", "BOLIVIA");
      this.CountryCodes.Add("779", "ARGENTINA");
      this.CountryCodes.Add("780", "CHILE");
      this.CountryCodes.Add("784", "PARAGUAY");
      this.CountryCodes.Add("785", "PERU");
      this.CountryCodes.Add("786", "ECUADOR");
      this.CountryCodes.Add("789", "BRAZIL");
      this.CountryCodes.Add("850", "CUBA");
      this.CountryCodes.Add("858", "SLOVAKIA");
      this.CountryCodes.Add("859", "CZECH REPUBLIC");
      this.CountryCodes.Add("860", "YUGLOSLAVIA");
      this.CountryCodes.Add("867", "NORTH KOREA");
      this.CountryCodes.Add("869", "TURKEY");
      this.CountryCodes.Add("880", "SOUTH KOREA");
      this.CountryCodes.Add("885", "THAILAND");
      this.CountryCodes.Add("888", "SINGAPORE");
      this.CountryCodes.Add("890", "INDIA");
      this.CountryCodes.Add("893", "VIETNAM");
      this.CountryCodes.Add("899", "INDONESIA");
      this.CountryCodes.Add("955", "MALAYSIA");
      this.CountryCodes.Add("958", "MACAU");
      this.CountryCodes.Add("977", "INTERNATIONAL STANDARD SERIAL NUMBER FOR PERIODICALS (ISSN)");
      this.CountryCodes.Add("978", "INTERNATIONAL STANDARD BOOK NUMBERING (ISBN)");
      this.CountryCodes.Add("979", "INTERNATIONAL STANDARD MUSIC NUMBER (ISMN)");
      this.CountryCodes.Add("980", "REFUND RECEIPTS");
      this.CountryCodes.Add("981", "COMMON CURRENCY COUPONS");
      this.CountryCodes.Add("982", "COMMON CURRENCY COUPONS");
    }

    private void CheckDigit()
    {
      try
      {
        string str = this.rawData.Substring(0, 12);
        int num1 = 0;
        int num2 = 0;
        for (int startIndex = 0; startIndex < str.Length; ++startIndex)
        {
          if (startIndex % 2 == 0)
            num2 += int.Parse(str.Substring(startIndex, 1));
          else
            num1 += int.Parse(str.Substring(startIndex, 1)) * 3;
        }
        int num3 = 10 - (num1 + num2) % 10;
        if (num3 == 10)
          num3 = 0;
        this.rawData = str + (object) num3.ToString()[0];
      }
      catch
      {
        this.Error("Error calculating check digit.");
      }
    }

    public string EncodedValue => this.Encode_EAN13();
  }
}
