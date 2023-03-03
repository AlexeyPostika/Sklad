// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Encoder.BarcodeMatrix
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Pdf417.Encoder
{
  internal sealed class BarcodeMatrix
  {
    private readonly BarcodeRow[] matrix;
    private int currentRow;
    private readonly int height;
    private readonly int width;

    public int Width => this.width;

    public int Height => this.height;

    internal BarcodeMatrix(int height, int width)
    {
      this.matrix = new BarcodeRow[height + 2];
      int index = 0;
      for (int length = this.matrix.Length; index < length; ++index)
        this.matrix[index] = new BarcodeRow((width + 4) * 17 + 1);
      this.width = width * 17;
      this.height = height + 2;
      this.currentRow = 0;
    }

    internal void Set(int x, int y, byte val) => this.matrix[y].Set(x, val);

    internal void SetMatrix(int x, int y, bool black) => this.Set(x, y, black ? (byte) 1 : (byte) 0);

    internal void StartRow() => ++this.currentRow;

    internal BarcodeRow GetCurrentRow() => this.matrix[this.currentRow];

    internal byte[][] GetMatrix() => this.GetScaledMatrix(1, 1);

    internal byte[][] GetScaledMatrix(int scale) => this.GetScaledMatrix(scale, scale);

    internal byte[][] GetScaledMatrix(int xScale, int yScale)
    {
      byte[][] scaledMatrix = new byte[this.height * yScale][];
      for (int index = 0; index < this.height * yScale; ++index)
        scaledMatrix[index] = new byte[this.width * xScale];
      int num = this.height * yScale;
      for (int index = 0; index < num; ++index)
        scaledMatrix[num - index - 1] = this.matrix[index / yScale].GetScaledRow(xScale);
      return scaledMatrix;
    }
  }
}
