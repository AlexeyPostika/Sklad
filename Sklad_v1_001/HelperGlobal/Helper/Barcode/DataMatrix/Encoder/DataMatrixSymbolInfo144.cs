﻿// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixSymbolInfo144
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal sealed class DataMatrixSymbolInfo144 : SymbolInfo
  {
    internal DataMatrixSymbolInfo144()
      : base(false, 1558, 620, 22, 22, 36)
    {
      this.rsBlockData = -1;
      this.rsBlockError = 62;
    }

    public override int InterleavedBlockCount => 10;

    public override int GetDataLengthForInterleavedBlock(int index) => index > 8 ? 155 : 156;
  }
}