// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.BinaryBitmap
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;

namespace MessagingToolkit.Barcode
{
  public sealed class BinaryBitmap
  {
    private Binarizer binarizer;
    private BitMatrix matrix;

    public int Width => this.binarizer.LuminanceSource.Width;

    public int Height => this.binarizer.LuminanceSource.Height;

    public BitMatrix BlackMatrix
    {
      get
      {
        if (this.matrix == null)
          this.matrix = this.binarizer.BlackMatrix;
        return this.matrix;
      }
    }

    public bool CropSupported => this.binarizer.LuminanceSource.CropSupported;

    public bool RotateSupported => this.binarizer.LuminanceSource.RotateSupported;

    public BinaryBitmap(Binarizer binarizer)
    {
      this.binarizer = binarizer != null ? binarizer : throw new ArgumentException("Binarizer must be non-null.");
      this.matrix = (BitMatrix) null;
    }

    public BitArray GetBlackRow(int y, BitArray row) => this.binarizer.GetBlackRow(y, row);

    public BinaryBitmap Crop(int left, int top, int width, int height) => new BinaryBitmap(this.binarizer.CreateBinarizer(this.binarizer.LuminanceSource.Crop(left, top, width, height)));

    public BinaryBitmap RotateCounterClockwise() => new BinaryBitmap(this.binarizer.CreateBinarizer(this.binarizer.LuminanceSource.RotateCounterClockwise()));

    public BinaryBitmap RotateCounterClockwise45() => new BinaryBitmap(this.binarizer.CreateBinarizer(this.binarizer.LuminanceSource.RotateCounterClockwise45()));
  }
}
