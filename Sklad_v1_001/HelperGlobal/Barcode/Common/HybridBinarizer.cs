// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.HybridBinarizer
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Common
{
  public class HybridBinarizer : GlobalHistogramBinarizer
  {
    private const int BLOCK_SIZE_POWER = 3;
    private const int BLOCK_SIZE = 8;
    private const int BLOCK_SIZE_MASK = 7;
    private const int MINIMUM_DIMENSION = 40;
    private const int MIN_DYNAMIC_RANGE = 24;
    private BitMatrix matrix;

    public HybridBinarizer(LuminanceSource source)
      : base(source)
    {
    }

    public override BitMatrix BlackMatrix
    {
      get
      {
        if (this.matrix != null)
          return this.matrix;
        LuminanceSource luminanceSource = this.LuminanceSource;
        int width = luminanceSource.Width;
        int height = luminanceSource.Height;
        if (width >= 40 && height >= 40)
        {
          byte[] matrix = luminanceSource.Matrix;
          int subWidth = width >> 3;
          if ((width & 7) != 0)
            ++subWidth;
          int subHeight = height >> 3;
          if ((height & 7) != 0)
            ++subHeight;
          int[][] blackPoints = HybridBinarizer.CalculateBlackPoints(matrix, subWidth, subHeight, width, height);
          BitMatrix matrix_0 = new BitMatrix(width, height);
          HybridBinarizer.CalculateThresholdForBlock(matrix, subWidth, subHeight, width, height, blackPoints, matrix_0);
          this.matrix = matrix_0;
        }
        else
          this.matrix = base.BlackMatrix;
        return this.matrix;
      }
    }

    public override Binarizer CreateBinarizer(LuminanceSource source) => (Binarizer) new HybridBinarizer(source);

    private static void CalculateThresholdForBlock(
      byte[] luminances,
      int subWidth,
      int subHeight,
      int width,
      int height,
      int[][] blackPoints,
      BitMatrix matrix_0)
    {
      for (int val1 = 0; val1 < subHeight; ++val1)
      {
        int yoffset = val1 << 3;
        int num1 = height - 8;
        if (yoffset > num1)
          yoffset = num1;
        for (int val2 = 0; val2 < subWidth; ++val2)
        {
          int xoffset = val2 << 3;
          int num2 = width - 8;
          if (xoffset > num2)
            xoffset = num2;
          int index1 = HybridBinarizer.Cap(val2, 2, subWidth - 3);
          int num3 = HybridBinarizer.Cap(val1, 2, subHeight - 3);
          int num4 = 0;
          for (int index2 = -2; index2 <= 2; ++index2)
          {
            int[] blackPoint = blackPoints[num3 + index2];
            num4 += blackPoint[index1 - 2] + blackPoint[index1 - 1] + blackPoint[index1] + blackPoint[index1 + 1] + blackPoint[index1 + 2];
          }
          int threshold = num4 / 25;
          HybridBinarizer.ThresholdBlock(luminances, xoffset, yoffset, threshold, width, matrix_0);
        }
      }
    }

    private static int Cap(int val, int min, int max)
    {
      if (val < min)
        return min;
      return val <= max ? val : max;
    }

    private static void ThresholdBlock(
      byte[] luminances,
      int xoffset,
      int yoffset,
      int threshold,
      int stride,
      BitMatrix matrix_0)
    {
      int num1 = 0;
      int num2 = yoffset * stride + xoffset;
      while (num1 < 8)
      {
        for (int index = 0; index < 8; ++index)
        {
          if (((int) luminances[num2 + index] & (int) byte.MaxValue) <= threshold)
            matrix_0.Set(xoffset + index, yoffset + num1);
        }
        ++num1;
        num2 += stride;
      }
    }

    private static int[][] CalculateBlackPoints(
      byte[] luminances,
      int subWidth,
      int subHeight,
      int width,
      int height)
    {
      int[][] blackPoints = new int[subHeight][];
      for (int index = 0; index < subHeight; ++index)
        blackPoints[index] = new int[subWidth];
      for (int index1 = 0; index1 < subHeight; ++index1)
      {
        int num1 = index1 << 3;
        int num2 = height - 8;
        if (num1 > num2)
          num1 = num2;
        for (int index2 = 0; index2 < subWidth; ++index2)
        {
          int num3 = index2 << 3;
          int num4 = width - 8;
          if (num3 > num4)
            num3 = num4;
          int num5 = 0;
          int num6 = (int) byte.MaxValue;
          int num7 = 0;
          int num8 = 0;
          int num9 = num1 * width + num3;
          while (num8 < 8)
          {
            for (int index3 = 0; index3 < 8; ++index3)
            {
              int num10 = (int) luminances[num9 + index3] & (int) byte.MaxValue;
              num5 += num10;
              if (num10 < num6)
                num6 = num10;
              if (num10 > num7)
                num7 = num10;
            }
            if (num7 - num6 > 24)
            {
              ++num8;
              num9 += width;
              while (num8 < 8)
              {
                for (int index4 = 0; index4 < 8; ++index4)
                  num5 += (int) luminances[num9 + index4] & (int) byte.MaxValue;
                ++num8;
                num9 += width;
              }
            }
            ++num8;
            num9 += width;
          }
          int num11 = num5 >> 6;
          if (num7 - num6 <= 24)
          {
            num11 = num6 >> 1;
            if (index1 > 0 && index2 > 0)
            {
              int num12 = blackPoints[index1 - 1][index2] + 2 * blackPoints[index1][index2 - 1] + blackPoints[index1 - 1][index2 - 1] >> 2;
              if (num6 < num12)
                num11 = num12;
            }
          }
          blackPoints[index1][index2] = num11;
        }
      }
      return blackPoints;
    }
  }
}
