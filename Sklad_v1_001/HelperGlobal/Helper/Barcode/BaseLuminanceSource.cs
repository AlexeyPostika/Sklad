// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.BaseLuminanceSource
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode
{
  public abstract class BaseLuminanceSource : LuminanceSource
  {
    protected byte[] luminances;

    protected BaseLuminanceSource(int width, int height)
      : base(width, height)
    {
      this.luminances = new byte[width * height];
    }

    protected BaseLuminanceSource(byte[] luminanceArray, int width, int height)
      : base(width, height)
    {
      this.luminances = new byte[width * height];
      Buffer.BlockCopy((Array) luminanceArray, 0, (Array) this.luminances, 0, width * height);
    }

    public override byte[] GetRow(int y, byte[] row)
    {
      int width = this.Width;
      if (row == null || row.Length < width)
        row = new byte[width];
      for (int index = 0; index < width; ++index)
        row[index] = this.luminances[y * width + index];
      return row;
    }

    public override byte[] Matrix => this.luminances;

    public override LuminanceSource RotateCounterClockwise()
    {
      byte[] newLuminances = new byte[this.Width * this.Height];
      int height = this.Height;
      int width = this.Width;
      byte[] matrix = this.Matrix;
      for (int index1 = 0; index1 < this.Height; ++index1)
      {
        for (int index2 = 0; index2 < this.Width; ++index2)
        {
          int num1 = index2;
          int num2 = height - index1 - 1;
          newLuminances[num1 * height + num2] = matrix[index1 * this.Width + index2];
        }
      }
      return this.CreateLuminanceSource(newLuminances, height, width);
    }

    public override LuminanceSource RotateCounterClockwise45() => base.RotateCounterClockwise45();

    public override bool RotateSupported => true;

    public override LuminanceSource Crop(int left, int top, int width, int height)
    {
      if (left + width > this.Width || top + height > this.Height)
        throw new ArgumentException("Crop rectangle does not fit within image data.");
      byte[] newLuminances = new byte[width * height];
      int num1 = top;
      int num2 = 0;
      while (num1 < height)
      {
        int num3 = left;
        int num4 = 0;
        while (num3 < width)
        {
          newLuminances[num2 * width + num4] = this.luminances[num1 * this.Width + num3];
          ++num3;
          ++num4;
        }
        ++num1;
        ++num2;
      }
      return this.CreateLuminanceSource(newLuminances, width, height);
    }

    public override bool CropSupported => true;

    public override bool InversionSupported => true;

    protected abstract LuminanceSource CreateLuminanceSource(
      byte[] newLuminances,
      int width,
      int height);
  }
}
