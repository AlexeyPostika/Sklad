// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Multi.QRCode.QRCodeMultiDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Multi.QRCode.Detector;
using MessagingToolkit.Barcode.QRCode;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Multi.QRCode
{
  public sealed class QRCodeMultiDecoder : QRCodeDecoder, MultipleBarcodeDecoder
  {
    private static readonly Result[] EMPTY_RESULT_ARRAY = new Result[0];

    public Result[] DecodeMultiple(BinaryBitmap image) => this.DecodeMultiple(image, (Dictionary<DecodeOptions, object>) null);

    public Result[] DecodeMultiple(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      List<Result> resultList = new List<Result>();
      foreach (DetectorResult detectorResult in new MultiDetector(image.BlackMatrix).DetectMulti(decodingOptions))
      {
        try
        {
          DecoderResult decoderResult = this.GetDecoder().Decode(detectorResult.Bits, decodingOptions);
          ResultPoint[] points = detectorResult.Points;
          Result result = new Result(decoderResult.Text, decoderResult.RawBytes, points, BarcodeFormat.QRCode);
          IList<byte[]> byteSegments = (IList<byte[]>) decoderResult.ByteSegments;
          if (byteSegments != null)
            result.PutMetadata(ResultMetadataType.ByteSegments, (object) byteSegments);
          string ecLevel = decoderResult.ECLevel;
          if (ecLevel != null)
            result.PutMetadata(ResultMetadataType.ErrorCorrectionLevel, (object) ecLevel);
          resultList.Add(result);
        }
        catch (BarcodeDecoderException ex)
        {
        }
      }
      if (resultList.Count == 0)
        return QRCodeMultiDecoder.EMPTY_RESULT_ARRAY;
      Result[] resultArray = new Result[resultList.Count];
      for (int index = 0; index < resultList.Count; ++index)
        resultArray[index] = resultList[index];
      return resultArray;
    }
  }
}
