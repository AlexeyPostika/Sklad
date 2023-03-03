// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCEDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class UPCEDecoder : UPCEANDecoder
  {
    private static readonly int[] MiddleEndPattern = new int[6]
    {
      1,
      1,
      1,
      1,
      1,
      1
    };
    private static readonly int[][] NumSysAndCheckDigitPatterns = new int[2][]
    {
      new int[10]{ 56, 52, 50, 49, 44, 38, 35, 42, 41, 37 },
      new int[10]{ 7, 11, 13, 14, 19, 25, 28, 21, 22, 26 }
    };
    private readonly int[] decodeMiddleCounters;

    public UPCEDecoder() => this.decodeMiddleCounters = new int[4];

    protected internal override int DecodeMiddle(
      BitArray row,
      int[] startRange,
      StringBuilder result)
    {
      int[] decodeMiddleCounters = this.decodeMiddleCounters;
      decodeMiddleCounters[0] = 0;
      decodeMiddleCounters[1] = 0;
      decodeMiddleCounters[2] = 0;
      decodeMiddleCounters[3] = 0;
      int size = row.GetSize();
      int rowOffset = startRange[1];
      int lgPatternFound = 0;
      for (int index1 = 0; index1 < 6 && rowOffset < size; ++index1)
      {
        int num = UPCEANDecoder.DecodeDigit(row, decodeMiddleCounters, rowOffset, UPCEANDecoder.LAndGPatterns);
        result.Append((char) (48 + num % 10));
        for (int index2 = 0; index2 < decodeMiddleCounters.Length; ++index2)
          rowOffset += decodeMiddleCounters[index2];
        if (num >= 10)
          lgPatternFound |= 1 << 5 - index1;
      }
      UPCEDecoder.DetermineNumSysAndCheckDigit(result, lgPatternFound);
      return rowOffset;
    }

    internal override int[] DecodeEnd(BitArray row, int endStart) => UPCEANDecoder.FindGuardPattern(row, endStart, true, UPCEDecoder.MiddleEndPattern);

    internal override bool CheckChecksum(string s) => base.CheckChecksum(UPCEDecoder.ConvertUPCEtoUPCA(s));

    private static void DetermineNumSysAndCheckDigit(StringBuilder resultString, int lgPatternFound)
    {
      for (int index1 = 0; index1 <= 1; ++index1)
      {
        for (int index2 = 0; index2 < 10; ++index2)
        {
          if (lgPatternFound == UPCEDecoder.NumSysAndCheckDigitPatterns[index1][index2])
          {
            resultString.Insert(0, new char[1]
            {
              (char) (48 + index1)
            });
            resultString.Append((char) (48 + index2));
            return;
          }
        }
      }
      throw NotFoundException.Instance;
    }

    internal override BarcodeFormat BarcodeFormat => BarcodeFormat.UPCE;

    public static string ConvertUPCEtoUPCA(string upce)
    {
      char[] destination = new char[6];
      upce.CopyTo(1, destination, 0, 6);
      StringBuilder stringBuilder = new StringBuilder(12);
      stringBuilder.Append(upce[0]);
      char ch = destination[5];
      switch (ch)
      {
        case '0':
        case '1':
        case '2':
          stringBuilder.Append(destination, 0, 2);
          stringBuilder.Append(ch);
          stringBuilder.Append("0000");
          stringBuilder.Append(destination, 2, 3);
          break;
        case '3':
          stringBuilder.Append(destination, 0, 3);
          stringBuilder.Append("00000");
          stringBuilder.Append(destination, 3, 2);
          break;
        case '4':
          stringBuilder.Append(destination, 0, 4);
          stringBuilder.Append("00000");
          stringBuilder.Append(destination[4]);
          break;
        default:
          stringBuilder.Append(destination, 0, 5);
          stringBuilder.Append("0000");
          stringBuilder.Append(ch);
          break;
      }
      stringBuilder.Append(upce[7]);
      return stringBuilder.ToString();
    }
  }
}
