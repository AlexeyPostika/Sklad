// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.EAN13Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class EAN13Decoder : UPCEANDecoder
  {
    internal static readonly int[] FirstDigitEncodings = new int[10]
    {
      0,
      11,
      13,
      14,
      19,
      25,
      28,
      21,
      22,
      26
    };
    private readonly int[] decodeMiddleCounters;

    public EAN13Decoder() => this.decodeMiddleCounters = new int[4];

    protected internal override int DecodeMiddle(
      BitArray row,
      int[] startRange,
      StringBuilder resultString)
    {
      int[] decodeMiddleCounters = this.decodeMiddleCounters;
      decodeMiddleCounters[0] = 0;
      decodeMiddleCounters[1] = 0;
      decodeMiddleCounters[2] = 0;
      decodeMiddleCounters[3] = 0;
      int size = row.GetSize();
      int rowOffset1 = startRange[1];
      int lgPatternFound = 0;
      for (int index = 0; index < 6 && rowOffset1 < size; ++index)
      {
        int num1 = UPCEANDecoder.DecodeDigit(row, decodeMiddleCounters, rowOffset1, UPCEANDecoder.LAndGPatterns);
        resultString.Append((char) (48 + num1 % 10));
        foreach (int num2 in decodeMiddleCounters)
          rowOffset1 += num2;
        if (num1 >= 10)
          lgPatternFound |= 1 << 5 - index;
      }
      EAN13Decoder.DetermineFirstDigit(resultString, lgPatternFound);
      int rowOffset2 = UPCEANDecoder.FindGuardPattern(row, rowOffset1, true, UPCEANDecoder.MiddlePattern)[1];
      for (int index = 0; index < 6 && rowOffset2 < size; ++index)
      {
        int num3 = UPCEANDecoder.DecodeDigit(row, decodeMiddleCounters, rowOffset2, UPCEANDecoder.LPatterns);
        resultString.Append((char) (48 + num3));
        foreach (int num4 in decodeMiddleCounters)
          rowOffset2 += num4;
      }
      return rowOffset2;
    }

    internal override BarcodeFormat BarcodeFormat => BarcodeFormat.EAN13;

    private static void DetermineFirstDigit(StringBuilder resultString, int lgPatternFound)
    {
      for (int index = 0; index < 10; ++index)
      {
        if (lgPatternFound == EAN13Decoder.FirstDigitEncodings[index])
        {
          resultString.Insert(0, new char[1]
          {
            (char) (48 + index)
          });
          return;
        }
      }
      throw NotFoundException.Instance;
    }
  }
}
