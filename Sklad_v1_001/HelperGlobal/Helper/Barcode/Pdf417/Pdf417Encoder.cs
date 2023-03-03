// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Pdf417Encoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.Pdf417.Encoder;
using MessagingToolkit.Barcode.QRCode.Encoder;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Pdf417
{
  public sealed class Pdf417Encoder : IEncoder
  {
    private const int QUIET_ZONE_SIZE = 4;

    public BitMatrix Encode(
      string contents,
      BarcodeFormat format,
      int width,
      int height,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      if (format != BarcodeFormat.PDF417)
        throw new ArgumentException("Can only encode PDF417, but got " + (object) format);
      MessagingToolkit.Barcode.Pdf417.Encoder.Pdf417 encoder = new MessagingToolkit.Barcode.Pdf417.Encoder.Pdf417();
      int quietZone = 4;
      if (encodingOptions != null)
      {
        if (encodingOptions.ContainsKey(EncodeOptions.Pdf417Compact))
        {
          object encodeOptionType = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.Pdf417Compact);
          encoder.SetCompact((bool) encodeOptionType);
        }
        if (encodingOptions.ContainsKey(EncodeOptions.Pdf417Compaction))
        {
          object encodeOptionType = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.Pdf417Compaction);
          encoder.SetCompaction((Compaction) encodeOptionType);
        }
        if (encodingOptions.ContainsKey(EncodeOptions.Pdf417Dimensions))
        {
          Dimensions encodeOptionType = (Dimensions) BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.Pdf417Dimensions);
          encoder.SetDimensions(encodeOptionType.MaxCols, encodeOptionType.MinCols, encodeOptionType.MaxRows, encodeOptionType.MinRows);
        }
        if (encodingOptions.ContainsKey(EncodeOptions.Margin))
          quietZone = Convert.ToInt32(BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.Margin));
      }
      return Pdf417Encoder.BitMatrixFromEncoder(encoder, contents, width, height, quietZone);
    }

    public BitMatrix Encode(string contents, BarcodeFormat format, int width, int height) => this.Encode(contents, format, width, height, (Dictionary<EncodeOptions, object>) null);

    private static BitMatrix BitMatrixFromEncoder(
      MessagingToolkit.Barcode.Pdf417.Encoder.Pdf417 encoder,
      string contents,
      int width,
      int height,
      int quietZone)
    {
      int errorCorrectionLevel = 2;
      encoder.GenerateBarcodeLogic(contents, errorCorrectionLevel);
      int xScale = 2;
      int num1 = 4;
      byte[][] numArray1 = encoder.GetBarcodeMatrix().GetScaledMatrix(xScale, num1 * xScale);
      bool flag = false;
      if (height > width ^ numArray1[0].Length < numArray1.Length)
      {
        numArray1 = Pdf417Encoder.RotateArray(numArray1);
        flag = true;
      }
      int num2 = width / numArray1[0].Length;
      int num3 = height / numArray1.Length;
      int num4 = num2 >= num3 ? num3 : num2;
      if (num4 <= 1)
        return Pdf417Encoder.RenderResult(new ByteMatrix(numArray1[0].Length, numArray1.Length, numArray1), contents, width, height, quietZone);
      byte[][] numArray2 = encoder.GetBarcodeMatrix().GetScaledMatrix(num4 * xScale, num4 * num1 * xScale);
      if (flag)
        numArray2 = Pdf417Encoder.RotateArray(numArray2);
      return Pdf417Encoder.RenderResult(new ByteMatrix(numArray2[0].Length, numArray2.Length, numArray2), contents, width, height, quietZone);
    }

    private static BitMatrix RenderResult(
      ByteMatrix input,
      string contents,
      int width,
      int height,
      int quietZone)
    {
      int width1 = input.Width;
      int height1 = input.Height;
      int val2_1 = width1 + (quietZone << 1);
      int val2_2 = height1 + (quietZone << 1);
      int width2 = Math.Max(width, val2_1);
      int height2 = Math.Max(height, val2_2);
      int num1 = Math.Min(width2 / val2_1, height2 / val2_2);
      int num2 = (width2 - width1 * num1) / 2;
      int num3 = (height2 - height1 * num1) / 2;
      BitMatrix bitMatrix = new BitMatrix(width2, height2);
      bitMatrix.LeftPadding = num2;
      bitMatrix.TopPadding = num3;
      bitMatrix.ActualHeight = num1 * height1;
      bitMatrix.ActualWidth = num1 * width1;
      int y = 0;
      int top = num3;
      while (y < height1)
      {
        int x = 0;
        int left = num2;
        while (x < width1)
        {
          if (input.Get(x, y) == (byte) 1)
            bitMatrix.SetRegion(left, top, num1, num1);
          ++x;
          left += num1;
        }
        ++y;
        top += num1;
      }
      return bitMatrix;
    }

    private static byte[][] RotateArray(byte[][] bitarray)
    {
      byte[][] numArray = new byte[bitarray[0].Length][];
      for (int index = 0; index < bitarray[0].Length; ++index)
        numArray[index] = new byte[bitarray.Length];
      for (int index1 = 0; index1 < bitarray.Length; ++index1)
      {
        int index2 = bitarray.Length - index1 - 1;
        for (int index3 = 0; index3 < bitarray[0].Length; ++index3)
          numArray[index3][index2] = bitarray[index1][index3];
      }
      return numArray;
    }
  }
}
