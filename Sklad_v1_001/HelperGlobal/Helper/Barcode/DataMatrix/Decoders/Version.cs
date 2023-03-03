// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Decoders.Version
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Decoders
{
  public sealed class Version
  {
    private static readonly Version[] VERSIONS = Version.BuildVersions();
    private readonly int versionNumber;
    private readonly int symbolSizeRows;
    private readonly int symbolSizeColumns;
    private readonly int dataRegionSizeRows;
    private readonly int dataRegionSizeColumns;
    private readonly Version.ECBlocks ecBlocks;
    private readonly int totalCodewords;

    private Version(
      int versionNumber,
      int symbolSizeRows,
      int symbolSizeColumns,
      int dataRegionSizeRows,
      int dataRegionSizeColumns,
      Version.ECBlocks ecBlocks)
    {
      this.versionNumber = versionNumber;
      this.symbolSizeRows = symbolSizeRows;
      this.symbolSizeColumns = symbolSizeColumns;
      this.dataRegionSizeRows = dataRegionSizeRows;
      this.dataRegionSizeColumns = dataRegionSizeColumns;
      this.ecBlocks = ecBlocks;
      int num = 0;
      int ecCodewords = ecBlocks.GetECCodewords();
      foreach (Version.ECB ecBlock in ecBlocks.GetECBlocks())
        num += ecBlock.GetCount() * (ecBlock.GetDataCodewords() + ecCodewords);
      this.totalCodewords = num;
    }

    public int GetVersionNumber() => this.versionNumber;

    public int GetSymbolSizeRows() => this.symbolSizeRows;

    public int GetSymbolSizeColumns() => this.symbolSizeColumns;

    public int GetDataRegionSizeRows() => this.dataRegionSizeRows;

    public int GetDataRegionSizeColumns() => this.dataRegionSizeColumns;

    public int GetTotalCodewords() => this.totalCodewords;

    internal Version.ECBlocks GetECBlocks() => this.ecBlocks;

    public static Version GetVersionForDimensions(int numRows, int numColumns)
    {
      if ((numRows & 1) != 0 || (numColumns & 1) != 0)
        throw FormatException.Instance;
      foreach (Version versionForDimensions in Version.VERSIONS)
      {
        if (versionForDimensions.symbolSizeRows == numRows && versionForDimensions.symbolSizeColumns == numColumns)
          return versionForDimensions;
      }
      throw FormatException.Instance;
    }

    public override string ToString() => this.versionNumber.ToString();

    private static Version[] BuildVersions() => new Version[30]
    {
      new Version(1, 10, 10, 8, 8, new Version.ECBlocks(5, new Version.ECB(1, 3))),
      new Version(2, 12, 12, 10, 10, new Version.ECBlocks(7, new Version.ECB(1, 5))),
      new Version(3, 14, 14, 12, 12, new Version.ECBlocks(10, new Version.ECB(1, 8))),
      new Version(4, 16, 16, 14, 14, new Version.ECBlocks(12, new Version.ECB(1, 12))),
      new Version(5, 18, 18, 16, 16, new Version.ECBlocks(14, new Version.ECB(1, 18))),
      new Version(6, 20, 20, 18, 18, new Version.ECBlocks(18, new Version.ECB(1, 22))),
      new Version(7, 22, 22, 20, 20, new Version.ECBlocks(20, new Version.ECB(1, 30))),
      new Version(8, 24, 24, 22, 22, new Version.ECBlocks(24, new Version.ECB(1, 36))),
      new Version(9, 26, 26, 24, 24, new Version.ECBlocks(28, new Version.ECB(1, 44))),
      new Version(10, 32, 32, 14, 14, new Version.ECBlocks(36, new Version.ECB(1, 62))),
      new Version(11, 36, 36, 16, 16, new Version.ECBlocks(42, new Version.ECB(1, 86))),
      new Version(12, 40, 40, 18, 18, new Version.ECBlocks(48, new Version.ECB(1, 114))),
      new Version(13, 44, 44, 20, 20, new Version.ECBlocks(56, new Version.ECB(1, 144))),
      new Version(14, 48, 48, 22, 22, new Version.ECBlocks(68, new Version.ECB(1, 174))),
      new Version(15, 52, 52, 24, 24, new Version.ECBlocks(42, new Version.ECB(2, 102))),
      new Version(16, 64, 64, 14, 14, new Version.ECBlocks(56, new Version.ECB(2, 140))),
      new Version(17, 72, 72, 16, 16, new Version.ECBlocks(36, new Version.ECB(4, 92))),
      new Version(18, 80, 80, 18, 18, new Version.ECBlocks(48, new Version.ECB(4, 114))),
      new Version(19, 88, 88, 20, 20, new Version.ECBlocks(56, new Version.ECB(4, 144))),
      new Version(20, 96, 96, 22, 22, new Version.ECBlocks(68, new Version.ECB(4, 174))),
      new Version(21, 104, 104, 24, 24, new Version.ECBlocks(56, new Version.ECB(6, 136))),
      new Version(22, 120, 120, 18, 18, new Version.ECBlocks(68, new Version.ECB(6, 175))),
      new Version(23, 132, 132, 20, 20, new Version.ECBlocks(62, new Version.ECB(8, 163))),
      new Version(24, 144, 144, 22, 22, new Version.ECBlocks(62, new Version.ECB(8, 156), new Version.ECB(2, 155))),
      new Version(25, 8, 18, 6, 16, new Version.ECBlocks(7, new Version.ECB(1, 5))),
      new Version(26, 8, 32, 6, 14, new Version.ECBlocks(11, new Version.ECB(1, 10))),
      new Version(27, 12, 26, 10, 24, new Version.ECBlocks(14, new Version.ECB(1, 16))),
      new Version(28, 12, 36, 10, 16, new Version.ECBlocks(18, new Version.ECB(1, 22))),
      new Version(29, 16, 36, 14, 16, new Version.ECBlocks(24, new Version.ECB(1, 32))),
      new Version(30, 16, 48, 14, 22, new Version.ECBlocks(28, new Version.ECB(1, 49)))
    };

    internal sealed class ECBlocks
    {
      private readonly int ecCodewords;
      private readonly Version.ECB[] ecBlocks;

      public ECBlocks(int ecCodewords_0, Version.ECB ecBlocks_1)
      {
        this.ecCodewords = ecCodewords_0;
        this.ecBlocks = new Version.ECB[1]{ ecBlocks_1 };
      }

      public ECBlocks(int ecCodewords_0, Version.ECB ecBlocks1, Version.ECB ecBlocks2)
      {
        this.ecCodewords = ecCodewords_0;
        this.ecBlocks = new Version.ECB[2]
        {
          ecBlocks1,
          ecBlocks2
        };
      }

      internal int GetECCodewords() => this.ecCodewords;

      internal Version.ECB[] GetECBlocks() => this.ecBlocks;
    }

    internal sealed class ECB
    {
      private readonly int count;
      private readonly int dataCodewords;

      public ECB(int count_0, int dataCodewords_1)
      {
        this.count = count_0;
        this.dataCodewords = dataCodewords_1;
      }

      internal int GetCount() => this.count;

      internal int GetDataCodewords() => this.dataCodewords;
    }
  }
}
