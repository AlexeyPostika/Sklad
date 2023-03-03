// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Provider.Svg
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Globalization;
using System.Text;
using System.Windows.Media;

namespace MessagingToolkit.Barcode.Provider
{
  public sealed class Svg
  {
    private readonly StringBuilder content;

    public string Content
    {
      get => this.content.ToString();
      set
      {
        this.content.Length = 0;
        if (value == null)
          return;
        this.content.Append(value);
      }
    }

    public Svg() => this.content = new StringBuilder();

    public Svg(string content) => this.content = new StringBuilder(content);

    public override string ToString() => this.content.ToString();

    internal void AddHeader()
    {
      this.content.Append("<?xml version=\"1.0\" standalone=\"no\"?>");
      this.content.Append("<!-- Created with MessagingToolkit Barcode Library (http://platform.twit88.com/projects/mt-barcode) -->");
      this.content.Append("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
    }

    internal void AddEnd() => this.content.Append("</svg>");

    internal void AddTag(
      int displaysizeX,
      int displaysizeY,
      int viewboxSizeX,
      int viewboxSizeY,
      Color background,
      Color fill)
    {
      if (displaysizeX <= 0 || displaysizeY <= 0)
        this.content.Append(string.Format("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" viewBox=\"0 0 {0} {1}\" viewport-fill=\"rgb({2})\" viewport-fill-opacity=\"{3}\" fill=\"rgb({4})\" fill-opacity=\"{5}\" {6}>", (object) viewboxSizeX, (object) viewboxSizeY, (object) Svg.GetColorRgb(background), (object) Svg.ConvertAlpha(background), (object) Svg.GetColorRgb(fill), (object) Svg.ConvertAlpha(fill), (object) Svg.GetBackgroundStyle(background)));
      else
        this.content.Append(string.Format("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.2\" baseProfile=\"tiny\" viewBox=\"0 0 {0} {1}\" viewport-fill=\"rgb({2})\" viewport-fill-opacity=\"{3}\" fill=\"rgb({4})\" fill-opacity=\"{5}\" {6} width=\"{7}\" height=\"{8}\">", (object) viewboxSizeX, (object) viewboxSizeY, (object) Svg.GetColorRgb(background), (object) Svg.ConvertAlpha(background), (object) Svg.GetColorRgb(fill), (object) Svg.ConvertAlpha(fill), (object) Svg.GetBackgroundStyle(background), (object) displaysizeX, (object) displaysizeY));
    }

    internal void AddRec(int posX, int posY, int width, int height) => this.content.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "<rect x=\"{0}\" y=\"{1}\" width=\"{2}\" height=\"{3}\"/>", (object) posX, (object) posY, (object) width, (object) height);

    internal static double ConvertAlpha(Color alpha) => Math.Round((double) alpha.A / (double) byte.MaxValue, 2);

    internal static string GetBackgroundStyle(Color color)
    {
      double num = Svg.ConvertAlpha(color);
      return string.Format("style=\"background-color:rgb({0},{1},{2});background-color:rgba({3});\"", (object) color.R, (object) color.G, (object) color.B, (object) num);
    }

    internal static string GetColorRgb(Color color) => color.R.ToString() + "," + (object) color.G + "," + (object) color.B;

    internal static string GetColorRgba(Color color)
    {
      double num = Svg.ConvertAlpha(color);
      return color.R.ToString() + "," + (object) color.G + "," + (object) color.B + "," + (object) num;
    }
  }
}
