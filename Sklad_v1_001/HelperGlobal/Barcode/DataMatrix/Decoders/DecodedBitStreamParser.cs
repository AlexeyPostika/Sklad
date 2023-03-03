// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Decoders.DecodedBitStreamParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Decoders
{
  internal sealed class DecodedBitStreamParser
  {
    private static readonly char[] C40_BASIC_SET_CHARS = new char[40]
    {
      '*',
      '*',
      '*',
      ' ',
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
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z'
    };
    private static readonly char[] C40_SHIFT2_SET_CHARS = new char[27]
    {
      '!',
      '"',
      '#',
      '$',
      '%',
      '&',
      '\'',
      '(',
      ')',
      '*',
      '+',
      ',',
      '-',
      '.',
      '/',
      ':',
      ';',
      '<',
      '=',
      '>',
      '?',
      '@',
      '[',
      '\\',
      ']',
      '^',
      '_'
    };
    private static readonly char[] TEXT_BASIC_SET_CHARS = new char[40]
    {
      '*',
      '*',
      '*',
      ' ',
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
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
      'g',
      'h',
      'i',
      'j',
      'k',
      'l',
      'm',
      'n',
      'o',
      'p',
      'q',
      'r',
      's',
      't',
      'u',
      'v',
      'w',
      'x',
      'y',
      'z'
    };
    private static readonly char[] TEXT_SHIFT3_SET_CHARS = new char[32]
    {
      '\'',
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'I',
      'J',
      'K',
      'L',
      'M',
      'N',
      'O',
      'P',
      'Q',
      'R',
      'S',
      'T',
      'U',
      'V',
      'W',
      'X',
      'Y',
      'Z',
      '{',
      '|',
      '}',
      '~',
      '\u007F'
    };

    private DecodedBitStreamParser()
    {
    }

    internal static DecoderResult Decode(byte[] bytes)
    {
      BitSource bits = new BitSource(bytes);
      StringBuilder result = new StringBuilder(100);
      StringBuilder resultTrailer = new StringBuilder(0);
      List<byte[]> byteSegments = new List<byte[]>(1);
      DecodedBitStreamParser.Mode mode = DecodedBitStreamParser.Mode.ASCII_ENCODE;
      do
      {
        switch (mode)
        {
          case DecodedBitStreamParser.Mode.ASCII_ENCODE:
            mode = DecodedBitStreamParser.DecodeAsciiSegment(bits, result, resultTrailer);
            goto label_10;
          case DecodedBitStreamParser.Mode.C40_ENCODE:
            DecodedBitStreamParser.DecodeC40Segment(bits, result);
            break;
          case DecodedBitStreamParser.Mode.TEXT_ENCODE:
            DecodedBitStreamParser.DecodeTextSegment(bits, result);
            break;
          case DecodedBitStreamParser.Mode.ANSIX12_ENCODE:
            DecodedBitStreamParser.DecodeAnsiX12Segment(bits, result);
            break;
          case DecodedBitStreamParser.Mode.EDIFACT_ENCODE:
            DecodedBitStreamParser.DecodeEdifactSegment(bits, result);
            break;
          case DecodedBitStreamParser.Mode.BASE256_ENCODE:
            DecodedBitStreamParser.DecodeBase256Segment(bits, result, (ICollection<byte[]>) byteSegments);
            break;
          default:
            throw MessagingToolkit.Barcode.FormatException.Instance;
        }
        mode = DecodedBitStreamParser.Mode.ASCII_ENCODE;
label_10:;
      }
      while (mode != DecodedBitStreamParser.Mode.PAD_ENCODE && bits.Available() > 0);
      if (resultTrailer.Length > 0)
        result.Append(resultTrailer.ToString());
      return new DecoderResult(bytes, result.ToString(), byteSegments.Count == 0 ? (List<byte[]>) null : byteSegments, (string) null);
    }

    private static DecodedBitStreamParser.Mode DecodeAsciiSegment(
      BitSource bits,
      StringBuilder result,
      StringBuilder resultTrailer)
    {
      bool flag = false;
      do
      {
        int num1 = bits.ReadBits(8);
        if (num1 == 0)
          throw MessagingToolkit.Barcode.FormatException.Instance;
        if (num1 <= 128)
        {
          if (flag)
            num1 += 128;
          result.Append((char) (num1 - 1));
          return DecodedBitStreamParser.Mode.ASCII_ENCODE;
        }
        if (num1 == 129)
          return DecodedBitStreamParser.Mode.PAD_ENCODE;
        if (num1 <= 229)
        {
          int num2 = num1 - 130;
          if (num2 < 10)
            result.Append('0');
          result.Append(num2);
        }
        else
        {
          if (num1 == 230)
            return DecodedBitStreamParser.Mode.C40_ENCODE;
          if (num1 == 231)
            return DecodedBitStreamParser.Mode.BASE256_ENCODE;
          if (num1 == 232)
            result.Append('\u001D');
          else if (num1 != 233 && num1 != 234)
          {
            switch (num1)
            {
              case 235:
                flag = true;
                break;
              case 236:
                result.Append("[)>\u001E05\u001D");
                resultTrailer.Insert(0, "\u001E\u0004");
                break;
              case 237:
                result.Append("[)>\u001E06\u001D");
                resultTrailer.Insert(0, "\u001E\u0004");
                break;
              case 238:
                return DecodedBitStreamParser.Mode.ANSIX12_ENCODE;
              case 239:
                return DecodedBitStreamParser.Mode.TEXT_ENCODE;
              case 240:
                return DecodedBitStreamParser.Mode.EDIFACT_ENCODE;
              default:
                if (num1 != 241 && num1 >= 242 && (num1 != 254 || bits.Available() != 0))
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                break;
            }
          }
        }
      }
      while (bits.Available() > 0);
      return DecodedBitStreamParser.Mode.ASCII_ENCODE;
    }

    private static void DecodeC40Segment(BitSource bits, StringBuilder result)
    {
      bool flag = false;
      int[] result1 = new int[3];
      int num = 0;
      while (bits.Available() != 8)
      {
        int firstByte = bits.ReadBits(8);
        if (firstByte == 254)
          break;
        DecodedBitStreamParser.ParseTwoBytes(firstByte, bits.ReadBits(8), result1);
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int index2 = result1[index1];
          switch (num)
          {
            case 0:
              if (index2 < 3)
              {
                num = index2 + 1;
                break;
              }
              if (index2 >= DecodedBitStreamParser.C40_BASIC_SET_CHARS.Length)
                throw MessagingToolkit.Barcode.FormatException.Instance;
              char c40BasicSetChar = DecodedBitStreamParser.C40_BASIC_SET_CHARS[index2];
              if (flag)
              {
                result.Append((char) ((uint) c40BasicSetChar + 128U));
                flag = false;
                break;
              }
              result.Append(c40BasicSetChar);
              break;
            case 1:
              if (flag)
              {
                result.Append((char) (index2 + 128));
                flag = false;
              }
              else
                result.Append((char) index2);
              num = 0;
              break;
            case 2:
              if (index2 < DecodedBitStreamParser.C40_SHIFT2_SET_CHARS.Length)
              {
                char c40ShifT2SetChar = DecodedBitStreamParser.C40_SHIFT2_SET_CHARS[index2];
                if (flag)
                {
                  result.Append((char) ((uint) c40ShifT2SetChar + 128U));
                  flag = false;
                }
                else
                  result.Append(c40ShifT2SetChar);
              }
              else if (index2 == 27)
              {
                result.Append('\u001D');
              }
              else
              {
                if (index2 != 30)
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                flag = true;
              }
              num = 0;
              break;
            case 3:
              if (flag)
              {
                result.Append((char) (index2 + 224));
                flag = false;
              }
              else
                result.Append((char) (index2 + 96));
              num = 0;
              break;
            default:
              throw MessagingToolkit.Barcode.FormatException.Instance;
          }
        }
        if (bits.Available() <= 0)
          break;
      }
    }

    private static void DecodeTextSegment(BitSource bits, StringBuilder result)
    {
      bool flag = false;
      int[] result1 = new int[3];
      int num = 0;
      while (bits.Available() != 8)
      {
        int firstByte = bits.ReadBits(8);
        if (firstByte == 254)
          break;
        DecodedBitStreamParser.ParseTwoBytes(firstByte, bits.ReadBits(8), result1);
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int index2 = result1[index1];
          switch (num)
          {
            case 0:
              if (index2 < 3)
              {
                num = index2 + 1;
                break;
              }
              if (index2 >= DecodedBitStreamParser.TEXT_BASIC_SET_CHARS.Length)
                throw MessagingToolkit.Barcode.FormatException.Instance;
              char textBasicSetChar = DecodedBitStreamParser.TEXT_BASIC_SET_CHARS[index2];
              if (flag)
              {
                result.Append((char) ((uint) textBasicSetChar + 128U));
                flag = false;
                break;
              }
              result.Append(textBasicSetChar);
              break;
            case 1:
              if (flag)
              {
                result.Append((char) (index2 + 128));
                flag = false;
              }
              else
                result.Append((char) index2);
              num = 0;
              break;
            case 2:
              if (index2 < DecodedBitStreamParser.C40_SHIFT2_SET_CHARS.Length)
              {
                char c40ShifT2SetChar = DecodedBitStreamParser.C40_SHIFT2_SET_CHARS[index2];
                if (flag)
                {
                  result.Append((char) ((uint) c40ShifT2SetChar + 128U));
                  flag = false;
                }
                else
                  result.Append(c40ShifT2SetChar);
              }
              else if (index2 == 27)
              {
                result.Append('\u001D');
              }
              else
              {
                if (index2 != 30)
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                flag = true;
              }
              num = 0;
              break;
            case 3:
              if (index2 >= DecodedBitStreamParser.TEXT_SHIFT3_SET_CHARS.Length)
                throw MessagingToolkit.Barcode.FormatException.Instance;
              char textShifT3SetChar = DecodedBitStreamParser.TEXT_SHIFT3_SET_CHARS[index2];
              if (flag)
              {
                result.Append((char) ((uint) textShifT3SetChar + 128U));
                flag = false;
              }
              else
                result.Append(textShifT3SetChar);
              num = 0;
              break;
            default:
              throw MessagingToolkit.Barcode.FormatException.Instance;
          }
        }
        if (bits.Available() <= 0)
          break;
      }
    }

    private static void DecodeAnsiX12Segment(BitSource bits, StringBuilder result)
    {
      int[] result1 = new int[3];
      while (bits.Available() != 8)
      {
        int firstByte = bits.ReadBits(8);
        if (firstByte == 254)
          break;
        DecodedBitStreamParser.ParseTwoBytes(firstByte, bits.ReadBits(8), result1);
        for (int index = 0; index < 3; ++index)
        {
          int num = result1[index];
          switch (num)
          {
            case 0:
              result.Append('\r');
              break;
            case 1:
              result.Append('*');
              break;
            case 2:
              result.Append('>');
              break;
            case 3:
              result.Append(' ');
              break;
            default:
              if (num < 14)
              {
                result.Append((char) (num + 44));
                break;
              }
              if (num >= 40)
                throw MessagingToolkit.Barcode.FormatException.Instance;
              result.Append((char) (num + 51));
              break;
          }
        }
        if (bits.Available() <= 0)
          break;
      }
    }

    private static void ParseTwoBytes(int firstByte, int secondByte, int[] result)
    {
      int num1 = (firstByte << 8) + secondByte - 1;
      int num2 = num1 / 1600;
      result[0] = num2;
      int num3 = num1 - num2 * 1600;
      int num4 = num3 / 40;
      result[1] = num4;
      result[2] = num3 - num4 * 40;
    }

    private static void DecodeEdifactSegment(BitSource bits, StringBuilder result)
    {
      while (bits.Available() > 16)
      {
        for (int index = 0; index < 4; ++index)
        {
          int num = bits.ReadBits(6);
          if (num == 31)
          {
            int numBits = 8 - bits.BitOffset;
            if (numBits == 8)
              return;
            bits.ReadBits(numBits);
            return;
          }
          if ((num & 32) == 0)
            num |= 64;
          result.Append((char) num);
        }
        if (bits.Available() <= 0)
          break;
      }
    }

    private static void DecodeBase256Segment(
      BitSource bits,
      StringBuilder result,
      ICollection<byte[]> byteSegments)
    {
      int num1 = 1 + bits.ByteOffset;
      int randomizedBase256Codeword = bits.ReadBits(8);
      int base256CodewordPosition = num1;
      int num2 = base256CodewordPosition + 1;
      int num3 = DecodedBitStreamParser.Unrandomize255State(randomizedBase256Codeword, base256CodewordPosition);
      int length = num3 != 0 ? (num3 >= 250 ? 250 * (num3 - 249) + DecodedBitStreamParser.Unrandomize255State(bits.ReadBits(8), num2++) : num3) : bits.Available() / 8;
      byte[] bytes = length >= 0 ? new byte[length] : throw MessagingToolkit.Barcode.FormatException.Instance;
      for (int index = 0; index < length; ++index)
        bytes[index] = bits.Available() >= 8 ? (byte) DecodedBitStreamParser.Unrandomize255State(bits.ReadBits(8), num2++) : throw MessagingToolkit.Barcode.FormatException.Instance;
      byteSegments.Add(bytes);
      try
      {
        result.Append(Encoding.GetEncoding("ISO-8859-1").GetString(bytes, 0, bytes.Length));
      }
      catch (IOException ex)
      {
        throw new InvalidOperationException("Platform does not support required encoding: " + (object) ex);
      }
    }

    private static int Unrandomize255State(
      int randomizedBase256Codeword,
      int base256CodewordPosition)
    {
      int num1 = 149 * base256CodewordPosition % (int) byte.MaxValue + 1;
      int num2 = randomizedBase256Codeword - num1;
      return num2 < 0 ? num2 + 256 : num2;
    }

    public enum Mode
    {
      PAD_ENCODE,
      ASCII_ENCODE,
      C40_ENCODE,
      TEXT_ENCODE,
      ANSIX12_ENCODE,
      EDIFACT_ENCODE,
      BASE256_ENCODE,
    }
  }
}
