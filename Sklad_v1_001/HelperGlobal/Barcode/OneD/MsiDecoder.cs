// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.MsiDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class MsiDecoder : OneDDecoder
  {
    private const int StartEncoding = 6;
    private const int EndEncoding = 9;
    internal static string AlphabetString = "0123456789";
    private static readonly char[] Alphabet = MsiDecoder.AlphabetString.ToCharArray();
    internal static int[] CharacterEncodings = new int[10]
    {
      2340,
      2342,
      2356,
      2358,
      2468,
      2470,
      2484,
      2486,
      3364,
      3366
    };
    private readonly bool usingCheckDigit;
    private readonly StringBuilder decodeRowResult;
    private readonly int[] counters;
    private int averageCounterWidth;
    private static readonly int[] DoubleAndCrossSum = new int[10]
    {
      0,
      2,
      4,
      6,
      8,
      1,
      3,
      5,
      7,
      9
    };

    public MsiDecoder()
      : this(false)
    {
    }

    public MsiDecoder(bool usingCheckDigit)
    {
      this.usingCheckDigit = usingCheckDigit;
      this.decodeRowResult = new StringBuilder(20);
      this.counters = new int[8];
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      for (int index = 0; index < this.counters.Length; ++index)
        this.counters[index] = 0;
      this.decodeRowResult.Length = 0;
      int[] startPattern = this.FindStartPattern(row, this.counters);
      int num1 = startPattern != null ? row.GetNextSet(startPattern[1]) : throw NotFoundException.Instance;
      int num2;
      char c;
      do
      {
        try
        {
          OneDDecoder.RecordPattern(row, num1, this.counters);
        }
        catch
        {
          int[] endPattern = this.FindEndPattern(row, num1, this.counters);
          if (endPattern == null)
            throw NotFoundException.Instance;
          num2 = num1;
          num1 = endPattern[1];
          break;
        }
        if (!MsiDecoder.PatternToChar(this.ToPattern(this.counters, 8), out c))
        {
          int[] endPattern = this.FindEndPattern(row, num1, this.counters);
          if (endPattern == null)
            throw NotFoundException.Instance;
          num2 = num1;
          num1 = endPattern[1];
          break;
        }
        this.decodeRowResult.Append(c);
        num2 = num1;
        foreach (int counter in this.counters)
          num1 += counter;
        num1 = row.GetNextSet(num1);
      }
      while (c != '*');
      byte[] rawBytes = this.decodeRowResult.Length >= 3 ? Encoding.UTF8.GetBytes(this.decodeRowResult.ToString()) : throw NotFoundException.Instance;
      string text = this.decodeRowResult.ToString();
      if (this.usingCheckDigit)
      {
        string number = text.Substring(0, text.Length - 1);
        if ((int) (ushort) (MsiDecoder.CalculateChecksumLuhn(number) + 48) != (int) text[number.Length])
          throw ChecksumException.Instance;
      }
      float x1 = (float) (startPattern[1] + startPattern[0]) / 2f;
      float x2 = (float) (num1 + num2) / 2f;
      ResultPointCallback resultPointCallback = decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.NeedResultPointCallback) ? (ResultPointCallback) null : (ResultPointCallback) decodingOptions[DecodeOptions.NeedResultPointCallback];
      if (resultPointCallback != null)
      {
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x1, (float) rowNumber));
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x2, (float) rowNumber));
      }
      return new Result(text, rawBytes, new ResultPoint[2]
      {
        new ResultPoint(x1, (float) rowNumber),
        new ResultPoint(x2, (float) rowNumber)
      }, BarcodeFormat.MSIMod10);
    }

    private int[] FindStartPattern(BitArray row, int[] counters)
    {
      int size = row.Size;
      int nextSet = row.GetNextSet(0);
      int index = 0;
      int end = nextSet;
      bool flag = false;
      counters[0] = 0;
      counters[1] = 0;
      for (int i = nextSet; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++counters[index];
        }
        else
        {
          if (index == 1)
          {
            float num = (float) counters[0] / (float) counters[1];
            if ((double) num >= 1.5 && (double) num <= 5.0)
            {
              this.CalculateAverageCounterWidth(counters, 2);
              if (this.ToPattern(counters, 2) == 6 && row.IsRange(Math.Max(0, end - (i - end >> 1)), end, false))
                return new int[2]{ end, i };
            }
            end += counters[0] + counters[1];
            Array.Copy((Array) counters, 2, (Array) counters, 0, 0);
            counters[0] = 0;
            counters[1] = 0;
            --index;
          }
          else
            ++index;
          counters[index] = 1;
          flag = !flag;
        }
      }
      return (int[]) null;
    }

    private int[] FindEndPattern(BitArray row, int rowOffset, int[] counters)
    {
      int size = row.Size;
      int index1 = 0;
      int num1 = rowOffset;
      bool flag = false;
      counters[0] = 0;
      counters[1] = 0;
      counters[2] = 0;
      for (int index2 = rowOffset; index2 < size; ++index2)
      {
        if (row.Get(index2) ^ flag)
        {
          ++counters[index1];
        }
        else
        {
          if (index1 == 2)
          {
            float num2 = (float) counters[1] / (float) counters[0];
            if ((double) num2 >= 1.5 && (double) num2 <= 5.0 && this.ToPattern(counters, 3) == 9)
            {
              int end = Math.Min(row.Size - 1, index2 + (index2 - num1 >> 1));
              if (row.IsRange(index2, end, false))
                return new int[2]{ num1, index2 };
            }
            return (int[]) null;
          }
          ++index1;
          counters[index1] = 1;
          flag = !flag;
        }
      }
      return (int[]) null;
    }

    private void CalculateAverageCounterWidth(int[] counters, int patternLength)
    {
      int num1 = int.MaxValue;
      int num2 = 0;
      for (int index = 0; index < patternLength; ++index)
      {
        int counter = counters[index];
        if (counter < num1)
          num1 = counter;
        if (counter > num2)
          num2 = counter;
      }
      this.averageCounterWidth = ((num2 << 8) + (num1 << 8)) / 2;
    }

    private int ToPattern(int[] counters, int patternLength)
    {
      int pattern = 0;
      int num1 = 1;
      int num2 = 3;
      for (int index = 0; index < patternLength; ++index)
      {
        pattern = counters[index] << 8 >= this.averageCounterWidth ? pattern << 2 | num2 : pattern << 1 | num1;
        num1 ^= 1;
        num2 ^= 3;
      }
      return pattern;
    }

    private static bool PatternToChar(int pattern, out char c)
    {
      for (int index = 0; index < MsiDecoder.CharacterEncodings.Length; ++index)
      {
        if (MsiDecoder.CharacterEncodings[index] == pattern)
        {
          c = MsiDecoder.Alphabet[index];
          return true;
        }
      }
      c = '*';
      return false;
    }

    private static int CalculateChecksumLuhn(string number)
    {
      int num1 = 0;
      for (int index = number.Length - 2; index >= 0; index -= 2)
      {
        int num2 = (int) number[index] - 48;
        num1 += num2;
      }
      for (int index = number.Length - 1; index >= 0; index -= 2)
      {
        int num3 = MsiDecoder.DoubleAndCrossSum[(int) number[index] - 48];
        num1 += num3;
      }
      return (10 - num1 % 10) % 10;
    }
  }
}
