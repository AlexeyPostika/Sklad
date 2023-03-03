// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixDirection
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal enum DataMatrixDirection
  {
    DirNone = 0,
    DirUp = 1,
    DirLeft = 2,
    DirDown = 4,
    DirVertical = 5,
    DirLeftDown = 6,
    DirRight = 8,
    DirRightUp = 9,
    DirHorizontal = 10, // 0x0000000A
  }
}
