// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI01393xDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class AI01393xDecoder : AI01decoder
  {
    private const int headerSize = 8;
    private const int lastDigitSize = 2;
    private const int firstThreeDigitsSize = 10;

    internal AI01393xDecoder(BitArray information)
      : base(information)
    {
    }

    public override string ParseInformation()
    {
      if (this.information.Size < 48)
        throw NotFoundException.Instance;
      StringBuilder buf = new StringBuilder();
      this.EncodeCompressedGtin(buf, 8);
      int valueFromBitArray1 = this.generalDecoder.ExtractNumericValueFromBitArray(48, 2);
      buf.Append("(393");
      buf.Append(valueFromBitArray1);
      buf.Append(')');
      int valueFromBitArray2 = this.generalDecoder.ExtractNumericValueFromBitArray(50, 10);
      if (valueFromBitArray2 / 100 == 0)
        buf.Append('0');
      if (valueFromBitArray2 / 10 == 0)
        buf.Append('0');
      buf.Append(valueFromBitArray2);
      DecodedInformation decodedInformation = this.generalDecoder.DecodeGeneralPurposeField(60, (string) null);
      buf.Append(decodedInformation.NewString);
      return buf.ToString();
    }
  }
}
