// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.DataMatrix.Encoder.DefaultPlacement
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

namespace MessagingToolkit.Barcode.DataMatrix.Encoder
{
  public class DefaultPlacement
  {
    private string codewords;
    protected internal int numrows;
    protected internal int numcols;
    protected internal sbyte[] bits;

    public DefaultPlacement(string codewords, int numcols, int numrows)
    {
      this.codewords = codewords;
      this.numcols = numcols;
      this.numrows = numrows;
      this.bits = new sbyte[numcols * numrows];
      for (int index = 0; index < this.bits.Length; ++index)
        this.bits[index] = (sbyte) -1;
    }

    internal int Numrows => this.numrows;

    internal int Numcols => this.numcols;

    internal sbyte[] Bits => this.bits;

    public virtual bool GetBit(int col, int row) => this.bits[row * this.numcols + col] == (sbyte) 1;

    public virtual void SetBit(int col, int row, bool bit) => this.bits[row * this.numcols + col] = bit ? (sbyte) 1 : (sbyte) 0;

    public virtual bool HasBit(int col, int row) => this.bits[row * this.numcols + col] >= (sbyte) 0;

    public virtual void Place()
    {
      int num = 0;
      int row1 = 4;
      int col1 = 0;
      do
      {
        if (row1 == this.numrows && col1 == 0)
          this.Corner1(num++);
        if (row1 == this.numrows - 2 && col1 == 0 && this.numcols % 4 != 0)
          this.Corner2(num++);
        if (row1 == this.numrows - 2 && col1 == 0 && this.numcols % 8 == 4)
          this.Corner3(num++);
        if (row1 == this.numrows + 4 && col1 == 2 && this.numcols % 8 == 0)
          this.Corner4(num++);
        do
        {
          if (row1 < this.numrows && col1 >= 0 && !this.HasBit(col1, row1))
            this.Utah(row1, col1, num++);
          row1 -= 2;
          col1 += 2;
        }
        while (row1 >= 0 && col1 < this.numcols);
        int row2 = row1 + 1;
        int col2 = col1 + 3;
        do
        {
          if (row2 >= 0 && col2 < this.numcols && !this.HasBit(col2, row2))
            this.Utah(row2, col2, num++);
          row2 += 2;
          col2 -= 2;
        }
        while (row2 < this.numrows && col2 >= 0);
        row1 = row2 + 3;
        col1 = col2 + 1;
      }
      while (row1 < this.numrows || col1 < this.numcols);
      if (this.HasBit(this.numcols - 1, this.numrows - 1))
        return;
      this.SetBit(this.numcols - 1, this.numrows - 1, true);
      this.SetBit(this.numcols - 2, this.numrows - 2, true);
    }

    private void Module(int row, int col, int pos, int bit)
    {
      if (row < 0)
      {
        row += this.numrows;
        col += 4 - (this.numrows + 4) % 8;
      }
      if (col < 0)
      {
        col += this.numcols;
        row += 4 - (this.numcols + 4) % 8;
      }
      int num = (int) this.codewords[pos] & 1 << 8 - bit;
      this.SetBit(col, row, num != 0);
    }

    private void Utah(int row, int col, int pos)
    {
      this.Module(row - 2, col - 2, pos, 1);
      this.Module(row - 2, col - 1, pos, 2);
      this.Module(row - 1, col - 2, pos, 3);
      this.Module(row - 1, col - 1, pos, 4);
      this.Module(row - 1, col, pos, 5);
      this.Module(row, col - 2, pos, 6);
      this.Module(row, col - 1, pos, 7);
      this.Module(row, col, pos, 8);
    }

    private void Corner1(int pos)
    {
      this.Module(this.numrows - 1, 0, pos, 1);
      this.Module(this.numrows - 1, 1, pos, 2);
      this.Module(this.numrows - 1, 2, pos, 3);
      this.Module(0, this.numcols - 2, pos, 4);
      this.Module(0, this.numcols - 1, pos, 5);
      this.Module(1, this.numcols - 1, pos, 6);
      this.Module(2, this.numcols - 1, pos, 7);
      this.Module(3, this.numcols - 1, pos, 8);
    }

    private void Corner2(int pos)
    {
      this.Module(this.numrows - 3, 0, pos, 1);
      this.Module(this.numrows - 2, 0, pos, 2);
      this.Module(this.numrows - 1, 0, pos, 3);
      this.Module(0, this.numcols - 4, pos, 4);
      this.Module(0, this.numcols - 3, pos, 5);
      this.Module(0, this.numcols - 2, pos, 6);
      this.Module(0, this.numcols - 1, pos, 7);
      this.Module(1, this.numcols - 1, pos, 8);
    }

    private void Corner3(int pos)
    {
      this.Module(this.numrows - 3, 0, pos, 1);
      this.Module(this.numrows - 2, 0, pos, 2);
      this.Module(this.numrows - 1, 0, pos, 3);
      this.Module(0, this.numcols - 2, pos, 4);
      this.Module(0, this.numcols - 1, pos, 5);
      this.Module(1, this.numcols - 1, pos, 6);
      this.Module(2, this.numcols - 1, pos, 7);
      this.Module(3, this.numcols - 1, pos, 8);
    }

    private void Corner4(int pos)
    {
      this.Module(this.numrows - 1, 0, pos, 1);
      this.Module(this.numrows - 1, this.numcols - 1, pos, 2);
      this.Module(0, this.numcols - 3, pos, 3);
      this.Module(0, this.numcols - 2, pos, 4);
      this.Module(0, this.numcols - 1, pos, 5);
      this.Module(1, this.numcols - 3, pos, 6);
      this.Module(1, this.numcols - 2, pos, 7);
      this.Module(1, this.numcols - 1, pos, 8);
    }
  }
}
