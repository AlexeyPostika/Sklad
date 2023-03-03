// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Helper.MatrixToImageHelper
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessagingToolkit.Barcode.Helper
{
  public sealed class MatrixToImageHelper
  {
    private MatrixToImageHelper()
    {
    }

    public static void SetPixel(WriteableBitmap bmp, int x, int y, Color color)
    {
      int a = (int) color.A;
      bmp.SetPixeli(y * bmp.PixelWidth + x, color);
    }

    private static WriteableBitmap RenderBitmap(
      BitMatrix matrix,
      Color foreColor,
      Color backColor)
    {
      int width = matrix.Width;
      int height = matrix.Height;
      WriteableBitmap bmp = BitmapFactory.New(width, height);
      using (bmp.GetBitmapContext())
      {
        for (int x = 0; x < width; ++x)
        {
          for (int y = 0; y < height; ++y)
          {
            Color color = matrix.Get(x, y) ? foreColor : backColor;
            MatrixToImageHelper.SetPixel(bmp, x, y, color);
          }
        }
      }
      return bmp;
    }

    public static WriteableBitmap ToBitmap(BitMatrix matrix) => MatrixToImageHelper.RenderBitmap(matrix, Colors.Black, Colors.White);

    public static WriteableBitmap ToBitmap(
      BitMatrix matrix,
      Color foreColor,
      Color backColor)
    {
      return MatrixToImageHelper.RenderBitmap(matrix, foreColor, backColor);
    }
  }
}
