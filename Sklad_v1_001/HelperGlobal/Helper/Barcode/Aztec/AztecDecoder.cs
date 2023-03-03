// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Aztec.AztecDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Aztec
{
  public sealed class AztecDecoder : IDecoder
  {
    public Result Decode(BinaryBitmap image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      AztecDetectorResult detectorResult = new MessagingToolkit.Barcode.Aztec.Detector.Detector(image.BlackMatrix).Detect();
      ResultPoint[] points = detectorResult.Points;
      if (decodingOptions != null)
      {
        ResultPointCallback resultPointCallback = decodingOptions == null ? (ResultPointCallback) null : (ResultPointCallback) BarcodeHelper.GetDecodeOptionType(decodingOptions, DecodeOptions.NeedResultPointCallback);
        if (resultPointCallback != null)
        {
          foreach (ResultPoint point in points)
            resultPointCallback.FoundPossibleResultPoint(point);
        }
      }
      DecoderResult decoderResult = new MessagingToolkit.Barcode.Aztec.Decoder.Decoder().Decode(detectorResult);
      Result result = new Result(decoderResult.Text, decoderResult.RawBytes, points, BarcodeFormat.Aztec);
      List<byte[]> byteSegments = decoderResult.ByteSegments;
      if (byteSegments != null)
        result.PutMetadata(ResultMetadataType.ByteSegments, (object) byteSegments);
      string ecLevel = decoderResult.ECLevel;
      if (ecLevel != null)
        result.PutMetadata(ResultMetadataType.ErrorCorrectionLevel, (object) ecLevel);
      return result;
    }

    public void Reset()
    {
    }
  }
}
