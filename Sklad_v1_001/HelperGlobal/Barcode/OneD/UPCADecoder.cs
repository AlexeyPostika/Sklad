// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.UPCADecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;
using System.Text;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class UPCADecoder : UPCEANDecoder
  {
    private readonly UPCEANDecoder ean13Decoder;

    public UPCADecoder() => this.ean13Decoder = (UPCEANDecoder) new EAN13Decoder();

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      int[] startGuardRange,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      return UPCADecoder.MaybeReturnResult(this.ean13Decoder.DecodeRow(rowNumber, row, startGuardRange, decodingOptions));
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      return UPCADecoder.MaybeReturnResult(this.ean13Decoder.DecodeRow(rowNumber, row, decodingOptions));
    }

    public override Result Decode(BinaryBitmap image) => UPCADecoder.MaybeReturnResult(this.ean13Decoder.Decode(image));

    public override Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      return UPCADecoder.MaybeReturnResult(this.ean13Decoder.Decode(image, decodingOptions));
    }

    internal override BarcodeFormat BarcodeFormat => BarcodeFormat.UPCA;

    protected internal override int DecodeMiddle(
      BitArray row,
      int[] startRange,
      StringBuilder resultString)
    {
      return this.ean13Decoder.DecodeMiddle(row, startRange, resultString);
    }

    private static Result MaybeReturnResult(Result result)
    {
      string text = result.Text;
      if (text[0] == '0')
        return new Result(text.Substring(1), (byte[]) null, result.ResultPoints, BarcodeFormat.UPCA);
      throw FormatException.Instance;
    }
  }
}
