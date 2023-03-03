// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Detector.Pdf417Barcode
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using MessagingToolkit.Barcode.Pdf417.Decoder;
using System.Collections.Generic;
using System.Linq;

namespace MessagingToolkit.Barcode.Pdf417.Detector
{
  public sealed class Pdf417Barcode
  {
    private int[][] matrix;
    private int rows;
    private int columns;
    private int ecLevel;
    private int erasures;

    public Pdf417Barcode(int r, int c, int ec)
    {
      this.rows = r;
      this.columns = c;
      this.ecLevel = ec;
      this.matrix = new int[this.columns][];
      for (int index = 0; index < this.columns; ++index)
        this.matrix[index] = new int[this.rows];
      for (int index1 = 0; index1 < this.columns; ++index1)
      {
        for (int index2 = 0; index2 < this.rows; ++index2)
          this.matrix[index1][index2] = -1;
      }
      this.erasures = this.rows * this.columns;
    }

    public void Set(int x, int y, int codeword)
    {
      if (x < 0 || x >= this.columns || y < 0 || y >= this.rows || codeword < 0)
        return;
      if (this.matrix[x][y] == -1)
        --this.erasures;
      this.matrix[x][y] = codeword;
    }

    public void SetGuess(int x, int y, int codeword)
    {
      if (x < 0 || x >= this.columns || y < 0 || y >= this.rows || codeword < 0 || this.matrix[x][y] != -1)
        return;
      this.matrix[x][y] = codeword;
      --this.erasures;
    }

    public int Get(int x, int y) => this.matrix[x][y];

    public int GetRows() => this.rows;

    public int GetColumns() => this.columns;

    public int GetECLevel() => this.ecLevel;

    public int GetErasures() => this.erasures;

    public int[] GetCodewords()
    {
      int[] codewords = new int[this.rows * this.columns];
      for (int index1 = 0; index1 < this.rows; ++index1)
      {
        for (int index2 = 0; index2 < this.columns; ++index2)
        {
          int index3 = index1 * this.columns + index2;
          codewords[index3] = this.matrix[index2][index1] == -1 ? 0 : this.matrix[index2][index1];
        }
      }
      return codewords;
    }

    public bool CorrectErrors(int[] codewords)
    {
      int num1 = 2 << this.ecLevel + 1;
      int num2 = codewords.Length >= 4 ? codewords[0] : throw ChecksumException.Instance;
      if (num2 > ((IEnumerable<int>) codewords).Count<int>() || num2 < 0)
        throw ChecksumException.Instance;
      if (num2 == 0)
      {
        if (num1 >= ((IEnumerable<int>) codewords).Count<int>())
          throw ChecksumException.Instance;
        codewords[0] = ((IEnumerable<int>) codewords).Count<int>() - num1;
      }
      int num3 = new Pdf417RsDecoder().CorrectErrors(codewords, (int[]) null, 0, this.rows * this.columns, this.rows * this.columns - codewords[0]);
      if (num3 < 0)
        return false;
      return num3 <= 0 || true;
    }
  }
}
