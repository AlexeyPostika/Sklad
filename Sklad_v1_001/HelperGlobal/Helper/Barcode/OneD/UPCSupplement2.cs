// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCSupplement2
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class UPCSupplement2 : BarcodeCommon, IBarcode
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
    private string[] UPC_SUPP_2 = new string[4]
    {
      "aa",
      "ab",
      "ba",
      "bb"
    };

    public UPCSupplement2(string input) => this.rawData = input;

    private string Encode_UPCSupplemental_2()
    {
      if (this.rawData.Length != 2)
        this.Error("Invalid data length. (Length = 2 required)");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      string str1 = "";
      try
      {
        str1 = this.UPC_SUPP_2[int.Parse(this.rawData.Trim()) % 4];
      }
      catch
      {
        this.Error("Invalid data. (Numeric only)");
      }
      string str2 = "1011";
      int index = 0;
      foreach (char ch in str1)
      {
        switch (ch)
        {
          case 'a':
            str2 += this.EAN_CodeA[int.Parse(this.rawData[index].ToString())];
            break;
          case 'b':
            str2 += this.EAN_CodeB[int.Parse(this.rawData[index].ToString())];
            break;
        }
        if (index++ == 0)
          str2 += "01";
      }
      return str2;
    }

    public string EncodedValue => this.Encode_UPCSupplemental_2();
  }
}
