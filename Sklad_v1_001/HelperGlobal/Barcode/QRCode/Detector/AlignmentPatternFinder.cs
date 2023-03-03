// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Detector.AlignmentPatternFinder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.QRCode.Detector
{
  internal sealed class AlignmentPatternFinder
  {
    private readonly BitMatrix image;
    private List<object> possibleCenters;
    private readonly int startX;
    private readonly int startY;
    private readonly int width;
    private readonly int height;
    private readonly float moduleSize;
    private readonly int[] crossCheckStateCount;
    private readonly ResultPointCallback resultPointCallback;

    internal AlignmentPatternFinder(
      BitMatrix image,
      int startX,
      int startY,
      int width,
      int height,
      float moduleSize,
      ResultPointCallback resultPointCallback)
    {
      this.image = image;
      this.possibleCenters = new List<object>(5);
      this.startX = startX;
      this.startY = startY;
      this.width = width;
      this.height = height;
      this.moduleSize = moduleSize;
      this.crossCheckStateCount = new int[3];
      this.resultPointCallback = resultPointCallback;
    }

    internal AlignmentPattern Find()
    {
      int startX = this.startX;
      int height = this.height;
      int j = startX + this.width;
      int num1 = this.startY + (height >> 1);
      int[] stateCount = new int[3];
      for (int index1 = 0; index1 < height; ++index1)
      {
        int num2 = num1 + ((index1 & 1) == 0 ? index1 + 1 >> 1 : -(index1 + 1 >> 1));
        stateCount[0] = 0;
        stateCount[1] = 0;
        stateCount[2] = 0;
        int num3 = startX;
        while (num3 < j && !this.image.Get(num3, num2))
          ++num3;
        int index2 = 0;
        for (; num3 < j; ++num3)
        {
          if (this.image.Get(num3, num2))
          {
            switch (index2)
            {
              case 1:
                ++stateCount[index2];
                continue;
              case 2:
                if (this.FoundPatternCross(stateCount))
                {
                  AlignmentPattern alignmentPattern = this.HandlePossibleCenter(stateCount, num2, num3);
                  if (alignmentPattern != null)
                    return alignmentPattern;
                }
                stateCount[0] = stateCount[2];
                stateCount[1] = 1;
                stateCount[2] = 0;
                index2 = 1;
                continue;
              default:
                ++stateCount[++index2];
                continue;
            }
          }
          else
          {
            if (index2 == 1)
              ++index2;
            ++stateCount[index2];
          }
        }
        if (this.FoundPatternCross(stateCount))
        {
          AlignmentPattern alignmentPattern = this.HandlePossibleCenter(stateCount, num2, j);
          if (alignmentPattern != null)
            return alignmentPattern;
        }
      }
      return this.possibleCenters.Count != 0 ? (AlignmentPattern) this.possibleCenters[0] : throw NotFoundException.Instance;
    }

    private static float CenterFromEnd(int[] stateCount, int end) => (float) (end - stateCount[2]) - (float) stateCount[1] / 2f;

    private bool FoundPatternCross(int[] stateCount)
    {
      float moduleSize = this.moduleSize;
      float num = moduleSize / 2f;
      for (int index = 0; index < 3; ++index)
      {
        if ((double) Math.Abs(moduleSize - (float) stateCount[index]) >= (double) num)
          return false;
      }
      return true;
    }

    private float CrossCheckVertical(
      int startI,
      int centerJ,
      int maxCount,
      int originalStateCountTotal)
    {
      BitMatrix image = this.image;
      int height = image.GetHeight();
      int[] crossCheckStateCount = this.crossCheckStateCount;
      crossCheckStateCount[0] = 0;
      crossCheckStateCount[1] = 0;
      crossCheckStateCount[2] = 0;
      int y;
      for (y = startI; y >= 0 && image.Get(centerJ, y) && crossCheckStateCount[1] <= maxCount; --y)
        ++crossCheckStateCount[1];
      if (y < 0 || crossCheckStateCount[1] > maxCount)
        return float.NaN;
      for (; y >= 0 && !image.Get(centerJ, y) && crossCheckStateCount[0] <= maxCount; --y)
        ++crossCheckStateCount[0];
      if (crossCheckStateCount[0] > maxCount)
        return float.NaN;
      int num;
      for (num = startI + 1; num < height && image.Get(centerJ, num) && crossCheckStateCount[1] <= maxCount; ++num)
        ++crossCheckStateCount[1];
      if (num == height || crossCheckStateCount[1] > maxCount)
        return float.NaN;
      for (; num < height && !image.Get(centerJ, num) && crossCheckStateCount[2] <= maxCount; ++num)
        ++crossCheckStateCount[2];
      return crossCheckStateCount[2] > maxCount || 5 * Math.Abs(crossCheckStateCount[0] + crossCheckStateCount[1] + crossCheckStateCount[2] - originalStateCountTotal) >= 2 * originalStateCountTotal || !this.FoundPatternCross(crossCheckStateCount) ? float.NaN : AlignmentPatternFinder.CenterFromEnd(crossCheckStateCount, num);
    }

    private AlignmentPattern HandlePossibleCenter(int[] stateCount, int i, int j)
    {
      int originalStateCountTotal = stateCount[0] + stateCount[1] + stateCount[2];
      float num1 = AlignmentPatternFinder.CenterFromEnd(stateCount, j);
      float num2 = this.CrossCheckVertical(i, (int) num1, 2 * stateCount[1], originalStateCountTotal);
      if (!float.IsNaN(num2))
      {
        float num3 = (float) (stateCount[0] + stateCount[1] + stateCount[2]) / 3f;
        foreach (AlignmentPattern possibleCenter in this.possibleCenters)
        {
          if (possibleCenter.AboutEquals(num3, num2, num1))
            return possibleCenter.CombineEstimate(num2, num1, num3);
        }
        AlignmentPattern point = new AlignmentPattern(num1, num2, num3);
        this.possibleCenters.Add((object) point);
        if (this.resultPointCallback != null)
          this.resultPointCallback.FoundPossibleResultPoint((ResultPoint) point);
      }
      return (AlignmentPattern) null;
    }
  }
}
