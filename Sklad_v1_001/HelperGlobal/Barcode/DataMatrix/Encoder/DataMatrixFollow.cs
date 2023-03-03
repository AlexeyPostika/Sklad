// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixFollow
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal struct DataMatrixFollow
  {
    private int _ptrIndex;

    internal int PtrIndex
    {
      set => this._ptrIndex = value;
    }

    internal byte CurrentPtr
    {
      get => this.Ptr[this._ptrIndex];
      set => this.Ptr[this._ptrIndex] = value;
    }

    internal byte[] Ptr { get; set; }

    internal byte Neighbor
    {
      get => this.Ptr[this._ptrIndex];
      set => this.Ptr[this._ptrIndex] = value;
    }

    internal int Step { get; set; }

    internal DataMatrixPixelLoc Loc { get; set; }
  }
}
