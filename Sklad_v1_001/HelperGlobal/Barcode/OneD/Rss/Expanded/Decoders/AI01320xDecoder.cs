// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI01320xDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class AI01320xDecoder : AI013x0xDecoder
  {
    internal AI01320xDecoder(BitArray information)
      : base(information)
    {
    }

    protected internal override void AddWeightCode(StringBuilder buf, int weight)
    {
      if (weight < 10000)
        buf.Append("(3202)");
      else
        buf.Append("(3203)");
    }

    protected internal override int CheckWeight(int weight) => weight < 10000 ? weight : weight - 10000;
  }
}
