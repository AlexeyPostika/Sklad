// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI01decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal abstract class AI01decoder : AbstractExpandedDecoder
  {
    protected internal const int gtinSize = 40;

    internal AI01decoder(BitArray information)
      : base(information)
    {
    }

    protected internal void EncodeCompressedGtin(StringBuilder buf, int currentPos)
    {
      buf.Append("(01)");
      int length = buf.Length;
      buf.Append('9');
      this.EncodeCompressedGtinWithoutAI(buf, currentPos, length);
    }

    protected internal void EncodeCompressedGtinWithoutAI(
      StringBuilder buf,
      int currentPos,
      int initialBufferPosition)
    {
      for (int index = 0; index < 4; ++index)
      {
        int valueFromBitArray = this.generalDecoder.ExtractNumericValueFromBitArray(currentPos + 10 * index, 10);
        if (valueFromBitArray / 100 == 0)
          buf.Append('0');
        if (valueFromBitArray / 10 == 0)
          buf.Append('0');
        buf.Append(valueFromBitArray);
      }
      AI01decoder.AppendCheckDigit(buf, initialBufferPosition);
    }

    private static void AppendCheckDigit(StringBuilder buf, int currentPos)
    {
      int num1 = 0;
      for (int index = 0; index < 13; ++index)
      {
        int num2 = (int) buf[index + currentPos] - 48;
        num1 += (index & 1) == 0 ? 3 * num2 : num2;
      }
      int num3 = 10 - num1 % 10;
      if (num3 == 10)
        num3 = 0;
      buf.Append(num3);
    }
  }
}
