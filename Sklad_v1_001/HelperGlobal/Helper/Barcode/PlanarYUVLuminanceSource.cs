// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.PlanarYUVLuminanceSource
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode
{
  public sealed class PlanarYUVLuminanceSource : BaseLuminanceSource
  {
    private static readonly int THUMBNAIL_SCALE_FACTOR = 2;
    private readonly byte[] yuvData;
    private readonly int dataWidth;
    private readonly int dataHeight;
    private readonly int left;
    private readonly int top;

    public PlanarYUVLuminanceSource(
      byte[] yuvData,
      int dataWidth,
      int dataHeight,
      int left,
      int top,
      int width,
      int height,
      bool reverseHoriz)
      : base(width, height)
    {
      if (left + width > dataWidth || top + height > dataHeight)
        throw new ArgumentException("Crop rectangle does not fit within image data.");
      this.yuvData = yuvData;
      this.dataWidth = dataWidth;
      this.dataHeight = dataHeight;
      this.left = left;
      this.top = top;
      if (!reverseHoriz)
        return;
      this.ReverseHorizontal(width, height);
    }

    private PlanarYUVLuminanceSource(byte[] luminances, int width, int height)
      : base(width, height)
    {
      this.yuvData = luminances;
      this.luminances = luminances;
      this.dataWidth = width;
      this.dataHeight = height;
      this.left = 0;
      this.top = 0;
    }

    public override byte[] GetRow(int y, byte[] row)
    {
      if (y < 0 || y >= this.Height)
        throw new ArgumentException("Requested row is outside the image: " + (object) y);
      int width = this.Width;
      if (row == null || row.Length < width)
        row = new byte[width];
      Array.Copy((Array) this.yuvData, (y + this.top) * this.dataWidth + this.left, (Array) row, 0, width);
      return row;
    }

    public override byte[] Matrix
    {
      get
      {
        int width = this.Width;
        int height = this.Height;
        if (width == this.dataWidth && height == this.dataHeight)
          return this.yuvData;
        int length = width * height;
        byte[] destinationArray = new byte[length];
        int sourceIndex = this.top * this.dataWidth + this.left;
        if (width == this.dataWidth)
        {
          Array.Copy((Array) this.yuvData, sourceIndex, (Array) destinationArray, 0, length);
          return destinationArray;
        }
        byte[] yuvData = this.yuvData;
        for (int index = 0; index < height; ++index)
        {
          int destinationIndex = index * width;
          Array.Copy((Array) yuvData, sourceIndex, (Array) destinationArray, destinationIndex, width);
          sourceIndex += this.dataWidth;
        }
        return destinationArray;
      }
    }

    public override bool CropSupported => true;

    public override LuminanceSource Crop(int left, int top, int width, int height) => (LuminanceSource) new PlanarYUVLuminanceSource(this.yuvData, this.dataWidth, this.dataHeight, this.left + left, this.top + top, width, height, false);

    public int[] RenderThumbnail()
    {
      int num1 = this.Width / PlanarYUVLuminanceSource.THUMBNAIL_SCALE_FACTOR;
      int num2 = this.Height / PlanarYUVLuminanceSource.THUMBNAIL_SCALE_FACTOR;
      int[] numArray = new int[num1 * num2];
      byte[] yuvData = this.yuvData;
      int num3 = this.top * this.dataWidth + this.left;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        int num4 = index1 * num1;
        for (int index2 = 0; index2 < num1; ++index2)
        {
          int num5 = (int) yuvData[num3 + index2 * PlanarYUVLuminanceSource.THUMBNAIL_SCALE_FACTOR] & (int) byte.MaxValue;
          numArray[num4 + index2] = -16777216 | num5 * 65793;
        }
        num3 += this.dataWidth * PlanarYUVLuminanceSource.THUMBNAIL_SCALE_FACTOR;
      }
      return numArray;
    }

    public int ThumbnailWidth => this.Width / PlanarYUVLuminanceSource.THUMBNAIL_SCALE_FACTOR;

    public int ThumbnailHeight => this.Height / PlanarYUVLuminanceSource.THUMBNAIL_SCALE_FACTOR;

    private void ReverseHorizontal(int width, int height)
    {
      byte[] yuvData = this.yuvData;
      int num1 = 0;
      int num2 = this.top * this.dataWidth + this.left;
      while (num1 < height)
      {
        int num3 = num2 + width / 2;
        int index1 = num2;
        int index2 = num2 + width - 1;
        while (index1 < num3)
        {
          byte num4 = yuvData[index1];
          yuvData[index1] = yuvData[index2];
          yuvData[index2] = num4;
          ++index1;
          --index2;
        }
        ++num1;
        num2 += this.dataWidth;
      }
    }

    protected override LuminanceSource CreateLuminanceSource(
      byte[] newLuminances,
      int width,
      int height)
    {
      return (LuminanceSource) new PlanarYUVLuminanceSource(newLuminances, width, height);
    }
  }
}
