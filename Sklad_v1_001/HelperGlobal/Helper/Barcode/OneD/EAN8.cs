// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.EAN8
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class EAN8 : BarcodeCommon, IBarcode
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

    public EAN8(string input)
    {
      this.rawData = input;
      this.CheckDigit();
    }

    private string Encode_EAN8()
    {
      if (this.rawData.Length != 8 && this.rawData.Length != 7)
        this.Error("Invalid data length. (7 or 8 numbers only)");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric only.");
      string str1 = "101";
      for (int index = 0; index < this.rawData.Length / 2; ++index)
        str1 += this.EAN_CodeA[int.Parse(this.rawData[index].ToString())];
      string str2 = str1 + "01010";
      for (int index = this.rawData.Length / 2; index < this.rawData.Length; ++index)
        str2 += this.EAN_CodeC[int.Parse(this.rawData[index].ToString())];
      return str2 + "101";
    }

    private void CheckDigit()
    {
      if (this.rawData.Length != 7)
        return;
      int num1 = 0;
      int num2 = 0;
      for (int startIndex = 0; startIndex <= 6; startIndex += 2)
        num2 += int.Parse(this.rawData.Substring(startIndex, 1)) * 3;
      for (int startIndex = 1; startIndex <= 5; startIndex += 2)
        num1 += int.Parse(this.rawData.Substring(startIndex, 1));
      int num3 = 10 - (num1 + num2) % 10;
      if (num3 == 10)
        num3 = 0;
      this.rawData += num3.ToString();
    }

    public string EncodedValue => this.Encode_EAN8();
  }
}
