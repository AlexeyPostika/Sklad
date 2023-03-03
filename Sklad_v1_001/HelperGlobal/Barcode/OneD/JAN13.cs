// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.JAN13
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.OneD
{
  internal class JAN13 : BarcodeCommon, IBarcode
  {
    public JAN13(string input) => this.rawData = input;

    private string Encode_JAN13()
    {
      if (!this.rawData.StartsWith("49"))
        this.Error("Invalid Country Code for JAN13 (49 required)");
      if (!BarcodeEncoder.CheckNumericOnly(this.rawData))
        this.Error("Numeric data Only");
      return new EAN13(this.rawData).EncodedValue;
    }

    public string EncodedValue => this.Encode_JAN13();
  }
}
