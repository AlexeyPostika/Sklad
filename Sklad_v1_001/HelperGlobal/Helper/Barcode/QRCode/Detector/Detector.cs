// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Detector.Detector
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.Detector;
using MessagingToolkit.Barcode.Helper;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.QRCode.Detector
{
  public class Detector
  {
    private readonly BitMatrix image;
    private ResultPointCallback resultPointCallback;

    public Detector(BitMatrix image) => this.image = image;

    protected internal BitMatrix Image => this.image;

    protected internal ResultPointCallback ResultPointCallback => this.resultPointCallback;

    public DetectorResult Detect() => this.Detect((Dictionary<DecodeOptions, object>) null);

    public DetectorResult Detect(Dictionary<DecodeOptions, object> decodingOptions)
    {
      this.resultPointCallback = decodingOptions == null ? (ResultPointCallback) null : (ResultPointCallback) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.NeedResultPointCallback);
      return this.ProcessFinderPatternInfo(new FinderPatternFinder(this.image, this.resultPointCallback).Find(decodingOptions));
    }

    protected internal DetectorResult ProcessFinderPatternInfo(FinderPatternInfo info)
    {
      FinderPattern topLeft = info.TopLeft;
      FinderPattern topRight = info.TopRight;
      FinderPattern bottomLeft = info.BottomLeft;
      float moduleSize = this.CalculateModuleSize((ResultPoint) topLeft, (ResultPoint) topRight, (ResultPoint) bottomLeft);
      if ((double) moduleSize < 1.0)
        throw NotFoundException.Instance;
      int dimension = MessagingToolkit.Barcode.QRCode.Detector.Detector.ComputeDimension((ResultPoint) topLeft, (ResultPoint) topRight, (ResultPoint) bottomLeft, moduleSize);
      MessagingToolkit.Barcode.QRCode.Decoder.Version versionForDimension = MessagingToolkit.Barcode.QRCode.Decoder.Version.GetProvisionalVersionForDimension(dimension);
      int num1 = versionForDimension.DimensionForVersion - 7;
      AlignmentPattern alignmentPattern = (AlignmentPattern) null;
      if (versionForDimension.AlignmentPatternCenters.Length > 0)
      {
        float num2 = topRight.X - topLeft.X + bottomLeft.X;
        float num3 = topRight.Y - topLeft.Y + bottomLeft.Y;
        float num4 = (float) (1.0 - 3.0 / (double) num1);
        int estAlignmentX = (int) ((double) topLeft.X + (double) num4 * ((double) num2 - (double) topLeft.X));
        int estAlignmentY = (int) ((double) topLeft.Y + (double) num4 * ((double) num3 - (double) topLeft.Y));
        for (int allowanceFactor = 4; allowanceFactor <= 16; allowanceFactor <<= 1)
        {
          try
          {
            alignmentPattern = this.FindAlignmentInRegion(moduleSize, estAlignmentX, estAlignmentY, (float) allowanceFactor);
            break;
          }
          catch (NotFoundException ex)
          {
          }
        }
      }
      BitMatrix bits = MessagingToolkit.Barcode.QRCode.Detector.Detector.SampleGrid(this.image, MessagingToolkit.Barcode.QRCode.Detector.Detector.CreateTransform((ResultPoint) topLeft, (ResultPoint) topRight, (ResultPoint) bottomLeft, (ResultPoint) alignmentPattern, dimension), dimension);
      ResultPoint[] points;
      if (alignmentPattern == null)
        points = new ResultPoint[3]
        {
          (ResultPoint) bottomLeft,
          (ResultPoint) topLeft,
          (ResultPoint) topRight
        };
      else
        points = new ResultPoint[4]
        {
          (ResultPoint) bottomLeft,
          (ResultPoint) topLeft,
          (ResultPoint) topRight,
          (ResultPoint) alignmentPattern
        };
      return new DetectorResult(bits, points);
    }

    public static PerspectiveTransform CreateTransform(
      ResultPoint topLeft,
      ResultPoint topRight,
      ResultPoint bottomLeft,
      ResultPoint alignmentPattern,
      int dimension)
    {
      float num = (float) dimension - 3.5f;
      float x2p;
      float y2p;
      float x2;
      float y2;
      if (alignmentPattern != null)
      {
        x2p = alignmentPattern.X;
        y2p = alignmentPattern.Y;
        x2 = num - 3f;
        y2 = x2;
      }
      else
      {
        x2p = topRight.X - topLeft.X + bottomLeft.X;
        y2p = topRight.Y - topLeft.Y + bottomLeft.Y;
        x2 = num;
        y2 = num;
      }
      return PerspectiveTransform.QuadrilateralToQuadrilateral(3.5f, 3.5f, num, 3.5f, x2, y2, 3.5f, num, topLeft.X, topLeft.Y, topRight.X, topRight.Y, x2p, y2p, bottomLeft.X, bottomLeft.Y);
    }

    private static BitMatrix SampleGrid(
      BitMatrix image,
      PerspectiveTransform transform,
      int dimension)
    {
      return GridSampler.GetInstance().SampleGrid(image, dimension, dimension, transform);
    }

    protected internal static int ComputeDimension(
      ResultPoint topLeft,
      ResultPoint topRight,
      ResultPoint bottomLeft,
      float moduleSize)
    {
      int dimension = (MathUtils.Round(ResultPoint.Distance(topLeft, topRight) / moduleSize) + MathUtils.Round(ResultPoint.Distance(topLeft, bottomLeft) / moduleSize) >> 1) + 7;
      switch (dimension & 3)
      {
        case 0:
          ++dimension;
          break;
        case 2:
          --dimension;
          break;
        case 3:
          throw NotFoundException.Instance;
      }
      return dimension;
    }

    protected internal float CalculateModuleSize(
      ResultPoint topLeft,
      ResultPoint topRight,
      ResultPoint bottomLeft)
    {
      return (float) (((double) this.CalculateModuleSizeOneWay(topLeft, topRight) + (double) this.CalculateModuleSizeOneWay(topLeft, bottomLeft)) / 2.0);
    }

    private float CalculateModuleSizeOneWay(ResultPoint pattern, ResultPoint otherPattern)
    {
      float f1 = this.SizeOfBlackWhiteBlackRunBothWays((int) pattern.X, (int) pattern.Y, (int) otherPattern.X, (int) otherPattern.Y);
      float f2 = this.SizeOfBlackWhiteBlackRunBothWays((int) otherPattern.X, (int) otherPattern.Y, (int) pattern.X, (int) pattern.Y);
      if (float.IsNaN(f1))
        return f2 / 7f;
      return float.IsNaN(f2) ? f1 / 7f : (float) (((double) f1 + (double) f2) / 14.0);
    }

    private float SizeOfBlackWhiteBlackRunBothWays(int fromX, int fromY, int toX, int toY)
    {
      float num1 = this.SizeOfBlackWhiteBlackRun(fromX, fromY, toX, toY);
      float num2 = 1f;
      int num3 = fromX - (toX - fromX);
      if (num3 < 0)
      {
        num2 = (float) fromX / (float) (fromX - num3);
        num3 = 0;
      }
      else if (num3 >= this.image.GetWidth())
      {
        num2 = (float) (this.image.GetWidth() - 1 - fromX) / (float) (num3 - fromX);
        num3 = this.image.GetWidth() - 1;
      }
      int toY1 = (int) ((double) fromY - (double) (toY - fromY) * (double) num2);
      float num4 = 1f;
      if (toY1 < 0)
      {
        num4 = (float) fromY / (float) (fromY - toY1);
        toY1 = 0;
      }
      else if (toY1 >= this.image.GetHeight())
      {
        num4 = (float) (this.image.GetHeight() - 1 - fromY) / (float) (toY1 - fromY);
        toY1 = this.image.GetHeight() - 1;
      }
      int toX1 = (int) ((double) fromX + (double) (num3 - fromX) * (double) num4);
      return num1 + this.SizeOfBlackWhiteBlackRun(fromX, fromY, toX1, toY1) - 1f;
    }

    private float SizeOfBlackWhiteBlackRun(int fromX, int fromY, int toX, int toY)
    {
      bool flag = Math.Abs(toY - fromY) > Math.Abs(toX - fromX);
      if (flag)
      {
        int num1 = fromX;
        fromX = fromY;
        fromY = num1;
        int num2 = toX;
        toX = toY;
        toY = num2;
      }
      int num3 = Math.Abs(toX - fromX);
      int num4 = Math.Abs(toY - fromY);
      int num5 = -num3 >> 1;
      int num6 = fromX < toX ? 1 : -1;
      int num7 = fromY < toY ? 1 : -1;
      int num8 = 0;
      int num9 = toX + num6;
      int aX = fromX;
      int aY = fromY;
      for (; aX != num9; aX += num6)
      {
        int x = flag ? aY : aX;
        int y = flag ? aX : aY;
        if (num8 == 1 == this.image.Get(x, y))
        {
          if (num8 == 2)
            return MathUtils.Distance(aX, aY, fromX, fromY);
          ++num8;
        }
        num5 += num4;
        if (num5 > 0)
        {
          if (aY != toY)
          {
            aY += num7;
            num5 -= num3;
          }
          else
            break;
        }
      }
      return num8 == 2 ? MathUtils.Distance(toX + num6, toY, fromX, fromY) : float.NaN;
    }

    protected internal AlignmentPattern FindAlignmentInRegion(
      float overallEstModuleSize,
      int estAlignmentX,
      int estAlignmentY,
      float allowanceFactor)
    {
      int num1 = (int) ((double) allowanceFactor * (double) overallEstModuleSize);
      int startX = Math.Max(0, estAlignmentX - num1);
      int num2 = Math.Min(this.image.GetWidth() - 1, estAlignmentX + num1);
      if ((double) (num2 - startX) < (double) overallEstModuleSize * 3.0)
        throw NotFoundException.Instance;
      int startY = Math.Max(0, estAlignmentY - num1);
      int num3 = Math.Min(this.image.GetHeight() - 1, estAlignmentY + num1);
      if ((double) (num3 - startY) < (double) overallEstModuleSize * 3.0)
        throw NotFoundException.Instance;
      return new AlignmentPatternFinder(this.image, startX, startY, num2 - startX, num3 - startY, overallEstModuleSize, this.resultPointCallback).Find();
    }
  }
}
