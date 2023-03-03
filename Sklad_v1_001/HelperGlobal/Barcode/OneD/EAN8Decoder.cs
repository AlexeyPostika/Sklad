// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.EAN8Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class EAN8Decoder : UPCEANDecoder
  {
    private readonly int[] decodeMiddleCounters;

    public EAN8Decoder() => this.decodeMiddleCounters = new int[4];

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
      int rowOffset1 = startRange[1];
      for (int index = 0; index < 4 && rowOffset1 < size; ++index)
      {
        int num1 = UPCEANDecoder.DecodeDigit(row, decodeMiddleCounters, rowOffset1, UPCEANDecoder.LPatterns);
        result.Append((char) (48 + num1));
        foreach (int num2 in decodeMiddleCounters)
          rowOffset1 += num2;
      }
      int rowOffset2 = UPCEANDecoder.FindGuardPattern(row, rowOffset1, true, UPCEANDecoder.MiddlePattern)[1];
      for (int index = 0; index < 4 && rowOffset2 < size; ++index)
      {
        int num3 = UPCEANDecoder.DecodeDigit(row, decodeMiddleCounters, rowOffset2, UPCEANDecoder.LPatterns);
        result.Append((char) (48 + num3));
        foreach (int num4 in decodeMiddleCounters)
          rowOffset2 += num4;
      }
      return rowOffset2;
    }

    internal override BarcodeFormat BarcodeFormat => BarcodeFormat.EAN8;
  }
}
