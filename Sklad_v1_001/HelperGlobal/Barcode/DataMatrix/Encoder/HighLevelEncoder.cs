// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.HighLevelEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  public sealed class HighLevelEncoder
  {
    internal const int ASCII_ENCODATION = 0;
    internal const int C40_ENCODATION = 1;
    internal const int TEXT_ENCODATION = 2;
    internal const int X12_ENCODATION = 3;
    internal const int EDIFACT_ENCODATION = 4;
    internal const int BASE256_ENCODATION = 5;
    internal static char PAD = '\u0081';
    internal static char LATCH_TO_C40 = 'æ';
    internal static char LATCH_TO_BASE256 = 'ç';
    internal static char UPPER_SHIFT = 'ë';
    internal static char MACRO_05 = 'ì';
    internal static char MACRO_06 = 'í';
    internal static char LATCH_TO_ANSIX12 = 'î';
    internal static char LATCH_TO_TEXT = 'ï';
    internal static char LATCH_TO_EDIFACT = 'ð';
    internal static char ECI = 'ñ';
    internal static char C40_UNLATCH = 'þ';
    internal static char X12_UNLATCH = 'þ';
    internal static string MACRO_05_HEADER = "[)>\u001E05\u001D";
    internal static string MACRO_06_HEADER = "[)>\u001E06\u001D";
    internal static string MACRO_TRAILER = "\u001E\u0004";

    private HighLevelEncoder()
    {
    }

    public static byte[] GetBytesForMessage(string msg)
    {
      try
      {
        return Encoding.GetEncoding("CP437").GetBytes(msg);
      }
      catch (ArgumentException ex)
      {
        throw new NotSupportedException("Incompatible environment. The 'CP437' charset is not available!");
      }
    }

    private static char Randomize253State(char ch, int codewordPosition)
    {
      int num1 = 149 * codewordPosition % 253 + 1;
      int num2 = (int) ch + num1;
      return num2 > 254 ? (char) (num2 - 254) : (char) num2;
    }

    private static char Randomize255State(char ch, int codewordPosition)
    {
      int num1 = 149 * codewordPosition % (int) byte.MaxValue + 1;
      int num2 = (int) ch + num1;
      return num2 <= (int) byte.MaxValue ? (char) num2 : (char) (num2 - 256);
    }

    public static string EncodeHighLevel(string msg) => HighLevelEncoder.EncodeHighLevel(msg, SymbolShapeHint.ForceNone, (Dimension) null, (Dimension) null, (Dictionary<EncodeOptions, object>) null);

    public static string EncodeHighLevel(
      string msg,
      SymbolShapeHint shape,
      Dimension minSize,
      Dimension maxSize,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder[] encoderArray = new MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder[6]
      {
        (MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder) new ASCIIEncoder(),
        (MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder) new C40Encoder(),
        (MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder) new TextEncoder(),
        (MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder) new X12Encoder(),
        (MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder) new EdifactEncoder(),
        (MessagingToolkit.Barcode.DataMatrix.Encoder.Encoder) new Base256Encoder()
      };
      EncoderContext context = new EncoderContext(msg);
      context.SymbolShape = shape;
      context.SetSizeConstraints(minSize, maxSize);
      if (msg.StartsWith(HighLevelEncoder.MACRO_05_HEADER) && msg.EndsWith(HighLevelEncoder.MACRO_TRAILER))
      {
        context.WriteCodeword(HighLevelEncoder.MACRO_05);
        context.SkipAtEnd = 2;
        context.pos += HighLevelEncoder.MACRO_05_HEADER.Length;
      }
      else if (msg.StartsWith(HighLevelEncoder.MACRO_06_HEADER) && msg.EndsWith(HighLevelEncoder.MACRO_TRAILER))
      {
        context.WriteCodeword(HighLevelEncoder.MACRO_06);
        context.SkipAtEnd = 2;
        context.pos += HighLevelEncoder.MACRO_06_HEADER.Length;
      }
      int index = 0;
      while (context.HasMoreCharacters())
      {
        encoderArray[index].Encode(context);
        if (context.newEncoding >= 0)
        {
          index = context.newEncoding;
          context.ResetEncoderSignal();
        }
      }
      int length = context.codewords.Length;
      context.UpdateSymbolInfo();
      int dataCapacity = context.symbolInfo.dataCapacity;
      if (length < dataCapacity && index != 0 && index != 5)
        context.WriteCodeword('þ');
      StringBuilder codewords = context.codewords;
      if (codewords.Length < dataCapacity)
        codewords.Append(HighLevelEncoder.PAD);
      while (codewords.Length < dataCapacity)
        codewords.Append(HighLevelEncoder.Randomize253State(HighLevelEncoder.PAD, codewords.Length + 1));
      return context.codewords.ToString();
    }

    internal static int LookAheadTest(string msg, int startpos, int currentMode)
    {
      if (startpos >= msg.Length)
        return currentMode;
      float[] charCounts;
      if (currentMode == 0)
      {
        charCounts = new float[6]
        {
          0.0f,
          1f,
          1f,
          1f,
          1f,
          1.25f
        };
      }
      else
      {
        charCounts = new float[6]
        {
          1f,
          2f,
          2f,
          2f,
          2f,
          2.25f
        };
        charCounts[currentMode] = 0.0f;
      }
      int num = 0;
      while (startpos + num != msg.Length)
      {
        char ch1 = msg[startpos + num];
        ++num;
        if (HighLevelEncoder.IsDigit(ch1))
          charCounts[0] += 0.5f;
        else if (HighLevelEncoder.IsExtendedASCII(ch1))
        {
          charCounts[0] = (float) (int) Math.Ceiling((double) charCounts[0]);
          charCounts[0] += 2f;
        }
        else
        {
          charCounts[0] = (float) (int) Math.Ceiling((double) charCounts[0]);
          ++charCounts[0];
        }
        if (HighLevelEncoder.IsNativeC40(ch1))
          charCounts[1] += 0.6666667f;
        else if (HighLevelEncoder.IsExtendedASCII(ch1))
          charCounts[1] += 2.666667f;
        else
          charCounts[1] += 1.333333f;
        if (HighLevelEncoder.IsNativeText(ch1))
          charCounts[2] += 0.6666667f;
        else if (HighLevelEncoder.IsExtendedASCII(ch1))
          charCounts[2] += 2.666667f;
        else
          charCounts[2] += 1.333333f;
        if (HighLevelEncoder.IsNativeX12(ch1))
          charCounts[3] += 0.6666667f;
        else if (HighLevelEncoder.IsExtendedASCII(ch1))
          charCounts[3] += 4.333333f;
        else
          charCounts[3] += 3.333333f;
        if (HighLevelEncoder.IsNativeEDIFACT(ch1))
          charCounts[4] += 0.75f;
        else if (HighLevelEncoder.IsExtendedASCII(ch1))
          charCounts[4] += 4.25f;
        else
          charCounts[4] += 3.25f;
        if (HighLevelEncoder.IsSpecialB256(ch1))
          charCounts[5] += 4f;
        else
          ++charCounts[5];
        if (num >= 4)
        {
          int[] intCharCounts = new int[6];
          sbyte[] mins = new sbyte[6];
          HighLevelEncoder.FindMinimums(charCounts, intCharCounts, int.MaxValue, mins);
          int minimumCount = HighLevelEncoder.GetMinimumCount(mins);
          if (intCharCounts[0] < intCharCounts[5] && intCharCounts[0] < intCharCounts[1] && intCharCounts[0] < intCharCounts[2] && intCharCounts[0] < intCharCounts[3] && intCharCounts[0] < intCharCounts[4])
            return 0;
          if (intCharCounts[5] < intCharCounts[0] || (int) mins[1] + (int) mins[2] + (int) mins[3] + (int) mins[4] == 0)
            return 5;
          if (minimumCount == 1 && mins[4] > (sbyte) 0)
            return 4;
          if (minimumCount == 1 && mins[2] > (sbyte) 0)
            return 2;
          if (minimumCount == 1 && mins[3] > (sbyte) 0)
            return 3;
          if (intCharCounts[1] + 1 < intCharCounts[0] && intCharCounts[1] + 1 < intCharCounts[5] && intCharCounts[1] + 1 < intCharCounts[4] && intCharCounts[1] + 1 < intCharCounts[2])
          {
            if (intCharCounts[1] < intCharCounts[3])
              return 1;
            if (intCharCounts[1] == intCharCounts[3])
            {
              for (int index = startpos + num + 1; index < msg.Length; ++index)
              {
                char ch2 = msg[index];
                if (HighLevelEncoder.IsX12TermSep(ch2))
                  return 3;
                if (!HighLevelEncoder.IsNativeX12(ch2))
                  break;
              }
              return 1;
            }
          }
        }
      }
      int maxValue = int.MaxValue;
      sbyte[] mins1 = new sbyte[6];
      int[] intCharCounts1 = new int[6];
      int minimums = HighLevelEncoder.FindMinimums(charCounts, intCharCounts1, maxValue, mins1);
      int minimumCount1 = HighLevelEncoder.GetMinimumCount(mins1);
      if (intCharCounts1[0] == minimums)
        return 0;
      if (minimumCount1 == 1 && mins1[5] > (sbyte) 0)
        return 5;
      if (minimumCount1 == 1 && mins1[4] > (sbyte) 0)
        return 4;
      if (minimumCount1 == 1 && mins1[2] > (sbyte) 0)
        return 2;
      return minimumCount1 == 1 && mins1[3] > (sbyte) 0 ? 3 : 1;
    }

    private static int FindMinimums(
      float[] charCounts,
      int[] intCharCounts,
      int min,
      sbyte[] mins)
    {
      for (int index = 0; index < mins.Length; ++index)
        mins[index] = (sbyte) 0;
      for (int index1 = 0; index1 < 6; ++index1)
      {
        intCharCounts[index1] = (int) Math.Ceiling((double) charCounts[index1]);
        int intCharCount = intCharCounts[index1];
        if (min > intCharCount)
        {
          min = intCharCount;
          for (int index2 = 0; index2 < mins.Length; ++index2)
            mins[index2] = (sbyte) 0;
        }
        if (min == intCharCount)
          ++mins[index1];
      }
      return min;
    }

    private static int GetMinimumCount(sbyte[] mins)
    {
      int minimumCount = 0;
      for (int index = 0; index < 6; ++index)
        minimumCount += (int) mins[index];
      return minimumCount;
    }

    internal static bool IsDigit(char ch) => ch >= '0' && ch <= '9';

    internal static bool IsExtendedASCII(char ch) => ch >= '\u0080' && ch <= 'ÿ';

    private static bool IsASCII7(char ch) => ch >= char.MinValue && ch <= '\u007F';

    private static bool IsNativeC40(char ch)
    {
      if (ch == ' ' || ch >= '0' && ch <= '9')
        return true;
      return ch >= 'A' && ch <= 'Z';
    }

    private static bool IsNativeText(char ch)
    {
      if (ch == ' ' || ch >= '0' && ch <= '9')
        return true;
      return ch >= 'a' && ch <= 'z';
    }

    private static bool IsNativeX12(char ch)
    {
      if (HighLevelEncoder.IsX12TermSep(ch) || ch == ' ' || ch >= '0' && ch <= '9')
        return true;
      return ch >= 'A' && ch <= 'Z';
    }

    private static bool IsX12TermSep(char ch) => ch == '\r' || ch == '*' || ch == '>';

    private static bool IsNativeEDIFACT(char ch) => ch >= ' ' && ch <= '^';

    private static bool IsSpecialB256(char ch) => false;

    public static int DetermineConsecutiveDigitCount(string msg, int startpos)
    {
      int consecutiveDigitCount = 0;
      int length = msg.Length;
      int index = startpos;
      if (index < length)
      {
        char ch = msg[index];
        while (HighLevelEncoder.IsDigit(ch) && index < length)
        {
          ++consecutiveDigitCount;
          ++index;
          if (index < length)
            ch = msg[index];
        }
      }
      return consecutiveDigitCount;
    }

    internal static void IllegalCharacter(char c)
    {
      string str1 = Convert.ToInt32(c).ToString("X");
      string str2 = "0000".Substring(0, 4 - str1.Length) + str1;
      throw new ArgumentException("Illegal character: " + (object) c + " (0x" + str2 + (object) ')');
    }
  }
}
