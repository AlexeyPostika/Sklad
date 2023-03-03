// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Helper.BitmapFactory
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode.Helper
{
  public static class BitmapFactory
  {
    public static WriteableBitmap New(int pixelWidth, int pixelHeight) => new WriteableBitmap(pixelWidth, pixelHeight, 96.0, 96.0, PixelFormats.Pbgra32, (BitmapPalette) null);

    public static WriteableBitmap ConvertToPbgra32Format(BitmapSource source)
    {
      if (source.Format == PixelFormats.Pbgra32)
        return new WriteableBitmap(source);
      FormatConvertedBitmap source1 = new FormatConvertedBitmap();
      source1.BeginInit();
      source1.Source = source;
      source1.DestinationFormat = PixelFormats.Pbgra32;
      source1.EndInit();
      return new WriteableBitmap((BitmapSource) source1);
    }
  }
}
