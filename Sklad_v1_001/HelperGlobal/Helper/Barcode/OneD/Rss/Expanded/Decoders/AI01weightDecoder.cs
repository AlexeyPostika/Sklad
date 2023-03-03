// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI01weightDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal abstract class AI01weightDecoder : AI01decoder
  {
    internal AI01weightDecoder(BitArray information)
      : base(information)
    {
    }

    protected internal void EncodeCompressedWeight(
      StringBuilder buf,
      int currentPos,
      int weightSize)
    {
      int valueFromBitArray = this.generalDecoder.ExtractNumericValueFromBitArray(currentPos, weightSize);
      this.AddWeightCode(buf, valueFromBitArray);
      int num1 = this.CheckWeight(valueFromBitArray);
      int num2 = 100000;
      for (int index = 0; index < 5; ++index)
      {
        if (num1 / num2 == 0)
          buf.Append('0');
        num2 /= 10;
      }
      buf.Append(num1);
    }

    protected internal abstract void AddWeightCode(StringBuilder buf, int weight);

    protected internal abstract int CheckWeight(int weight);
  }
}
