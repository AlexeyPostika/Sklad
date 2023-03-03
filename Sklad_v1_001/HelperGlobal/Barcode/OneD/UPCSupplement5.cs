// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCSupplement5
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class UPCSupplement5 : BarcodeCommon, IBarcode
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
    private string[] UPC_SUPP_5 = new string[10]
    {
      "bbaaa",
      "babaa",
      "baaba",
      "baaab",
      "abbaa",
      "aabba",
      "aaabb",
      "ababa",
      "abaab",
      "aabab"
    };

    public UPCSupplement5(string input) => this.rawData = input;

    private string Encode_UPCSupplemental_5()
    {
      if (this.rawData.Length != 5)
        this.Error("Invalid data length. (Length = 5 required)");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      int num1 = 0;
      int num2 = 0;
      for (int startIndex = 0; startIndex <= 4; startIndex += 2)
        num2 += int.Parse(this.rawData.Substring(startIndex, 1)) * 3;
      for (int startIndex = 1; startIndex < 4; startIndex += 2)
        num1 += int.Parse(this.rawData.Substring(startIndex, 1)) * 9;
      string str1 = this.UPC_SUPP_5[(num1 + num2) % 10];
      string str2 = "";
      int index = 0;
      foreach (char ch in str1)
      {
        str2 = index != 0 ? str2 + "01" : str2 + "1011";
        switch (ch)
        {
          case 'a':
            str2 += this.EAN_CodeA[int.Parse(this.rawData[index].ToString())];
            break;
          case 'b':
            str2 += this.EAN_CodeB[int.Parse(this.rawData[index].ToString())];
            break;
        }
        ++index;
      }
      return str2;
    }

    public string EncodedValue => this.Encode_UPCSupplemental_5();
  }
}
