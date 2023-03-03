// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.PerspectiveTransform
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Common
{
  public sealed class PerspectiveTransform
  {
    private readonly float a11;
    private readonly float a12;
    private readonly float a13;
    private readonly float a21;
    private readonly float a22;
    private readonly float a23;
    private readonly float a31;
    private readonly float a32;
    private readonly float a33;

    private PerspectiveTransform(
      float a11_0,
      float a21_1,
      float a31_2,
      float a12_3,
      float a22_4,
      float a32_5,
      float a13_6,
      float a23_7,
      float a33_8)
    {
      this.a11 = a11_0;
      this.a12 = a12_3;
      this.a13 = a13_6;
      this.a21 = a21_1;
      this.a22 = a22_4;
      this.a23 = a23_7;
      this.a31 = a31_2;
      this.a32 = a32_5;
      this.a33 = a33_8;
    }

    public static PerspectiveTransform QuadrilateralToQuadrilateral(
      float x0,
      float y0,
      float x1,
      float y1,
      float x2,
      float y2,
      float x3,
      float y3,
      float x0p,
      float y0p,
      float x1p,
      float y1p,
      float x2p,
      float y2p,
      float x3p,
      float y3p)
    {
      PerspectiveTransform square = PerspectiveTransform.QuadrilateralToSquare(x0, y0, x1, y1, x2, y2, x3, y3);
      return PerspectiveTransform.SquareToQuadrilateral(x0p, y0p, x1p, y1p, x2p, y2p, x3p, y3p).Times(square);
    }

    public void TransformPoints(float[] points)
    {
      int length = points.Length;
      float a11 = this.a11;
      float a12 = this.a12;
      float a13 = this.a13;
      float a21 = this.a21;
      float a22 = this.a22;
      float a23 = this.a23;
      float a31 = this.a31;
      float a32 = this.a32;
      float a33 = this.a33;
      for (int index = 0; index < length; index += 2)
      {
        float point1 = points[index];
        float point2 = points[index + 1];
        float num = (float) ((double) a13 * (double) point1 + (double) a23 * (double) point2) + a33;
        points[index] = ((float) ((double) a11 * (double) point1 + (double) a21 * (double) point2) + a31) / num;
        points[index + 1] = ((float) ((double) a12 * (double) point1 + (double) a22 * (double) point2) + a32) / num;
      }
    }

    public void TransformPoints(float[] xValues, float[] yValues)
    {
      int length = xValues.Length;
      for (int index = 0; index < length; ++index)
      {
        float xValue = xValues[index];
        float yValue = yValues[index];
        float num = (float) ((double) this.a13 * (double) xValue + (double) this.a23 * (double) yValue) + this.a33;
        xValues[index] = ((float) ((double) this.a11 * (double) xValue + (double) this.a21 * (double) yValue) + this.a31) / num;
        yValues[index] = ((float) ((double) this.a12 * (double) xValue + (double) this.a22 * (double) yValue) + this.a32) / num;
      }
    }

    public static PerspectiveTransform SquareToQuadrilateral(
      float x0,
      float y0,
      float x1,
      float y1,
      float x2,
      float y2,
      float x3,
      float y3)
    {
      float num1 = x0 - x1 + x2 - x3;
      float num2 = y0 - y1 + y2 - y3;
      if ((double) num1 == 0.0 && (double) num2 == 0.0)
        return new PerspectiveTransform(x1 - x0, x2 - x1, x0, y1 - y0, y2 - y1, y0, 0.0f, 0.0f, 1f);
      float num3 = x1 - x2;
      float num4 = x3 - x2;
      float num5 = y1 - y2;
      float num6 = y3 - y2;
      float num7 = (float) ((double) num3 * (double) num6 - (double) num4 * (double) num5);
      float a13_6 = (float) ((double) num1 * (double) num6 - (double) num4 * (double) num2) / num7;
      float a23_7 = (float) ((double) num3 * (double) num2 - (double) num1 * (double) num5) / num7;
      return new PerspectiveTransform((float) ((double) x1 - (double) x0 + (double) a13_6 * (double) x1), (float) ((double) x3 - (double) x0 + (double) a23_7 * (double) x3), x0, (float) ((double) y1 - (double) y0 + (double) a13_6 * (double) y1), (float) ((double) y3 - (double) y0 + (double) a23_7 * (double) y3), y0, a13_6, a23_7, 1f);
    }

    public static PerspectiveTransform QuadrilateralToSquare(
      float x0,
      float y0,
      float x1,
      float y1,
      float x2,
      float y2,
      float x3,
      float y3)
    {
      return PerspectiveTransform.SquareToQuadrilateral(x0, y0, x1, y1, x2, y2, x3, y3).BuildAdjoint();
    }

    internal PerspectiveTransform BuildAdjoint() => new PerspectiveTransform((float) ((double) this.a22 * (double) this.a33 - (double) this.a23 * (double) this.a32), (float) ((double) this.a23 * (double) this.a31 - (double) this.a21 * (double) this.a33), (float) ((double) this.a21 * (double) this.a32 - (double) this.a22 * (double) this.a31), (float) ((double) this.a13 * (double) this.a32 - (double) this.a12 * (double) this.a33), (float) ((double) this.a11 * (double) this.a33 - (double) this.a13 * (double) this.a31), (float) ((double) this.a12 * (double) this.a31 - (double) this.a11 * (double) this.a32), (float) ((double) this.a12 * (double) this.a23 - (double) this.a13 * (double) this.a22), (float) ((double) this.a13 * (double) this.a21 - (double) this.a11 * (double) this.a23), (float) ((double) this.a11 * (double) this.a22 - (double) this.a12 * (double) this.a21));

    internal PerspectiveTransform Times(PerspectiveTransform other) => new PerspectiveTransform((float) ((double) this.a11 * (double) other.a11 + (double) this.a21 * (double) other.a12 + (double) this.a31 * (double) other.a13), (float) ((double) this.a11 * (double) other.a21 + (double) this.a21 * (double) other.a22 + (double) this.a31 * (double) other.a23), (float) ((double) this.a11 * (double) other.a31 + (double) this.a21 * (double) other.a32 + (double) this.a31 * (double) other.a33), (float) ((double) this.a12 * (double) other.a11 + (double) this.a22 * (double) other.a12 + (double) this.a32 * (double) other.a13), (float) ((double) this.a12 * (double) other.a21 + (double) this.a22 * (double) other.a22 + (double) this.a32 * (double) other.a23), (float) ((double) this.a12 * (double) other.a31 + (double) this.a22 * (double) other.a32 + (double) this.a32 * (double) other.a33), (float) ((double) this.a13 * (double) other.a11 + (double) this.a23 * (double) other.a12 + (double) this.a33 * (double) other.a13), (float) ((double) this.a13 * (double) other.a21 + (double) this.a23 * (double) other.a22 + (double) this.a33 * (double) other.a23), (float) ((double) this.a13 * (double) other.a31 + (double) this.a23 * (double) other.a32 + (double) this.a33 * (double) other.a33));
  }
}
