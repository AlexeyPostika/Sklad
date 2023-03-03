// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.BitArray
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;
using System.Text;

namespace MessagingToolkit.Barcode.Common
{
  public sealed class BitArray
  {
    private int[] bits;
    private int size;
    private static readonly int[] Lookup = new int[37]
    {
      32,
      0,
      1,
      26,
      2,
      23,
      27,
      0,
      3,
      16,
      24,
      30,
      28,
      11,
      0,
      13,
      4,
      7,
      17,
      0,
      25,
      22,
      31,
      15,
      29,
      10,
      12,
      6,
      0,
      21,
      14,
      9,
      5,
      20,
      8,
      19,
      18
    };

    public BitArray()
    {
      this.size = 0;
      this.bits = new int[1];
    }

    public BitArray(int size)
    {
      this.size = size;
      this.bits = BitArray.MakeArray(size);
    }

    public int GetSize() => this.size;

    public int Size => this.size;

    public int GetSizeInBytes() => this.size + 7 >> 3;

    private void EnsureCapacity(int size)
    {
      if (size <= this.bits.Length << 5)
        return;
      int[] destinationArray = BitArray.MakeArray(size);
      Array.Copy((Array) this.bits, 0, (Array) destinationArray, 0, this.bits.Length);
      this.bits = destinationArray;
    }

    public bool Get(int i) => (this.bits[i >> 5] & 1 << i) != 0;

    public bool GetValue(int i) => this.Get(i);

    public void Set(int i) => this.bits[i >> 5] |= 1 << i;

    public void Flip(int i) => this.bits[i >> 5] ^= 1 << i;

    public int GetNextSet(int from)
    {
      if (from >= this.size)
        return this.size;
      int index = from >> 5;
      int num1;
      for (num1 = this.bits[index] & ~((1 << from) - 1); num1 == 0; num1 = this.bits[index])
      {
        if (++index == this.bits.Length)
          return this.size;
      }
      int num2 = (index << 5) + BitArray.NumberOfTrailingZeros(num1);
      return num2 <= this.size ? num2 : this.size;
    }

    public int GetNextUnset(int from)
    {
      if (from >= this.size)
        return this.size;
      int index = from >> 5;
      int num1;
      for (num1 = ~this.bits[index] & ~((1 << from) - 1); num1 == 0; num1 = ~this.bits[index])
      {
        if (++index == this.bits.Length)
          return this.size;
      }
      int num2 = (index << 5) + BitArray.NumberOfTrailingZeros(num1);
      return num2 <= this.size ? num2 : this.size;
    }

    private static int NumberOfTrailingZeros(int num)
    {
      int index = (-num & num) % 37;
      if (index < 0)
        index *= -1;
      return BitArray.Lookup[index];
    }

    public void SetBulk(int i, int newBits) => this.bits[i >> 5] = newBits;

    public void SetRange(int start, int end)
    {
      if (end < start)
        throw new ArgumentException();
      if (end == start)
        return;
      --end;
      int num1 = start >> 5;
      int num2 = end >> 5;
      for (int index1 = num1; index1 <= num2; ++index1)
      {
        int num3 = index1 > num1 ? 0 : start & 31;
        int num4 = index1 < num2 ? 31 : end & 31;
        int num5;
        if (num3 == 0 && num4 == 31)
        {
          num5 = -1;
        }
        else
        {
          num5 = 0;
          for (int index2 = num3; index2 <= num4; ++index2)
            num5 |= 1 << index2;
        }
        this.bits[index1] |= num5;
      }
    }

    public void Clear()
    {
      int length = this.bits.Length;
      for (int index = 0; index < length; ++index)
        this.bits[index] = 0;
    }

    public bool IsRange(int start, int end, bool val)
    {
      if (end < start)
        throw new ArgumentException();
      if (end == start)
        return true;
      --end;
      int num1 = start >> 5;
      int num2 = end >> 5;
      for (int index1 = num1; index1 <= num2; ++index1)
      {
        int num3 = index1 > num1 ? 0 : start & 31;
        int num4 = index1 < num2 ? 31 : end & 31;
        int num5;
        if (num3 == 0 && num4 == 31)
        {
          num5 = -1;
        }
        else
        {
          num5 = 0;
          for (int index2 = num3; index2 <= num4; ++index2)
            num5 |= 1 << index2;
        }
        if ((this.bits[index1] & num5) != (val ? num5 : 0))
          return false;
      }
      return true;
    }

    public void AppendBit(bool bit)
    {
      this.EnsureCapacity(this.size + 1);
      if (bit)
        this.bits[this.size >> 5] |= 1 << this.size;
      ++this.size;
    }

    public void AppendBits(int val, int numBits)
    {
      if (numBits < 0 || numBits > 32)
        throw new ArgumentException("Num bits must be between 0 and 32");
      this.EnsureCapacity(this.size + numBits);
      for (int index = numBits; index > 0; --index)
        this.AppendBit((val >> index - 1 & 1) == 1);
    }

    public void AppendBitArray(BitArray other)
    {
      int size = other.size;
      this.EnsureCapacity(this.size + size);
      for (int i = 0; i < size; ++i)
        this.AppendBit(other.Get(i));
    }

    public void Xor(BitArray other)
    {
      if (this.bits.Length != other.bits.Length)
        throw new ArgumentException("Sizes don't match");
      for (int index = 0; index < this.bits.Length; ++index)
        this.bits[index] ^= other.bits[index];
    }

    public void ToBytes(int bitOffset, byte[] array, int offset, int numBytes)
    {
      for (int index1 = 0; index1 < numBytes; ++index1)
      {
        int num = 0;
        for (int index2 = 0; index2 < 8; ++index2)
        {
          if (this.Get(bitOffset))
            num |= 1 << 7 - index2;
          ++bitOffset;
        }
        array[offset + index1] = (byte) num;
      }
    }

    public int[] GetBitArray() => this.bits;

    public void Reverse()
    {
      int[] numArray = new int[this.bits.Length];
      int size = this.size;
      for (int index = 0; index < size; ++index)
      {
        if (this.Get(size - index - 1))
          numArray[index >> 5] |= 1 << index;
      }
      this.bits = numArray;
    }

    private static int[] MakeArray(int size) => new int[size + 31 >> 5];

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(this.size);
      for (int i = 0; i < this.size; ++i)
      {
        if ((i & 7) == 0)
          stringBuilder.Append(' ');
        stringBuilder.Append(this.Get(i) ? 'X' : '.');
      }
      return stringBuilder.ToString();
    }
  }
}
