// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.ReedSolomon.Gf256
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Common.ReedSolomon
{
  public sealed class Gf256
  {
    public static readonly Gf256 QrCodeField = new Gf256(285);
    public static readonly Gf256 DataMatrixField = new Gf256(301);
    private int[] expTable;
    private int[] logTable;
    private GF256Poly zero;
    private GF256Poly one;

    internal GF256Poly Zero => this.zero;

    internal GF256Poly One => this.one;

    private Gf256(int primitive)
    {
      this.expTable = new int[256];
      this.logTable = new int[256];
      int num = 1;
      for (int index = 0; index < 256; ++index)
      {
        this.expTable[index] = num;
        num <<= 1;
        if (num >= 256)
          num ^= primitive;
      }
      for (int index = 0; index < (int) byte.MaxValue; ++index)
        this.logTable[this.expTable[index]] = index;
      this.zero = new GF256Poly(this, new int[1]);
      this.one = new GF256Poly(this, new int[1]{ 1 });
    }

    internal GF256Poly BuildMonomial(int degree, int coefficient)
    {
      if (degree < 0)
        throw new ArgumentException();
      if (coefficient == 0)
        return this.zero;
      int[] coefficients = new int[degree + 1];
      coefficients[0] = coefficient;
      return new GF256Poly(this, coefficients);
    }

    internal static int AddOrSubtract(int a, int b) => a ^ b;

    internal int Exp(int a) => this.expTable[a];

    internal int Log(int a) => a != 0 ? this.logTable[a] : throw new ArgumentException();

    internal int Inverse(int a)
    {
      if (a == 0)
        throw new ArithmeticException();
      return this.expTable[(int) byte.MaxValue - this.logTable[a]];
    }

    internal int Multiply(int a, int b)
    {
      if (a == 0 || b == 0)
        return 0;
      if (a == 1)
        return b;
      return b == 1 ? a : this.expTable[(this.logTable[a] + this.logTable[b]) % (int) byte.MaxValue];
    }
  }
}
