// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AnyAIDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class AnyAIDecoder : AbstractExpandedDecoder
  {
    private const int HeaderSize = 5;

    internal AnyAIDecoder(BitArray information)
      : base(information)
    {
    }

    public override string ParseInformation() => this.generalDecoder.DecodeAllCodes(new StringBuilder(), 5);
  }
}
