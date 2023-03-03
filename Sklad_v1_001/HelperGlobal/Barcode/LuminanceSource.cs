// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.LuminanceSource
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode
{
  public abstract class LuminanceSource
  {
    private int width;
    private int height;

    protected LuminanceSource(int width, int height)
    {
      this.width = width;
      this.height = height;
    }

    public abstract byte[] GetRow(int y, byte[] row);

    public abstract byte[] Matrix { get; }

    public virtual int Width
    {
      get => this.width;
      protected set => this.width = value;
    }

    public virtual int Height
    {
      get => this.height;
      protected set => this.height = value;
    }

    public virtual bool CropSupported => false;

    public virtual LuminanceSource Crop(int left, int top, int width, int height) => throw new NotSupportedException("This luminance source does not support cropping.");

    public virtual bool RotateSupported => false;

    public virtual LuminanceSource RotateCounterClockwise() => throw new NotSupportedException("This luminance source does not support rotation.");

    public virtual LuminanceSource RotateCounterClockwise45() => throw new NotSupportedException("This luminance source does not support rotation by 45 degrees.");

    public virtual bool InversionSupported => true;

    public virtual LuminanceSource Invert() => (LuminanceSource) new InvertedLuminanceSource(this);

    public override string ToString()
    {
      byte[] row = new byte[this.width];
      StringBuilder stringBuilder = new StringBuilder(this.height * (this.width + 1));
      for (int y = 0; y < this.height; ++y)
      {
        row = this.GetRow(y, row);
        for (int index = 0; index < this.width; ++index)
        {
          int num = (int) row[index] & (int) byte.MaxValue;
          char ch = num >= 64 ? (num >= 128 ? (num >= 192 ? ' ' : '.') : '+') : '#';
          stringBuilder.Append(ch);
        }
        stringBuilder.Append('\n');
      }
      return stringBuilder.ToString();
    }
  }
}
