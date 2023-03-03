// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.BitMatrix
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.Common
{
  public sealed class BitMatrix
  {
    private readonly int width;
    private readonly int height;
    private readonly int rowSize;
    private readonly int[] bits;

    public BitMatrix(int dimension)
      : this(dimension, dimension)
    {
    }

    public BitMatrix(int width, int height)
    {
      this.width = width >= 1 && height >= 1 ? width : throw new ArgumentException("Both dimensions must be greater than 0");
      this.height = height;
      this.rowSize = width + 31 >> 5;
      this.bits = new int[this.rowSize * height];
      this.TopPadding = 0;
      this.LeftPadding = 0;
      this.ActualHeight = 0;
      this.ActualHeight = 0;
    }

    public bool Get(int x, int y)
    {
      int index = y * this.rowSize + (x >> 5);
      return index < this.bits.Length && ((int) ((uint) this.bits[index] >> x) & 1) != 0;
    }

    public bool GetValue(int x, int y) => this.Get(x, y);

    public void Set(int x, int y) => this.bits[y * this.rowSize + (x >> 5)] |= 1 << x;

    public void SetValue(int x, int y) => this.Set(x, y);

    public void Flip(int x, int y) => this.bits[y * this.rowSize + (x >> 5)] ^= 1 << x;

    public void Clear()
    {
      int length = this.bits.Length;
      for (int index = 0; index < length; ++index)
        this.bits[index] = 0;
    }

    public void SetRegion(int left, int top, int width_0, int height_1)
    {
      if (top < 0 || left < 0)
        throw new ArgumentException("Left and top must be nonnegative");
      if (height_1 < 1 || width_0 < 1)
        throw new ArgumentException("Height and width must be at least 1");
      int num1 = left + width_0;
      int num2 = top + height_1;
      if (num2 > this.height || num1 > this.width)
        throw new ArgumentException("The region must fit inside the matrix");
      for (int index1 = top; index1 < num2; ++index1)
      {
        int num3 = index1 * this.rowSize;
        for (int index2 = left; index2 < num1; ++index2)
          this.bits[num3 + (index2 >> 5)] |= 1 << index2;
      }
    }

    public BitArray GetRow(int y, BitArray row)
    {
      if (row == null || row.GetSize() < this.width)
        row = new BitArray(this.width);
      int num = y * this.rowSize;
      for (int index = 0; index < this.rowSize; ++index)
        row.SetBulk(index << 5, this.bits[num + index]);
      return row;
    }

    public void SetRow(int y, BitArray row) => Array.Copy((Array) row.GetBitArray(), 0, (Array) this.bits, y * this.rowSize, this.rowSize);

    public int[] GetEnclosingRectangle()
    {
      int num1 = this.width;
      int num2 = this.height;
      int num3 = -1;
      int num4 = -1;
      for (int index1 = 0; index1 < this.height; ++index1)
      {
        for (int index2 = 0; index2 < this.rowSize; ++index2)
        {
          int bit = this.bits[index1 * this.rowSize + index2];
          if (bit != 0)
          {
            if (index1 < num2)
              num2 = index1;
            if (index1 > num4)
              num4 = index1;
            if (index2 * 32 < num1)
            {
              int num5 = 0;
              while (bit << 31 - num5 == 0)
                ++num5;
              if (index2 * 32 + num5 < num1)
                num1 = index2 * 32 + num5;
            }
            if (index2 * 32 + 31 > num3)
            {
              int num6 = 31;
              while ((uint) bit >> num6 == 0U)
                --num6;
              if (index2 * 32 + num6 > num3)
                num3 = index2 * 32 + num6;
            }
          }
        }
      }
      int num7 = num3 - num1;
      int num8 = num4 - num2;
      if (num7 < 0 || num8 < 0)
        return (int[]) null;
      return new int[4]{ num1, num2, num7, num8 };
    }

    public int[] GetTopLeftOnBit()
    {
      int index = 0;
      while (index < this.bits.Length && this.bits[index] == 0)
        ++index;
      if (index == this.bits.Length)
        return (int[]) null;
      int num1 = index / this.rowSize;
      int num2 = index % this.rowSize << 5;
      int bit = this.bits[index];
      int num3 = 0;
      while (bit << 31 - num3 == 0)
        ++num3;
      return new int[2]{ num2 + num3, num1 };
    }

    public int[] GetBottomRightOnBit()
    {
      int index = this.bits.Length - 1;
      while (index >= 0 && this.bits[index] == 0)
        --index;
      if (index < 0)
        return (int[]) null;
      int num1 = index / this.rowSize;
      int num2 = index % this.rowSize << 5;
      int bit = this.bits[index];
      int num3 = 31;
      while ((uint) bit >> num3 == 0U)
        --num3;
      return new int[2]{ num2 + num3, num1 };
    }

    public int GetWidth() => this.width;

    public int GetHeight() => this.height;

    public int Width => this.width;

    public int Height => this.height;

    public override bool Equals(object o)
    {
      if (!(o is BitMatrix))
        return false;
      BitMatrix bitMatrix = (BitMatrix) o;
      if (this.width != bitMatrix.width || this.height != bitMatrix.height || this.rowSize != bitMatrix.rowSize || this.bits.Length != bitMatrix.bits.Length)
        return false;
      for (int index = 0; index < this.bits.Length; ++index)
      {
        if (this.bits[index] != bitMatrix.bits[index])
          return false;
      }
      return true;
    }

    public override int GetHashCode()
    {
      int hashCode = 31 * (31 * (31 * this.width + this.width) + this.height) + this.rowSize;
      foreach (int bit in this.bits)
        hashCode = 31 * hashCode + bit;
      return hashCode;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(this.height * (this.width + 1));
      for (int y = 0; y < this.height; ++y)
      {
        for (int x = 0; x < this.width; ++x)
          stringBuilder.Append(this.Get(x, y) ? "X " : "  ");
        stringBuilder.Append('\n');
      }
      return stringBuilder.ToString();
    }

    public int LeftPadding { get; set; }

    public int TopPadding { get; set; }

    public int ActualWidth { get; set; }

    public int ActualHeight { get; set; }
  }
}
