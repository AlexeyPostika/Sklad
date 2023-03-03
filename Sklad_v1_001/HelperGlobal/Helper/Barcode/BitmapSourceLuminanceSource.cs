// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.BitmapSourceLuminanceSource
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode
{
  public class BitmapSourceLuminanceSource : BaseLuminanceSource
  {
    protected BitmapSourceLuminanceSource(int width, int height)
      : base(width, height)
    {
    }

    public BitmapSourceLuminanceSource(BitmapSource bitmap)
      : base(bitmap.PixelWidth, bitmap.PixelHeight)
    {
      switch (bitmap.Format.ToString())
      {
        case "Bgr24":
        case "Bgr32":
        case "Bgra32":
          this.CalculateLuminanceBGRA(bitmap);
          break;
        case "Rgb24":
          this.CalculateLuminanceRGB(bitmap);
          break;
        case "Bgr565":
          this.CalculateLuminanceBGR565(bitmap);
          break;
        default:
          bitmap = (BitmapSource) new FormatConvertedBitmap(bitmap, PixelFormats.Bgra32, (BitmapPalette) null, 0.0);
          this.CalculateLuminanceBGR(bitmap);
          break;
      }
    }

    private void CalculateLuminanceRGB(BitmapSource bitmap)
    {
      int pixelWidth = bitmap.PixelWidth;
      int pixelHeight = bitmap.PixelHeight;
      int num1 = (bitmap.Format.BitsPerPixel + 7) / 8;
      int stride = pixelWidth * num1;
      byte[] pixels = new byte[stride];
      Int32Rect sourceRect = new Int32Rect(0, 0, pixelWidth, 1);
      int index1 = 0;
      this.luminances = new byte[pixelWidth * pixelHeight];
      for (int index2 = 0; index2 < pixelHeight; ++index2)
      {
        bitmap.CopyPixels(sourceRect, (Array) pixels, stride, 0);
        for (int index3 = 0; index3 < stride; index3 += num1)
        {
          byte num2 = pixels[index3];
          byte num3 = pixels[index3 + 1];
          byte num4 = pixels[index3 + 2];
          this.luminances[index1] = (byte) (0.3 * (double) num2 + 0.59 * (double) num3 + 0.11 * (double) num4 + 0.01);
          ++index1;
        }
        ++sourceRect.Y;
      }
    }

    private void CalculateLuminanceBGR(BitmapSource bitmap)
    {
      int pixelWidth = bitmap.PixelWidth;
      int pixelHeight = bitmap.PixelHeight;
      int num1 = (bitmap.Format.BitsPerPixel + 7) / 8;
      int stride = pixelWidth * num1;
      byte[] pixels = new byte[stride];
      Int32Rect sourceRect = new Int32Rect(0, 0, pixelWidth, 1);
      int index1 = 0;
      this.luminances = new byte[pixelWidth * pixelHeight];
      for (int index2 = 0; index2 < pixelHeight; ++index2)
      {
        bitmap.CopyPixels(sourceRect, (Array) pixels, stride, 0);
        for (int index3 = 0; index3 < stride; index3 += num1)
        {
          byte num2 = pixels[index3];
          byte num3 = pixels[index3 + 1];
          byte num4 = pixels[index3 + 2];
          this.luminances[index1] = (byte) (0.3 * (double) num4 + 0.59 * (double) num3 + 0.11 * (double) num2 + 0.01);
          ++index1;
        }
        ++sourceRect.Y;
      }
    }

    private void CalculateLuminanceBGRA(BitmapSource bitmap)
    {
      int pixelWidth = bitmap.PixelWidth;
      int pixelHeight = bitmap.PixelHeight;
      int num1 = (bitmap.Format.BitsPerPixel + 7) / 8;
      int stride = pixelWidth * num1;
      byte[] pixels = new byte[stride];
      Int32Rect sourceRect = new Int32Rect(0, 0, pixelWidth, 1);
      int index1 = 0;
      this.luminances = new byte[pixelWidth * pixelHeight];
      for (int index2 = 0; index2 < pixelHeight; ++index2)
      {
        bitmap.CopyPixels(sourceRect, (Array) pixels, stride, 0);
        for (int index3 = 0; index3 < stride; index3 += num1)
        {
          byte num2 = pixels[index3];
          byte num3 = pixels[index3 + 1];
          byte num4 = (byte) (0.3 * (double) pixels[index3 + 2] + 0.59 * (double) num3 + 0.11 * (double) num2 + 0.01);
          byte num5 = pixels[index3 + 3];
          byte num6 = (byte) (((int) num4 * (int) num5 >> 8) + ((int) byte.MaxValue * ((int) byte.MaxValue - (int) num5) >> 8));
          this.luminances[index1] = num6;
          ++index1;
        }
        ++sourceRect.Y;
      }
    }

    private void CalculateLuminanceBGR565(BitmapSource bitmap)
    {
      int pixelWidth = bitmap.PixelWidth;
      int pixelHeight = bitmap.PixelHeight;
      int num1 = (bitmap.Format.BitsPerPixel + 7) / 8;
      int stride = pixelWidth * num1;
      byte[] pixels = new byte[stride];
      Int32Rect sourceRect = new Int32Rect(0, 0, pixelWidth, 1);
      int index1 = 0;
      this.luminances = new byte[pixelWidth * pixelHeight];
      for (int index2 = 0; index2 < pixelHeight; ++index2)
      {
        bitmap.CopyPixels(sourceRect, (Array) pixels, stride, 0);
        for (int index3 = 0; index3 < stride; index3 += num1)
        {
          byte num2 = pixels[index3];
          byte num3 = pixels[index3 + 1];
          int num4 = (int) num2 & 31;
          int num5 = (((int) num2 & 224) >> 5 | ((int) num3 & 3) << 3) & 31;
          int num6 = ((int) num3 >> 2 & 31) * 527 + 23 >> 6;
          int num7 = num5 * 527 + 23 >> 6;
          int num8 = num4 * 527 + 23 >> 6;
          this.luminances[index1] = (byte) (0.3 * (double) num6 + 0.59 * (double) num7 + 0.11 * (double) num8 + 0.01);
          ++index1;
        }
        ++sourceRect.Y;
      }
    }

    protected override LuminanceSource CreateLuminanceSource(
      byte[] newLuminances,
      int width,
      int height)
    {
      BitmapSourceLuminanceSource luminanceSource = new BitmapSourceLuminanceSource(width, height);
      luminanceSource.luminances = newLuminances;
      return (LuminanceSource) luminanceSource;
    }
  }
}
