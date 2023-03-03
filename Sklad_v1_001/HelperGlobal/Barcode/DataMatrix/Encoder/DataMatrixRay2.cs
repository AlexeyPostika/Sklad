// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixRay2
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixRay2
  {
    private DataMatrixVector2 _p;
    private DataMatrixVector2 _v;

    internal DataMatrixVector2 P
    {
      get => this._p ?? (this._p = new DataMatrixVector2());
      set => this._p = value;
    }

    internal DataMatrixVector2 V
    {
      get => this._v ?? (this._v = new DataMatrixVector2());
      set => this._v = value;
    }

    internal double TMin { get; set; }

    internal double TMax { get; set; }
  }
}
