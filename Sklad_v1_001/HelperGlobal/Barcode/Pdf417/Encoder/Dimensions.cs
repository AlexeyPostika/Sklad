// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Encoder.Dimensions
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Pdf417.Encoder
{
  public sealed class Dimensions
  {
    private int minCols;
    private int maxCols;
    private int minRows;
    private int maxRows;

    public Dimensions(int minCols, int maxCols, int minRows, int maxRows)
    {
      this.minCols = minCols;
      this.maxCols = maxCols;
      this.minRows = minRows;
      this.maxRows = maxRows;
    }

    public int MinCols => this.minCols;

    public int MaxCols => this.maxCols;

    public int MinRows => this.minRows;

    public int MaxRows => this.maxRows;
  }
}
