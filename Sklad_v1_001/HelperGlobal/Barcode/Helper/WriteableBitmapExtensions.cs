// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Helper.WriteableBitmapExtensions
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode.Helper
{
  public static class WriteableBitmapExtensions
  {
    internal const int SizeOfArgb = 4;

    public static byte[] ToByteArray(this WriteableBitmap bmp, int offset, int count)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        if (count == -1)
          count = bitmapContext.Length;
        int count1 = count * 4;
        byte[] dest = new byte[count1];
        BitmapContext.BlockCopy(bitmapContext, offset, dest, 0, count1);
        return dest;
      }
    }

    public static byte[] ToByteArray(this WriteableBitmap bmp, int count) => bmp.ToByteArray(0, count);

    public static byte[] ToByteArray(this WriteableBitmap bmp) => bmp.ToByteArray(0, -1);

    public static WriteableBitmap FromByteArray(
      this WriteableBitmap bmp,
      byte[] buffer,
      int offset,
      int count)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        BitmapContext.BlockCopy(buffer, offset, bitmapContext, 0, count);
        return bmp;
      }
    }

    public static WriteableBitmap FromByteArray(
      this WriteableBitmap bmp,
      byte[] buffer,
      int count)
    {
      return bmp.FromByteArray(buffer, 0, count);
    }

    public static WriteableBitmap FromByteArray(
      this WriteableBitmap bmp,
      byte[] buffer)
    {
      return bmp.FromByteArray(buffer, 0, buffer.Length);
    }

    public static unsafe void WriteTga(this WriteableBitmap bmp, Stream destination)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int* pixels = bitmapContext.Pixels;
        byte[] buffer1 = new byte[bitmapContext.Length * 4];
        int index1 = 0;
        int num1 = width << 2;
        int num2 = width << 3;
        int index2 = (height - 1) * num1;
        for (int index3 = 0; index3 < height; ++index3)
        {
          for (int index4 = 0; index4 < width; ++index4)
          {
            int num3 = pixels[index1];
            buffer1[index2] = (byte) (num3 & (int) byte.MaxValue);
            buffer1[index2 + 1] = (byte) (num3 >> 8 & (int) byte.MaxValue);
            buffer1[index2 + 2] = (byte) (num3 >> 16 & (int) byte.MaxValue);
            buffer1[index2 + 3] = (byte) (num3 >> 24);
            ++index1;
            index2 += 4;
          }
          index2 -= num2;
        }
        byte[] numArray = new byte[18];
        numArray[2] = (byte) 2;
        numArray[12] = (byte) (width & (int) byte.MaxValue);
        numArray[13] = (byte) ((width & 65280) >> 8);
        numArray[14] = (byte) (height & (int) byte.MaxValue);
        numArray[15] = (byte) ((height & 65280) >> 8);
        numArray[16] = (byte) 32;
        byte[] buffer2 = numArray;
        using (BinaryWriter binaryWriter = new BinaryWriter(destination))
        {
          binaryWriter.Write(buffer2);
          binaryWriter.Write(buffer1);
        }
      }
    }

    public static WriteableBitmap FromResource(
      this WriteableBitmap bmp,
      string relativePath)
    {
      string name = new AssemblyName(Assembly.GetCallingAssembly().FullName).Name;
      return bmp.FromContent(name + ";component/" + relativePath);
    }

    public static WriteableBitmap FromContent(
      this WriteableBitmap bmp,
      string relativePath)
    {
      using (Stream stream = Application.GetResourceStream(new Uri(relativePath, UriKind.Relative)).Stream)
        return bmp.FromStream(stream);
    }

    public static WriteableBitmap FromStream(this WriteableBitmap bmp, Stream stream)
    {
      BitmapImage source = new BitmapImage();
      source.StreamSource = stream;
      bmp = new WriteableBitmap((BitmapSource) source);
      source.UriSource = (Uri) null;
      return bmp;
    }

    private static int ConvertColor(Color color)
    {
      int num = (int) color.A + 1;
      return (int) color.A << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
    }

    public static unsafe void Clear(this WriteableBitmap bmp, Color color)
    {
      int num1 = WriteableBitmapExtensions.ConvertColor(color);
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int* pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int num2 = width * 4;
        for (int index = 0; index < width; ++index)
          pixels[index] = num1;
        int num3 = 1;
        int num4 = 1;
        while (num4 < height)
        {
          BitmapContext.BlockCopy(bitmapContext, 0, bitmapContext, num4 * num2, num3 * num2);
          num4 += num3;
          num3 = Math.Min(2 * num3, height - num4);
        }
      }
    }

    public static void Clear(this WriteableBitmap bmp)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Clear();
    }

    public static WriteableBitmap Clone(this WriteableBitmap bmp)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext(ReadWriteMode.ReadOnly))
      {
        WriteableBitmap bmp1 = BitmapFactory.New(bitmapContext1.Width, bitmapContext1.Height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
          BitmapContext.BlockCopy(bitmapContext1, 0, bitmapContext2, 0, bitmapContext1.Length * 4);
        return bmp1;
      }
    }

    public static unsafe void ForEach(this WriteableBitmap bmp, Func<int, int, Color> func)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int* pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int num = 0;
        for (int index1 = 0; index1 < height; ++index1)
        {
          for (int index2 = 0; index2 < width; ++index2)
          {
            Color color = func(index2, index1);
            pixels[num++] = WriteableBitmapExtensions.ConvertColor(color);
          }
        }
      }
    }

    public static unsafe void ForEach(this WriteableBitmap bmp, Func<int, int, Color, Color> func)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int* pixels = bitmapContext.Pixels;
        int width = bitmapContext.Width;
        int height = bitmapContext.Height;
        int index1 = 0;
        for (int index2 = 0; index2 < height; ++index2)
        {
          for (int index3 = 0; index3 < width; ++index3)
          {
            int num1 = pixels[index1];
            byte a = (byte) (num1 >> 24);
            int num2 = (int) a;
            if (num2 == 0)
              num2 = 1;
            int num3 = 65280 / num2;
            Color color1 = Color.FromArgb(a, (byte) ((num1 >> 16 & (int) byte.MaxValue) * num3 >> 8), (byte) ((num1 >> 8 & (int) byte.MaxValue) * num3 >> 8), (byte) ((num1 & (int) byte.MaxValue) * num3 >> 8));
            Color color2 = func(index3, index2, color1);
            pixels[index1++] = WriteableBitmapExtensions.ConvertColor(color2);
          }
        }
      }
    }

    public static unsafe int GetPixeli(this WriteableBitmap bmp, int x, int y)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        return bitmapContext.Pixels[y * bitmapContext.Width + x];
    }

    public static unsafe Color GetPixel(this WriteableBitmap bmp, int x, int y)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int num1 = bitmapContext.Pixels[y * bitmapContext.Width + x];
        byte a = (byte) (num1 >> 24);
        int num2 = (int) a;
        if (num2 == 0)
          num2 = 1;
        int num3 = 65280 / num2;
        return Color.FromArgb(a, (byte) ((num1 >> 16 & (int) byte.MaxValue) * num3 >> 8), (byte) ((num1 >> 8 & (int) byte.MaxValue) * num3 >> 8), (byte) ((num1 & (int) byte.MaxValue) * num3 >> 8));
      }
    }

    public static unsafe byte GetBrightness(this WriteableBitmap bmp, int x, int y)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext(ReadWriteMode.ReadOnly))
      {
        int num = bitmapContext.Pixels[y * bitmapContext.Width + x];
        return (byte) ((int) (byte) (num >> 16) * 6966 + (int) (byte) (num >> 8) * 23436 + (int) (byte) num * 2366 >> 15);
      }
    }

    public static unsafe void SetPixeli(
      this WriteableBitmap bmp,
      int index,
      byte r,
      byte g,
      byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = -16777216 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    public static unsafe void SetPixel(
      this WriteableBitmap bmp,
      int x,
      int y,
      byte r,
      byte g,
      byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = -16777216 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    public static unsafe void SetPixeli(
      this WriteableBitmap bmp,
      int index,
      byte a,
      byte r,
      byte g,
      byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = (int) a << 24 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    public static unsafe void SetPixel(
      this WriteableBitmap bmp,
      int x,
      int y,
      byte a,
      byte r,
      byte g,
      byte b)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = (int) a << 24 | (int) r << 16 | (int) g << 8 | (int) b;
    }

    public static unsafe void SetPixeli(this WriteableBitmap bmp, int index, Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = WriteableBitmapExtensions.ConvertColor(color);
    }

    public static unsafe void SetPixel(this WriteableBitmap bmp, int x, int y, Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = WriteableBitmapExtensions.ConvertColor(color);
    }

    public static unsafe void SetPixeli(this WriteableBitmap bmp, int index, byte a, Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int num = (int) a + 1;
        bitmapContext.Pixels[index] = (int) a << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
      }
    }

    public static unsafe void SetPixel(
      this WriteableBitmap bmp,
      int x,
      int y,
      byte a,
      Color color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
      {
        int num = (int) a + 1;
        bitmapContext.Pixels[y * bitmapContext.Width + x] = (int) a << 24 | (int) (byte) ((int) color.R * num >> 8) << 16 | (int) (byte) ((int) color.G * num >> 8) << 8 | (int) (byte) ((int) color.B * num >> 8);
      }
    }

    public static unsafe void SetPixeli(this WriteableBitmap bmp, int index, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[index] = color;
    }

    public static unsafe void SetPixel(this WriteableBitmap bmp, int x, int y, int color)
    {
      using (BitmapContext bitmapContext = bmp.GetBitmapContext())
        bitmapContext.Pixels[y * bitmapContext.Width + x] = color;
    }

    public static void Blit(
      this WriteableBitmap bmp,
      Rect destRect,
      WriteableBitmap source,
      Rect sourceRect,
      BlendMode BlendMode)
    {
      bmp.Blit(destRect, source, sourceRect, Colors.White, BlendMode);
    }

    public static void Blit(
      this WriteableBitmap bmp,
      Rect destRect,
      WriteableBitmap source,
      Rect sourceRect)
    {
      bmp.Blit(destRect, source, sourceRect, Colors.White, BlendMode.Alpha);
    }

    public static void Blit(
      this WriteableBitmap bmp,
      Point destPosition,
      WriteableBitmap source,
      Rect sourceRect,
      Color color,
      BlendMode BlendMode)
    {
      Rect destRect = new Rect(destPosition, new Size(sourceRect.Width, sourceRect.Height));
      bmp.Blit(destRect, source, sourceRect, color, BlendMode);
    }

    public static unsafe void Blit(
      this WriteableBitmap bmp,
      Rect destRect,
      WriteableBitmap source,
      Rect sourceRect,
      Color color,
      BlendMode BlendMode)
    {
      if (color.A == (byte) 0)
        return;
      int width1 = (int) destRect.Width;
      int height1 = (int) destRect.Height;
      using (BitmapContext bitmapContext1 = source.GetBitmapContext())
      {
        using (BitmapContext bitmapContext2 = bmp.GetBitmapContext())
        {
          int width2 = bitmapContext1.Width;
          int width3 = bitmapContext2.Width;
          int height2 = bitmapContext2.Height;
          Rect rect = new Rect(0.0, 0.0, (double) width3, (double) height2);
          rect.Intersect(destRect);
          if (rect.IsEmpty)
            return;
          int* pixels1 = bitmapContext1.Pixels;
          int* pixels2 = bitmapContext2.Pixels;
          int length1 = bitmapContext1.Length;
          int length2 = bitmapContext2.Length;
          int x1 = (int) destRect.X;
          int y1 = (int) destRect.Y;
          int num1 = 0;
          int num2 = 0;
          int num3 = 0;
          int num4 = 0;
          int a = (int) color.A;
          int r = (int) color.R;
          int g = (int) color.G;
          int b = (int) color.B;
          bool flag = color != Colors.White;
          int width4 = (int) sourceRect.Width;
          double num5 = sourceRect.Width / destRect.Width;
          double num6 = sourceRect.Height / destRect.Height;
          int x2 = (int) sourceRect.X;
          int y2 = (int) sourceRect.Y;
          int num7 = -1;
          int num8 = -1;
          double num9 = (double) y2;
          int num10 = y1;
          for (int index1 = 0; index1 < height1; ++index1)
          {
            if (num10 >= 0 && num10 < height2)
            {
              double num11 = (double) x2;
              int index2 = x1 + num10 * width3;
              int num12 = x1;
              int num13 = *pixels1;
              if (BlendMode == BlendMode.None && !flag)
              {
                int num14 = (int) num11 + (int) num9 * width2;
                int num15 = num12 < 0 ? -num12 : 0;
                int num16 = num12 + num15;
                int num17 = width2 - num15;
                int num18 = num16 + num17 < width3 ? num17 : width3 - num16;
                if (num18 > width4)
                  num18 = width4;
                if (num18 > width1)
                  num18 = width1;
                BitmapContext.BlockCopy(bitmapContext1, (num14 + num15) * 4, bitmapContext2, (index2 + num15) * 4, num18 * 4);
              }
              else
              {
                for (int index3 = 0; index3 < width1; ++index3)
                {
                  if (num12 >= 0 && num12 < width3)
                  {
                    if ((int) num11 != num7 || (int) num9 != num8)
                    {
                      int index4 = (int) num11 + (int) num9 * width2;
                      if (index4 >= 0 && index4 < length1)
                      {
                        num13 = pixels1[index4];
                        num4 = num13 >> 24 & (int) byte.MaxValue;
                        num1 = num13 >> 16 & (int) byte.MaxValue;
                        num2 = num13 >> 8 & (int) byte.MaxValue;
                        num3 = num13 & (int) byte.MaxValue;
                        if (flag && num4 != 0)
                        {
                          num4 = num4 * a * 32897 >> 23;
                          num1 = (num1 * r * 32897 >> 23) * a * 32897 >> 23;
                          num2 = (num2 * g * 32897 >> 23) * a * 32897 >> 23;
                          num3 = (num3 * b * 32897 >> 23) * a * 32897 >> 23;
                          num13 = num4 << 24 | num1 << 16 | num2 << 8 | num3;
                        }
                      }
                      else
                        num4 = 0;
                    }
                    switch (BlendMode)
                    {
                      case BlendMode.Mask:
                        int num19 = pixels2[index2];
                        int num20 = num19 >> 24 & (int) byte.MaxValue;
                        int num21 = num19 >> 16 & (int) byte.MaxValue;
                        int num22 = num19 >> 8 & (int) byte.MaxValue;
                        int num23 = num19 & (int) byte.MaxValue;
                        int num24 = num20 * num4 * 32897 >> 23 << 24 | num21 * num4 * 32897 >> 23 << 16 | num22 * num4 * 32897 >> 23 << 8 | num23 * num4 * 32897 >> 23;
                        pixels2[index2] = num24;
                        break;
                      case BlendMode.ColorKeying:
                        num1 = num13 >> 16 & (int) byte.MaxValue;
                        num2 = num13 >> 8 & (int) byte.MaxValue;
                        num3 = num13 & (int) byte.MaxValue;
                        if (num1 != (int) color.R || num2 != (int) color.G || num3 != (int) color.B)
                        {
                          pixels2[index2] = num13;
                          break;
                        }
                        break;
                      case BlendMode.None:
                        pixels2[index2] = num13;
                        break;
                      default:
                        if (num4 > 0)
                        {
                          int num25 = pixels2[index2];
                          int num26 = num25 >> 24 & (int) byte.MaxValue;
                          if ((num4 == (int) byte.MaxValue || num26 == 0) && BlendMode != BlendMode.Additive && BlendMode != BlendMode.Subtractive && BlendMode != BlendMode.Multiply)
                          {
                            pixels2[index2] = num13;
                            break;
                          }
                          int num27 = num25 >> 16 & (int) byte.MaxValue;
                          int num28 = num25 >> 8 & (int) byte.MaxValue;
                          int num29 = num25 & (int) byte.MaxValue;
                          switch (BlendMode)
                          {
                            case BlendMode.Alpha:
                              num25 = num4 + (num26 * ((int) byte.MaxValue - num4) * 32897 >> 23) << 24 | num1 + (num27 * ((int) byte.MaxValue - num4) * 32897 >> 23) << 16 | num2 + (num28 * ((int) byte.MaxValue - num4) * 32897 >> 23) << 8 | num3 + (num29 * ((int) byte.MaxValue - num4) * 32897 >> 23);
                              break;
                            case BlendMode.Additive:
                              int num30 = (int) byte.MaxValue <= num4 + num26 ? (int) byte.MaxValue : num4 + num26;
                              num25 = num30 << 24 | (num30 <= num1 + num27 ? num30 : num1 + num27) << 16 | (num30 <= num2 + num28 ? num30 : num2 + num28) << 8 | (num30 <= num3 + num29 ? num30 : num3 + num29);
                              break;
                            case BlendMode.Subtractive:
                              num25 = num26 << 24 | (num1 >= num27 ? 0 : num1 - num27) << 16 | (num2 >= num28 ? 0 : num2 - num28) << 8 | (num3 >= num29 ? 0 : num3 - num29);
                              break;
                            case BlendMode.Multiply:
                              int num31 = num4 * num26 + 128;
                              int num32 = num1 * num27 + 128;
                              int num33 = num2 * num28 + 128;
                              int num34 = num3 * num29 + 128;
                              int num35 = (num31 >> 8) + num31 >> 8;
                              int num36 = (num32 >> 8) + num32 >> 8;
                              int num37 = (num33 >> 8) + num33 >> 8;
                              int num38 = (num34 >> 8) + num34 >> 8;
                              num25 = num35 << 24 | (num35 <= num36 ? num35 : num36) << 16 | (num35 <= num37 ? num35 : num37) << 8 | (num35 <= num38 ? num35 : num38);
                              break;
                          }
                          pixels2[index2] = num25;
                          break;
                        }
                        break;
                    }
                  }
                  ++num12;
                  ++index2;
                  num11 += num5;
                }
              }
            }
            num9 += num6;
            ++num10;
          }
        }
      }
    }

    public static WriteableBitmap Crop(
      this WriteableBitmap bmp,
      int x,
      int y,
      int width,
      int height)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width1 = bitmapContext1.Width;
        int height1 = bitmapContext1.Height;
        if (x > width1 || y > height1)
          return BitmapFactory.New(0, 0);
        if (x < 0)
          x = 0;
        if (x + width > width1)
          width = width1 - x;
        if (y < 0)
          y = 0;
        if (y + height > height1)
          height = height1 - y;
        WriteableBitmap bmp1 = BitmapFactory.New(width, height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
        {
          for (int index = 0; index < height; ++index)
          {
            int srcOffset = ((y + index) * width1 + x) * 4;
            int destOffset = index * width * 4;
            BitmapContext.BlockCopy(bitmapContext1, srcOffset, bitmapContext2, destOffset, width * 4);
          }
          return bmp1;
        }
      }
    }

    public static WriteableBitmap Crop(this WriteableBitmap bmp, Rect region) => bmp.Crop((int) region.X, (int) region.Y, (int) region.Width, (int) region.Height);

    public static WriteableBitmap Resize(
      this WriteableBitmap bmp,
      int width,
      int height,
      WriteableBitmapExtensions.Interpolation interpolation)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int[] src = WriteableBitmapExtensions.Resize(bitmapContext1, bitmapContext1.Width, bitmapContext1.Height, width, height, interpolation);
        WriteableBitmap bmp1 = BitmapFactory.New(width, height);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
          BitmapContext.BlockCopy(src, 0, bitmapContext2, 0, 4 * src.Length);
        return bmp1;
      }
    }

    public static unsafe int[] Resize(
      BitmapContext srcContext,
      int widthSource,
      int heightSource,
      int width,
      int height,
      WriteableBitmapExtensions.Interpolation interpolation)
    {
      int* pixels = srcContext.Pixels;
      int[] numArray = new int[width * height];
      float num1 = (float) widthSource / (float) width;
      float num2 = (float) heightSource / (float) height;
      switch (interpolation)
      {
        case WriteableBitmapExtensions.Interpolation.NearestNeighbor:
          int num3 = 0;
          for (int index1 = 0; index1 < height; ++index1)
          {
            for (int index2 = 0; index2 < width; ++index2)
            {
              float num4 = (float) index2 * num1;
              float num5 = (float) index1 * num2;
              int num6 = (int) num4;
              int num7 = (int) num5;
              numArray[num3++] = pixels[num7 * widthSource + num6];
            }
          }
          break;
        case WriteableBitmapExtensions.Interpolation.Bilinear:
          int num8 = 0;
          for (int index3 = 0; index3 < height; ++index3)
          {
            for (int index4 = 0; index4 < width; ++index4)
            {
              float num9 = (float) index4 * num1;
              float num10 = (float) index3 * num2;
              int num11 = (int) num9;
              int num12 = (int) num10;
              float num13 = num9 - (float) num11;
              float num14 = num10 - (float) num12;
              float num15 = 1f - num13;
              float num16 = 1f - num14;
              int num17 = num11 + 1;
              if (num17 >= widthSource)
                num17 = num11;
              int num18 = num12 + 1;
              if (num18 >= heightSource)
                num18 = num12;
              int num19 = pixels[num12 * widthSource + num11];
              byte num20 = (byte) (num19 >> 24);
              byte num21 = (byte) (num19 >> 16);
              byte num22 = (byte) (num19 >> 8);
              byte num23 = (byte) num19;
              int num24 = pixels[num12 * widthSource + num17];
              byte num25 = (byte) (num24 >> 24);
              byte num26 = (byte) (num24 >> 16);
              byte num27 = (byte) (num24 >> 8);
              byte num28 = (byte) num24;
              int num29 = pixels[num18 * widthSource + num11];
              byte num30 = (byte) (num29 >> 24);
              byte num31 = (byte) (num29 >> 16);
              byte num32 = (byte) (num29 >> 8);
              byte num33 = (byte) num29;
              int num34 = pixels[num18 * widthSource + num17];
              byte num35 = (byte) (num34 >> 24);
              byte num36 = (byte) (num34 >> 16);
              byte num37 = (byte) (num34 >> 8);
              byte num38 = (byte) num34;
              float num39 = (float) ((double) num15 * (double) num20 + (double) num13 * (double) num25);
              float num40 = (float) ((double) num15 * (double) num30 + (double) num13 * (double) num35);
              byte num41 = (byte) ((double) num16 * (double) num39 + (double) num14 * (double) num40);
              float num42 = (float) ((double) num15 * (double) num21 * (double) num20 + (double) num13 * (double) num26 * (double) num25);
              float num43 = (float) ((double) num15 * (double) num31 * (double) num30 + (double) num13 * (double) num36 * (double) num35);
              float num44 = (float) ((double) num16 * (double) num42 + (double) num14 * (double) num43);
              float num45 = (float) ((double) num15 * (double) num22 * (double) num20 + (double) num13 * (double) num27 * (double) num25);
              float num46 = (float) ((double) num15 * (double) num32 * (double) num30 + (double) num13 * (double) num37 * (double) num35);
              float num47 = (float) ((double) num16 * (double) num45 + (double) num14 * (double) num46);
              float num48 = (float) ((double) num15 * (double) num23 * (double) num20 + (double) num13 * (double) num28 * (double) num25);
              float num49 = (float) ((double) num15 * (double) num33 * (double) num30 + (double) num13 * (double) num38 * (double) num35);
              float num50 = (float) ((double) num16 * (double) num48 + (double) num14 * (double) num49);
              if (num41 > (byte) 0)
              {
                num44 /= (float) num41;
                num47 /= (float) num41;
                num50 /= (float) num41;
              }
              byte num51 = (byte) num44;
              byte num52 = (byte) num47;
              byte num53 = (byte) num50;
              numArray[num8++] = (int) num41 << 24 | (int) num51 << 16 | (int) num52 << 8 | (int) num53;
            }
          }
          break;
      }
      return numArray;
    }

    public static unsafe WriteableBitmap Rotate(this WriteableBitmap bmp, int angle)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width = bitmapContext1.Width;
        int height = bitmapContext1.Height;
        int* pixels1 = bitmapContext1.Pixels;
        int index1 = 0;
        angle %= 360;
        WriteableBitmap bmp1;
        if (angle > 0 && angle <= 90)
        {
          bmp1 = BitmapFactory.New(height, width);
          using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
          {
            int* pixels2 = bitmapContext2.Pixels;
            for (int index2 = 0; index2 < width; ++index2)
            {
              for (int index3 = height - 1; index3 >= 0; --index3)
              {
                int index4 = index3 * width + index2;
                pixels2[index1] = pixels1[index4];
                ++index1;
              }
            }
          }
        }
        else if (angle > 90 && angle <= 180)
        {
          bmp1 = BitmapFactory.New(width, height);
          using (BitmapContext bitmapContext3 = bmp1.GetBitmapContext())
          {
            int* pixels3 = bitmapContext3.Pixels;
            for (int index5 = height - 1; index5 >= 0; --index5)
            {
              for (int index6 = width - 1; index6 >= 0; --index6)
              {
                int index7 = index5 * width + index6;
                pixels3[index1] = pixels1[index7];
                ++index1;
              }
            }
          }
        }
        else if (angle > 180 && angle <= 270)
        {
          bmp1 = BitmapFactory.New(height, width);
          using (BitmapContext bitmapContext4 = bmp1.GetBitmapContext())
          {
            int* pixels4 = bitmapContext4.Pixels;
            for (int index8 = width - 1; index8 >= 0; --index8)
            {
              for (int index9 = 0; index9 < height; ++index9)
              {
                int index10 = index9 * width + index8;
                pixels4[index1] = pixels1[index10];
                ++index1;
              }
            }
          }
        }
        else
          bmp1 = bmp.Clone();
        return bmp1;
      }
    }

    public static unsafe WriteableBitmap RotateFree(
      this WriteableBitmap bmp,
      double angle,
      bool crop = true)
    {
      double num1 = -1.0 * Math.PI / 180.0 * angle;
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width1 = bitmapContext1.Width;
        int height = bitmapContext1.Height;
        int pixelWidth;
        int pixelHeight;
        if (crop)
        {
          pixelWidth = width1;
          pixelHeight = height;
        }
        else
        {
          double num2 = angle / (180.0 / Math.PI);
          pixelWidth = (int) Math.Ceiling(Math.Abs(Math.Sin(num2) * (double) height) + Math.Abs(Math.Cos(num2) * (double) width1));
          pixelHeight = (int) Math.Ceiling(Math.Abs(Math.Sin(num2) * (double) width1) + Math.Abs(Math.Cos(num2) * (double) height));
        }
        int num3 = width1 / 2;
        int num4 = height / 2;
        int num5 = pixelWidth / 2;
        int num6 = pixelHeight / 2;
        WriteableBitmap bmp1 = BitmapFactory.New(pixelWidth, pixelHeight);
        using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
        {
          int* pixels1 = bitmapContext2.Pixels;
          int* pixels2 = bitmapContext1.Pixels;
          int width2 = bitmapContext1.Width;
          for (int index1 = 0; index1 < pixelHeight; ++index1)
          {
            for (int index2 = 0; index2 < pixelWidth; ++index2)
            {
              int x = index2 - num5;
              int y = num6 - index1;
              double num7 = Math.Sqrt((double) (x * x + y * y));
              double num8;
              if (x == 0)
              {
                if (y == 0)
                {
                  pixels1[index1 * pixelWidth + index2] = pixels2[num4 * width2 + num3];
                  continue;
                }
                num8 = y >= 0 ? Math.PI / 2.0 : 3.0 * Math.PI / 2.0;
              }
              else
                num8 = Math.Atan2((double) y, (double) x);
              double num9 = num8 - num1;
              double num10 = num7 * Math.Cos(num9);
              double num11 = num7 * Math.Sin(num9);
              double num12 = num10 + (double) num3;
              double num13 = (double) num4 - num11;
              int num14 = (int) Math.Floor(num12);
              int num15 = (int) Math.Floor(num13);
              int num16 = (int) Math.Ceiling(num12);
              int num17 = (int) Math.Ceiling(num13);
              if (num14 >= 0 && num16 >= 0 && num14 < width1 && num16 < width1 && num15 >= 0 && num17 >= 0 && num15 < height && num17 < height)
              {
                double num18 = num12 - (double) num14;
                double num19 = num13 - (double) num15;
                int num20 = pixels2[num15 * width2 + num14];
                int num21 = pixels2[num15 * width2 + num16];
                int num22 = pixels2[num17 * width2 + num14];
                int num23 = pixels2[num17 * width2 + num16];
                double num24 = (1.0 - num18) * (double) (num20 >> 24 & (int) byte.MaxValue) + num18 * (double) (num21 >> 24 & (int) byte.MaxValue);
                double num25 = (1.0 - num18) * (double) (num20 >> 16 & (int) byte.MaxValue) + num18 * (double) (num21 >> 16 & (int) byte.MaxValue);
                double num26 = (1.0 - num18) * (double) (num20 >> 8 & (int) byte.MaxValue) + num18 * (double) (num21 >> 8 & (int) byte.MaxValue);
                double num27 = (1.0 - num18) * (double) (num20 & (int) byte.MaxValue) + num18 * (double) (num21 & (int) byte.MaxValue);
                double num28 = (1.0 - num18) * (double) (num22 >> 24 & (int) byte.MaxValue) + num18 * (double) (num23 >> 24 & (int) byte.MaxValue);
                double num29 = (1.0 - num18) * (double) (num22 >> 16 & (int) byte.MaxValue) + num18 * (double) (num23 >> 16 & (int) byte.MaxValue);
                double num30 = (1.0 - num18) * (double) (num22 >> 8 & (int) byte.MaxValue) + num18 * (double) (num23 >> 8 & (int) byte.MaxValue);
                double num31 = (1.0 - num18) * (double) (num22 & (int) byte.MaxValue) + num18 * (double) (num23 & (int) byte.MaxValue);
                int num32 = (int) Math.Round((1.0 - num19) * num25 + num19 * num29);
                int num33 = (int) Math.Round((1.0 - num19) * num26 + num19 * num30);
                int num34 = (int) Math.Round((1.0 - num19) * num27 + num19 * num31);
                int num35 = (int) Math.Round((1.0 - num19) * num24 + num19 * num28);
                if (num32 < 0)
                  num32 = 0;
                if (num32 > (int) byte.MaxValue)
                  num32 = (int) byte.MaxValue;
                if (num33 < 0)
                  num33 = 0;
                if (num33 > (int) byte.MaxValue)
                  num33 = (int) byte.MaxValue;
                if (num34 < 0)
                  num34 = 0;
                if (num34 > (int) byte.MaxValue)
                  num34 = (int) byte.MaxValue;
                if (num35 < 0)
                  num35 = 0;
                if (num35 > (int) byte.MaxValue)
                  num35 = (int) byte.MaxValue;
                int num36 = num35 + 1;
                pixels1[index1 * pixelWidth + index2] = num35 << 24 | (int) (byte) (num32 * num36 >> 8) << 16 | (int) (byte) (num33 * num36 >> 8) << 8 | (int) (byte) (num34 * num36 >> 8);
              }
            }
          }
          return bmp1;
        }
      }
    }

    public static unsafe WriteableBitmap Flip(
      this WriteableBitmap bmp,
      WriteableBitmapExtensions.FlipMode flipMode)
    {
      using (BitmapContext bitmapContext1 = bmp.GetBitmapContext())
      {
        int width = bitmapContext1.Width;
        int height = bitmapContext1.Height;
        int* pixels1 = bitmapContext1.Pixels;
        int index1 = 0;
        WriteableBitmap bmp1 = (WriteableBitmap) null;
        switch (flipMode)
        {
          case WriteableBitmapExtensions.FlipMode.Vertical:
            bmp1 = BitmapFactory.New(width, height);
            using (BitmapContext bitmapContext2 = bmp1.GetBitmapContext())
            {
              int* pixels2 = bitmapContext2.Pixels;
              for (int index2 = 0; index2 < height; ++index2)
              {
                for (int index3 = width - 1; index3 >= 0; --index3)
                {
                  int index4 = index2 * width + index3;
                  pixels2[index1] = pixels1[index4];
                  ++index1;
                }
              }
              break;
            }
          case WriteableBitmapExtensions.FlipMode.Horizontal:
            bmp1 = BitmapFactory.New(width, height);
            using (BitmapContext bitmapContext3 = bmp1.GetBitmapContext())
            {
              int* pixels3 = bitmapContext3.Pixels;
              for (int index5 = height - 1; index5 >= 0; --index5)
              {
                for (int index6 = 0; index6 < width; ++index6)
                {
                  int index7 = index5 * width + index6;
                  pixels3[index1] = pixels1[index7];
                  ++index1;
                }
              }
              break;
            }
        }
        return bmp1;
      }
    }

    public enum Interpolation
    {
      NearestNeighbor,
      Bilinear,
    }

    public enum FlipMode
    {
      Vertical,
      Horizontal,
    }
  }
}
