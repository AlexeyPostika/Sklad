// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.FIM
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class FIM : BarcodeCommon, IBarcode
  {
    private string[] FIM_Codes = new string[4]
    {
      "110010011",
      "101101101",
      "110101011",
      "111010111"
    };

    public FIM(string input)
    {
      input = input.Trim();
      switch (input)
      {
        case "A":
        case "a":
          this.rawData = this.FIM_Codes[0];
          break;
        case "B":
        case "b":
          this.rawData = this.FIM_Codes[1];
          break;
        case "C":
        case "c":
          this.rawData = this.FIM_Codes[2];
          break;
        case "D":
        case "d":
          this.rawData = this.FIM_Codes[3];
          break;
        default:
          this.Error("Could not determine encoding type. (Only pass in A, B, C, or D)");
          break;
      }
    }

    public string Encode_FIM()
    {
      string str = "";
      foreach (char ch in this.RawData)
        str = str + (object) ch + "0";
      return str.Substring(0, str.Length - 1);
    }

    public string EncodedValue => this.Encode_FIM();

    public enum FIMTypes
    {
      FIM_A,
      FIM_B,
      FIM_C,
      FIM_D,
    }
  }
}
