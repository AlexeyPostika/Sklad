// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.DataMatrixDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.DataMatrix.Decoders;
using MessagingToolkit.Barcode.DataMatrix.detector;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.DataMatrix
{
  public sealed class DataMatrixDecoder : IDecoder
  {
    private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];
    private readonly Decoder decoder;

    public DataMatrixDecoder() => this.decoder = new Decoder();

    public Result Decode(BinaryBitmap image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      DecoderResult decoderResult;
      ResultPoint[] resultPoints;
      if (decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.PureBarcode))
      {
        decoderResult = this.decoder.Decode(DataMatrixDecoder.ExtractPureBits(image.BlackMatrix));
        resultPoints = DataMatrixDecoder.NO_POINTS;
      }
      else
      {
        DetectorResult detectorResult = new Detector(image.BlackMatrix).Detect();
        decoderResult = this.decoder.Decode(detectorResult.Bits);
        resultPoints = detectorResult.Points;
      }
      Result result = new Result(decoderResult.Text, decoderResult.RawBytes, resultPoints, BarcodeFormat.DataMatrix);
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

    private static BitMatrix ExtractPureBits(BitMatrix image)
    {
      int[] topLeftOnBit = image.GetTopLeftOnBit();
      int[] bottomRightOnBit = image.GetBottomRightOnBit();
      int num1 = topLeftOnBit != null && bottomRightOnBit != null ? DataMatrixDecoder.ModuleSize(topLeftOnBit, image) : throw NotFoundException.Instance;
      int num2 = topLeftOnBit[1];
      int num3 = bottomRightOnBit[1];
      int num4 = topLeftOnBit[0];
      int width = (bottomRightOnBit[0] - num4 + 1) / num1;
      int height = (num3 - num2 + 1) / num1;
      if (width <= 0 || height <= 0)
        throw NotFoundException.Instance;
      int num5 = num1 >> 1;
      int num6 = num2 + num5;
      int num7 = num4 + num5;
      BitMatrix pureBits = new BitMatrix(width, height);
      for (int y1 = 0; y1 < height; ++y1)
      {
        int y2 = num6 + y1 * num1;
        for (int x = 0; x < width; ++x)
        {
          if (image.Get(num7 + x * num1, y2))
            pureBits.Set(x, y1);
        }
      }
      return pureBits;
    }

    private static int ModuleSize(int[] leftTopBlack, BitMatrix image)
    {
      int width = image.Width;
      int x = leftTopBlack[0];
      int y = leftTopBlack[1];
      while (x < width && image.Get(x, y))
        ++x;
      if (x == width)
        throw NotFoundException.Instance;
      int num = x - leftTopBlack[0];
      return num != 0 ? num : throw NotFoundException.Instance;
    }
  }
}
