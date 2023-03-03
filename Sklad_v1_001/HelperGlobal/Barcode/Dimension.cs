// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Dimension
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode
{
  public sealed class Dimension
  {
    private readonly int width;
    private readonly int height;

    public Dimension(int width, int height)
    {
      this.width = width >= 0 && height >= 0 ? width : throw new ArgumentException();
      this.height = height;
    }

    public int Width => this.width;

    public int Height => this.height;

    public override bool Equals(object other)
    {
      if (!(other is Dimension))
        return false;
      Dimension dimension = (Dimension) other;
      return this.width == dimension.width && this.height == dimension.height;
    }

    public override int GetHashCode() => this.width * 32713 + this.height;

    public override string ToString() => this.width.ToString() + "x" + (object) this.height;
  }
}
