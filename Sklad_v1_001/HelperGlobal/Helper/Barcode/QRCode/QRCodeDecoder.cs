// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.QRCodeDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.QRCode
{
  public class QRCodeDecoder : IDecoder
  {
    private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];
    private readonly MessagingToolkit.Barcode.QRCode.Decoder.Decoder decoder;

    public QRCodeDecoder() => this.decoder = new MessagingToolkit.Barcode.QRCode.Decoder.Decoder();

    protected internal MessagingToolkit.Barcode.QRCode.Decoder.Decoder GetDecoder() => this.decoder;

    public virtual Result Decode(BinaryBitmap image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public virtual Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      DecoderResult decoderResult;
      ResultPoint[] resultPoints;
      if (decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.PureBarcode))
      {
        decoderResult = this.decoder.Decode(QRCodeDecoder.ExtractPureBits(image.BlackMatrix), decodingOptions);
        resultPoints = QRCodeDecoder.NO_POINTS;
      }
      else
      {
        DetectorResult detectorResult = new MessagingToolkit.Barcode.QRCode.Detector.Detector(image.BlackMatrix).Detect(decodingOptions);
        decoderResult = this.decoder.Decode(detectorResult.Bits, decodingOptions);
        resultPoints = detectorResult.Points;
      }
      Result result = new Result(decoderResult.Text, decoderResult.RawBytes, resultPoints, BarcodeFormat.QRCode);
      IList<byte[]> byteSegments = (IList<byte[]>) decoderResult.ByteSegments;
      if (byteSegments != null)
        result.PutMetadata(ResultMetadataType.ByteSegments, (object) byteSegments);
      string ecLevel = decoderResult.ECLevel;
      if (ecLevel != null)
        result.PutMetadata(ResultMetadataType.ErrorCorrectionLevel, (object) ecLevel);
      return result;
    }

    public virtual void Reset()
    {
    }

    private static BitMatrix ExtractPureBits(BitMatrix image)
    {
      int[] topLeftOnBit = image.GetTopLeftOnBit();
      int[] bottomRightOnBit = image.GetBottomRightOnBit();
      float num1 = topLeftOnBit != null && bottomRightOnBit != null ? QRCodeDecoder.ModuleSize(topLeftOnBit, image) : throw NotFoundException.Instance;
      int num2 = topLeftOnBit[1];
      int num3 = bottomRightOnBit[1];
      int num4 = topLeftOnBit[0];
      int num5 = bottomRightOnBit[0];
      if (num3 - num2 != num5 - num4)
        num5 = num4 + (num3 - num2);
      int width = (int) Math.Round((double) (num5 - num4 + 1) / (double) num1);
      int height = (int) Math.Round((double) (num3 - num2 + 1) / (double) num1);
      if (width <= 0 || height <= 0)
        throw NotFoundException.Instance;
      if (height != width)
        throw NotFoundException.Instance;
      int num6 = (int) ((double) num1 / 2.0);
      int num7 = num2 + num6;
      int num8 = num4 + num6;
      BitMatrix pureBits = new BitMatrix(width, height);
      for (int y1 = 0; y1 < height; ++y1)
      {
        int y2 = num7 + (int) ((double) y1 * (double) num1);
        for (int x = 0; x < width; ++x)
        {
          if (image.Get(num8 + (int) ((double) x * (double) num1), y2))
            pureBits.Set(x, y1);
        }
      }
      return pureBits;
    }

    private static float ModuleSize(int[] leftTopBlack, BitMatrix image)
    {
      int height = image.GetHeight();
      int width = image.GetWidth();
      int x = leftTopBlack[0];
      int y = leftTopBlack[1];
      bool flag = true;
      int num = 0;
      for (; x < width && y < height; ++y)
      {
        if (flag != image.Get(x, y))
        {
          if (++num != 5)
            flag = !flag;
          else
            break;
        }
        ++x;
      }
      if (x == width || y == height)
        throw NotFoundException.Instance;
      return (float) (x - leftTopBlack[0]) / 7f;
    }
  }
}
