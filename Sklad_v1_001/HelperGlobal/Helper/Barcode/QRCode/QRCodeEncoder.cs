// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.QRCodeEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.QRCode.Decoder;
using MessagingToolkit.Barcode.QRCode.Encoder;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.QRCode
{
  public sealed class QRCodeEncoder : IEncoder
  {
    private const int QUIET_ZONE_SIZE = 4;

    public BitMatrix Encode(string contents, BarcodeFormat format, int width, int height) => this.Encode(contents, format, width, height, (Dictionary<EncodeOptions, object>) null);

    public BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (string.IsNullOrEmpty(contents))
        throw new ArgumentException("Found empty contents");
      if (format != BarcodeFormat.QRCode)
        throw new ArgumentException("Can only Encode QR_CODE, but got " + (object) format);
      if (width < 0 || height < 0)
        throw new ArgumentException("Requested dimensions are too small: " + (object) width + (object) 'x' + (object) height);
      ErrorCorrectionLevel ecLevel = ErrorCorrectionLevel.L;
      int quietZone = 4;
      if (encodingOptions != null)
      {
        ErrorCorrectionLevel encodeOptionType1 = (ErrorCorrectionLevel) BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.ErrorCorrection);
        if (encodeOptionType1 != null)
          ecLevel = encodeOptionType1;
        object encodeOptionType2 = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.Margin);
        if (encodeOptionType2 != null)
          quietZone = Convert.ToInt32(encodeOptionType2);
      }
      return QRCodeEncoder.RenderResult(MessagingToolkit.Barcode.QRCode.Encoder.Encoder.Encode(contents, ecLevel, encodingOptions), width, height, quietZone);
    }

    private static BitMatrix RenderResult(
      MessagingToolkit.Barcode.QRCode.Encoder.QRCode code,
      int width,
      int height,
      int quietZone)
    {
      ByteMatrix matrix = code.Matrix;
      int num1 = matrix != null ? matrix.Width : throw new InvalidOperationException();
      int height1 = matrix.Height;
      int val2_1 = num1 + (quietZone << 1);
      int val2_2 = height1 + (quietZone << 1);
      int width1 = Math.Max(width, val2_1);
      int height2 = Math.Max(height, val2_2);
      int num2 = Math.Min(width1 / val2_1, height2 / val2_2);
      int num3 = (width1 - num1 * num2) / 2;
      int num4 = (height2 - height1 * num2) / 2;
      BitMatrix bitMatrix = new BitMatrix(width1, height2);
      bitMatrix.LeftPadding = num3;
      bitMatrix.TopPadding = num4;
      bitMatrix.ActualHeight = num2 * height1;
      bitMatrix.ActualWidth = num2 * num1;
      int y = 0;
      int top = num4;
      while (y < height1)
      {
        int x = 0;
        int left = num3;
        while (x < num1)
        {
          if (matrix.Get(x, y) == (byte) 1)
            bitMatrix.SetRegion(left, top, num2, num2);
          ++x;
          left += num2;
        }
        ++y;
        top += num2;
      }
      return bitMatrix;
    }
  }
}
