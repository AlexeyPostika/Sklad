// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Provider.SvgProvider
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;
using System.Windows.Media;

namespace MessagingToolkit.Barcode.Provider
{
  public sealed class SvgProvider : IOutputProvider<Svg>
  {
    public Color Foreground { get; set; }

    public Color Background { get; set; }

    public SvgProvider()
    {
      this.Foreground = Colors.Black;
      this.Background = Colors.White;
    }

    private void Create(
      Svg image,
      BitMatrix matrix,
      BarcodeFormat format,
      string content,
      Dictionary<EncodeOptions, object> options)
    {
      if (matrix == null)
        return;
      int width = matrix.Width;
      int height = matrix.Height;
      image.AddHeader();
      image.AddTag(0, 0, 10 + width, 10 + height, this.Background, this.Foreground);
      SvgProvider.AppendDarkCell(image, matrix, 5, 5);
      image.AddEnd();
    }

    private static void AppendDarkCell(Svg image, BitMatrix matrix, int offsetX, int offSetY)
    {
      if (matrix == null)
        return;
      int width = matrix.Width;
      int height = matrix.Height;
      BitMatrix processed = new BitMatrix(width, height);
      bool flag = false;
      int startPosX = 0;
      int startPosY = 0;
      for (int x = 0; x < width; ++x)
      {
        int endPosX;
        for (int index = 0; index < height; ++index)
        {
          if (!processed.Get(x, index))
          {
            processed.Set(x, index);
            if (matrix.Get(x, index))
            {
              if (!flag)
              {
                startPosX = x;
                startPosY = index;
                flag = true;
              }
            }
            else if (flag)
            {
              SvgProvider.FindMaximumRectangle(matrix, processed, startPosX, startPosY, index, out endPosX);
              image.AddRec(startPosX + offsetX, startPosY + offSetY, endPosX - startPosX + 1, index - startPosY);
              flag = false;
            }
          }
        }
        if (flag)
        {
          SvgProvider.FindMaximumRectangle(matrix, processed, startPosX, startPosY, height, out endPosX);
          image.AddRec(startPosX + offsetX, startPosY + offSetY, endPosX - startPosX + 1, height - startPosY);
          flag = false;
        }
      }
    }

    private static void FindMaximumRectangle(
      BitMatrix matrix,
      BitMatrix processed,
      int startPosX,
      int startPosY,
      int endPosY,
      out int endPosX)
    {
      endPosX = startPosX + 1;
      for (int x = startPosX + 1; x < matrix.Width; ++x)
      {
        for (int y = startPosY; y < endPosY; ++y)
        {
          if (!matrix.Get(x, y))
            return;
        }
        endPosX = x;
        for (int y = startPosY; y < endPosY; ++y)
          processed.Set(x, y);
      }
    }

    public Svg Generate(BitMatrix bitMatrix, BarcodeFormat format, string content) => this.Generate(bitMatrix, format, content, (Dictionary<EncodeOptions, object>) null);

    public Svg Generate(
      BitMatrix bitMatrix,
      BarcodeFormat format,
      string content,
      Dictionary<EncodeOptions, object> options)
    {
      Svg image = new Svg();
      this.Create(image, bitMatrix, format, content, options);
      return image;
    }
  }
}
