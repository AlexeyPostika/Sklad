// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.BitSource
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Common
{
  public sealed class BitSource
  {
    private readonly byte[] bytes;
    private int byteOffset;
    private int bitOffset;

    public BitSource(byte[] bytes) => this.bytes = bytes;

    public int ByteOffset => this.byteOffset;

    public int BitOffset => this.bitOffset;

    public int ReadBits(int numBits)
    {
      if (numBits < 1 || numBits > 32)
        throw new ArgumentException(numBits.ToString());
      int num1 = 0;
      if (this.bitOffset > 0)
      {
        int num2 = 8 - this.bitOffset;
        int num3 = numBits < num2 ? numBits : num2;
        int num4 = num2 - num3;
        num1 = ((int) this.bytes[this.byteOffset] & (int) byte.MaxValue >> 8 - num3 << num4) >> num4;
        numBits -= num3;
        this.bitOffset += num3;
        if (this.bitOffset == 8)
        {
          this.bitOffset = 0;
          ++this.byteOffset;
        }
      }
      if (numBits > 0)
      {
        for (; numBits >= 8; numBits -= 8)
        {
          num1 = num1 << 8 | (int) this.bytes[this.byteOffset] & (int) byte.MaxValue;
          ++this.byteOffset;
        }
        if (numBits > 0)
        {
          int num5 = 8 - numBits;
          int num6 = (int) byte.MaxValue >> num5 << num5;
          num1 = num1 << numBits | ((int) this.bytes[this.byteOffset] & num6) >> num5;
          this.bitOffset += numBits;
        }
      }
      return num1;
    }

    public int Available() => 8 * (this.bytes.Length - this.byteOffset) - this.bitOffset;
  }
}
