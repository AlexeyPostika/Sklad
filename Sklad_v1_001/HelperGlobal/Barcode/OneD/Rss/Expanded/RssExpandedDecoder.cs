// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.RssExpandedDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded
{
  public sealed class RssExpandedDecoder : AbstractRssDecoder
  {
    private const int FinderPatA = 0;
    private const int FinderPatB = 1;
    private const int FinderPatC = 2;
    private const int FinderPatD = 3;
    private const int FinderPatE = 4;
    private const int FinderPatF = 5;
    private const int MaxPairs = 11;
    private static readonly int[] SymbolWidest = new int[5]
    {
      7,
      5,
      4,
      3,
      1
    };
    private static readonly int[] EventTotalSubset = new int[5]
    {
      4,
      20,
      52,
      104,
      204
    };
    private static readonly int[] GSum = new int[5]
    {
      0,
      348,
      1388,
      2948,
      3988
    };
    private static readonly int[][] FinderPatterns = new int[6][]
    {
      new int[4]{ 1, 8, 4, 1 },
      new int[4]{ 3, 6, 4, 1 },
      new int[4]{ 3, 4, 6, 1 },
      new int[4]{ 3, 2, 8, 1 },
      new int[4]{ 2, 6, 5, 1 },
      new int[4]{ 2, 2, 9, 1 }
    };
    private static readonly int[][] Weights = new int[23][]
    {
      new int[8]{ 1, 3, 9, 27, 81, 32, 96, 77 },
      new int[8]{ 20, 60, 180, 118, 143, 7, 21, 63 },
      new int[8]{ 189, 145, 13, 39, 117, 140, 209, 205 },
      new int[8]{ 193, 157, 49, 147, 19, 57, 171, 91 },
      new int[8]{ 62, 186, 136, 197, 169, 85, 44, 132 },
      new int[8]{ 185, 133, 188, 142, 4, 12, 36, 108 },
      new int[8]{ 113, 128, 173, 97, 80, 29, 87, 50 },
      new int[8]{ 150, 28, 84, 41, 123, 158, 52, 156 },
      new int[8]{ 46, 138, 203, 187, 139, 206, 196, 166 },
      new int[8]{ 76, 17, 51, 153, 37, 111, 122, 155 },
      new int[8]{ 43, 129, 176, 106, 107, 110, 119, 146 },
      new int[8]{ 16, 48, 144, 10, 30, 90, 59, 177 },
      new int[8]{ 109, 116, 137, 200, 178, 112, 125, 164 },
      new int[8]{ 70, 210, 208, 202, 184, 130, 179, 115 },
      new int[8]{ 134, 191, 151, 31, 93, 68, 204, 190 },
      new int[8]{ 148, 22, 66, 198, 172, 94, 71, 2 },
      new int[8]{ 6, 18, 54, 162, 64, 192, 154, 40 },
      new int[8]{ 120, 149, 25, 75, 14, 42, 126, 167 },
      new int[8]{ 79, 26, 78, 23, 69, 207, 199, 175 },
      new int[8]{ 103, 98, 83, 38, 114, 131, 182, 124 },
      new int[8]
      {
        161,
        61,
        183,
        (int) sbyte.MaxValue,
        170,
        88,
        53,
        159
      },
      new int[8]{ 55, 165, 73, 8, 24, 72, 5, 15 },
      new int[8]{ 45, 135, 194, 160, 58, 174, 100, 89 }
    };
    private static readonly int[][] FinderPatternSequences = new int[10][]
    {
      new int[2],
      new int[3]{ 0, 1, 1 },
      new int[4]{ 0, 2, 1, 3 },
      new int[5]{ 0, 4, 1, 3, 2 },
      new int[6]{ 0, 4, 1, 3, 3, 5 },
      new int[7]{ 0, 4, 1, 3, 4, 5, 5 },
      new int[8]{ 0, 0, 1, 1, 2, 2, 3, 3 },
      new int[9]{ 0, 0, 1, 1, 2, 2, 3, 4, 4 },
      new int[10]{ 0, 0, 1, 1, 2, 2, 3, 4, 5, 5 },
      new int[11]{ 0, 0, 1, 1, 2, 3, 3, 4, 4, 5, 5 }
    };
    private readonly List<ExpandedPair> pairs;
    private readonly List<ExpandedRow> rows;
    private readonly int[] startEnd;
    private bool startFromEven;

    public RssExpandedDecoder()
    {
      this.pairs = new List<ExpandedPair>(11);
      this.rows = new List<ExpandedRow>();
      this.startEnd = new int[2];
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      this.pairs.Clear();
      this.startFromEven = false;
      try
      {
        this.DecodeRow2pairs(rowNumber, row);
        return RssExpandedDecoder.ConstructResult(this.pairs);
      }
      catch (NotFoundException ex)
      {
      }
      this.pairs.Clear();
      this.startFromEven = true;
      this.DecodeRow2pairs(rowNumber, row);
      return RssExpandedDecoder.ConstructResult(this.pairs);
    }

    public override void Reset()
    {
      this.pairs.Clear();
      this.rows.Clear();
    }

    private List<ExpandedPair> DecodeRow2pairs(int rowNumber, BitArray row)
    {
      try
      {
        while (true)
          this.pairs.Add(this.RetrieveNextPair(row, this.pairs, rowNumber));
      }
      catch (NotFoundException ex)
      {
        if (this.pairs.Count == 0)
          throw ex;
      }
      if (this.CheckChecksum())
        return this.pairs;
      bool flag = this.rows.Count != 0;
      bool wasReversed = false;
      this.StoreRow(rowNumber, wasReversed);
      if (flag)
      {
        List<ExpandedPair> expandedPairList1 = this.CheckRows(false);
        if (expandedPairList1 != null)
          return expandedPairList1;
        List<ExpandedPair> expandedPairList2 = this.CheckRows(true);
        if (expandedPairList2 != null)
          return expandedPairList2;
      }
      throw NotFoundException.Instance;
    }

    private List<ExpandedPair> CheckRows(bool reverse)
    {
      if (this.rows.Count > 25)
      {
        this.rows.Clear();
        return (List<ExpandedPair>) null;
      }
      this.pairs.Clear();
      if (reverse)
        this.rows.Reverse();
      List<ExpandedPair> expandedPairList = (List<ExpandedPair>) null;
      try
      {
        expandedPairList = this.CheckRows(new List<ExpandedRow>(), 0);
      }
      catch (NotFoundException ex)
      {
      }
      if (reverse)
        this.rows.Reverse();
      return expandedPairList;
    }

    private List<ExpandedPair> CheckRows(
      List<ExpandedRow> collectedRows,
      int currentRow)
    {
      for (int index1 = currentRow; index1 < this.rows.Count; ++index1)
      {
        ExpandedRow row = this.rows[index1];
        this.pairs.Clear();
        int count = collectedRows.Count;
        for (int index2 = 0; index2 < count; ++index2)
          this.pairs.AddRange((IEnumerable<ExpandedPair>) collectedRows[index2].Pairs);
        this.pairs.AddRange((IEnumerable<ExpandedPair>) row.Pairs);
        if (RssExpandedDecoder.IsValidSequence(this.pairs))
        {
          if (this.CheckChecksum())
            return this.pairs;
          List<ExpandedRow> collectedRows1 = new List<ExpandedRow>();
          collectedRows1.AddRange((IEnumerable<ExpandedRow>) collectedRows);
          collectedRows1.Add(row);
          try
          {
            return this.CheckRows(collectedRows1, index1 + 1);
          }
          catch (NotFoundException ex)
          {
          }
        }
      }
      throw NotFoundException.Instance;
    }

    private static bool IsValidSequence(List<ExpandedPair> pairs)
    {
      foreach (int[] finderPatternSequence in RssExpandedDecoder.FinderPatternSequences)
      {
        if (pairs.Count <= finderPatternSequence.Length)
        {
          bool flag = true;
          for (int index = 0; index < pairs.Count; ++index)
          {
            if (pairs[index].FinderPattern.Value != finderPatternSequence[index])
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return true;
        }
      }
      return false;
    }

    private void StoreRow(int rowNumber, bool wasReversed)
    {
      int index = 0;
      bool flag1 = false;
      bool flag2 = false;
      for (; index < this.rows.Count; ++index)
      {
        ExpandedRow row = this.rows[index];
        if (row.RowNumber > rowNumber)
        {
          flag2 = row.IsEquivalent(this.pairs);
          break;
        }
        flag1 = row.IsEquivalent(this.pairs);
      }
      if (flag2 || flag1 || RssExpandedDecoder.IsPartialRow((IEnumerable<ExpandedPair>) this.pairs, (IEnumerable<ExpandedRow>) this.rows))
        return;
      this.rows.Insert(index, new ExpandedRow(this.pairs, rowNumber, wasReversed));
      RssExpandedDecoder.RemovePartialRows(this.pairs, this.rows);
    }

    private static void RemovePartialRows(List<ExpandedPair> pairs, List<ExpandedRow> rows)
    {
      for (int index = 0; index < rows.Count; ++index)
      {
        ExpandedRow row = rows[index];
        if (row.Pairs.Count != pairs.Count)
        {
          bool flag1 = true;
          foreach (ExpandedPair pair1 in row.Pairs)
          {
            bool flag2 = false;
            foreach (ExpandedPair pair2 in pairs)
            {
              if (pair1.Equals((object) pair2))
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
            {
              flag1 = false;
              break;
            }
          }
          if (flag1)
            rows.RemoveAt(index);
        }
      }
    }

    private static bool IsPartialRow(IEnumerable<ExpandedPair> pairs, IEnumerable<ExpandedRow> rows)
    {
      foreach (ExpandedRow row in rows)
      {
        bool flag1 = true;
        foreach (ExpandedPair pair1 in pairs)
        {
          bool flag2 = false;
          foreach (ExpandedPair pair2 in row.Pairs)
          {
            if (pair1.Equals((object) pair2))
            {
              flag2 = true;
              break;
            }
          }
          if (!flag2)
          {
            flag1 = false;
            break;
          }
        }
        if (flag1)
          return true;
      }
      return false;
    }

    internal List<ExpandedRow> Rows => this.rows;

    private static Result ConstructResult(List<ExpandedPair> pairs)
    {
      string information = AbstractExpandedDecoder.CreateDecoder(BitArrayBuilder.BuildBitArray(pairs)).ParseInformation();
      ResultPoint[] resultPoints1 = pairs[0].FinderPattern.ResultPoints;
      ResultPoint[] resultPoints2 = pairs[pairs.Count - 1].FinderPattern.ResultPoints;
      return new Result(information, (byte[]) null, new ResultPoint[4]
      {
        resultPoints1[0],
        resultPoints1[1],
        resultPoints2[0],
        resultPoints2[1]
      }, BarcodeFormat.RSSExpanded);
    }

    private bool CheckChecksum()
    {
      ExpandedPair pair1 = this.pairs[0];
      DataCharacter leftChar = pair1.LeftChar;
      DataCharacter rightChar = pair1.RightChar;
      if (rightChar == null)
        return false;
      int checksumPortion = rightChar.ChecksumPortion;
      int num1 = 2;
      for (int index = 1; index < this.pairs.Count; ++index)
      {
        ExpandedPair pair2 = this.pairs[index];
        checksumPortion += pair2.LeftChar.ChecksumPortion;
        ++num1;
        if (pair2.RightChar != null)
        {
          checksumPortion += pair2.RightChar.ChecksumPortion;
          ++num1;
        }
      }
      int num2 = checksumPortion % 211;
      return 211 * (num1 - 4) + num2 == leftChar.Value;
    }

    private static int GetNextSecondBar(BitArray row, int initialPos)
    {
      int nextSecondBar;
      if (row.Get(initialPos))
      {
        int nextUnset = row.GetNextUnset(initialPos);
        nextSecondBar = row.GetNextSet(nextUnset);
      }
      else
      {
        int nextSet = row.GetNextSet(initialPos);
        nextSecondBar = row.GetNextUnset(nextSet);
      }
      return nextSecondBar;
    }

    internal ExpandedPair RetrieveNextPair(
      BitArray row,
      List<ExpandedPair> previousPairs,
      int rowNumber)
    {
      bool flag1 = previousPairs.Count % 2 == 0;
      if (this.startFromEven)
        flag1 = !flag1;
      bool flag2 = true;
      int forcedOffset = -1;
      FinderPattern foundFinderPattern;
      do
      {
        this.FindNextPair(row, previousPairs, forcedOffset);
        foundFinderPattern = this.ParseFoundFinderPattern(row, rowNumber, flag1);
        if (foundFinderPattern == null)
          forcedOffset = RssExpandedDecoder.GetNextSecondBar(row, this.startEnd[0]);
        else
          flag2 = false;
      }
      while (flag2);
      DataCharacter leftChar = this.DecodeDataCharacter(row, foundFinderPattern, flag1, true);
      if (previousPairs.Count != 0)
      {
        if (previousPairs[previousPairs.Count - 1].MustBeLast)
          throw NotFoundException.Instance;
      }
      DataCharacter rightChar;
      try
      {
        rightChar = this.DecodeDataCharacter(row, foundFinderPattern, flag1, false);
      }
      catch (NotFoundException ex)
      {
        rightChar = (DataCharacter) null;
      }
      bool mayBeLast = true;
      return new ExpandedPair(leftChar, rightChar, foundFinderPattern, mayBeLast);
    }

    private void FindNextPair(BitArray row, List<ExpandedPair> previousPairs, int forcedOffset)
    {
      int[] decodeFinderCounters = this.decodeFinderCounters;
      decodeFinderCounters[0] = 0;
      decodeFinderCounters[1] = 0;
      decodeFinderCounters[2] = 0;
      decodeFinderCounters[3] = 0;
      int size = row.GetSize();
      int i1 = forcedOffset < 0 ? (previousPairs.Count != 0 ? previousPairs[previousPairs.Count - 1].FinderPattern.StartEnd[1] : 0) : forcedOffset;
      bool flag1 = previousPairs.Count % 2 != 0;
      if (this.startFromEven)
        flag1 = !flag1;
      bool flag2 = false;
      for (; i1 < size; ++i1)
      {
        flag2 = !row.Get(i1);
        if (!flag2)
          break;
      }
      int index = 0;
      int num = i1;
      for (int i2 = i1; i2 < size; ++i2)
      {
        if (row.Get(i2) ^ flag2)
        {
          ++decodeFinderCounters[index];
        }
        else
        {
          if (index == 3)
          {
            if (flag1)
              RssExpandedDecoder.ReverseCounters(decodeFinderCounters);
            if (AbstractRssDecoder.IsFinderPattern(decodeFinderCounters))
            {
              this.startEnd[0] = num;
              this.startEnd[1] = i2;
              return;
            }
            if (flag1)
              RssExpandedDecoder.ReverseCounters(decodeFinderCounters);
            num += decodeFinderCounters[0] + decodeFinderCounters[1];
            decodeFinderCounters[0] = decodeFinderCounters[2];
            decodeFinderCounters[1] = decodeFinderCounters[3];
            decodeFinderCounters[2] = 0;
            decodeFinderCounters[3] = 0;
            --index;
          }
          else
            ++index;
          decodeFinderCounters[index] = 1;
          flag2 = !flag2;
        }
      }
      throw NotFoundException.Instance;
    }

    private static void ReverseCounters(int[] counters)
    {
      int length = counters.Length;
      for (int index = 0; index < length / 2; ++index)
      {
        int counter = counters[index];
        counters[index] = counters[length - index - 1];
        counters[length - index - 1] = counter;
      }
    }

    private FinderPattern ParseFoundFinderPattern(
      BitArray row,
      int rowNumber,
      bool oddPattern)
    {
      int num1;
      int start;
      int nextUnset;
      if (oddPattern)
      {
        int i = this.startEnd[0] - 1;
        while (i >= 0 && !row.Get(i))
          --i;
        int num2 = i + 1;
        num1 = this.startEnd[0] - num2;
        start = num2;
        nextUnset = this.startEnd[1];
      }
      else
      {
        start = this.startEnd[0];
        nextUnset = row.GetNextUnset(this.startEnd[1] + 1);
        num1 = nextUnset - this.startEnd[1];
      }
      int[] decodeFinderCounters = this.decodeFinderCounters;
      for (int index = decodeFinderCounters.Length - 1; index > 0; --index)
        decodeFinderCounters[index] = decodeFinderCounters[index - 1];
      decodeFinderCounters[0] = num1;
      int finderValue;
      try
      {
        finderValue = AbstractRssDecoder.ParseFinderValue(decodeFinderCounters, RssExpandedDecoder.FinderPatterns);
      }
      catch (NotFoundException ex)
      {
        return (FinderPattern) null;
      }
      return new FinderPattern(finderValue, new int[2]
      {
        start,
        nextUnset
      }, start, nextUnset, rowNumber);
    }

    internal DataCharacter DecodeDataCharacter(
      BitArray row,
      FinderPattern pattern,
      bool isOddPattern,
      bool leftChar)
    {
      int[] characterCounters = this.dataCharacterCounters;
      characterCounters[0] = 0;
      characterCounters[1] = 0;
      characterCounters[2] = 0;
      characterCounters[3] = 0;
      characterCounters[4] = 0;
      characterCounters[5] = 0;
      characterCounters[6] = 0;
      characterCounters[7] = 0;
      if (leftChar)
      {
        OneDDecoder.RecordPatternInReverse(row, pattern.StartEnd[0], characterCounters);
      }
      else
      {
        OneDDecoder.RecordPattern(row, pattern.StartEnd[1], characterCounters);
        int index1 = 0;
        for (int index2 = characterCounters.Length - 1; index1 < index2; --index2)
        {
          int num = characterCounters[index1];
          characterCounters[index1] = characterCounters[index2];
          characterCounters[index2] = num;
          ++index1;
        }
      }
      float num1 = (float) AbstractRssDecoder.Count(characterCounters) / 17f;
      float num2 = (float) (pattern.StartEnd[1] - pattern.StartEnd[0]) / 15f;
      if ((double) Math.Abs(num1 - num2) / (double) num2 > 0.300000011920929)
        throw NotFoundException.Instance;
      int[] oddCounts = this.oddCounts;
      int[] evenCounts = this.evenCounts;
      float[] oddRoundingErrors = this.oddRoundingErrors;
      float[] evenRoundingErrors = this.evenRoundingErrors;
      for (int index3 = 0; index3 < characterCounters.Length; ++index3)
      {
        float num3 = 1f * (float) characterCounters[index3] / num1;
        int num4 = (int) ((double) num3 + 0.5);
        if (num4 < 1)
        {
          if ((double) num3 < 0.300000011920929)
            throw NotFoundException.Instance;
          num4 = 1;
        }
        else if (num4 > 8)
        {
          if ((double) num3 > 8.69999980926514)
            throw NotFoundException.Instance;
          num4 = 8;
        }
        int index4 = index3 >> 1;
        if ((index3 & 1) == 0)
        {
          oddCounts[index4] = num4;
          oddRoundingErrors[index4] = num3 - (float) num4;
        }
        else
        {
          evenCounts[index4] = num4;
          evenRoundingErrors[index4] = num3 - (float) num4;
        }
      }
      this.AdjustOddEvenCounts(17);
      int index5 = 4 * pattern.Value + (isOddPattern ? 0 : 2) + (leftChar ? 0 : 1) - 1;
      int num5 = 0;
      int num6 = 0;
      for (int index6 = oddCounts.Length - 1; index6 >= 0; --index6)
      {
        if (RssExpandedDecoder.IsNotA1left(pattern, isOddPattern, leftChar))
        {
          int num7 = RssExpandedDecoder.Weights[index5][2 * index6];
          num6 += oddCounts[index6] * num7;
        }
        num5 += oddCounts[index6];
      }
      int num8 = 0;
      int num9 = 0;
      for (int index7 = evenCounts.Length - 1; index7 >= 0; --index7)
      {
        if (RssExpandedDecoder.IsNotA1left(pattern, isOddPattern, leftChar))
        {
          int num10 = RssExpandedDecoder.Weights[index5][2 * index7 + 1];
          num8 += evenCounts[index7] * num10;
        }
        num9 += evenCounts[index7];
      }
      int checksumPortion = num6 + num8;
      if ((num5 & 1) != 0 || num5 > 13 || num5 < 4)
        throw NotFoundException.Instance;
      int index8 = (13 - num5) / 2;
      int maxWidth1 = RssExpandedDecoder.SymbolWidest[index8];
      int maxWidth2 = 9 - maxWidth1;
      int rssValue1 = RssUtils.GetRssValue(oddCounts, maxWidth1, true);
      int rssValue2 = RssUtils.GetRssValue(evenCounts, maxWidth2, false);
      int num11 = RssExpandedDecoder.EventTotalSubset[index8];
      int num12 = RssExpandedDecoder.GSum[index8];
      return new DataCharacter(rssValue1 * num11 + rssValue2 + num12, checksumPortion);
    }

    private static bool IsNotA1left(FinderPattern pattern, bool isOddPattern, bool leftChar) => pattern.Value != 0 || !isOddPattern || !leftChar;

    private void AdjustOddEvenCounts(int numModules)
    {
      int num1 = AbstractRssDecoder.Count(this.oddCounts);
      int num2 = AbstractRssDecoder.Count(this.evenCounts);
      int num3 = num1 + num2 - numModules;
      bool flag1 = (num1 & 1) == 1;
      bool flag2 = (num2 & 1) == 0;
      bool flag3 = false;
      bool flag4 = false;
      if (num1 > 13)
        flag4 = true;
      else if (num1 < 4)
        flag3 = true;
      bool flag5 = false;
      bool flag6 = false;
      if (num2 > 13)
        flag6 = true;
      else if (num2 < 4)
        flag5 = true;
      switch (num3)
      {
        case -1:
          if (flag1)
          {
            if (flag2)
              throw NotFoundException.Instance;
            flag3 = true;
            break;
          }
          if (!flag2)
            throw NotFoundException.Instance;
          flag5 = true;
          break;
        case 0:
          if (flag1)
          {
            if (!flag2)
              throw NotFoundException.Instance;
            if (num1 < num2)
            {
              flag3 = true;
              flag6 = true;
              break;
            }
            flag4 = true;
            flag5 = true;
            break;
          }
          if (flag2)
            throw NotFoundException.Instance;
          break;
        case 1:
          if (flag1)
          {
            if (flag2)
              throw NotFoundException.Instance;
            flag4 = true;
            break;
          }
          if (!flag2)
            throw NotFoundException.Instance;
          flag6 = true;
          break;
        default:
          throw NotFoundException.Instance;
      }
      if (flag3)
      {
        if (flag4)
          throw NotFoundException.Instance;
        AbstractRssDecoder.Increment(this.oddCounts, this.oddRoundingErrors);
      }
      if (flag4)
        AbstractRssDecoder.Decrement(this.oddCounts, this.oddRoundingErrors);
      if (flag5)
      {
        if (flag6)
          throw NotFoundException.Instance;
        AbstractRssDecoder.Increment(this.evenCounts, this.oddRoundingErrors);
      }
      if (!flag6)
        return;
      AbstractRssDecoder.Decrement(this.evenCounts, this.evenRoundingErrors);
    }
  }
}
