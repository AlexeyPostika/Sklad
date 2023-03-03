// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Rss14Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss
{
  public sealed class Rss14Decoder : AbstractRssDecoder
  {
    private static readonly int[] OutsideEvenTotalSubset = new int[5]
    {
      1,
      10,
      34,
      70,
      126
    };
    private static readonly int[] InsideOddTotalSubset = new int[4]
    {
      4,
      20,
      48,
      81
    };
    private static readonly int[] OutsideGSum = new int[5]
    {
      0,
      161,
      961,
      2015,
      2715
    };
    private static readonly int[] InsideGSum = new int[4]
    {
      0,
      336,
      1036,
      1516
    };
    private static readonly int[] OutsideOddWidest = new int[5]
    {
      8,
      6,
      4,
      3,
      1
    };
    private static readonly int[] InsideOddWidest = new int[4]
    {
      2,
      4,
      6,
      8
    };
    private static readonly int[][] FinderPatterns = new int[9][]
    {
      new int[4]{ 3, 8, 2, 1 },
      new int[4]{ 3, 5, 5, 1 },
      new int[4]{ 3, 3, 7, 1 },
      new int[4]{ 3, 1, 9, 1 },
      new int[4]{ 2, 7, 4, 1 },
      new int[4]{ 2, 5, 6, 1 },
      new int[4]{ 2, 3, 8, 1 },
      new int[4]{ 1, 5, 7, 1 },
      new int[4]{ 1, 3, 9, 1 }
    };
    private readonly List<Pair> possibleLeftPairs;
    private readonly List<Pair> possibleRightPairs;

    public Rss14Decoder()
    {
      this.possibleLeftPairs = new List<Pair>();
      this.possibleRightPairs = new List<Pair>();
    }

    public override Result DecodeRow(
      int rowNumber,
      MessagingToolkit.Barcode.Common.BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      Rss14Decoder.AddOrTally(this.possibleLeftPairs, this.DecodePair(row, false, rowNumber, decodingOptions));
      row.Reverse();
      Rss14Decoder.AddOrTally(this.possibleRightPairs, this.DecodePair(row, true, rowNumber, decodingOptions));
      row.Reverse();
      int count1 = this.possibleLeftPairs.Count;
      for (int index1 = 0; index1 < count1; ++index1)
      {
        Pair possibleLeftPair = this.possibleLeftPairs[index1];
        if (possibleLeftPair.Count > 1)
        {
          int count2 = this.possibleRightPairs.Count;
          for (int index2 = 0; index2 < count2; ++index2)
          {
            Pair possibleRightPair = this.possibleRightPairs[index2];
            if (possibleRightPair.Count > 1 && Rss14Decoder.CheckChecksum(possibleLeftPair, possibleRightPair))
              return Rss14Decoder.ConstructResult(possibleLeftPair, possibleRightPair);
          }
        }
      }
      throw NotFoundException.Instance;
    }

    private static void AddOrTally(List<Pair> possiblePairs, Pair pair)
    {
      if (pair == null)
        return;
      IEnumerator enumerator = (IEnumerator) possiblePairs.GetEnumerator();
      bool flag = false;
      while (enumerator.MoveNext())
      {
        Pair current = (Pair) enumerator.Current;
        if (current.Value == pair.Value)
        {
          current.IncrementCount();
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      possiblePairs.Add(pair);
    }

    public override void Reset()
    {
      this.possibleLeftPairs.RemoveRange(0, this.possibleLeftPairs.Count);
      this.possibleRightPairs.RemoveRange(0, this.possibleRightPairs.Count);
    }

    private static Result ConstructResult(Pair leftPair, Pair rightPair)
    {
      string str = (4537077L * (long) leftPair.Value + (long) rightPair.Value).ToString();
      StringBuilder stringBuilder = new StringBuilder(14);
      for (int index = 13 - str.Length; index > 0; --index)
        stringBuilder.Append('0');
      stringBuilder.Append(str);
      int num1 = 0;
      for (int index = 0; index < 13; ++index)
      {
        int num2 = (int) stringBuilder[index] - 48;
        num1 += (index & 1) == 0 ? 3 * num2 : num2;
      }
      int num3 = 10 - num1 % 10;
      if (num3 == 10)
        num3 = 0;
      stringBuilder.Append(num3);
      ResultPoint[] resultPoints1 = leftPair.FinderPattern.ResultPoints;
      ResultPoint[] resultPoints2 = rightPair.FinderPattern.ResultPoints;
      return new Result(stringBuilder.ToString().ToString(), (byte[]) null, new ResultPoint[4]
      {
        resultPoints1[0],
        resultPoints1[1],
        resultPoints2[0],
        resultPoints2[1]
      }, BarcodeFormat.RSS14);
    }

    private static bool CheckChecksum(Pair leftPair, Pair rightPair)
    {
      int num1 = (leftPair.ChecksumPortion + 16 * rightPair.ChecksumPortion) % 79;
      int num2 = 9 * leftPair.FinderPattern.Value + rightPair.FinderPattern.Value;
      if (num2 > 72)
        --num2;
      if (num2 > 8)
        --num2;
      return num1 == num2;
    }

    private Pair DecodePair(
      MessagingToolkit.Barcode.Common.BitArray row,
      bool right,
      int rowNumber,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      try
      {
        int[] finderPattern = this.FindFinderPattern(row, 0, right);
        FinderPattern foundFinderPattern = this.ParseFoundFinderPattern(row, rowNumber, right, finderPattern);
        ResultPointCallback resultPointCallback = decodingOptions == null ? (ResultPointCallback) null : (ResultPointCallback) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.NeedResultPointCallback);
        if (resultPointCallback != null)
        {
          float x = (float) (finderPattern[0] + finderPattern[1]) / 2f;
          if (right)
            x = (float) (row.GetSize() - 1) - x;
          resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x, (float) rowNumber));
        }
        DataCharacter dataCharacter1 = this.DecodeDataCharacter(row, foundFinderPattern, true);
        DataCharacter dataCharacter2 = this.DecodeDataCharacter(row, foundFinderPattern, false);
        return new Pair(1597 * dataCharacter1.Value + dataCharacter2.Value, dataCharacter1.ChecksumPortion + 4 * dataCharacter2.ChecksumPortion, foundFinderPattern);
      }
      catch (NotFoundException ex)
      {
        return (Pair) null;
      }
    }

    private DataCharacter DecodeDataCharacter(
      MessagingToolkit.Barcode.Common.BitArray row,
      FinderPattern pattern,
      bool outsideChar)
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
      if (outsideChar)
      {
        OneDDecoder.RecordPatternInReverse(row, pattern.StartEnd[0], characterCounters);
      }
      else
      {
        OneDDecoder.RecordPattern(row, pattern.StartEnd[1] + 1, characterCounters);
        int index1 = 0;
        for (int index2 = characterCounters.Length - 1; index1 < index2; --index2)
        {
          int num = characterCounters[index1];
          characterCounters[index1] = characterCounters[index2];
          characterCounters[index2] = num;
          ++index1;
        }
      }
      int numModules = outsideChar ? 16 : 15;
      float num1 = (float) AbstractRssDecoder.Count(characterCounters) / (float) numModules;
      int[] oddCounts = this.oddCounts;
      int[] evenCounts = this.evenCounts;
      float[] oddRoundingErrors = this.oddRoundingErrors;
      float[] evenRoundingErrors = this.evenRoundingErrors;
      for (int index3 = 0; index3 < characterCounters.Length; ++index3)
      {
        float num2 = (float) characterCounters[index3] / num1;
        int num3 = (int) ((double) num2 + 0.5);
        if (num3 < 1)
          num3 = 1;
        else if (num3 > 8)
          num3 = 8;
        int index4 = index3 >> 1;
        if ((index3 & 1) == 0)
        {
          oddCounts[index4] = num3;
          oddRoundingErrors[index4] = num2 - (float) num3;
        }
        else
        {
          evenCounts[index4] = num3;
          evenRoundingErrors[index4] = num2 - (float) num3;
        }
      }
      this.AdjustOddEvenCounts(outsideChar, numModules);
      int num4 = 0;
      int num5 = 0;
      for (int index = oddCounts.Length - 1; index >= 0; --index)
      {
        num5 = num5 * 9 + oddCounts[index];
        num4 += oddCounts[index];
      }
      int num6 = 0;
      int num7 = 0;
      for (int index = evenCounts.Length - 1; index >= 0; --index)
      {
        num6 = num6 * 9 + evenCounts[index];
        num7 += evenCounts[index];
      }
      int checksumPortion = num5 + 3 * num6;
      if (outsideChar)
      {
        if ((num4 & 1) != 0 || num4 > 12 || num4 < 4)
          throw NotFoundException.Instance;
        int index = (12 - num4) / 2;
        int maxWidth1 = Rss14Decoder.OutsideOddWidest[index];
        int maxWidth2 = 9 - maxWidth1;
        int rssValue1 = RssUtils.GetRssValue(oddCounts, maxWidth1, false);
        int rssValue2 = RssUtils.GetRssValue(evenCounts, maxWidth2, true);
        int num8 = Rss14Decoder.OutsideEvenTotalSubset[index];
        int num9 = Rss14Decoder.OutsideGSum[index];
        return new DataCharacter(rssValue1 * num8 + rssValue2 + num9, checksumPortion);
      }
      if ((num7 & 1) != 0 || num7 > 10 || num7 < 4)
        throw NotFoundException.Instance;
      int index5 = (10 - num7) / 2;
      int maxWidth3 = Rss14Decoder.InsideOddWidest[index5];
      int maxWidth4 = 9 - maxWidth3;
      int rssValue3 = RssUtils.GetRssValue(oddCounts, maxWidth3, true);
      int rssValue4 = RssUtils.GetRssValue(evenCounts, maxWidth4, false);
      int num10 = Rss14Decoder.InsideOddTotalSubset[index5];
      int num11 = Rss14Decoder.InsideGSum[index5];
      return new DataCharacter(rssValue4 * num10 + rssValue3 + num11, checksumPortion);
    }

    private int[] FindFinderPattern(MessagingToolkit.Barcode.Common.BitArray row, int rowOffset, bool rightFinderPattern)
    {
      int[] decodeFinderCounters = this.decodeFinderCounters;
      decodeFinderCounters[0] = 0;
      decodeFinderCounters[1] = 0;
      decodeFinderCounters[2] = 0;
      decodeFinderCounters[3] = 0;
      int size = row.GetSize();
      bool flag = false;
      for (; rowOffset < size; ++rowOffset)
      {
        flag = !row.Get(rowOffset);
        if (rightFinderPattern == flag)
          break;
      }
      int index = 0;
      int num = rowOffset;
      for (int i = rowOffset; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++decodeFinderCounters[index];
        }
        else
        {
          if (index == 3)
          {
            if (AbstractRssDecoder.IsFinderPattern(decodeFinderCounters))
              return new int[2]{ num, i };
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
          flag = !flag;
        }
      }
      throw NotFoundException.Instance;
    }

    private FinderPattern ParseFoundFinderPattern(
      MessagingToolkit.Barcode.Common.BitArray row,
      int rowNumber,
      bool right,
      int[] startEnd)
    {
      bool flag = row.Get(startEnd[0]);
      int i = startEnd[0] - 1;
      while (i >= 0 && flag ^ row.Get(i))
        --i;
      int num1 = i + 1;
      int num2 = startEnd[0] - num1;
      int[] decodeFinderCounters = this.decodeFinderCounters;
      for (int index = decodeFinderCounters.Length - 1; index > 0; --index)
        decodeFinderCounters[index] = decodeFinderCounters[index - 1];
      decodeFinderCounters[0] = num2;
      int finderValue = AbstractRssDecoder.ParseFinderValue(decodeFinderCounters, Rss14Decoder.FinderPatterns);
      int start = num1;
      int end = startEnd[1];
      if (right)
      {
        start = row.GetSize() - 1 - start;
        end = row.GetSize() - 1 - end;
      }
      return new FinderPattern(finderValue, new int[2]
      {
        num1,
        startEnd[1]
      }, start, end, rowNumber);
    }

    private void AdjustOddEvenCounts(bool outsideChar, int numModules)
    {
      int num1 = AbstractRssDecoder.Count(this.oddCounts);
      int num2 = AbstractRssDecoder.Count(this.evenCounts);
      int num3 = num1 + num2 - numModules;
      bool flag1 = (num1 & 1) == (outsideChar ? 1 : 0);
      bool flag2 = (num2 & 1) == 1;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      if (outsideChar)
      {
        if (num1 > 12)
          flag4 = true;
        else if (num1 < 4)
          flag3 = true;
        if (num2 > 12)
          flag6 = true;
        else if (num2 < 4)
          flag5 = true;
      }
      else
      {
        if (num1 > 11)
          flag4 = true;
        else if (num1 < 5)
          flag3 = true;
        if (num2 > 10)
          flag6 = true;
        else if (num2 < 4)
          flag5 = true;
      }
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
