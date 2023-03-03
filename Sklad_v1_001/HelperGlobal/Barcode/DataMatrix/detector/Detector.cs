// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.detector.Detector
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.Detector;
using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.DataMatrix.detector
{
  public sealed class Detector
  {
    private readonly BitMatrix image;
    private readonly WhiteRectangleDetector rectangleDetector;

    public Detector(BitMatrix image)
    {
      this.image = image;
      this.rectangleDetector = new WhiteRectangleDetector(image);
    }

    public DetectorResult Detect()
    {
      ResultPoint[] resultPointArray = this.rectangleDetector.Detect();
      ResultPoint resultPoint1 = resultPointArray[0];
      ResultPoint resultPoint2 = resultPointArray[1];
      ResultPoint resultPoint3 = resultPointArray[2];
      ResultPoint to = resultPointArray[3];
      List<MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions> vector = new List<MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions>(4);
      vector.Add(this.TransitionsBetween(resultPoint1, resultPoint2));
      vector.Add(this.TransitionsBetween(resultPoint1, resultPoint3));
      vector.Add(this.TransitionsBetween(resultPoint2, to));
      vector.Add(this.TransitionsBetween(resultPoint3, to));
      MessagingToolkit.Barcode.Common.Collections.InsertionSort<MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions>(vector, (Comparator) new MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitionsComparator());
      MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions pointsAndTransitions1 = vector[0];
      MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions pointsAndTransitions2 = vector[1];
      Dictionary<ResultPoint, int> table = new Dictionary<ResultPoint, int>();
      MessagingToolkit.Barcode.DataMatrix.detector.Detector.Increment(table, pointsAndTransitions1.GetFrom());
      MessagingToolkit.Barcode.DataMatrix.detector.Detector.Increment(table, pointsAndTransitions1.GetTo());
      MessagingToolkit.Barcode.DataMatrix.detector.Detector.Increment(table, pointsAndTransitions2.GetFrom());
      MessagingToolkit.Barcode.DataMatrix.detector.Detector.Increment(table, pointsAndTransitions2.GetTo());
      ResultPoint resultPoint4 = (ResultPoint) null;
      ResultPoint resultPoint5 = (ResultPoint) null;
      ResultPoint resultPoint6 = (ResultPoint) null;
      IEnumerator<ResultPoint> enumerator = (IEnumerator<ResultPoint>) table.Keys.GetEnumerator();
      while (enumerator.MoveNext())
      {
        ResultPoint current = enumerator.Current;
        if (table[current] == 2)
          resultPoint5 = current;
        else if (resultPoint4 == null)
          resultPoint4 = current;
        else
          resultPoint6 = current;
      }
      if (resultPoint4 == null || resultPoint5 == null || resultPoint6 == null)
        throw NotFoundException.Instance;
      ResultPoint[] patterns = new ResultPoint[3]
      {
        resultPoint4,
        resultPoint5,
        resultPoint6
      };
      ResultPoint.OrderBestPatterns(patterns);
      ResultPoint resultPoint7 = patterns[0];
      ResultPoint bottomLeft = patterns[1];
      ResultPoint resultPoint8 = patterns[2];
      ResultPoint resultPoint9 = table.ContainsKey(resultPoint1) ? (table.ContainsKey(resultPoint2) ? (table.ContainsKey(resultPoint3) ? to : resultPoint3) : resultPoint2) : resultPoint1;
      int transitions1 = this.TransitionsBetween(resultPoint8, resultPoint9).GetTransitions();
      int transitions2 = this.TransitionsBetween(resultPoint7, resultPoint9).GetTransitions();
      if ((transitions1 & 1) == 1)
        ++transitions1;
      int num1 = transitions1 + 2;
      if ((transitions2 & 1) == 1)
        ++transitions2;
      int num2 = transitions2 + 2;
      ResultPoint resultPoint10;
      BitMatrix bits;
      if (4 * num1 >= 7 * num2 || 4 * num2 >= 7 * num1)
      {
        resultPoint10 = this.CorrectTopRightRectangular(bottomLeft, resultPoint7, resultPoint8, resultPoint9, num1, num2) ?? resultPoint9;
        int transitions3 = this.TransitionsBetween(resultPoint8, resultPoint10).GetTransitions();
        int transitions4 = this.TransitionsBetween(resultPoint7, resultPoint10).GetTransitions();
        if ((transitions3 & 1) == 1)
          ++transitions3;
        if ((transitions4 & 1) == 1)
          ++transitions4;
        bits = MessagingToolkit.Barcode.DataMatrix.detector.Detector.SampleGrid(this.image, resultPoint8, bottomLeft, resultPoint7, resultPoint10, transitions3, transitions4);
      }
      else
      {
        int dimension = Math.Min(num2, num1);
        resultPoint10 = this.CorrectTopRight(bottomLeft, resultPoint7, resultPoint8, resultPoint9, dimension) ?? resultPoint9;
        int num3 = Math.Max(this.TransitionsBetween(resultPoint8, resultPoint10).GetTransitions(), this.TransitionsBetween(resultPoint7, resultPoint10).GetTransitions()) + 1;
        if ((num3 & 1) == 1)
          ++num3;
        bits = MessagingToolkit.Barcode.DataMatrix.detector.Detector.SampleGrid(this.image, resultPoint8, bottomLeft, resultPoint7, resultPoint10, num3, num3);
      }
      return new DetectorResult(bits, new ResultPoint[4]
      {
        resultPoint8,
        bottomLeft,
        resultPoint7,
        resultPoint10
      });
    }

    private ResultPoint CorrectTopRightRectangular(
      ResultPoint bottomLeft,
      ResultPoint bottomRight,
      ResultPoint topLeft,
      ResultPoint topRight,
      int dimensionTop,
      int dimensionRight)
    {
      float num1 = (float) MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(bottomLeft, bottomRight) / (float) dimensionTop;
      int num2 = MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(topLeft, topRight);
      if (num2 == 0)
        return (ResultPoint) null;
      float num3 = (topRight.X - topLeft.X) / (float) num2;
      float num4 = (topRight.Y - topLeft.Y) / (float) num2;
      ResultPoint resultPoint1 = new ResultPoint(topRight.X + num1 * num3, topRight.Y + num1 * num4);
      float num5 = (float) MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(bottomLeft, topLeft) / (float) dimensionRight;
      int num6 = MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(bottomRight, topRight);
      if (num6 == 0)
        return (ResultPoint) null;
      float num7 = (topRight.X - bottomRight.X) / (float) num6;
      float num8 = (topRight.Y - bottomRight.Y) / (float) num6;
      ResultPoint resultPoint2 = new ResultPoint(topRight.X + num5 * num7, topRight.Y + num5 * num8);
      return !this.IsValid(resultPoint1) ? (this.IsValid(resultPoint2) ? resultPoint2 : (ResultPoint) null) : (!this.IsValid(resultPoint2) || Math.Abs(dimensionTop - this.TransitionsBetween(topLeft, resultPoint1).GetTransitions()) + Math.Abs(dimensionRight - this.TransitionsBetween(bottomRight, resultPoint1).GetTransitions()) <= Math.Abs(dimensionTop - this.TransitionsBetween(topLeft, resultPoint2).GetTransitions()) + Math.Abs(dimensionRight - this.TransitionsBetween(bottomRight, resultPoint2).GetTransitions()) ? resultPoint1 : resultPoint2);
    }

    private ResultPoint CorrectTopRight(
      ResultPoint bottomLeft,
      ResultPoint bottomRight,
      ResultPoint topLeft,
      ResultPoint topRight,
      int dimension)
    {
      float num1 = (float) MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(bottomLeft, bottomRight) / (float) dimension;
      int num2 = MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(topLeft, topRight);
      if (num2 == 0)
        return (ResultPoint) null;
      float num3 = (topRight.X - topLeft.X) / (float) num2;
      float num4 = (topRight.Y - topLeft.Y) / (float) num2;
      ResultPoint resultPoint1 = new ResultPoint(topRight.X + num1 * num3, topRight.Y + num1 * num4);
      float num5 = (float) MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(bottomLeft, bottomRight) / (float) dimension;
      int num6 = MessagingToolkit.Barcode.DataMatrix.detector.Detector.Distance(bottomRight, topRight);
      if (num6 == 0)
        return (ResultPoint) null;
      float num7 = (topRight.X - bottomRight.X) / (float) num6;
      float num8 = (topRight.Y - bottomRight.Y) / (float) num6;
      ResultPoint resultPoint2 = new ResultPoint(topRight.X + num5 * num7, topRight.Y + num5 * num8);
      return !this.IsValid(resultPoint1) ? (this.IsValid(resultPoint2) ? resultPoint2 : (ResultPoint) null) : (!this.IsValid(resultPoint2) || Math.Abs(this.TransitionsBetween(topLeft, resultPoint1).GetTransitions() - this.TransitionsBetween(bottomRight, resultPoint1).GetTransitions()) <= Math.Abs(this.TransitionsBetween(topLeft, resultPoint2).GetTransitions() - this.TransitionsBetween(bottomRight, resultPoint2).GetTransitions()) ? resultPoint1 : resultPoint2);
    }

    private bool IsValid(ResultPoint p) => (double) p.X >= 0.0 && (double) p.X < (double) this.image.Width && (double) p.Y > 0.0 && (double) p.Y < (double) this.image.Height;

    private static int Distance(ResultPoint a, ResultPoint b) => MathUtils.Round(ResultPoint.Distance(a, b));

    private static void Increment(Dictionary<ResultPoint, int> table, ResultPoint key)
    {
      if (table.ContainsKey(key))
      {
        int num = table[key];
        table[key] = num + 1;
      }
      else
        table[key] = 1;
    }

    private static BitMatrix SampleGrid(
      BitMatrix image_0,
      ResultPoint topLeft,
      ResultPoint bottomLeft,
      ResultPoint bottomRight,
      ResultPoint topRight,
      int dimensionX,
      int dimensionY)
    {
      return GridSampler.GetInstance().SampleGrid(image_0, dimensionX, dimensionY, 0.5f, 0.5f, (float) dimensionX - 0.5f, 0.5f, (float) dimensionX - 0.5f, (float) dimensionY - 0.5f, 0.5f, (float) dimensionY - 0.5f, topLeft.X, topLeft.Y, topRight.X, topRight.Y, bottomRight.X, bottomRight.Y, bottomLeft.X, bottomLeft.Y);
    }

    private MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions TransitionsBetween(
      ResultPoint from,
      ResultPoint to)
    {
      int num1 = (int) from.X;
      int num2 = (int) from.Y;
      int num3 = (int) to.X;
      int num4 = (int) to.Y;
      bool flag1 = Math.Abs(num4 - num2) > Math.Abs(num3 - num1);
      if (flag1)
      {
        int num5 = num1;
        num1 = num2;
        num2 = num5;
        int num6 = num3;
        num3 = num4;
        num4 = num6;
      }
      int num7 = Math.Abs(num3 - num1);
      int num8 = Math.Abs(num4 - num2);
      int num9 = -num7 >> 1;
      int num10 = num2 < num4 ? 1 : -1;
      int num11 = num1 < num3 ? 1 : -1;
      int transitions = 0;
      bool flag2 = this.image.Get(flag1 ? num2 : num1, flag1 ? num1 : num2);
      int num12 = num1;
      int num13 = num2;
      for (; num12 != num3; num12 += num11)
      {
        bool flag3 = this.image.Get(flag1 ? num13 : num12, flag1 ? num12 : num13);
        if (flag3 != flag2)
        {
          ++transitions;
          flag2 = flag3;
        }
        num9 += num8;
        if (num9 > 0)
        {
          if (num13 != num4)
          {
            num13 += num10;
            num9 -= num7;
          }
          else
            break;
        }
      }
      return new MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions(from, to, transitions);
    }

    private sealed class ResultPointsAndTransitions
    {
      private readonly ResultPoint from;
      private readonly ResultPoint to;
      private readonly int transitions;

      public ResultPointsAndTransitions(ResultPoint from, ResultPoint to, int transitions)
      {
        this.from = from;
        this.to = to;
        this.transitions = transitions;
      }

      internal ResultPoint GetFrom() => this.from;

      internal ResultPoint GetTo() => this.to;

      public int GetTransitions() => this.transitions;

      public override string ToString() => this.from.ToString() + "/" + (object) this.to + (object) '/' + (object) this.transitions;
    }

    private class ResultPointsAndTransitionsComparator : Comparator
    {
      public virtual int Compare(object o1, object o2) => ((MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions) o1).GetTransitions() - ((MessagingToolkit.Barcode.DataMatrix.detector.Detector.ResultPointsAndTransitions) o2).GetTransitions();
    }
  }
}
