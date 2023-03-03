// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Interleaved2of5
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class Interleaved2of5 : BarcodeCommon, IBarcode
  {
    private string[] I25_Code = new string[10]
    {
      "NNWWN",
      "WNNNW",
      "NWNNW",
      "WWNNN",
      "NNWNW",
      "WNWNN",
      "NWWNN",
      "NNNWW",
      "WNNWN",
      "NWNWN"
    };

    public Interleaved2of5(string input) => this.rawData = input;

    private string Encode_Interleaved2of5()
    {
      if (this.rawData.Length % 2 != 0)
        this.Error("Data length invalid.");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      string str1 = "1010";
      for (int index = 0; index < this.rawData.Length; index += 2)
      {
        bool flag = true;
        string str2 = this.I25_Code[int.Parse(this.rawData[index].ToString())];
        string str3 = this.I25_Code[int.Parse(this.rawData[index + 1].ToString())];
        string str4 = "";
        while (str2.Length > 0)
        {
          str4 = str4 + str2[0].ToString() + str3[0].ToString();
          str2 = str2.Substring(1);
          str3 = str3.Substring(1);
        }
        foreach (char ch in str4)
        {
          str1 = !flag ? (ch != 'N' ? str1 + "00" : str1 + "0") : (ch != 'N' ? str1 + "11" : str1 + "1");
          flag = !flag;
        }
      }
      return str1 + "1101";
    }

    public string EncodedValue => this.Encode_Interleaved2of5();
  }
}
