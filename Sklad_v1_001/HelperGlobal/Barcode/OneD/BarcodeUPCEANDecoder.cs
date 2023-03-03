// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.OneD.BarcodeUPCEANDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.OneD
{
  public sealed class BarcodeUPCEANDecoder : OneDDecoder
  {
    private readonly UPCEANDecoder[] decoders;

    public BarcodeUPCEANDecoder(Dictionary<DecodeOptions, object> decodingOptions)
    {
      List<BarcodeFormat> barcodeFormatList = decodingOptions == null ? (List<BarcodeFormat>) null : (List<BarcodeFormat>) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.PossibleFormats);
      List<UPCEANDecoder> upceanDecoderList = new List<UPCEANDecoder>();
      if (barcodeFormatList != null)
      {
        if (barcodeFormatList.Contains(BarcodeFormat.EAN13))
          upceanDecoderList.Add((UPCEANDecoder) new EAN13Decoder());
        else if (barcodeFormatList.Contains(BarcodeFormat.UPCA))
          upceanDecoderList.Add((UPCEANDecoder) new UPCADecoder());
        if (barcodeFormatList.Contains(BarcodeFormat.EAN8))
          upceanDecoderList.Add((UPCEANDecoder) new EAN8Decoder());
        if (barcodeFormatList.Contains(BarcodeFormat.UPCE))
          upceanDecoderList.Add((UPCEANDecoder) new UPCEDecoder());
      }
      if (upceanDecoderList.Count == 0)
      {
        upceanDecoderList.Add((UPCEANDecoder) new EAN13Decoder());
        upceanDecoderList.Add((UPCEANDecoder) new EAN8Decoder());
        upceanDecoderList.Add((UPCEANDecoder) new UPCEDecoder());
      }
      this.decoders = upceanDecoderList.ToArray();
    }

    public override Result DecodeRow(
      int rowNumber,
      BitArray row,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int[] startGuardPattern = UPCEANDecoder.FindStartGuardPattern(row);
      foreach (UPCEANDecoder decoder in this.decoders)
      {
        Result result1;
        try
        {
          result1 = decoder.DecodeRow(rowNumber, row, startGuardPattern, decodingOptions);
        }
        catch (BarcodeDecoderException ex)
        {
          continue;
        }
        bool flag1 = result1.BarcodeFormat == BarcodeFormat.EAN13 && result1.Text[0] == '0';
        List<BarcodeFormat> barcodeFormatList = decodingOptions == null ? (List<BarcodeFormat>) null : (List<BarcodeFormat>) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.PossibleFormats);
        bool flag2 = barcodeFormatList == null || barcodeFormatList.Contains(BarcodeFormat.UPCA);
        if (!flag1 || !flag2)
          return result1;
        Result result2 = new Result(result1.Text.Substring(1), result1.RawBytes, result1.ResultPoints, BarcodeFormat.UPCA);
        result2.PutAllMetadata(result1.ResultMetadata);
        return result2;
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
