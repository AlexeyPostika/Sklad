// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Aztec.Detector.Detector
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using MessagingToolkit.Barcode.Common.Detector;
using MessagingToolkit.Barcode.Common.ReedSolomon;

namespace MessagingToolkit.Barcode.Aztec.Detector
{
  public sealed class Detector
  {
    private readonly BitMatrix image;
    private bool compact;
    private int nbLayers;
    private int nbDataBlocks;
    private int nbCenterLayers;
    private int shift;

    public Detector(BitMatrix image) => this.image = image;

    public AztecDetectorResult Detect()
    {
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point[] bullEyeCornerPoints = this.GetBullEyeCornerPoints(this.GetMatrixCenter());
      this.ExtractParameters(bullEyeCornerPoints);
      ResultPoint[] matrixCornerPoints = this.GetMatrixCornerPoints(bullEyeCornerPoints);
      return new AztecDetectorResult(this.SampleGrid(this.image, matrixCornerPoints[this.shift % 4], matrixCornerPoints[(this.shift + 3) % 4], matrixCornerPoints[(this.shift + 2) % 4], matrixCornerPoints[(this.shift + 1) % 4]), matrixCornerPoints, this.compact, this.nbDataBlocks, this.nbLayers);
    }

    private void ExtractParameters(MessagingToolkit.Barcode.Aztec.Detector.Detector.Point[] bullEyeCornerPoints)
    {
      int index1 = 2 * this.nbCenterLayers;
      bool[] flagArray1 = this.SampleLine(bullEyeCornerPoints[0], bullEyeCornerPoints[1], index1 + 1);
      bool[] flagArray2 = this.SampleLine(bullEyeCornerPoints[1], bullEyeCornerPoints[2], index1 + 1);
      bool[] flagArray3 = this.SampleLine(bullEyeCornerPoints[2], bullEyeCornerPoints[3], index1 + 1);
      bool[] flagArray4 = this.SampleLine(bullEyeCornerPoints[3], bullEyeCornerPoints[0], index1 + 1);
      if (flagArray1[0] && flagArray1[index1])
        this.shift = 0;
      else if (flagArray2[0] && flagArray2[index1])
        this.shift = 1;
      else if (flagArray3[0] && flagArray3[index1])
      {
        this.shift = 2;
      }
      else
      {
        if (!flagArray4[0] || !flagArray4[index1])
          throw NotFoundException.Instance;
        this.shift = 3;
      }
      bool[] parameterData;
      if (this.compact)
      {
        bool[] flagArray5 = new bool[28];
        for (int index2 = 0; index2 < 7; ++index2)
        {
          flagArray5[index2] = flagArray1[2 + index2];
          flagArray5[index2 + 7] = flagArray2[2 + index2];
          flagArray5[index2 + 14] = flagArray3[2 + index2];
          flagArray5[index2 + 21] = flagArray4[2 + index2];
        }
        parameterData = new bool[28];
        for (int index3 = 0; index3 < 28; ++index3)
          parameterData[index3] = flagArray5[(index3 + this.shift * 7) % 28];
      }
      else
      {
        bool[] flagArray6 = new bool[40];
        for (int index4 = 0; index4 < 11; ++index4)
        {
          if (index4 < 5)
          {
            flagArray6[index4] = flagArray1[2 + index4];
            flagArray6[index4 + 10] = flagArray2[2 + index4];
            flagArray6[index4 + 20] = flagArray3[2 + index4];
            flagArray6[index4 + 30] = flagArray4[2 + index4];
          }
          if (index4 > 5)
          {
            flagArray6[index4 - 1] = flagArray1[2 + index4];
            flagArray6[index4 + 9] = flagArray2[2 + index4];
            flagArray6[index4 + 19] = flagArray3[2 + index4];
            flagArray6[index4 + 29] = flagArray4[2 + index4];
          }
        }
        parameterData = new bool[40];
        for (int index5 = 0; index5 < 40; ++index5)
          parameterData[index5] = flagArray6[(index5 + this.shift * 10) % 40];
      }
      MessagingToolkit.Barcode.Aztec.Detector.Detector.CorrectParameterData(parameterData, this.compact);
      this.GetParameters(parameterData);
    }

    private ResultPoint[] GetMatrixCornerPoints(MessagingToolkit.Barcode.Aztec.Detector.Detector.Point[] bullEyeCornerPoints)
    {
      float num1 = (float) (2 * this.nbLayers + (this.nbLayers > 4 ? 1 : 0) + (this.nbLayers - 4) / 8) / (2f * (float) this.nbCenterLayers);
      int num2 = bullEyeCornerPoints[0].x - bullEyeCornerPoints[2].x;
      int num3 = num2 + (num2 > 0 ? 1 : -1);
      int num4 = bullEyeCornerPoints[0].y - bullEyeCornerPoints[2].y;
      int num5 = num4 + (num4 > 0 ? 1 : -1);
      int x1 = MathUtils.Round((float) bullEyeCornerPoints[2].x - num1 * (float) num3);
      int y1 = MathUtils.Round((float) bullEyeCornerPoints[2].y - num1 * (float) num5);
      int x2 = MathUtils.Round((float) bullEyeCornerPoints[0].x + num1 * (float) num3);
      int y2 = MathUtils.Round((float) bullEyeCornerPoints[0].y + num1 * (float) num5);
      int num6 = bullEyeCornerPoints[1].x - bullEyeCornerPoints[3].x;
      int num7 = num6 + (num6 > 0 ? 1 : -1);
      int num8 = bullEyeCornerPoints[1].y - bullEyeCornerPoints[3].y;
      int num9 = num8 + (num8 > 0 ? 1 : -1);
      int x3 = MathUtils.Round((float) bullEyeCornerPoints[3].x - num1 * (float) num7);
      int y3 = MathUtils.Round((float) bullEyeCornerPoints[3].y - num1 * (float) num9);
      int x4 = MathUtils.Round((float) bullEyeCornerPoints[1].x + num1 * (float) num7);
      int y4 = MathUtils.Round((float) bullEyeCornerPoints[1].y + num1 * (float) num9);
      if (!this.IsValid(x2, y2) || !this.IsValid(x4, y4) || !this.IsValid(x1, y1) || !this.IsValid(x3, y3))
        throw NotFoundException.Instance;
      return new ResultPoint[4]
      {
        new ResultPoint((float) x2, (float) y2),
        new ResultPoint((float) x4, (float) y4),
        new ResultPoint((float) x1, (float) y1),
        new ResultPoint((float) x3, (float) y3)
      };
    }

    private static void CorrectParameterData(bool[] parameterData, bool compact_0)
    {
      int length;
      int num1;
      if (compact_0)
      {
        length = 7;
        num1 = 2;
      }
      else
      {
        length = 10;
        num1 = 4;
      }
      int twoS = length - num1;
      int[] received = new int[length];
      int num2 = 4;
      for (int index1 = 0; index1 < length; ++index1)
      {
        int num3 = 1;
        for (int index2 = 1; index2 <= num2; ++index2)
        {
          if (parameterData[num2 * index1 + num2 - index2])
            received[index1] += num3;
          num3 <<= 1;
        }
      }
      try
      {
        new ReedSolomonDecoder(GenericGF.AztecParam).Decode(received, twoS);
      }
      catch (ReedSolomonException ex)
      {
        throw NotFoundException.Instance;
      }
      for (int index3 = 0; index3 < num1; ++index3)
      {
        int num4 = 1;
        for (int index4 = 1; index4 <= num2; ++index4)
        {
          parameterData[index3 * num2 + num2 - index4] = (received[index3] & num4) == num4;
          num4 <<= 1;
        }
      }
    }

    private MessagingToolkit.Barcode.Aztec.Detector.Detector.Point[] GetBullEyeCornerPoints(
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point pCenter)
    {
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point point1 = pCenter;
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point init1 = pCenter;
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point init2 = pCenter;
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point point2 = pCenter;
      bool color = true;
      for (this.nbCenterLayers = 1; this.nbCenterLayers < 9; ++this.nbCenterLayers)
      {
        MessagingToolkit.Barcode.Aztec.Detector.Detector.Point firstDifferent1 = this.GetFirstDifferent(point1, color, 1, -1);
        MessagingToolkit.Barcode.Aztec.Detector.Detector.Point firstDifferent2 = this.GetFirstDifferent(init1, color, 1, 1);
        MessagingToolkit.Barcode.Aztec.Detector.Detector.Point firstDifferent3 = this.GetFirstDifferent(init2, color, -1, 1);
        MessagingToolkit.Barcode.Aztec.Detector.Detector.Point firstDifferent4 = this.GetFirstDifferent(point2, color, -1, -1);
        if (this.nbCenterLayers > 2)
        {
          float num = (float) ((double) MessagingToolkit.Barcode.Aztec.Detector.Detector.Distance(firstDifferent4, firstDifferent1) * (double) this.nbCenterLayers / ((double) MessagingToolkit.Barcode.Aztec.Detector.Detector.Distance(point2, point1) * (double) (this.nbCenterLayers + 2)));
          if ((double) num < 0.75 || (double) num > 1.25 || !this.IsWhiteOrBlackRectangle(firstDifferent1, firstDifferent2, firstDifferent3, firstDifferent4))
            break;
        }
        point1 = firstDifferent1;
        init1 = firstDifferent2;
        init2 = firstDifferent3;
        point2 = firstDifferent4;
        color = !color;
      }
      if (this.nbCenterLayers != 5 && this.nbCenterLayers != 7)
        throw NotFoundException.Instance;
      this.compact = this.nbCenterLayers == 5;
      float num1 = 1.5f / (float) (2 * this.nbCenterLayers - 3);
      int num2 = point1.x - init2.x;
      int num3 = point1.y - init2.y;
      int x1 = MathUtils.Round((float) init2.x - num1 * (float) num2);
      int y1 = MathUtils.Round((float) init2.y - num1 * (float) num3);
      int x2 = MathUtils.Round((float) point1.x + num1 * (float) num2);
      int y2 = MathUtils.Round((float) point1.y + num1 * (float) num3);
      int num4 = init1.x - point2.x;
      int num5 = init1.y - point2.y;
      int x3 = MathUtils.Round((float) point2.x - num1 * (float) num4);
      int y3 = MathUtils.Round((float) point2.y - num1 * (float) num5);
      int x4 = MathUtils.Round((float) init1.x + num1 * (float) num4);
      int y4 = MathUtils.Round((float) init1.y + num1 * (float) num5);
      if (!this.IsValid(x2, y2) || !this.IsValid(x4, y4) || !this.IsValid(x1, y1) || !this.IsValid(x3, y3))
        throw NotFoundException.Instance;
      return new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point[4]
      {
        new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x2, y2),
        new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x4, y4),
        new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x1, y1),
        new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x3, y3)
      };
    }

    private MessagingToolkit.Barcode.Aztec.Detector.Detector.Point GetMatrixCenter()
    {
      ResultPoint resultPoint1;
      ResultPoint resultPoint2;
      ResultPoint resultPoint3;
      ResultPoint resultPoint4;
      try
      {
        ResultPoint[] resultPointArray = new WhiteRectangleDetector(this.image).Detect();
        resultPoint1 = resultPointArray[0];
        resultPoint2 = resultPointArray[1];
        resultPoint3 = resultPointArray[2];
        resultPoint4 = resultPointArray[3];
      }
      catch (NotFoundException ex)
      {
        int num1 = this.image.GetWidth() / 2;
        int num2 = this.image.GetHeight() / 2;
        resultPoint1 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(num1 + 7, num2 - 7), false, 1, -1).ToResultPoint();
        resultPoint2 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(num1 + 7, num2 + 7), false, 1, 1).ToResultPoint();
        resultPoint3 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(num1 - 7, num2 + 7), false, -1, 1).ToResultPoint();
        resultPoint4 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(num1 - 7, num2 - 7), false, -1, -1).ToResultPoint();
      }
      int x = MathUtils.Round((float) (((double) resultPoint1.X + (double) resultPoint4.X + (double) resultPoint2.X + (double) resultPoint3.X) / 4.0));
      int y = MathUtils.Round((float) (((double) resultPoint1.Y + (double) resultPoint4.Y + (double) resultPoint2.Y + (double) resultPoint3.Y) / 4.0));
      ResultPoint resultPoint5;
      ResultPoint resultPoint6;
      ResultPoint resultPoint7;
      ResultPoint resultPoint8;
      try
      {
        ResultPoint[] resultPointArray = new WhiteRectangleDetector(this.image, 15, x, y).Detect();
        resultPoint5 = resultPointArray[0];
        resultPoint6 = resultPointArray[1];
        resultPoint7 = resultPointArray[2];
        resultPoint8 = resultPointArray[3];
      }
      catch (NotFoundException ex)
      {
        resultPoint5 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x + 7, y - 7), false, 1, -1).ToResultPoint();
        resultPoint6 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x + 7, y + 7), false, 1, 1).ToResultPoint();
        resultPoint7 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x - 7, y + 7), false, -1, 1).ToResultPoint();
        resultPoint8 = this.GetFirstDifferent(new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x - 7, y - 7), false, -1, -1).ToResultPoint();
      }
      return new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(MathUtils.Round((float) (((double) resultPoint5.X + (double) resultPoint8.X + (double) resultPoint6.X + (double) resultPoint7.X) / 4.0)), MathUtils.Round((float) (((double) resultPoint5.Y + (double) resultPoint8.Y + (double) resultPoint6.Y + (double) resultPoint7.Y) / 4.0)));
    }

    private BitMatrix SampleGrid(
      BitMatrix image_0,
      ResultPoint topLeft,
      ResultPoint bottomLeft,
      ResultPoint bottomRight,
      ResultPoint topRight)
    {
      int num = !this.compact ? (this.nbLayers > 4 ? 4 * this.nbLayers + 2 * ((this.nbLayers - 4) / 8 + 1) + 15 : 4 * this.nbLayers + 15) : 4 * this.nbLayers + 11;
      return GridSampler.GetInstance().SampleGrid(image_0, num, num, 0.5f, 0.5f, (float) num - 0.5f, 0.5f, (float) num - 0.5f, (float) num - 0.5f, 0.5f, (float) num - 0.5f, topLeft.X, topLeft.Y, topRight.X, topRight.Y, bottomRight.X, bottomRight.Y, bottomLeft.X, bottomLeft.Y);
    }

    private void GetParameters(bool[] parameterData)
    {
      int num1;
      int num2;
      if (this.compact)
      {
        num1 = 2;
        num2 = 6;
      }
      else
      {
        num1 = 5;
        num2 = 11;
      }
      for (int index = 0; index < num1; ++index)
      {
        this.nbLayers <<= 1;
        if (parameterData[index])
          ++this.nbLayers;
      }
      for (int index = num1; index < num1 + num2; ++index)
      {
        this.nbDataBlocks <<= 1;
        if (parameterData[index])
          ++this.nbDataBlocks;
      }
      ++this.nbLayers;
      ++this.nbDataBlocks;
    }

    private bool[] SampleLine(MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p1, MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p2, int size)
    {
      bool[] flagArray = new bool[size];
      float num1 = MessagingToolkit.Barcode.Aztec.Detector.Detector.Distance(p1, p2);
      float num2 = num1 / (float) (size - 1);
      float num3 = num2 * (float) (p2.x - p1.x) / num1;
      float num4 = num2 * (float) (p2.y - p1.y) / num1;
      float x = (float) p1.x;
      float y = (float) p1.y;
      for (int index = 0; index < size; ++index)
      {
        flagArray[index] = this.image.Get(MathUtils.Round(x), MathUtils.Round(y));
        x += num3;
        y += num4;
      }
      return flagArray;
    }

    private bool IsWhiteOrBlackRectangle(
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p1,
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p2,
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p3,
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p4)
    {
      int num = 3;
      p1 = new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(p1.x - num, p1.y + num);
      p2 = new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(p2.x - num, p2.y - num);
      p3 = new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(p3.x + num, p3.y - num);
      p4 = new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(p4.x + num, p4.y + num);
      int color = this.GetColor(p4, p1);
      return color != 0 && this.GetColor(p1, p2) == color && this.GetColor(p2, p3) == color && this.GetColor(p3, p4) == color;
    }

    private int GetColor(MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p1, MessagingToolkit.Barcode.Aztec.Detector.Detector.Point p2)
    {
      float num1 = MessagingToolkit.Barcode.Aztec.Detector.Detector.Distance(p1, p2);
      float num2 = (float) (p2.x - p1.x) / num1;
      float num3 = (float) (p2.y - p1.y) / num1;
      int num4 = 0;
      float x = (float) p1.x;
      float y = (float) p1.y;
      bool flag = this.image.Get(p1.x, p1.y);
      for (int index = 0; (double) index < (double) num1; ++index)
      {
        x += num2;
        y += num3;
        if (this.image.Get(MathUtils.Round(x), MathUtils.Round(y)) != flag)
          ++num4;
      }
      float num5 = (float) num4 / num1;
      if ((double) num5 > 0.100000001490116 && (double) num5 < 0.899999976158142)
        return 0;
      return (double) num5 <= 0.100000001490116 != flag ? -1 : 1;
    }

    private MessagingToolkit.Barcode.Aztec.Detector.Detector.Point GetFirstDifferent(
      MessagingToolkit.Barcode.Aztec.Detector.Detector.Point init,
      bool color,
      int dx,
      int dy)
    {
      int x1 = init.x + dx;
      int y1;
      for (y1 = init.y + dy; this.IsValid(x1, y1) && this.image.Get(x1, y1) == color; y1 += dy)
        x1 += dx;
      int x2 = x1 - dx;
      int y2 = y1 - dy;
      while (this.IsValid(x2, y2) && this.image.Get(x2, y2) == color)
        x2 += dx;
      int x3 = x2 - dx;
      while (this.IsValid(x3, y2) && this.image.Get(x3, y2) == color)
        y2 += dy;
      int y3 = y2 - dy;
      return new MessagingToolkit.Barcode.Aztec.Detector.Detector.Point(x3, y3);
    }

    private bool IsValid(int x, int y) => x >= 0 && x < this.image.Width && y > 0 && y < this.image.Height;

    private static float Distance(MessagingToolkit.Barcode.Aztec.Detector.Detector.Point a, MessagingToolkit.Barcode.Aztec.Detector.Detector.Point b) => MathUtils.Distance(a.x, a.y, b.x, b.y);

    internal sealed class Point
    {
      public int x;
      public int y;

      public ResultPoint ToResultPoint() => new ResultPoint((float) this.x, (float) this.y);

      public Point(int x, int y)
      {
        this.x = x;
        this.y = y;
      }
    }
  }
}
