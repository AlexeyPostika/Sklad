// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.GlobalHistogramBinarizer
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Common
{
  public class GlobalHistogramBinarizer : Binarizer
  {
    private const int LUMINANCE_BITS = 5;
    private const int LUMINANCE_SHIFT = 3;
    private const int LUMINANCE_BUCKETS = 32;
    private static readonly byte[] EMPTY = new byte[0];
    private byte[] luminances;
    private readonly int[] buckets;

    public GlobalHistogramBinarizer(LuminanceSource source)
      : base(source)
    {
      this.luminances = GlobalHistogramBinarizer.EMPTY;
      this.buckets = new int[32];
    }

    public override BitArray GetBlackRow(int y, BitArray row)
    {
      LuminanceSource luminanceSource = this.LuminanceSource;
      int width = luminanceSource.Width;
      if (row == null || row.GetSize() < width)
        row = new BitArray(width);
      else
        row.Clear();
      this.InitArrays(width);
      byte[] row1 = luminanceSource.GetRow(y, this.luminances);
      int[] buckets = this.buckets;
      for (int index = 0; index < width; ++index)
      {
        int num = (int) row1[index] & (int) byte.MaxValue;
        ++buckets[num >> 3];
      }
      int num1 = GlobalHistogramBinarizer.EstimateBlackPoint(buckets);
      int num2 = (int) row1[0] & (int) byte.MaxValue;
      int num3 = (int) row1[1] & (int) byte.MaxValue;
      for (int i = 1; i < width - 1; ++i)
      {
        int num4 = (int) row1[i + 1] & (int) byte.MaxValue;
        if ((num3 << 2) - num2 - num4 >> 1 < num1)
          row.Set(i);
        num2 = num3;
        num3 = num4;
      }
      return row;
    }

    public override BitMatrix BlackMatrix
    {
      get
      {
        LuminanceSource luminanceSource = this.LuminanceSource;
        int width = luminanceSource.Width;
        int height = luminanceSource.Height;
        BitMatrix blackMatrix = new BitMatrix(width, height);
        this.InitArrays(width);
        int[] buckets = this.buckets;
        for (int index1 = 1; index1 < 5; ++index1)
        {
          int y = height * index1 / 5;
          byte[] row = luminanceSource.GetRow(y, this.luminances);
          int num1 = (width << 2) / 5;
          for (int index2 = width / 5; index2 < num1; ++index2)
          {
            int num2 = (int) row[index2] & (int) byte.MaxValue;
            ++buckets[num2 >> 3];
          }
        }
        int num3 = GlobalHistogramBinarizer.EstimateBlackPoint(buckets);
        byte[] matrix = luminanceSource.Matrix;
        for (int y = 0; y < height; ++y)
        {
          int num4 = y * width;
          for (int x = 0; x < width; ++x)
          {
            if (((int) matrix[num4 + x] & (int) byte.MaxValue) < num3)
              blackMatrix.Set(x, y);
          }
        }
        return blackMatrix;
      }
    }

    public override Binarizer CreateBinarizer(LuminanceSource source) => (Binarizer) new GlobalHistogramBinarizer(source);

    private void InitArrays(int luminanceSize)
    {
      if (this.luminances.Length < luminanceSize)
        this.luminances = new byte[luminanceSize];
      for (int index = 0; index < 32; ++index)
        this.buckets[index] = 0;
    }

    private static int EstimateBlackPoint(int[] buckets)
    {
      int length = buckets.Length;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < length; ++index)
      {
        if (buckets[index] > num3)
        {
          num2 = index;
          num3 = buckets[index];
        }
        if (buckets[index] > num1)
          num1 = buckets[index];
      }
      int num4 = 0;
      int num5 = 0;
      for (int index = 0; index < length; ++index)
      {
        int num6 = index - num2;
        int num7 = buckets[index] * num6 * num6;
        if (num7 > num5)
        {
          num4 = index;
          num5 = num7;
        }
      }
      if (num2 > num4)
      {
        int num8 = num2;
        num2 = num4;
        num4 = num8;
      }
      if (num4 - num2 <= length >> 4)
        throw NotFoundException.Instance;
      int num9 = num4 - 1;
      int num10 = -1;
      for (int index = num4 - 1; index > num2; --index)
      {
        int num11 = index - num2;
        int num12 = num11 * num11 * (num4 - index) * (num1 - buckets[index]);
        if (num12 > num10)
        {
          num9 = index;
          num10 = num12;
        }
      }
      return num9 << 3;
    }
  }
}
