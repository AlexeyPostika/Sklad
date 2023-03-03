// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.BaseColor
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Windows.Media;

namespace MessagingToolkit.Barcode.Common
{
  public class BaseColor
  {
    private const double FACTOR = 0.7;
    public static readonly BaseColor WHITE = new BaseColor((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    public static readonly BaseColor LIGHT_GRAY = new BaseColor(192, 192, 192);
    public static readonly BaseColor GRAY = new BaseColor(128, 128, 128);
    public static readonly BaseColor DARK_GRAY = new BaseColor(64, 64, 64);
    public static readonly BaseColor BLACK = new BaseColor(0, 0, 0);
    public static readonly BaseColor RED = new BaseColor((int) byte.MaxValue, 0, 0);
    public static readonly BaseColor PINK = new BaseColor((int) byte.MaxValue, 175, 175);
    public static readonly BaseColor ORANGE = new BaseColor((int) byte.MaxValue, 200, 0);
    public static readonly BaseColor YELLOW = new BaseColor((int) byte.MaxValue, (int) byte.MaxValue, 0);
    public static readonly BaseColor GREEN = new BaseColor(0, (int) byte.MaxValue, 0);
    public static readonly BaseColor MAGENTA = new BaseColor((int) byte.MaxValue, 0, (int) byte.MaxValue);
    public static readonly BaseColor CYAN = new BaseColor(0, (int) byte.MaxValue, (int) byte.MaxValue);
    public static readonly BaseColor BLUE = new BaseColor(0, 0, (int) byte.MaxValue);
    private Color color;

    public BaseColor(int red, int green, int blue) => this.color = Color.FromArgb(byte.MaxValue, (byte) red, (byte) green, (byte) blue);

    public BaseColor(int red, int green, int blue, int alpha) => this.color = Color.FromArgb((byte) alpha, (byte) red, (byte) green, (byte) blue);

    public BaseColor(float red, float green, float blue) => this.color = Color.FromArgb(byte.MaxValue, (byte) ((double) red * (double) byte.MaxValue + 0.5), (byte) ((double) green * (double) byte.MaxValue + 0.5), (byte) ((double) blue * (double) byte.MaxValue + 0.5));

    public BaseColor(float red, float green, float blue, float alpha) => this.color = Color.FromArgb((byte) ((double) alpha * (double) byte.MaxValue + 0.5), (byte) ((double) red * (double) byte.MaxValue + 0.5), (byte) ((double) green * (double) byte.MaxValue + 0.5), (byte) ((double) blue * (double) byte.MaxValue + 0.5));

    public BaseColor(Color color) => this.color = color;

    public int R => (int) this.color.R;

    public int G => (int) this.color.G;

    public int B => (int) this.color.B;

    public BaseColor Brighter()
    {
      int num1 = (int) this.color.R;
      int num2 = (int) this.color.G;
      int num3 = (int) this.color.B;
      int num4 = 3;
      if (num1 == 0 && num2 == 0 && num3 == 0)
        return new BaseColor(num4, num4, num4);
      if (num1 > 0 && num1 < num4)
        num1 = num4;
      if (num2 > 0 && num2 < num4)
        num2 = num4;
      if (num3 > 0 && num3 < num4)
        num3 = num4;
      return new BaseColor(Math.Min((int) ((double) num1 / 0.7), (int) byte.MaxValue), Math.Min((int) ((double) num2 / 0.7), (int) byte.MaxValue), Math.Min((int) ((double) num3 / 0.7), (int) byte.MaxValue));
    }

    public BaseColor Darker() => new BaseColor(Math.Max((int) ((double) this.color.R * 0.7), 0), Math.Max((int) ((double) this.color.G * 0.7), 0), Math.Max((int) ((double) this.color.B * 0.7), 0));

    public override bool Equals(object obj) => obj is BaseColor && this.color.Equals(((BaseColor) obj).color);

    public override int GetHashCode() => this.color.GetHashCode();

    public override string ToString() => this.color.ToString();
  }
}
