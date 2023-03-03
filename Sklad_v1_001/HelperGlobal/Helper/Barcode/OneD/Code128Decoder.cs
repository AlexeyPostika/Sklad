// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code128Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class Code128Decoder : OneDDecoder
  {
    private const int MAX_AVG_VARIANCE = 64;
    private const int MAX_INDIVIDUAL_VARIANCE = 179;
    private const int CODE_SHIFT = 98;
    private const int CODE_CODE_C = 99;
    private const int CODE_CODE_B = 100;
    private const int CODE_CODE_A = 101;
    private const int CODE_FNC_1 = 102;
    private const int CODE_FNC_2 = 97;
    private const int CODE_FNC_3 = 96;
    private const int CODE_FNC_4_A = 101;
    private const int CODE_FNC_4_B = 100;
    private const int CODE_START_A = 103;
    private const int CODE_START_B = 104;
    private const int CODE_START_C = 105;
    private const int CODE_STOP = 106;
    internal static readonly int[][] CODE_PATTERNS = new int[107][]
    {
      new int[6]{ 2, 1, 2, 2, 2, 2 },
      new int[6]{ 2, 2, 2, 1, 2, 2 },
      new int[6]{ 2, 2, 2, 2, 2, 1 },
      new int[6]{ 1, 2, 1, 2, 2, 3 },
      new int[6]{ 1, 2, 1, 3, 2, 2 },
      new int[6]{ 1, 3, 1, 2, 2, 2 },
      new int[6]{ 1, 2, 2, 2, 1, 3 },
      new int[6]{ 1, 2, 2, 3, 1, 2 },
      new int[6]{ 1, 3, 2, 2, 1, 2 },
      new int[6]{ 2, 2, 1, 2, 1, 3 },
      new int[6]{ 2, 2, 1, 3, 1, 2 },
      new int[6]{ 2, 3, 1, 2, 1, 2 },
      new int[6]{ 1, 1, 2, 2, 3, 2 },
      new int[6]{ 1, 2, 2, 1, 3, 2 },
      new int[6]{ 1, 2, 2, 2, 3, 1 },
      new int[6]{ 1, 1, 3, 2, 2, 2 },
      new int[6]{ 1, 2, 3, 1, 2, 2 },
      new int[6]{ 1, 2, 3, 2, 2, 1 },
      new int[6]{ 2, 2, 3, 2, 1, 1 },
      new int[6]{ 2, 2, 1, 1, 3, 2 },
      new int[6]{ 2, 2, 1, 2, 3, 1 },
      new int[6]{ 2, 1, 3, 2, 1, 2 },
      new int[6]{ 2, 2, 3, 1, 1, 2 },
      new int[6]{ 3, 1, 2, 1, 3, 1 },
      new int[6]{ 3, 1, 1, 2, 2, 2 },
      new int[6]{ 3, 2, 1, 1, 2, 2 },
      new int[6]{ 3, 2, 1, 2, 2, 1 },
      new int[6]{ 3, 1, 2, 2, 1, 2 },
      new int[6]{ 3, 2, 2, 1, 1, 2 },
      new int[6]{ 3, 2, 2, 2, 1, 1 },
      new int[6]{ 2, 1, 2, 1, 2, 3 },
      new int[6]{ 2, 1, 2, 3, 2, 1 },
      new int[6]{ 2, 3, 2, 1, 2, 1 },
      new int[6]{ 1, 1, 1, 3, 2, 3 },
      new int[6]{ 1, 3, 1, 1, 2, 3 },
      new int[6]{ 1, 3, 1, 3, 2, 1 },
      new int[6]{ 1, 1, 2, 3, 1, 3 },
      new int[6]{ 1, 3, 2, 1, 1, 3 },
      new int[6]{ 1, 3, 2, 3, 1, 1 },
      new int[6]{ 2, 1, 1, 3, 1, 3 },
      new int[6]{ 2, 3, 1, 1, 1, 3 },
      new int[6]{ 2, 3, 1, 3, 1, 1 },
      new int[6]{ 1, 1, 2, 1, 3, 3 },
      new int[6]{ 1, 1, 2, 3, 3, 1 },
      new int[6]{ 1, 3, 2, 1, 3, 1 },
      new int[6]{ 1, 1, 3, 1, 2, 3 },
      new int[6]{ 1, 1, 3, 3, 2, 1 },
      new int[6]{ 1, 3, 3, 1, 2, 1 },
      new int[6]{ 3, 1, 3, 1, 2, 1 },
      new int[6]{ 2, 1, 1, 3, 3, 1 },
      new int[6]{ 2, 3, 1, 1, 3, 1 },
      new int[6]{ 2, 1, 3, 1, 1, 3 },
      new int[6]{ 2, 1, 3, 3, 1, 1 },
      new int[6]{ 2, 1, 3, 1, 3, 1 },
      new int[6]{ 3, 1, 1, 1, 2, 3 },
      new int[6]{ 3, 1, 1, 3, 2, 1 },
      new int[6]{ 3, 3, 1, 1, 2, 1 },
      new int[6]{ 3, 1, 2, 1, 1, 3 },
      new int[6]{ 3, 1, 2, 3, 1, 1 },
      new int[6]{ 3, 3, 2, 1, 1, 1 },
      new int[6]{ 3, 1, 4, 1, 1, 1 },
      new int[6]{ 2, 2, 1, 4, 1, 1 },
      new int[6]{ 4, 3, 1, 1, 1, 1 },
      new int[6]{ 1, 1, 1, 2, 2, 4 },
      new int[6]{ 1, 1, 1, 4, 2, 2 },
      new int[6]{ 1, 2, 1, 1, 2, 4 },
      new int[6]{ 1, 2, 1, 4, 2, 1 },
      new int[6]{ 1, 4, 1, 1, 2, 2 },
      new int[6]{ 1, 4, 1, 2, 2, 1 },
      new int[6]{ 1, 1, 2, 2, 1, 4 },
      new int[6]{ 1, 1, 2, 4, 1, 2 },
      new int[6]{ 1, 2, 2, 1, 1, 4 },
      new int[6]{ 1, 2, 2, 4, 1, 1 },
      new int[6]{ 1, 4, 2, 1, 1, 2 },
      new int[6]{ 1, 4, 2, 2, 1, 1 },
      new int[6]{ 2, 4, 1, 2, 1, 1 },
      new int[6]{ 2, 2, 1, 1, 1, 4 },
      new int[6]{ 4, 1, 3, 1, 1, 1 },
      new int[6]{ 2, 4, 1, 1, 1, 2 },
      new int[6]{ 1, 3, 4, 1, 1, 1 },
      new int[6]{ 1, 1, 1, 2, 4, 2 },
      new int[6]{ 1, 2, 1, 1, 4, 2 },
      new int[6]{ 1, 2, 1, 2, 4, 1 },
      new int[6]{ 1, 1, 4, 2, 1, 2 },
      new int[6]{ 1, 2, 4, 1, 1, 2 },
      new int[6]{ 1, 2, 4, 2, 1, 1 },
      new int[6]{ 4, 1, 1, 2, 1, 2 },
      new int[6]{ 4, 2, 1, 1, 1, 2 },
      new int[6]{ 4, 2, 1, 2, 1, 1 },
      new int[6]{ 2, 1, 2, 1, 4, 1 },
      new int[6]{ 2, 1, 4, 1, 2, 1 },
      new int[6]{ 4, 1, 2, 1, 2, 1 },
      new int[6]{ 1, 1, 1, 1, 4, 3 },
      new int[6]{ 1, 1, 1, 3, 4, 1 },
      new int[6]{ 1, 3, 1, 1, 4, 1 },
      new int[6]{ 1, 1, 4, 1, 1, 3 },
      new int[6]{ 1, 1, 4, 3, 1, 1 },
      new int[6]{ 4, 1, 1, 1, 1, 3 },
      new int[6]{ 4, 1, 1, 3, 1, 1 },
      new int[6]{ 1, 1, 3, 1, 4, 1 },
      new int[6]{ 1, 1, 4, 1, 3, 1 },
      new int[6]{ 3, 1, 1, 1, 4, 1 },
      new int[6]{ 4, 1, 1, 1, 3, 1 },
      new int[6]{ 2, 1, 1, 4, 1, 2 },
      new int[6]{ 2, 1, 1, 2, 1, 4 },
      new int[6]{ 2, 1, 1, 2, 3, 2 },
      new int[7]{ 2, 3, 3, 1, 1, 1, 2 }
    };

    private static int[] FindStartPattern(BitArray row)
    {
      int size = row.GetSize();
      int nextSet = row.GetNextSet(0);
      int index1 = 0;
      int[] numArray = new int[6];
      int end = nextSet;
      bool flag = false;
      int length = numArray.Length;
      for (int i = nextSet; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++numArray[index1];
        }
        else
        {
          if (index1 == length - 1)
          {
            int num1 = 64;
            int num2 = -1;
            for (int index2 = 103; index2 <= 105; ++index2)
            {
              int num3 = OneDDecoder.PatternMatchVariance(numArray, Code128Decoder.CODE_PATTERNS[index2], 179);
              if (num3 < num1)
              {
                num1 = num3;
                num2 = index2;
              }
            }
            if (num2 >= 0 && row.IsRange(Math.Max(0, end - (i - end) / 2), end, false))
              return new int[3]{ end, i, num2 };
            end += numArray[0] + numArray[1];
            Array.Copy((Array) numArray, 2, (Array) numArray, 0, length - 2);
            numArray[length - 2] = 0;
            numArray[length - 1] = 0;
            --index1;
          }
          else
            ++index1;
          numArray[index1] = 1;
          flag = !flag;
        }
      }
      throw NotFoundException.Instance;
    }

    private static int DecodeCode(BitArray row, int[] counters, int rowOffset)
    {
      OneDDecoder.RecordPattern(row, rowOffset, counters);
      int num1 = 64;
      int num2 = -1;
      for (int index = 0; index < Code128Decoder.CODE_PATTERNS.Length; ++index)
      {
        int[] pattern = Code128Decoder.CODE_PATTERNS[index];
        int num3 = OneDDecoder.PatternMatchVariance(counters, pattern, 179);
        if (num3 < num1)
        {
          num1 = num3;
          num2 = index;
        }
      }
      return num2 >= 0 ? num2 : throw NotFoundException.Instance;
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int[] startPattern = Code128Decoder.FindStartPattern(row);
      int num1 = startPattern[2];
      int num2;
      switch (num1)
      {
        case 103:
          num2 = 101;
          break;
        case 104:
          num2 = 100;
          break;
        case 105:
          num2 = 99;
          break;
        default:
          throw MessagingToolkit.Barcode.FormatException.Instance;
      }
      bool flag1 = false;
      bool flag2 = false;
      StringBuilder stringBuilder = new StringBuilder(20);
      IList<byte> byteList = (IList<byte>) new List<byte>(20);
      int num3 = startPattern[0];
      int num4 = startPattern[1];
      int[] counters = new int[6];
      int num5 = 0;
      int num6 = 0;
      int num7 = num1;
      int num8 = 0;
      bool flag3 = true;
      while (!flag1)
      {
        bool flag4 = flag2;
        flag2 = false;
        num5 = num6;
        num6 = Code128Decoder.DecodeCode(row, counters, num4);
        byteList.Add((byte) num6);
        if (num6 != 106)
          flag3 = true;
        if (num6 != 106)
        {
          ++num8;
          num7 += num8 * num6;
        }
        num3 = num4;
        foreach (int num9 in counters)
          num4 += num9;
        switch (num6)
        {
          case 103:
          case 104:
          case 105:
            throw MessagingToolkit.Barcode.FormatException.Instance;
          default:
            switch (num2)
            {
              case 99:
                if (num6 < 100)
                {
                  if (num6 < 10)
                    stringBuilder.Append('0');
                  stringBuilder.Append(num6);
                  break;
                }
                if (num6 != 106)
                  flag3 = false;
                switch (num6 - 100)
                {
                  case 0:
                    num2 = 100;
                    break;
                  case 1:
                    num2 = 101;
                    break;
                  case 6:
                    flag1 = true;
                    break;
                }
                break;
              case 100:
                if (num6 < 96)
                {
                  stringBuilder.Append((char) (32 + num6));
                  break;
                }
                if (num6 != 106)
                  flag3 = false;
                switch (num6 - 96)
                {
                  case 2:
                    flag2 = true;
                    num2 = 101;
                    break;
                  case 3:
                    num2 = 99;
                    break;
                  case 5:
                    num2 = 101;
                    break;
                  case 10:
                    flag1 = true;
                    break;
                }
                break;
              case 101:
                if (num6 < 64)
                {
                  stringBuilder.Append((char) (32 + num6));
                  break;
                }
                if (num6 < 96)
                {
                  stringBuilder.Append((char) (num6 - 64));
                  break;
                }
                if (num6 != 106)
                  flag3 = false;
                switch (num6 - 96)
                {
                  case 2:
                    flag2 = true;
                    num2 = 100;
                    break;
                  case 3:
                    num2 = 99;
                    break;
                  case 4:
                    num2 = 100;
                    break;
                  case 10:
                    flag1 = true;
                    break;
                }
                break;
            }
            if (flag4)
            {
              num2 = num2 == 101 ? 100 : 101;
              continue;
            }
            continue;
        }
      }
      int nextUnset = row.GetNextUnset(num4);
      if (!row.IsRange(nextUnset, Math.Min(row.GetSize(), nextUnset + (nextUnset - num3) / 2), false))
        throw NotFoundException.Instance;
      if ((num7 - num8 * num5) % 103 != num5)
        throw ChecksumException.Instance;
      int length = stringBuilder.Length;
      if (length == 0)
        throw NotFoundException.Instance;
      if (length > 0 && flag3)
      {
        if (num2 == 99)
          stringBuilder.Remove(length - 2, 2);
        else
          stringBuilder.Remove(length - 1, 1);
      }
      float x1 = (float) (startPattern[1] + startPattern[0]) / 2f;
      float x2 = (float) (nextUnset + num3) / 2f;
      ResultPointCallback resultPointCallback = decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.NeedResultPointCallback) ? (ResultPointCallback) null : (ResultPointCallback) decodingOptions[DecodeOptions.NeedResultPointCallback];
      if (resultPointCallback != null)
      {
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x1, (float) rowNumber));
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x2, (float) rowNumber));
      }
      int count = byteList.Count;
      byte[] rawBytes = new byte[count];
      for (int index = 0; index < count; ++index)
        rawBytes[index] = byteList[index];
      return new Result(stringBuilder.ToString(), rawBytes, new ResultPoint[2]
      {
        new ResultPoint(x1, (float) rowNumber),
        new ResultPoint(x2, (float) rowNumber)
      }, BarcodeFormat.Code128);
    }
  }
}
