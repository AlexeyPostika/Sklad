// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI013x0xDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal abstract class AI013x0xDecoder : AI01weightDecoder
  {
    private const int headerSize = 5;
    private const int weightSize = 15;

    internal AI013x0xDecoder(BitArray information)
      : base(information)
    {
    }

    public override string ParseInformation()
    {
      if (this.information.Size != 60)
        throw NotFoundException.Instance;
      StringBuilder buf = new StringBuilder();
      this.EncodeCompressedGtin(buf, 5);
      this.EncodeCompressedWeight(buf, 45, 15);
      return buf.ToString();
    }
  }
}
