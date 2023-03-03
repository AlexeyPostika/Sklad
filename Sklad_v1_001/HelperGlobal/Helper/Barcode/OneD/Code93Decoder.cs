// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Code93Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class Code93Decoder : OneDDecoder
  {
    private const string AlphabetString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%abcd*";
    private static readonly char[] Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%abcd*".ToCharArray();
    private static readonly int[] CharacterEncodings = new int[48]
    {
      276,
      328,
      324,
      322,
      296,
      292,
      290,
      336,
      274,
      266,
      424,
      420,
      418,
      404,
      402,
      394,
      360,
      356,
      354,
      308,
      282,
      344,
      332,
      326,
      300,
      278,
      436,
      434,
      428,
      422,
      406,
      410,
      364,
      358,
      310,
      314,
      302,
      468,
      466,
      458,
      366,
      374,
      430,
      294,
      474,
      470,
      306,
      350
    };
    private static readonly int AsteriskEncoding = Code93Decoder.CharacterEncodings[47];
    private readonly StringBuilder decodeRowResult;
    private readonly int[] counters;

    public Code93Decoder()
    {
      this.decodeRowResult = new StringBuilder(20);
      this.counters = new int[6];
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int[] asteriskPattern = this.FindAsteriskPattern(row);
      int nextSet = row.GetNextSet(asteriskPattern[1]);
      int size = row.GetSize();
      int[] counters = this.counters;
      for (int index = 0; index < counters.Length; ++index)
        counters[index] = 0;
      StringBuilder decodeRowResult = this.decodeRowResult;
      decodeRowResult.Length = 0;
      char ch;
      int num1;
      do
      {
        OneDDecoder.RecordPattern(row, nextSet, counters);
        int pattern = Code93Decoder.ToPattern(counters);
        ch = pattern >= 0 ? Code93Decoder.PatternToChar(pattern) : throw NotFoundException.Instance;
        decodeRowResult.Append(ch);
        num1 = nextSet;
        foreach (int num2 in counters)
          nextSet += num2;
        nextSet = row.GetNextSet(nextSet);
      }
      while (ch != '*');
      decodeRowResult.Remove(decodeRowResult.Length - 1, 1);
      if (nextSet == size || !row.Get(nextSet))
        throw NotFoundException.Instance;
      if (decodeRowResult.Length < 2)
        throw NotFoundException.Instance;
      Code93Decoder.CheckChecksums(decodeRowResult);
      decodeRowResult.Length -= 2;
      string text = Code93Decoder.DecodeExtended(decodeRowResult);
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
      }, BarcodeFormat.Code93);
    }

    private int[] FindAsteriskPattern(BitArray row)
    {
      int size = row.GetSize();
      int nextSet = row.GetNextSet(0);
      for (int index = 0; index < this.counters.Length; ++index)
        this.counters[index] = 0;
      int[] counters = this.counters;
      int num = nextSet;
      bool flag = false;
      int length = counters.Length;
      int index1 = 0;
      for (int i = nextSet; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++counters[index1];
        }
        else
        {
          if (index1 == length - 1)
          {
            if (Code93Decoder.ToPattern(counters) == Code93Decoder.AsteriskEncoding)
              return new int[2]{ num, i };
            num += counters[0] + counters[1];
            Array.Copy((Array) counters, 2, (Array) counters, 0, length - 2);
            counters[length - 2] = 0;
            counters[length - 1] = 0;
            --index1;
          }
          else
            ++index1;
          counters[index1] = 1;
          flag = !flag;
        }
      }
      throw NotFoundException.Instance;
    }

    private static int ToPattern(int[] counters)
    {
      int length = counters.Length;
      int num1 = 0;
      foreach (int counter in counters)
        num1 += counter;
      int pattern = 0;
      for (int index1 = 0; index1 < length; ++index1)
      {
        int num2 = (counters[index1] << 8) * 9 / num1;
        int num3 = num2 >> 8;
        if ((num2 & (int) byte.MaxValue) > (int) sbyte.MaxValue)
          ++num3;
        if (num3 < 1 || num3 > 4)
          return -1;
        if ((index1 & 1) == 0)
        {
          for (int index2 = 0; index2 < num3; ++index2)
            pattern = pattern << 1 | 1;
        }
        else
          pattern <<= num3;
      }
      return pattern;
    }

    private static char PatternToChar(int pattern)
    {
      for (int index = 0; index < Code93Decoder.CharacterEncodings.Length; ++index)
      {
        if (Code93Decoder.CharacterEncodings[index] == pattern)
          return Code93Decoder.Alphabet[index];
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
          case 'a':
          case 'b':
          case 'c':
          case 'd':
            char ch2 = index < length - 1 ? encoded[index + 1] : throw MessagingToolkit.Barcode.FormatException.Instance;
            char ch3 = char.MinValue;
            switch (ch1)
            {
              case 'a':
                if (ch2 < 'A' || ch2 > 'Z')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = (char) ((uint) ch2 - 64U);
                break;
              case 'b':
                if (ch2 >= 'A' && ch2 <= 'E')
                {
                  ch3 = (char) ((uint) ch2 - 38U);
                  break;
                }
                if (ch2 < 'F' || ch2 > 'W')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = (char) ((uint) ch2 - 11U);
                break;
              case 'c':
                if (ch2 >= 'A' && ch2 <= 'O')
                {
                  ch3 = (char) ((uint) ch2 - 32U);
                  break;
                }
                if (ch2 != 'Z')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = ':';
                break;
              case 'd':
                if (ch2 < 'A' || ch2 > 'Z')
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                ch3 = (char) ((uint) ch2 + 32U);
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

    private static void CheckChecksums(StringBuilder result)
    {
      int length = result.Length;
      Code93Decoder.CheckOneChecksum(result, length - 2, 20);
      Code93Decoder.CheckOneChecksum(result, length - 1, 15);
    }

    private static void CheckOneChecksum(StringBuilder result, int checkPosition, int weightMax)
    {
      int num1 = 1;
      int num2 = 0;
      for (int index = checkPosition - 1; index >= 0; --index)
      {
        num2 += num1 * "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%abcd*".IndexOf(result[index]);
        if (++num1 > weightMax)
          num1 = 1;
      }
      if ((int) result[checkPosition] != (int) Code93Decoder.Alphabet[num2 % 47])
        throw ChecksumException.Instance;
    }
  }
}
