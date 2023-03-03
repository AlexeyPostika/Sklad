// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI01392xDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class AI01392xDecoder : AI01decoder
  {
    private const int headerSize = 8;
    private const int lastDigitSize = 2;

    internal AI01392xDecoder(BitArray information)
      : base(information)
    {
    }

    public override string ParseInformation()
    {
      if (this.information.Size < 48)
        throw NotFoundException.Instance;
      StringBuilder buf = new StringBuilder();
      this.EncodeCompressedGtin(buf, 8);
      int valueFromBitArray = this.generalDecoder.ExtractNumericValueFromBitArray(48, 2);
      buf.Append("(392");
      buf.Append(valueFromBitArray);
      buf.Append(')');
      DecodedInformation decodedInformation = this.generalDecoder.DecodeGeneralPurposeField(50, (string) null);
      buf.Append(decodedInformation.NewString);
      return buf.ToString();
    }
  }
}
