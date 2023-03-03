// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.IBarcodeDecoder
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode
{
  public interface IBarcodeDecoder : IDecoder
  {
    Result Decode(WriteableBitmap image);

    Result Decode(WriteableBitmap image, Dictionary<DecodeOptions, object> decodingOptions);

    Result Decode(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format);

    Result Decode(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format,
      Dictionary<DecodeOptions, object> decodingOptions);

    Result[] DecodeMultiple(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format);

    Result[] DecodeMultiple(
      byte[] rawRGB,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat format,
      Dictionary<DecodeOptions, object> decodingOptions);

    Result[] DecodeMultiple(BinaryBitmap binaryBitmap);

    Result[] DecodeMultiple(
      BinaryBitmap binaryBitmap,
      Dictionary<DecodeOptions, object> decodingOptions);
  }
}
