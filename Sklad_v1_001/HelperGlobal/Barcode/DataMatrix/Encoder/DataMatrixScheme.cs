﻿// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixScheme
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  public enum DataMatrixScheme
  {
    SchemeAutoFast = -2, // 0xFFFFFFFE
    SchemeAutoBest = -1, // 0xFFFFFFFF
    SchemeAscii = 0,
    SchemeC40 = 1,
    SchemeText = 2,
    SchemeX12 = 3,
    SchemeEdifact = 4,
    SchemeBase256 = 5,
    SchemeAsciiGS1 = 6,
  }
}
