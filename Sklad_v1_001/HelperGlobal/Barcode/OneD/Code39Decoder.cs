// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code39Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class Code39Decoder : OneDDecoder
  {
    internal const string AlphabetString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. *$/+%";
    private static readonly char[] Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. *$/+%".ToCharArray();
    internal static readonly int[] CharacterEncodings = new int[44]
    {
      52,
      289,
      97,
      352,
      49,
      304,
      112,
      37,
      292,
      100,
      265,
      73,
      328,
      25,
      280,
      88,
      13,
      268,
      76,
      28,
      259,
      67,
      322,
      19,
      274,
      82,
      7,
      262,
      70,
      22,
      385,
      193,
      448,
      145,
      400,
      208,
      133,
      388,
      196,
      148,
      168,
      162,
      138,
      42
    };
    private static readonly int AsteriskEncoding = Code39Decoder.CharacterEncodings[39];
    private readonly bool usingCheckDigit;
    private readonly bool extendedMode;
    private readonly StringBuilder decodeRowResult;
    private readonly int[] counters;

    public Code39Decoder()
      : this(false)
    {
    }

    public Code39Decoder(bool usingCheckDigit)
      : this(usingCheckDigit, false)
    {
    }

    public Code39Decoder(bool usingCheckDigit, bool extendedMode)
    {
      this.usingCheckDigit = usingCheckDigit;
      this.extendedMode = extendedMode;
      this.decodeRowResult = new StringBuilder(20);
      this.counters = new int[9];
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int[] counters = this.counters;
      for (int index = 0; index < counters.Length; ++index)
        counters[index] = 0;
      StringBuilder decodeRowResult = this.decodeRowResult;
      decodeRowResult.Length = 0;
      int[] asteriskPattern = Code39Decoder.FindAsteriskPattern(row, counters);
      int nextSet = row.GetNextSet(asteriskPattern[1]);
      int size = row.GetSize();
      char ch;
      int num1;
      do
      {
        OneDDecoder.RecordPattern(row, nextSet, counters);
        int narrowWidePattern = Code39Decoder.ToNarrowWidePattern(counters);
        ch = narrowWidePattern >= 0 ? Code39Decoder.PatternToChar(narrowWidePattern) : throw NotFoundException.Instance;
        decodeRowResult.Append(ch);
        num1 = nextSet;
        foreach (int num2 in counters)
          nextSet += num2;
        nextSet = row.GetNextSet(nextSet);
      }
      while (ch != '*');
      --decodeRowResult.Length;
      int num3 = 0;
      foreach (int num4 in counters)
        num3 += num4;
      int num5 = nextSet - num1 - num3;
      if (nextSet != size && num5 >> 1 < num3)
        throw NotFoundException.Instance;
      if (this.usingCheckDigit)
      {
        int index1 = decodeRowResult.Length - 1;
        int num6 = 0;
        for (int index2 = 0; index2 < index1; ++index2)
          num6 += "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. *$/+%".IndexOf(this.decodeRowResult[index2]);
        if ((int) decodeRowResult[index1] != (int) Code39Decoder.Alphabet[num6 % 43])
          throw ChecksumException.Instance;
        decodeRowResult.Length = index1;
      }
      if (decodeRowResult.Length == 0)
        throw NotFoundException.Instance;
      string text = !this.extendedMode ? decodeRowResult.ToString() : Code39Decoder.DecodeExtended(decodeRowResult);
      float x1 = (float) (asteriskPattern[1] + asteriskPattern[0]) / 2f;
      float x2 = (float) (nextSet + num1) / 2f;
      ResultPointCallback resultPointCallback = decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.NeedResultPointCallback) ? (ResultPointCallback) null : (ResultPointCallback) decodingOptions[DecodeOptions.NeedResultPointCallback];
      if (resultPointCallback != null)
      {
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x1, (float) rowNumber));
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x2, (float) rowNumber));
      }
      return new Result(text, (byte[]) null, new ResultPoint[2]
      {
        new ResultPoint(x1, (float) rowNumber),
        new ResultPoint(x2, (float) rowNumber)
      }, BarcodeFormat.Code39);
    }

    private static int[] FindAsteriskPattern(BitArray row, int[] counters)
    {
      int size = row.GetSize();
      int nextSet = row.GetNextSet(0);
      int index = 0;
      int end = nextSet;
      bool flag = false;
      int length = counters.Length;
      for (int i = nextSet; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++counters[index];
        }
        else
        {
          if (index == length - 1)
          {
            if (Code39Decoder.ToNarrowWidePattern(counters) == Code39Decoder.AsteriskEncoding && row.IsRange(Math.Max(0, end - (i - end >> 1)), end, false))
              return new int[2]{ end, i };
            end += counters[0] + counters[1];
            Array.Copy((Array) counters, 2, (Array) counters, 0, length - 2);
            counters[length - 2] = 0;
            counters[length - 1] = 0;
            --index;
          }
          else
            ++index;
          counters[index] = 1;
          flag = !flag;
        }
      }
      throw NotFoundException.Instance;
    }

    private static int ToNarrowWidePattern(int[] counters)
    {
      int length = counters.Length;
      int num1 = 0;
      int num2;
      do
      {
        int num3 = int.MaxValue;
        foreach (int counter in counters)
        {
          if (counter < num3 && counter > num1)
            num3 = counter;
        }
        num1 = num3;
        num2 = 0;
        int num4 = 0;
        int narrowWidePattern = 0;
        for (int index = 0; index < length; ++index)
        {
          int counter = counters[index];
          if (counter > num1)
          {
            narrowWidePattern |= 1 << length - 1 - index;
            ++num2;
            num4 += counter;
          }
        }
        if (num2 == 3)
        {
          for (int index = 0; index < length && num2 > 0; ++index)
          {
            int counter = counters[index];
            if (counter > num1)
            {
              --num2;
              if (counter << 1 >= num4)
                return -1;
            }
          }
          return narrowWidePattern;
        }
      }
      while (num2 > 3);
      return -1;
    }

    private static char PatternToChar(int pattern)
    {
      for (int index = 0; index < Code39Decoder.CharacterEncodings.Length; ++index)
      {
        if (Code39Decoder.CharacterEncodings[index] == pattern)
          return Code39Decoder.Alphabet[index];
      }
      throw NotFoundException.Instance;
    }

    private static string DecodeExtended(StringBuilder encoded)
    {
      int length = encoded.Length;
      StringBuilder stringBuilder = new StringBuilder(length);
      for (int index = 0; index < length; ++index)
      {
        char ch1 = encoded[index];
        switch (ch1)
        {
          case '$':
          case '%':
          case '+':
          case '/':
            char ch2 = encoded[index + 1];
            char ch3 = char.MinValue;
            switch (ch1)
            {
              case '$':
                if (ch2 < 'A' || ch2 > 'Z')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = (char) ((uint) ch2 - 64U);
                break;
              case '%':
                if (ch2 >= 'A' && ch2 <= 'E')
                {
                  ch3 = (char) ((uint) ch2 - 38U);
                  break;
                }
                if (ch2 < 'F' || ch2 > 'W')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = (char) ((uint) ch2 - 11U);
                break;
              case '+':
                if (ch2 < 'A' || ch2 > 'Z')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = (char) ((uint) ch2 + 32U);
                break;
              case '/':
                if (ch2 >= 'A' && ch2 <= 'O')
                {
                  ch3 = (char) ((uint) ch2 - 32U);
                  break;
                }
                if (ch2 != 'Z')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = ':';
                break;
            }
            stringBuilder.Append(ch3);
            ++index;
            break;
          default:
            stringBuilder.Append(ch1);
            break;
        }
      }
      return stringBuilder.ToString();
    }
  }
}
