// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code11
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class Code11 : BarcodeCommon, IBarcode
  {
    private string[] C11_Code = new string[12]
    {
      "101011",
      "1101011",
      "1001011",
      "1100101",
      "1011011",
      "1101101",
      "1001101",
      "1010011",
      "1101001",
      "110101",
      "101101",
      "1011001"
    };

    public Code11(string input) => this.rawData = input;

    private string Encode_Code11()
    {
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData.Replace("-", "")))
        this.Error("Numeric data and '-' Only");
      int num1 = 1;
      int num2 = 0;
      string rawData = this.rawData;
      for (int index = this.rawData.Length - 1; index >= 0; --index)
      {
        if (num1 == 10)
          num1 = 1;
        if (this.rawData[index] != '-')
          num2 += int.Parse(this.rawData[index].ToString()) * num1++;
        else
          num2 += 10 * num1++;
      }
      int num3 = num2 % 11;
      string str1 = rawData + num3.ToString();
      if (this.rawData.Length >= 1)
      {
        int num4 = 1;
        int num5 = 0;
        for (int index = str1.Length - 1; index >= 0; --index)
        {
          if (num4 == 9)
            num4 = 1;
          if (str1[index] != '-')
            num5 += int.Parse(str1[index].ToString()) * num4++;
          else
            num5 += 10 * num4++;
        }
        int num6 = num5 % 11;
        str1 += num6.ToString();
      }
      string str2 = "0";
      string str3 = this.C11_Code[11] + str2;
      foreach (char ch in str1)
      {
        int index = ch == '-' ? 10 : int.Parse(ch.ToString());
        str3 = str3 + this.C11_Code[index] + str2;
      }
      return str3 + this.C11_Code[11];
    }

    public string EncodedValue => this.Encode_Code11();
  }
}
