// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Decoders.BitMatrixParser
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Common;
using System;

namespace MessagingToolkit.Barcode.DataMatrix.Decoders
{
  internal sealed class BitMatrixParser
  {
    private readonly BitMatrix mappingBitMatrix;
    private readonly BitMatrix readMappingMatrix;
    private readonly Version version;

    internal BitMatrixParser(BitMatrix bitMatrix)
    {
      int height = bitMatrix.Height;
      if (height < 8 || height > 144 || (height & 1) != 0)
        throw MessagingToolkit.Barcode.FormatException.Instance;
      this.version = BitMatrixParser.ReadVersion(bitMatrix);
      this.mappingBitMatrix = this.ExtractDataRegion(bitMatrix);
      this.readMappingMatrix = new BitMatrix(this.mappingBitMatrix.Width, this.mappingBitMatrix.Height);
    }

    internal Version GetVersion() => this.version;

    private static Version ReadVersion(BitMatrix bitMatrix) => Version.GetVersionForDimensions(bitMatrix.Height, bitMatrix.Width);

    internal byte[] ReadCodewords()
    {
      byte[] numArray = new byte[this.version.GetTotalCodewords()];
      int num1 = 0;
      int num2 = 4;
      int num3 = 0;
      int height = this.mappingBitMatrix.Height;
      int width = this.mappingBitMatrix.Width;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      do
      {
        if (num2 == height && num3 == 0 && !flag1)
        {
          numArray[num1++] = (byte) this.ReadCorner1(height, width);
          num2 -= 2;
          num3 += 2;
          flag1 = true;
        }
        else if (num2 == height - 2 && num3 == 0 && (width & 3) != 0 && !flag2)
        {
          numArray[num1++] = (byte) this.ReadCorner2(height, width);
          num2 -= 2;
          num3 += 2;
          flag2 = true;
        }
        else if (num2 == height + 4 && num3 == 2 && (width & 7) == 0 && !flag3)
        {
          numArray[num1++] = (byte) this.ReadCorner3(height, width);
          num2 -= 2;
          num3 += 2;
          flag3 = true;
        }
        else if (num2 == height - 2 && num3 == 0 && (width & 7) == 4 && !flag4)
        {
          numArray[num1++] = (byte) this.ReadCorner4(height, width);
          num2 -= 2;
          num3 += 2;
          flag4 = true;
        }
        else
        {
          do
          {
            if (num2 < height && num3 >= 0 && !this.readMappingMatrix.Get(num3, num2))
              numArray[num1++] = (byte) this.ReadUtah(num2, num3, height, width);
            num2 -= 2;
            num3 += 2;
          }
          while (num2 >= 0 && num3 < width);
          int num4 = num2 + 1;
          int num5 = num3 + 3;
          do
          {
            if (num4 >= 0 && num5 < width && !this.readMappingMatrix.Get(num5, num4))
              numArray[num1++] = (byte) this.ReadUtah(num4, num5, height, width);
            num4 += 2;
            num5 -= 2;
          }
          while (num4 < height && num5 >= 0);
          num2 = num4 + 3;
          num3 = num5 + 1;
        }
      }
      while (num2 < height || num3 < width);
      if (num1 != this.version.GetTotalCodewords())
        throw MessagingToolkit.Barcode.FormatException.Instance;
      return numArray;
    }

    internal bool ReadModule(int row, int column, int numRows, int numColumns)
    {
      if (row < 0)
      {
        row += numRows;
        column += 4 - (numRows + 4 & 7);
      }
      if (column < 0)
      {
        column += numColumns;
        row += 4 - (numColumns + 4 & 7);
      }
      this.readMappingMatrix.Set(column, row);
      return this.mappingBitMatrix.Get(column, row);
    }

    internal int ReadUtah(int row, int column, int numRows, int numColumns)
    {
      int num1 = 0;
      if (this.ReadModule(row - 2, column - 2, numRows, numColumns))
        num1 |= 1;
      int num2 = num1 << 1;
      if (this.ReadModule(row - 2, column - 1, numRows, numColumns))
        num2 |= 1;
      int num3 = num2 << 1;
      if (this.ReadModule(row - 1, column - 2, numRows, numColumns))
        num3 |= 1;
      int num4 = num3 << 1;
      if (this.ReadModule(row - 1, column - 1, numRows, numColumns))
        num4 |= 1;
      int num5 = num4 << 1;
      if (this.ReadModule(row - 1, column, numRows, numColumns))
        num5 |= 1;
      int num6 = num5 << 1;
      if (this.ReadModule(row, column - 2, numRows, numColumns))
        num6 |= 1;
      int num7 = num6 << 1;
      if (this.ReadModule(row, column - 1, numRows, numColumns))
        num7 |= 1;
      int num8 = num7 << 1;
      if (this.ReadModule(row, column, numRows, numColumns))
        num8 |= 1;
      return num8;
    }

    internal int ReadCorner1(int numRows, int numColumns)
    {
      int num1 = 0;
      if (this.ReadModule(numRows - 1, 0, numRows, numColumns))
        num1 |= 1;
      int num2 = num1 << 1;
      if (this.ReadModule(numRows - 1, 1, numRows, numColumns))
        num2 |= 1;
      int num3 = num2 << 1;
      if (this.ReadModule(numRows - 1, 2, numRows, numColumns))
        num3 |= 1;
      int num4 = num3 << 1;
      if (this.ReadModule(0, numColumns - 2, numRows, numColumns))
        num4 |= 1;
      int num5 = num4 << 1;
      if (this.ReadModule(0, numColumns - 1, numRows, numColumns))
        num5 |= 1;
      int num6 = num5 << 1;
      if (this.ReadModule(1, numColumns - 1, numRows, numColumns))
        num6 |= 1;
      int num7 = num6 << 1;
      if (this.ReadModule(2, numColumns - 1, numRows, numColumns))
        num7 |= 1;
      int num8 = num7 << 1;
      if (this.ReadModule(3, numColumns - 1, numRows, numColumns))
        num8 |= 1;
      return num8;
    }

    internal int ReadCorner2(int numRows, int numColumns)
    {
      int num1 = 0;
      if (this.ReadModule(numRows - 3, 0, numRows, numColumns))
        num1 |= 1;
      int num2 = num1 << 1;
      if (this.ReadModule(numRows - 2, 0, numRows, numColumns))
        num2 |= 1;
      int num3 = num2 << 1;
      if (this.ReadModule(numRows - 1, 0, numRows, numColumns))
        num3 |= 1;
      int num4 = num3 << 1;
      if (this.ReadModule(0, numColumns - 4, numRows, numColumns))
        num4 |= 1;
      int num5 = num4 << 1;
      if (this.ReadModule(0, numColumns - 3, numRows, numColumns))
        num5 |= 1;
      int num6 = num5 << 1;
      if (this.ReadModule(0, numColumns - 2, numRows, numColumns))
        num6 |= 1;
      int num7 = num6 << 1;
      if (this.ReadModule(0, numColumns - 1, numRows, numColumns))
        num7 |= 1;
      int num8 = num7 << 1;
      if (this.ReadModule(1, numColumns - 1, numRows, numColumns))
        num8 |= 1;
      return num8;
    }

    internal int ReadCorner3(int numRows, int numColumns)
    {
      int num1 = 0;
      if (this.ReadModule(numRows - 1, 0, numRows, numColumns))
        num1 |= 1;
      int num2 = num1 << 1;
      if (this.ReadModule(numRows - 1, numColumns - 1, numRows, numColumns))
        num2 |= 1;
      int num3 = num2 << 1;
      if (this.ReadModule(0, numColumns - 3, numRows, numColumns))
        num3 |= 1;
      int num4 = num3 << 1;
      if (this.ReadModule(0, numColumns - 2, numRows, numColumns))
        num4 |= 1;
      int num5 = num4 << 1;
      if (this.ReadModule(0, numColumns - 1, numRows, numColumns))
        num5 |= 1;
      int num6 = num5 << 1;
      if (this.ReadModule(1, numColumns - 3, numRows, numColumns))
        num6 |= 1;
      int num7 = num6 << 1;
      if (this.ReadModule(1, numColumns - 2, numRows, numColumns))
        num7 |= 1;
      int num8 = num7 << 1;
      if (this.ReadModule(1, numColumns - 1, numRows, numColumns))
        num8 |= 1;
      return num8;
    }

    internal int ReadCorner4(int numRows, int numColumns)
    {
      int num1 = 0;
      if (this.ReadModule(numRows - 3, 0, numRows, numColumns))
        num1 |= 1;
      int num2 = num1 << 1;
      if (this.ReadModule(numRows - 2, 0, numRows, numColumns))
        num2 |= 1;
      int num3 = num2 << 1;
      if (this.ReadModule(numRows - 1, 0, numRows, numColumns))
        num3 |= 1;
      int num4 = num3 << 1;
      if (this.ReadModule(0, numColumns - 2, numRows, numColumns))
        num4 |= 1;
      int num5 = num4 << 1;
      if (this.ReadModule(0, numColumns - 1, numRows, numColumns))
        num5 |= 1;
      int num6 = num5 << 1;
      if (this.ReadModule(1, numColumns - 1, numRows, numColumns))
        num6 |= 1;
      int num7 = num6 << 1;
      if (this.ReadModule(2, numColumns - 1, numRows, numColumns))
        num7 |= 1;
      int num8 = num7 << 1;
      if (this.ReadModule(3, numColumns - 1, numRows, numColumns))
        num8 |= 1;
      return num8;
    }

    internal BitMatrix ExtractDataRegion(BitMatrix bitMatrix)
    {
      int symbolSizeRows = this.version.GetSymbolSizeRows();
      int symbolSizeColumns = this.version.GetSymbolSizeColumns();
      if (bitMatrix.Height != symbolSizeRows)
        throw new ArgumentException("Dimension of bitMarix must match the version size");
      int dataRegionSizeRows = this.version.GetDataRegionSizeRows();
      int regionSizeColumns = this.version.GetDataRegionSizeColumns();
      int num1 = symbolSizeRows / dataRegionSizeRows;
      int num2 = symbolSizeColumns / regionSizeColumns;
      int height = num1 * dataRegionSizeRows;
      BitMatrix dataRegion = new BitMatrix(num2 * regionSizeColumns, height);
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int num3 = index1 * dataRegionSizeRows;
        for (int index2 = 0; index2 < num2; ++index2)
        {
          int num4 = index2 * regionSizeColumns;
          for (int index3 = 0; index3 < dataRegionSizeRows; ++index3)
          {
            int y1 = index1 * (dataRegionSizeRows + 2) + 1 + index3;
            int y2 = num3 + index3;
            for (int index4 = 0; index4 < regionSizeColumns; ++index4)
            {
              int x1 = index2 * (regionSizeColumns + 2) + 1 + index4;
              if (bitMatrix.Get(x1, y1))
              {
                int x2 = num4 + index4;
                dataRegion.Set(x2, y2);
              }
            }
          }
        }
      }
      return dataRegion;
    }
  }
}
