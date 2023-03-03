// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Pdf417Decoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Pdf417.Detector;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Pdf417
{
  internal sealed class Pdf417Decoder : IDecoder
  {
    private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];
    private readonly MessagingToolkit.Barcode.Pdf417.Decoder.Decoder decoder;

    public Pdf417Decoder() => this.decoder = new MessagingToolkit.Barcode.Pdf417.Decoder.Decoder();

    public Result Decode(BinaryBitmap image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      DecoderResult decoderResult;
      ResultPoint[] resultPoints;
      if (decodingOptions != null && decodingOptions.ContainsKey(DecodeOptions.PureBarcode))
      {
        decoderResult = this.decoder.Decode(Pdf417Decoder.ExtractPureBits(image.BlackMatrix));
        resultPoints = Pdf417Decoder.NO_POINTS;
      }
      else
      {
        DetectorResult detectorResult = new MessagingToolkit.Barcode.Pdf417.Detector.Detector(image).Detect(decodingOptions);
        try
        {
          decoderResult = this.decoder.Decode(detectorResult.Bits);
        }
        catch
        {
          decoderResult = new Detector2(image.BlackMatrix).Detect();
        }
        resultPoints = detectorResult.Points;
      }
      return new Result(decoderResult.Text, decoderResult.RawBytes, resultPoints, BarcodeFormat.PDF417);
    }

    public void Reset()
    {
    }

    private static BitMatrix ExtractPureBits(BitMatrix image)
    {
      int[] topLeftOnBit = image.GetTopLeftOnBit();
      int[] bottomRightOnBit = image.GetBottomRightOnBit();
      int num1 = topLeftOnBit != null && bottomRightOnBit != null ? Pdf417Decoder.ModuleSize(topLeftOnBit, image) : throw NotFoundException.Instance;
      int y1 = topLeftOnBit[1];
      int num2 = bottomRightOnBit[1];
      int patternStart = Pdf417Decoder.FindPatternStart(topLeftOnBit[0], y1, image);
      int width = (Pdf417Decoder.FindPatternEnd(topLeftOnBit[0], y1, image) - patternStart + 1) / num1;
      int height = (num2 - y1 + 1) / num1;
      if (width <= 0 || height <= 0)
        throw NotFoundException.Instance;
      int num3 = num1 >> 1;
      int num4 = y1 + num3;
      int num5 = patternStart + num3;
      BitMatrix pureBits = new BitMatrix(width, height);
      for (int y2 = 0; y2 < height; ++y2)
      {
        int y3 = num4 + y2 * num1;
        for (int x = 0; x < width; ++x)
        {
          if (image.Get(num5 + x * num1, y3))
            pureBits.Set(x, y2);
        }
      }
      return pureBits;
    }

    private static int ModuleSize(int[] leftTopBlack, BitMatrix image)
    {
      int x = leftTopBlack[0];
      int y = leftTopBlack[1];
      int width = image.GetWidth();
      while (x < width && image.Get(x, y))
        ++x;
      if (x == width)
        throw NotFoundException.Instance;
      int num = (int) ((uint) (x - leftTopBlack[0]) >> 3);
      return num != 0 ? num : throw NotFoundException.Instance;
    }

    private static int FindPatternStart(int x, int y, BitMatrix image)
    {
      int width = image.GetWidth();
      int x1 = x;
      int num = 0;
      bool flag1 = true;
      while (x1 < width - 1 && num < 8)
      {
        ++x1;
        bool flag2 = image.Get(x1, y);
        if (flag1 != flag2)
          ++num;
        flag1 = flag2;
      }
      return x1 != width - 1 ? x1 : throw NotFoundException.Instance;
    }

    private static int FindPatternEnd(int x, int y, BitMatrix image)
    {
      int x1 = image.GetWidth() - 1;
      while (x1 > x && !image.Get(x1, y))
        --x1;
      int num = 0;
      bool flag1 = true;
      while (x1 > x && num < 9)
      {
        --x1;
        bool flag2 = image.Get(x1, y);
        if (flag1 != flag2)
          ++num;
        flag1 = flag2;
      }
      return x1 != x ? x1 : throw NotFoundException.Instance;
    }
  }
}
