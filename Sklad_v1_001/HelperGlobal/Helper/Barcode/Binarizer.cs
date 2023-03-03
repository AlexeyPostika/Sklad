// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Binarizer
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;

namespace MessagingToolkit.Barcode
{
  public abstract class Binarizer
  {
    private readonly LuminanceSource source;

    public virtual LuminanceSource LuminanceSource => this.source;

    public abstract BitMatrix BlackMatrix { get; }

    protected internal Binarizer(LuminanceSource source) => this.source = source != null ? source : throw new ArgumentException("Source must be non-null.");

    public abstract BitArray GetBlackRow(int y, BitArray row);

    public abstract Binarizer CreateBinarizer(LuminanceSource source);

    public int Width => this.source.Width;

    public int Height => this.source.Height;
  }
}
