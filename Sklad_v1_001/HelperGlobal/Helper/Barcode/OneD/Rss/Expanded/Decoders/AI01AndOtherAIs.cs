// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders.AI01AndOtherAIs
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Text;

namespace MessagingToolkit.Barcode.OneD.Rss.Expanded.Decoders
{
  internal sealed class AI01AndOtherAIs : AI01decoder
  {
    private const int HEADER_SIZE = 4;

    internal AI01AndOtherAIs(BitArray information)
      : base(information)
    {
    }

    public override string ParseInformation()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("(01)");
      int length = stringBuilder.Length;
      int valueFromBitArray = this.generalDecoder.ExtractNumericValueFromBitArray(4, 4);
      stringBuilder.Append(valueFromBitArray);
      this.EncodeCompressedGtinWithoutAI(stringBuilder, 8, length);
      return this.generalDecoder.DecodeAllCodes(stringBuilder, 48);
    }
  }
}
