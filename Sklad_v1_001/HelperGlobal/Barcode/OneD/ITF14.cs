// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.ITF14
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class ITF14 : BarcodeCommon, IBarcode
  {
    private string[] ITF14_Code = new string[10]
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

    public ITF14(string input)
    {
      this.rawData = input;
      this.CheckDigit();
    }

    private string Encode_ITF14()
    {
      if (this.rawData.Length > 14 || this.rawData.Length < 13)
        this.Error("Data length invalid. (Length must be 13 or 14)");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data only.");
      string str1 = "1010";
      for (int index = 0; index < this.rawData.Length; index += 2)
      {
        bool flag = true;
        string str2 = this.ITF14_Code[int.Parse(this.rawData[index].ToString())];
        string str3 = this.ITF14_Code[int.Parse(this.rawData[index + 1].ToString())];
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

    private void CheckDigit()
    {
      if (this.rawData.Length != 13)
        return;
      int num1 = 0;
      int num2 = 0;
      for (int startIndex = 0; startIndex <= 10; startIndex += 2)
        num2 += int.Parse(this.rawData.Substring(startIndex, 1));
      for (int startIndex = 1; startIndex <= 11; startIndex += 2)
        num1 += int.Parse(this.rawData.Substring(startIndex, 1)) * 3;
      int num3 = 10 - (num1 + num2) % 10;
      if (num3 == 10)
        num3 = 0;
      this.rawData += num3.ToString();
    }

    public string EncodedValue => this.Encode_ITF14();
  }
}
