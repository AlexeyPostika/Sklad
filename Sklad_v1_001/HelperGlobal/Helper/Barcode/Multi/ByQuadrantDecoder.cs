// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Multi.ByQuadrantDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;

namespace MessagingToolkit.Barcode.Multi
{
  public sealed class ByQuadrantDecoder : IDecoder
  {
    private readonly IDecoder decoder;

    public ByQuadrantDecoder(IDecoder delegat0) => this.decoder = delegat0;

    public Result Decode(BinaryBitmap image) => this.Decode(image, (Dictionary<DecodeOptions, object>) null);

    public Result Decode(
      BinaryBitmap image,
      Dictionary<DecodeOptions, object> decodingOptions)
    {
      int width = image.Width;
      int height = image.Height;
      int num1 = width / 2;
      int num2 = height / 2;
      BinaryBitmap binaryBitmap1 = image.Crop(0, 0, num1, num2);
      try
      {
        return this.decoder.Decode(binaryBitmap1, decodingOptions);
      }
      catch (NotFoundException ex)
      {
      }
      BinaryBitmap binaryBitmap2 = image.Crop(num1, 0, num1, num2);
      try
      {
        return this.decoder.Decode(binaryBitmap2, decodingOptions);
      }
      catch (NotFoundException ex)
      {
      }
      BinaryBitmap binaryBitmap3 = image.Crop(0, num2, num1, num2);
      try
      {
        return this.decoder.Decode(binaryBitmap3, decodingOptions);
      }
      catch (NotFoundException ex)
      {
      }
      BinaryBitmap binaryBitmap4 = image.Crop(num1, num2, num1, num2);
      try
      {
        return this.decoder.Decode(binaryBitmap4, decodingOptions);
      }
      catch (NotFoundException ex)
      {
      }
      int left = num1 / 2;
      int top = num2 / 2;
      return this.decoder.Decode(image.Crop(left, top, num1, num2), decodingOptions);
    }

    public void Reset() => this.decoder.Reset();
  }
}
