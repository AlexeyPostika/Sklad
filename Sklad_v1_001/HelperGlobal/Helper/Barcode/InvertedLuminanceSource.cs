// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.InvertedLuminanceSource
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode
{
  public class InvertedLuminanceSource : LuminanceSource
  {
    private LuminanceSource source;

    public InvertedLuminanceSource(LuminanceSource source)
      : base(source.Width, source.Height)
    {
      this.source = source;
    }

    public override byte[] GetRow(int y, byte[] row)
    {
      row = this.source.GetRow(y, row);
      int width = this.Width;
      for (int index = 0; index < width; ++index)
        row[index] = (byte) ((int) byte.MaxValue - ((int) row[index] & (int) byte.MaxValue));
      return row;
    }

    public override byte[] Matrix
    {
      get
      {
        byte[] matrix1 = this.source.Matrix;
        int length = this.Width * this.Height;
        byte[] matrix2 = new byte[length];
        for (int index = 0; index < length; ++index)
          matrix2[index] = (byte) ((int) byte.MaxValue - ((int) matrix1[index] & (int) byte.MaxValue));
        return matrix2;
      }
    }

    public override bool CropSupported => this.source.CropSupported;

    public override LuminanceSource Crop(int left, int top, int width, int height) => (LuminanceSource) new InvertedLuminanceSource(this.source.Crop(left, top, width, height));

    public override bool RotateSupported => this.source.RotateSupported;

    public override LuminanceSource Invert() => this.source;

    public override LuminanceSource RotateCounterClockwise() => (LuminanceSource) new InvertedLuminanceSource(this.source.RotateCounterClockwise());

    public override LuminanceSource RotateCounterClockwise45() => (LuminanceSource) new InvertedLuminanceSource(this.source.RotateCounterClockwise45());
  }
}
