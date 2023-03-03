// Decompiled with JetBrains decompiler
// Type: MessagingToolkit.Barcode.Common.ReedSolomon.GenericGF
// Assembly: MessagingToolkit.Barcode, Version=1.7.0.1, Culture=neutral, PublicKeyToken=4d44dd7196d3c1ac
// MVID: DC7D3202-F545-437B-A4D5-5D45033281E1
// Assembly location: C:\Project\BarcodeTest\BarcodeTest\WpfApp1\DLL\MessagingToolkit.Barcode.dll

using System;

namespace MessagingToolkit.Barcode.Common.ReedSolomon
{
  public sealed class GenericGF
  {
    private const int InitializationThreshold = 0;
    public static readonly GenericGF AztecData12 = new GenericGF(4201, 4096, 1);
    public static readonly GenericGF AztecData10 = new GenericGF(1033, 1024, 1);
    public static readonly GenericGF AztecData6 = new GenericGF(67, 64, 1);
    public static readonly GenericGF AztecParam = new GenericGF(19, 16, 1);
    public static readonly GenericGF QRCodeField256 = new GenericGF(285, 256, 0);
    public static readonly GenericGF DataMatrixField256 = new GenericGF(301, 256, 1);
    public static readonly GenericGF AztecData8 = GenericGF.DataMatrixField256;
    public static readonly GenericGF MaxicodeField64 = GenericGF.AztecData6;
    private int[] expTable;
    private int[] logTable;
    private GenericGFPoly zero;
    private GenericGFPoly one;
    private readonly int size;
    private readonly int primitive;
    private readonly int generatorBase;
    private bool initialized;

    public GenericGF(int primitive, int size, int b)
    {
      this.primitive = primitive;
      this.size = size;
      this.generatorBase = b;
      if (size > 0)
        return;
      this.Initialize();
    }

    private void Initialize()
    {
      this.expTable = new int[this.size];
      this.logTable = new int[this.size];
      int num = 1;
      for (int index = 0; index < this.size; ++index)
      {
        this.expTable[index] = num;
        num <<= 1;
        if (num >= this.size)
          num = (num ^ this.primitive) & this.size - 1;
      }
      for (int index = 0; index < this.size - 1; ++index)
        this.logTable[this.expTable[index]] = index;
      this.zero = new GenericGFPoly(this, new int[1]);
      this.one = new GenericGFPoly(this, new int[1]{ 1 });
      this.initialized = true;
    }

    private void CheckInit()
    {
      if (this.initialized)
        return;
      this.Initialize();
    }

    internal GenericGFPoly GetZero()
    {
      this.CheckInit();
      return this.zero;
    }

    internal GenericGFPoly GetOne()
    {
      this.CheckInit();
      return this.one;
    }

    internal GenericGFPoly BuildMonomial(int degree, int coefficient)
    {
      this.CheckInit();
      if (degree < 0)
        throw new ArgumentException();
      if (coefficient == 0)
        return this.zero;
      int[] coefficients = new int[degree + 1];
      coefficients[0] = coefficient;
      return new GenericGFPoly(this, coefficients);
    }

    internal static int AddOrSubtract(int a, int b) => a ^ b;

    internal int Exp(int a)
    {
      this.CheckInit();
      return this.expTable[a];
    }

    internal int Log(int a)
    {
      this.CheckInit();
      return a != 0 ? this.logTable[a] : throw new ArgumentException();
    }

    internal int Inverse(int a)
    {
      this.CheckInit();
      if (a == 0)
        throw new ArithmeticException();
      return this.expTable[this.size - this.logTable[a] - 1];
    }

    internal int Multiply(int a, int b)
    {
      this.CheckInit();
      return a == 0 || b == 0 ? 0 : this.expTable[(this.logTable[a] + this.logTable[b]) % (this.size - 1)];
    }

    public int GetSize() => this.size;

    public int GetGeneratorBase() => this.generatorBase;

    public override string ToString() => "GF(0x" + this.primitive.ToString("X2") + (object) ',' + (object) this.size + (object) ')';
  }
}
