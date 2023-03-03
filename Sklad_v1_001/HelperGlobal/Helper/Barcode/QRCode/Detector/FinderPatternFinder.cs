// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Detector.FinderPatternFinder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.QRCode.Detector
{
  public class FinderPatternFinder
  {
    private const int CENTER_QUORUM = 6;
    protected internal const int MIN_SKIP = 3;
    protected internal const int MAX_MODULES = 57;
    private const int INTEGER_MATH_SHIFT = 8;
    private readonly BitMatrix image;
    private readonly List<object> possibleCenters;
    private bool hasSkipped;
    private readonly int[] crossCheckStateCount;
    private readonly ResultPointCallback resultPointCallback;

    public FinderPatternFinder(BitMatrix image)
      : this(image, (ResultPointCallback) null)
    {
    }

    public FinderPatternFinder(BitMatrix image, ResultPointCallback resultPointCallback)
    {
      this.image = image;
      this.possibleCenters = new List<object>();
      this.crossCheckStateCount = new int[5];
      this.resultPointCallback = resultPointCallback;
    }

    protected internal BitMatrix GetImage() => this.image;

    protected internal List<object> GetPossibleCenters() => this.possibleCenters;

    internal FinderPatternInfo Find(
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      bool flag1 = decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.TryHarder);
      int height = this.image.GetHeight();
      int width = this.image.GetWidth();
      int num = 3 * height / 228;
      if (num < 3 || flag1)
        num = 3;
      bool flag2 = false;
      int[] stateCount = new int[5];
      for (int index1 = num - 1; index1 < height && !flag2; index1 += num)
      {
        stateCount[0] = 0;
        stateCount[1] = 0;
        stateCount[2] = 0;
        stateCount[3] = 0;
        stateCount[4] = 0;
        int index2 = 0;
        for (int index3 = 0; index3 < width; ++index3)
        {
          if (this.image.Get(index3, index1))
          {
            if ((index2 & 1) == 1)
              ++index2;
            ++stateCount[index2];
          }
          else if ((index2 & 1) == 0)
          {
            if (index2 == 4)
            {
              if (FinderPatternFinder.FoundPatternCross(stateCount))
              {
                if (this.HandlePossibleCenter(stateCount, index1, index3))
                {
                  num = 2;
                  if (this.hasSkipped)
                  {
                    flag2 = this.HaveMultiplyConfirmedCenters();
                  }
                  else
                  {
                    int rowSkip = this.FindRowSkip();
                    if (rowSkip > stateCount[2])
                    {
                      index1 += rowSkip - stateCount[2] - num;
                      index3 = width - 1;
                    }
                  }
                  index2 = 0;
                  stateCount[0] = 0;
                  stateCount[1] = 0;
                  stateCount[2] = 0;
                  stateCount[3] = 0;
                  stateCount[4] = 0;
                }
                else
                {
                  stateCount[0] = stateCount[2];
                  stateCount[1] = stateCount[3];
                  stateCount[2] = stateCount[4];
                  stateCount[3] = 1;
                  stateCount[4] = 0;
                  index2 = 3;
                }
              }
              else
              {
                stateCount[0] = stateCount[2];
                stateCount[1] = stateCount[3];
                stateCount[2] = stateCount[4];
                stateCount[3] = 1;
                stateCount[4] = 0;
                index2 = 3;
              }
            }
            else
              ++stateCount[++index2];
          }
          else
            ++stateCount[index2];
        }
        if (FinderPatternFinder.FoundPatternCross(stateCount) && this.HandlePossibleCenter(stateCount, index1, width))
        {
          num = stateCount[0];
          if (this.hasSkipped)
            flag2 = this.HaveMultiplyConfirmedCenters();
        }
      }
      FinderPattern[] finderPatternArray = this.SelectBestPatterns();
      ResultPoint.OrderBestPatterns((ResultPoint[]) finderPatternArray);
      return new FinderPatternInfo(finderPatternArray);
    }

    private static float CenterFromEnd(int[] stateCount, int end) => (float) (end - stateCount[4] - stateCount[3]) - (float) stateCount[2] / 2f;

    protected internal static bool FoundPatternCross(int[] stateCount)
    {
      int num1 = 0;
      for (int index = 0; index < 5; ++index)
      {
        int num2 = stateCount[index];
        if (num2 == 0)
          return false;
        num1 += num2;
      }
      if (num1 < 7)
        return false;
      int num3 = (num1 << 8) / 7;
      int num4 = num3 / 2;
      return Math.Abs(num3 - (stateCount[0] << 8)) < num4 && Math.Abs(num3 - (stateCount[1] << 8)) < num4 && Math.Abs(3 * num3 - (stateCount[2] << 8)) < 3 * num4 && Math.Abs(num3 - (stateCount[3] << 8)) < num4 && Math.Abs(num3 - (stateCount[4] << 8)) < num4;
    }

    private int[] GetCrossCheckStateCount()
    {
      this.crossCheckStateCount[0] = 0;
      this.crossCheckStateCount[1] = 0;
      this.crossCheckStateCount[2] = 0;
      this.crossCheckStateCount[3] = 0;
      this.crossCheckStateCount[4] = 0;
      return this.crossCheckStateCount;
    }

    private float CrossCheckVertical(
      int startI,
      int centerJ,
      int maxCount,
      int originalStateCountTotal)
    {
      BitMatrix image = this.image;
      int height = image.GetHeight();
      int[] crossCheckStateCount = this.GetCrossCheckStateCount();
      int y;
      for (y = startI; y >= 0 && image.Get(centerJ, y); --y)
        ++crossCheckStateCount[2];
      if (y < 0)
        return float.NaN;
      for (; y >= 0 && !image.Get(centerJ, y) && crossCheckStateCount[1] <= maxCount; --y)
        ++crossCheckStateCount[1];
      if (y < 0 || crossCheckStateCount[1] > maxCount)
        return float.NaN;
      for (; y >= 0 && image.Get(centerJ, y) && crossCheckStateCount[0] <= maxCount; --y)
        ++crossCheckStateCount[0];
      if (crossCheckStateCount[0] > maxCount)
        return float.NaN;
      int num;
      for (num = startI + 1; num < height && image.Get(centerJ, num); ++num)
        ++crossCheckStateCount[2];
      if (num == height)
        return float.NaN;
      for (; num < height && !image.Get(centerJ, num) && crossCheckStateCount[3] < maxCount; ++num)
        ++crossCheckStateCount[3];
      if (num == height || crossCheckStateCount[3] >= maxCount)
        return float.NaN;
      for (; num < height && image.Get(centerJ, num) && crossCheckStateCount[4] < maxCount; ++num)
        ++crossCheckStateCount[4];
      return crossCheckStateCount[4] >= maxCount || 5 * Math.Abs(crossCheckStateCount[0] + crossCheckStateCount[1] + crossCheckStateCount[2] + crossCheckStateCount[3] + crossCheckStateCount[4] - originalStateCountTotal) >= 2 * originalStateCountTotal || !FinderPatternFinder.FoundPatternCross(crossCheckStateCount) ? float.NaN : FinderPatternFinder.CenterFromEnd(crossCheckStateCount, num);
    }

    private float CrossCheckHorizontal(
      int startJ,
      int centerI,
      int maxCount,
      int originalStateCountTotal)
    {
      BitMatrix image = this.image;
      int width = image.GetWidth();
      int[] crossCheckStateCount = this.GetCrossCheckStateCount();
      int x;
      for (x = startJ; x >= 0 && image.Get(x, centerI); --x)
        ++crossCheckStateCount[2];
      if (x < 0)
        return float.NaN;
      for (; x >= 0 && !image.Get(x, centerI) && crossCheckStateCount[1] <= maxCount; --x)
        ++crossCheckStateCount[1];
      if (x < 0 || crossCheckStateCount[1] > maxCount)
        return float.NaN;
      for (; x >= 0 && image.Get(x, centerI) && crossCheckStateCount[0] <= maxCount; --x)
        ++crossCheckStateCount[0];
      if (crossCheckStateCount[0] > maxCount)
        return float.NaN;
      int num;
      for (num = startJ + 1; num < width && image.Get(num, centerI); ++num)
        ++crossCheckStateCount[2];
      if (num == width)
        return float.NaN;
      for (; num < width && !image.Get(num, centerI) && crossCheckStateCount[3] < maxCount; ++num)
        ++crossCheckStateCount[3];
      if (num == width || crossCheckStateCount[3] >= maxCount)
        return float.NaN;
      for (; num < width && image.Get(num, centerI) && crossCheckStateCount[4] < maxCount; ++num)
        ++crossCheckStateCount[4];
      return crossCheckStateCount[4] >= maxCount || 5 * Math.Abs(crossCheckStateCount[0] + crossCheckStateCount[1] + crossCheckStateCount[2] + crossCheckStateCount[3] + crossCheckStateCount[4] - originalStateCountTotal) >= originalStateCountTotal || !FinderPatternFinder.FoundPatternCross(crossCheckStateCount) ? float.NaN : FinderPatternFinder.CenterFromEnd(crossCheckStateCount, num);
    }

    protected internal bool HandlePossibleCenter(int[] stateCount, int i, int j)
    {
      int originalStateCountTotal = stateCount[0] + stateCount[1] + stateCount[2] + stateCount[3] + stateCount[4];
      float num1 = FinderPatternFinder.CenterFromEnd(stateCount, j);
      float num2 = this.CrossCheckVertical(i, (int) num1, stateCount[2], originalStateCountTotal);
      if (!float.IsNaN(num2))
      {
        float num3 = this.CrossCheckHorizontal((int) num1, (int) num2, stateCount[2], originalStateCountTotal);
        if (!float.IsNaN(num3))
        {
          float num4 = (float) originalStateCountTotal / 7f;
          bool flag = false;
          for (int index = 0; index < this.possibleCenters.Count; ++index)
          {
            FinderPattern possibleCenter = (FinderPattern) this.possibleCenters[index];
            if (possibleCenter.AboutEquals(num4, num2, num3))
            {
              this.possibleCenters[index] = (object) possibleCenter.CombineEstimate(num2, num3, num4);
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            FinderPattern point = new FinderPattern(num3, num2, num4);
            this.possibleCenters.Add((object) point);
            if (this.resultPointCallback != null)
              this.resultPointCallback.FoundPossibleResultPoint((ResultPoint) point);
          }
          return true;
        }
      }
      return false;
    }

    private int FindRowSkip()
    {
      if (this.possibleCenters.Count <= 1)
        return 0;
      FinderPattern finderPattern = (FinderPattern) null;
      foreach (FinderPattern possibleCenter in this.possibleCenters)
      {
        if (possibleCenter.Count >= 6)
        {
          if (finderPattern == null)
          {
            finderPattern = possibleCenter;
          }
          else
          {
            this.hasSkipped = true;
            return (int) ((double) Math.Abs(finderPattern.X - possibleCenter.X) - (double) Math.Abs(finderPattern.Y - possibleCenter.Y)) / 2;
          }
        }
      }
      return 0;
    }

    private bool HaveMultiplyConfirmedCenters()
    {
      int num1 = 0;
      float num2 = 0.0f;
      int count = this.possibleCenters.Count;
      foreach (FinderPattern possibleCenter in this.possibleCenters)
      {
        if (possibleCenter.Count >= 6)
        {
          ++num1;
          num2 += possibleCenter.EstimatedModuleSize;
        }
      }
      if (num1 < 3)
        return false;
      float num3 = num2 / (float) count;
      float num4 = 0.0f;
      foreach (FinderPattern possibleCenter in this.possibleCenters)
        num4 += Math.Abs(possibleCenter.EstimatedModuleSize - num3);
      return (double) num4 <= 0.0500000007450581 * (double) num2;
    }

    private FinderPattern[] SelectBestPatterns()
    {
      int count = this.possibleCenters.Count;
      if (count < 3)
        throw NotFoundException.Instance;
      if (count > 3)
      {
        float num1 = 0.0f;
        float num2 = 0.0f;
        foreach (FinderPattern possibleCenter in this.possibleCenters)
        {
          float estimatedModuleSize = possibleCenter.EstimatedModuleSize;
          num1 += estimatedModuleSize;
          num2 += estimatedModuleSize * estimatedModuleSize;
        }
        float f = num1 / (float) count;
        float val2 = (float) Math.Sqrt((double) num2 / (double) count - (double) f * (double) f);
        MessagingToolkit.Barcode.Common.Collections.InsertionSort<object>(this.possibleCenters, (Comparator) new FinderPatternFinder.FurthestFromAverageComparator(f));
        float num3 = Math.Max(0.2f * f, val2);
        for (int index = 0; index < this.possibleCenters.Count && this.possibleCenters.Count > 3; ++index)
        {
          if ((double) Math.Abs(((FinderPattern) this.possibleCenters[index]).EstimatedModuleSize - f) > (double) num3)
          {
            this.possibleCenters.RemoveAt(index);
            --index;
          }
        }
      }
      if (this.possibleCenters.Count > 3)
      {
        float num = 0.0f;
        foreach (FinderPattern possibleCenter in this.possibleCenters)
          num += possibleCenter.EstimatedModuleSize;
        MessagingToolkit.Barcode.Common.Collections.InsertionSort<object>(this.possibleCenters, (Comparator) new FinderPatternFinder.CenterComparator(num / (float) this.possibleCenters.Count));
        this.possibleCenters.RemoveRange(3, this.possibleCenters.Count - 3);
      }
      return new FinderPattern[3]
      {
        (FinderPattern) this.possibleCenters[0],
        (FinderPattern) this.possibleCenters[1],
        (FinderPattern) this.possibleCenters[2]
      };
    }

    private class FurthestFromAverageComparator : Comparator
    {
      private readonly float average;

      public FurthestFromAverageComparator(float f) => this.average = f;

      public virtual int Compare(object center1, object center2)
      {
        float num1 = Math.Abs(((FinderPattern) center2).EstimatedModuleSize - this.average);
        float num2 = Math.Abs(((FinderPattern) center1).EstimatedModuleSize - this.average);
        if ((double) num1 < (double) num2)
          return -1;
        return (double) num1 != (double) num2 ? 1 : 0;
      }
    }

    private class CenterComparator : Comparator
    {
      private readonly float average;

      public CenterComparator(float f) => this.average = f;

      public virtual int Compare(object center1, object center2)
      {
        if (((FinderPattern) center2).Count != ((FinderPattern) center1).Count)
          return ((FinderPattern) center2).Count - ((FinderPattern) center1).Count;
        float num1 = Math.Abs(((FinderPattern) center2).EstimatedModuleSize - this.average);
        float num2 = Math.Abs(((FinderPattern) center1).EstimatedModuleSize - this.average);
        if ((double) num1 < (double) num2)
          return 1;
        return (double) num1 != (double) num2 ? -1 : 0;
      }
    }
  }
}
