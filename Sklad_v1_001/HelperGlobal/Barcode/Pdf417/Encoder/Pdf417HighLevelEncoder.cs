// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Encoder.Pdf417HighLevelEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Numerics;
using System.Text;

namespace MessagingToolkit.Barcode.Pdf417.Encoder
{
  internal sealed class Pdf417HighLevelEncoder
  {
    private const int TEXT_COMPACTION = 0;
    private const int BYTE_COMPACTION = 1;
    private const int NUMERIC_COMPACTION = 2;
    private const int SUBMODE_ALPHA = 0;
    private const int SUBMODE_LOWER = 1;
    private const int SUBMODE_MIXED = 2;
    private const int SUBMODE_PUNCTUATION = 3;
    private const int LATCH_TO_TEXT = 900;
    private const int LATCH_TO_BYTE_PADDED = 901;
    private const int LATCH_TO_NUMERIC = 902;
    private const int SHIFT_TO_BYTE = 913;
    private const int LATCH_TO_BYTE = 924;
    private static readonly sbyte[] TEXT_MIXED_RAW = new sbyte[30]
    {
      (sbyte) 48,
      (sbyte) 49,
      (sbyte) 50,
      (sbyte) 51,
      (sbyte) 52,
      (sbyte) 53,
      (sbyte) 54,
      (sbyte) 55,
      (sbyte) 56,
      (sbyte) 57,
      (sbyte) 38,
      (sbyte) 13,
      (sbyte) 9,
      (sbyte) 44,
      (sbyte) 58,
      (sbyte) 35,
      (sbyte) 45,
      (sbyte) 46,
      (sbyte) 36,
      (sbyte) 47,
      (sbyte) 43,
      (sbyte) 37,
      (sbyte) 42,
      (sbyte) 61,
      (sbyte) 94,
      (sbyte) 0,
      (sbyte) 32,
      (sbyte) 0,
      (sbyte) 0,
      (sbyte) 0
    };
    private static readonly sbyte[] TEXT_PUNCTUATION_RAW = new sbyte[30]
    {
      (sbyte) 59,
      (sbyte) 60,
      (sbyte) 62,
      (sbyte) 64,
      (sbyte) 91,
      (sbyte) 92,
      (sbyte) 93,
      (sbyte) 95,
      (sbyte) 96,
      (sbyte) 126,
      (sbyte) 33,
      (sbyte) 13,
      (sbyte) 9,
      (sbyte) 44,
      (sbyte) 58,
      (sbyte) 10,
      (sbyte) 45,
      (sbyte) 46,
      (sbyte) 36,
      (sbyte) 47,
      (sbyte) 34,
      (sbyte) 124,
      (sbyte) 42,
      (sbyte) 40,
      (sbyte) 41,
      (sbyte) 63,
      (sbyte) 123,
      (sbyte) 125,
      (sbyte) 39,
      (sbyte) 0
    };
    private static readonly sbyte[] MIXED = new sbyte[128];
    private static readonly sbyte[] PUNCTUATION = new sbyte[128];

    private Pdf417HighLevelEncoder()
    {
    }

    private static byte[] GetBytesForMessage(string msg) => Encoding.GetEncoding("UTF-8").GetBytes(msg);

    internal static string EncodeHighLevel(string msg, Compaction compaction)
    {
      byte[] bytes = (byte[]) null;
      StringBuilder sb = new StringBuilder(msg.Length);
      int length = msg.Length;
      int startpos = 0;
      int initialSubmode = 0;
      switch (compaction)
      {
        case Compaction.Text:
          Pdf417HighLevelEncoder.EncodeText(msg, startpos, length, sb, initialSubmode);
          break;
        case Compaction.Byte:
          byte[] bytesForMessage = Pdf417HighLevelEncoder.GetBytesForMessage(msg);
          Pdf417HighLevelEncoder.EncodeBinary(bytesForMessage, startpos, bytesForMessage.Length, 1, sb);
          break;
        case Compaction.Numeric:
          sb.Append('Ά');
          Pdf417HighLevelEncoder.EncodeNumeric(msg, startpos, length, sb);
          break;
        default:
          int startmode = 0;
          while (startpos < length)
          {
            int consecutiveDigitCount = Pdf417HighLevelEncoder.DetermineConsecutiveDigitCount(msg, startpos);
            if (consecutiveDigitCount >= 13)
            {
              sb.Append('Ά');
              startmode = 2;
              initialSubmode = 0;
              Pdf417HighLevelEncoder.EncodeNumeric(msg, startpos, consecutiveDigitCount, sb);
              startpos += consecutiveDigitCount;
            }
            else
            {
              int consecutiveTextCount = Pdf417HighLevelEncoder.DetermineConsecutiveTextCount(msg, startpos);
              if (consecutiveTextCount >= 5 || consecutiveDigitCount == length)
              {
                if (startmode != 0)
                {
                  sb.Append('΄');
                  startmode = 0;
                  initialSubmode = 0;
                }
                initialSubmode = Pdf417HighLevelEncoder.EncodeText(msg, startpos, consecutiveTextCount, sb, initialSubmode);
                startpos += consecutiveTextCount;
              }
              else
              {
                if (bytes == null)
                  bytes = Pdf417HighLevelEncoder.GetBytesForMessage(msg);
                int count = Pdf417HighLevelEncoder.DetermineConsecutiveBinaryCount(msg, bytes, startpos);
                if (count == 0)
                  count = 1;
                if (count == 1 && startmode == 0)
                {
                  Pdf417HighLevelEncoder.EncodeBinary(bytes, startpos, 1, 0, sb);
                }
                else
                {
                  Pdf417HighLevelEncoder.EncodeBinary(bytes, startpos, count, startmode, sb);
                  startmode = 1;
                  initialSubmode = 0;
                }
                startpos += count;
              }
            }
          }
          break;
      }
      return sb.ToString();
    }

    private static int EncodeText(
      string msg,
      int startpos,
      int count,
      StringBuilder sb,
      int initialSubmode)
    {
      StringBuilder stringBuilder = new StringBuilder(count);
      int num1 = initialSubmode;
      int num2 = 0;
      do
      {
        char ch = msg[startpos + num2];
        switch (num1)
        {
          case 0:
            if (Pdf417HighLevelEncoder.IsAlphaUpper(ch))
            {
              if (ch == ' ')
              {
                stringBuilder.Append('\u001A');
                break;
              }
              stringBuilder.Append((char) ((uint) ch - 65U));
              break;
            }
            if (Pdf417HighLevelEncoder.IsAlphaLower(ch))
            {
              num1 = 1;
              stringBuilder.Append('\u001B');
              continue;
            }
            if (Pdf417HighLevelEncoder.IsMixed(ch))
            {
              num1 = 2;
              stringBuilder.Append('\u001C');
              continue;
            }
            stringBuilder.Append('\u001D');
            stringBuilder.Append((char) Pdf417HighLevelEncoder.PUNCTUATION[(int) ch]);
            break;
          case 1:
            if (Pdf417HighLevelEncoder.IsAlphaLower(ch))
            {
              if (ch == ' ')
              {
                stringBuilder.Append('\u001A');
                break;
              }
              stringBuilder.Append((char) ((uint) ch - 97U));
              break;
            }
            if (Pdf417HighLevelEncoder.IsAlphaUpper(ch))
            {
              stringBuilder.Append('\u001B');
              stringBuilder.Append((char) ((uint) ch - 65U));
              break;
            }
            if (Pdf417HighLevelEncoder.IsMixed(ch))
            {
              num1 = 2;
              stringBuilder.Append('\u001C');
              continue;
            }
            stringBuilder.Append('\u001D');
            stringBuilder.Append((char) Pdf417HighLevelEncoder.PUNCTUATION[(int) ch]);
            break;
          case 2:
            if (Pdf417HighLevelEncoder.IsMixed(ch))
            {
              stringBuilder.Append((char) Pdf417HighLevelEncoder.MIXED[(int) ch]);
              break;
            }
            if (Pdf417HighLevelEncoder.IsAlphaUpper(ch))
            {
              num1 = 0;
              stringBuilder.Append('\u001C');
              continue;
            }
            if (Pdf417HighLevelEncoder.IsAlphaLower(ch))
            {
              num1 = 1;
              stringBuilder.Append('\u001B');
              continue;
            }
            if (startpos + num2 + 1 < count && Pdf417HighLevelEncoder.IsPunctuation(msg[startpos + num2 + 1]))
            {
              num1 = 3;
              stringBuilder.Append('\u0019');
              continue;
            }
            stringBuilder.Append('\u001D');
            stringBuilder.Append((char) Pdf417HighLevelEncoder.PUNCTUATION[(int) ch]);
            break;
          default:
            if (Pdf417HighLevelEncoder.IsPunctuation(ch))
            {
              stringBuilder.Append((char) Pdf417HighLevelEncoder.PUNCTUATION[(int) ch]);
              break;
            }
            num1 = 0;
            stringBuilder.Append('\u001D');
            continue;
        }
        ++num2;
      }
      while (num2 < count);
      char ch1 = char.MinValue;
      int length = stringBuilder.Length;
      for (int index = 0; index < length; ++index)
      {
        if (index % 2 != 0)
        {
          ch1 = (char) ((uint) ch1 * 30U + (uint) stringBuilder[index]);
          sb.Append(ch1);
        }
        else
          ch1 = stringBuilder[index];
      }
      if (length % 2 != 0)
        sb.Append((char) ((int) ch1 * 30 + 29));
      return num1;
    }

    private static void EncodeBinary(
      byte[] bytes,
      int startpos,
      int count,
      int startmode,
      StringBuilder sb)
    {
      if (count == 1 && startmode == 0)
        sb.Append('Α');
      int num1 = startpos;
      if (count >= 6)
      {
        sb.Append('Μ');
        char[] chArray = new char[5];
        for (; startpos + count - num1 >= 6; num1 += 6)
        {
          long num2 = 0;
          for (int index = 0; index < 6; ++index)
            num2 = (num2 << 8) + (long) ((int) bytes[num1 + index] & (int) byte.MaxValue);
          for (int index = 0; index < 5; ++index)
          {
            chArray[index] = (char) ((ulong) num2 % 900UL);
            num2 /= 900L;
          }
          for (int index = chArray.Length - 1; index >= 0; --index)
            sb.Append(chArray[index]);
        }
      }
      if (num1 < startpos + count)
        sb.Append('΅');
      for (int index = num1; index < startpos + count; ++index)
      {
        int num3 = (int) bytes[index] & (int) byte.MaxValue;
        sb.Append((char) num3);
      }
    }

    private static void EncodeNumeric(string msg, int startpos, int count, StringBuilder sb)
    {
      int num = 0;
      StringBuilder stringBuilder = new StringBuilder(count / 3 + 1);
      BigInteger divisor = new BigInteger(900);
      BigInteger other = new BigInteger(0);
      int length;
      for (; num < count - 1; num += length)
      {
        stringBuilder.Length = 0;
        length = Math.Min(44, count - num);
        BigInteger dividend = BigInteger.Parse('1'.ToString() + msg.Substring(startpos + num, length));
        do
        {
          BigInteger bigInteger = dividend % divisor;
          stringBuilder.Append((char) (ushort) bigInteger);
          dividend = BigInteger.Divide(dividend, divisor);
        }
        while (!dividend.Equals(other));
        for (int index = stringBuilder.Length - 1; index >= 0; --index)
          sb.Append(stringBuilder[index]);
      }
    }

    private static bool IsDigit(char ch) => ch >= '0' && ch <= '9';

    private static bool IsAlphaUpper(char ch)
    {
      if (ch == ' ')
        return true;
      return ch >= 'A' && ch <= 'Z';
    }

    private static bool IsAlphaLower(char ch)
    {
      if (ch == ' ')
        return true;
      return ch >= 'a' && ch <= 'z';
    }

    private static bool IsMixed(char ch) => Pdf417HighLevelEncoder.MIXED[(int) ch] != (sbyte) -1;

    private static bool IsPunctuation(char ch) => Pdf417HighLevelEncoder.PUNCTUATION[(int) ch] != (sbyte) -1;

    private static bool IsText(char ch)
    {
      if (ch == '\t' || ch == '\n' || ch == '\r')
        return true;
      return ch >= ' ' && ch <= '~';
    }

    private static int DetermineConsecutiveDigitCount(string msg, int startpos)
    {
      int consecutiveDigitCount = 0;
      int length = msg.Length;
      int index = startpos;
      if (index < length)
      {
        char ch = msg[index];
        while (Pdf417HighLevelEncoder.IsDigit(ch) && index < length)
        {
          ++consecutiveDigitCount;
          ++index;
          if (index < length)
            ch = msg[index];
        }
      }
      return consecutiveDigitCount;
    }

    private static int DetermineConsecutiveTextCount(string msg, int startpos)
    {
      int length = msg.Length;
      int index = startpos;
      while (index < length)
      {
        char ch = msg[index];
        int num = 0;
        while (num < 13 && Pdf417HighLevelEncoder.IsDigit(ch) && index < length)
        {
          ++num;
          ++index;
          if (index < length)
            ch = msg[index];
        }
        if (num >= 13)
          return index - startpos - num;
        if (num <= 0)
        {
          if (Pdf417HighLevelEncoder.IsText(msg[index]))
            ++index;
          else
            break;
        }
      }
      return index - startpos;
    }

    private static int DetermineConsecutiveBinaryCount(string msg, byte[] bytes, int startpos)
    {
      int length = msg.Length;
      int index1;
      for (index1 = startpos; index1 < length; ++index1)
      {
        char ch1 = msg[index1];
        int num1;
        int index2;
        for (num1 = 0; num1 < 13 && Pdf417HighLevelEncoder.IsDigit(ch1); ch1 = msg[index2])
        {
          ++num1;
          index2 = index1 + num1;
          if (index2 >= length)
            break;
        }
        if (num1 >= 13)
          return index1 - startpos;
        int num2;
        int index3;
        for (num2 = 0; num2 < 5 && Pdf417HighLevelEncoder.IsText(ch1); ch1 = msg[index3])
        {
          ++num2;
          index3 = index1 + num2;
          if (index3 >= length)
            break;
        }
        if (num2 >= 5)
          return index1 - startpos;
        char ch2 = msg[index1];
        if (bytes[index1] == (byte) 63 && ch2 != '?')
          throw new BarcodeEncoderException("Non-encodable character detected: " + (object) ch2 + " (Unicode: " + (object) (int) ch2 + (object) ')');
      }
      return index1 - startpos;
    }

    static Pdf417HighLevelEncoder()
    {
      for (int index = 0; index < Pdf417HighLevelEncoder.MIXED.Length; ++index)
        Pdf417HighLevelEncoder.MIXED[index] = (sbyte) -1;
      for (sbyte index1 = 0; (int) index1 < Pdf417HighLevelEncoder.TEXT_MIXED_RAW.Length; ++index1)
      {
        sbyte index2 = Pdf417HighLevelEncoder.TEXT_MIXED_RAW[(int) index1];
        if (index2 > (sbyte) 0)
          Pdf417HighLevelEncoder.MIXED[(int) index2] = index1;
      }
      for (int index = 0; index < Pdf417HighLevelEncoder.PUNCTUATION.Length; ++index)
        Pdf417HighLevelEncoder.PUNCTUATION[index] = (sbyte) -1;
      for (sbyte index3 = 0; (int) index3 < Pdf417HighLevelEncoder.TEXT_PUNCTUATION_RAW.Length; ++index3)
      {
        sbyte index4 = Pdf417HighLevelEncoder.TEXT_PUNCTUATION_RAW[(int) index3];
        if (index4 > (sbyte) 0)
          Pdf417HighLevelEncoder.PUNCTUATION[(int) index4] = index3;
      }
    }
  }
}
