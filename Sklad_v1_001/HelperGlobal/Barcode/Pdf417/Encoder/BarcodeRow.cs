// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Encoder.BarcodeRow
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.Pdf417.Encoder
{
  internal sealed class BarcodeRow
  {
    private readonly byte[] row;
    private int currentLocation;

    internal BarcodeRow(int width)
    {
      this.row = new byte[width];
      this.currentLocation = 0;
    }

    internal void Set(int x, byte value) => this.row[x] = value;

    internal void Set(int x, bool black) => this.row[x] = black ? (byte) 1 : (byte) 0;

    internal void AddBar(bool black, int width)
    {
      for (int index = 0; index < width; ++index)
        this.Set(this.currentLocation++, black);
    }

    internal byte[] GetRow() => this.row;

    internal byte[] GetScaledRow(int scale)
    {
      byte[] scaledRow = new byte[this.row.Length * scale];
      for (int index = 0; index < scaledRow.Length; ++index)
        scaledRow[index] = this.row[index / scale];
      return scaledRow;
    }
  }
}
