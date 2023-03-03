// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.Detector.MonochromeRectangleDetector
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Common.Detector
{
  public sealed class MonochromeRectangleDetector
  {
    private const int MaxModules = 32;
    private BitMatrix image;

    public MonochromeRectangleDetector(BitMatrix image) => this.image = image;

    public ResultPoint[] Detect()
    {
      int height = this.image.Height;
      int width = this.image.Width;
      int centerY = height >> 1;
      int centerX = width >> 1;
      int deltaY = Math.Max(1, height / 256);
      int deltaX = Math.Max(1, width / 256);
      int top1 = 0;
      int bottom1 = height;
      int left1 = 0;
      int right1 = width;
      int top2 = (int) this.FindCornerFromCenter(centerX, 0, left1, right1, centerY, -deltaY, top1, bottom1, centerX >> 1).Y - 1;
      ResultPoint cornerFromCenter1 = this.FindCornerFromCenter(centerX, -deltaX, left1, right1, centerY, 0, top2, bottom1, centerY >> 1);
      int left2 = (int) cornerFromCenter1.X - 1;
      ResultPoint cornerFromCenter2 = this.FindCornerFromCenter(centerX, deltaX, left2, right1, centerY, 0, top2, bottom1, centerY >> 1);
      int right2 = (int) cornerFromCenter2.X + 1;
      ResultPoint cornerFromCenter3 = this.FindCornerFromCenter(centerX, 0, left2, right2, centerY, deltaY, top2, bottom1, centerX >> 1);
      int bottom2 = (int) cornerFromCenter3.Y + 1;
      return new ResultPoint[4]
      {
        this.FindCornerFromCenter(centerX, 0, left2, right2, centerY, -deltaY, top2, bottom2, centerX >> 2),
        cornerFromCenter1,
        cornerFromCenter2,
        cornerFromCenter3
      };
    }

    private ResultPoint FindCornerFromCenter(
      int centerX,
      int deltaX,
      int left,
      int right,
      int centerY,
      int deltaY,
      int top,
      int bottom,
      int maxWhiteRun)
    {
      int[] numArray1 = (int[]) null;
      int fixedDimension1 = centerY;
      for (int fixedDimension2 = centerX; fixedDimension1 < bottom && fixedDimension1 >= top && fixedDimension2 < right && fixedDimension2 >= left; fixedDimension2 += deltaX)
      {
        int[] numArray2 = deltaX != 0 ? this.BlackWhiteRange(fixedDimension2, maxWhiteRun, top, bottom, false) : this.BlackWhiteRange(fixedDimension1, maxWhiteRun, left, right, true);
        if (numArray2 == null)
        {
          if (numArray1 == null)
            throw NotFoundException.Instance;
          if (deltaX == 0)
          {
            int y = fixedDimension1 - deltaY;
            if (numArray1[0] >= centerX)
              return new ResultPoint((float) numArray1[1], (float) y);
            return numArray1[1] > centerX ? new ResultPoint(deltaY > 0 ? (float) numArray1[0] : (float) numArray1[1], (float) y) : new ResultPoint((float) numArray1[0], (float) y);
          }
          int x = fixedDimension2 - deltaX;
          if (numArray1[0] >= centerY)
            return new ResultPoint((float) x, (float) numArray1[1]);
          return numArray1[1] > centerY ? new ResultPoint((float) x, deltaX < 0 ? (float) numArray1[0] : (float) numArray1[1]) : new ResultPoint((float) x, (float) numArray1[0]);
        }
        numArray1 = numArray2;
        fixedDimension1 += deltaY;
      }
      throw NotFoundException.Instance;
    }

    private int[] BlackWhiteRange(
      int fixedDimension,
      int maxWhiteRun,
      int minDim,
      int maxDim,
      bool horizontal)
    {
      int num1 = minDim + maxDim >> 1;
      int num2 = num1;
      while (num2 >= minDim)
      {
        if ((horizontal ? (this.image.GetValue(num2, fixedDimension) ? 1 : 0) : (this.image.GetValue(fixedDimension, num2) ? 1 : 0)) != 0)
        {
          --num2;
        }
        else
        {
          int num3 = num2;
          do
          {
            --num2;
          }
          while (num2 >= minDim && (horizontal ? (this.image.GetValue(num2, fixedDimension) ? 1 : 0) : (this.image.GetValue(fixedDimension, num2) ? 1 : 0)) == 0);
          int num4 = num3 - num2;
          if (num2 < minDim || num4 > maxWhiteRun)
          {
            num2 = num3;
            break;
          }
        }
      }
      int num5 = num2 + 1;
      int num6 = num1;
      while (num6 < maxDim)
      {
        if ((horizontal ? (this.image.GetValue(num6, fixedDimension) ? 1 : 0) : (this.image.GetValue(fixedDimension, num6) ? 1 : 0)) != 0)
        {
          ++num6;
        }
        else
        {
          int num7 = num6;
          do
          {
            ++num6;
          }
          while (num6 < maxDim && (horizontal ? (this.image.GetValue(num6, fixedDimension) ? 1 : 0) : (this.image.GetValue(fixedDimension, num6) ? 1 : 0)) == 0);
          int num8 = num6 - num7;
          if (num6 >= maxDim || num8 > maxWhiteRun)
          {
            num6 = num7;
            break;
          }
        }
      }
      int num9 = num6 - 1;
      if (num9 <= num5)
        return (int[]) null;
      return new int[2]{ num5, num9 };
    }
  }
}
