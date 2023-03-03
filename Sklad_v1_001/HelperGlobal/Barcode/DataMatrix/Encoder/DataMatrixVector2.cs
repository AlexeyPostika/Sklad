// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixVector2
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixVector2
  {
    internal DataMatrixVector2()
    {
      this.X = 0.0;
      this.Y = 0.0;
    }

    internal DataMatrixVector2(double x, double y)
    {
      this.X = x;
      this.Y = y;
    }

    public static DataMatrixVector2 operator +(
      DataMatrixVector2 v1,
      DataMatrixVector2 v2)
    {
      DataMatrixVector2 dataMatrixVector2 = new DataMatrixVector2(v1.X, v1.Y);
      dataMatrixVector2.X += v2.X;
      dataMatrixVector2.Y += v2.Y;
      return dataMatrixVector2;
    }

    public static DataMatrixVector2 operator -(
      DataMatrixVector2 v1,
      DataMatrixVector2 v2)
    {
      DataMatrixVector2 dataMatrixVector2 = new DataMatrixVector2(v1.X, v1.Y);
      dataMatrixVector2.X -= v2.X;
      dataMatrixVector2.Y -= v2.Y;
      return dataMatrixVector2;
    }

    public static DataMatrixVector2 operator *(DataMatrixVector2 v1, double factor) => new DataMatrixVector2(v1.X * factor, v1.Y * factor);

    internal double Cross(DataMatrixVector2 v2) => this.X * v2.Y - this.Y * v2.X;

    internal double Norm()
    {
      double num = this.Mag();
      if (num <= DataMatrixConstants.DataMatrixAlmostZero)
        return -1.0;
      this.X /= num;
      this.Y /= num;
      return num;
    }

    internal double Dot(DataMatrixVector2 v2) => Math.Sqrt(this.X * v2.X + this.Y * v2.Y);

    internal double Mag() => Math.Sqrt(this.X * this.X + this.Y * this.Y);

    internal double DistanceFromRay2(DataMatrixRay2 ray)
    {
      if (Math.Abs(1.0 - ray.V.Mag()) > DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("DistanceFromRay2: The ray's V vector must be a unit vector");
      return ray.V.Cross(this - ray.P);
    }

    internal double DistanceAlongRay2(DataMatrixRay2 ray)
    {
      if (Math.Abs(1.0 - ray.V.Mag()) > DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("DistanceAlongRay2: The ray's V vector must be a unit vector");
      return (this - ray.P).Dot(ray.V);
    }

    internal bool Intersect(DataMatrixRay2 p0, DataMatrixRay2 p1)
    {
      double num1 = p1.V.Cross(p0.V);
      if (Math.Abs(num1) < DataMatrixConstants.DataMatrixAlmostZero)
        return false;
      double num2 = p1.V.Cross(p1.P - p0.P);
      return this.PointAlongRay2(p0, num2 / num1);
    }

    internal bool PointAlongRay2(DataMatrixRay2 ray, double t)
    {
      if (Math.Abs(1.0 - ray.V.Mag()) > DataMatrixConstants.DataMatrixAlmostZero)
        throw new ArgumentException("PointAlongRay: The ray's V vector must be a unit vector");
      DataMatrixVector2 dataMatrixVector2 = new DataMatrixVector2(ray.V.X * t, ray.V.Y * t);
      this.X = ray.P.X + dataMatrixVector2.X;
      this.Y = ray.P.Y + dataMatrixVector2.Y;
      return true;
    }

    internal double X { get; set; }

    internal double Y { get; set; }
  }
}
