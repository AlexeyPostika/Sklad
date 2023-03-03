// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Multi.QRCode.Detector.MultiFinderPatternFinder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.QRCode.Detector;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Multi.QRCode.Detector
{
  internal sealed class MultiFinderPatternFinder : FinderPatternFinder
  {
    private const float MAX_MODULE_COUNT_PER_EDGE = 180f;
    private const float MIN_MODULE_COUNT_PER_EDGE = 9f;
    private const float DIFF_MODSIZE_CUTOFF_PERCENT = 0.05f;
    private const float DIFF_MODSIZE_CUTOFF = 0.5f;
    private static readonly FinderPatternInfo[] EMPTY_RESULT_ARRAY = new FinderPatternInfo[0];

    internal MultiFinderPatternFinder(BitMatrix image)
      : base(image)
    {
    }

    internal MultiFinderPatternFinder(BitMatrix image, ResultPointCallback resultPointCallback)
      : base(image, resultPointCallback)
    {
    }

    private FinderPattern[][] SelectMutipleBestPatterns()
    {
      List<object> possibleCenters = this.GetPossibleCenters();
      int count = possibleCenters.Count;
      if (count < 3)
        throw NotFoundException.Instance;
      if (count == 3)
        return new FinderPattern[1][]
        {
          new FinderPattern[3]
          {
            (FinderPattern) possibleCenters[0],
            (FinderPattern) possibleCenters[1],
            (FinderPattern) possibleCenters[2]
          }
        };
      MessagingToolkit.Barcode.Common.Collections.InsertionSort<object>(possibleCenters, (Comparator) new MultiFinderPatternFinder.ModuleSizeComparator());
      List<FinderPattern[]> finderPatternArrayList = new List<FinderPattern[]>(10);
      for (int index1 = 0; index1 < count - 2; ++index1)
      {
        FinderPattern finderPattern1 = (FinderPattern) possibleCenters[index1];
        if (finderPattern1 != null)
        {
          for (int index2 = index1 + 1; index2 < count - 1; ++index2)
          {
            FinderPattern finderPattern2 = (FinderPattern) possibleCenters[index2];
            if (finderPattern2 != null)
            {
              float num1 = (finderPattern1.EstimatedModuleSize - finderPattern2.EstimatedModuleSize) / Math.Min(finderPattern1.EstimatedModuleSize, finderPattern2.EstimatedModuleSize);
              if ((double) Math.Abs(finderPattern1.EstimatedModuleSize - finderPattern2.EstimatedModuleSize) <= 0.5 || (double) num1 < 0.0500000007450581)
              {
                for (int index3 = index2 + 1; index3 < count; ++index3)
                {
                  FinderPattern finderPattern3 = (FinderPattern) possibleCenters[index3];
                  if (finderPattern3 != null)
                  {
                    float num2 = (finderPattern2.EstimatedModuleSize - finderPattern3.EstimatedModuleSize) / Math.Min(finderPattern2.EstimatedModuleSize, finderPattern3.EstimatedModuleSize);
                    if ((double) Math.Abs(finderPattern2.EstimatedModuleSize - finderPattern3.EstimatedModuleSize) <= 0.5 || (double) num2 < 0.0500000007450581)
                    {
                      FinderPattern[] finderPatternArray = new FinderPattern[3]
                      {
                        finderPattern1,
                        finderPattern2,
                        finderPattern3
                      };
                      ResultPoint.OrderBestPatterns((ResultPoint[]) finderPatternArray);
                      FinderPatternInfo finderPatternInfo = new FinderPatternInfo(finderPatternArray);
                      float val1_1 = ResultPoint.Distance((ResultPoint) finderPatternInfo.TopLeft, (ResultPoint) finderPatternInfo.BottomLeft);
                      float val1_2 = ResultPoint.Distance((ResultPoint) finderPatternInfo.TopRight, (ResultPoint) finderPatternInfo.BottomLeft);
                      float val2_1 = ResultPoint.Distance((ResultPoint) finderPatternInfo.TopLeft, (ResultPoint) finderPatternInfo.TopRight);
                      float num3 = (float) (((double) val1_1 + (double) val2_1) / ((double) finderPattern1.EstimatedModuleSize * 2.0));
                      if ((double) num3 <= 180.0 && (double) num3 >= 9.0 && (double) Math.Abs((val1_1 - val2_1) / Math.Min(val1_1, val2_1)) < 0.100000001490116)
                      {
                        float val2_2 = (float) Math.Sqrt((double) val1_1 * (double) val1_1 + (double) val2_1 * (double) val2_1);
                        if ((double) Math.Abs((val1_2 - val2_2) / Math.Min(val1_2, val2_2)) < 0.100000001490116)
                          finderPatternArrayList.Add(finderPatternArray);
                      }
                    }
                    else
                      break;
                  }
                }
              }
              else
                break;
            }
          }
        }
      }
      FinderPattern[][] finderPatternArray1 = finderPatternArrayList.Count != 0 ? new FinderPattern[finderPatternArrayList.Count][] : throw NotFoundException.Instance;
      for (int index = 0; index < finderPatternArrayList.Count; ++index)
        finderPatternArray1[index] = finderPatternArrayList[index];
      return finderPatternArray1;
    }

    public FinderPatternInfo[] FindMulti(
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      bool flag = decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.TryHarder);
      BitMatrix image = this.GetImage();
      int height = image.Height;
      int width = image.Width;
      int num = (int) ((double) height / 228.0 * 3.0);
      if (num < 3 || flag)
        num = 3;
      int[] stateCount = new int[5];
      for (int index1 = num - 1; index1 < height; index1 += num)
      {
        stateCount[0] = 0;
        stateCount[1] = 0;
        stateCount[2] = 0;
        stateCount[3] = 0;
        stateCount[4] = 0;
        int index2 = 0;
        for (int index3 = 0; index3 < width; ++index3)
        {
          if (image.Get(index3, index1))
          {
            if ((index2 & 1) == 1)
              ++index2;
            ++stateCount[index2];
          }
          else if ((index2 & 1) == 0)
          {
            if (index2 == 4)
            {
              if (FinderPatternFinder.FoundPatternCross(stateCount) && this.HandlePossibleCenter(stateCount, index1, index3))
              {
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
              ++stateCount[++index2];
          }
          else
            ++stateCount[index2];
        }
        if (FinderPatternFinder.FoundPatternCross(stateCount))
          this.HandlePossibleCenter(stateCount, index1, width);
      }
      FinderPattern[][] finderPatternArray1 = this.SelectMutipleBestPatterns();
      List<FinderPatternInfo> finderPatternInfoList = new List<FinderPatternInfo>();
      foreach (FinderPattern[] finderPatternArray2 in finderPatternArray1)
      {
        ResultPoint.OrderBestPatterns((ResultPoint[]) finderPatternArray2);
        finderPatternInfoList.Add(new FinderPatternInfo(finderPatternArray2));
      }
      if (finderPatternInfoList.Count == 0)
        return MultiFinderPatternFinder.EMPTY_RESULT_ARRAY;
      FinderPatternInfo[] multi = new FinderPatternInfo[finderPatternInfoList.Count];
      for (int index = 0; index < finderPatternInfoList.Count; ++index)
        multi[index] = finderPatternInfoList[index];
      return multi;
    }

    private class ModuleSizeComparator : Comparator
    {
      public virtual int Compare(object center1, object center2)
      {
        float num = ((FinderPattern) center2).EstimatedModuleSize - ((FinderPattern) center1).EstimatedModuleSize;
        if ((double) num < 0.0)
          return -1;
        return (double) num <= 0.0 ? 0 : 1;
      }
    }
  }
}
