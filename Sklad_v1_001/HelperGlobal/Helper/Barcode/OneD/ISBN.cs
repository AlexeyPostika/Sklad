// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.ISBN
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class ISBN : BarcodeCommon, IBarcode
  {
    public ISBN(string input) => this.rawData = input;

    private string Encode_ISBN_Bookland()
    {
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      string str = "UNKNOWN";
      if (this.rawData.Length == 10 || this.rawData.Length == 9)
      {
        if (this.rawData.Length == 10)
          this.rawData = this.rawData.Remove(9, 1);
        this.rawData = "978" + this.rawData;
        str = nameof (ISBN);
      }
      else if (this.rawData.Length == 12 && this.rawData.StartsWith("978"))
        str = "Bookland-NOCHECKDIGIT";
      else if (this.rawData.Length == 13 && this.rawData.StartsWith("978"))
      {
        str = "Bookland-CHECKDIGIT";
        this.rawData = this.rawData.Remove(12, 1);
      }
      if (str == "UNKNOWN")
        this.Error("Invalid input.  Must start with 978 and the length must be 9, 10, 12, 13 characters.");
      return new EAN13(this.rawData).EncodedValue;
    }

    public string EncodedValue => this.Encode_ISBN_Bookland();
  }
}
