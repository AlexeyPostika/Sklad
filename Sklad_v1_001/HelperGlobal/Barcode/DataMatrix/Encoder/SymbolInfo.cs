// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.SymbolInfo
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  public class SymbolInfo
  {
    public static readonly SymbolInfo[] ProdSymbols = new SymbolInfo[30]
    {
      new SymbolInfo(false, 3, 5, 8, 8, 1),
      new SymbolInfo(false, 5, 7, 10, 10, 1),
      new SymbolInfo(true, 5, 7, 16, 6, 1),
      new SymbolInfo(false, 8, 10, 12, 12, 1),
      new SymbolInfo(true, 10, 11, 14, 6, 2),
      new SymbolInfo(false, 12, 12, 14, 14, 1),
      new SymbolInfo(true, 16, 14, 24, 10, 1),
      new SymbolInfo(false, 18, 14, 16, 16, 1),
      new SymbolInfo(false, 22, 18, 18, 18, 1),
      new SymbolInfo(true, 22, 18, 16, 10, 2),
      new SymbolInfo(false, 30, 20, 20, 20, 1),
      new SymbolInfo(true, 32, 24, 16, 14, 2),
      new SymbolInfo(false, 36, 24, 22, 22, 1),
      new SymbolInfo(false, 44, 28, 24, 24, 1),
      new SymbolInfo(true, 49, 28, 22, 14, 2),
      new SymbolInfo(false, 62, 36, 14, 14, 4),
      new SymbolInfo(false, 86, 42, 16, 16, 4),
      new SymbolInfo(false, 114, 48, 18, 18, 4),
      new SymbolInfo(false, 144, 56, 20, 20, 4),
      new SymbolInfo(false, 174, 68, 22, 22, 4),
      new SymbolInfo(false, 204, 84, 24, 24, 4, 102, 42),
      new SymbolInfo(false, 280, 112, 14, 14, 16, 140, 56),
      new SymbolInfo(false, 368, 144, 16, 16, 16, 92, 36),
      new SymbolInfo(false, 456, 192, 18, 18, 16, 114, 48),
      new SymbolInfo(false, 576, 224, 20, 20, 16, 144, 56),
      new SymbolInfo(false, 696, 272, 22, 22, 16, 174, 68),
      new SymbolInfo(false, 816, 336, 24, 24, 16, 136, 56),
      new SymbolInfo(false, 1050, 408, 18, 18, 36, 175, 68),
      new SymbolInfo(false, 1304, 496, 20, 20, 36, 163, 62),
      (SymbolInfo) new DataMatrixSymbolInfo144()
    };
    private static SymbolInfo[] symbols = SymbolInfo.ProdSymbols;
    public bool Rectangular;
    public int dataCapacity;
    public int errorCodewords;
    public int matrixWidth;
    public int matrixHeight;
    public int dataRegions;
    public int rsBlockData;
    public int rsBlockError;

    public static void OverrideSymbolSet(SymbolInfo[] s) => SymbolInfo.symbols = s;

    public SymbolInfo(
      bool rectangular,
      int dataCapacity,
      int errorCodewords,
      int matrixWidth,
      int matrixHeight,
      int dataRegions)
      : this(rectangular, dataCapacity, errorCodewords, matrixWidth, matrixHeight, dataRegions, dataCapacity, errorCodewords)
    {
    }

    public SymbolInfo(
      bool rectangular,
      int dataCapacity,
      int errorCodewords,
      int matrixWidth,
      int matrixHeight,
      int dataRegions,
      int rsBlockData,
      int rsBlockError)
    {
      this.Rectangular = rectangular;
      this.dataCapacity = dataCapacity;
      this.errorCodewords = errorCodewords;
      this.matrixWidth = matrixWidth;
      this.matrixHeight = matrixHeight;
      this.dataRegions = dataRegions;
      this.rsBlockData = rsBlockData;
      this.rsBlockError = rsBlockError;
    }

    public static SymbolInfo Lookup(int dataCodewords) => SymbolInfo.Lookup(dataCodewords, SymbolShapeHint.ForceNone, true);

    public static SymbolInfo Lookup(int dataCodewords, SymbolShapeHint shape) => SymbolInfo.Lookup(dataCodewords, shape, true);

    public static SymbolInfo Lookup(int dataCodewords, bool allowRectangular, bool fail)
    {
      SymbolShapeHint shape = allowRectangular ? SymbolShapeHint.ForceNone : SymbolShapeHint.ForceSquare;
      return SymbolInfo.Lookup(dataCodewords, shape, fail);
    }

    public static SymbolInfo Lookup(int dataCodewords, SymbolShapeHint shape, bool fail) => SymbolInfo.Lookup(dataCodewords, shape, (Dimension) null, (Dimension) null, fail);

    public static SymbolInfo Lookup(
      int dataCodewords,
      SymbolShapeHint shape,
      Dimension minSize,
      Dimension maxSize,
      bool fail)
    {
      int index = 0;
      for (int length = SymbolInfo.symbols.Length; index < length; ++index)
      {
        SymbolInfo symbol = SymbolInfo.symbols[index];
        if ((shape != SymbolShapeHint.ForceSquare || !symbol.Rectangular) && (shape != SymbolShapeHint.ForceRectangle || symbol.Rectangular) && (minSize == null || symbol.SymbolWidth >= minSize.Width && symbol.SymbolHeight >= minSize.Height) && (maxSize == null || symbol.SymbolWidth <= maxSize.Width && symbol.SymbolHeight <= maxSize.Height) && dataCodewords <= symbol.dataCapacity)
          return symbol;
      }
      if (fail)
        throw new ArgumentException("Can't find a symbol arrangement that matches the message. Data codewords: " + (object) dataCodewords);
      return (SymbolInfo) null;
    }

    public virtual int HorzDataRegions
    {
      get
      {
        switch (this.dataRegions)
        {
          case 1:
            return 1;
          case 2:
            return 2;
          case 4:
            return 2;
          case 16:
            return 4;
          case 36:
            return 6;
          default:
            throw new ArgumentException("Cannot handle this number of data regions");
        }
      }
    }

    public virtual int VertDataRegions
    {
      get
      {
        switch (this.dataRegions)
        {
          case 1:
            return 1;
          case 2:
            return 1;
          case 4:
            return 2;
          case 16:
            return 4;
          case 36:
            return 6;
          default:
            throw new ArgumentException("Cannot handle this number of data regions");
        }
      }
    }

    public virtual int SymbolDataWidth => this.HorzDataRegions * this.matrixWidth;

    public virtual int SymbolDataHeight => this.VertDataRegions * this.matrixHeight;

    public virtual int SymbolWidth => this.SymbolDataWidth + this.HorzDataRegions * 2;

    public virtual int SymbolHeight => this.SymbolDataHeight + this.VertDataRegions * 2;

    public virtual int CodewordCount => this.dataCapacity + this.errorCodewords;

    public virtual int InterleavedBlockCount => this.dataCapacity / this.rsBlockData;

    public virtual int GetDataLengthForInterleavedBlock(int index) => this.rsBlockData;

    public virtual int GetErrorLengthForInterleavedBlock(int index) => this.rsBlockError;

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.Rectangular ? "Rectangular Symbol:" : "Square Symbol:");
      stringBuilder.Append(" data region ").Append(this.matrixWidth).Append("x").Append(this.matrixHeight);
      stringBuilder.Append(", symbol size ").Append(this.SymbolWidth).Append("x").Append(this.SymbolHeight);
      stringBuilder.Append(", symbol data size ").Append(this.SymbolDataWidth).Append("x").Append(this.SymbolDataHeight);
      stringBuilder.Append(", codewords ").Append(this.dataCapacity).Append("+").Append(this.errorCodewords);
      return stringBuilder.ToString();
    }
  }
}
