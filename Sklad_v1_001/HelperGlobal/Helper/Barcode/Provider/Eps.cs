// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Provider.Eps
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;
using System.Text;
using System.Windows.Media;

namespace MessagingToolkit.Barcode.Provider
{
  public sealed class Eps
  {
    private const int BlockSize = 4;
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

    public Eps() => this.content = new StringBuilder();

    public Eps(string content) => this.content = new StringBuilder(content);

    public override string ToString() => this.content.ToString();

    internal void AddHeader(double width, double height)
    {
      this.content.Append("%!PS-Adobe-3.0 EPSF-3.0\n");
      this.content.Append("%%BoundingBox: 0 0 " + (object) Math.Round(Math.Ceiling(width)) + " " + (object) Math.Round(Math.Ceiling(height)) + "\n");
      this.content.Append("%%HiResBoundingBox: 0 0 " + width.ToString("0.####") + " " + height.ToString("0.####") + "\n");
      this.content.Append("%%Creator: MessagingToolkit Barcode Library (http://platform.twit88.com/projects/mt-barcode)\n");
      this.content.Append("%%CreationDate: " + DateTime.Now.ToString("yyyy-MM-dd'T'hh:mm:ss") + "\n");
      this.content.Append("%%LanguageLevel: 1\n");
      this.content.Append("%%EndComments\n");
      this.content.Append("%%BeginProlog\n");
      this.content.Append("%%BeginProcSet: barcode-procset 1.1\n");
      this.content.Append("/rf {\n");
      this.content.Append("newpath\n");
      this.content.Append("4 -2 roll moveto\n");
      this.content.Append("dup neg 0 exch rlineto\n");
      this.content.Append("exch 0 rlineto\n");
      this.content.Append("0 neg exch rlineto\n");
      this.content.Append("closepath fill\n");
      this.content.Append("} def\n");
      this.content.Append("/ct {\n");
      this.content.Append("moveto dup stringwidth\n");
      this.content.Append("2 div neg exch 2 div neg exch\n");
      this.content.Append("rmoveto show\n");
      this.content.Append("} def\n");
      this.content.Append("/rt {\n");
      this.content.Append("4 -1 roll dup stringwidth pop\n");
      this.content.Append("5 -2 roll 1 index sub\n");
      this.content.Append("3 -1 roll sub\n");
      this.content.Append("add\n");
      this.content.Append("3 -1 roll moveto show\n");
      this.content.Append("} def\n");
      this.content.Append("/jt {\n");
      this.content.Append("4 -1 roll dup stringwidth pop\n");
      this.content.Append("5 -2 roll 1 index sub\n");
      this.content.Append("3 -1 roll sub\n");
      this.content.Append("2 index length\n");
      this.content.Append("1 sub div\n");
      this.content.Append("0 4 -1 roll 4 -1 roll 5 -1 roll\n");
      this.content.Append("moveto ashow\n");
      this.content.Append("} def\n");
      this.content.Append("%%EndProcSet: barcode-procset 1.0\n");
      this.content.Append("%%EndProlog\n");
    }

    internal void AddEnd() => this.content.Append("%%EOF\n");

    internal void AddDef(BitMatrix matrix)
    {
      this.content.Append("/bits [");
      for (int y = matrix.Height - 1; y >= 0; --y)
      {
        for (int x = 0; x < matrix.Width; ++x)
          this.content.Append(matrix.Get(x, y) ? "1 " : "0 ");
      }
      this.content.Append("] def\n");
      this.content.Append("/width " + (object) matrix.Width + " def\n");
      this.content.Append("/height " + (object) matrix.Height + " def\n");
      this.content.Append("/y 0 def\n" + (object) 1 + " " + (object) 1 + " scale\nheight {\n   /x 0 def\n   width {\n      bits y width mul x add get 1 eq {\n         newpath\n         x y moveto\n         0 1 rlineto\n         1 0 rlineto\n         0 -1 rlineto\n         closepath\n         fill\n      } if\n      /x x 1 add def\n   } repeat\n   /y y 1 add def\n} repeat\n");
    }

    internal void SetForeground(Color color) => this.content.Append(this.FormatColor(color.R) + " " + this.FormatColor(color.G) + " " + this.FormatColor(color.B) + " setrgbcolor\n");

    internal string FormatColor(byte c) => ((double) c / (double) byte.MaxValue).ToString("0.###");
  }
}
