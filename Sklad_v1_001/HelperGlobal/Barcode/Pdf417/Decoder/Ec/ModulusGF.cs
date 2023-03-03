// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Pdf417.Decoder.Ec.ModulusGF
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Pdf417.Decoder.Ec
{
  internal sealed class ModulusGF
  {
    public static ModulusGF PDF417_GF = new ModulusGF(929, 3);
    private int[] expTable;
    private int[] logTable;
    private ModulusPoly zero;
    private ModulusPoly one;
    private int modulus;

    public ModulusGF(int modulus, int generator)
    {
      this.modulus = modulus;
      this.expTable = new int[modulus];
      this.logTable = new int[modulus];
      int num = 1;
      for (int index = 0; index < modulus; ++index)
      {
        this.expTable[index] = num;
        num = num * generator % modulus;
      }
      for (int index = 0; index < modulus - 1; ++index)
        this.logTable[this.expTable[index]] = index;
      this.zero = new ModulusPoly(this, new int[1]);
      this.one = new ModulusPoly(this, new int[1]{ 1 });
    }

    internal ModulusPoly GetZero() => this.zero;

    internal ModulusPoly GetOne() => this.one;

    internal ModulusPoly BuildMonomial(int degree, int coefficient)
    {
      if (degree < 0)
        throw new ArgumentException();
      if (coefficient == 0)
        return this.zero;
      int[] coefficients = new int[degree + 1];
      coefficients[0] = coefficient;
      return new ModulusPoly(this, coefficients);
    }

    internal int Add(int a, int b) => (a + b) % this.modulus;

    internal int Subtract(int a, int b) => (this.modulus + a - b) % this.modulus;

    internal int Exp(int a) => this.expTable[a];

    internal int Log(int a) => a != 0 ? this.logTable[a] : throw new ArgumentException();

    internal int Inverse(int a)
    {
      if (a == 0)
        throw new ArithmeticException();
      return this.expTable[this.modulus - this.logTable[a] - 1];
    }

    internal int Multiply(int a, int b) => a == 0 || b == 0 ? 0 : this.expTable[(this.logTable[a] + this.logTable[b]) % (this.modulus - 1)];

    internal int Size => this.modulus;
  }
}
