// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.DataMatrixNewEncoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.DataMatrix.Encoder;
using MessagingToolkit.Barcode.Helper;
using MessagingToolkit.Barcode.QRCode.Encoder;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.DataMatrix
{
  public sealed class DataMatrixNewEncoder : IEncoder
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
      if (format != BarcodeFormat.DataMatrix)
        throw new ArgumentException("Can only encode Data Matrix, but got " + (object) format);
      if (width < 0 || height < 0)
        throw new ArgumentException("Requested dimensions are too small: " + (object) width + (object) 'x' + (object) height);
      SymbolShapeHint shape = SymbolShapeHint.ForceNone;
      Dimension minSize = (Dimension) null;
      Dimension maxSize = (Dimension) null;
      int quietZone = 4;
      if (encodingOptions != null)
      {
        object encodeOptionType1 = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.DataMatrixShape);
        if (encodeOptionType1 != null)
          shape = (SymbolShapeHint) encodeOptionType1;
        object encodeOptionType2 = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.MinimumSize);
        if (encodeOptionType2 != null)
          minSize = (Dimension) encodeOptionType2;
        object encodeOptionType3 = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.MaximumSize);
        if (encodeOptionType3 != null)
          maxSize = (Dimension) encodeOptionType3;
        object encodeOptionType4 = BarcodeHelper.GetEncodeOptionType(encodingOptions, EncodeOptions.Margin);
        if (encodeOptionType4 != null)
          quietZone = Convert.ToInt32(encodeOptionType4);
      }
      string codewords;
      try
      {
        codewords = HighLevelEncoder.EncodeHighLevel(contents, shape, minSize, maxSize, encodingOptions);
      }
      catch (Exception ex)
      {
        throw new ArgumentException("Cannot fetch data: " + ex.Message);
      }
      SymbolInfo symbolInfo = SymbolInfo.Lookup(codewords.Length, shape, minSize, maxSize, true);
      DefaultPlacement placement = new DefaultPlacement(ErrorCorrection.EncodeECC200(codewords, symbolInfo), symbolInfo.SymbolDataWidth, symbolInfo.SymbolDataHeight);
      placement.Place();
      return DataMatrixNewEncoder.RenderResult(this.EncodeLowLevel(placement, symbolInfo), width, height, quietZone);
    }

    private ByteMatrix EncodeLowLevel(DefaultPlacement placement, SymbolInfo symbolInfo)
    {
      int symbolDataWidth = symbolInfo.SymbolDataWidth;
      int symbolDataHeight = symbolInfo.SymbolDataHeight;
      ByteMatrix byteMatrix = new ByteMatrix(symbolInfo.SymbolWidth, symbolInfo.SymbolHeight);
      int y = 0;
      for (int row = 0; row < symbolDataHeight; ++row)
      {
        if (row % symbolInfo.matrixHeight == 0)
        {
          int x = 0;
          for (int index = 0; index < symbolInfo.SymbolWidth; ++index)
          {
            byteMatrix.Set(x, y, index % 2 == 0);
            ++x;
          }
          ++y;
        }
        int x1 = 0;
        for (int col = 0; col < symbolDataWidth; ++col)
        {
          if (col % symbolInfo.matrixWidth == 0)
          {
            byteMatrix.Set(x1, y, true);
            ++x1;
          }
          byteMatrix.Set(x1, y, placement.GetBit(col, row));
          ++x1;
          if (col % symbolInfo.matrixWidth == symbolInfo.matrixWidth - 1)
          {
            byteMatrix.Set(x1, y, row % 2 == 0);
            ++x1;
          }
        }
        ++y;
        if (row % symbolInfo.matrixHeight == symbolInfo.matrixHeight - 1)
        {
          int x2 = 0;
          for (int index = 0; index < symbolInfo.SymbolWidth; ++index)
          {
            byteMatrix.Set(x2, y, true);
            ++x2;
          }
          ++y;
        }
      }
      return byteMatrix;
    }

    private BitMatrix ConvertByteMatrixToBitMatrix(ByteMatrix matrix)
    {
      int width = matrix.Width;
      int height = matrix.Height;
      BitMatrix bitMatrix = new BitMatrix(width, height);
      bitMatrix.Clear();
      for (int x = 0; x < width; ++x)
      {
        for (int y = 0; y < height; ++y)
        {
          if (matrix.Get(x, y) == (byte) 1)
            bitMatrix.Set(x, y);
        }
      }
      return bitMatrix;
    }

    private static BitMatrix RenderResult(
      ByteMatrix input,
      int width,
      int height,
      int quietZone)
    {
      int num1 = input != null ? input.Width : throw new InvalidOperationException();
      int height1 = input.Height;
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
          if (input.Get(x, y) == (byte) 1)
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
