// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.BarcodeOneDDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.OneD.Rss;
using MessagingToolkit.Barcode.OneD.Rss.Expanded;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class BarcodeOneDDecoder : OneDDecoder
  {
    private readonly List<OneDDecoder> decoders;

    public BarcodeOneDDecoder(Dictionary<DecodeOptions, object> decodingOptions)
    {
      List<BarcodeFormat> barcodeFormatList = decodingOptions == null ? (List<BarcodeFormat>) null : (List<BarcodeFormat>) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.PossibleFormats);
      bool usingCheckDigit1 = false;
      object decodeOptionType1 = BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.AssumeCode39CheckDigit);
      if (decodeOptionType1 != null)
        usingCheckDigit1 = (bool) decodeOptionType1;
      bool usingCheckDigit2 = true;
      object decodeOptionType2 = BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.AssumeMsiCheckDigit);
      if (decodeOptionType2 != null)
        usingCheckDigit2 = (bool) decodeOptionType2;
      this.decoders = new List<OneDDecoder>();
      if (barcodeFormatList != null)
      {
        if (barcodeFormatList.Contains(BarcodeFormat.EAN13) || barcodeFormatList.Contains(BarcodeFormat.UPCA) || barcodeFormatList.Contains(BarcodeFormat.EAN8) || barcodeFormatList.Contains(BarcodeFormat.UPCE))
          this.decoders.Add((OneDDecoder) new BarcodeUPCEANDecoder(decodingOptions));
        if (barcodeFormatList.Contains(BarcodeFormat.MSIMod10))
          this.decoders.Add((OneDDecoder) new MsiDecoder(usingCheckDigit2));
        if (barcodeFormatList.Contains(BarcodeFormat.Code39))
          this.decoders.Add((OneDDecoder) new Code39Decoder(usingCheckDigit1));
        if (barcodeFormatList.Contains(BarcodeFormat.Code93))
          this.decoders.Add((OneDDecoder) new Code93Decoder());
        if (barcodeFormatList.Contains(BarcodeFormat.Code128))
          this.decoders.Add((OneDDecoder) new Code128Decoder());
        if (barcodeFormatList.Contains(BarcodeFormat.ITF14))
          this.decoders.Add((OneDDecoder) new ITFDecoder());
        if (barcodeFormatList.Contains(BarcodeFormat.Codabar))
          this.decoders.Add((OneDDecoder) new CodaBarDecoder());
        if (barcodeFormatList.Contains(BarcodeFormat.RSS14))
          this.decoders.Add((OneDDecoder) new Rss14Decoder());
        if (barcodeFormatList.Contains(BarcodeFormat.RSSExpanded))
          this.decoders.Add((OneDDecoder) new RssExpandedDecoder());
      }
      if (this.decoders.Count != 0)
        return;
      this.decoders.Add((OneDDecoder) new BarcodeUPCEANDecoder(decodingOptions));
      this.decoders.Add((OneDDecoder) new Code39Decoder(usingCheckDigit1));
      this.decoders.Add((OneDDecoder) new MsiDecoder(usingCheckDigit2));
      this.decoders.Add((OneDDecoder) new CodaBarDecoder());
      this.decoders.Add((OneDDecoder) new Code93Decoder());
      this.decoders.Add((OneDDecoder) new Code128Decoder());
      this.decoders.Add((OneDDecoder) new ITFDecoder());
      this.decoders.Add((OneDDecoder) new Rss14Decoder());
      this.decoders.Add((OneDDecoder) new RssExpandedDecoder());
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      foreach (OneDDecoder decoder in this.decoders)
      {
        try
        {
          return decoder.DecodeRow(rowNumber, row, decodingOptions);
        }
        catch (BarcodeDecoderException ex)
        {
        }
      }
      throw NotFoundException.Instance;
    }

    public override void Reset()
    {
      foreach (IDecoder decoder in this.decoders)
        decoder.Reset();
    }
  }
}
