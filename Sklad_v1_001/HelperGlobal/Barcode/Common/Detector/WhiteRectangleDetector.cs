// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.Detector.WhiteRectangleDetector
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Common.Detector
{
  public sealed class WhiteRectangleDetector
  {
    private const int InitSize = 30;
    private const int Corr = 1;
    private readonly BitMatrix image;
    private readonly int height;
    private readonly int width;
    private readonly int leftInit;
    private readonly int rightInit;
    private readonly int downInit;
    private readonly int upInit;

    public WhiteRectangleDetector(BitMatrix img)
    {
      this.image = img;
      this.height = img.Height;
      this.width = img.Width;
      this.leftInit = this.width - 30 >> 1;
      this.rightInit = this.width + 30 >> 1;
      this.upInit = this.height - 30 >> 1;
      this.downInit = this.height + 30 >> 1;
      if (this.upInit < 0 || this.leftInit < 0 || this.downInit >= this.height || this.rightInit >= this.width)
        throw NotFoundException.Instance;
    }

    public WhiteRectangleDetector(BitMatrix img, int initSize, int x, int y)
    {
      this.image = img;
      this.height = img.Height;
      this.width = img.Width;
      int num = initSize >> 1;
      this.leftInit = x - num;
      this.rightInit = x + num;
      this.upInit = y - num;
      this.downInit = y + num;
      if (this.upInit < 0 || this.leftInit < 0 || this.downInit >= this.height || this.rightInit >= this.width)
        throw NotFoundException.Instance;
    }

    public ResultPoint[] Detect()
    {
      int leftInit = this.leftInit;
      int rightInit = this.rightInit;
      int upInit = this.upInit;
      int downInit = this.downInit;
      bool flag1 = false;
      bool flag2 = true;
      bool flag3 = false;
      while (flag2)
      {
        flag2 = false;
        bool flag4 = true;
        while (flag4 && rightInit < this.width)
        {
          flag4 = this.ContainsBlackPoint(upInit, downInit, rightInit, false);
          if (flag4)
          {
            ++rightInit;
            flag2 = true;
          }
        }
        if (rightInit >= this.width)
        {
          flag1 = true;
          break;
        }
        bool flag5 = true;
        while (flag5 && downInit < this.height)
        {
          flag5 = this.ContainsBlackPoint(leftInit, rightInit, downInit, true);
          if (flag5)
          {
            ++downInit;
            flag2 = true;
          }
        }
        if (downInit >= this.height)
        {
          flag1 = true;
          break;
        }
        bool flag6 = true;
        while (flag6 && leftInit >= 0)
        {
          flag6 = this.ContainsBlackPoint(upInit, downInit, leftInit, false);
          if (flag6)
          {
            --leftInit;
            flag2 = true;
          }
        }
        if (leftInit < 0)
        {
          flag1 = true;
          break;
        }
        bool flag7 = true;
        while (flag7 && upInit >= 0)
        {
          flag7 = this.ContainsBlackPoint(leftInit, rightInit, upInit, true);
          if (flag7)
          {
            --upInit;
            flag2 = true;
          }
        }
        if (upInit < 0)
        {
          flag1 = true;
          break;
        }
        if (flag2)
          flag3 = true;
      }
      if (flag1 || !flag3)
        throw NotFoundException.Instance;
      int num = rightInit - leftInit;
      ResultPoint z = (ResultPoint) null;
      for (int index = 1; index < num; ++index)
      {
        z = this.GetBlackPointOnSegment((float) leftInit, (float) (downInit - index), (float) (leftInit + index), (float) downInit);
        if (z != null)
          break;
      }
      if (z == null)
        throw NotFoundException.Instance;
      ResultPoint t = (ResultPoint) null;
      for (int index = 1; index < num; ++index)
      {
        t = this.GetBlackPointOnSegment((float) leftInit, (float) (upInit + index), (float) (leftInit + index), (float) upInit);
        if (t != null)
          break;
      }
      if (t == null)
        throw NotFoundException.Instance;
      ResultPoint x = (ResultPoint) null;
      for (int index = 1; index < num; ++index)
      {
        x = this.GetBlackPointOnSegment((float) rightInit, (float) (upInit + index), (float) (rightInit - index), (float) upInit);
        if (x != null)
          break;
      }
      if (x == null)
        throw NotFoundException.Instance;
      ResultPoint y = (ResultPoint) null;
      for (int index = 1; index < num; ++index)
      {
        y = this.GetBlackPointOnSegment((float) rightInit, (float) (downInit - index), (float) (rightInit - index), (float) downInit);
        if (y != null)
          break;
      }
      if (y == null)
        throw NotFoundException.Instance;
      return this.CenterEdges(y, z, x, t);
    }

    private ResultPoint GetBlackPointOnSegment(float aX, float aY, float bX, float bY)
    {
      int num1 = MathUtils.Round(MathUtils.Distance(aX, aY, bX, bY));
      float num2 = (bX - aX) / (float) num1;
      float num3 = (bY - aY) / (float) num1;
      for (int index = 0; index < num1; ++index)
      {
        int x = MathUtils.Round(aX + (float) index * num2);
        int y = MathUtils.Round(aY + (float) index * num3);
        if (this.image.Get(x, y))
          return new ResultPoint((float) x, (float) y);
      }
      return (ResultPoint) null;
    }

    private ResultPoint[] CenterEdges(
      ResultPoint y,
      ResultPoint z,
      ResultPoint x,
      ResultPoint t)
    {
      float x1 = y.X;
      float y1 = y.Y;
      float x2 = z.X;
      float y2 = z.Y;
      float x3 = x.X;
      float y3 = x.Y;
      float x4 = t.X;
      float y4 = t.Y;
      return (double) x1 < (double) this.width / 2.0 ? new ResultPoint[4]
      {
        new ResultPoint(x4 - 1f, y4 + 1f),
        new ResultPoint(x2 + 1f, y2 + 1f),
        new ResultPoint(x3 - 1f, y3 - 1f),
        new ResultPoint(x1 + 1f, y1 - 1f)
      } : new ResultPoint[4]
      {
        new ResultPoint(x4 + 1f, y4 + 1f),
        new ResultPoint(x2 + 1f, y2 - 1f),
        new ResultPoint(x3 - 1f, y3 + 1f),
        new ResultPoint(x1 - 1f, y1 - 1f)
      };
    }

    private bool ContainsBlackPoint(int a, int b, int fix, bool horizontal)
    {
      if (horizontal)
      {
        for (int x = a; x <= b; ++x)
        {
          if (this.image.Get(x, fix))
            return true;
        }
      }
      else
      {
        for (int y = a; y <= b; ++y)
        {
          if (this.image.Get(fix, y))
            return true;
        }
      }
      return false;
    }
  }
}
