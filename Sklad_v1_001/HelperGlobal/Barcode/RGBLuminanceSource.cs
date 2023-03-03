// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.RGBLuminanceSource
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode
{
  public class RGBLuminanceSource : BaseLuminanceSource
  {
    protected RGBLuminanceSource(int width, int height)
      : base(width, height)
    {
    }

    public RGBLuminanceSource(byte[] rgbRawBytes, int width, int height)
      : this(rgbRawBytes, width, height, RGBLuminanceSource.BitmapFormat.RGB24)
    {
    }

    [Obsolete("Use RGBLuminanceSource(luminanceArray, width, height, BitmapFormat.Gray8)")]
    public RGBLuminanceSource(byte[] luminanceArray, int width, int height, bool is8Bit)
      : this(luminanceArray, width, height, RGBLuminanceSource.BitmapFormat.Gray8)
    {
    }

    public RGBLuminanceSource(
      byte[] rgbRawBytes,
      int width,
      int height,
      RGBLuminanceSource.BitmapFormat bitmapFormat)
      : base(width, height)
    {
      this.CalculateLuminance(rgbRawBytes, bitmapFormat);
    }

    protected override LuminanceSource CreateLuminanceSource(
      byte[] newLuminances,
      int width,
      int height)
    {
      RGBLuminanceSource luminanceSource = new RGBLuminanceSource(width, height);
      luminanceSource.luminances = newLuminances;
      return (LuminanceSource) luminanceSource;
    }

    private static RGBLuminanceSource.BitmapFormat DetermineBitmapFormat(
      byte[] rgbRawBytes,
      int width,
      int height)
    {
      int num = width * height;
      switch (rgbRawBytes.Length / num)
      {
        case 1:
          return RGBLuminanceSource.BitmapFormat.Gray8;
        case 2:
          return RGBLuminanceSource.BitmapFormat.RGB565;
        case 3:
          return RGBLuminanceSource.BitmapFormat.RGB24;
        case 4:
          return RGBLuminanceSource.BitmapFormat.RGB32;
        default:
          throw new ArgumentException("The bitmap format could not be determined. Please specify the correct value.");
      }
    }

    protected void CalculateLuminance(
      byte[] rgbRawBytes,
      RGBLuminanceSource.BitmapFormat bitmapFormat)
    {
      if (bitmapFormat == RGBLuminanceSource.BitmapFormat.Unknown)
        bitmapFormat = RGBLuminanceSource.DetermineBitmapFormat(rgbRawBytes, this.Width, this.Height);
      switch (bitmapFormat - 1)
      {
        case RGBLuminanceSource.BitmapFormat.Unknown:
          Buffer.BlockCopy((Array) rgbRawBytes, 0, (Array) this.luminances, 0, rgbRawBytes.Length < this.luminances.Length ? rgbRawBytes.Length : this.luminances.Length);
          break;
        case RGBLuminanceSource.BitmapFormat.Gray8:
        case RGBLuminanceSource.BitmapFormat.ARGB32:
          this.CalculateLuminanceRGB24(rgbRawBytes);
          break;
        case RGBLuminanceSource.BitmapFormat.RGB24:
        case RGBLuminanceSource.BitmapFormat.BGR24:
          this.CalculateLuminanceRGB32(rgbRawBytes);
          break;
        case RGBLuminanceSource.BitmapFormat.RGB32:
          this.CalculateLuminanceARGB32(rgbRawBytes);
          break;
        case RGBLuminanceSource.BitmapFormat.BGR32:
          this.CalculateLuminanceBGRA32(rgbRawBytes);
          break;
        case RGBLuminanceSource.BitmapFormat.BGRA32:
          this.CalculateLuminanceRGB565(rgbRawBytes);
          break;
        case RGBLuminanceSource.BitmapFormat.RGB565:
          this.CalculateLuminanceRGBA32(rgbRawBytes);
          break;
        default:
          throw new ArgumentException("The bitmap format isn't supported.", bitmapFormat.ToString());
      }
    }

    private void CalculateLuminanceBGRA32(byte[] rgbRawBytes)
    {
      int num1 = 0;
      for (int index1 = 0; num1 < rgbRawBytes.Length && index1 < this.luminances.Length; ++index1)
      {
        byte[] numArray1 = rgbRawBytes;
        int index2 = num1;
        int num2 = index2 + 1;
        byte num3 = numArray1[index2];
        byte[] numArray2 = rgbRawBytes;
        int index3 = num2;
        int num4 = index3 + 1;
        byte num5 = numArray2[index3];
        byte[] numArray3 = rgbRawBytes;
        int index4 = num4;
        int num6 = index4 + 1;
        byte num7 = numArray3[index4];
        byte[] numArray4 = rgbRawBytes;
        int index5 = num6;
        num1 = index5 + 1;
        byte num8 = numArray4[index5];
        byte num9 = (byte) ((int) num7 + (int) num5 + (int) num5 + (int) num3 >> 2);
        this.luminances[index1] = (byte) (((int) num9 * (int) num8 >> 8) + ((int) byte.MaxValue * ((int) byte.MaxValue - (int) num8) >> 8));
      }
    }

    private void CalculateLuminanceRGBA32(byte[] rgbRawBytes)
    {
      int num1 = 0;
      for (int index1 = 0; num1 < rgbRawBytes.Length && index1 < this.luminances.Length; ++index1)
      {
        byte[] numArray1 = rgbRawBytes;
        int index2 = num1;
        int num2 = index2 + 1;
        byte num3 = numArray1[index2];
        byte[] numArray2 = rgbRawBytes;
        int index3 = num2;
        int num4 = index3 + 1;
        byte num5 = numArray2[index3];
        byte[] numArray3 = rgbRawBytes;
        int index4 = num4;
        int num6 = index4 + 1;
        byte num7 = numArray3[index4];
        byte[] numArray4 = rgbRawBytes;
        int index5 = num6;
        num1 = index5 + 1;
        byte num8 = numArray4[index5];
        byte num9 = (byte) ((int) num3 + (int) num5 + (int) num5 + (int) num7 >> 2);
        this.luminances[index1] = (byte) (((int) num9 * (int) num8 >> 8) + ((int) byte.MaxValue * ((int) byte.MaxValue - (int) num8) >> 8));
      }
    }

    private void CalculateLuminanceRGB565(byte[] rgb565RawData)
    {
      int index1 = 0;
      for (int index2 = 0; index2 < rgb565RawData.Length && index1 < this.luminances.Length; ++index1)
      {
        byte num1 = rgb565RawData[index2];
        byte num2 = rgb565RawData[index2 + 1];
        int num3 = (int) num1 & 31;
        int num4 = (((int) num1 & 224) >> 5 | ((int) num2 & 3) << 3) & 31;
        int num5 = ((int) num2 >> 2 & 31) * 527 + 23 >> 6;
        int num6 = num4 * 527 + 23 >> 6;
        int num7 = num3 * 527 + 23 >> 6;
        this.luminances[index1] = (byte) (0.3 * (double) num5 + 0.59 * (double) num6 + 0.11 * (double) num7 + 0.01);
        index2 += 2;
      }
    }

    private void CalculateLuminanceRGB24(byte[] rgbRawBytes)
    {
      int num1 = 0;
      for (int index1 = 0; num1 < rgbRawBytes.Length && index1 < this.luminances.Length; ++index1)
      {
        byte[] numArray1 = rgbRawBytes;
        int index2 = num1;
        int num2 = index2 + 1;
        int num3 = (int) numArray1[index2];
        byte[] numArray2 = rgbRawBytes;
        int index3 = num2;
        int num4 = index3 + 1;
        int num5 = (int) numArray2[index3];
        byte[] numArray3 = rgbRawBytes;
        int index4 = num4;
        num1 = index4 + 1;
        int num6 = (int) numArray3[index4];
        this.luminances[index1] = (byte) (num3 + num5 + num5 + num6 >> 2);
      }
    }

    private void CalculateLuminanceRGB32(byte[] rgbRawBytes)
    {
      int num1 = 0;
      for (int index1 = 0; num1 < rgbRawBytes.Length && index1 < this.luminances.Length; ++index1)
      {
        byte[] numArray1 = rgbRawBytes;
        int index2 = num1;
        int num2 = index2 + 1;
        int num3 = (int) numArray1[index2];
        byte[] numArray2 = rgbRawBytes;
        int index3 = num2;
        int num4 = index3 + 1;
        int num5 = (int) numArray2[index3];
        byte[] numArray3 = rgbRawBytes;
        int index4 = num4;
        int num6 = index4 + 1;
        int num7 = (int) numArray3[index4];
        num1 = num6 + 1;
        this.luminances[index1] = (byte) (num3 + num5 + num5 + num7 >> 2);
      }
    }

    private void CalculateLuminanceARGB32(byte[] rgbRawBytes)
    {
      int num1 = 0;
      for (int index1 = 0; num1 < rgbRawBytes.Length && index1 < this.luminances.Length; ++index1)
      {
        byte[] numArray1 = rgbRawBytes;
        int index2 = num1;
        int num2 = index2 + 1;
        byte num3 = numArray1[index2];
        byte[] numArray2 = rgbRawBytes;
        int index3 = num2;
        int num4 = index3 + 1;
        byte num5 = numArray2[index3];
        byte[] numArray3 = rgbRawBytes;
        int index4 = num4;
        int num6 = index4 + 1;
        byte num7 = numArray3[index4];
        byte[] numArray4 = rgbRawBytes;
        int index5 = num6;
        num1 = index5 + 1;
        byte num8 = numArray4[index5];
        byte num9 = (byte) ((int) num5 + (int) num7 + (int) num7 + (int) num8 >> 2);
        this.luminances[index1] = (byte) (((int) num9 * (int) num3 >> 8) + ((int) byte.MaxValue * ((int) byte.MaxValue - (int) num3) >> 8));
      }
    }

    public enum BitmapFormat
    {
      Unknown,
      Gray8,
      RGB24,
      RGB32,
      ARGB32,
      BGR24,
      BGR32,
      BGRA32,
      RGB565,
      RGBA32,
    }
  }
}
