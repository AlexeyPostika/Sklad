// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.CodaBarDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class CodaBarDecoder : OneDDecoder
  {
    private const int MAX_ACCEPTABLE = 512;
    private const int PADDING = 384;
    private const string ALPHABET_STRING = "0123456789-$:/.+ABCD";
    private const int MIN_CHARACTER_LENGTH = 3;
    internal static readonly char[] ALPHABET = "0123456789-$:/.+ABCD".ToCharArray();
    internal static readonly int[] CHARACTER_ENCODINGS = new int[20]
    {
      3,
      6,
      9,
      96,
      18,
      66,
      33,
      36,
      48,
      72,
      12,
      24,
      69,
      81,
      84,
      21,
      26,
      41,
      11,
      14
    };
    private static readonly char[] STARTEND_ENCODING = new char[4]
    {
      'A',
      'B',
      'C',
      'D'
    };
    private readonly StringBuilder decodeRowResult;
    private int[] counters;
    private int counterLength;

    public CodaBarDecoder()
    {
      this.decodeRowResult = new StringBuilder(20);
      this.counters = new int[80];
      this.counterLength = 0;
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      for (int index = 0; index < this.counters.Length; ++index)
        this.counters[index] = 0;
      this.SetCounters(row);
      int startPattern = this.FindStartPattern();
      int position = startPattern;
      this.decodeRowResult.Length = 0;
      int narrowWidePattern;
      do
      {
        narrowWidePattern = this.ToNarrowWidePattern(position);
        if (narrowWidePattern == -1)
          throw NotFoundException.Instance;
        this.decodeRowResult.Append((char) narrowWidePattern);
        position += 8;
      }
      while ((this.decodeRowResult.Length <= 1 || !CodaBarDecoder.ArrayContains(CodaBarDecoder.STARTEND_ENCODING, CodaBarDecoder.ALPHABET[narrowWidePattern])) && position < this.counterLength);
      int counter = this.counters[position - 1];
      int num1 = 0;
      for (int index = -8; index < -1; ++index)
        num1 += this.counters[position + index];
      if (position < this.counterLength && counter < num1 / 2)
        throw NotFoundException.Instance;
      this.ValidatePattern(startPattern);
      for (int index = 0; index < this.decodeRowResult.Length; ++index)
        this.decodeRowResult[index] = CodaBarDecoder.ALPHABET[(int) this.decodeRowResult[index]];
      char key1 = this.decodeRowResult[0];
      if (!CodaBarDecoder.ArrayContains(CodaBarDecoder.STARTEND_ENCODING, key1))
        throw NotFoundException.Instance;
      char key2 = this.decodeRowResult[this.decodeRowResult.Length - 1];
      if (!CodaBarDecoder.ArrayContains(CodaBarDecoder.STARTEND_ENCODING, key2))
        throw NotFoundException.Instance;
      if (this.decodeRowResult.Length <= 3)
        throw NotFoundException.Instance;
      this.decodeRowResult.Remove(this.decodeRowResult.Length - 1, 1);
      this.decodeRowResult.Remove(0, 1);
      int num2 = 0;
      for (int index = 0; index < startPattern; ++index)
        num2 += this.counters[index];
      float x1 = (float) num2;
      for (int index = startPattern; index < position - 1; ++index)
        num2 += this.counters[index];
      float x2 = (float) num2;
      ResultPointCallback resultPointCallback = decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.NeedResultPointCallback) ? (ResultPointCallback) null : (ResultPointCallback) decodingOptions[DecodeOptions.NeedResultPointCallback];
      if (resultPointCallback != null)
      {
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x1, (float) rowNumber));
        resultPointCallback.FoundPossibleResultPoint(new ResultPoint(x2, (float) rowNumber));
      }
      return new Result(this.decodeRowResult.ToString(), (byte[]) null, new ResultPoint[2]
      {
        new ResultPoint(x1, (float) rowNumber),
        new ResultPoint(x2, (float) rowNumber)
      }, BarcodeFormat.Codabar);
    }

    internal void ValidatePattern(int start)
    {
      int[] numArray1 = new int[4];
      int[] numArray2 = new int[4];
      int num1 = this.decodeRowResult.Length - 1;
      int num2 = start;
      int index1 = 0;
      while (true)
      {
        int num3 = CodaBarDecoder.CHARACTER_ENCODINGS[(int) this.decodeRowResult[index1]];
        for (int index2 = 6; index2 >= 0; --index2)
        {
          int index3 = (index2 & 1) + (num3 & 1) * 2;
          numArray1[index3] += this.counters[num2 + index2];
          ++numArray2[index3];
          num3 >>= 1;
        }
        if (index1 < num1)
        {
          num2 += 8;
          ++index1;
        }
        else
          break;
      }
      int[] numArray3 = new int[4];
      int[] numArray4 = new int[4];
      for (int index4 = 0; index4 < 2; ++index4)
      {
        numArray4[index4] = 0;
        numArray4[index4 + 2] = (numArray1[index4] << 8) / numArray2[index4] + (numArray1[index4 + 2] << 8) / numArray2[index4 + 2] >> 1;
        numArray3[index4] = numArray4[index4 + 2];
        numArray3[index4 + 2] = (numArray1[index4 + 2] * 512 + 384) / numArray2[index4 + 2];
      }
      int num4 = start;
      int index5 = 0;
      while (true)
      {
        int num5 = CodaBarDecoder.CHARACTER_ENCODINGS[(int) this.decodeRowResult[index5]];
        for (int index6 = 6; index6 >= 0; --index6)
        {
          int index7 = (index6 & 1) + (num5 & 1) * 2;
          int num6 = this.counters[num4 + index6] << 8;
          if (num6 < numArray4[index7] || num6 > numArray3[index7])
            throw NotFoundException.Instance;
          num5 >>= 1;
        }
        if (index5 < num1)
        {
          num4 += 8;
          ++index5;
        }
        else
          break;
      }
    }

    private void SetCounters(BitArray row)
    {
      this.counterLength = 0;
      int nextUnset = row.GetNextUnset(0);
      int size = row.GetSize();
      if (nextUnset >= size)
        throw NotFoundException.Instance;
      bool flag = true;
      int e = 0;
      for (; nextUnset < size; ++nextUnset)
      {
        if (row.Get(nextUnset) ^ flag)
        {
          ++e;
        }
        else
        {
          this.CounterAppend(e);
          e = 1;
          flag = !flag;
        }
      }
      this.CounterAppend(e);
    }

    private void CounterAppend(int e)
    {
      this.counters[this.counterLength] = e;
      ++this.counterLength;
      if (this.counterLength < this.counters.Length)
        return;
      int[] destinationArray = new int[this.counterLength * 2];
      Array.Copy((Array) this.counters, 0, (Array) destinationArray, 0, this.counterLength);
      this.counters = destinationArray;
    }

    private int FindStartPattern()
    {
      for (int position = 1; position < this.counterLength; position += 2)
      {
        int narrowWidePattern = this.ToNarrowWidePattern(position);
        if (narrowWidePattern != -1 && CodaBarDecoder.ArrayContains(CodaBarDecoder.STARTEND_ENCODING, CodaBarDecoder.ALPHABET[narrowWidePattern]))
        {
          int num = 0;
          for (int index = position; index < position + 7; ++index)
            num += this.counters[index];
          if (position == 1 || this.counters[position - 1] >= num / 2)
            return position;
        }
      }
      throw NotFoundException.Instance;
    }

    internal static bool ArrayContains(char[] array, char key)
    {
      if (array != null)
      {
        foreach (int num in array)
        {
          if (num == (int) key)
            return true;
        }
      }
      return false;
    }

    private int ToNarrowWidePattern(int position)
    {
      int num1 = position + 7;
      if (num1 >= this.counterLength)
        return -1;
      int[] counters = this.counters;
      int num2 = 0;
      int num3 = int.MaxValue;
      for (int index = position; index < num1; index += 2)
      {
        int num4 = counters[index];
        if (num4 < num3)
          num3 = num4;
        if (num4 > num2)
          num2 = num4;
      }
      int num5 = (num3 + num2) / 2;
      int num6 = 0;
      int num7 = int.MaxValue;
      for (int index = position + 1; index < num1; index += 2)
      {
        int num8 = counters[index];
        if (num8 < num7)
          num7 = num8;
        if (num8 > num6)
          num6 = num8;
      }
      int num9 = (num7 + num6) / 2;
      int num10 = 128;
      int num11 = 0;
      for (int index = 0; index < 7; ++index)
      {
        int num12 = (index & 1) == 0 ? num5 : num9;
        num10 >>= 1;
        if (counters[position + index] > num12)
          num11 |= num10;
      }
      for (int narrowWidePattern = 0; narrowWidePattern < CodaBarDecoder.CHARACTER_ENCODINGS.Length; ++narrowWidePattern)
      {
        if (CodaBarDecoder.CHARACTER_ENCODINGS[narrowWidePattern] == num11)
          return narrowWidePattern;
      }
      return -1;
    }
  }
}
