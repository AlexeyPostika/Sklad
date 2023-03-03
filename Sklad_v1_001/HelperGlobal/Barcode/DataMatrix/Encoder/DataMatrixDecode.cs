// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DataMatrixDecode
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Collections.Generic;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  internal class DataMatrixDecode
  {
    private int _edgeMin;
    private int _edgeMax;
    private int _scanGap;
    private double _squareDevn;
    private DataMatrixSymbolSize _sizeIdxExpected;
    private int _edgeThresh;
    private int _xMin;
    private int _yMin;
    private int _xMax;
    private int _yMax;
    private int _scale;
    private byte[] _cache;
    private DataMatrixImage _image;
    private DataMatrixScanGrid _grid;

    internal DataMatrixDecode(DataMatrixImage img, int scale)
    {
      int num1 = img.Width / scale;
      int num2 = img.Height / scale;
      this._edgeMin = DataMatrixConstants.DataMatrixUndefined;
      this._edgeMax = DataMatrixConstants.DataMatrixUndefined;
      this._scanGap = 1;
      this._squareDevn = Math.Cos(5.0 * Math.PI / 18.0);
      this._sizeIdxExpected = DataMatrixSymbolSize.SymbolShapeAuto;
      this._edgeThresh = 10;
      this._xMin = 0;
      this._xMax = num1 - 1;
      this._yMin = 0;
      this._yMax = num2 - 1;
      this._scale = scale;
      this._cache = new byte[num1 * num2];
      this._image = img;
      this.ValidateSettingsAndInitScanGrid();
    }

    private void ValidateSettingsAndInitScanGrid()
    {
      if (this._squareDevn <= 0.0 || this._squareDevn >= 1.0)
        throw new ArgumentException("Invalid Decode settings!");
      if (this._scanGap < 1)
        throw new ArgumentException("Invalid Decode settings!");
      if (this._edgeThresh < 1 || this._edgeThresh > 100)
        throw new ArgumentException("Invalid Decode settings!");
      this._grid = new DataMatrixScanGrid(this);
    }

    internal int GetCacheIndex(int x, int y)
    {
      if (x < 0 || x >= this.Width || y < 0 || y >= this.Height)
        throw new ArgumentException("Error: x and/or y outside cache size");
      return y * this.Width + x;
    }

    private bool GetPixelValue(int x, int y, int channel, ref int value) => this._image.GetPixelValue(x * this._scale, y * this._scale, channel, ref value);

    internal void CacheFillQuad(
      DataMatrixPixelLoc p0,
      DataMatrixPixelLoc p1,
      DataMatrixPixelLoc p2,
      DataMatrixPixelLoc p3)
    {
      DataMatrixBresLine[] dataMatrixBresLineArray = new DataMatrixBresLine[4];
      DataMatrixPixelLoc locInside = new DataMatrixPixelLoc()
      {
        X = 0,
        Y = 0
      };
      dataMatrixBresLineArray[0] = new DataMatrixBresLine(p0, p1, locInside);
      dataMatrixBresLineArray[1] = new DataMatrixBresLine(p1, p2, locInside);
      dataMatrixBresLineArray[2] = new DataMatrixBresLine(p2, p3, locInside);
      dataMatrixBresLineArray[3] = new DataMatrixBresLine(p3, p0, locInside);
      int yMax = this._yMax;
      int x1 = 0;
      int x2 = DataMatrixCommon.Min<int>(yMax, p0.Y);
      int x3 = DataMatrixCommon.Max<int>(x1, p0.Y);
      int x4 = DataMatrixCommon.Min<int>(x2, p1.Y);
      int x5 = DataMatrixCommon.Max<int>(x3, p1.Y);
      int x6 = DataMatrixCommon.Min<int>(x4, p2.Y);
      int x7 = DataMatrixCommon.Max<int>(x5, p2.Y);
      int num1 = DataMatrixCommon.Min<int>(x6, p3.Y);
      int num2 = DataMatrixCommon.Max<int>(x7, p3.Y);
      int length = num2 - num1 + 1;
      int[] numArray1 = new int[length];
      int[] numArray2 = new int[length];
      for (int index = 0; index < length; ++index)
        numArray1[index] = this._xMax;
      for (int index1 = 0; index1 < 4; ++index1)
      {
        while (dataMatrixBresLineArray[index1].Loc.X != dataMatrixBresLineArray[index1].Loc1.X || dataMatrixBresLineArray[index1].Loc.Y != dataMatrixBresLineArray[index1].Loc1.Y)
        {
          int index2 = dataMatrixBresLineArray[index1].Loc.Y - num1;
          numArray1[index2] = DataMatrixCommon.Min<int>(numArray1[index2], dataMatrixBresLineArray[index1].Loc.X);
          numArray2[index2] = DataMatrixCommon.Max<int>(numArray2[index2], dataMatrixBresLineArray[index1].Loc.X);
          dataMatrixBresLineArray[index1].Step(1, 0);
        }
      }
      for (int y = num1; y < num2 && y < this._yMax; ++y)
      {
        int index = y - num1;
        for (int x8 = numArray1[index]; x8 < numArray2[index] && x8 < this._xMax; ++x8)
        {
          if (x8 >= 0)
          {
            if (y >= 0)
            {
              try
              {
                this._cache[this.GetCacheIndex(x8, y)] |= (byte) 128;
              }
              catch
              {
              }
            }
          }
        }
      }
    }

    internal DataMatrixMessage MosaicRegion(DataMatrixRegion reg, int fix)
    {
      int plane = reg.FlowBegin.Plane;
      reg.FlowBegin.Plane = 0;
      DataMatrixMessage dataMatrixMessage1 = this.MatrixRegion(reg, fix);
      reg.FlowBegin.Plane = 1;
      DataMatrixMessage dataMatrixMessage2 = this.MatrixRegion(reg, fix);
      reg.FlowBegin.Plane = 2;
      DataMatrixMessage dataMatrixMessage3 = this.MatrixRegion(reg, fix);
      reg.FlowBegin.Plane = plane;
      DataMatrixMessage dataMatrixMessage4 = new DataMatrixMessage(reg.SizeIdx, DataMatrixFormat.Mosaic);
      List<byte> byteList = new List<byte>();
      for (int index = 0; index < dataMatrixMessage3.OutputSize && dataMatrixMessage3.Output[index] != (byte) 0; ++index)
        byteList.Add(dataMatrixMessage3.Output[index]);
      for (int index = 0; index < dataMatrixMessage2.OutputSize && dataMatrixMessage2.Output[index] != (byte) 0; ++index)
        byteList.Add(dataMatrixMessage2.Output[index]);
      for (int index = 0; index < dataMatrixMessage1.OutputSize && dataMatrixMessage1.Output[index] != (byte) 0; ++index)
        byteList.Add(dataMatrixMessage1.Output[index]);
      byteList.Add((byte) 0);
      dataMatrixMessage4.Output = byteList.ToArray();
      return dataMatrixMessage4;
    }

    internal DataMatrixMessage MatrixRegion(DataMatrixRegion reg, int fix)
    {
      DataMatrixMessage msg = new DataMatrixMessage(reg.SizeIdx, DataMatrixFormat.Matrix);
      DataMatrixVector2 dataMatrixVector2_1 = new DataMatrixVector2();
      DataMatrixVector2 dataMatrixVector2_2 = new DataMatrixVector2();
      DataMatrixVector2 dataMatrixVector2_3 = new DataMatrixVector2();
      DataMatrixVector2 dataMatrixVector2_4 = new DataMatrixVector2();
      DataMatrixPixelLoc p0 = new DataMatrixPixelLoc();
      DataMatrixPixelLoc p1 = new DataMatrixPixelLoc();
      DataMatrixPixelLoc p3 = new DataMatrixPixelLoc();
      DataMatrixPixelLoc p2 = new DataMatrixPixelLoc();
      if (!this.PopulateArrayFromMatrix(reg, msg))
        throw new Exception("Populating Array from matrix failed!");
      DataMatrixDecode.ModulePlacementEcc200(msg.Array, msg.Code, reg.SizeIdx, DataMatrixConstants.DataMatrixModuleOnRed | DataMatrixConstants.DataMatrixModuleOnGreen | DataMatrixConstants.DataMatrixModuleOnBlue);
      if (!DataMatrixCommon.DecodeCheckErrors(msg.Code, 0, reg.SizeIdx, fix))
        return (DataMatrixMessage) null;
      dataMatrixVector2_1.X = dataMatrixVector2_3.X = dataMatrixVector2_1.Y = dataMatrixVector2_2.Y = -0.1;
      dataMatrixVector2_2.X = dataMatrixVector2_4.X = dataMatrixVector2_3.Y = dataMatrixVector2_4.Y = 1.1;
      DataMatrixVector2 dataMatrixVector2_5 = dataMatrixVector2_1 * reg.Fit2Raw;
      DataMatrixVector2 dataMatrixVector2_6 = dataMatrixVector2_2 * reg.Fit2Raw;
      DataMatrixVector2 dataMatrixVector2_7 = dataMatrixVector2_3 * reg.Fit2Raw * reg.Fit2Raw;
      p0.X = (int) (0.5 + dataMatrixVector2_5.X);
      p0.Y = (int) (0.5 + dataMatrixVector2_5.Y);
      p3.X = (int) (0.5 + dataMatrixVector2_7.X);
      p3.Y = (int) (0.5 + dataMatrixVector2_7.Y);
      p1.X = (int) (0.5 + dataMatrixVector2_6.X);
      p1.Y = (int) (0.5 + dataMatrixVector2_6.Y);
      p2.X = (int) (0.5 + dataMatrixVector2_4.X);
      p2.Y = (int) (0.5 + dataMatrixVector2_4.Y);
      this.CacheFillQuad(p0, p1, p2, p3);
      msg.DecodeDataStream(reg.SizeIdx, (byte[]) null);
      return msg;
    }

    private bool PopulateArrayFromMatrix(DataMatrixRegion reg, DataMatrixMessage msg)
    {
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribHorizDataRegions, reg.SizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribVertDataRegions, reg.SizeIdx);
      int symbolAttribute3 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribDataRegionCols, reg.SizeIdx);
      int symbolAttribute4 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribDataRegionRows, reg.SizeIdx);
      int num1 = 2 * (symbolAttribute4 + symbolAttribute3 + 2);
      if (num1 <= 0)
        throw new ArgumentException("PopulateArrayFromMatrix error: Weight Factor must be greater 0");
      for (int index1 = 0; index1 < symbolAttribute2; ++index1)
      {
        int yOrigin = index1 * (symbolAttribute4 + 2) + 1;
        for (int index2 = 0; index2 < symbolAttribute1; ++index2)
        {
          int[,] tally = new int[24, 24];
          int xOrigin = index2 * (symbolAttribute3 + 2) + 1;
          for (int index3 = 0; index3 < 24; ++index3)
          {
            for (int index4 = 0; index4 < 24; ++index4)
              tally[index3, index4] = 0;
          }
          this.TallyModuleJumps(reg, tally, xOrigin, yOrigin, symbolAttribute3, symbolAttribute4, DataMatrixDirection.DirUp);
          this.TallyModuleJumps(reg, tally, xOrigin, yOrigin, symbolAttribute3, symbolAttribute4, DataMatrixDirection.DirLeft);
          this.TallyModuleJumps(reg, tally, xOrigin, yOrigin, symbolAttribute3, symbolAttribute4, DataMatrixDirection.DirDown);
          this.TallyModuleJumps(reg, tally, xOrigin, yOrigin, symbolAttribute3, symbolAttribute4, DataMatrixDirection.DirRight);
          for (int index5 = 0; index5 < symbolAttribute4; ++index5)
          {
            for (int index6 = 0; index6 < symbolAttribute3; ++index6)
            {
              int num2 = index1 * symbolAttribute4 + index5;
              int num3 = symbolAttribute2 * symbolAttribute4 - num2 - 1;
              int num4 = index2 * symbolAttribute3 + index6;
              int index7 = num3 * symbolAttribute1 * symbolAttribute3 + num4;
              msg.Array[index7] = (double) tally[index5, index6] / (double) num1 < 0.5 ? (byte) DataMatrixConstants.DataMatrixModuleOff : (byte) DataMatrixConstants.DataMatrixModuleOnRGB;
              msg.Array[index7] |= (byte) DataMatrixConstants.DataMatrixModuleAssigned;
            }
          }
        }
      }
      return true;
    }

    private void TallyModuleJumps(
      DataMatrixRegion reg,
      int[,] tally,
      int xOrigin,
      int yOrigin,
      int mapWidth,
      int mapHeight,
      DataMatrixDirection dir)
    {
      if (dir != DataMatrixDirection.DirUp && dir != DataMatrixDirection.DirLeft && dir != DataMatrixDirection.DirDown && dir != DataMatrixDirection.DirRight)
        throw new ArgumentException("Only orthogonal directions are allowed in tally module jumps!");
      int num1 = dir == DataMatrixDirection.DirUp || dir == DataMatrixDirection.DirRight ? 1 : -1;
      bool flag1 = false;
      int num2;
      int num3;
      int num4;
      int num5;
      int num6;
      if ((dir & DataMatrixDirection.DirHorizontal) != DataMatrixDirection.DirNone)
      {
        flag1 = true;
        num2 = mapWidth;
        num3 = yOrigin;
        num4 = yOrigin + mapHeight;
        num5 = num1 == 1 ? xOrigin - 1 : xOrigin + mapWidth;
        num6 = num1 == 1 ? xOrigin + mapWidth : xOrigin - 1;
      }
      else
      {
        num2 = mapHeight;
        num3 = xOrigin;
        num4 = xOrigin + mapWidth;
        num5 = num1 == 1 ? yOrigin - 1 : yOrigin + mapHeight;
        num6 = num1 == 1 ? yOrigin + mapHeight : yOrigin - 1;
      }
      bool flag2 = reg.OffColor > reg.OnColor;
      int num7 = Math.Abs((int) (0.4 * (double) (reg.OffColor - reg.OnColor) + 0.5));
      if (num7 < 0)
        throw new Exception("Negative jump threshold is not allowed in tally module jumps");
      for (int index1 = num3; index1 < num4; ++index1)
      {
        int num8 = num5;
        int num9 = flag1 ? this.ReadModuleColor(reg, index1, num8, reg.SizeIdx, reg.FlowBegin.Plane) : this.ReadModuleColor(reg, num8, index1, reg.SizeIdx, reg.FlowBegin.Plane);
        int num10 = flag2 ? reg.OffColor - num9 : num9 - reg.OffColor;
        int num11 = num1 == 1 || (index1 & 1) == 0 ? DataMatrixConstants.DataMatrixModuleOnRGB : DataMatrixConstants.DataMatrixModuleOff;
        int num12 = num2;
        while ((num8 += num1) != num6)
        {
          int num13 = num10;
          int num14 = num11;
          int num15 = flag1 ? this.ReadModuleColor(reg, index1, num8, reg.SizeIdx, reg.FlowBegin.Plane) : this.ReadModuleColor(reg, num8, index1, reg.SizeIdx, reg.FlowBegin.Plane);
          num10 = flag2 ? reg.OffColor - num15 : num15 - reg.OffColor;
          if (num14 == DataMatrixConstants.DataMatrixModuleOnRGB)
            num11 = num10 < num13 - num7 ? DataMatrixConstants.DataMatrixModuleOff : DataMatrixConstants.DataMatrixModuleOnRGB;
          else if (num14 == DataMatrixConstants.DataMatrixModuleOff)
            num11 = num10 > num13 + num7 ? DataMatrixConstants.DataMatrixModuleOnRGB : DataMatrixConstants.DataMatrixModuleOff;
          int index2;
          int index3;
          if (flag1)
          {
            index2 = index1 - yOrigin;
            index3 = num8 - xOrigin;
          }
          else
          {
            index2 = num8 - yOrigin;
            index3 = index1 - xOrigin;
          }
          if (index2 >= 24 || index3 >= 24)
            throw new Exception("Tally module mump failed, index out of range!");
          if (num11 == DataMatrixConstants.DataMatrixModuleOnRGB)
            tally[index2, index3] += 2 * num12;
          --num12;
        }
        if (num12 != 0)
          throw new Exception("Tally module jump failed, weight <> 0!");
      }
    }

    private int ReadModuleColor(
      DataMatrixRegion reg,
      int symbolRow,
      int symbolCol,
      DataMatrixSymbolSize sizeIdx,
      int colorPlane)
    {
      double[] numArray1 = new double[5]
      {
        0.5,
        0.4,
        0.5,
        0.6,
        0.5
      };
      double[] numArray2 = new double[5]
      {
        0.5,
        0.5,
        0.4,
        0.5,
        0.6
      };
      DataMatrixVector2 dataMatrixVector2 = new DataMatrixVector2();
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolRows, sizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolCols, sizeIdx);
      int num1;
      int num2 = num1 = 0;
      for (int index = 0; index < 5; ++index)
      {
        dataMatrixVector2.X = 1.0 / (double) symbolAttribute2 * ((double) symbolCol + numArray1[index]);
        dataMatrixVector2.Y = 1.0 / (double) symbolAttribute1 * ((double) symbolRow + numArray2[index]);
        dataMatrixVector2 *= reg.Fit2Raw;
        this.GetPixelValue((int) (dataMatrixVector2.X + 0.5), (int) (dataMatrixVector2.Y + 0.5), colorPlane, ref num2);
        num1 += num2;
      }
      return num1 / 5;
    }

    internal static int ModulePlacementEcc200(
      byte[] modules,
      byte[] codewords,
      DataMatrixSymbolSize sizeIdx,
      int moduleOnColor)
    {
      if ((moduleOnColor & (DataMatrixConstants.DataMatrixModuleOnRed | DataMatrixConstants.DataMatrixModuleOnGreen | DataMatrixConstants.DataMatrixModuleOnBlue)) == 0)
        throw new Exception("Error with module placement ECC 200");
      int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixRows, sizeIdx);
      int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixCols, sizeIdx);
      int num = 0;
      int row1 = 4;
      int col1 = 0;
      do
      {
        if (row1 == symbolAttribute1 && col1 == 0)
          DataMatrixDecode.PatternShapeSpecial1(modules, symbolAttribute1, symbolAttribute2, codewords, num++, moduleOnColor);
        else if (row1 == symbolAttribute1 - 2 && col1 == 0 && symbolAttribute2 % 4 != 0)
          DataMatrixDecode.PatternShapeSpecial2(modules, symbolAttribute1, symbolAttribute2, codewords, num++, moduleOnColor);
        else if (row1 == symbolAttribute1 - 2 && col1 == 0 && symbolAttribute2 % 8 == 4)
          DataMatrixDecode.PatternShapeSpecial3(modules, symbolAttribute1, symbolAttribute2, codewords, num++, moduleOnColor);
        else if (row1 == symbolAttribute1 + 4 && col1 == 2 && symbolAttribute2 % 8 == 0)
          DataMatrixDecode.PatternShapeSpecial4(modules, symbolAttribute1, symbolAttribute2, codewords, num++, moduleOnColor);
        do
        {
          if (row1 < symbolAttribute1 && col1 >= 0 && ((int) modules[row1 * symbolAttribute2 + col1] & DataMatrixConstants.DataMatrixModuleVisited) == 0)
            DataMatrixDecode.PatternShapeStandard(modules, symbolAttribute1, symbolAttribute2, row1, col1, codewords, num++, moduleOnColor);
          row1 -= 2;
          col1 += 2;
        }
        while (row1 >= 0 && col1 < symbolAttribute2);
        int row2 = row1 + 1;
        int col2 = col1 + 3;
        do
        {
          if (row2 >= 0 && col2 < symbolAttribute2 && ((int) modules[row2 * symbolAttribute2 + col2] & DataMatrixConstants.DataMatrixModuleVisited) == 0)
            DataMatrixDecode.PatternShapeStandard(modules, symbolAttribute1, symbolAttribute2, row2, col2, codewords, num++, moduleOnColor);
          row2 += 2;
          col2 -= 2;
        }
        while (row2 < symbolAttribute1 && col2 >= 0);
        row1 = row2 + 3;
        col1 = col2 + 1;
      }
      while (row1 < symbolAttribute1 || col1 < symbolAttribute2);
      if (((int) modules[symbolAttribute1 * symbolAttribute2 - 1] & DataMatrixConstants.DataMatrixModuleVisited) == 0)
      {
        modules[symbolAttribute1 * symbolAttribute2 - 1] |= (byte) moduleOnColor;
        modules[symbolAttribute1 * symbolAttribute2 - symbolAttribute2 - 2] |= (byte) moduleOnColor;
      }
      return num;
    }

    internal static void PatternShapeStandard(
      byte[] modules,
      int mappingRows,
      int mappingCols,
      int row,
      int col,
      byte[] codeword,
      int codeWordIndex,
      int moduleOnColor)
    {
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row - 2, col - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit1, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row - 2, col - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit2, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row - 1, col - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit3, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row - 1, col - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit4, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row - 1, col, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit5, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row, col - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit6, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row, col - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit7, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, row, col, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit8, moduleOnColor);
    }

    internal static void PatternShapeSpecial1(
      byte[] modules,
      int mappingRows,
      int mappingCols,
      byte[] codeword,
      int codeWordIndex,
      int moduleOnColor)
    {
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit1, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit2, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit3, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit4, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit5, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit6, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 2, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit7, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 3, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit8, moduleOnColor);
    }

    internal static void PatternShapeSpecial2(
      byte[] modules,
      int mappingRows,
      int mappingCols,
      byte[] codeword,
      int codeWordIndex,
      int moduleOnColor)
    {
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 3, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit1, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 2, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit2, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit3, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 4, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit4, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 3, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit5, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit6, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit7, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit8, moduleOnColor);
    }

    internal static void PatternShapeSpecial3(
      byte[] modules,
      int mappingRows,
      int mappingCols,
      byte[] codeword,
      int codeWordIndex,
      int moduleOnColor)
    {
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 3, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit1, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 2, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit2, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit3, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit4, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit5, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit6, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 2, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit7, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 3, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit8, moduleOnColor);
    }

    internal static void PatternShapeSpecial4(
      byte[] modules,
      int mappingRows,
      int mappingCols,
      byte[] codeword,
      int codeWordIndex,
      int moduleOnColor)
    {
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, 0, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit1, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, mappingRows - 1, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit2, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 3, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit3, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit4, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 0, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit5, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 3, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit6, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 2, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit7, moduleOnColor);
      DataMatrixDecode.PlaceModule(modules, mappingRows, mappingCols, 1, mappingCols - 1, codeword, codeWordIndex, DataMatrixMaskBit.MaskBit8, moduleOnColor);
    }

    internal static void PlaceModule(
      byte[] modules,
      int mappingRows,
      int mappingCols,
      int row,
      int col,
      byte[] codeword,
      int codeWordIndex,
      DataMatrixMaskBit mask,
      int moduleOnColor)
    {
      if (row < 0)
      {
        row += mappingRows;
        col += 4 - (mappingRows + 4) % 8;
      }
      if (col < 0)
      {
        col += mappingCols;
        row += 4 - (mappingCols + 4) % 8;
      }
      if (((int) modules[row * mappingCols + col] & DataMatrixConstants.DataMatrixModuleAssigned) != 0)
      {
        if (((int) modules[row * mappingCols + col] & moduleOnColor) != 0)
          codeword[codeWordIndex] |= (byte) mask;
        else
          codeword[codeWordIndex] &= (byte) ((DataMatrixMaskBit.MaskBit8 | DataMatrixMaskBit.MaskBit7 | DataMatrixMaskBit.MaskBit6 | DataMatrixMaskBit.MaskBit5 | DataMatrixMaskBit.MaskBit4 | DataMatrixMaskBit.MaskBit3 | DataMatrixMaskBit.MaskBit2 | DataMatrixMaskBit.MaskBit1) ^ mask);
      }
      else
      {
        if (((int) codeword[codeWordIndex] & (int) (byte) mask) != 0)
          modules[row * mappingCols + col] |= (byte) moduleOnColor;
        modules[row * mappingCols + col] |= (byte) DataMatrixConstants.DataMatrixModuleAssigned;
      }
      modules[row * mappingCols + col] |= (byte) DataMatrixConstants.DataMatrixModuleVisited;
    }

    internal DataMatrixRegion RegionFindNext(TimeSpan timeout)
    {
      DataMatrixPixelLoc loc = new DataMatrixPixelLoc();
      DateTime now = DateTime.Now;
      while (this._grid.PopGridLocation(ref loc) != DataMatrixRange.RangeEnd)
      {
        DataMatrixRegion next = this.RegionScanPixel(loc.X, loc.Y);
        if (next != null)
          return next;
        if (DateTime.Now - now > timeout)
          break;
      }
      return (DataMatrixRegion) null;
    }

    private DataMatrixRegion RegionScanPixel(int x, int y)
    {
      DataMatrixRegion dataMatrixRegion = new DataMatrixRegion();
      DataMatrixPixelLoc loc = new DataMatrixPixelLoc()
      {
        X = x,
        Y = y
      };
      int cache = this.DecodeGetCache(loc.X, loc.Y);
      if (cache == -1)
        return (DataMatrixRegion) null;
      if (this._cache[cache] != (byte) 0)
        return (DataMatrixRegion) null;
      DataMatrixPointFlow begin = this.MatrixRegionSeekEdge(loc);
      if (begin.Mag < (int) ((double) this._edgeThresh * 7.65 + 0.5))
        return (DataMatrixRegion) null;
      if (!this.MatrixRegionOrientation(dataMatrixRegion, begin))
        return (DataMatrixRegion) null;
      if (!this.RegionUpdateXfrms(dataMatrixRegion))
        return (DataMatrixRegion) null;
      if (!this.MatrixRegionAlignCalibEdge(dataMatrixRegion, DataMatrixEdge.EdgeTop))
        return (DataMatrixRegion) null;
      if (!this.RegionUpdateXfrms(dataMatrixRegion))
        return (DataMatrixRegion) null;
      if (!this.MatrixRegionAlignCalibEdge(dataMatrixRegion, DataMatrixEdge.EdgeRight))
        return (DataMatrixRegion) null;
      if (!this.RegionUpdateXfrms(dataMatrixRegion))
        return (DataMatrixRegion) null;
      return !this.MatrixRegionFindSize(dataMatrixRegion) ? (DataMatrixRegion) null : new DataMatrixRegion(dataMatrixRegion);
    }

    private int DecodeGetCache(int x, int y)
    {
      int width = this.Width;
      int height = this.Height;
      return x < 0 || x >= width || y < 0 || y >= height ? DataMatrixConstants.DataMatrixUndefined : y * width + x;
    }

    private DataMatrixPointFlow MatrixRegionSeekEdge(DataMatrixPixelLoc loc)
    {
      DataMatrixPointFlow[] dataMatrixPointFlowArray = new DataMatrixPointFlow[3];
      int channelCount = this._image.ChannelCount;
      int index = 0;
      for (int colorPlane = 0; colorPlane < channelCount; ++colorPlane)
      {
        dataMatrixPointFlowArray[colorPlane] = this.GetPointFlow(colorPlane, loc, DataMatrixConstants.DataMatrixNeighborNone);
        if (colorPlane > 0 && dataMatrixPointFlowArray[colorPlane].Mag > dataMatrixPointFlowArray[index].Mag)
          index = colorPlane;
      }
      if (dataMatrixPointFlowArray[index].Mag < 10)
        return DataMatrixConstants.DataMatrixBlankEdge;
      DataMatrixPointFlow center = dataMatrixPointFlowArray[index];
      DataMatrixPointFlow strongestNeighbor1 = this.FindStrongestNeighbor(center, 1);
      DataMatrixPointFlow strongestNeighbor2 = this.FindStrongestNeighbor(center, -1);
      if (strongestNeighbor1.Mag != 0 && strongestNeighbor2.Mag != 0)
      {
        DataMatrixPointFlow strongestNeighbor3 = this.FindStrongestNeighbor(strongestNeighbor1, -1);
        DataMatrixPointFlow strongestNeighbor4 = this.FindStrongestNeighbor(strongestNeighbor2, 1);
        if (strongestNeighbor1.Arrive == (strongestNeighbor3.Arrive + 4) % 8 && strongestNeighbor2.Arrive == (strongestNeighbor4.Arrive + 4) % 8)
        {
          center.Arrive = DataMatrixConstants.DataMatrixNeighborNone;
          return center;
        }
      }
      return DataMatrixConstants.DataMatrixBlankEdge;
    }

    private DataMatrixPointFlow FindStrongestNeighbor(
      DataMatrixPointFlow center,
      int sign)
    {
      DataMatrixPixelLoc loc = new DataMatrixPixelLoc();
      DataMatrixPointFlow[] dataMatrixPointFlowArray = new DataMatrixPointFlow[8];
      int num1 = sign < 0 ? center.Depart : (center.Depart + 4) % 8;
      int num2 = 0;
      int index = DataMatrixConstants.DataMatrixUndefined;
      for (int arrive = 0; arrive < 8; ++arrive)
      {
        loc.X = center.Loc.X + DataMatrixConstants.DataMatrixPatternX[arrive];
        loc.Y = center.Loc.Y + DataMatrixConstants.DataMatrixPatternY[arrive];
        int cache = this.DecodeGetCache(loc.X, loc.Y);
        if (cache != DataMatrixConstants.DataMatrixUndefined)
        {
          if (((int) this._cache[cache] & 128) != 0)
          {
            if (++num2 > 2)
              return DataMatrixConstants.DataMatrixBlankEdge;
          }
          else
          {
            int num3 = Math.Abs(num1 - arrive);
            if (num3 > 4)
              num3 = 8 - num3;
            if (num3 <= 1)
            {
              dataMatrixPointFlowArray[arrive] = this.GetPointFlow(center.Plane, loc, arrive);
              if (index == DataMatrixConstants.DataMatrixUndefined || dataMatrixPointFlowArray[arrive].Mag > dataMatrixPointFlowArray[index].Mag || dataMatrixPointFlowArray[arrive].Mag == dataMatrixPointFlowArray[index].Mag && (arrive & 1) != 0)
                index = arrive;
            }
          }
        }
      }
      return index != DataMatrixConstants.DataMatrixUndefined ? dataMatrixPointFlowArray[index] : DataMatrixConstants.DataMatrixBlankEdge;
    }

    private DataMatrixPointFlow GetPointFlow(
      int colorPlane,
      DataMatrixPixelLoc loc,
      int arrive)
    {
      int[] numArray1 = new int[8]
      {
        0,
        1,
        2,
        1,
        0,
        -1,
        -2,
        -1
      };
      int[] numArray2 = new int[4];
      int[] numArray3 = new int[8];
      DataMatrixPointFlow pointFlow = new DataMatrixPointFlow();
      for (int index = 0; index < 8; ++index)
      {
        if (!this.GetPixelValue(loc.X + DataMatrixConstants.DataMatrixPatternX[index], loc.Y + DataMatrixConstants.DataMatrixPatternY[index], colorPlane, ref numArray3[index]))
          return DataMatrixConstants.DataMatrixBlankEdge;
      }
      int index1 = 0;
      for (int index2 = 0; index2 < 4; ++index2)
      {
        for (int index3 = 0; index3 < 8; ++index3)
        {
          int index4 = (index3 - index2 + 8) % 8;
          if (numArray1[index4] != 0)
          {
            int num = numArray3[index3];
            switch (numArray1[index4])
            {
              case -2:
                numArray2[index2] -= 2 * num;
                continue;
              case -1:
                numArray2[index2] -= num;
                continue;
              case 1:
                numArray2[index2] += num;
                continue;
              case 2:
                numArray2[index2] += 2 * num;
                continue;
              default:
                continue;
            }
          }
        }
        if (index2 != 0 && Math.Abs(numArray2[index2]) > Math.Abs(numArray2[index1]))
          index1 = index2;
      }
      pointFlow.Plane = colorPlane;
      pointFlow.Arrive = arrive;
      pointFlow.Depart = numArray2[index1] > 0 ? index1 + 4 : index1;
      pointFlow.Mag = Math.Abs(numArray2[index1]);
      pointFlow.Loc = loc;
      return pointFlow;
    }

    private bool MatrixRegionFindSize(DataMatrixRegion reg)
    {
      DataMatrixSymbolSize matrixSymbolSize1 = DataMatrixSymbolSize.SymbolShapeAuto;
      int num1 = 0;
      int num2;
      int num3 = num2 = 0;
      DataMatrixSymbolSize matrixSymbolSize2;
      DataMatrixSymbolSize matrixSymbolSize3;
      if (this._sizeIdxExpected == DataMatrixSymbolSize.SymbolShapeAuto)
      {
        matrixSymbolSize2 = DataMatrixSymbolSize.Symbol10x10;
        matrixSymbolSize3 = (DataMatrixSymbolSize) (DataMatrixConstants.DataMatrixSymbolSquareCount + DataMatrixConstants.DataMatrixSymbolRectCount);
      }
      else if (this._sizeIdxExpected == DataMatrixSymbolSize.SymbolSquareAuto)
      {
        matrixSymbolSize2 = DataMatrixSymbolSize.Symbol10x10;
        matrixSymbolSize3 = (DataMatrixSymbolSize) DataMatrixConstants.DataMatrixSymbolSquareCount;
      }
      else if (this._sizeIdxExpected == DataMatrixSymbolSize.SymbolRectAuto)
      {
        matrixSymbolSize2 = (DataMatrixSymbolSize) DataMatrixConstants.DataMatrixSymbolSquareCount;
        matrixSymbolSize3 = (DataMatrixSymbolSize) (DataMatrixConstants.DataMatrixSymbolSquareCount + DataMatrixConstants.DataMatrixSymbolRectCount);
      }
      else
      {
        matrixSymbolSize2 = this._sizeIdxExpected;
        matrixSymbolSize3 = this._sizeIdxExpected + 1;
      }
      for (DataMatrixSymbolSize sizeIdx = matrixSymbolSize2; sizeIdx < matrixSymbolSize3; ++sizeIdx)
      {
        int symbolAttribute1 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolRows, sizeIdx);
        int symbolAttribute2 = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolCols, sizeIdx);
        int num4;
        int num5 = num4 = 0;
        int symbolRow1 = symbolAttribute1 - 1;
        for (int symbolCol = 0; symbolCol < symbolAttribute2; ++symbolCol)
        {
          int num6 = this.ReadModuleColor(reg, symbolRow1, symbolCol, sizeIdx, reg.FlowBegin.Plane);
          if ((symbolCol & 1) != 0)
            num4 += num6;
          else
            num5 += num6;
        }
        int symbolCol1 = symbolAttribute2 - 1;
        for (int symbolRow2 = 0; symbolRow2 < symbolAttribute1; ++symbolRow2)
        {
          int num7 = this.ReadModuleColor(reg, symbolRow2, symbolCol1, sizeIdx, reg.FlowBegin.Plane);
          if ((symbolRow2 & 1) != 0)
            num4 += num7;
          else
            num5 += num7;
        }
        int num8 = num5 * 2 / (symbolAttribute1 + symbolAttribute2);
        int num9 = num4 * 2 / (symbolAttribute1 + symbolAttribute2);
        int num10 = Math.Abs(num8 - num9);
        if (num10 >= 20 && num10 > num1)
        {
          num1 = num10;
          matrixSymbolSize1 = sizeIdx;
          num3 = num8;
          num2 = num9;
        }
      }
      if (matrixSymbolSize1 == DataMatrixSymbolSize.SymbolShapeAuto || num1 < 20)
        return false;
      reg.SizeIdx = matrixSymbolSize1;
      reg.OnColor = num3;
      reg.OffColor = num2;
      reg.SymbolRows = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolRows, reg.SizeIdx);
      reg.SymbolCols = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribSymbolCols, reg.SizeIdx);
      reg.MappingRows = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixRows, reg.SizeIdx);
      reg.MappingCols = DataMatrixCommon.GetSymbolAttribute(DataMatrixSymAttribute.SymAttribMappingMatrixCols, reg.SizeIdx);
      int num11 = this.CountJumpTally(reg, 0, reg.SymbolRows - 1, DataMatrixDirection.DirRight);
      int num12 = Math.Abs(1 + num11 - reg.SymbolCols);
      if (num11 < 0 || num12 > 2)
        return false;
      int num13 = this.CountJumpTally(reg, reg.SymbolCols - 1, 0, DataMatrixDirection.DirUp);
      int num14 = Math.Abs(1 + num13 - reg.SymbolRows);
      if (num13 < 0 || num14 > 2)
        return false;
      int num15 = this.CountJumpTally(reg, 0, 0, DataMatrixDirection.DirRight);
      if (num13 < 0 || num15 > 2)
        return false;
      int num16 = this.CountJumpTally(reg, 0, 0, DataMatrixDirection.DirUp);
      if (num16 < 0 || num16 > 2)
        return false;
      int num17 = this.CountJumpTally(reg, 0, -1, DataMatrixDirection.DirRight);
      if (num17 < 0 || num17 > 2)
        return false;
      int num18 = this.CountJumpTally(reg, -1, 0, DataMatrixDirection.DirUp);
      if (num18 < 0 || num18 > 2)
        return false;
      int num19 = this.CountJumpTally(reg, 0, reg.SymbolRows, DataMatrixDirection.DirRight);
      if (num19 < 0 || num19 > 2)
        return false;
      int num20 = this.CountJumpTally(reg, reg.SymbolCols, 0, DataMatrixDirection.DirUp);
      return num20 >= 0 && num20 <= 2;
    }

    private int CountJumpTally(
      DataMatrixRegion reg,
      int xStart,
      int yStart,
      DataMatrixDirection dir)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = DataMatrixConstants.DataMatrixModuleOn;
      int num4 = 0;
      if (xStart != 0 && yStart != 0)
        throw new Exception("CountJumpTally failed, xStart or yStart must be zero!");
      if (dir == DataMatrixDirection.DirRight)
        num1 = 1;
      else
        num2 = 1;
      if (xStart == -1 || xStart == reg.SymbolCols || yStart == -1 || yStart == reg.SymbolRows)
        num3 = DataMatrixConstants.DataMatrixModuleOff;
      bool flag = reg.OffColor > reg.OnColor;
      int num5 = Math.Abs((int) (0.4 * (double) (reg.OnColor - reg.OffColor) + 0.5));
      int num6 = this.ReadModuleColor(reg, yStart, xStart, reg.SizeIdx, reg.FlowBegin.Plane);
      int num7 = flag ? reg.OffColor - num6 : num6 - reg.OffColor;
      int symbolCol = xStart + num1;
      for (int symbolRow = yStart + num2; dir == DataMatrixDirection.DirRight && symbolCol < reg.SymbolCols || dir == DataMatrixDirection.DirUp && symbolRow < reg.SymbolRows; symbolRow += num2)
      {
        int num8 = num7;
        int num9 = this.ReadModuleColor(reg, symbolRow, symbolCol, reg.SizeIdx, reg.FlowBegin.Plane);
        num7 = flag ? reg.OffColor - num9 : num9 - reg.OffColor;
        if (num3 == DataMatrixConstants.DataMatrixModuleOff)
        {
          if (num7 > num8 + num5)
          {
            ++num4;
            num3 = DataMatrixConstants.DataMatrixModuleOn;
          }
        }
        else if (num7 < num8 - num5)
        {
          ++num4;
          num3 = DataMatrixConstants.DataMatrixModuleOff;
        }
        symbolCol += num1;
      }
      return num4;
    }

    private bool MatrixRegionOrientation(DataMatrixRegion reg, DataMatrixPointFlow begin)
    {
      DataMatrixSymbolSize matrixSymbolSize = this._sizeIdxExpected == DataMatrixSymbolSize.SymbolSquareAuto || this._sizeIdxExpected >= DataMatrixSymbolSize.Symbol10x10 && this._sizeIdxExpected <= DataMatrixSymbolSize.Symbol144x144 ? DataMatrixSymbolSize.SymbolSquareAuto : (this._sizeIdxExpected == DataMatrixSymbolSize.SymbolRectAuto || this._sizeIdxExpected >= DataMatrixSymbolSize.Symbol8x18 && this._sizeIdxExpected <= DataMatrixSymbolSize.Symbol16x48 ? DataMatrixSymbolSize.SymbolRectAuto : DataMatrixSymbolSize.SymbolShapeAuto);
      int maxDiagonal = this._edgeMax == DataMatrixConstants.DataMatrixUndefined ? DataMatrixConstants.DataMatrixUndefined : (matrixSymbolSize != DataMatrixSymbolSize.SymbolRectAuto ? (int) (1.56 * (double) this._edgeMax + 0.5) : (int) (1.23 * (double) this._edgeMax + 0.5));
      if (!this.TrailBlazeContinuous(reg, begin, maxDiagonal) || reg.StepsTotal < 40)
      {
        this.TrailClear(reg, 64);
        return false;
      }
      if (this._edgeMin != DataMatrixConstants.DataMatrixUndefined)
      {
        int scale = this._scale;
        int num = matrixSymbolSize != DataMatrixSymbolSize.SymbolSquareAuto ? 2 * this._edgeMin * this._edgeMin / (scale * scale) : this._edgeMin * this._edgeMin / (scale * scale);
        if ((reg.BoundMax.X - reg.BoundMin.X) * (reg.BoundMax.Y - reg.BoundMin.Y) < num)
        {
          this.TrailClear(reg, 64);
          return false;
        }
      }
      DataMatrixBestLine bestSolidLine1 = this.FindBestSolidLine(reg, 0, 0, 1, DataMatrixConstants.DataMatrixUndefined);
      if (bestSolidLine1.Mag < 5)
      {
        this.TrailClear(reg, 64);
        return false;
      }
      this.FindTravelLimits(reg, ref bestSolidLine1);
      if (bestSolidLine1.DistSq < 100 || bestSolidLine1.Devn * 10.0 >= Math.Sqrt((double) bestSolidLine1.DistSq))
      {
        this.TrailClear(reg, 64);
        return false;
      }
      if (bestSolidLine1.StepPos < bestSolidLine1.StepNeg)
        throw new Exception("Error calculating matrix region orientation");
      DataMatrixFollow dataMatrixFollow1 = this.FollowSeek(reg, bestSolidLine1.StepPos + 5);
      DataMatrixBestLine bestSolidLine2 = this.FindBestSolidLine(reg, dataMatrixFollow1.Step, bestSolidLine1.StepNeg, 1, bestSolidLine1.Angle);
      DataMatrixFollow dataMatrixFollow2 = this.FollowSeek(reg, bestSolidLine1.StepNeg - 5);
      DataMatrixBestLine bestSolidLine3 = this.FindBestSolidLine(reg, dataMatrixFollow2.Step, bestSolidLine1.StepPos, -1, bestSolidLine1.Angle);
      if (DataMatrixCommon.Max<int>(bestSolidLine2.Mag, bestSolidLine3.Mag) < 5)
        return false;
      if (bestSolidLine2.Mag > bestSolidLine3.Mag)
      {
        DataMatrixBestLine line = bestSolidLine2;
        this.FindTravelLimits(reg, ref line);
        if (line.DistSq < 100 || line.Devn * 10.0 >= Math.Sqrt((double) line.DistSq))
          return false;
        if ((bestSolidLine1.LocPos.X - bestSolidLine1.LocNeg.X) * (line.LocPos.Y - line.LocNeg.Y) - (bestSolidLine1.LocPos.Y - bestSolidLine1.LocNeg.Y) * (line.LocPos.X - line.LocNeg.X) > 0)
        {
          reg.Polarity = 1;
          reg.LocR = line.LocPos;
          reg.StepR = line.StepPos;
          reg.LocT = bestSolidLine1.LocNeg;
          reg.StepT = bestSolidLine1.StepNeg;
          reg.LeftLoc = bestSolidLine1.LocBeg;
          reg.LeftAngle = bestSolidLine1.Angle;
          reg.BottomLoc = line.LocBeg;
          reg.BottomAngle = line.Angle;
          reg.LeftLine = bestSolidLine1;
          reg.BottomLine = line;
        }
        else
        {
          reg.Polarity = -1;
          reg.LocR = bestSolidLine1.LocNeg;
          reg.StepR = bestSolidLine1.StepNeg;
          reg.LocT = line.LocPos;
          reg.StepT = line.StepPos;
          reg.LeftLoc = line.LocBeg;
          reg.LeftAngle = line.Angle;
          reg.BottomLoc = bestSolidLine1.LocBeg;
          reg.BottomAngle = bestSolidLine1.Angle;
          reg.LeftLine = line;
          reg.BottomLine = bestSolidLine1;
        }
      }
      else
      {
        DataMatrixBestLine line = bestSolidLine3;
        this.FindTravelLimits(reg, ref line);
        if (line.DistSq < 100 || line.Devn / Math.Sqrt((double) line.DistSq) >= 0.1)
          return false;
        if ((bestSolidLine1.LocNeg.X - bestSolidLine1.LocPos.X) * (line.LocNeg.Y - line.LocPos.Y) - (bestSolidLine1.LocNeg.Y - bestSolidLine1.LocPos.Y) * (line.LocNeg.X - line.LocPos.X) > 0)
        {
          reg.Polarity = -1;
          reg.LocR = line.LocNeg;
          reg.StepR = line.StepNeg;
          reg.LocT = bestSolidLine1.LocPos;
          reg.StepT = bestSolidLine1.StepPos;
          reg.LeftLoc = bestSolidLine1.LocBeg;
          reg.LeftAngle = bestSolidLine1.Angle;
          reg.BottomLoc = line.LocBeg;
          reg.BottomAngle = line.Angle;
          reg.LeftLine = bestSolidLine1;
          reg.BottomLine = line;
        }
        else
        {
          reg.Polarity = 1;
          reg.LocR = bestSolidLine1.LocPos;
          reg.StepR = bestSolidLine1.StepPos;
          reg.LocT = line.LocNeg;
          reg.StepT = line.StepNeg;
          reg.LeftLoc = line.LocBeg;
          reg.LeftAngle = line.Angle;
          reg.BottomLoc = bestSolidLine1.LocBeg;
          reg.BottomAngle = bestSolidLine1.Angle;
          reg.LeftLine = line;
          reg.BottomLine = bestSolidLine1;
        }
      }
      reg.LeftKnown = reg.BottomKnown = 1;
      return true;
    }

    private DataMatrixBestLine FindBestSolidLine(
      DataMatrixRegion reg,
      int step0,
      int step1,
      int streamDir,
      int houghAvoid)
    {
      int[,] numArray = new int[3, DataMatrixConstants.DataMatrixHoughRes];
      char[] chArray = new char[DataMatrixConstants.DataMatrixHoughRes];
      int sign = 0;
      int num1 = 0;
      DataMatrixBestLine bestSolidLine = new DataMatrixBestLine();
      int index1 = 0;
      int index2 = 0;
      if (step0 != 0)
      {
        if (step0 > 0)
        {
          sign = 1;
          num1 = (step1 - step0 + reg.StepsTotal) % reg.StepsTotal;
        }
        else
        {
          sign = -1;
          num1 = (step0 - step1 + reg.StepsTotal) % reg.StepsTotal;
        }
        if (num1 == 0)
          num1 = reg.StepsTotal;
      }
      else if (step1 != 0)
      {
        sign = step1 > 0 ? 1 : -1;
        num1 = Math.Abs(step1);
      }
      else if (step1 == 0)
      {
        sign = 1;
        num1 = reg.StepsTotal;
      }
      if (sign != streamDir)
        throw new Exception("Sign must equal stream direction!");
      DataMatrixFollow followBeg = this.FollowSeek(reg, step0);
      DataMatrixPixelLoc loc = followBeg.Loc;
      bestSolidLine.StepBeg = bestSolidLine.StepPos = bestSolidLine.StepNeg = step0;
      bestSolidLine.LocBeg = followBeg.Loc;
      bestSolidLine.LocPos = followBeg.Loc;
      bestSolidLine.LocNeg = followBeg.Loc;
      for (int index3 = 0; index3 < DataMatrixConstants.DataMatrixHoughRes; ++index3)
      {
        if (houghAvoid == DataMatrixConstants.DataMatrixUndefined)
        {
          chArray[index3] = '\u0001';
        }
        else
        {
          int num2 = (houghAvoid + DataMatrixConstants.DataMatrixHoughRes / 6) % DataMatrixConstants.DataMatrixHoughRes;
          int num3 = (houghAvoid - DataMatrixConstants.DataMatrixHoughRes / 6 + DataMatrixConstants.DataMatrixHoughRes) % DataMatrixConstants.DataMatrixHoughRes;
          chArray[index3] = num2 <= num3 ? (index3 <= num2 || index3 >= num3 ? char.MinValue : '\u0001') : (index3 > num2 || index3 < num3 ? '\u0001' : char.MinValue);
        }
      }
      for (int index4 = 0; index4 < num1; ++index4)
      {
        int num4 = followBeg.Loc.X - loc.X;
        int num5 = followBeg.Loc.Y - loc.Y;
        for (int index5 = 0; index5 < DataMatrixConstants.DataMatrixHoughRes; ++index5)
        {
          if (chArray[index5] != char.MinValue)
          {
            int num6 = DataMatrixConstants.rHvX[index5] * num5 - DataMatrixConstants.rHvY[index5] * num4;
            if (num6 >= -384 && num6 <= 384)
            {
              int index6 = num6 <= 128 ? (num6 < (int) sbyte.MinValue ? 0 : 1) : 2;
              ++numArray[index6, index5];
              if (numArray[index6, index5] > numArray[index2, index1])
              {
                index1 = index5;
                index2 = index6;
              }
            }
          }
        }
        followBeg = this.FollowStep(reg, followBeg, sign);
      }
      bestSolidLine.Angle = index1;
      bestSolidLine.HOffset = index2;
      bestSolidLine.Mag = numArray[index2, index1];
      return bestSolidLine;
    }

    private DataMatrixFollow FollowSeek(DataMatrixRegion reg, int seek)
    {
      DataMatrixFollow followBeg = new DataMatrixFollow()
      {
        Loc = reg.FlowBegin.Loc,
        Step = 0,
        Ptr = this._cache
      };
      followBeg.PtrIndex = this.DecodeGetCache(followBeg.Loc.X, followBeg.Loc.Y);
      int sign = seek > 0 ? 1 : -1;
      for (int index = 0; index != seek; index += sign)
      {
        followBeg = this.FollowStep(reg, followBeg, sign);
        if (Math.Abs(followBeg.Step) > reg.StepsTotal)
          throw new Exception("Follow step count larger total step count!");
      }
      return followBeg;
    }

    private bool TrailBlazeContinuous(
      DataMatrixRegion reg,
      DataMatrixPointFlow flowBegin,
      int maxDiagonal)
    {
      DataMatrixPixelLoc loc;
      DataMatrixPixelLoc dataMatrixPixelLoc = loc = flowBegin.Loc;
      int cache1 = this.DecodeGetCache(flowBegin.Loc.X, flowBegin.Loc.Y);
      this._cache[cache1] = (byte) 192;
      reg.FlowBegin = flowBegin;
      int num1;
      int num2 = num1 = 0;
      for (int sign = 1; sign >= -1; sign -= 2)
      {
        DataMatrixPointFlow center = flowBegin;
        int index = cache1;
        int num3 = 0;
        while (maxDiagonal == DataMatrixConstants.DataMatrixUndefined || loc.X - dataMatrixPixelLoc.X <= maxDiagonal && loc.Y - dataMatrixPixelLoc.Y <= maxDiagonal)
        {
          DataMatrixPointFlow strongestNeighbor = this.FindStrongestNeighbor(center, sign);
          if (strongestNeighbor.Mag >= 50)
          {
            int cache2 = this.DecodeGetCache(strongestNeighbor.Loc.X, strongestNeighbor.Loc.Y);
            if (((int) this._cache[cache2] & 128) != 0)
              throw new Exception("Error creating Trail Blaze");
            this._cache[index] |= sign < 0 ? (byte) strongestNeighbor.Arrive : (byte) (strongestNeighbor.Arrive << 3);
            this._cache[cache2] = sign < 0 ? (byte) ((strongestNeighbor.Arrive + 4) % 8 << 3) : (byte) ((strongestNeighbor.Arrive + 4) % 8);
            this._cache[cache2] |= (byte) 192;
            if (sign > 0)
              ++num2;
            else
              ++num1;
            index = cache2;
            center = strongestNeighbor;
            if (center.Loc.X > loc.X)
              loc.X = center.Loc.X;
            else if (center.Loc.X < dataMatrixPixelLoc.X)
              dataMatrixPixelLoc.X = center.Loc.X;
            if (center.Loc.Y > loc.Y)
              loc.Y = center.Loc.Y;
            else if (center.Loc.Y < dataMatrixPixelLoc.Y)
              dataMatrixPixelLoc.Y = center.Loc.Y;
            ++num3;
          }
          else
            break;
        }
        if (sign > 0)
        {
          reg.FinalPos = center.Loc;
          reg.JumpToNeg = num3;
        }
        else
        {
          reg.FinalNeg = center.Loc;
          reg.JumpToPos = num3;
        }
      }
      reg.StepsTotal = reg.JumpToPos + reg.JumpToNeg;
      reg.BoundMin = dataMatrixPixelLoc;
      reg.BoundMax = loc;
      int num4 = this.TrailClear(reg, 128);
      if (num2 + num1 != num4 - 1)
        throw new Exception("Error cleaning after trail blaze continuous");
      return maxDiagonal == DataMatrixConstants.DataMatrixUndefined || loc.X - dataMatrixPixelLoc.X <= maxDiagonal && loc.Y - dataMatrixPixelLoc.Y <= maxDiagonal;
    }

    private int TrailClear(DataMatrixRegion reg, int clearMask)
    {
      if ((clearMask | (int) byte.MaxValue) != (int) byte.MaxValue)
        throw new Exception("TrailClear mask is invalid!");
      int num = 0;
      DataMatrixFollow followBeg = this.FollowSeek(reg, 0);
      while (Math.Abs(followBeg.Step) <= reg.StepsTotal)
      {
        if (((int) followBeg.CurrentPtr & clearMask) == 0)
          throw new Exception("Error performing TrailClear");
        followBeg.CurrentPtr &= (byte) (clearMask ^ (int) byte.MaxValue);
        followBeg = this.FollowStep(reg, followBeg, 1);
        ++num;
      }
      return num;
    }

    private DataMatrixFollow FollowStep(
      DataMatrixRegion reg,
      DataMatrixFollow followBeg,
      int sign)
    {
      DataMatrixFollow dataMatrixFollow = new DataMatrixFollow();
      if (Math.Abs(sign) != 1)
        throw new Exception("Invalid parameter 'sign', can only be -1 or +1");
      int num1 = reg.StepsTotal + 1;
      int num2 = sign <= 0 ? (num1 - followBeg.Step % num1) % num1 : (num1 + followBeg.Step % num1) % num1;
      if (sign > 0 && num2 == reg.JumpToNeg)
        dataMatrixFollow.Loc = reg.FinalNeg;
      else if (sign < 0 && num2 == reg.JumpToPos)
      {
        dataMatrixFollow.Loc = reg.FinalPos;
      }
      else
      {
        int index = sign < 0 ? (int) followBeg.Neighbor & 7 : ((int) followBeg.Neighbor & 56) >> 3;
        dataMatrixFollow.Loc = new DataMatrixPixelLoc()
        {
          X = followBeg.Loc.X + DataMatrixConstants.DataMatrixPatternX[index],
          Y = followBeg.Loc.Y + DataMatrixConstants.DataMatrixPatternY[index]
        };
      }
      dataMatrixFollow.Step = followBeg.Step + sign;
      dataMatrixFollow.Ptr = this._cache;
      dataMatrixFollow.PtrIndex = this.DecodeGetCache(dataMatrixFollow.Loc.X, dataMatrixFollow.Loc.Y);
      return dataMatrixFollow;
    }

    private void FindTravelLimits(DataMatrixRegion reg, ref DataMatrixBestLine line)
    {
      DataMatrixFollow followBeg1;
      DataMatrixFollow followBeg2 = followBeg1 = this.FollowSeek(reg, line.StepBeg);
      DataMatrixPixelLoc loc1 = followBeg2.Loc;
      int num1 = DataMatrixConstants.rHvX[line.Angle];
      int num2 = DataMatrixConstants.rHvY[line.Angle];
      int num3 = 0;
      DataMatrixPixelLoc loc2;
      DataMatrixPixelLoc b = loc2 = followBeg2.Loc;
      int num4;
      int num5 = num4 = 0;
      int num6;
      int num7 = num6 = 0;
      int num8 = num6;
      int x1 = num6;
      int x2 = num6;
      int y1 = num6;
      int num9;
      int num10 = num9 = 0;
      int num11 = num9;
      int x3 = num9;
      int x4 = num9;
      int y2 = num9;
      for (int index = 0; index < reg.StepsTotal / 2; ++index)
      {
        bool flag1 = index < 10 || Math.Abs(y1) < Math.Abs(num5);
        bool flag2 = index < 10 || Math.Abs(y2) < Math.Abs(num4);
        if (flag1)
        {
          int num12 = followBeg2.Loc.X - loc1.X;
          int num13 = followBeg2.Loc.Y - loc1.Y;
          num5 = num1 * num12 + num2 * num13;
          y1 = num1 * num13 - num2 * num12;
          if (y1 >= -768 && y1 <= 768)
          {
            int num14 = this.DistanceSquared(followBeg2.Loc, loc2);
            if (num14 > num3)
            {
              b = followBeg2.Loc;
              num3 = num14;
              line.StepPos = followBeg2.Step;
              line.LocPos = followBeg2.Loc;
              num8 = x2;
              num7 = x1;
            }
          }
          else
          {
            x2 = DataMatrixCommon.Min<int>(x2, y1);
            x1 = DataMatrixCommon.Max<int>(x1, y1);
          }
        }
        else if (!flag2)
          break;
        if (flag2)
        {
          int num15 = followBeg1.Loc.X - loc1.X;
          int num16 = followBeg1.Loc.Y - loc1.Y;
          num4 = num1 * num15 + num2 * num16;
          y2 = num1 * num16 - num2 * num15;
          if (y2 >= -768 && y2 < 768)
          {
            int num17 = this.DistanceSquared(followBeg1.Loc, b);
            if (num17 > num3)
            {
              loc2 = followBeg1.Loc;
              num3 = num17;
              line.StepNeg = followBeg1.Step;
              line.LocNeg = followBeg1.Loc;
              num11 = x4;
              num10 = x3;
            }
          }
          else
          {
            x4 = DataMatrixCommon.Min<int>(x4, y2);
            x3 = DataMatrixCommon.Max<int>(x3, y2);
          }
        }
        followBeg2 = this.FollowStep(reg, followBeg2, 1);
        followBeg1 = this.FollowStep(reg, followBeg1, -1);
      }
      line.Devn = (double) (DataMatrixCommon.Max<int>(num7 - num8, num10 - num11) / 256);
      line.DistSq = num3;
    }

    private int DistanceSquared(DataMatrixPixelLoc a, DataMatrixPixelLoc b)
    {
      int num1 = a.X - b.X;
      int num2 = a.Y - b.Y;
      return num1 * num1 + num2 * num2;
    }

    private bool RegionUpdateXfrms(DataMatrixRegion reg)
    {
      DataMatrixRay2 dataMatrixRay2_1 = new DataMatrixRay2();
      DataMatrixRay2 dataMatrixRay2_2 = new DataMatrixRay2();
      DataMatrixRay2 dataMatrixRay2_3 = new DataMatrixRay2();
      DataMatrixRay2 dataMatrixRay2_4 = new DataMatrixRay2();
      DataMatrixVector2 p00 = new DataMatrixVector2();
      DataMatrixVector2 p10 = new DataMatrixVector2();
      DataMatrixVector2 p11 = new DataMatrixVector2();
      DataMatrixVector2 p01 = new DataMatrixVector2();
      if (reg.LeftKnown == 0 || reg.BottomKnown == 0)
        throw new ArgumentException("Error updating Xfrms!");
      dataMatrixRay2_1.P.X = (double) reg.LeftLoc.X;
      dataMatrixRay2_1.P.Y = (double) reg.LeftLoc.Y;
      double num1 = (double) reg.LeftAngle * (Math.PI / (double) DataMatrixConstants.DataMatrixHoughRes);
      dataMatrixRay2_1.V.X = Math.Cos(num1);
      dataMatrixRay2_1.V.Y = Math.Sin(num1);
      dataMatrixRay2_1.TMin = 0.0;
      dataMatrixRay2_1.TMax = dataMatrixRay2_1.V.Norm();
      dataMatrixRay2_2.P.X = (double) reg.BottomLoc.X;
      dataMatrixRay2_2.P.Y = (double) reg.BottomLoc.Y;
      double num2 = (double) reg.BottomAngle * (Math.PI / (double) DataMatrixConstants.DataMatrixHoughRes);
      dataMatrixRay2_2.V.X = Math.Cos(num2);
      dataMatrixRay2_2.V.Y = Math.Sin(num2);
      dataMatrixRay2_2.TMin = 0.0;
      dataMatrixRay2_2.TMax = dataMatrixRay2_2.V.Norm();
      if (reg.TopKnown != 0)
      {
        dataMatrixRay2_3.P.X = (double) reg.TopLoc.X;
        dataMatrixRay2_3.P.Y = (double) reg.TopLoc.Y;
        double num3 = (double) reg.TopAngle * (Math.PI / (double) DataMatrixConstants.DataMatrixHoughRes);
        dataMatrixRay2_3.V.X = Math.Cos(num3);
        dataMatrixRay2_3.V.Y = Math.Sin(num3);
        dataMatrixRay2_3.TMin = 0.0;
        dataMatrixRay2_3.TMax = dataMatrixRay2_3.V.Norm();
      }
      else
      {
        dataMatrixRay2_3.P.X = (double) reg.LocT.X;
        dataMatrixRay2_3.P.Y = (double) reg.LocT.Y;
        double num4 = (double) reg.BottomAngle * (Math.PI / (double) DataMatrixConstants.DataMatrixHoughRes);
        dataMatrixRay2_3.V.X = Math.Cos(num4);
        dataMatrixRay2_3.V.Y = Math.Sin(num4);
        dataMatrixRay2_3.TMin = 0.0;
        dataMatrixRay2_3.TMax = dataMatrixRay2_2.TMax;
      }
      if (reg.RightKnown != 0)
      {
        dataMatrixRay2_4.P.X = (double) reg.RightLoc.X;
        dataMatrixRay2_4.P.Y = (double) reg.RightLoc.Y;
        double num5 = (double) reg.RightAngle * (Math.PI / (double) DataMatrixConstants.DataMatrixHoughRes);
        dataMatrixRay2_4.V.X = Math.Cos(num5);
        dataMatrixRay2_4.V.Y = Math.Sin(num5);
        dataMatrixRay2_4.TMin = 0.0;
        dataMatrixRay2_4.TMax = dataMatrixRay2_4.V.Norm();
      }
      else
      {
        dataMatrixRay2_4.P.X = (double) reg.LocR.X;
        dataMatrixRay2_4.P.Y = (double) reg.LocR.Y;
        double num6 = (double) reg.LeftAngle * (Math.PI / (double) DataMatrixConstants.DataMatrixHoughRes);
        dataMatrixRay2_4.V.X = Math.Cos(num6);
        dataMatrixRay2_4.V.Y = Math.Sin(num6);
        dataMatrixRay2_4.TMin = 0.0;
        dataMatrixRay2_4.TMax = dataMatrixRay2_1.TMax;
      }
      return p00.Intersect(dataMatrixRay2_1, dataMatrixRay2_2) && p10.Intersect(dataMatrixRay2_2, dataMatrixRay2_4) && p11.Intersect(dataMatrixRay2_4, dataMatrixRay2_3) && p01.Intersect(dataMatrixRay2_3, dataMatrixRay2_1) && this.RegionUpdateCorners(reg, p00, p10, p11, p01);
    }

    private bool RegionUpdateCorners(
      DataMatrixRegion reg,
      DataMatrixVector2 p00,
      DataMatrixVector2 p10,
      DataMatrixVector2 p11,
      DataMatrixVector2 p01)
    {
      double num1 = (double) (this.Width - 1);
      double num2 = (double) (this.Height - 1);
      if (p00.X < 0.0 || p00.Y < 0.0 || p00.X > num1 || p00.Y > num2 || p01.X < 0.0 || p01.Y < 0.0 || p01.X > num1 || p01.Y > num2 || p10.X < 0.0 || p10.Y < 0.0 || p10.X > num1 || p10.Y > num2)
        return false;
      DataMatrixVector2 dataMatrixVector2_1 = p01 - p00;
      DataMatrixVector2 dataMatrixVector2_2 = p10 - p00;
      DataMatrixVector2 v2_1 = p11 - p01;
      DataMatrixVector2 v2_2 = p11 - p10;
      double num3 = dataMatrixVector2_1.Mag();
      double num4 = dataMatrixVector2_2.Mag();
      double num5 = v2_1.Mag();
      double num6 = v2_2.Mag();
      if (num3 <= 8.0 || num4 <= 8.0 || num5 <= 8.0 || num6 <= 8.0)
        return false;
      double num7 = num3 / num6;
      if (num7 <= 0.5 || num7 >= 2.0)
        return false;
      double num8 = num4 / num5;
      if (num8 <= 0.5 || num8 >= 2.0 || dataMatrixVector2_2.Cross(v2_2) <= 0.0 || dataMatrixVector2_1.Cross(v2_1) >= 0.0 || DataMatrixCommon.RightAngleTrueness(p00, p10, p11, Math.PI / 2.0) <= this._squareDevn || DataMatrixCommon.RightAngleTrueness(p10, p11, p01, Math.PI / 2.0) <= this._squareDevn)
        return false;
      double tx = -1.0 * p00.X;
      double ty = -1.0 * p00.Y;
      DataMatrixMatrix3 dataMatrixMatrix3_1 = DataMatrixMatrix3.Translate(tx, ty);
      double angle = Math.Atan2(dataMatrixVector2_1.X, dataMatrixVector2_1.Y);
      DataMatrixMatrix3 dataMatrixMatrix3_2 = DataMatrixMatrix3.Rotate(angle);
      DataMatrixMatrix3 dataMatrixMatrix3_3 = dataMatrixMatrix3_1 * dataMatrixMatrix3_2;
      DataMatrixVector2 dataMatrixVector2_3 = p10 * dataMatrixMatrix3_3;
      double shy = -dataMatrixVector2_3.Y / dataMatrixVector2_3.X;
      DataMatrixMatrix3 dataMatrixMatrix3_4 = DataMatrixMatrix3.Shear(0.0, shy);
      DataMatrixMatrix3 dataMatrixMatrix3_5 = dataMatrixMatrix3_3 * dataMatrixMatrix3_4;
      double sx = 1.0 / dataMatrixVector2_3.X;
      DataMatrixMatrix3 dataMatrixMatrix3_6 = DataMatrixMatrix3.Scale(sx, 1.0);
      DataMatrixMatrix3 dataMatrixMatrix3_7 = dataMatrixMatrix3_5 * dataMatrixMatrix3_6;
      double sy = 1.0 / (p11 * dataMatrixMatrix3_7).Y;
      DataMatrixMatrix3 dataMatrixMatrix3_8 = DataMatrixMatrix3.Scale(1.0, sy);
      DataMatrixMatrix3 dataMatrixMatrix3_9 = dataMatrixMatrix3_7 * dataMatrixMatrix3_8;
      double x = (p11 * dataMatrixMatrix3_9).X;
      DataMatrixMatrix3 dataMatrixMatrix3_10 = DataMatrixMatrix3.LineSkewSide(1.0, x, 1.0);
      DataMatrixMatrix3 dataMatrixMatrix3_11 = dataMatrixMatrix3_9 * dataMatrixMatrix3_10;
      double y = (p01 * dataMatrixMatrix3_11).Y;
      DataMatrixMatrix3 dataMatrixMatrix3_12 = DataMatrixMatrix3.LineSkewTop(y, 1.0, 1.0);
      reg.Raw2Fit = dataMatrixMatrix3_11 * dataMatrixMatrix3_12;
      DataMatrixMatrix3 dataMatrixMatrix3_13 = DataMatrixMatrix3.LineSkewTopInv(y, 1.0, 1.0) * DataMatrixMatrix3.LineSkewSideInv(1.0, x, 1.0) * DataMatrixMatrix3.Scale(1.0 / sx, 1.0 / sy) * DataMatrixMatrix3.Shear(0.0, -shy) * DataMatrixMatrix3.Rotate(-angle);
      DataMatrixMatrix3 dataMatrixMatrix3_14 = DataMatrixMatrix3.Translate(-tx, -ty);
      reg.Fit2Raw = dataMatrixMatrix3_13 * dataMatrixMatrix3_14;
      return true;
    }

    private bool MatrixRegionAlignCalibEdge(DataMatrixRegion reg, DataMatrixEdge edgeLoc)
    {
      DataMatrixVector2 dataMatrixVector2_1 = new DataMatrixVector2();
      DataMatrixPixelLoc loc1 = new DataMatrixPixelLoc();
      DataMatrixPixelLoc locInside = new DataMatrixPixelLoc();
      dataMatrixVector2_1.X = 0.0;
      dataMatrixVector2_1.Y = 0.0;
      DataMatrixVector2 dataMatrixVector2_2 = dataMatrixVector2_1 * reg.Fit2Raw;
      locInside.X = (int) (dataMatrixVector2_2.X + 0.5);
      locInside.Y = (int) (dataMatrixVector2_2.Y + 0.5);
      DataMatrixSymbolSize matrixSymbolSize = this._sizeIdxExpected == DataMatrixSymbolSize.SymbolSquareAuto || this._sizeIdxExpected >= DataMatrixSymbolSize.Symbol10x10 && this._sizeIdxExpected <= DataMatrixSymbolSize.Symbol144x144 ? DataMatrixSymbolSize.SymbolSquareAuto : (this._sizeIdxExpected == DataMatrixSymbolSize.SymbolRectAuto || this._sizeIdxExpected >= DataMatrixSymbolSize.Symbol8x18 && this._sizeIdxExpected <= DataMatrixSymbolSize.Symbol16x48 ? DataMatrixSymbolSize.SymbolRectAuto : DataMatrixSymbolSize.SymbolShapeAuto);
      int num;
      int angle;
      DataMatrixFollow dataMatrixFollow;
      if (edgeLoc == DataMatrixEdge.EdgeTop)
      {
        num = reg.Polarity * -1;
        angle = reg.LeftLine.Angle;
        dataMatrixFollow = this.FollowSeekLoc(reg.LocT);
        dataMatrixVector2_2.X = 0.8;
        dataMatrixVector2_2.Y = matrixSymbolSize == DataMatrixSymbolSize.SymbolRectAuto ? 0.2 : 0.6;
      }
      else
      {
        num = reg.Polarity;
        angle = reg.BottomLine.Angle;
        dataMatrixFollow = this.FollowSeekLoc(reg.LocR);
        dataMatrixVector2_2.X = matrixSymbolSize == DataMatrixSymbolSize.SymbolSquareAuto ? 0.7 : 0.9;
        dataMatrixVector2_2.Y = 0.8;
      }
      DataMatrixVector2 dataMatrixVector2_3 = dataMatrixVector2_2 * reg.Fit2Raw;
      loc1.X = (int) (dataMatrixVector2_3.X + 0.5);
      loc1.Y = (int) (dataMatrixVector2_3.Y + 0.5);
      DataMatrixPixelLoc loc = dataMatrixFollow.Loc;
      DataMatrixBresLine line = new DataMatrixBresLine(loc, loc1, locInside);
      int tripSteps = this.TrailBlazeGapped(reg, line, num);
      DataMatrixBestLine bestSolidLine2 = this.FindBestSolidLine2(loc, tripSteps, num, angle);
      if (edgeLoc == DataMatrixEdge.EdgeTop)
      {
        reg.TopKnown = 1;
        reg.TopAngle = bestSolidLine2.Angle;
        reg.TopLoc = bestSolidLine2.LocBeg;
      }
      else
      {
        reg.RightKnown = 1;
        reg.RightAngle = bestSolidLine2.Angle;
        reg.RightLoc = bestSolidLine2.LocBeg;
      }
      return true;
    }

    private int TrailBlazeGapped(DataMatrixRegion reg, DataMatrixBresLine line, int streamDir)
    {
      int travel = 0;
      int outward = 0;
      int[] numArray = new int[9]
      {
        0,
        1,
        2,
        7,
        8,
        3,
        6,
        5,
        4
      };
      DataMatrixPixelLoc loc1 = line.Loc;
      DataMatrixPointFlow center = this.GetPointFlow(reg.FlowBegin.Plane, loc1, DataMatrixConstants.DataMatrixNeighborNone);
      int num1 = line.XDelta * line.XDelta + line.YDelta * line.YDelta;
      int num2 = 0;
      bool flag = true;
      DataMatrixPixelLoc dataMatrixPixelLoc = loc1;
      int index = this.DecodeGetCache(loc1.X, loc1.Y);
      if (index == -1)
        return 0;
      this._cache[index] = (byte) 0;
      int num3;
      do
      {
        if (flag)
        {
          DataMatrixPointFlow strongestNeighbor = this.FindStrongestNeighbor(center, streamDir);
          if (strongestNeighbor.Mag != DataMatrixConstants.DataMatrixUndefined)
          {
            new DataMatrixBresLine(line).GetStep(strongestNeighbor.Loc, ref travel, ref outward);
            if (strongestNeighbor.Mag < 50 || outward < 0 || outward == 0 && travel < 0)
            {
              flag = false;
            }
            else
            {
              line.Step(travel, outward);
              center = strongestNeighbor;
            }
          }
          else
            break;
        }
        if (!flag)
        {
          line.Step(1, 0);
          center = this.GetPointFlow(reg.FlowBegin.Plane, line.Loc, DataMatrixConstants.DataMatrixNeighborNone);
          if (center.Mag > 50)
            flag = true;
        }
        DataMatrixPixelLoc loc2 = line.Loc;
        int cache = this.DecodeGetCache(loc2.X, loc2.Y);
        if (cache != -1)
        {
          int num4 = loc2.X - dataMatrixPixelLoc.X;
          int num5 = loc2.Y - dataMatrixPixelLoc.Y;
          if (Math.Abs(num4) > 1 || Math.Abs(num5) > 1)
            throw new Exception("Invalid step directions!");
          int num6 = numArray[3 * num5 + num4 + 4];
          if (num6 == 8)
            throw new Exception("Invalid step direction!");
          if (streamDir < 0)
          {
            this._cache[index] |= (byte) (64 | num6);
            this._cache[cache] = (byte) ((num6 + 4) % 8 << 3);
          }
          else
          {
            this._cache[index] |= (byte) (64 | num6 << 3);
            this._cache[cache] = (byte) ((num6 + 4) % 8);
          }
          int num7 = line.Loc.X - loc1.X;
          int num8 = line.Loc.Y - loc1.Y;
          num3 = num7 * num7 + num8 * num8;
          dataMatrixPixelLoc = line.Loc;
          index = cache;
          ++num2;
        }
        else
          break;
      }
      while (num3 < num1);
      return num2;
    }

    private DataMatrixBestLine FindBestSolidLine2(
      DataMatrixPixelLoc loc0,
      int tripSteps,
      int sign,
      int houghAvoid)
    {
      int[,] numArray = new int[3, DataMatrixConstants.DataMatrixHoughRes];
      char[] chArray = new char[DataMatrixConstants.DataMatrixHoughRes];
      DataMatrixBestLine bestSolidLine2 = new DataMatrixBestLine();
      int index1 = 0;
      int index2 = 0;
      DataMatrixFollow followBeg = this.FollowSeekLoc(loc0);
      DataMatrixPixelLoc dataMatrixPixelLoc = bestSolidLine2.LocBeg = bestSolidLine2.LocPos = bestSolidLine2.LocNeg = followBeg.Loc;
      bestSolidLine2.StepBeg = bestSolidLine2.StepPos = bestSolidLine2.StepNeg = 0;
      for (int index3 = 0; index3 < DataMatrixConstants.DataMatrixHoughRes; ++index3)
      {
        if (houghAvoid == DataMatrixConstants.DataMatrixUndefined)
        {
          chArray[index3] = '\u0001';
        }
        else
        {
          int num1 = (houghAvoid + DataMatrixConstants.DataMatrixHoughRes / 6) % DataMatrixConstants.DataMatrixHoughRes;
          int num2 = (houghAvoid - DataMatrixConstants.DataMatrixHoughRes / 6 + DataMatrixConstants.DataMatrixHoughRes) % DataMatrixConstants.DataMatrixHoughRes;
          chArray[index3] = num1 <= num2 ? (index3 <= num1 || index3 >= num2 ? char.MinValue : '\u0001') : (index3 > num1 || index3 < num2 ? '\u0001' : char.MinValue);
        }
      }
      for (int index4 = 0; index4 < tripSteps; ++index4)
      {
        int num3 = followBeg.Loc.X - dataMatrixPixelLoc.X;
        int num4 = followBeg.Loc.Y - dataMatrixPixelLoc.Y;
        for (int index5 = 0; index5 < DataMatrixConstants.DataMatrixHoughRes; ++index5)
        {
          if (chArray[index5] != char.MinValue)
          {
            int num5 = DataMatrixConstants.rHvX[index5] * num4 - DataMatrixConstants.rHvY[index5] * num3;
            if (num5 >= -384 && num5 <= 384)
            {
              int index6 = num5 <= 128 ? (num5 < (int) sbyte.MinValue ? 0 : 1) : 2;
              ++numArray[index6, index5];
              if (numArray[index6, index5] > numArray[index2, index1])
              {
                index1 = index5;
                index2 = index6;
              }
            }
          }
        }
        followBeg = this.FollowStep2(followBeg, sign);
      }
      bestSolidLine2.Angle = index1;
      bestSolidLine2.HOffset = index2;
      bestSolidLine2.Mag = numArray[index2, index1];
      return bestSolidLine2;
    }

    private DataMatrixFollow FollowStep2(DataMatrixFollow followBeg, int sign)
    {
      DataMatrixFollow dataMatrixFollow = new DataMatrixFollow();
      if (Math.Abs(sign) != 1)
        throw new Exception("Invalid parameter 'sign', can only be -1 or +1");
      if (((int) followBeg.Neighbor & 64) == 0)
        throw new Exception("Invalid value for neighbor!");
      int index = sign < 0 ? (int) followBeg.Neighbor & 7 : ((int) followBeg.Neighbor & 56) >> 3;
      dataMatrixFollow.Loc = new DataMatrixPixelLoc()
      {
        X = followBeg.Loc.X + DataMatrixConstants.DataMatrixPatternX[index],
        Y = followBeg.Loc.Y + DataMatrixConstants.DataMatrixPatternY[index]
      };
      dataMatrixFollow.Step = followBeg.Step + sign;
      dataMatrixFollow.Ptr = this._cache;
      dataMatrixFollow.PtrIndex = this.DecodeGetCache(dataMatrixFollow.Loc.X, dataMatrixFollow.Loc.Y);
      return dataMatrixFollow;
    }

    private DataMatrixFollow FollowSeekLoc(DataMatrixPixelLoc loc)
    {
      DataMatrixFollow dataMatrixFollow = new DataMatrixFollow()
      {
        Loc = loc,
        Step = 0,
        Ptr = this._cache
      };
      dataMatrixFollow.PtrIndex = this.DecodeGetCache(dataMatrixFollow.Loc.X, dataMatrixFollow.Loc.Y);
      return dataMatrixFollow;
    }

    internal int EdgeMin
    {
      get => this._edgeMin;
      set
      {
        this._edgeMin = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int EdgeMax
    {
      get => this._edgeMax;
      set
      {
        this._edgeMax = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int ScanGap
    {
      get => this._scanGap;
      set
      {
        this._scanGap = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int SquareDevn
    {
      get => (int) (Math.Acos(this._squareDevn) * 180.0 / Math.PI);
      set
      {
        this._squareDevn = Math.Cos((double) value * (Math.PI / 180.0));
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal DataMatrixSymbolSize SizeIdxExpected
    {
      get => this._sizeIdxExpected;
      set
      {
        this._sizeIdxExpected = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int EdgeThresh
    {
      get => this._edgeThresh;
      set
      {
        this._edgeThresh = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int XMin
    {
      get => this._xMin;
      set
      {
        this._xMin = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int XMax
    {
      get => this._xMax;
      set
      {
        this._xMax = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int YMin
    {
      get => this._yMin;
      set
      {
        this._yMin = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int YMax
    {
      get => this._yMax;
      set
      {
        this._yMax = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal int Scale
    {
      get => this._scale;
      set
      {
        this._scale = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal byte[] Cache
    {
      get => this._cache;
      set
      {
        this._cache = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal DataMatrixImage Image
    {
      get => this._image;
      set
      {
        this._image = value;
        this.ValidateSettingsAndInitScanGrid();
      }
    }

    internal DataMatrixScanGrid Grid
    {
      get => this._grid;
      set => this._grid = value;
    }

    internal int Height => this._image.Height / this._scale;

    internal int Width => this._image.Width / this._scale;
  }
}
