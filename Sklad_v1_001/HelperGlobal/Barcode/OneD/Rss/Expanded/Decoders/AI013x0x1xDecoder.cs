// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI013x0x1xDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class AI013x0x1xDecoder : AI01weightDecoder
  {
    private const int headerSize = 8;
    private const int weightSize = 20;
    private const int dateSize = 16;
    private readonly string dateCode;
    private readonly string firstAIdigits;

    internal AI013x0x1xDecoder(BitArray information, string firstAIdigits, string dateCode)
      : base(information)
    {
      this.dateCode = dateCode;
      this.firstAIdigits = firstAIdigits;
    }

    public override string ParseInformation()
    {
      if (this.information.Size != 84)
        throw NotFoundException.Instance;
      StringBuilder buf = new StringBuilder();
      this.EncodeCompressedGtin(buf, 8);
      this.EncodeCompressedWeight(buf, 48, 20);
      this.EncodeCompressedDate(buf, 68);
      return buf.ToString();
    }

    private void EncodeCompressedDate(StringBuilder buf, int currentPos)
    {
      int valueFromBitArray = this.generalDecoder.ExtractNumericValueFromBitArray(currentPos, 16);
      if (valueFromBitArray == 38400)
        return;
      buf.Append('(');
      buf.Append(this.dateCode);
      buf.Append(')');
      int num1 = valueFromBitArray % 32;
      int num2 = valueFromBitArray / 32;
      int num3 = num2 % 12 + 1;
      int num4 = num2 / 12;
      if (num4 / 10 == 0)
        buf.Append('0');
      buf.Append(num4);
      if (num3 / 10 == 0)
        buf.Append('0');
      buf.Append(num3);
      if (num1 / 10 == 0)
        buf.Append('0');
      buf.Append(num1);
    }

    protected internal override void AddWeightCode(StringBuilder buf, int weight)
    {
      int num = weight / 100000;
      buf.Append('(');
      buf.Append(this.firstAIdigits);
      buf.Append(num);
      buf.Append(')');
    }

    protected internal override int CheckWeight(int weight) => weight % 100000;
  }
}
