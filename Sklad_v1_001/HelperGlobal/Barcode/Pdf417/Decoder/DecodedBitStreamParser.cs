// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.DecodedBitStreamParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MessagingToolkit.Barcode.Pdf417.Decoder
{
  internal sealed class DecodedBitStreamParser
  {
    private const int TEXT_COMPACTION_MODE_LATCH = 900;
    private const int BYTE_COMPACTION_MODE_LATCH = 901;
    private const int NUMERIC_COMPACTION_MODE_LATCH = 902;
    private const int BYTE_COMPACTION_MODE_LATCH_6 = 924;
    private const int BEGIN_MACRO_PDF417_CONTROL_BLOCK = 928;
    private const int BEGIN_MACRO_PDF417_OPTIONAL_FIELD = 923;
    private const int MACRO_PDF417_TERMINATOR = 922;
    private const int MODE_SHIFT_TO_BYTE_COMPACTION_MODE = 913;
    private const int MAX_NUMERIC_CODEWORDS = 15;
    private const int PL = 25;
    private const int LL = 27;
    private const int AS = 27;
    private const int ML = 28;
    private const int AL = 28;
    private const int PS = 29;
    private const int PAL = 29;
    private static readonly char[] PUNCT_CHARS = new char[29]
    {
      ';',
      '<',
      '>',
      '@',
      '[',
      '\\',
      '}',
      '_',
      '`',
      '~',
      '!',
      '\r',
      '\t',
      ',',
      ':',
      '\n',
      '-',
      '.',
      '$',
      '/',
      '"',
      '|',
      '*',
      '(',
      ')',
      '?',
      '{',
      '}',
      '\''
    };
    private static readonly char[] MIXED_CHARS = new char[25]
    {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      '&',
      '\r',
      '\t',
      ',',
      ':',
      '#',
      '-',
      '.',
      '$',
      '/',
      '+',
      '%',
      '*',
      '=',
      '^'
    };
    private static readonly BigInteger[] EXP900;
    private static readonly int NUMBER_OF_SEQUENCE_CODEWORDS = 2;

    private DecodedBitStreamParser()
    {
    }

    public static string Decode2(int[] codewords)
    {
      StringBuilder result = new StringBuilder(100);
      int num1 = 1;
      int[] numArray1 = codewords;
      int index1 = num1;
      int codeIndex1 = index1 + 1;
      int mode = numArray1[index1];
      int[] array = ((IEnumerable<int>) codewords).ToArray<int>();
      while (codeIndex1 < codewords[0])
      {
        int num2;
        switch (mode)
        {
          case 900:
            num2 = DecodedBitStreamParser.TextCompaction(array, codeIndex1, result);
            break;
          case 901:
            num2 = DecodedBitStreamParser.ByteCompaction(mode, array, codeIndex1, result);
            break;
          case 902:
            num2 = DecodedBitStreamParser.NumericCompaction(array, codeIndex1, result);
            break;
          case 913:
            num2 = DecodedBitStreamParser.ByteCompaction(mode, array, codeIndex1, result);
            break;
          case 924:
            num2 = DecodedBitStreamParser.ByteCompaction(mode, array, codeIndex1, result);
            break;
          default:
            int codeIndex2 = codeIndex1 - 1;
            num2 = DecodedBitStreamParser.TextCompaction(array, codeIndex2, result);
            break;
        }
        if (num2 >= codewords.Length)
          throw MessagingToolkit.Barcode.FormatException.Instance;
        int[] numArray2 = codewords;
        int index2 = num2;
        codeIndex1 = index2 + 1;
        mode = numArray2[index2];
      }
      return result.ToString();
    }

    internal static DecoderResult Decode(
      int[] codewords,
      string ecLevel,
      int errorCorrectionCount)
    {
      StringBuilder result = new StringBuilder(codewords.Length * 2);
      int num1 = 1;
      int[] numArray1 = codewords;
      int index1 = num1;
      int codeIndex1 = index1 + 1;
      int mode = numArray1[index1];
      MacroPdf417Block macroPdf417Block = (MacroPdf417Block) null;
      while (codeIndex1 < codewords[0])
      {
        int num2;
        switch (mode)
        {
          case 900:
            num2 = DecodedBitStreamParser.TextCompaction(codewords, codeIndex1, result);
            break;
          case 901:
            num2 = DecodedBitStreamParser.ByteCompaction(mode, codewords, codeIndex1, result);
            break;
          case 902:
            num2 = DecodedBitStreamParser.NumericCompaction(codewords, codeIndex1, result);
            break;
          case 913:
            num2 = DecodedBitStreamParser.ByteCompaction(mode, codewords, codeIndex1, result);
            break;
          case 924:
            num2 = DecodedBitStreamParser.ByteCompaction(mode, codewords, codeIndex1, result);
            break;
          case 928:
            macroPdf417Block = new MacroPdf417Block();
            num2 = DecodedBitStreamParser.DecodeMacroBlock(codewords, codeIndex1, macroPdf417Block);
            break;
          default:
            int codeIndex2 = codeIndex1 - 1;
            num2 = DecodedBitStreamParser.TextCompaction(codewords, codeIndex2, result);
            break;
        }
        if (num2 >= codewords.Length)
          throw MessagingToolkit.Barcode.FormatException.Instance;
        int[] numArray2 = codewords;
        int index2 = num2;
        codeIndex1 = index2 + 1;
        mode = numArray2[index2];
      }
      if (result.Length == 0)
        throw MessagingToolkit.Barcode.FormatException.Instance;
      return (DecoderResult) new PdfDecoderResult((byte[]) null, result.ToString(), (List<byte[]>) null, ecLevel, errorCorrectionCount, macroPdf417Block);
    }

    private static int DecodeMacroBlock(
      int[] codewords,
      int codeIndex,
      MacroPdf417Block macroPdf417Block)
    {
      bool flag = false;
      if (codeIndex + DecodedBitStreamParser.NUMBER_OF_SEQUENCE_CODEWORDS > codewords[0])
        throw MessagingToolkit.Barcode.FormatException.Instance;
      int[] codewords1 = new int[DecodedBitStreamParser.NUMBER_OF_SEQUENCE_CODEWORDS];
      int index1 = 0;
      while (index1 < DecodedBitStreamParser.NUMBER_OF_SEQUENCE_CODEWORDS)
      {
        codewords1[index1] = codewords[codeIndex];
        ++index1;
        ++codeIndex;
      }
      macroPdf417Block.SegmentIndex = Convert.ToInt32(DecodedBitStreamParser.DecodeBase900toBase10(codewords1, DecodedBitStreamParser.NUMBER_OF_SEQUENCE_CODEWORDS));
      StringBuilder result = new StringBuilder();
      codeIndex = DecodedBitStreamParser.TextCompaction(codewords, codeIndex, result);
      macroPdf417Block.FileId = result.ToString();
      if (923 == codewords[codeIndex])
      {
        ++codeIndex;
        int[] numArray1 = new int[codewords[0] - codeIndex];
        int num = 0;
        while (codeIndex < codewords[0] && !flag)
        {
          int codeword = codewords[codeIndex++];
          if (codeword < 900)
          {
            numArray1[num++] = codeword;
          }
          else
          {
            if (codeword != 922)
              throw MessagingToolkit.Barcode.FormatException.Instance;
            ++codeIndex;
            flag = true;
          }
        }
        int length = num;
        if (length > numArray1.Length)
          length = numArray1.Length;
        int[] numArray2 = new int[length];
        for (int index2 = 0; index2 < length; ++index2)
          numArray2[index2] = numArray1[index2];
        macroPdf417Block.OptionalData = numArray2;
      }
      else if (922 == codewords[codeIndex])
        ++codeIndex;
      return codeIndex;
    }

    private static int TextCompaction(int[] codewords, int codeIndex, StringBuilder result)
    {
      int[] textCompactionData = new int[codewords[0] - codeIndex << 1];
      int[] byteCompactionData = new int[codewords[0] - codeIndex << 1];
      int length = 0;
      bool flag = false;
      while (codeIndex < codewords[0] && !flag)
      {
        int codeword1 = codewords[codeIndex++];
        if (codeword1 < 900)
        {
          textCompactionData[length] = codeword1 / 30;
          textCompactionData[length + 1] = codeword1 % 30;
          length += 2;
        }
        else
        {
          switch (codeword1)
          {
            case 900:
              textCompactionData[length++] = 900;
              continue;
            case 901:
              --codeIndex;
              flag = true;
              continue;
            case 902:
              --codeIndex;
              flag = true;
              continue;
            case 913:
              textCompactionData[length] = 913;
              int codeword2 = codewords[codeIndex++];
              byteCompactionData[length] = codeword2;
              ++length;
              continue;
            case 922:
              --codeIndex;
              flag = true;
              continue;
            case 923:
              --codeIndex;
              flag = true;
              continue;
            case 924:
              --codeIndex;
              flag = true;
              continue;
            case 928:
              --codeIndex;
              flag = true;
              continue;
            default:
              continue;
          }
        }
      }
      DecodedBitStreamParser.DecodeTextCompaction(textCompactionData, byteCompactionData, length, result);
      return codeIndex;
    }

    private static void DecodeTextCompaction(
      int[] textCompactionData,
      int[] byteCompactionData,
      int length,
      StringBuilder result)
    {
      DecodedBitStreamParser.Mode mode1 = DecodedBitStreamParser.Mode.ALPHA;
      DecodedBitStreamParser.Mode mode2 = DecodedBitStreamParser.Mode.ALPHA;
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = textCompactionData[index1];
        char ch = char.MinValue;
        switch (mode1)
        {
          case DecodedBitStreamParser.Mode.ALPHA:
            if (index2 < 26)
            {
              ch = (char) (65 + index2);
              break;
            }
            switch (index2)
            {
              case 26:
                ch = ' ';
                break;
              case 27:
                mode1 = DecodedBitStreamParser.Mode.LOWER;
                break;
              case 28:
                mode1 = DecodedBitStreamParser.Mode.MIXED;
                break;
              case 29:
                mode2 = mode1;
                mode1 = DecodedBitStreamParser.Mode.PUNCT_SHIFT;
                break;
              case 900:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 913:
                result.Append((char) byteCompactionData[index1]);
                break;
            }
            break;
          case DecodedBitStreamParser.Mode.LOWER:
            if (index2 < 26)
            {
              ch = (char) (97 + index2);
              break;
            }
            switch (index2)
            {
              case 26:
                ch = ' ';
                break;
              case 27:
                mode2 = mode1;
                mode1 = DecodedBitStreamParser.Mode.ALPHA_SHIFT;
                break;
              case 28:
                mode1 = DecodedBitStreamParser.Mode.MIXED;
                break;
              case 29:
                mode2 = mode1;
                mode1 = DecodedBitStreamParser.Mode.PUNCT_SHIFT;
                break;
              case 900:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 913:
                result.Append((char) byteCompactionData[index1]);
                break;
            }
            break;
          case DecodedBitStreamParser.Mode.MIXED:
            if (index2 < 25)
            {
              ch = DecodedBitStreamParser.MIXED_CHARS[index2];
              break;
            }
            switch (index2)
            {
              case 25:
                mode1 = DecodedBitStreamParser.Mode.PUNCT;
                break;
              case 26:
                ch = ' ';
                break;
              case 27:
                mode1 = DecodedBitStreamParser.Mode.LOWER;
                break;
              case 28:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 29:
                mode2 = mode1;
                mode1 = DecodedBitStreamParser.Mode.PUNCT_SHIFT;
                break;
              case 900:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 913:
                result.Append((char) byteCompactionData[index1]);
                break;
            }
            break;
          case DecodedBitStreamParser.Mode.PUNCT:
            if (index2 < 29)
            {
              ch = DecodedBitStreamParser.PUNCT_CHARS[index2];
              break;
            }
            switch (index2)
            {
              case 29:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 900:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 913:
                result.Append((char) byteCompactionData[index1]);
                break;
            }
            break;
          case DecodedBitStreamParser.Mode.ALPHA_SHIFT:
            mode1 = mode2;
            if (index2 < 26)
            {
              ch = (char) (65 + index2);
              break;
            }
            switch (index2)
            {
              case 26:
                ch = ' ';
                break;
              case 900:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
            }
            break;
          case DecodedBitStreamParser.Mode.PUNCT_SHIFT:
            mode1 = mode2;
            if (index2 < 29)
            {
              ch = DecodedBitStreamParser.PUNCT_CHARS[index2];
              break;
            }
            switch (index2)
            {
              case 29:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 900:
                mode1 = DecodedBitStreamParser.Mode.ALPHA;
                break;
              case 913:
                result.Append((char) byteCompactionData[index1]);
                break;
            }
            break;
        }
        if (ch != char.MinValue)
          result.Append(ch);
      }
    }

    private static int ByteCompaction(
      int mode,
      int[] codewords,
      int codeIndex,
      StringBuilder result)
    {
      switch (mode)
      {
        case 901:
          int num1 = 0;
          long num2 = 0;
          char[] chArray1 = new char[6];
          int[] numArray = new int[6];
          bool flag1 = false;
          int codeword1 = codewords[codeIndex++];
          while (codeIndex < codewords[0] && !flag1)
          {
            numArray[num1++] = codeword1;
            num2 = 900L * num2 + (long) codeword1;
            codeword1 = codewords[codeIndex++];
            switch (codeword1)
            {
              case 900:
              case 901:
              case 902:
              case 922:
              case 923:
              case 924:
              case 928:
                --codeIndex;
                flag1 = true;
                continue;
              default:
                if (num1 % 5 == 0 && num1 > 0)
                {
                  for (int index = 0; index < 6; ++index)
                  {
                    chArray1[5 - index] = (char) ((ulong) num2 % 256UL);
                    num2 >>= 8;
                  }
                  result.Append(chArray1);
                  num1 = 0;
                  continue;
                }
                continue;
            }
          }
          if (codeIndex == codewords[0] && codeword1 < 900)
            numArray[num1++] = codeword1;
          for (int index = 0; index < num1; ++index)
            result.Append((char) numArray[index]);
          break;
        case 924:
          int num3 = 0;
          long num4 = 0;
          bool flag2 = false;
          while (codeIndex < codewords[0] && !flag2)
          {
            int codeword2 = codewords[codeIndex++];
            if (codeword2 < 900)
            {
              ++num3;
              num4 = 900L * num4 + (long) codeword2;
            }
            else if (codeword2 == 900 || codeword2 == 901 || codeword2 == 902 || codeword2 == 924 || codeword2 == 928 || codeword2 == 923 || codeword2 == 922)
            {
              --codeIndex;
              flag2 = true;
            }
            if (num3 % 5 == 0 && num3 > 0)
            {
              char[] chArray2 = new char[6];
              for (int index = 0; index < 6; ++index)
              {
                chArray2[5 - index] = (char) ((ulong) num4 & (ulong) byte.MaxValue);
                num4 >>= 8;
              }
              result.Append(chArray2);
              num3 = 0;
            }
          }
          break;
      }
      return codeIndex;
    }

    private static int NumericCompaction(int[] codewords, int codeIndex, StringBuilder result)
    {
      int count = 0;
      bool flag = false;
      int[] codewords1 = new int[15];
      while (codeIndex < codewords[0] && !flag)
      {
        int codeword = codewords[codeIndex++];
        if (codeIndex == codewords[0])
          flag = true;
        if (codeword < 900)
        {
          codewords1[count] = codeword;
          ++count;
        }
        else if (codeword == 900 || codeword == 901 || codeword == 924 || codeword == 928 || codeword == 923 || codeword == 922)
        {
          --codeIndex;
          flag = true;
        }
        if (count % 15 == 0 || codeword == 902 || flag)
        {
          string str = DecodedBitStreamParser.DecodeBase900toBase10(codewords1, count);
          result.Append(str);
          count = 0;
        }
      }
      return codeIndex;
    }

    private static string DecodeBase900toBase10(int[] codewords, int count)
    {
      BigInteger left = BigInteger.Zero;
      for (int index = 0; index < count; ++index)
        left = BigInteger.Add(left, BigInteger.Multiply(DecodedBitStreamParser.EXP900[count - index - 1], new BigInteger(codewords[index])));
      string str = left.ToString();
      return str[0] == '1' ? str.Substring(1) : throw MessagingToolkit.Barcode.FormatException.Instance;
    }

    static DecodedBitStreamParser()
    {
      DecodedBitStreamParser.EXP900 = new BigInteger[16];
      DecodedBitStreamParser.EXP900[0] = BigInteger.One;
      BigInteger right = new BigInteger(900);
      DecodedBitStreamParser.EXP900[1] = right;
      for (int index = 2; index < DecodedBitStreamParser.EXP900.Length; ++index)
        DecodedBitStreamParser.EXP900[index] = BigInteger.Multiply(DecodedBitStreamParser.EXP900[index - 1], right);
    }

    public enum Mode
    {
      ALPHA,
      LOWER,
      MIXED,
      PUNCT,
      ALPHA_SHIFT,
      PUNCT_SHIFT,
    }
  }
}
