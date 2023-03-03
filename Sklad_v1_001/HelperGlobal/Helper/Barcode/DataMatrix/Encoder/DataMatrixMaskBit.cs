// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixMaskBit
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal enum DataMatrixMaskBit
  {
    MaskBit8 = 1,
    MaskBit7 = 2,
    MaskBit6 = 4,
    MaskBit5 = 8,
    MaskBit4 = 16, // 0x00000010
    MaskBit3 = 32, // 0x00000020
    MaskBit2 = 64, // 0x00000040
    MaskBit1 = 128, // 0x00000080
  }
}
