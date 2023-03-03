// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Provider.EpsProvider
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System.Collections.Generic;
using System.Windows.Media;

namespace MessagingToolkit.Barcode.Provider
{
  public sealed class EpsProvider : IOutputProvider<Eps>
  {
    public Color Foreground { get; set; }

    public Color Background { get; set; }

    public EpsProvider()
    {
      this.Foreground = Colors.Black;
      this.Background = Colors.White;
    }

    public Eps Generate(BitMatrix bitMatrix, BarcodeFormat format, string content)
    {
      Eps image = new Eps();
      this.Create(image, bitMatrix, format, content, (Dictionary<EncodeOptions, object>) null);
      return image;
    }

    public Eps Generate(
      BitMatrix bitMatrix,
      BarcodeFormat format,
      string content,
      Dictionary<EncodeOptions, object> encodingOptions)
    {
      Eps image = new Eps();
      this.Create(image, bitMatrix, format, content, encodingOptions);
      return image;
    }

    private void Create(
      Eps image,
      BitMatrix matrix,
      BarcodeFormat format,
      string content,
      Dictionary<EncodeOptions, object> options)
    {
      if (matrix == null)
        return;
      int width = matrix.Width;
      int height = matrix.Height;
      image.AddHeader((double) width, (double) height);
      image.SetForeground(this.Foreground);
      image.AddDef(matrix);
      image.AddEnd();
    }
  }
}
