// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Standard2of5
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class Standard2of5 : BarcodeCommon, IBarcode
  {
    private string[] S25_Code = new string[10]
    {
      "11101010101110",
      "10111010101110",
      "11101110101010",
      "10101110101110",
      "11101011101010",
      "10111011101010",
      "10101011101110",
      "10101110111010",
      "11101010111010",
      "10111010111010"
    };

    public Standard2of5(string input) => this.rawData = input;

    private string Encode_Standard2of5()
    {
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      string str = "11011010";
      foreach (char ch in this.rawData)
        str += this.S25_Code[int.Parse(ch.ToString())];
      return str + "1101011";
    }

    public string EncodedValue => this.Encode_Standard2of5();
  }
}
