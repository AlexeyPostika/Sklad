// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCE
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class UPCE : BarcodeCommon, IBarcode
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
    private string[] UPCE_Code_0 = new string[10]
    {
      "bbbaaa",
      "bbabaa",
      "bbaaba",
      "bbaaab",
      "babbaa",
      "baabba",
      "baaabb",
      "bababa",
      "babaab",
      "baabab"
    };
    private string[] UPCE_Code_1 = new string[10]
    {
      "aaabbb",
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

    public UPCE(string input) => this.rawData = input;

    private string Encode_UPCE()
    {
      if (this.rawData.Length != 6 && this.rawData.Length != 8 && this.rawData.Length != 12)
        this.Error("Invalid data length. (8 or 12 numbers only)");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric only.");
      int index1 = int.Parse(this.rawData[this.rawData.Length - 1].ToString());
      int num1 = int.Parse(this.rawData[0].ToString());
      if (this.rawData.Length == 12)
      {
        string str1 = "";
        string str2 = this.rawData.Substring(1, 5);
        string s = this.rawData.Substring(6, 5);
        if (num1 != 0 && num1 != 1)
          this.Error("Invalid Number System (only 0 & 1 are valid)");
        if (str2.EndsWith("000") || str2.EndsWith("100") || str2.EndsWith("200") && int.Parse(s) <= 999)
          str1 = str1 + str2.Substring(0, 2) + s.Substring(2, 3) + str2[2].ToString();
        else if (str2.EndsWith("00") && int.Parse(s) <= 99)
          str1 = str1 + str2.Substring(0, 3) + s.Substring(3, 2) + "3";
        else if (str2.EndsWith("0") && int.Parse(s) <= 9)
          str1 = str1 + str2.Substring(0, 4) + (object) s[4] + "4";
        else if (!str2.EndsWith("0") && int.Parse(s) <= 9 && int.Parse(s) >= 5)
          str1 = str1 + str2 + (object) s[4];
        else
          this.Error("Illegal UPC-A entered for conversion.  Unable to convert.");
        this.rawData = str1;
      }
      string str3 = num1 != 0 ? this.UPCE_Code_1[index1] : this.UPCE_Code_0[index1];
      string str4 = "101";
      int num2 = 0;
      foreach (char ch in str3)
      {
        int index2 = int.Parse(this.rawData[num2++].ToString());
        switch (ch)
        {
          case 'a':
            str4 += this.EAN_CodeA[index2];
            break;
          case 'b':
            str4 += this.EAN_CodeB[index2];
            break;
        }
      }
      return str4 + "01010" + "1";
    }

    public string EncodedValue => this.Encode_UPCE();
  }
}
