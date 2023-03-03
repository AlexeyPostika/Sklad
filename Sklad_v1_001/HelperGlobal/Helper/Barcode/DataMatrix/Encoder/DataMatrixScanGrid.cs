// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixScanGrid
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal struct DataMatrixScanGrid
  {
    private int _minExtent;
    private int _maxExtent;
    private int _xOffset;
    private int _yOffset;
    private int _xMin;
    private int _xMax;
    private int _yMin;
    private int _yMax;
    private int _total;
    private int _extent;
    private int _jumpSize;
    private int _pixelTotal;
    private int _startPos;
    private int _pixelCount;
    private int _xCenter;
    private int _yCenter;

    internal DataMatrixScanGrid(DataMatrixDecode dec)
    {
      int scanGap = dec.ScanGap;
      this._xMin = dec.XMin;
      this._xMax = dec.XMax;
      this._yMin = dec.YMin;
      this._yMax = dec.YMax;
      int num1 = this._xMax - this._xMin;
      int num2 = this._yMax - this._yMin;
      int num3 = num1 > num2 ? num1 : num2;
      if (num3 < 1)
        throw new ArgumentException("Invalid max extent for Scan Grid: Must be greater than 0");
      int num4 = 1;
      this._minExtent = num4;
      for (; num4 < num3; num4 = (num4 + 1) * 2 - 1)
      {
        if (num4 <= scanGap)
          this._minExtent = num4;
      }
      this._maxExtent = num4;
      this._xOffset = (this._xMin + this._xMax - this._maxExtent) / 2;
      this._yOffset = (this._yMin + this._yMax - this._maxExtent) / 2;
      this._total = 1;
      this._extent = this._maxExtent;
      this._jumpSize = this._extent + 1;
      this._pixelTotal = 2 * this._extent - 1;
      this._startPos = this._extent / 2;
      this._pixelCount = 0;
      this._xCenter = this._yCenter = this._startPos;
      this.SetDerivedFields();
    }

    internal DataMatrixRange PopGridLocation(ref DataMatrixPixelLoc loc)
    {
      DataMatrixRange gridCoordinates;
      do
      {
        gridCoordinates = this.GetGridCoordinates(ref loc);
        ++this._pixelCount;
      }
      while (gridCoordinates == DataMatrixRange.RangeBad);
      return gridCoordinates;
    }

    private DataMatrixRange GetGridCoordinates(ref DataMatrixPixelLoc locRef)
    {
      if (this._pixelCount >= this._pixelTotal)
      {
        this._pixelCount = 0;
        this._xCenter += this._jumpSize;
      }
      if (this._xCenter > this._maxExtent)
      {
        this._xCenter = this._startPos;
        this._yCenter += this._jumpSize;
      }
      if (this._yCenter > this._maxExtent)
      {
        this._total *= 4;
        this._extent /= 2;
        this.SetDerivedFields();
      }
      if (this._extent == 0 || this._extent < this._minExtent)
      {
        locRef.X = locRef.Y = -1;
        return DataMatrixRange.RangeEnd;
      }
      int pixelCount = this._pixelCount;
      if (pixelCount >= this._pixelTotal)
        throw new Exception("Scangrid is beyong image limits!");
      DataMatrixPixelLoc dataMatrixPixelLoc = new DataMatrixPixelLoc();
      if (pixelCount == this._pixelTotal - 1)
      {
        dataMatrixPixelLoc.X = this._xCenter;
        dataMatrixPixelLoc.Y = this._yCenter;
      }
      else
      {
        int num1 = this._pixelTotal / 2;
        int num2 = num1 / 2;
        if (pixelCount < num1)
        {
          dataMatrixPixelLoc.X = this._xCenter + (pixelCount < num2 ? pixelCount - num2 : num1 - pixelCount);
          dataMatrixPixelLoc.Y = this._yCenter;
        }
        else
        {
          int num3 = pixelCount - num1;
          dataMatrixPixelLoc.X = this._xCenter;
          dataMatrixPixelLoc.Y = this._yCenter + (num3 < num2 ? num3 - num2 : num1 - num3);
        }
      }
      dataMatrixPixelLoc.X += this._xOffset;
      dataMatrixPixelLoc.Y += this._yOffset;
      locRef.X = dataMatrixPixelLoc.X;
      locRef.Y = dataMatrixPixelLoc.Y;
      return dataMatrixPixelLoc.X < this._xMin || dataMatrixPixelLoc.X > this._xMax || dataMatrixPixelLoc.Y < this._yMin || dataMatrixPixelLoc.Y > this._yMax ? DataMatrixRange.RangeBad : DataMatrixRange.RangeGood;
    }

    private void SetDerivedFields()
    {
      this._jumpSize = this._extent + 1;
      this._pixelTotal = 2 * this._extent - 1;
      this._startPos = this._extent / 2;
      this._pixelCount = 0;
      this._xCenter = this._yCenter = this._startPos;
    }

    internal int MinExtent
    {
      get => this._minExtent;
      set => this._minExtent = value;
    }

    internal int MaxExtent
    {
      get => this._maxExtent;
      set => this._maxExtent = value;
    }

    internal int XOffset
    {
      get => this._xOffset;
      set => this._xOffset = value;
    }

    internal int YOffset
    {
      get => this._yOffset;
      set => this._yOffset = value;
    }

    internal int XMin
    {
      get => this._xMin;
      set => this._xMin = value;
    }

    internal int XMax
    {
      get => this._xMax;
      set => this._xMax = value;
    }

    internal int YMin
    {
      get => this._yMin;
      set => this._yMin = value;
    }

    internal int YMax
    {
      get => this._yMax;
      set => this._yMax = value;
    }

    internal int Total
    {
      get => this._total;
      set => this._total = value;
    }

    internal int Extent
    {
      get => this._extent;
      set => this._extent = value;
    }

    internal int JumpSize
    {
      get => this._jumpSize;
      set => this._jumpSize = value;
    }

    internal int PixelTotal
    {
      get => this._pixelTotal;
      set => this._pixelTotal = value;
    }

    internal int StartPos
    {
      get => this._startPos;
      set => this._startPos = value;
    }

    internal int PixelCount
    {
      get => this._pixelCount;
      set => this._pixelCount = value;
    }

    internal int XCenter
    {
      get => this._xCenter;
      set => this._xCenter = value;
    }

    internal int YCenter
    {
      get => this._yCenter;
      set => this._yCenter = value;
    }
  }
}
