// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCEANDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public abstract class UPCEANDecoder : OneDDecoder
  {
    private const int MaxAvgVariance = 122;
    private const int MaxIndividualVariance = 179;
    internal static readonly int[] StartEndPattern = new int[3]
    {
      1,
      1,
      1
    };
    internal static readonly int[] MiddlePattern = new int[5]
    {
      1,
      1,
      1,
      1,
      1
    };
    internal static readonly int[][] LPatterns = new int[10][]
    {
      new int[4]{ 3, 2, 1, 1 },
      new int[4]{ 2, 2, 2, 1 },
      new int[4]{ 2, 1, 2, 2 },
      new int[4]{ 1, 4, 1, 1 },
      new int[4]{ 1, 1, 3, 2 },
      new int[4]{ 1, 2, 3, 1 },
      new int[4]{ 1, 1, 1, 4 },
      new int[4]{ 1, 3, 1, 2 },
      new int[4]{ 1, 2, 1, 3 },
      new int[4]{ 3, 1, 1, 2 }
    };
    internal static readonly int[][] LAndGPatterns = new int[20][];
    private readonly StringBuilder decodeRowStringBuffer;
    private readonly UPCEANExtensionSupport extensionReader;
    private readonly EANManufacturerOrgSupport eanManSupport;

    protected internal UPCEANDecoder()
    {
      this.decodeRowStringBuffer = new StringBuilder(20);
      this.extensionReader = new UPCEANExtensionSupport();
      this.eanManSupport = new EANManufacturerOrgSupport();
    }

    internal static int[] FindStartGuardPattern(BitArray row)
    {
      bool flag = false;
      int[] startGuardPattern = (int[]) null;
      int rowOffset = 0;
      int[] counters = new int[UPCEANDecoder.StartEndPattern.Length];
      while (!flag)
      {
        for (int index = 0; index < UPCEANDecoder.StartEndPattern.Length; ++index)
          counters[index] = 0;
        startGuardPattern = UPCEANDecoder.FindGuardPattern(row, rowOffset, false, UPCEANDecoder.StartEndPattern, counters);
        int end = startGuardPattern[0];
        rowOffset = startGuardPattern[1];
        int start = end - (rowOffset - end);
        if (start >= 0)
          flag = row.IsRange(start, end, false);
      }
      return startGuardPattern;
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      return this.DecodeRow(rowNumber, row, UPCEANDecoder.FindStartGuardPattern(row), decodingOptions);
    }

    public virtual Result DecodeRow(
      int rowNumber,
      BitArray row,
      int[] startGuardRange,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      ResultPointCallback resultPointCallback = decodingOptions == null ? (ResultPointCallback) null : (ResultPointCallback) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.NeedResultPointCallback);
      resultPointCallback?.FoundPossibleResultPoint(new ResultPoint((float) (startGuardRange[0] + startGuardRange[1]) / 2f, (float) rowNumber));
      StringBuilder decodeRowStringBuffer = this.decodeRowStringBuffer;
      decodeRowStringBuffer.Length = 0;
      int num = this.DecodeMiddle(row, startGuardRange, decodeRowStringBuffer);
      resultPointCallback?.FoundPossibleResultPoint(new ResultPoint((float) num, (float) rowNumber));
      int[] numArray = this.DecodeEnd(row, num);
      resultPointCallback?.FoundPossibleResultPoint(new ResultPoint((float) (numArray[0] + numArray[1]) / 2f, (float) rowNumber));
      int start = numArray[1];
      int end = start + (start - numArray[0]);
      if (end >= row.GetSize() || !row.IsRange(start, end, false))
        throw NotFoundException.Instance;
      string str1 = decodeRowStringBuffer.ToString();
      if (!this.CheckChecksum(str1))
        throw ChecksumException.Instance;
      float x1 = (float) (startGuardRange[1] + startGuardRange[0]) / 2f;
      float x2 = (float) (numArray[1] + numArray[0]) / 2f;
      BarcodeFormat barcodeFormat = this.BarcodeFormat;
      Result result1 = new Result(str1, (byte[]) null, new ResultPoint[2]
      {
        new ResultPoint(x1, (float) rowNumber),
        new ResultPoint(x2, (float) rowNumber)
      }, barcodeFormat);
      try
      {
        Result result2 = this.extensionReader.DecodeRow(rowNumber, row, numArray[1]);
        result1.PutMetadata(ResultMetadataType.UPCEANExtension, (object) result2.Text);
        result1.PutAllMetadata(result2.ResultMetadata);
        result1.AddResultPoints(result2.ResultPoints);
      }
      catch (BarcodeDecoderException ex)
      {
      }
      if (barcodeFormat == BarcodeFormat.EAN13 || barcodeFormat == BarcodeFormat.UPCA)
      {
        string str2 = this.eanManSupport.LookupCountryIdentifier(str1);
        if (str2 != null)
          result1.PutMetadata(ResultMetadataType.PossibleCountry, (object) str2);
      }
      return result1;
    }

    internal virtual bool CheckChecksum(string s) => UPCEANDecoder.CheckStandardUPCEANChecksum(s);

    internal static bool CheckStandardUPCEANChecksum(string s)
    {
      int length = s.Length;
      if (length == 0)
        return false;
      int num1 = 0;
      for (int index = length - 2; index >= 0; index -= 2)
      {
        int num2 = (int) s[index] - 48;
        if (num2 < 0 || num2 > 9)
          throw MessagingToolkit.Barcode.FormatException.Instance;
        num1 += num2;
      }
      int num3 = num1 * 3;
      for (int index = length - 1; index >= 0; index -= 2)
      {
        int num4 = (int) s[index] - 48;
        if (num4 < 0 || num4 > 9)
          throw MessagingToolkit.Barcode.FormatException.Instance;
        num3 += num4;
      }
      return num3 % 10 == 0;
    }

    internal virtual int[] DecodeEnd(BitArray row, int endStart) => UPCEANDecoder.FindGuardPattern(row, endStart, false, UPCEANDecoder.StartEndPattern);

    internal static int[] FindGuardPattern(
      BitArray row,
      int rowOffset,
      bool whiteFirst,
      int[] pattern)
    {
      return UPCEANDecoder.FindGuardPattern(row, rowOffset, whiteFirst, pattern, new int[pattern.Length]);
    }

    private static int[] FindGuardPattern(
      BitArray row,
      int rowOffset,
      bool whiteFirst,
      int[] pattern,
      int[] counters)
    {
      int length = pattern.Length;
      int size = row.GetSize();
      bool flag = whiteFirst;
      rowOffset = whiteFirst ? row.GetNextUnset(rowOffset) : row.GetNextSet(rowOffset);
      int index = 0;
      int num = rowOffset;
      for (int i = rowOffset; i < size; ++i)
      {
        if (row.Get(i) ^ flag)
        {
          ++counters[index];
        }
        else
        {
          if (index == length - 1)
          {
            if (OneDDecoder.PatternMatchVariance(counters, pattern, 179) < 122)
              return new int[2]{ num, i };
            num += counters[0] + counters[1];
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

    internal static int DecodeDigit(BitArray row, int[] counters, int rowOffset, int[][] patterns)
    {
      OneDDecoder.RecordPattern(row, rowOffset, counters);
      int num1 = 122;
      int num2 = -1;
      int length = patterns.Length;
      for (int index = 0; index < length; ++index)
      {
        int[] pattern = patterns[index];
        int num3 = OneDDecoder.PatternMatchVariance(counters, pattern, 179);
        if (num3 < num1)
        {
          num1 = num3;
          num2 = index;
        }
      }
      return num2 >= 0 ? num2 : throw NotFoundException.Instance;
    }

    internal abstract BarcodeFormat BarcodeFormat { get; }

    protected internal abstract int DecodeMiddle(
      BitArray row,
      int[] startRange,
      StringBuilder resultString);

    static UPCEANDecoder()
    {
      Array.Copy((Array) UPCEANDecoder.LPatterns, 0, (Array) UPCEANDecoder.LAndGPatterns, 0, 10);
      for (int index1 = 10; index1 < 20; ++index1)
      {
        int[] lpattern = UPCEANDecoder.LPatterns[index1 - 10];
        int[] numArray = new int[lpattern.Length];
        for (int index2 = 0; index2 < lpattern.Length; ++index2)
          numArray[index2] = lpattern[lpattern.Length - index2 - 1];
        UPCEANDecoder.LAndGPatterns[index1] = numArray;
      }
    }
  }
}
