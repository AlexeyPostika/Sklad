// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Codabar
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  internal class Codabar : BarcodeCommon, IBarcode
  {
    private Dictionary<char, string> CodabarCode = new Dictionary<char, string>();

    public Codabar(string input) => this.rawData = input;

    private string EncodeCodabar()
    {
      if (this.rawData.Length < 2)
        this.Error("Data Format invalid. (Invalid length)");
      switch (this.rawData[0].ToString().ToUpper().Trim())
      {
        case "A":
        case "B":
        case "C":
        case "D":
          switch (this.rawData[this.rawData.Trim().Length - 1].ToString().ToUpper().Trim())
          {
            case "A":
            case "B":
            case "C":
            case "D":
              if (!this.IsNumeric(this.rawData.Trim().Substring(1, this.RawData.Trim().Length - 2)))
                this.Error("Data contains non-numeric characters.");
              string str1 = "";
              this.init_Codabar();
              foreach (char key in this.rawData)
                str1 = str1 + this.CodabarCode[key].ToString() + "0";
              string str2 = str1.Remove(str1.Length - 1);
              this.CodabarCode.Clear();
              this.rawData = this.rawData.Trim().Substring(1, this.RawData.Trim().Length - 2);
              return str2;
            default:
              this.Error("Data Format invalid. (Invalid STOP character)");
              goto case "A";
          }
        default:
          this.Error("Data Format invalid. (Invalid Start character)");
          goto case "A";
      }
    }

    private void init_Codabar()
    {
      this.CodabarCode.Clear();
      this.CodabarCode.Add('0', "101010011");
      this.CodabarCode.Add('1', "101011001");
      this.CodabarCode.Add('2', "101001011");
      this.CodabarCode.Add('3', "110010101");
      this.CodabarCode.Add('4', "101101001");
      this.CodabarCode.Add('5', "110101001");
      this.CodabarCode.Add('6', "100101011");
      this.CodabarCode.Add('7', "100101101");
      this.CodabarCode.Add('8', "100110101");
      this.CodabarCode.Add('9', "110100101");
      this.CodabarCode.Add('-', "101001101");
      this.CodabarCode.Add('$', "101100101");
      this.CodabarCode.Add(':', "1101011011");
      this.CodabarCode.Add('/', "1101101011");
      this.CodabarCode.Add('.', "1101101101");
      this.CodabarCode.Add('+', "101100110011");
      this.CodabarCode.Add('A', "1011001001");
      this.CodabarCode.Add('B', "1010010011");
      this.CodabarCode.Add('C', "1001001011");
      this.CodabarCode.Add('D', "1010011001");
      this.CodabarCode.Add('a', "1011001001");
      this.CodabarCode.Add('b', "1010010011");
      this.CodabarCode.Add('c', "1001001011");
      this.CodabarCode.Add('d', "1010011001");
    }

    public string EncodedValue => this.EncodeCodabar();
  }
}
