// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.QRCode.Encoder.ByteMatrix
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System.Text;

namespace MessagingToolkit.Barcode.QRCode.Encoder
{
  public sealed class ByteMatrix
  {
    private readonly byte[][] bytes;
    private readonly int width;
    private readonly int height;

    public ByteMatrix(int width, int height, byte[][] data)
    {
      this.width = width;
      this.height = height;
      this.bytes = data;
    }

    public ByteMatrix(int width, int height)
    {
      this.bytes = new byte[height][];
      for (int index = 0; index < height; ++index)
        this.bytes[index] = new byte[width];
      this.width = width;
      this.height = height;
    }

    public int Height => this.height;

    public int Width => this.width;

    public byte Get(int x, int y) => this.bytes[y][x];

    public byte[][] Array => this.bytes;

    public void Set(int x, int y, byte val) => this.bytes[y][x] = val;

    public void Set(int x, int y, int val) => this.bytes[y][x] = (byte) val;

    public void Set(int x, int y, bool val) => this.bytes[y][x] = val ? (byte) 1 : (byte) 0;

    public void Clear(byte val)
    {
      for (int index1 = 0; index1 < this.height; ++index1)
      {
        for (int index2 = 0; index2 < this.width; ++index2)
          this.bytes[index1][index2] = val;
      }
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(2 * this.width * this.height + 2);
      for (int index1 = 0; index1 < this.height; ++index1)
      {
        for (int index2 = 0; index2 < this.width; ++index2)
        {
          switch (this.bytes[index1][index2])
          {
            case 0:
              stringBuilder.Append(" 0");
              break;
            case 1:
              stringBuilder.Append(" 1");
              break;
            default:
              stringBuilder.Append("  ");
              break;
          }
        }
        stringBuilder.Append('\n');
      }
      return stringBuilder.ToString();
    }
  }
}
