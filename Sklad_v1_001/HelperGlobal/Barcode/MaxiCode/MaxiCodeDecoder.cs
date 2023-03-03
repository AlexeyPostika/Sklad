// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.MaxiCode.MaxiCodeDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.MaxiCode
{
  public sealed class MaxiCodeDecoder : IDecoder
  {
    private const int MATRIX_WIDTH = 30;
    private const int MATRIX_HEIGHT = 33;
    private static readonly ResultPoint[] NO_POINTS = new ResultPoint[0];
    private readonly MessagingToolkit.Barcode.MaxiCode.Decoder.Decoder decoder = new MessagingToolkit.Barcode.MaxiCode.Decoder.Decoder();

    internal MessagingToolkit.Barcode.MaxiCode.Decoder.Decoder GetDecoder() => this.decoder;

    public Result Decode(BinaryBitmap image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      if (decodingOptions == null || !decodingOptions.ContainsKey(DecodeOptions.PureBarcode))
        throw NotFoundException.Instance;
      DecoderResult decoderResult = this.decoder.Decode(MaxiCodeDecoder.ExtractPureBits(image.BlackMatrix), decodingOptions);
      ResultPoint[] noPoints = MaxiCodeDecoder.NO_POINTS;
      Result result = new Result(decoderResult.Text, decoderResult.RawBytes, noPoints, BarcodeFormat.MaxiCode);
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
      int[] enclosingRectangle = image.GetEnclosingRectangle();
      int num1 = enclosingRectangle != null ? enclosingRectangle[0] : throw NotFoundException.Instance;
      int num2 = enclosingRectangle[1];
      int num3 = enclosingRectangle[2];
      int num4 = enclosingRectangle[3];
      BitMatrix pureBits = new BitMatrix(30, 33);
      for (int y1 = 0; y1 < 33; ++y1)
      {
        int y2 = num2 + (y1 * num4 + num4 / 2) / 33;
        for (int x1 = 0; x1 < 30; ++x1)
        {
          int x2 = num1 + (x1 * num3 + num3 / 2 + (y1 & 1) * num3 / 2) / 30;
          if (image.Get(x2, y2))
            pureBits.Set(x1, y1);
        }
      }
      return pureBits;
    }
  }
}
