// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Decoder.DecodedBitStreamParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.QRCode.Decoder
{
  internal sealed class DecodedBitStreamParser
  {
    private const int Gb2312Subset = 1;
    private static readonly char[] AlphaNumericChars = new char[45]
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
      ' ',
      '$',
      '%',
      '*',
      '+',
      '-',
      '.',
      '/',
      ':'
    };

    private DecodedBitStreamParser()
    {
    }

    internal static DecoderResult Decode(
      byte[] bytes,
      Version version,
      ErrorCorrectionLevel ecLevel,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      BitSource bits = new BitSource(bytes);
      StringBuilder result = new StringBuilder(50);
      List<byte[]> byteSegments = new List<byte[]>(1);
      try
      {
        CharacterSetECI currentCharacterSetECI = (CharacterSetECI) null;
        bool fc1InEffect = false;
        Mode mode;
        do
        {
          mode = bits.Available() >= 4 ? Mode.ForBits(bits.ReadBits(4)) : Mode.Terminator;
          if (mode != Mode.Terminator)
          {
            if (mode == Mode.Fnc1FirstPosition || mode == Mode.Fnc1SecondPosition)
              fc1InEffect = true;
            else if (mode == Mode.StructuredAppend)
            {
              if (bits.Available() < 16)
                throw MessagingToolkit.Barcode.FormatException.Instance;
              bits.ReadBits(16);
            }
            else if (mode == Mode.Eci)
            {
              currentCharacterSetECI = CharacterSetECI.GetCharacterSetECIByValue(DecodedBitStreamParser.ParseECIValue(bits));
              if (currentCharacterSetECI == null)
                throw MessagingToolkit.Barcode.FormatException.Instance;
            }
            else if (mode == Mode.Hanzi)
            {
              int num = bits.ReadBits(4);
              int count = bits.ReadBits(mode.GetCharacterCountBits(version));
              if (num == 1)
                DecodedBitStreamParser.DecodeHanziSegment(bits, result, count);
            }
            else
            {
              int count = bits.ReadBits(mode.GetCharacterCountBits(version));
              if (mode == Mode.Numeric)
                DecodedBitStreamParser.DecodeNumericSegment(bits, result, count);
              else if (mode == Mode.Alphanumeric)
                DecodedBitStreamParser.DecodeAlphanumericSegment(bits, result, count, fc1InEffect);
              else if (mode == Mode.Byte)
              {
                DecodedBitStreamParser.DecodeByteSegment(bits, result, count, currentCharacterSetECI, byteSegments, decodingOptions);
              }
              else
              {
                if (mode != Mode.Kanji)
                  throw MessagingToolkit.Barcode.FormatException.Instance;
                DecodedBitStreamParser.DecodeKanjiSegment(bits, result, count);
              }
            }
          }
        }
        while (mode != Mode.Terminator);
      }
      catch (ArgumentException ex)
      {
        throw MessagingToolkit.Barcode.FormatException.Instance;
      }
      return new DecoderResult(bytes, result.ToString(), byteSegments.Count == 0 ? (List<byte[]>) null : byteSegments, ecLevel == null ? (string) null : ecLevel.ToString());
    }

    private static void DecodeHanziSegment(BitSource bits, StringBuilder result, int count)
    {
      byte[] bytes = count * 13 <= bits.Available() ? new byte[2 * count] : throw MessagingToolkit.Barcode.FormatException.Instance;
      int index = 0;
      for (; count > 0; --count)
      {
        int num1 = bits.ReadBits(13);
        int num2 = num1 / 96 << 8 | num1 % 96;
        int num3 = num2 >= 959 ? num2 + 42657 : num2 + 41377;
        bytes[index] = (byte) (num3 >> 8 & (int) byte.MaxValue);
        bytes[index + 1] = (byte) (num3 & (int) byte.MaxValue);
        index += 2;
      }
      try
      {
        result.Append(Encoding.GetEncoding("GB2312").GetString(bytes, 0, bytes.Length));
      }
      catch (ArgumentException ex1)
      {
        try
        {
          result.Append(Encoding.GetEncoding("UTF-8").GetString(bytes, 0, bytes.Length));
        }
        catch (Exception ex2)
        {
          throw MessagingToolkit.Barcode.FormatException.Instance;
        }
      }
      catch (Exception ex)
      {
        throw MessagingToolkit.Barcode.FormatException.Instance;
      }
    }

    private static void DecodeKanjiSegment(BitSource bits, StringBuilder result, int count)
    {
      byte[] bytes = count * 13 <= bits.Available() ? new byte[2 * count] : throw MessagingToolkit.Barcode.FormatException.Instance;
      int index = 0;
      for (; count > 0; --count)
      {
        int num1 = bits.ReadBits(13);
        int num2 = num1 / 192 << 8 | num1 % 192;
        int num3 = num2 >= 7936 ? num2 + 49472 : num2 + 33088;
        bytes[index] = (byte) (num3 >> 8);
        bytes[index + 1] = (byte) num3;
        index += 2;
      }
      try
      {
        result.Append(Encoding.GetEncoding("SHIFT-JIS").GetString(bytes, 0, bytes.Length));
      }
      catch (ArgumentException ex1)
      {
        try
        {
          result.Append(Encoding.GetEncoding("UTF-8").GetString(bytes, 0, bytes.Length));
        }
        catch (Exception ex2)
        {
          throw MessagingToolkit.Barcode.FormatException.Instance;
        }
      }
      catch (Exception ex)
      {
        throw MessagingToolkit.Barcode.FormatException.Instance;
      }
    }

    private static void DecodeByteSegment(
      BitSource bits,
      StringBuilder result,
      int count,
      CharacterSetECI currentCharacterSetECI,
      List<byte[]> byteSegments,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      byte[] bytes = count << 3 <= bits.Available() ? new byte[count] : throw MessagingToolkit.Barcode.FormatException.Instance;
      for (int index = 0; index < count; ++index)
        bytes[index] = (byte) bits.ReadBits(8);
      string name = currentCharacterSetECI != null ? currentCharacterSetECI.EncodingName : StringHelper.GuessEncoding(bytes, decodingOptions);
      try
      {
        result.Append(Encoding.GetEncoding(name).GetString(bytes, 0, bytes.Length));
      }
      catch (ArgumentException ex1)
      {
        try
        {
          result.Append(Encoding.GetEncoding("UTF-8").GetString(bytes, 0, bytes.Length));
        }
        catch (Exception ex2)
        {
          throw MessagingToolkit.Barcode.FormatException.Instance;
        }
      }
      catch (Exception ex)
      {
        throw MessagingToolkit.Barcode.FormatException.Instance;
      }
      byteSegments.Add(bytes);
    }

    private static char ToAlphaNumericChar(int val)
    {
      if (val >= DecodedBitStreamParser.AlphaNumericChars.Length)
        throw MessagingToolkit.Barcode.FormatException.Instance;
      return DecodedBitStreamParser.AlphaNumericChars[val];
    }

    private static void DecodeAlphanumericSegment(
      BitSource bits,
      StringBuilder result,
      int count,
      bool fc1InEffect)
    {
      int length = result.Length;
      for (; count > 1; count -= 2)
      {
        int num = bits.Available() >= 11 ? bits.ReadBits(11) : throw MessagingToolkit.Barcode.FormatException.Instance;
        result.Append(DecodedBitStreamParser.ToAlphaNumericChar(num / 45));
        result.Append(DecodedBitStreamParser.ToAlphaNumericChar(num % 45));
      }
      if (count == 1)
      {
        if (bits.Available() < 6)
          throw MessagingToolkit.Barcode.FormatException.Instance;
        result.Append(DecodedBitStreamParser.ToAlphaNumericChar(bits.ReadBits(6)));
      }
      if (!fc1InEffect)
        return;
      for (int index = length; index < result.Length; ++index)
      {
        if (result[index] == '%')
        {
          if (index < result.Length - 1 && result[index + 1] == '%')
            result.Remove(index + 1, 1);
          else
            result[index] = '\u001D';
        }
      }
    }

    private static void DecodeNumericSegment(BitSource bits, StringBuilder result, int count)
    {
      for (; count >= 3; count -= 3)
      {
        int num = bits.Available() >= 10 ? bits.ReadBits(10) : throw MessagingToolkit.Barcode.FormatException.Instance;
        if (num >= 1000)
          throw MessagingToolkit.Barcode.FormatException.Instance;
        result.Append(DecodedBitStreamParser.ToAlphaNumericChar(num / 100));
        result.Append(DecodedBitStreamParser.ToAlphaNumericChar(num / 10 % 10));
        result.Append(DecodedBitStreamParser.ToAlphaNumericChar(num % 10));
      }
      switch (count)
      {
        case 1:
          int val = bits.Available() >= 4 ? bits.ReadBits(4) : throw MessagingToolkit.Barcode.FormatException.Instance;
          if (val >= 10)
            throw MessagingToolkit.Barcode.FormatException.Instance;
          result.Append(DecodedBitStreamParser.ToAlphaNumericChar(val));
          break;
        case 2:
          int num1 = bits.Available() >= 7 ? bits.ReadBits(7) : throw MessagingToolkit.Barcode.FormatException.Instance;
          if (num1 >= 100)
            throw MessagingToolkit.Barcode.FormatException.Instance;
          result.Append(DecodedBitStreamParser.ToAlphaNumericChar(num1 / 10));
          result.Append(DecodedBitStreamParser.ToAlphaNumericChar(num1 % 10));
          break;
      }
    }

    private static int ParseECIValue(BitSource bits)
    {
      int num1 = bits.ReadBits(8);
      if ((num1 & 128) == 0)
        return num1 & (int) sbyte.MaxValue;
      if ((num1 & 192) == 128)
      {
        int num2 = bits.ReadBits(8);
        return (num1 & 63) << 8 | num2;
      }
      if ((num1 & 224) != 192)
        throw MessagingToolkit.Barcode.FormatException.Instance;
      int num3 = bits.ReadBits(16);
      return (num1 & 31) << 16 | num3;
    }
  }
}
