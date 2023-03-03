// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.MSI
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.OneD
{
  internal class MSI : BarcodeCommon, IBarcode
  {
    private string[] MSI_Code = new string[10]
    {
      "100100100100",
      "100100100110",
      "100100110100",
      "100100110110",
      "100110100100",
      "100110100110",
      "100110110100",
      "100110110110",
      "110100100100",
      "110100100110"
    };
    private BarcodeFormat Encoded_Type;

    public MSI(string input, BarcodeFormat EncodedType)
    {
      this.Encoded_Type = EncodedType;
      this.rawData = input;
    }

    private string Encode_MSI()
    {
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      string rawData = this.rawData;
      if (this.Encoded_Type == BarcodeFormat.MSIMod10 || this.Encoded_Type == BarcodeFormat.MSI2Mod10)
      {
        string s = "";
        string str1 = "";
        for (int index = rawData.Length - 1; index >= 0; index -= 2)
        {
          s = rawData[index].ToString() + s;
          if (index - 1 >= 0)
            str1 = rawData[index - 1].ToString() + str1;
        }
        string str2 = Convert.ToString(int.Parse(s) * 2);
        int num1 = 0;
        int num2 = 0;
        foreach (char ch in str1)
          num1 += int.Parse(ch.ToString());
        foreach (char ch in str2)
          num2 += int.Parse(ch.ToString());
        int num3 = 10 - (num2 + num1) % 10;
        rawData += num3.ToString();
      }
      if (this.Encoded_Type == BarcodeFormat.MSIMod11 || this.Encoded_Type == BarcodeFormat.MSIMod11Mod10)
      {
        int num4 = 0;
        int num5 = 2;
        for (int index = rawData.Length - 1; index >= 0; --index)
        {
          if (num5 > 7)
            num5 = 2;
          num4 += int.Parse(rawData[index].ToString()) * num5++;
        }
        int num6 = 11 - num4 % 11;
        rawData += num6.ToString();
      }
      if (this.Encoded_Type == BarcodeFormat.MSI2Mod10 || this.Encoded_Type == BarcodeFormat.MSIMod11Mod10)
      {
        string s = "";
        string str3 = "";
        for (int index = rawData.Length - 1; index >= 0; index -= 2)
        {
          s = rawData[index].ToString() + s;
          if (index - 1 >= 0)
            str3 = rawData[index - 1].ToString() + str3;
        }
        string str4 = Convert.ToString(int.Parse(s) * 2);
        int num7 = 0;
        int num8 = 0;
        foreach (char ch in str3)
          num7 += int.Parse(ch.ToString());
        foreach (char ch in str4)
          num8 += int.Parse(ch.ToString());
        int num9 = 10 - (num8 + num7) % 10;
        rawData += num9.ToString();
      }
      string str = "110";
      foreach (char ch in rawData)
        str += this.MSI_Code[int.Parse(ch.ToString())];
      return str + "1001";
    }

    public string EncodedValue => this.Encode_MSI();
  }
}
