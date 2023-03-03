// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Encoder.BlockPair
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.QRCode.Encoder
{
  internal sealed class BlockPair
  {
    private readonly byte[] dataBytes;
    private readonly byte[] errorCorrectionBytes;

    internal BlockPair(byte[] data, byte[] errorCorrection)
    {
      this.dataBytes = data;
      this.errorCorrectionBytes = errorCorrection;
    }

    public byte[] GetDataBytes() => this.dataBytes;

    public byte[] GetErrorCorrectionBytes() => this.errorCorrectionBytes;
  }
}
