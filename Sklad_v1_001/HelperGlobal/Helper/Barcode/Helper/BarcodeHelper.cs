// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Helper.BarcodeHelper
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode.Helper
{
  internal sealed class BarcodeHelper
  {
    public static int URShift(int number, int bits) => number >= 0 ? number >> bits : (number >> bits) + (2 << ~bits);

    public static object GetDecodeOptionType(
      Dictionary<DecodeOptions, object> dict,
      DecodeOptions key)
    {
      try
      {
        if (dict == null)
          return (object) null;
        object obj = (object) null;
        return dict.TryGetValue(key, out obj) ? obj : (object) null;
      }
      catch
      {
        return (object) null;
      }
    }

    public static object GetEncodeOptionType(
      Dictionary<EncodeOptions, object> dict,
      EncodeOptions key)
    {
      try
      {
        if (dict == null)
          return (object) null;
        object obj = (object) null;
        return dict.TryGetValue(key, out obj) ? obj : (object) null;
      }
      catch
      {
        return (object) null;
      }
    }

    public static string Join<T>(string separator, T[] values) => string.Join(",", ((IEnumerable<T>) values).Select<T, string>((Func<T, string>) (x => x.ToString())).ToArray<string>());

    public static unsafe byte[] ImageToByteArray(WriteableBitmap bmp)
    {
      int pixelWidth = bmp.PixelWidth;
      int pixelHeight = bmp.PixelHeight;
      BitmapContext bitmapContext = new BitmapContext(bmp);
      int* pixels = bitmapContext.Pixels;
      int length = bitmapContext.Length;
      byte[] byteArray = new byte[4 * pixelWidth * pixelHeight];
      int index1 = 0;
      int index2 = 0;
      while (index1 < length)
      {
        int num = pixels[index1];
        byteArray[index2] = (byte) (num >> 24);
        byteArray[index2 + 1] = (byte) (num >> 16);
        byteArray[index2 + 2] = (byte) (num >> 8);
        byteArray[index2 + 3] = (byte) num;
        ++index1;
        index2 += 4;
      }
      return byteArray;
    }

    public static BitmapSource ToBitmapSource(Bitmap source)
    {
      IntPtr hbitmap = source.GetHbitmap();
      try
      {
        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
      }
      catch
      {
        return (BitmapSource) null;
      }
      finally
      {
        NativeMethods.DeleteObject(hbitmap);
      }
    }

    public static System.Drawing.Color ToWinFormsColor(System.Windows.Media.Color color) => System.Drawing.Color.FromArgb((int) color.A, (int) color.R, (int) color.G, (int) color.B);

    public static Bitmap ToWinFormsBitmap(BitmapSource bitmapsource)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        BitmapEncoder bitmapEncoder = (BitmapEncoder) new BmpBitmapEncoder();
        bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapsource));
        bitmapEncoder.Save((Stream) memoryStream);
        using (Bitmap original = new Bitmap((Stream) memoryStream))
          return new Bitmap((Image) original);
      }
    }

    public static WriteableBitmap ByteArrayToImage(byte[] imageData) => BitmapFactory.New(250, 250).FromByteArray(imageData, imageData.Length);
  }
}
