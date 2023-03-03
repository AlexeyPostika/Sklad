// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Detector.Detector
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.Detector;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Pdf417.Detector
{
  public sealed class Detector
  {
    private const int INTEGER_MATH_SHIFT = 8;
    private const int PATTERN_MATCH_RESULT_SCALE_FACTOR = 256;
    private const int MAX_AVG_VARIANCE = 107;
    private const int MAX_INDIVIDUAL_VARIANCE = 204;
    private const int SKEW_THRESHOLD = 3;
    private static readonly int[] START_PATTERN = new int[8]
    {
      8,
      1,
      1,
      1,
      1,
      1,
      1,
      3
    };
    private static readonly int[] START_PATTERN_REVERSE = new int[8]
    {
      3,
      1,
      1,
      1,
      1,
      1,
      1,
      8
    };
    private static readonly int[] STOP_PATTERN = new int[9]
    {
      7,
      1,
      1,
      3,
      1,
      1,
      1,
      2,
      1
    };
    private static readonly int[] STOP_PATTERN_REVERSE = new int[9]
    {
      1,
      2,
      1,
      1,
      1,
      3,
      1,
      1,
      7
    };
    private readonly BinaryBitmap image;

    public Detector(BinaryBitmap image) => this.image = image;

    public DetectorResult Detect() => this.Detect((Dictionary<DecodeOptions, object>) null);

    public DetectorResult Detect(Dictionary<DecodeOptions, object> decodingOptions)
    {
      BitMatrix blackMatrix = this.image.BlackMatrix;
      bool tryHarder = decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.TryHarder);
      ResultPointCallback resultPointCallback = decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.NeedResultPointCallback) ? (ResultPointCallback) null : (ResultPointCallback) decodingOptions[DecodeOptions.NeedResultPointCallback];
      ResultPoint[] vertices = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindVertices(blackMatrix, tryHarder);
      if (vertices == null)
      {
        vertices = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindVertices180(blackMatrix, tryHarder);
        if (vertices != null)
          MessagingToolkit.Barcode.Pdf417.Detector.Detector.CorrectCodeWordVertices(vertices, true);
      }
      else
        MessagingToolkit.Barcode.Pdf417.Detector.Detector.CorrectCodeWordVertices(vertices, false);
      float moduleWidth = vertices != null ? MessagingToolkit.Barcode.Pdf417.Detector.Detector.ComputeModuleWidth(vertices) : throw NotFoundException.Instance;
      int xdimension = (double) moduleWidth >= 1.0 ? MessagingToolkit.Barcode.Pdf417.Detector.Detector.ComputeDimension(vertices[4], vertices[6], vertices[5], vertices[7], moduleWidth) : throw NotFoundException.Instance;
      if (xdimension < 1)
        throw NotFoundException.Instance;
      int ydimension1 = MessagingToolkit.Barcode.Pdf417.Detector.Detector.ComputeYDimension(vertices[4], vertices[6], vertices[5], vertices[7], moduleWidth);
      int ydimension2 = ydimension1 > xdimension ? ydimension1 : xdimension;
      BitMatrix bits = MessagingToolkit.Barcode.Pdf417.Detector.Detector.SampleGrid(blackMatrix, vertices[4], vertices[5], vertices[6], vertices[7], xdimension, ydimension2);
      if (resultPointCallback != null)
      {
        resultPointCallback.FoundPossibleResultPoint(vertices[5]);
        resultPointCallback.FoundPossibleResultPoint(vertices[4]);
        resultPointCallback.FoundPossibleResultPoint(vertices[6]);
        resultPointCallback.FoundPossibleResultPoint(vertices[7]);
      }
      return new DetectorResult(bits, new ResultPoint[4]
      {
        vertices[5],
        vertices[4],
        vertices[6],
        vertices[7]
      });
    }

    private static ResultPoint[] FindVertices(BitMatrix matrix, bool tryHarder)
    {
      int height = matrix.GetHeight();
      int width = matrix.GetWidth();
      ResultPoint[] resultPointArray = new ResultPoint[8];
      bool flag = false;
      int[] counters1 = new int[MessagingToolkit.Barcode.Pdf417.Detector.Detector.START_PATTERN.Length];
      int num = tryHarder ? 1 : Math.Max(1, height >> 7);
      for (int index = 0; index < height; index += num)
      {
        int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, 0, index, width, false, MessagingToolkit.Barcode.Pdf417.Detector.Detector.START_PATTERN, counters1);
        if (guardPattern != null)
        {
          resultPointArray[0] = new ResultPoint((float) guardPattern[0], (float) index);
          resultPointArray[4] = new ResultPoint((float) guardPattern[1], (float) index);
          flag = true;
          break;
        }
      }
      if (flag)
      {
        flag = false;
        for (int index = height - 1; index > 0; index -= num)
        {
          int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, 0, index, width, false, MessagingToolkit.Barcode.Pdf417.Detector.Detector.START_PATTERN, counters1);
          if (guardPattern != null)
          {
            resultPointArray[1] = new ResultPoint((float) guardPattern[0], (float) index);
            resultPointArray[5] = new ResultPoint((float) guardPattern[1], (float) index);
            flag = true;
            break;
          }
        }
      }
      int[] counters2 = new int[MessagingToolkit.Barcode.Pdf417.Detector.Detector.STOP_PATTERN.Length];
      if (flag)
      {
        flag = false;
        for (int index = 0; index < height; index += num)
        {
          int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, 0, index, width, false, MessagingToolkit.Barcode.Pdf417.Detector.Detector.STOP_PATTERN, counters2);
          if (guardPattern != null)
          {
            resultPointArray[2] = new ResultPoint((float) guardPattern[1], (float) index);
            resultPointArray[6] = new ResultPoint((float) guardPattern[0], (float) index);
            flag = true;
            break;
          }
        }
      }
      if (flag)
      {
        flag = false;
        for (int index = height - 1; index > 0; index -= num)
        {
          int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, 0, index, width, false, MessagingToolkit.Barcode.Pdf417.Detector.Detector.STOP_PATTERN, counters2);
          if (guardPattern != null)
          {
            resultPointArray[3] = new ResultPoint((float) guardPattern[1], (float) index);
            resultPointArray[7] = new ResultPoint((float) guardPattern[0], (float) index);
            flag = true;
            break;
          }
        }
      }
      return !flag ? (ResultPoint[]) null : resultPointArray;
    }

    private static ResultPoint[] FindVertices180(BitMatrix matrix, bool tryHarder)
    {
      int height = matrix.GetHeight();
      int num1 = matrix.GetWidth() >> 1;
      ResultPoint[] resultPointArray = new ResultPoint[8];
      bool flag = false;
      int[] counters1 = new int[MessagingToolkit.Barcode.Pdf417.Detector.Detector.START_PATTERN_REVERSE.Length];
      int num2 = Math.Max(1, height >> (tryHarder ? 9 : 7));
      for (int index = height - 1; index > 0; index -= num2)
      {
        int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, num1, index, num1, true, MessagingToolkit.Barcode.Pdf417.Detector.Detector.START_PATTERN_REVERSE, counters1);
        if (guardPattern != null)
        {
          resultPointArray[0] = new ResultPoint((float) guardPattern[1], (float) index);
          resultPointArray[4] = new ResultPoint((float) guardPattern[0], (float) index);
          flag = true;
          break;
        }
      }
      if (flag)
      {
        flag = false;
        for (int index = 0; index < height; index += num2)
        {
          int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, num1, index, num1, true, MessagingToolkit.Barcode.Pdf417.Detector.Detector.START_PATTERN_REVERSE, counters1);
          if (guardPattern != null)
          {
            resultPointArray[1] = new ResultPoint((float) guardPattern[1], (float) index);
            resultPointArray[5] = new ResultPoint((float) guardPattern[0], (float) index);
            flag = true;
            break;
          }
        }
      }
      int[] counters2 = new int[MessagingToolkit.Barcode.Pdf417.Detector.Detector.STOP_PATTERN_REVERSE.Length];
      if (flag)
      {
        flag = false;
        for (int index = height - 1; index > 0; index -= num2)
        {
          int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, 0, index, num1, false, MessagingToolkit.Barcode.Pdf417.Detector.Detector.STOP_PATTERN_REVERSE, counters2);
          if (guardPattern != null)
          {
            resultPointArray[2] = new ResultPoint((float) guardPattern[0], (float) index);
            resultPointArray[6] = new ResultPoint((float) guardPattern[1], (float) index);
            flag = true;
            break;
          }
        }
      }
      if (flag)
      {
        flag = false;
        for (int index = 0; index < height; index += num2)
        {
          int[] guardPattern = MessagingToolkit.Barcode.Pdf417.Detector.Detector.FindGuardPattern(matrix, 0, index, num1, false, MessagingToolkit.Barcode.Pdf417.Detector.Detector.STOP_PATTERN_REVERSE, counters2);
          if (guardPattern != null)
          {
            resultPointArray[3] = new ResultPoint((float) guardPattern[0], (float) index);
            resultPointArray[7] = new ResultPoint((float) guardPattern[1], (float) index);
            flag = true;
            break;
          }
        }
      }
      return !flag ? (ResultPoint[]) null : resultPointArray;
    }

    private static void CorrectCodeWordVertices(ResultPoint[] vertices, bool upsideDown)
    {
      float x1 = vertices[0].X;
      float y1 = vertices[0].Y;
      float x2 = vertices[2].X;
      float y2 = vertices[2].Y;
      float x3 = vertices[4].X;
      float y3 = vertices[4].Y;
      float x4 = vertices[6].X;
      float y4 = vertices[6].Y;
      float num1 = y3 - y4;
      if (upsideDown)
        num1 = -num1;
      if ((double) num1 > 3.0)
      {
        float num2 = x4 - x1;
        float num3 = y4 - y1;
        float num4 = (float) ((double) num2 * (double) num2 + (double) num3 * (double) num3);
        float num5 = (x3 - x1) * num2 / num4;
        vertices[4] = new ResultPoint(x1 + num5 * num2, y1 + num5 * num3);
      }
      else if (-(double) num1 > 3.0)
      {
        float num6 = x2 - x3;
        float num7 = y2 - y3;
        float num8 = (float) ((double) num6 * (double) num6 + (double) num7 * (double) num7);
        float num9 = (x2 - x4) * num6 / num8;
        vertices[6] = new ResultPoint(x2 - num9 * num6, y2 - num9 * num7);
      }
      float x5 = vertices[1].X;
      float y5 = vertices[1].Y;
      float x6 = vertices[3].X;
      float y6 = vertices[3].Y;
      float x7 = vertices[5].X;
      float y7 = vertices[5].Y;
      float x8 = vertices[7].X;
      float y8 = vertices[7].Y;
      float num10 = y8 - y7;
      if (upsideDown)
        num10 = -num10;
      if ((double) num10 > 3.0)
      {
        float num11 = x8 - x5;
        float num12 = y8 - y5;
        float num13 = (float) ((double) num11 * (double) num11 + (double) num12 * (double) num12);
        float num14 = (x7 - x5) * num11 / num13;
        vertices[5] = new ResultPoint(x5 + num14 * num11, y5 + num14 * num12);
      }
      else
      {
        if (-(double) num10 <= 3.0)
          return;
        float num15 = x6 - x7;
        float num16 = y6 - y7;
        float num17 = (float) ((double) num15 * (double) num15 + (double) num16 * (double) num16);
        float num18 = (x6 - x8) * num15 / num17;
        vertices[7] = new ResultPoint(x6 - num18 * num15, y6 - num18 * num16);
      }
    }

    private static float ComputeModuleWidth(ResultPoint[] vertices) => (float) ((((double) ResultPoint.Distance(vertices[0], vertices[4]) + (double) ResultPoint.Distance(vertices[1], vertices[5])) / ((double) PDF417Constants.MODULES_IN_SYMBOL * 2.0) + ((double) ResultPoint.Distance(vertices[6], vertices[2]) + (double) ResultPoint.Distance(vertices[7], vertices[3])) / 36.0) / 2.0);

    private static int ComputeDimension(
      ResultPoint topLeft,
      ResultPoint topRight,
      ResultPoint bottomLeft,
      ResultPoint bottomRight,
      float moduleWidth)
    {
      return ((MathUtils.Round(ResultPoint.Distance(topLeft, topRight) / moduleWidth) + MathUtils.Round(ResultPoint.Distance(bottomLeft, bottomRight) / moduleWidth) >> 1) + (PDF417Constants.MODULES_IN_SYMBOL >> 1)) / PDF417Constants.MODULES_IN_SYMBOL * PDF417Constants.MODULES_IN_SYMBOL;
    }

    private static int ComputeYDimension(
      ResultPoint topLeft,
      ResultPoint topRight,
      ResultPoint bottomLeft,
      ResultPoint bottomRight,
      float moduleWidth)
    {
      return MathUtils.Round(ResultPoint.Distance(topLeft, bottomLeft) / moduleWidth) + MathUtils.Round(ResultPoint.Distance(topRight, bottomRight) / moduleWidth) >> 1;
    }

    private static BitMatrix SampleGrid(
      BitMatrix matrix,
      ResultPoint topLeft,
      ResultPoint bottomLeft,
      ResultPoint topRight,
      ResultPoint bottomRight,
      int xdimension,
      int ydimension)
    {
      return GridSampler.GetInstance().SampleGrid(matrix, xdimension, ydimension, 0.0f, 0.0f, (float) xdimension, 0.0f, (float) xdimension, (float) ydimension, 0.0f, (float) ydimension, topLeft.X, topLeft.Y, topRight.X, topRight.Y, bottomRight.X, bottomRight.Y, bottomLeft.X, bottomLeft.Y);
    }

    private static int[] FindGuardPattern(
      BitMatrix matrix,
      int column,
      int row,
      int width,
      bool whiteFirst,
      int[] pattern,
      int[] counters)
    {
      for (int index = 0; index < counters.Length; ++index)
        counters[index] = 0;
      int length = pattern.Length;
      bool flag = whiteFirst;
      int index1 = 0;
      int num = column;
      int x;
      for (x = column; x < column + width; ++x)
      {
        if (matrix.Get(x, row) ^ flag)
        {
          ++counters[index1];
        }
        else
        {
          if (index1 == length - 1)
          {
            if (MessagingToolkit.Barcode.Pdf417.Detector.Detector.PatternMatchVariance(counters, pattern, 204) < 107)
              return new int[2]{ num, x };
            num += counters[0] + counters[1];
            Array.Copy((Array) counters, 2, (Array) counters, 0, length - 2);
            counters[length - 2] = 0;
            counters[length - 1] = 0;
            --index1;
          }
          else
            ++index1;
          counters[index1] = 1;
          flag = !flag;
        }
      }
      if (index1 != length - 1 || MessagingToolkit.Barcode.Pdf417.Detector.Detector.PatternMatchVariance(counters, pattern, 204) >= 107)
        return (int[]) null;
      return new int[2]{ num, x - 1 };
    }

    private static int PatternMatchVariance(
      int[] counters,
      int[] pattern,
      int maxIndividualVariance)
    {
      int length = counters.Length;
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < length; ++index)
      {
        num1 += counters[index];
        num2 += pattern[index];
      }
      if (num1 < num2)
        return int.MaxValue;
      int num3 = (num1 << 8) / num2;
      maxIndividualVariance = maxIndividualVariance * num3 >> 8;
      int num4 = 0;
      for (int index = 0; index < length; ++index)
      {
        int num5 = counters[index] << 8;
        int num6 = pattern[index] * num3;
        int num7 = num5 > num6 ? num5 - num6 : num6 - num5;
        if (num7 > maxIndividualVariance)
          return int.MaxValue;
        num4 += num7;
      }
      return num4 / num1;
    }
  }
}
