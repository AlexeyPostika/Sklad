// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Helper.WriteableBitmapContextExtensions
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode.Helper
{
  public static class WriteableBitmapContextExtensions
  {
    public static BitmapContext GetBitmapContext(this WriteableBitmap bmp) => new BitmapContext(bmp);

    public static BitmapContext GetBitmapContext(
      this WriteableBitmap bmp,
      ReadWriteMode mode)
    {
      return new BitmapContext(bmp, mode);
    }
  }
}
