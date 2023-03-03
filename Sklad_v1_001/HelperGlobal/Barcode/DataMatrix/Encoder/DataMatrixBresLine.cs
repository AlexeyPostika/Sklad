// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixBresLine
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal struct DataMatrixBresLine
  {
    private int _xStep;
    private int _yStep;
    private int _xDelta;
    private int _yDelta;
    private bool _steep;
    private int _xOut;
    private int _yOut;
    private int _travel;
    private int _outward;
    private int _error;
    private DataMatrixPixelLoc _loc;
    private DataMatrixPixelLoc _loc0;
    private DataMatrixPixelLoc _loc1;

    internal DataMatrixBresLine(DataMatrixBresLine orig)
    {
      this._error = orig._error;
      this._loc = new DataMatrixPixelLoc()
      {
        X = orig._loc.X,
        Y = orig._loc.Y
      };
      this._loc0 = new DataMatrixPixelLoc()
      {
        X = orig._loc0.X,
        Y = orig._loc0.Y
      };
      this._loc1 = new DataMatrixPixelLoc()
      {
        X = orig._loc1.X,
        Y = orig._loc1.Y
      };
      this._outward = orig._outward;
      this._steep = orig._steep;
      this._travel = orig._travel;
      this._xDelta = orig._xDelta;
      this._xOut = orig._xOut;
      this._xStep = orig._xStep;
      this._yDelta = orig._yDelta;
      this._yOut = orig._yOut;
      this._yStep = orig._yStep;
    }

    internal DataMatrixBresLine(
      DataMatrixPixelLoc loc0,
      DataMatrixPixelLoc loc1,
      DataMatrixPixelLoc locInside)
    {
      this._loc0 = loc0;
      this._loc1 = loc1;
      this._xStep = loc0.X < loc1.X ? 1 : -1;
      this._yStep = loc0.Y < loc1.Y ? 1 : -1;
      this._xDelta = Math.Abs(loc1.X - loc0.X);
      this._yDelta = Math.Abs(loc1.Y - loc0.Y);
      this._steep = this._yDelta > this._xDelta;
      if (this._steep)
      {
        DataMatrixPixelLoc dataMatrixPixelLoc1;
        DataMatrixPixelLoc dataMatrixPixelLoc2;
        if (loc0.Y < loc1.Y)
        {
          dataMatrixPixelLoc1 = loc0;
          dataMatrixPixelLoc2 = loc1;
        }
        else
        {
          dataMatrixPixelLoc1 = loc1;
          dataMatrixPixelLoc2 = loc0;
        }
        this._xOut = (dataMatrixPixelLoc2.X - dataMatrixPixelLoc1.X) * (locInside.Y - dataMatrixPixelLoc2.Y) - (dataMatrixPixelLoc2.Y - dataMatrixPixelLoc1.Y) * (locInside.X - dataMatrixPixelLoc2.X) > 0 ? 1 : -1;
        this._yOut = 0;
      }
      else
      {
        DataMatrixPixelLoc dataMatrixPixelLoc3;
        DataMatrixPixelLoc dataMatrixPixelLoc4;
        if (loc0.X > loc1.X)
        {
          dataMatrixPixelLoc3 = loc0;
          dataMatrixPixelLoc4 = loc1;
        }
        else
        {
          dataMatrixPixelLoc3 = loc1;
          dataMatrixPixelLoc4 = loc0;
        }
        int num = (dataMatrixPixelLoc4.X - dataMatrixPixelLoc3.X) * (locInside.Y - dataMatrixPixelLoc4.Y) - (dataMatrixPixelLoc4.Y - dataMatrixPixelLoc3.Y) * (locInside.X - dataMatrixPixelLoc4.X);
        this._xOut = 0;
        this._yOut = num > 0 ? 1 : -1;
      }
      this._loc = loc0;
      this._travel = 0;
      this._outward = 0;
      this._error = this._steep ? this._yDelta / 2 : this._xDelta / 2;
    }

    internal bool GetStep(DataMatrixPixelLoc target, ref int travel, ref int outward)
    {
      if (this._steep)
      {
        travel = this._yStep > 0 ? target.Y - this._loc.Y : this._loc.Y - target.Y;
        this.Step(travel, 0);
        outward = this._xOut > 0 ? target.X - this._loc.X : this._loc.X - target.X;
        if (this._yOut != 0)
          throw new Exception("Invald yOut value for bresline step!");
      }
      else
      {
        travel = this._xStep > 0 ? target.X - this._loc.X : this._loc.X - target.X;
        this.Step(travel, 0);
        outward = this._yOut > 0 ? target.Y - this._loc.Y : this._loc.Y - target.Y;
        if (this._xOut != 0)
          throw new Exception("Invald xOut value for bresline step!");
      }
      return true;
    }

    internal bool Step(int travel, int outward)
    {
      if (Math.Abs(travel) >= 2)
        throw new ArgumentException("Invalid value for 'travel' in BaseLineStep!");
      if (travel > 0)
      {
        ++this._travel;
        if (this._steep)
        {
          this._loc = new DataMatrixPixelLoc()
          {
            X = this._loc.X,
            Y = this._loc.Y + this._yStep
          };
          this._error -= this._xDelta;
          if (this._error < 0)
          {
            this._loc = new DataMatrixPixelLoc()
            {
              X = this._loc.X + this._xStep,
              Y = this._loc.Y
            };
            this._error += this._yDelta;
          }
        }
        else
        {
          this._loc = new DataMatrixPixelLoc()
          {
            X = this._loc.X + this._xStep,
            Y = this._loc.Y
          };
          this._error -= this._yDelta;
          if (this._error < 0)
          {
            this._loc = new DataMatrixPixelLoc()
            {
              X = this._loc.X,
              Y = this._loc.Y + this._yStep
            };
            this._error += this._xDelta;
          }
        }
      }
      else if (travel < 0)
      {
        --this._travel;
        if (this._steep)
        {
          this._loc = new DataMatrixPixelLoc()
          {
            X = this._loc.X,
            Y = this._loc.Y - this._yStep
          };
          this._error += this._xDelta;
          if (this.Error >= this.YDelta)
          {
            this._loc = new DataMatrixPixelLoc()
            {
              X = this._loc.X - this._xStep,
              Y = this._loc.Y
            };
            this._error -= this._yDelta;
          }
        }
        else
        {
          this._loc = new DataMatrixPixelLoc()
          {
            X = this._loc.X - this._xStep,
            Y = this._loc.Y
          };
          this._error += this._yDelta;
          if (this._error >= this._xDelta)
          {
            this._loc = new DataMatrixPixelLoc()
            {
              X = this._loc.X,
              Y = this._loc.Y - this._yStep
            };
            this._error -= this._xDelta;
          }
        }
      }
      for (int index = 0; index < outward; ++index)
      {
        ++this._outward;
        this._loc = new DataMatrixPixelLoc()
        {
          X = this._loc.X + this._xOut,
          Y = this._loc.Y + this._yOut
        };
      }
      return true;
    }

    internal int XStep
    {
      get => this._xStep;
      set => this._xStep = value;
    }

    internal int YStep
    {
      get => this._yStep;
      set => this._yStep = value;
    }

    internal int XDelta
    {
      get => this._xDelta;
      set => this._xDelta = value;
    }

    internal int YDelta
    {
      get => this._yDelta;
      set => this._yDelta = value;
    }

    internal bool Steep
    {
      get => this._steep;
      set => this._steep = value;
    }

    internal int XOut
    {
      get => this._xOut;
      set => this._xOut = value;
    }

    internal int YOut
    {
      get => this._yOut;
      set => this._yOut = value;
    }

    internal int Travel
    {
      get => this._travel;
      set => this._travel = value;
    }

    internal int Outward
    {
      get => this._outward;
      set => this._outward = value;
    }

    internal int Error
    {
      get => this._error;
      set => this._error = value;
    }

    internal DataMatrixPixelLoc Loc
    {
      get => this._loc;
      set => this._loc = value;
    }

    internal DataMatrixPixelLoc Loc0
    {
      get => this._loc0;
      set => this._loc0 = value;
    }

    internal DataMatrixPixelLoc Loc1
    {
      get => this._loc1;
      set => this._loc1 = value;
    }
  }
}
