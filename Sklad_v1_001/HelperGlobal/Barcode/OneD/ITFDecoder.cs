// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.ITFDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class ITFDecoder : OneDDecoder
  {
    private const int MAX_AVG_VARIANCE = 107;
    private const int MAX_INDIVIDUAL_VARIANCE = 204;
    private const int W = 3;
    private const int N = 1;
    private static readonly int[] DEFAULT_ALLOWED_LENGTHS = new int[10]
    {
      44,
      24,
      20,
      18,
      16,
      14,
      12,
      10,
      8,
      6
    };
    private int narrowLineWidth;
    private static readonly int[] START_PATTERN = new int[4]
    {
      1,
      1,
      1,
      1
    };
    private static readonly int[] END_PATTERN_REVERSED = new int[3]
    {
      1,
      1,
      3
    };
    internal static readonly int[][] PATTERNS = new int[10][]
    {
      new int[5]{ 1, 1, 3, 3, 1 },
      new int[5]{ 3, 1, 1, 1, 3 },
      new int[5]{ 1, 3, 1, 1, 3 },
      new int[5]{ 3, 3, 1, 1, 1 },
      new int[5]{ 1, 1, 3, 1, 3 },
      new int[5]{ 3, 1, 3, 1, 1 },
      new int[5]{ 1, 3, 3, 1, 1 },
      new int[5]{ 1, 1, 1, 3, 3 },
      new int[5]{ 3, 1, 1, 3, 1 },
      new int[5]{ 1, 3, 1, 3, 1 }
    };

    public ITFDecoder() => this.narrowLineWidth = -1;

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int[] numArray1 = this.DecodeStart(row);
      int[] numArray2 = this.DecodeEnd(row);
      StringBuilder resultString = new StringBuilder(20);
      ITFDecoder.DecodeMiddle(row, numArray1[1], numArray2[0], resultString);
      string text = resultString.ToString();
      int[] numArray3 = (int[]) null;
      if (decodingOptions != null)
        numArray3 = (int[]) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.AllowedLengths);
      if (numArray3 == null)
        numArray3 = ITFDecoder.DEFAULT_ALLOWED_LENGTHS;
      int length = text.Length;
      bool flag = false;
      foreach (int num in numArray3)
      {
        if (length == num)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        throw MessagingToolkit.Barcode.FormatException.Instance;
      ResultPointCallback resultPointCallback = decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.NeedResultPointCallback) ? (ResultPointCallback) null : (ResultPointCallback) decodingOptions[DecodeOptions.NeedResultPointCallback];
      if (resultPointCallback != null)
      {
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint((float) numArray1[1], (float) rowNumber));
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint((float) numArray2[0], (float) rowNumber));
      }
      return new Result(text, (byte[]) null, new ResultPoint[2]
      {
        new ResultPoint((float) numArray1[1], (float) rowNumber),
        new ResultPoint((float) numArray2[0], (float) rowNumber)
      }, BarcodeFormat.ITF14);
    }

    private static void DecodeMiddle(
      BitArray row,
      int payloadStart,
      int payloadEnd,
      StringBuilder resultString)
    {
      int[] counters1 = new int[10];
      int[] counters2 = new int[5];
      int[] counters3 = new int[5];
      while (payloadStart < payloadEnd)
      {
        OneDDecoder.RecordPattern(row, payloadStart, counters1);
        for (int index1 = 0; index1 < 5; ++index1)
        {
          int index2 = index1 << 1;
          counters2[index1] = counters1[index2];
          counters3[index1] = counters1[index2 + 1];
        }
        int num1 = ITFDecoder.DecodeDigit(counters2);
        resultString.Append((char) (48 + num1));
        int num2 = ITFDecoder.DecodeDigit(counters3);
        resultString.Append((char) (48 + num2));
        foreach (int num3 in counters1)
          payloadStart += num3;
      }
    }

    internal int[] DecodeStart(BitArray row)
    {
      int rowOffset = ITFDecoder.SkipWhiteSpace(row);
      int[] guardPattern = ITFDecoder.FindGuardPattern(row, rowOffset, ITFDecoder.START_PATTERN);
      this.narrowLineWidth = guardPattern[1] - guardPattern[0] >> 2;
      this.ValidateQuietZone(row, guardPattern[0]);
      return guardPattern;
    }

    private void ValidateQuietZone(BitArray row, int startPattern)
    {
      int num = this.narrowLineWidth * 10;
      for (int i = startPattern - 1; num > 0 && i >= 0 && !row.Get(i); --i)
        --num;
      if (num != 0)
        throw NotFoundException.Instance;
    }

    private static int SkipWhiteSpace(BitArray row)
    {
      int size = row.GetSize();
      int nextSet = row.GetNextSet(0);
      return nextSet != size ? nextSet : throw NotFoundException.Instance;
    }

    internal int[] DecodeEnd(BitArray row)
    {
      row.Reverse();
      try
      {
        int rowOffset = ITFDecoder.SkipWhiteSpace(row);
        int[] guardPattern = ITFDecoder.FindGuardPattern(row, rowOffset, ITFDecoder.END_PATTERN_REVERSED);
        this.ValidateQuietZone(row, guardPattern[0]);
        int num = guardPattern[0];
        guardPattern[0] = row.GetSize() - guardPattern[1];
        guardPattern[1] = row.GetSize() - num;
        return guardPattern;
      }
      finally
      {
        row.Reverse();
      }
    }

    private static int[] FindGuardPattern(BitArray row, int rowOffset, int[] pattern)
    {
      int length = pattern.Length;
      int[] numArray = new int[length];
      int size = row.GetSize();
      bool flag = false;
      int index = 0;
      int num = rowOffset;
      for (int i = rowOffset; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++numArray[index];
        }
        else
        {
          if (index == length - 1)
          {
            if (OneDDecoder.PatternMatchVariance(numArray, pattern, 204) < 107)
              return new int[2]{ num, i };
            num += numArray[0] + numArray[1];
            Array.Copy((Array) numArray, 2, (Array) numArray, 0, length - 2);
            numArray[length - 2] = 0;
            numArray[length - 1] = 0;
            --index;
          }
          else
            ++index;
          numArray[index] = 1;
          flag = !flag;
        }
      }
      throw NotFoundException.Instance;
    }

    private static int DecodeDigit(int[] counters)
    {
      int num1 = 107;
      int num2 = -1;
      int length = ITFDecoder.PATTERNS.Length;
      for (int index = 0; index < length; ++index)
      {
        int[] pattern = ITFDecoder.PATTERNS[index];
        int num3 = OneDDecoder.PatternMatchVariance(counters, pattern, 204);
        if (num3 < num1)
        {
          num1 = num3;
          num2 = index;
        }
      }
      return num2 >= 0 ? num2 : throw NotFoundException.Instance;
    }
  }
}
