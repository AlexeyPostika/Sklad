// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.ResultPoint
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common.Detector;
using System;
using System.Text;

namespace MessagingToolkit.Barcode
{
  public class ResultPoint
  {
    private float x;
    private float y;
    private readonly byte[] bytesX;
    private readonly byte[] bytesY;

    public virtual float X => this.x;

    public virtual float Y => this.y;

    public ResultPoint(float x, float y)
    {
      this.x = x;
      this.y = y;
      this.bytesX = BitConverter.GetBytes(x);
      this.bytesY = BitConverter.GetBytes(y);
    }

    public override sealed bool Equals(object other)
    {
      if (!(other is ResultPoint))
        return false;
      ResultPoint resultPoint = (ResultPoint) other;
      return (double) this.x == (double) resultPoint.x && (double) this.y == (double) resultPoint.y;
    }

    public override sealed int GetHashCode() => 31 * (((int) this.bytesX[0] << 24) + ((int) this.bytesX[1] << 16) + ((int) this.bytesX[2] << 8) + (int) this.bytesX[3]) + ((int) this.bytesY[0] << 24) + ((int) this.bytesY[1] << 16) + ((int) this.bytesY[2] << 8) + (int) this.bytesY[3];

    public override sealed string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(25);
      stringBuilder.Append('(');
      stringBuilder.Append(this.x);
      stringBuilder.Append(',');
      stringBuilder.Append(this.y);
      stringBuilder.Append(')');
      return stringBuilder.ToString();
    }

    public static void OrderBestPatterns(ResultPoint[] patterns)
    {
      float num1 = ResultPoint.Distance(patterns[0], patterns[1]);
      float num2 = ResultPoint.Distance(patterns[1], patterns[2]);
      float num3 = ResultPoint.Distance(patterns[0], patterns[2]);
      ResultPoint pattern;
      ResultPoint pointA;
      ResultPoint pointC;
      if ((double) num2 >= (double) num1 && (double) num2 >= (double) num3)
      {
        pattern = patterns[0];
        pointA = patterns[1];
        pointC = patterns[2];
      }
      else if ((double) num3 >= (double) num2 && (double) num3 >= (double) num1)
      {
        pattern = patterns[1];
        pointA = patterns[0];
        pointC = patterns[2];
      }
      else
      {
        pattern = patterns[2];
        pointA = patterns[0];
        pointC = patterns[1];
      }
      if ((double) ResultPoint.CrossProductZ(pointA, pattern, pointC) < 0.0)
      {
        ResultPoint resultPoint = pointA;
        pointA = pointC;
        pointC = resultPoint;
      }
      patterns[0] = pointA;
      patterns[1] = pattern;
      patterns[2] = pointC;
    }

    public static float Distance(ResultPoint pattern1, ResultPoint pattern2) => MathUtils.Distance(pattern1.x, pattern1.y, pattern2.x, pattern2.y);

    private static float CrossProductZ(ResultPoint pointA, ResultPoint pointB, ResultPoint pointC)
    {
      float x = pointB.x;
      float y = pointB.y;
      return (float) (((double) pointC.x - (double) x) * ((double) pointA.y - (double) y) - ((double) pointC.y - (double) y) * ((double) pointA.x - (double) x));
    }
  }
}
