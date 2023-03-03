// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Postnet
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.OneD
{
  internal class Postnet : BarcodeCommon, IBarcode
  {
    private string[] POSTNET_Code = new string[10]
    {
      "11000",
      "00011",
      "00101",
      "00110",
      "01001",
      "01010",
      "01100",
      "10001",
      "10010",
      "10100"
    };

    public Postnet(string input) => this.rawData = input;

    private string Encode_Postnet()
    {
      this.rawData = this.rawData.Replace("-", "");
      switch (this.rawData.Length)
      {
        case 5:
        case 6:
        case 9:
        case 11:
          string str = "1";
          int num1 = 0;
          foreach (char ch in this.rawData)
          {
            try
            {
              int int32 = Convert.ToInt32(ch.ToString());
              str += this.POSTNET_Code[int32];
              num1 += int32;
            }
            catch (Exception ex)
            {
              this.Error("Invalid data. (Numeric only) --> " + ex.Message);
            }
          }
          int num2 = num1 % 10;
          int index = 10 - (num2 == 0 ? 10 : num2);
          return str + this.POSTNET_Code[index] + "1";
        default:
          this.Error("Invalid data length. (5, 6, 9, or 11 digits only)");
          goto case 5;
      }
    }

    public string EncodedValue => this.Encode_Postnet();
  }
}
